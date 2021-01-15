Total Warfare Mod 2
====

Total Warfare Mod 2 for Tribes 2, Conversion mod built on a base mix of Construction .69a and CCM. Tribes 2 is a game developed on a pre-release version of the Torque Game Engine.

Web Sites/Pages:
* http://www.tribesnext.com : Tribes 2 / TN Page
* https://github.com/PhantomGamesDevelopment/TWM2/ : Official Git Repostitory

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

====

MOD DEVELOPMENT HISTORY (GIT VERSIONS):

PLEASE NOTE: I've moved all old changelogs into the version_history folder. This file will now only track the current update's changes

3.91 -> 3.92 (In Progress):
* PGD Connect
  * As I have closed down the Phantom Games Development website, PGD Connect services are no longer active, I have removed all functioning of PGD Connect from the mod to allow users to continue to enjoy TWM2 without requiring this.
  * The Mod Load Menu has been updated, removing old links to PGD and now showing the mod's GitHub Repository Page.
  * The following mod features have been depricated and are no longer available due to the removal of PGD Connect:
    * Cross-Server Rank Files
    * Cross-Server Buildings
    * Daily Challenges
    * PGD Events (Including XP Events)
    * News Panel
    * Top-15 Players List
* Zombie Changes
  * Global
    * Massive "spring cleaning" of the zombie code files, fixing a bunch of bad coding practices and a few logic errors.
	* Moved all functioning into a core control script, added additional modifiers and flags to grant more customizability to zombies
	* **WARNING: Only specific zombies are functional at this moment in time**
  * Normal
    * Reduced HP from 280 (2.8) to 200 (2.0)
      * Given damage number reductions in a large amount of the mod's arsenal to balance things, zombie HP needed to be cut across the board for the most part.
  * Ravager
    * Ravagers will now perform ambush style attacks on targets, making them much more challenging
	* Increased the XP reward from killing ravager zombies	
  * Lord
    * Reduced HP from 1800 (18.0) to 1250 (12.5)
    * Modified the behavior of zombie lords
	  * Replaced the acid cannon with an anti-tank photon cannon
	  * Zombie lords will now preferential target enemy ground armor before infantry, and engage their photon cannon on targets
	  * Zombie lords can now activate a defensive barrier to protect themselves and allies from damage temporarily... enjoy this new rage inducing mechanic :)	
  * Demon
    * Reduced HP from 400 (4.0) to 300 (3.0)
    * Increased resistance to fire damage to 1000% from 3%
    * Demon Zombies now light players on fire instead of infecting them on collision
    * Players will be knocked back with a higher force when hit by demon zombies		
  * Air Rapier
	* Modified the damage scalar of missiles to be a OHK on rapier zombies	
  * Demon Lord
    * Reduced HP from 900 (9.0) to 750 (7.5)
    * Cleaned up this script file substantially
	* Replaced the standard lunge with a fire lunge which creates a firey explosion on impact
	* Reduced the hit damage of the demon lord from 0.8 to 0.5
	* Demon Lords, like the regular demons will no longer infect on collision, but set the player on fire instead	
  * Shifter
    * Reduced HP from 280 (2.8) to 225 (2.25)
    * The change to the shifter teleportation in 3.91 made these zombies ridiculously overpowered, they will be tuned down
	* Increased the maximum targeting range of the teleport attack from 200m to 400m
	* Increased the cooldown of the teleport attack from 7 seconds to 12.5 seconds
	* Shifter zombies will now have to "lock down" for a 1.5 seconds before teleporting, during this time they will be easily targetable	
  * Sniper
    * The sniper zombie is now armed with two new weapons. 
	  * The first is an acid sniper rifle which infects players on striking
	  * The second is a rapid fire sidearm that the sniper will use when targets move too close
	* Reduced the health of Sniper Zombies from 400 (4.0) to 250 (2.5)	
* Added Boss Proficiency
  * Hidden challenges embedded in boss fights that award additional experience for completing tough feats
  * For example: Defeat the shade lord without dying by the elemental shades
* Living World Mode
  * Added a new option for admins to toggle (Living World) in the Construction Mode. Players will be able to vote toggle this option
  * This aspect of the update will be coming soon...
* Challenges / Medals
  * All Challenges and Medals are now persistent, such that progress is not lost through officer promotion
    * Go get those bronze, silver, gold, and titan commendations!!!  
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
* General Enhancements
  * Centralized some of the datablocks that were shared across the mod to allow for easier access / modification
  * Centralized the demon lord's missile seeking logic that was used across multiple zombies & bosses to allow for easier access and editing
  * Streamlined all weapon rank, medal checks into one single function
    * Added the capability to restrict via challenge as well
	* Re-did the inventory hud logic to be much cleaner and readable  
  * Modified the vote logic in admin.cs to clean up a ton of redundant if/else paths
  * Re-did the player collision logic in player.cs to make things a whole lot easier to modify in the future
  * Did a pass through all of the weapon files, cleaning the code up and making each unique weapon have its own damage type.
    * Some projectiles are still tied to bosses and killstreaks, these were not altered.
    * For the updated damage types, death messages were added to the system such that PvP kills will be more easily tracked now.
* Gameplay Changes
  * All 'Mini-Boss' Zombies will now have green text in their name.
  * All 'Boss' level enemies will now have gold text in their name.
  * The Helljump 'Oh Lordy' modifier has been changed to 'Reduces the cooldown time of the Zombie Lord's Photon Cannon by 50%'.	
  * The RSA Laser Rifle has been renamed to the 'UR-22 Laser Rifle'
  * The Pistol Weapon Slot is now called the Sidearm Slot
  * Adjusted the mod's XP table:
	* The final rank (Master Commander) now only requires 2,500,000 XP
	* The earlier ranks will now progress much more quickly, with the mid-point now occuring at 14,000 XP instead of 20,000 XP
* General Bug Fixes
  * Addressed the issue when gaining more than 1 million EXP that would result in your total EXP being reset by the difference between 1 million exp and your current exp.
  * Fixed the bug in which picking up weapon clips with an empty weapon would not automatically trigger weapon reload on that weapon.
  * Fixed a typo in the damage types for the M1700 shotgun
* Weapon Balancing Pass (3.9.2)
  * The 3.9.2 content update is bringing with it a full balance pass on the entire mod's arsenal, targeting for the most part weapons that have been underused in my personal opinion.
  * Most of the changes are power up changes (As is my prefered choice of balancing), but a few high end outliers have been tuned down just a bit.
  * Melee Weapons
    * This weapon class is one that needed a pass the most, as soon as players get the blade of vengenace, they drop every other choice and only use the BoV, this is unintended
	* The balance pass here should help to make the other two choices a little more enticing without ruining the power of the BoV for the players loyal to it.
    * Blade of Vengenace
      * Reduced damage from 100.0 to 0.9 (No longer OHKs in PvP)
	    * Assassination is still a OHK
	  * Added a 100.0 damage modifier to all zombie types for Blade of Vengeance
	    * This includes player zombies.
	  * Removed the clause that bosses could not be damaged by the Blade of Vengeance
	    * Bosses now take 6.5 (650 HP) damage from the blade.
    * Plasmasaber
      * Removed the damage flag on player type
	  * Now deals a flat damage of 5.0 to all targets (OHK to all players, low tier zombies), 500 HP damage to bosses.
	  * Reduced the melee attack cooldown from 1.5 seconds to 0.8 seconds.
	  * Holding the Plasmasaber will grant you a protective buff preventing zombie infection from impact damage.
	    * You can still be infected by taking zombie based projectile damage. 
  * Sidearms
    * At the moment, the two statistical outliers in the pool (Crimson Hawk and Pulse Phaser) are a bit too strong, so some minor adjustments will help smooth gameplay and bring the others into play
    * Crimson Hawk Pistol
      * Trevor Haliade's personal sidearm is still pretty high on the lethality end of the spectrum as it pertains to both PvP and PvE
	    * This behavior is intended for PvE, but not for PvP
	  * Reduced pulse damage from 0.23 to 0.17
	    * Assuming all five pulses hit, this equates to 0.85 damage, or 1.2 bursts to kill
      * Increased the spread factor from 5:1000 to 7:1000
        * This combined with the range reduction below will help to keep this weapon in check at the longer ranges	  
	  * Additionally, the pulses travel quite far at the moment, able to reach across one end of the Christmas Mall map to the other, this will be reduced
	    * Reduced the projectile life from 1 second to 0.45 seconds
	  * Added a 1.9 damage modifier to all zombie types
	    * This works out to ~0.32 damage per shot which is about a 40% damage increase against zombies
		* I want the Crimson Hawk to be a great PvE choice, but allow the other sidearms a chance to shine in PvP
    * ES-77 Pulse Phaser
      * Last time I touched this weapon, I nearly started a riot amongst the entire Tribes 2 community, lol.
	    * Let's not do this again, but instead look at some easy to make and good adjustments
	  * After looking at how this weapon has performed between 3.3 and 3.9.1, I feel it needs a slight bump in the damage department
	    * Increased the base damage of the pulse phaser from 0.2 to 0.26
	    * Increased the damage of the phaser blade pulse from 0.21 to 0.3
	  * On the flip side, my analysis of why this weapon was selected so much over all other sidearms boils down to two things.
	    * 1. This is the only sidearm in the mod without a required reload, being completely energy based
		* 2. The pulses have an extensive reach and can strike targets at a very long distance
	  * The following adjustments will be made to counter
	    * Reduced the projectile lifetime from 3 seconds to 0.8 seconds
	    * Increased the projectile base speed from 120 to 165
		  * Note: The Phaser Blades pulse is slightly slower at 160 base speed
	  * The end result with be a stronger pulse phaser at the close range that loses out on the far reach aspect.
    * Desert Eagle
	  * Increased bullet damage from 0.3 to 0.38
	  * Reduced spread factor from 6:1000 to 4:1000
	  * Added a damage multiplier of 2.5 against all Zombie types
  * Sniper Rifles
    * The sniper rifle class has always been one of the stronger ones in TWM2, and I feel no reason to neuter it now.
    * The goal of this update is to tweak the other sniper options to add more viable choices instead of just dropping the R700 as soon as you get it.	
    * G17 Sniper Rifle
      * This heavy marksman rifle for the longest time has stood in the shadow of the almighty R700, I figured it's time to power it up a bit
	  * Increased the direct impact damage of the bullet from 0.25 to 0.55
	  * Reduced the round rechamber time from 1.1 seconds to 0.7 seconds
    * M1 Sniper Rifle
      * As with the G17, the M1 typically goes into the bin once players unlock the R700, this should still be a viable choice in the arsenal
	  * Increased the direct impact damage from 0.3 to 0.65
	  * Reduced the clip reload time from 5 seconds to 4 seconds
	  * Reduced the round rechamber time from 1.1 seconds to 0.9 seconds
    * R700 
      * The monstrous R700 sniper rifle has for the most part, gone completely unchallenged since the release of the mod, and it's time for it to be reigned in.
      * Reduced direct impact damage from 0.62 to 0.6
      * Removed the headshot multiplier on the projectile (The weapon still OHK on headshot, but this will affect high-tier PvE combatants)
      * Increased the ammo per clip from 4 to 7
      * The goal of this adjustment pass is to make the R700 a high-end choice in regards to having moderate damage and the largest magazine size, but allow the other two snipers to be competitive in choice.	  
  * Assault Rifles
    * Assault Rifles have always felt good in TWM2, the strong jack of all trade weapons good for most ranges, while being outperformed in the range of other tools
	* Only minor tweaking is needed here to help some of the unused tools get up to speed
    * G-41
      * For the most part, this rifle has been collecting dust in the bin of unused weapons due to other tools such as the S3 and R700 in the pool
	  * This weapon has been in need of a long coming tweak of power to make it a more viable choice
	    * Increased the bullet impact damage from 0.3 to 0.44
	    * Reduced the round rechamber time from 0.3 seconds to 0.25 seconds
    * M4A1
      * This jack of all trades weapon currently sits as a power outlier in the assault rifle bin due to the various attachments it has
	  * Coupled with a high base round damage, this weapon would have turned into the go-to gun without adjustments
	  * Reduced the damage of the rounds from 0.09 to 0.085
	  * Increased the clip reload time from 4 seconds to 6 seconds
    * Pulse Rifle 
      * As it stands right now, the pulse rifle is one of the weakest weapons in the mod, with the only advantage being the fastest and most accurate semi-auto in the pool
	  * With the enhancements to the G-41, the pulse rifle will also follow suit, which should make it a more viable choice in mid to long range fights
	  * Increased the shot damage from 0.2 to 0.38
	  * Reduced the round rechamber time from 0.3 seconds to 0.2 seconds
    * S3 Rifle
      * The weapon every player starts with in the mod has remained untouched since the first release of the mod.
	  * However, after a long hard thought on this one, I feel as if this weapon has actually been acting more like a sniper rifle than a semi-auto.
	  * With the changes to the G-41 and the Pulse Rifle however, the S3 still needs to be a viable tool, otherwise it will fall flat in the endgame
	  * The following adjustments have been made
	    * Reduced the direct impact damage from 0.7 to 0.44
		* Increased the headshot multiplier from 1.5 to 1.7
		* Removed the OSK from S3 Rifle Headshots (Sorry!)
	    * Reduced the round rechamber time from 0.9 seconds to 0.4 seconds
	    * Removed the delay between round fire and round rechambering (Shaving another 0.2 seconds off the rechamber time)
	    * Increased the spread factor from 3:1000 to 5:1000
	  * The Specialist S3 Rifle (S3S) for Helljump has recieved the same adjustments
  * Shotguns
    * The buckshot tools of TWM2 have always been in a good place in terms of PvP, however I've noticed a sad trend of these being tossed aside in PvE aspects
    * This update will mainly focus on tuning the damage up a bit in PvE, but balancing the range and spread for PvP.
	* M1700
	  * The mod's first shotgun is a powerhouse in close range, with a heavy hitting OHK lurking for anyone foolish enough to come close.
	  * It's a bit strong in PvP right now, but it doesn't do quite enough in PvE
	  * Increased the pellet damage from 0.1125 to 0.13
	  * Increased the clip reload time from 2 seconds to 3 seconds
	  * Increased the damage modifier against zombies from 2.0/3.0 (Depending on the type) to a flat 4.5 across all types
	  * Cut the projectile lifetime from 1 second to 0.15 seconds
	* Wp-400 / LD06 Savager (Sidearm)
	  * The semi-auto powerhouse shotgun is also getting some tuning done on it.
	  * Reduced the projectile lifetime from 1 second to 0.1 seconds (Slightly less range than the M1700)
	  * Increased the spread factor from 10:1000 to 15:1000
	  * Reduced the round rechamber time from 0.9 to 0.5
	  * Added a damage factor of 4.0 to all zombie types
	  * Increased the clip reload time from 4 seconds to 7 seconds
	* SCD-343
	  * This monstrous one-shot machine has been the bane of my close quarters players since I unveiled this weapon.
	  * I have no plans to change this aspect, but to line it up with the other shotguns, I've made some tuning adjustments
	  * Increased the pellet damage from 0.1125 to 0.17
	  * Reduced the projectile lifetime from 1 second to 0.1 seconds
	  * Increased the spread factor from 6:1000 to 10:1000
	  * Added a damage factor of 4.0 to all zombie types
	* SA-2400
	  * The SA-2400 is the wildcard of the shotgun class, firing slugs at a rapid speed versus standard spreads
	  * This weapon has been highly ineffective against zombie combatants, but no longer
	  * Reduced the time between slug shots from 0.2 seconds to 0.14 seconds
	  * Added a damage factor of 5.0 to all zombie types
	  * Reduced the projectile lifetime from 3 seconds (wtf?) to 0.2 seconds
	* Model-1887
	  * The high end of the shotgun spectrum is the 1887, although a statisical look at the weapon really revealed that it was the weakest numerically
	  * Increased the pellet damage from 0.096 to 0.11
	  * Increased the number of pellets from 14 to 18
	  * Reduced the projectile lifetime from 0.5 s to 0.15 seconds
	  * Added a damage factor of 4.0 to all zombie types
  * Other Weapons
    * Flamethrower
	  * Although "pretty" in the effects department, and with a powerful burn effect, the Flamethrower itself is rather weak in terms of damage.
	  * Increased the direct impact damage of fire "bolts" from 0.02 to 0.07.
	    * This is kind of a "first pass" test to see how it feels.
    * Acid Cannon
      * The weapon that players could only obtain from zombie lords was more of a novelty toy players could pick up an fire back at zombies with
      * In 3.9.2, players will find the Acid Cannon as an officer promotion award, so therefore some adjustments were made to this weapon
      * Moved the Zombie Acid pulse out of this weapon, and replaced it with it's own custom projectile
      * The projectile will flash enemy players when stuck by it, and cause an effective radial damage to serve as a lockdown tool
      * The weapon projectile has been given a 3.0 damage modifier across all zombie types to be highly effective against zombies	  