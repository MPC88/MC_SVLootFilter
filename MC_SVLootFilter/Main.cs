using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace MC_SVLootFilter
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "mc.starvalor.lootfilter";
        public const string pluginName = "SV Loot Filter";
        public const string pluginVersion = "0.2.0";

        private static ConfigEntry<KeyCodeSubset> configInputInc;
        private static ConfigEntry<KeyCodeSubset> configInputDec;
        private static ConfigEntry<KeyCodeSubset> configInputEnableDisable;
        private static ConfigEntry<KeyCodeSubset> configInputLabelsRadarToggle;

        private const string exceptionsFile = ".\\BepInEx\\config\\MC_SVLootFilter.txt";
        private static Dictionary<int, string> rarities = new Dictionary<int, string>()
        {
            {0, "gray"},
            {1, "white"},
            {2, "green"},
            {3, "blue"},
            {4, "purple"}
        };

        public static int filterLevel = 0;

        private static bool loaded = false;
        private static bool filterEnabled = false;
        private static bool inGame = false;
        private static bool showLabelsRadar = false;
        private static List<int> exceptedItemTypes = new List<int>()
        {
            4, // Ship
            5  // Escape Pod
        };
        private static List<int> exceptedItemIDs;

        public void Awake()
        {
            LoadInputsConfig();
            Harmony.CreateAndPatchAll(typeof(Main));
        }

        private void LoadInputsConfig()
        {
            configInputInc = Config.Bind("Input",
                "Increment",
                KeyCodeSubset.F4,
                "Increases filter level.");
            configInputDec = Config.Bind("Input",
                "Decrement",
                KeyCodeSubset.F3,
                "Decreases filter level.");
            configInputEnableDisable = Config.Bind("Input",
                "EnableDisable",
                KeyCodeSubset.F2,
                "Enables/disables the filter.");
            configInputLabelsRadarToggle = Config.Bind("Input",
                "LabelsRadarToggle",
                KeyCodeSubset.F7,
                "Toggles labels and radar icons for filtered items.");
        }

        private void LoadExceptionsConfig()
        {
            if (exceptedItemIDs == null)
                exceptedItemIDs = new List<int>();

            if(!File.Exists(exceptionsFile))
            {
                try
                {
                    StreamWriter sw = new StreamWriter(exceptionsFile);
                    sw.WriteLine("To add an item, type its name on a new line.");
                    sw.WriteLine(Lang.Get(3, 48));
                    sw.Close();
                }
                catch (Exception e)
                {
                    Logger.LogWarning("Failed to create exceptions file: " + e);
                }
                finally
                {
                    exceptedItemIDs.Add(24);
                    loaded = true;
                }
            }
            else
            {
                try
                {
                    StreamReader sr = new StreamReader(exceptionsFile);
                    string[] exceptedItemNames = File.ReadAllLines(exceptionsFile);
                    ItemDB.LoadDatabase();
                    foreach (Item item in AccessTools.StaticFieldRefAccess<ItemDB, List<Item>>("items"))
                        for (int nameI = 1; nameI < exceptedItemNames.Length; nameI++)
                            if (exceptedItemNames[nameI].Trim().ToLower().Equals(item.itemName.ToLower()))
                                exceptedItemIDs.Add(item.id);
                    sr.Close();
                }
                catch (Exception e)
                {
                    Logger.LogWarning("Failed to read exceptions file: " + e);
                }
                finally
                {
                    loaded = true;
                }
            }
        }

        public void Update()
        {
            if(!loaded)
                LoadExceptionsConfig();

            if (GameManager.instance != null)
            {
                // Show message to remind player of filtering on entering "in game" state.
                if (GameManager.instance.inGame && !Main.inGame)
                {
                    inGame = GameManager.instance.inGame;
                    ShowFilterLevelMessage();
                    ShowLabelsMessage();
                    ShowFilterEnableDisableMessage();
                }
                else if (!GameManager.instance.inGame && Main.inGame)
                {
                    inGame = GameManager.instance.inGame;
                }

                // Actually do stuff.
                if (inGame)
                    // Handle inputs
                    Inputs();
            }
        }

        static void Inputs()
        {
            // Toggle on/off
            if (Input.GetKeyDown((KeyCode)configInputEnableDisable.Value))
            {
                filterEnabled = !Main.filterEnabled;
                ShowFilterEnableDisableMessage();
            }

            // Level down
            if (Input.GetKeyDown((KeyCode)configInputDec.Value))
                ChangeLevel(false);

            // Level up
            if (Input.GetKeyDown((KeyCode)configInputInc.Value))
                ChangeLevel(true);

            // Toggle labels/radar
            if (Input.GetKeyDown((KeyCode)configInputLabelsRadarToggle.Value))
            {
                showLabelsRadar = !showLabelsRadar;
                ShowLabelsMessage();
            }
        }

        static void ChangeLevel(bool up)
        {
            if (up && filterLevel < 4)
                filterLevel++;
            else if (!up && filterLevel > 0)
                filterLevel--;
            ShowFilterLevelMessage();
        }

        static void ShowFilterEnableDisableMessage()
        {
            if (filterEnabled)
                SideInfo.AddMsg("<color=yellow>Filtering ENABLED</color>");
            else
                SideInfo.AddMsg("<color=yellow>Filtering </color><color=red>DISABLED</color>");
        }

        static void ShowFilterLevelMessage()
        {
            string color = ItemDB.GetRarityColor(filterLevel);
            if (String.IsNullOrWhiteSpace(color))
                color = "<color=white>";

            SideInfo.AddMsg("<color=yellow>Filtering: </color>" + color + Main.rarities[Main.filterLevel].ToUpper() + "</color><color=yellow> and lower.</color>");
        }

        static void ShowLabelsMessage()
        {
            if (showLabelsRadar)
                SideInfo.AddMsg("<color=yellow>Labels ENABLED</color>");
            else
                SideInfo.AddMsg("<color=yellow>Labels </color> <color=red>DISABLED</color>");
        }

        public static bool IsItemFiltered(Collectible c)
        {
            if (c == null || !filterEnabled)
                return false;

            bool rarityFiltered = false;

            // As mining and savenging loot is dropped at rarity 1 (regardless of actual rarity)
            // here, lookup "default" rarity from ItemDB.
            // This will need to be changed if generic cargo item rarities are ever randomised.
            int rarity = c.rarity;
            if (c.itemType == 3)
                rarity = ItemDB.GetItem(c.itemID).rarity;

            int level = 0;
            while (level <= filterLevel)
            {
                if (rarity <= filterLevel)
                    rarityFiltered = true;
                level++;
            }

            if (rarityFiltered &&
                !exceptedItemTypes.Contains(c.itemType) &&
                !exceptedItemIDs.Contains(c.itemID))
                return true;

            return false;
        }

        private static void FilterCollectible(Collectible c, GameObject minimapIcon)
        {
            c.collectEnabled = false;

            if (!showLabelsRadar)
            {
                c.HideLabel();
                if (minimapIcon != null)
                    minimapIcon.SetActive(false);
            }
            else
                c.ShowLabel();
        }

        private static void UnfilterCollectible(Collectible c)
        {
            c.collectEnabled = true;

            if (c.timeToDestroy > 0f &&
                !exceptedItemIDs.Contains(c.itemID) &&
                !exceptedItemTypes.Contains(c.itemType))
            {
                c.ShowLabel();
            }
        }

        [HarmonyPatch(typeof(Collectible), "Start")]
        [HarmonyPrefix]
        public static void Start_Post(Collectible __instance, ref GameObject ___minimapIcon)
        {
            if (__instance == null)
                return;

            if(IsItemFiltered(__instance))
                FilterCollectible(__instance, ___minimapIcon);
        }

        [HarmonyPatch(typeof(Collectible), "Update")]
        [HarmonyPostfix]
        public static void CollectibleUpdate_Post(Collectible __instance, ref GameObject ___minimapIcon)
        {
            if (__instance == null)
                return;

            if (IsItemFiltered(__instance))
                FilterCollectible(__instance, ___minimapIcon);
            else
                UnfilterCollectible(__instance);
        }

        [HarmonyPatch(typeof(BuffCollectorBeam), "Update")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> BuffCollectorBeamUpdate_Trans(IEnumerable<CodeInstruction> instructions)
        {
            int foundBltUnCnt = 0;
            bool done = false;
            Label? branchTarget = null;

            foreach (CodeInstruction instruction in instructions)
            {
                if (foundBltUnCnt == 1 && branchTarget != null && !done)
                {
                    // Load collectible onto stack
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    // Call is filtered test
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Main), "IsItemFiltered"));
                    // Branch if result is true
                    yield return new CodeInstruction(OpCodes.Brtrue, branchTarget);

                    done = true;
                }
                                
                yield return instruction;

                if (instruction.opcode.Equals(OpCodes.Blt_Un))
                {
                    if (foundBltUnCnt > 0)
                        branchTarget = (Label)instruction.operand;
                    else
                        foundBltUnCnt++;
                }
            }
        }
    }

    public enum KeyCodeSubset
    {
        None = 0,
        Backspace = 8,
        Delete = 0x7F,
        Tab = 9,
        Return = 13,
        Pause = 19,
        Escape = 27,
        Space = 0x20,
        Keypad0 = 0x100,
        Keypad1 = 257,
        Keypad2 = 258,
        Keypad3 = 259,
        Keypad4 = 260,
        Keypad5 = 261,
        Keypad6 = 262,
        Keypad7 = 263,
        Keypad8 = 264,
        Keypad9 = 265,
        KeypadPeriod = 266,
        KeypadDivide = 267,
        KeypadMultiply = 268,
        KeypadMinus = 269,
        KeypadPlus = 270,
        KeypadEnter = 271,
        KeypadEquals = 272,
        UpArrow = 273,
        DownArrow = 274,
        RightArrow = 275,
        LeftArrow = 276,
        Insert = 277,
        Home = 278,
        End = 279,
        PageUp = 280,
        PageDown = 281,
        F1 = 282,
        F2 = 283,
        F3 = 284,
        F4 = 285,
        F5 = 286,
        F6 = 287,
        F7 = 288,
        F8 = 289,
        F9 = 290,
        F10 = 291,
        F11 = 292,
        F12 = 293,
        F13 = 294,
        F14 = 295,
        F15 = 296,
        Alpha0 = 48,
        Alpha1 = 49,
        Alpha2 = 50,
        Alpha3 = 51,
        Alpha4 = 52,
        Alpha5 = 53,
        Alpha6 = 54,
        Alpha7 = 55,
        Alpha8 = 56,
        Alpha9 = 57,
        Exclaim = 33,
        DoubleQuote = 34,
        Hash = 35,
        Dollar = 36,
        Percent = 37,
        Ampersand = 38,
        Quote = 39,
        LeftParen = 40,
        RightParen = 41,
        Asterisk = 42,
        Plus = 43,
        Comma = 44,
        Minus = 45,
        Period = 46,
        Slash = 47,
        Colon = 58,
        Semicolon = 59,
        Less = 60,
        Equals = 61,
        Greater = 62,
        Question = 0x3F,
        At = 0x40,
        LeftBracket = 91,
        Backslash = 92,
        RightBracket = 93,
        Caret = 94,
        Underscore = 95,
        BackQuote = 96,
        A = 97,
        B = 98,
        C = 99,
        D = 100,
        E = 101,
        F = 102,
        G = 103,
        H = 104,
        I = 105,
        J = 106,
        K = 107,
        L = 108,
        M = 109,
        N = 110,
        O = 111,
        P = 112,
        Q = 113,
        R = 114,
        S = 115,
        T = 116,
        U = 117,
        V = 118,
        W = 119,
        X = 120,
        Y = 121,
        Z = 122,
        LeftCurlyBracket = 123,
        Pipe = 124,
        RightCurlyBracket = 125,
        Tilde = 126,
        RightShift = 303,
        LeftShift = 304,
        RightControl = 305,
        LeftControl = 306,
        RightAlt = 307,
        LeftAlt = 308,
        LeftCommand = 310,
        LeftApple = 310,
        LeftWindows = 311,
        RightCommand = 309,
        RightApple = 309,
        RightWindows = 312,
        AltGr = 313,
        Print = 316,
        Break = 318,
    }
}

