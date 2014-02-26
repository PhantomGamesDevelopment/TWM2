Total Warfare Mod 2
====

Total Warfare Mod 2 for Tribes 2, Conversion mod built on a base mix of Construction .69a and CCM. Tribes 2 is a pre 1.0 game of the Torque Game Engine.

Web Sites/Pages:
* https://github.com/PhantomGamesDevelopment/TWM2/ : Official Git Repostitory
* http://www.phantomdev.net - http://www.tacticaluprising.phantomdev.net - OFFICIAL SITES
* http://www.moddb.com/mods/total-warfare-mod-2-advanced-warfare
* http://www.moddb.com/groups/twm-development-team

Current Version: 3.9 (alpha)

Credits:
* Phantom139 (Lead Coder / Mod Developer, Official Host)
* DarknessOfLight (Lead Map Maker / Mod Tester (CoDev))
* Dark Dragon DX (Asset Functions / CoDev)
* Signal360 (Many functions and improvements/ CoDev)
* Dondelium_X (Various CCM Functions)
* Castiger (Official Secondary Host, Bug Finding, Mod Tester)
* Mostlikely, Construct, JackTL (Construction .70a Developers)

Thanks:
* Thyth (Tribes Next, Merge Tool)
* Linker (Doors, Spawnpoints)
* My Loyal Hosts (For standing behind me and the mod for the entirety of it's length)
* Various Others (Who have contributed to Cons Mod, TWM, and TWM 2, You have my thanks)
* You (For Downloading, and Contributing.)

Install:
* Unzip the TWM2 Folder to C:/Dynamix/Tribes2/Gamedata/ (change the links if in a different location)
* Create a link with the target mod -mod TWM2
* Rename serverControl_R.cs to serverControl.cs
* Run.
* ???
* Profit.

Uninstall: Delete the TWM2 Folder

Server Setup:

Provided in this folder is a file named ServerControl.cs. Inside you will find many configurable
objects to modify server settings. For starters, set the host GUID to your GUID. To Do so,
start up the dedicated server. Join it, once in game, type ListGUIDS(); in the server console
to obtain your GUID. Then modify the line: $TWM2::HostGUID = "SetMeUp";, to match your GUID.

MOD DEVELOPMENT LOG:
3.8 -> 3.9:
* Community Led Version (Add Changes From GitHub here: https://github.com/PhantomGamesDevelopment/TWM2/)

3.7 -> 3.8:
* Addressed a problem in the TCPQueryList that would cause some transfers to deadlock and never complete
* Did some prep work for some special zombie related missions.
* Did some serious balancing work on Mr. Trolololol Shade Lord
* Fixed some UE Problems related to Lord Vardison, General Vegenor, and Lord Insignia
* Gave Vardison (and his DA counterpart) a brand new evil hyper laser attack
* Gave The Final Vardison Form a missile spam attack
* Balanced out pretty much all of the weapons to a point that now seems reasonable
* Added extremely challenging Bronze, Silver, Gold, and Titan commendation challenges to every weapon that
 grant loads of EXP. These will be for your players who are loyal to certain guns. :)
* Upgraded the Horde Spawning System to cause some less UE problems and added error checks to pretty much 
 prevent Horde from causing UEs anymore.
* Added a few new Hellclasses to Helljump
* Removed the old Hellclasses that used the gravity weapons removed in V2.5
* Pretty much wrapped everything else up, official mod support at this point is dropped...

3.6 -> 3.7:
* Added a Phase One Vardison Boss - Dark Archmage Vardison
* We've hit the function limit, so I revitalized the chat command system and recovered ~500 functions
* Updated a lot of old code from the mod
* Addressed the bug in the PGD Connect system that would only allow one player to use the system at a 
  time
* At the request of players, I have added a damage to boss system that will give your boss xp award based
 on the amount of damage done to a boss
- SCG Kills count as 0 exp to the admin using it
* Upgraded the Zombie AI a little bit to use less functions and overall be more balanced
* Zombie Lords now drop the Acid Cannon
* Added three new missions to the mission system, these are much harder than their previous counterparts
* Added mission completion medals to the game
* Updated the online rank transmit system - IE: Top 15 list.
* Lots of undocumented changes
 
3.5 -> 3.6:
* Completed the remaining perks in the mod
* Added the Napalm Airstrike killstreak
* Fixed a few bugs in some of the old killstreaks
* Fixed a part of the camera-lock problem in the turret streaks, unfortunately the way tribes 2 is coded
 I don't think the entire bug will ever be fixable.
* Added weekly and monthy challenges to the challenge system
* Added some new Hellclass options to Helljump
* Added some new zombie options to TWM2 Infection
* Added a new endgame boss, The Shade Lord
- This is a really challenging two phase boss with 80000 HP and one hit kill attacks, it will take
   A lot of players to bring him down

3.4 -> 3.5:
* Upgraded the rank storage container to be in a single container rather than 3.
- The system will auto-convert old files to the new format
* Addressed a bug in the Daily Challenge System that would cause challenges to continue beyond expiration
* Balanced some weapons
* Addressed an exploitable method for implementing unfriendly code by means of the saving system
- Thanks to Thyth for pointing this out.

3.3 -> 3.4:
* Daily Challenge System Implemented
* Reworked the entire rank system
- Handled by a quicker and more efficient file system
- PGD Connect updated to support new version
* Implemented Phantom Games Interaction System
- Daily EXP Caps
- Rank / Officer Level Caps
- Other Services (News Panel, ect)

3.2 -> 3.3 (or 3.2 Revised):
* Updated all PGD Services to point to the new domain.

3.1 -> 3.2:
* Undoccumented
 
3.0 -> 3.1:
* Added the Sandstorm MRLS Tank
* A Few fixes addressed

2.9 -> 3.0:
* Modified the PGD Connect Algorith to be a little less required, less conflicts should occur.
* Re added the TWM1 Nalcidic Armor
- Enjoy!
* Horde 2 is now Horde 3
- Added Scoring
- Added The Wave Highlight System
- Made it less challenging to get to wave 50.
* Added The Martyrdom Perk
* Fixed a bug that allowed admins to get killstreaks with /giveGun
* Drastically increased the Centaur Artillery Strike Damage

2.8 -> 2.9:
* Fixed a console spam issue with "Commander Stormrider"
* Removed 2 un-used medals
* Fixed sniper rifles showing in the inventory list when they are disabled
* Added 1 reload weapons
- Added the Model 1887 Shotgun (Requires Instructive Colonel)
- See the new shotgun (Model 1887) or the SA2400 for examples
* Ion Progression removed in Sabotage, Domination, and Wartower game modes
* Redid the weapon reload bar
- It is now math based :P
- Weapons with longer than 1 second delays are no longer cut off
- The reload bar halts if you use an invo while reloading.
-- Weapon Balance Changes (A Large Amount)
- Reduced the damage of the Plasma Torpedo Cannon to 1.5 from 2.2
- Reduced the Range of the Flamethrower
- Reduced the Reload Time of the M1700 Shotgun to 2 seconds from 3 seconds
- Reduced the Reload Time of the SCD343 Shotgun to 4 seconds from 5 seconds
- Increased the Reload Time of the WP400 Shotgun to 4 seconds from 3 seconds
- Reduced the range of the Grapple hook to 50% of what it was
- Removed the Centaur Dual Pistols Weapon
-- Made it an upgrade of the Colt Pistol (Excessive Duality)
- Increased the damage of the R700 Sniper To 0.62 From 0.49
- Increased the Damage of the MP26 SMG to 0.17 from 0.1
- Increased the Damage of the Pg700 SMG to 0.09 from 0.07
- Increased the Damage of the P90 SMG to 0.08 from 0.07
- Increased the Damage of the Mini-Chaingun to 0.06 from 0.035, and the Fusion Mini-Chaingun to 0.07 from 0.05
- Reduced the Reload Time of the MRXX ZC4 MG to 9 seconds from 12 seconds
- Increased the Damage of the M1A4 to 0.09 from 0.07
- Increased the Damage of the ALSWP Sniper to 0.5 from 0.3
* PGD Updates
- Modified the hash of the PGD Authenticator.
- Modified some of the PGD Errors.
* Horde 2 - You now gain XP for completing a wave, the amount is handled by a special formula

2.7 -> 2.8:
* Player Phrases, use /myPhrase to set it
- Working on a parse script to allow them to be set in satellite servers
- For now, they will only 'Stay' if set in a core server
* Patched a few issues with the universal ranks
* Added a few new commands
* Modified a few zombie spawn functions to support missions
* Missions:
- Modified the ordering of missions to provide a short bio of the mission first
- Added the "Surrounded" Mission
* Modified the server assistant (chat bot)
- Allows hosts to change it's name :P
- Added a plothera of things to do with it
-- Curse Filter
-- "Get Weather by Zip Code", "Define Word (In development)", "Get Time"
-- Performs Administrative Functions
-- And much more!

2.6 -> 2.7:
* Ported in a plothera of TWM1 Commands
* Fixed the new chat command system that allowed hosts to disable certain commands
- Logic errors caused the system to ignore the PGD Commands.
* Added additional missions
* Brought back the TWM Infection Game Mode
* Added Armor Flags (Unlocked at Prestige Level 5)
- These are just like the phantom armor flags of TWM1
- Only they apply to all armors.
* PGD Updates (The brunt of 2.7)
- Fixed additional HTML Errors
- Provided By Signal360, the new and enhanced PGD Rank System
-- Sha1 security for rank uploads
-- Better processing of the rank data
-- Core/Satellite determined by a new Username/Password system provided by the servers.
-- Lifted all PGD Bans [For now]
* Started the framework of a new mission start menu (for Ordering Missions)
* Rank Tags (For Roleplay), hosts can use the /RankTags command to toggle rank tags
* Renamed the Rank "General Of The Army"[57] to "Commanding General".
* Patched an issue with new players Rank Files that would cause them to error out
- Players with this issue in their file can join a 2.7 server, and rank up once to fix it.

2.5 -> 2.6:
* Added the MRXX ZC4 Machine Gun
* Prep-Work on additional Scenarios
* Added an instant notification system for attempted hacks
* Modified the anti-tamper system to be a little more sensative (this will not affect your gameplay)
* Re-Patched the rapid team crash
* Patched Players being able to change to team 0
* Modified the system to double check possible EXP super gain hack
* Added Banning from PGD Ranks
* Fixed a Glitch in the rank downloader with HTML Tags.
* Modified functioning with voting.
* Added the Mass EMP Killstreak (20 Kills)
* Added In-complete Upgrades on certain weapons
- Please Note, that some do still remain
* Completed the "Team Gain" Perk
* Reformatted the Inventory list to go by Weapon Type/Rank
* Added a Center Print MOTD Function, check serverControl.cs
* Fixed the 'Endless' Bottom print bug for camera killstreaks
* Airstrikes are now directional based on the direction of the camera
* Air rapier zombies can now be targeted by missile weapons
* Added restrictions to the /VoteBoss Command
- 1 Boss Vote per hour (even if it fails)
- Hosts may disable the usage of the command
* BOSS: Lordranius Trebor's Official EXP Reward Cut in Half to 25000.
* Devs/Hosts can now disable chat commands
* Fixed some of the Spelling errors in the death messages.
* Top Ranks are now only downloaded from PGD
* Hosts can disable boss vote / change map votes

2.4 -> 2.5:
* Scenarios.
* Gravity Weapons Removed
* Patched a few UE Causing aspects with Lord Vardison
* Added The Shadow Armor
* Added The M93 Pistol
* Added The ALSWP Sniper Rifle
* Added The Crimson Hawk Pistol
* Added The Stinger Missile Launcher
* Added The Proton(Plasma) Torpedo Cannon
* Added The P90 SMG
* Added The Satellite Strike Killstreak (8 Kills)
* Fixed the Harbinger HQ Map (It's Playable Now)
* Added More Non-Weapon Challenges
- Prestige Challenge Category Completed
- Sabotage Challenge Category Completed
- Domination Challenge Category Completed
- Horde 2 Challenge Category Completed
- From The Top Challenge Category Completed
* AC130 and Harbingers Wrath Streaks can now detect incoming missiles, they will fire off flares when missiles are detected.
* Added 4 More Prestige Levels, bringing the total to 9.
- Removed the Second Prestige Image, replaced 9 with the PGD Logo, all others downgraded 1
* New Perks:
- Bomb Shadower (Tier 3): In Sabotage, the WP that shows you holding the bomb will not show when this perk is active (Requires the 3 For 3 Sabotage Challenge)
- Double Down (Tier 1): Gain double EXP for every non boss kill (Requires Prestige Level 1)
- Radar Phantom (Tier 2): Active Ability, Jams Sensors (Requires Commander Rank)
- Second Chance (Tier 3): In Horde, you can spend one team revive to respawn (Requires the Army Of 50 Stopped Challenge)
* Modified The Following Vehicles:
- Modified the AC130 and Harbinger Gunships to both have gunner seats, the pilot switches the turret weapons when there is no gunner.
- Combat Helicopter: Added a gunner seat, the gunner controls the turret, otherwise the turret is auto fire.
- Gunship Helicopter: See above, pilot can fire a missile pod with the mine key
- Apache Helicopter: See above, pilot can fire a flare pod with the mine key
* Modified The Following Weapons:
- Desert Eagle Pistol: Added a Spread
- RP-342 Machine Gun: Damage increased to 0.035 from 0.02

2.35 -> 2.4:
* Added additional security measures to the universal system
* The All important reason you downloaded this version:
* FIXED THE RANK FILE GLITCH, HORRAH!
* Added the Fission Bomb Killstreak (only a few will find this.... :P)
* Additional system patches
* Patched the universal rank system to output all error codes, should the rank upload fail.
* Reduced the power on the RSA Laser Rifle: From 1.4 -> 0.6
* Banned Gravity Weapons and Ion Weapons From The War Tower Game Mode

2.3 -> 2.35:
* Patched serious security loopholes:
- Univeral Saved Files will now be scanned on load for corruption
- Key Download and scanning is now handled by PGD and not the Server
- Prestige will check for your rank upon attempting to do so.
- Universal Ranks are now forced to provide the key when uploading 
-- Added /SaveRank and /LoadRank in case the server fails to do so, or you want to
* Weapons can now have a required prestige level
* Added 2 new weapons
- Both require a prestige level to test the new system updates.
* Started the harbinger soldier AI.

2.2 -> 2.3:
* modified the auto-patcher
* Added Officer Ranks
- Reach Master Commander, then hit the reset button
* Added the AC-130
- AC-130 Gunner Streak
* Signal360 Added the following:
- /passVote
- /VoteBoss
- System Ammendments
* Brought back 2 Classic TWM Bosses
- Stormrider
- The Ghost Of Fire 
-- Enjoy, Complements of Phantom139
* Fixed a glitch with weapon challenges.
* Delayed camera based superweapons to prevent the "Lock" Glitch (this can still occur though)
* And some various other unnoted changes

2.1 - 2.2:
* Super Release!
* Added the auto-patcher

2.0 -> 2.1:
* New Guns!
- Javelin
- M4A1
- SCD343
* Universal Rank System Implementation
* More Non-Weapon Challenge Categories
- Boss Challenges
- Blacklist Challenges
* Additional Fixes 

1.9 -> 2.0:
* Christmas Mall 2009 Map
- Decent Sized Map, Good for close and long range battles
- Sabotage, Domination
* Domination Enhancement:
- Mappers can now name areas:
- $Domination::Flag[Num(1, 2, or 3), "Map File Name without .mis"] = "Name"; (Thanks To DoL For This)
* 3 More Killstreaks:
- Plasma Harrier Airstrike: 7 Kills: Airstrike with a remaining plasma harrier
- Gunship Helicopter: 9 Kills: Call in a heavily armed helicopter
- Z-Bomb: 100 Zombie Kills: Wipe out all zombies in a flashing blast
* Killstreak Upgrades:
- Added Host Streak Settings:
-- 1: Current Method: 3 Streaks/Player
-- 2: Old Method: All Streaks Earneds if Unlocked
-- 3: New Method: X Streaks/Player, X Set by host.
-- 4: No Streaks: Simple enough...
- Chopper Gunner: Renamed to Apache Gunner
-- Apache Helicopter has a more powerful chaingun than the Combat Helicopter, slower RoF, but larger splash damage
- Apache Gunner and Harbingers wrath now count to your kills.
* Opened the Non-Weapon Challenges Up
- Special Event Challenges: Challenges to do on "Special" Days.
- Killstreak Challenges: Challenges completed for using killstreak rewards
* RPG-7 Enhanced
- Now Randomly Moves While Fired
- Requires Rank 50.
* Slingshot Zombie Added (Finally), Pilots Beware
* Explosive Weapons Now Can Count To Weapon Kills
* Helljump Updates
- The Transport is now invincible (bug fix)
- Players who jump too early out will be placed back in the transport

1.8 -> 1.9:
* Removed the old Killstreak methods.
* Implemented a brand new killstreak system
- 3 Streaks Per Player
- Unlocked via rank/medal
- 4 New Killstreaks
-- Stealth Bomber: 8 Kills: Carpet bomb an area without notice.... until it's too late
-- Apache Gunner: 10 Kills: Control a combat Helicoper's turret for 1 minute
-- Centaur Bombardment: 15 Kills: Call in a massive proton collider bombardment.
-- Arrov IV Nuke Strike: 25 Kills: Boom... you should know what this is.
* Modified some weapons that were implemented in 1.8
* /setRot Added
* Full Scale and Nudge added to the MIST
* /SetNudge added
* Added the Hardline Perk
- Killstreaks require one less kill

1.7 -> 1.8:
* Added the plasmasaber melee weapon.
* Added the UAMS Guided Missile Strike Kill Streak (6 Kills)
* Added the Harbinger's Wrath Kill Streak (10 Kills)
- Hosts, to create a gunship that endlessly circles the map (Gunship support), type StartHarbingersWrath(your CID, 1, 1);
* Helljump: Players can no longer kill the transports.
* Fixed the helicopters on crack glitch.
- If you don't know what this is... lol, start up helljump and call in a heli.
- Hosts, if you have good eyes with script files, you can find the function to make you a chopper gunner (like harbinger's wrath, but with a helicopter)
* DarknessOfLight provided me with the SA2400 Shotgun (Rank 33).
* Added the Wartower game mode (FFA Tower Wars)
- Tower 2009 Limited Edition Map
- Century Maze Map
* Fixed the sabotage bomb explosion (it properly explodes now)
* Fixed the S3S Rifle Glitch (You could shoot yourself :p)
* Shotgun weapons (Excluding the LD06 Savager) now properly make their fire sound
* Added the Silencer Attachment to all guns that have them.
* Ported in TWM's Centaur Dual Pistols.
* Ported in TWM's Desert Eagle Pistol.
* Shotguns now display in the weapons challenge list, but do not have upgrades.
* Fixed a glitch with the RP-432 Machine gun, the sound is now correct, fixed a similar issue on the MG42.
* Signal360 has provided me with a new way of loading/saving universally, I will check it and possibly use it in 1.9.
* Kill triggers can now kill invincible players.
* Added a new trigger, Invincibility Disabler (used in the wartower game mode), to disable your invincibility when you leave the spawn area.
* Kill streaks can now be tracked via the player info page.
* some other things.

1.6 -> 1.7:
* PGD Connect Implemented
- Universal Saving/Loading Added
* Fixes, Fixes, Fixes
* Start of Universal Ranks
* MP-26 CMDO Weapon Added For Helljump
* S3-S Rifle For Helljump

1.5 -> 1.6:
* 9 New Medals (Some are not obtainable yet)
* Hell-Class Selection (With Medal)
* Centaur Artillery Vehicle
* Gravity Axe Weapon
* PRTCLR-995 Weapon
* Weapon Challenge System Upgrade
* KillTrac System Updates
* BOSS: Lordranius Trebor Added
* Fix: Fixed Major Insignia Name, it would display General Vegenor instead of Major Insignia.
* Lobby Options such as kick.ban can now be perfomed on players that are loading
* Admin options now display the name of the admin instead of "The Admin" (Like in TWM 1)
* Some new chat commands

1.4 -> 1.5:
* Hell-Jump Game
* Additional Zombie Namers
* Fixed the Host Setup Glitch
* Fixes, Fixes Fixes
* Online Services now point to PGD.com (www.phantomgamesdevelopment.com)

1.3 rev -> 1.4:
* A various amount of fixes
* Volatile Ravenger Zombies
* Door Fixes
* Grapple Hook Added
* more?

1.3 -> 1.3 Revised:
* Fixed News Panel
* Modified the Score Menu Loadup
* Doors should now save and load properly, no matter the texture (needs testing)
* Fixed an Issue with object moving where inv. stations would not work
* Modified a few other things

1.2 -> 1.3:
* Weapons Challenge System Fixed
* Added a Plothera Of Upgrades
* added a gun or two
* harbinger HQ Tower Map Added
* a few bugs fixed
* bosses Renamed

1.1 -> 1.2 (F.P.R):
- FPR: First Public Release
* More Guns
* 4 new zombie types
* Horde 2 Game
* Improved Many Functioning

1.0 -> 1.1:
* Final 3 Bosses Added
* Added The Domination Game
* Added 3 New Maps (2 By DoL)
* Even more guns added
* Weapon Challenges System
* CSS Improved to 3.1
* Cleaned a few files of un-used functions
* added server logging

Alpha .02 -> 1.0:
* Cleaned over 120 Un-Used Datablocks from the mod
* Moar Guns :)
* More Bosses Added (4 now)
* Added the Perks System
* Added The Sabotage Game
* Fixed many glitches / bugs

Alpha .01 -> Alpha .02:
* Added many chat commands
* Added 10+ Weapons
* Improved the inventory Menu
* Modified the armors
* Added C4 Mines
* Added 2 Bosses
* Various Other Improvements

Cons .70a -> Alpha .01:
* Added CCM's Zombies
* Added CCM's F39 Fighter
* Implemented P-Con 1.7 Functions
* Implemented Clip System
* Implemented Rank System
* Improved / Added New Systems
