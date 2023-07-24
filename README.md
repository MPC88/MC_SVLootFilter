# MC_SVLootFilter

F2 - enable/disable
F3 - lower filter level
F4 - raise filter level
F7 - toggles labels for filtered drops

Suggested exceptions (note these must match your language file):  
  
Ancient Relic  
Scrap Metal  
Refined Metal  
Base Component  
Fine Component  
Superior Component  
Ardonian Component  
Pirate Heatsink  
Syndicate Cargo Expansion Module  
Nodolo's Combat Mod  
Blueprint  
  
Add these if you plan to refine for extra scrap/refined metal:  
    
Green Ore  
Red Ore  
Grey Ore  


Tigger's list, includes all faction/endgame gear:  

MartinC
martinc
Online

[12:49]Dread Pirate Tigger: Also single player so it's not like you're inconveniencing anyone else.
[12:49]LadyHawk: yeah crew mechanic as it is is bad enough for me to simply accept that having the good crew the game  can offer is blocked for me, so crew reroll lets me access that
[12:49]LadyHawk: just not gotten round to it yet
[12:50]LadyHawk: for testing purposes where i went 'ah fuck need x' i rerolled one, but testing purposes never get saved
[14:58]Unknownsailor: Can I run bepinex if I've already tweaked a DLL?
[15:06]Absrd: Generally yes, I've experimented with it a bunch and have yet to encounter conflicts.
[15:07]Unknownsailor: I just ran the game and bepinex didnt initialize
[15:48]MartinC: As Absrd said, if you've already modified the .dll manually, it shouldn't be a problem unless you've changed something significant that one of the plugins you install will look for (method signatures for example).  Make sure you download the bepinex version linked in this post: ⁠modding⁠
[16:19]Unknownsailor: I used 5.4.21, unzipped direct into steam/steamapps/common/starvalor, nothing
[16:21]Unknownsailor: only folder in bepinex folder is core, config isnt written
[16:24]MartinC: x86
[16:24]MartinC: ?
[16:54]Unknownsailor: x64
[16:55]Unknownsailor: oh wait, is star valor 64 bit?
[16:55]MartinC: nop
[16:55]Unknownsailor: ahhh, thats probably it
[16:56]MartinC: yeah probably 😄
[16:57]Unknownsailor: ive been using 64 bit OS for so long, I just assume 64 bit by default now 🙂
[16:58]MartinC: I and several others have done the same 🙂
[16:59]Unknownsailor: there is goes
[17:02]Unknownsailor: well, crew reroll hasnt crashed the game yet...
[17:11]Unknownsailor: well dang rerolling crew costs lots of money
[17:13]MartinC: You can customise it in the config
[17:13]MartinC: but yeah, I played it safe when I made it.  Probably too safe, but, well, you can customise it in the config 😄
[17:14]Unknownsailor: cant go from white to yellow with it, no?
[17:16]MartinC: No, you can re-roll skills and bouses, but you can't change their rarity, level or learning style (specialised, balanced etc.)
[17:24]Unknownsailor: cant mess with unique crew?
[17:53]MartinC: Nah.  Tbf, I probably should have made it optional with the "don't expect to be able to restore them" disclaimer.
[21:50]Dread Pirate Tigger: THere's a setting file for crew reroll. Costs were made high to keep it 'fair' in normal play throughs, as if you were sending them back to be retrained. If you just want the convenience you can get the prices down to like 1-10 credits for everything.
[23:22]Mike C: I have mine set to 1% of standard.
[04:40]Unknownsailor: I knocked off two zeros on the price
[06:07]MartinC: Yeah, the idea was if you spend ages trying to get a crew member to be what you want, you'll earn credits while you're doing it and can use that to tweak them.  Far less of a time saver, more like occasional pity 😅 .  FYI you can set the prices to 0 or, if you really want, you can make them negative and the mod will pay you for changing crew.  It's not that smart. 
[06:30]MartinC: but yeah, default set up not to change game too much, but you can (hint hint) change it 😉
[06:31]MartinC: It's like a prize for people who actually read the readme :p
[09:03]MartinC: @NiteTerror https://www.nexusmods.com/starvalor/mods/10 until an official solution is made.  It still requires https://www.nexusmods.com/starvalor/mods/14 to be installed to I'm afraid.  I might rework it to use the new crafting recipe save system at some point, but for now you need both.
[18:20]MartinC: Probably still too jank to let anyone know about it, but it seems to work 🤷‍♂️ .

When docked at a station, press the configurable hotkey (default: backspace) to open the auto buy configuration menu on the lobby panel.

Check the box to attempt automatic "full repair" when docking. If you can't afford it, it will just fail with the usual message as if you clicked the button.

For the remaining items, -1 will mean "no action" for an item type. Any other positive whole number will be fulfilled if possible (automatic buying and selling).

The game's "buyX" and "sellX" functions are used, so normal errors should appear. Not tested what happens if you can't afford something.

If the station doesn't stock something i.e. doesn't trade in that thing, a message will appear in the scrolling menu at the bottom right (same place as crew evolution stuff). If the station doesn't have enough of a thing, mod will buy what's available.

https://github.com/MPC88/MC_SVBuyOrders/releases/tag/v0.0.2 
Image
[18:21]MartinC: I do plan to put a button to open and close this menu, but I've spent too many hours battling with that task today, so hotkey it is.
[20:21]Laious: Nice!
[09:16]HerpMcderpinstan: is there a way to make gunners shoot rocks while mining?
[09:16]HerpMcderpinstan: derp wrong channel
[09:18]Dread Pirate Tigger: Actually exactly the right channel in this case.
[09:18]Dread Pirate Tigger: Sec...
[09:19]Dread Pirate Tigger: The spreadsheet has a tab listing various mods hosted on github. But for what you want ...
[09:20]Dread Pirate Tigger: Guide to installing BepInEx. You want the 32 bit version btw.
https://docs.bepinex.dev/articles/user_guide/installation/index.html?tabs=tabid-win

The relevant mod: You just need the one for mining gunners. It sets another attitude per gunner to manual, defense (normal) or mining.
https://github.com/Technological-Singularity/Star-Valor---Minifix
Installing BepInEx | BepInEx Docs
BepInEx documentation and API listing
Image
GitHub
GitHub - Technological-Singularity/Star-Valor---Minifix
Contribute to Technological-Singularity/Star-Valor---Minifix development by creating an account on GitHub.
GitHub - Technological-Singularity/Star-Valor---Minifix
[09:20]Dread Pirate Tigger: @HerpMcderpinstan
[09:21]HerpMcderpinstan: tyty
[09:22]Dread Pirate Tigger: Last I checked it still works, at least as of 2.08. It's one of the few I use since I run big ships a lot.
[01:54]captinjoehenry: Out of curiosity is there any sort of central location with mods for Star Valor?
[01:55]Dread Pirate Tigger: I have a tab on the spreadsheet for mods hosted on github. Not keeping track of stuff like mods on nexus or dll edit mods.
[01:58]captinjoehenry: 👍
[00:31]Dread Pirate Tigger: @Barblegarb 
FYI this loot filter
https://github.com/MPC88/MC_SVLootFilter
GitHub
GitHub - MPC88/MC_SVLootFilter
Contribute to MPC88/MC_SVLootFilter development by creating an account on GitHub.
GitHub - MPC88/MC_SVLootFilter
[00:31]Dread Pirate Tigger: I only turn it on when I'm mining late game, or don't need iron anymore. 😄
[00:32]Dread Pirate Tigger: Also just added it to the spreadsheet, which I thought I'd done ages ago. Derp.
[00:33]MartinC: I hardly use it anymore either 😅 .  Sometimes when I'm scavenging a ship graveyard to stop picking up energy cells.
[00:33]Dread Pirate Tigger: That's another use yeah.
[00:34]Dread Pirate Tigger: End game hunter swarm farming is another solid use for the filter. I haven't done that for a while though.
[00:35]MartinC: That's why I made it originally before the item was added to the game.  But yeah, same, been a while...
[00:36]Dread Pirate Tigger: If anyone wants my exceptions file. It's set up for mining and faction gear. This going in BepInEx/config
[00:37]Dread Pirate Tigger:
To add an item, type its name on a new line.
Ancient Relic
Scrap Metal
Microchips
Robotics
Green Ore
Red Ore
Gray Ore
Blue Crystal
Red Crystal
Green Crystal                           
#Cannon Ammo
#Railgun Slug
#Vulcan Ammo
#Missile Ammo
Drone Parts
Silicon
Iridium
Base Component
Fine Component
Superior Component
Ardonian Component
Transmitter
Crystal Chamber
Thermal Regulator
Upgrade Kit
Improved Upgrade Kit
Miners Reactor
Large Miners Reactor
PMC Collection Beam
Heavy Tractor Beam
PMC Mobile Refinery
Heavy Mining Drone Bay
Miners Laser
Large Ion Reactor
Impulse Drive Mk.V
Heavy Impulse Drive Mk.V
Capital Impulse Drive Mk.V
Syndicate Scanner
Syndicate Expansion
Thorium Warp Drive
Pirate Impulse Drive
Pirate Heavy Impulse Drive
Pirate Capital Impulse Drive
Pirate Speed Booster
Pirate Heavy Booster
Pirate Heat Sink
Pirate Protector
Terran Mantle
Terran Armor
Venghi Light Armor
Venghi Heavy Armor
Venghi Shield Generator
Venghi Heavy Shield Generator
Venghi Charger
Venghi Reactor
Venghi Large Reactor
Venghi Light Blaster
Venghi Heavy Blaster
Venghi Missile Launcher
Venghi Death Ray
Optronic Computer
Optronic Warp Drive
Nodolo's Warp Mod
Nodolo's Combat Mod
X-27 Flak Cannon
Capital Pulse Laser
Capital Laser Beam
Capital Laser Beam Mk. II
Peacemaker
Doom Cannon
D.O. Sweeper Drive
Optronic Warp Drive
Optronic Computer
Mining Drone Bay
Warp Diverter
Flux Capacitor
Asteroid Analyzer 3.0
Debris Analyzer 3.0
Trader's Codex 3.0
Lootfinder 3.0
Pathfinder 3.0
Advanced Titanium Plating
Tritanium Plating
Heavy Tritanium Plating
Zortrium Plating
Heavy Zortrium Plating
Lithrium Plating
Neutronium Plating
Heavy Neutronium Plating
Fullerite Plating
Heavy Fullerite Plating
Nanomorph Plating
Heavy Nanomorph Skin
Xentronium Plating
Heavy Xentronium Plating
Ardonian Plating
Terran Armor
Venghi Light Armor
Venghi Heavy Armor
... (6 lines left)
Collapse
MC_SVLootFilter.txt
3 KB
[00:38]Dread Pirate Tigger: I basically added the mine only metals, crystals, relic, drop only gear, crafting stuff like robotics and microchips then every faction specific item.
[00:38]Dread Pirate Tigger: I think you can just comment out stuff you don't want all the time with #
[00:38]MartinC: Boy, sure is a butt load more stuff than I have 😄
[00:38]Barblegarb: awesome, thanks!
﻿
To add an item, type its name on a new line.  
Ancient Relic  
Scrap Metal  
Microchips  
Robotics  
Green Ore  
Red Ore  
Gray Ore  
Blue Crystal  
Red Crystal  
Green Crystal  
#Cannon Ammo  
#Railgun Slug  
#Vulcan Ammo  
#Missile Ammo  
Drone Parts  
Silicon  
Iridium  
Base Component  
Fine Component  
Superior Component  
Ardonian Component  
Transmitter  
Crystal Chamber  
Thermal Regulator  
Upgrade Kit  
Improved Upgrade Kit  
Miners Reactor  
Large Miners Reactor  
PMC Collection Beam  
Heavy Tractor Beam  
PMC Mobile Refinery  
Heavy Mining Drone Bay  
Miners Laser  
Large Ion Reactor  
Impulse Drive Mk.V  
Heavy Impulse Drive Mk.V  
Capital Impulse Drive Mk.V  
Syndicate Scanner  
Syndicate Expansion  
Thorium Warp Drive  
Pirate Impulse Drive  
Pirate Heavy Impulse Drive  
Pirate Capital Impulse Drive  
Pirate Speed Booster  
Pirate Heavy Booster  
Pirate Heat Sink  
Pirate Protector  
Terran Mantle  
Terran Armor  
Venghi Light Armor  
Venghi Heavy Armor  
Venghi Shield Generator  
Venghi Heavy Shield Generator  
Venghi Charger  
Venghi Reactor  
Venghi Large Reactor  
Venghi Light Blaster  
Venghi Heavy Blaster  
Venghi Missile Launcher  
Venghi Death Ray  
Optronic Computer  
Optronic Warp Drive  
Nodolo's Warp Mod  
Nodolo's Combat Mod  
X-27 Flak Cannon  
Capital Pulse Laser  
Capital Laser Beam  
Capital Laser Beam Mk. II  
Peacemaker  
Doom Cannon  
D.O. Sweeper Drive  
Optronic Warp Drive  
Optronic Computer  
Mining Drone Bay  
Warp Diverter  
Flux Capacitor  
Asteroid Analyzer 3.0  
Debris Analyzer 3.0  
Trader's Codex 3.0  
Lootfinder 3.0  
Pathfinder 3.0  
Advanced Titanium Plating  
Tritanium Plating  
Heavy Tritanium Plating  
Zortrium Plating  
Heavy Zortrium Plating  
Lithrium Plating  
Neutronium Plating  
Heavy Neutronium Plating  
Fullerite Plating  
Heavy Fullerite Plating  
Nanomorph Plating  
Heavy Nanomorph Skin  
Xentronium Plating  
Heavy Xentronium Plating  
Ardonian Plating  
Terran Armor  
Venghi Light Armor  
Venghi Heavy Armor  
Energy Armor  
Heavy Energy Armor  
Improved Repair System  
Collection Beam    
