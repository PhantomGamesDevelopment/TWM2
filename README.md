Total Warfare Mod 2
====

Total Warfare Mod 2 for Tribes 2, Conversion mod built on a base mix of Construction .69a and CCM. Tribes 2 is a game developed on a pre-release version of the Torque Game Engine.

Web Sites/Pages:
* http://www.tribesnext.com : Tribes 2 / TN Page
* https://github.com/PhantomGamesDevelopment/TWM2/ : Official Git Repostitory
* http://www.phantomdev.net : Offical Website

Current Version: 3.92 {Development}

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

PLEASE NOTE: I've moved all old changelogs into the version_history folder. This file will now only track the current update's changes

3.91 -> 3.92 (In Progress):
* Zombie Changes
  * Global
    * Massive "spring cleaning" of the zombie code files, fixing a bunch of bad coding practices and a few logic errors.
    * Redid the zombie targeting and movement methods to make them much "smoother"
	  * Scaled down zombie movement times on some types to smooth movement
	  * To compensate for speed, these zombies saw a reduction of total speed to match the factor
	  * This should result in smoother looking movement at the same speed
	* Moved all functioning into a core control script, added additional modifiers and flags to grant more customizability to zombies
	* **WARNING: Only specific zombies are functional at this moment in time**
  * Ravager
    * Ravagers will now perform ambush style attacks on targets, making them much more challenging
	* Increased the XP reward from killing ravager zombies	
  * Lord
    * Modified the behavior of zombie lords
	* Replaced the acid cannon with an anti-tank photon cannon
	* Zombie lords will now preferential target enemy ground armor before infantry, and engage their photon cannon on targets
	* Zombie lords can now activate a defensive barrier to protect themselves and allies from damage temporarily... enjoy this new rage inducing mechanic :)	
* Added Boss Proficiency
  * Hidden challenges embedded in boss fights that award additional experience for completing tough feats
  * For example: Defeat the shade lord without dying by the elemental shades
* Living World Mode
  * Added a new option for admins to toggle (Living World) in the Construction Mode. Players will be able to vote toggle this option
  * This aspect of the update will be coming soon...
* Boss Balancing / Updates:
  * All Bosses
    * Testing a new difficulty system
	  * Boss health damage will now scale to the number of players in the game
	  * Boss attack damage also now scales to the number of players in the game
	    * NOTE: Some attacks are still designated OHK attacks and will not be affected
	  * This will allow solo players a fighting chance to actually fight against the bosses
	  * For the time being, this change will only apply to the Lord Yvex fight, if testing goes well, I will adapt to other bosses
  * Colonel Windshear
    * Addressed a silly bug with the Colonel Windshear fight that caused the platform turrets to be on a different team than the gunship itself
      * This will address the following two problems:
	    * 1. Turrets targeting the gunship allies
		* 2. Gunship allies targeting the boss
	* Colonel Windshear can now call for additional air support during the fight
  * Stormrider 
	* Re-did his ground detection script to "hopefully" eradicate those funny moments when he suicide bombs the ground, ending the fight
  * Lord Vardison 
    * Fixed an erraneous text prompt that would appear when the shadow rift detonated outside of WTF difficulty stating vardison had healed when in fact he did not
* General Bug Fixes
  * Addressed the issue when gaining more than 1 million EXP that would result in your total EXP being reset by the difference between 1 million exp and your current exp.