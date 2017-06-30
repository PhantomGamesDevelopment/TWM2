Total Warfare Mod 2
====

Total Warfare Mod 2 for Tribes 2, Conversion mod built on a base mix of Construction .69a and CCM. Tribes 2 is a game developed on a pre-release version of the Torque Game Engine.

Web Sites/Pages:
* http://www.tribesnext.com : Tribes 2 / TN Page
* https://github.com/PhantomGamesDevelopment/TWM2/ : Official Git Repostitory
* http://www.phantomdev.net : Offical Website

Current Version: 3.91 {Development}

====

Credits:
* Phantom139 (Lead Coder / Mod Developer, Official Host)
* DarknessOfLight (Lead Map Maker / Mod Tester (CoDev))
* Dark Dragon DX (Asset Functions / CoDev)
* Signal360 (Many functions and improvements/ CoDev)
* Dondelium_X (The original Combat Construction Mod (CCM))
* Castiger (Official Secondary Host, Bug Finding, Mod Tester)
* Mostlikely, Construct, JackTL (Construction .70a Developers)

Thanks:
* Thyth (Tribes Next, Merge Tool)
* Krash123 (Tribes Next)
* Linker (Doors, Spawnpoints)
* My Loyal Hosts (For standing behind me and the mod for the entirety of it's length)
* Various Others (Who have contributed to Cons Mod, CCM, TWM, and TWM 2, You have my thanks)
* You (For Downloading, and Contributing.)

====

Install:
* Unzip the TWM2 Folder to your Tribes 2's Gamedata folder
* Create a link with the target mod -mod TWM2
* Adjust the serverControl.cs if you feel like it
* Run.
* ???
* Profit.

Uninstall: Delete the TWM2 Folder

====

Server Setup:

Provided in this folder is a file named ServerControl.cs. Inside you will find many configurable
objects to modify server settings. For starters, set the host GUID to your GUID. To Do so,
start up the dedicated server. Join it, once in game, type ListGUIDS(); in the server console
to obtain your GUID. Then modify the line: $TWM2::HostGUID = "SetMeUp";, to match your GUID.

TWM2 Servers:

TWM2 uses a inter-server connectivity system known as PGD Connect. This system will allow users who
play TWM2 to transfer their rank/progression as well as load in buildings created in other servers
in your server. While PGD Connect itself is manditory across all TWM2 servers, saving information is
only permitted in what we designate as a "Core Server". This requires the server host to abide by the
TWM2 PGD Connect Core rules established by Phantom139. If you are interested in hosting a server with
this level of authority, please contact Phantom139 on the PGD Forums, or by email.

====

MOD DEVELOPMENT HISTORY (GIT VERSIONS):

PLEASE NOTE: For the Legacy (Pre-GitHub Versions) Changelogs, please see LEGACY CHANGELOG.md

3.9 -> 3.91 (In Progress):
* Reduced the requirement to order missions to the rank of General (49) from Commanding Officer (59)
  * This change also applies to the 'From The Top' Challenge Category
  * In-Game, Missions have been renamed to Operations
* Fixed the incorrect internal tags for Lordranius Trevor
* Lib'd the TWM2 MainControl file and PGD Connect Support files to recover some functions
* The F2 Menu now detects PGD Connected accounts and will no longer show the option to PGD Connect to already connected users
* No more scary scientific notion when performing /checkstats or using the F2 menu to view player experience
* Addressed a security issue regarding players who are "attempting" to load code via universal rank files.
  * Nice try DDDX, but your fem-phantom fantasies will NOT be happening... ;)
* Another boss balancing pass
* Addressed the bug that would allow Vardison 2.0 to summon infinite minions
* Addressed the bug with Vardison 2.0 that made his Shadow Rift invincible
* Removed two un-used game objects that were never completed, the Medal Seal and the UAV Control Panel
  * Note: This has inadvertently introduced a new bug causing the UAMS streak to no longer correctly function, this will be addressed before the official release of 3.91
* Removed a few erraneous exec calls to non-existent files in the mod load script
* Depricated the F2 Inventory Screen (Nobody ever used this)
* Fixed a few bugs in the F2 Menu
  * Incorrect links
  * Missing tags
  * Overlap on some lines of text
* [phantomdev.net] Fixed the CRON setting on the daily challenge script so it runs every day now
* Adjusted the daily challenge script to point to the correct link, effectively re-enabling the system
* Addressed the bug preventing weapon challenge progress from recording
* Flipped the /help public and /help additional roles, now using /help will provide the list of accessors
* Removed all aspects of daily exp recording as we are no longer capping progression in the mod
* Re-did the Challenge Menus in the F2 Menu
  * Renamed Weapon Challenges to just Challenges
  * From this menu, players now select General Tasks or Weapon Challenges
  * Weapon Upgrades are now selected from the Settings Window
  * Added the option to jump between the weapon upgrades and weapon challenge menus
  * Completed Challenges are now highlighted in Green and show the requirement instead of simply stating "done"
    * I did this as more of a nostalgia move, so players can remember their accomplishments!
* TWM2 Challenge System Changes
  * Depricated the Blacklist Challenges, and replaced it with Wargames challenges, which are focused on PvP tasks across all modes.
    * Let's see who can complete some of those "tough" ones :)
  * Completed the Zombie Slaying Challenges, allowing players to earn some nice healthy batches of EXP for slaying zombies
  * Added boss challenges for the bosses that did not have any:
    * Shade Lord
	* Ghost of Fire
	* Commander Stormrider
  * Added in additional challenges for the other categories to help players make some more mod progression
  * Added officer promotion challenges for the remaining officer levels without them
  * Removed erraneous reward notes on the boss challenges, as we removed the "AI Follower" system back in 3.7
  * Addressed the bug preventing the four Vardison challenges for specific difficulty completions from actually completing
  * Internally, Redid the entire challenge system to automate most of it, allowing for eally easy deployment of future challenges
    * Added in some cool new features for these as well for things such as hidden challenges, and embedded additional requirements
* Added in the necessary hooks to enable officer ranks 10 - 15
  * Have fun!
* Re-did the Officer Promotion windows to preview the rewards upcoming at that level
* Added in the capability to "reset" your entire TWM2 progression upon hitting max level of Officer 15 (Max Level)
  * I'm currently considering replacing this with a different system, will be decided as a 3.92 feature
* Depricated the Store and Money systems, these systems will become progression based unlocks for "higher" officer levels (10 - 15)
  * Armor effects will not return
  * Armor flags will return in 3.92
* Fixed the bug with Demon Lord zombies not targeting properly
* Fixed a console warning bug caused by zombie objects despawning and then calling a scan method
* Shifter Zombies now have a randomzied element in their teleport method
* Plasma Torpedo Cannon now requires officer level 9 (Phantom)
* Addressed the bug preventing the challenges for reaching officer ranks from completing
* Weapon Balancing Pass
  * Grapple Hook
    * Slowed down the attacher projectile a bit, lowering the range
	* Grapple hook now requires 60% armor energy and consumes this when firing
	* There is now a 5 second cooldown when firing to prevent for quick escapes

3.8 -> 3.9:
* Progression System Adjustments
  * Daily EXP Cap removed from the mod
  * EXP Gain for PvP has been increased (5 -> 15)
  * Altered EXP gain rates from all zombies based on collected mod data
* Redesigned Lord Vardison to remove crashing problems
  * Still three phases to defeat, but all phases have been adjusted
  * Balanced to be easier, but harder...
  * Four difficulty options that can be set by a global variable
* Removed Dark Archmage Vardison (Was even more so prone to crashes)
* Balancing Pass on Bosses, this primarily focused on boss health, boss speed, and attack damage
  * Shade Lord:
    * Reduced Total HP
	* Reduced Boss Speed
	* Increased Damage Taken by Fire Weapons
	* Altered functioning of Shades (Enjoy your new death :P)
  * Colonel Windshear:
    * Addressed his "Attitude" Problem
* Lib'd the Shade Lord down to three functions, recovering ~40 more functions
* Bosses now track kills, enjoy seeing how "Inefficient" you are ;)
* Fixed a few issues in the PGD Connect system
* Added a TCPConnectionList instance to allow for multiple downloads to occur at once
* Depricated a few unused modules and re-coded a few instances that used these old modules
* Depricated a good amount of the P-Con External Library
* Added two new extremely challenging Missions (Surrounded 2.0, and Invasion) 
* Un-Did the change in 3.7 that caused boss exp rewards to be based upon player input to the fight, now there is a hard 5% damage requirement to earn boss EXP.
* Adjusted the in-game PGD Connect daily challenge system to accept the new format, still need to work out the server-side end of this system.
* Fixed some zombie naming issues
* Addressed the strike fighter chaingun bug
* Congealed all of the help list commands into /help