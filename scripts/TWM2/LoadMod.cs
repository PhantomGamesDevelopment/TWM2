// TWM 2, Mod Load Script, Place any Scripts To Be Executed In Here

echo("********************************************");
echo("********************************************");
echo("******** EXECUTING TWM2 MOD SCRIPTS ********");
echo("********************************************");
echo("********************************************");

                                                    //Mod Systems
exec("serverControl.cs");                           //Server Settings

exec("scripts/TWM2/Systems/Directorate.cs");        //Client Container Objects
exec("scripts/TWM2/Systems/AdvancedRankSystem.cs"); //Adv. Ranks
exec("scripts/TWM2/Systems/MainControl.cs");        //TWM2 Core Functions
exec("scripts/TWM2/Systems/LivingWorldMode.cs");    //Living World Mode (Construction 2.0)
exec("scripts/TWM2/Systems/ObjectDamage.cs");       //Object Damage Functioning
exec("scripts/TWM2/Systems/Scoremenucmds.cs");      //Score Menu
exec("scripts/TWM2/Systems/BossSystem.cs");         //Bosses
exec("scripts/TWM2/Systems/Medals.cs");             //Medals
exec("scripts/TWM2/Systems/NewsPanel.cs");          //Scoremenu News Page
exec("scripts/TWM2/Systems/Perks.cs");              //Special Perks
exec("scripts/TWM2/Systems/WeaponChallenges.cs");   //Weapon Challenges
exec("scripts/TWM2/Systems/NWChallengeIndex.cs");   //Non-Weapon Challenges
exec("scripts/TWM2/Systems/ChallengeMenus.cs");     //F2 Menus for Challenges
exec("scripts/TWM2/Systems/ClientSettings.cs");     //Save Client Settings
exec("scripts/TWM2/Systems/ChatLog.cs");            //Chat / Connection Logging
exec("scripts/TWM2/Systems/ChatBot.cs");            //Chat Monitoring/Commands
exec("scripts/TWM2/Systems/Weather.cs");            //Weather functions
exec("scripts/TWM2/Systems/Keystrokes.cs");         //Insert/Delete functions
exec("scripts/TWM2/Systems/Killstreak.cs");         //Killstreak Superweapons
exec("scripts/TWM2/Missions/MissionCore.cs");       //Missions
exec("scripts/TWM2/Systems/Scoremenucmds.cs");      //Score Menu load again to update the inv. changes
exec("scripts/TWM2/Systems/SuccessiveKills.cs");    //Successive Kills
exec("scripts/TWM2/Systems/PieceData.cs");          //Piece Data, /undo Command
exec("scripts/TWM2/Systems/DChalg.cs");             //Daily Challenges
exec("scripts/TWM2/Systems/ArmorEnergyShields.cs"); //Armor Shields
exec("scripts/TWM2/Systems/weaponModes.cs");        //Global Defines for Weapon Modes
exec("scripts/TWM2/AI/DroneAI.cs");                 //Drones
exec("scripts/TWM2/Systems/HarbingersWrath.cs");    //Harbinger's Wrath

                                                    //Mod Objects
exec("scripts/TWM2/ModObjects/UAMS.cs");            //UAMS Missile Satellite

                                                    //Mod Dependancies

exec("scripts/TWM2/loadmenu.cs");                   //loadscreen
exec("scripts/TWM2/WeaponFunctions.cs");            //TWM2 Weapon Functions
exec("scripts/TWM2/Zombie/LoadZombieScripts.cs");   //TWM2 Zombie Script Load
exec("scripts/TWM2/CustomCamera.cs");               //TWM2 Cameras
exec("scripts/TWM2/CustomArmors.cs");               //TWM2 Armors
exec("scripts/TWM2/ArmorFunctions.cs");             //TWM2 Armors Functions
exec("scripts/TWM2/VehicleReticles.cs");            //Vehicle Reticles

                                                    //Universal Systems
exec("scripts/TWM2/PGDConnect/ConnectionQueue.cs");       //TCP Connection List
exec("scripts/TWM2/PGDConnect/UniversalSupport.cs");      //Support Script
exec("scripts/TWM2/PGDConnect/UniversalSaving_Client.cs");//Saver
exec("scripts/TWM2/PGDConnect/UniversalLoading.cs");      //Loading
exec("scripts/TWM2/PGDConnect/UniversalRanks.cs");        //Ranks
exec("scripts/TWM2/PGDConnect/ServerInteraction.cs");     //Server Connection

schedule(1000, 0, "CheckCore");

                                                   //Exterior Functioning

exec("scripts/TWM2/ExteriorFunctioning/PulseStuff.cs");  //Aid Pulses
exec("scripts/TWM2/ExteriorFunctioning/killTrigger.cs"); //TWM2 Kill Trigger
exec("scripts/TWM2/ExteriorFunctioning/BloodEffects.cs");//TWM2 Gore Mod
exec("scripts/TWM2/ExteriorFunctioning/ProtPatch.cs");   //Alv's CCM Patch
exec("scripts/TWM2/ExteriorFunctioning/PConFunctions.cs");//P-Con Functions 1.7
exec("scripts/TWM2/ExteriorFunctioning/cmdArmor.cs");  //cmdArmor patch

                                                   //Chat Commands

exec("scripts/TWM2/ChatCommands/Public.cs");       //Public CCs
exec("scripts/TWM2/ChatCommands/Admin.cs");        //Admin CCs
exec("scripts/TWM2/ChatCommands/SuperAdmin.cs");   //SuperAdmin CCs
exec("scripts/TWM2/ChatCommands/DevAndHost.cs");   //Dev/Host CCs

                                                   //Server Dependancies
LoadRanksBase();                                   //Load Ranks
DownloadNewsPage();                                //Load News Page

                                                   //Weapons
exec("scripts/weapons/Pistols/Colt.cs");           //Colt Pistol
exec("scripts/weapons/Melee/melee.cs");            //Gun Blade
exec("scripts/weapons/Rifles/S3.cs");              //S3 Combat Rifle
exec("scripts/weapons/Equipment/C4.cs");           //C4 Mines
exec("scripts/weapons/Shotguns/M1700.cs");         //M1700 Shotgun
exec("scripts/weapons/Rifles/G41.cs");             //G41 Semi Auto Rifle
exec("scripts/weapons/Rifles/R700Sniper.cs");      //R700 Sniper Rifle
exec("scripts/weapons/SMGs/MP26.cs");              //MP26 SMG
exec("scripts/weapons/SMGs/Pg700.cs");             //Pg700 SMG
exec("scripts/weapons/Equipment/SWBeaconer.cs");   //Killstreak Superweapon Datablocks
exec("scripts/weapons/Rifles/M1Sniper.cs");        //M1 Sniper Rifle
exec("scripts/weapons/MGs/RP432.cs");              //RP432 Machine Gun
exec("scripts/weapons/Shotguns/Wp400.cs");         //Wp400 Shotgun
reload("scripts/weapons/SMGs/chaingun.cs");        //Mini Chaingun
exec("scripts/weapons/Rifles/RSALaserRifle.cs");   //RSA Laser Rifle
exec("scripts/weapons/Equipment/RPG7.cs");         //RPG-7
exec("scripts/weapons/Melee/BOV.cs");              //Blade Of Vengeance
exec("scripts/weapons/Construction/EditGun.cs");   //Manipulator Tool
exec("scripts/weapons/Pistols/PulsePhaser.cs");    //ES-77 Pulse Phaser
exec("scripts/weapons/Pistols/LD06Savager.cs");    //LD06 Savager
exec("scripts/weapons/Other/IonLauncher.cs");      //LUX-4 Ion Launcher
exec("scripts/weapons/Other/IonRifle.cs");         //LUST Ion Rifle
reload("scripts/weapons/T2Guns/Shocklance.cs");    //Shocklance & Shock Projs
exec("scripts/weapons/MGs/MG42.cs");               //MG42 Machine Gun
exec("scripts/weapons/Other/Flamethrower.cs");     //A|V|X Flamethrower
exec("scripts/weapons/Grenades/staticGrenade.cs"); //Static Grenade
exec("scripts/weapons/Rifles/G17Sniper.cs");       //G17 Sniper Rifle
exec("scripts/weapons/Other/ConcussionGun.cs");    //Concussion Gun
exec("scripts/weapons/Other/ShadowRifle.cs");      //Shadow Rifle
exec("scripts/weapons/Pistols/GrappleHook.cs");    //NeX 4 Grapple Hook
exec("scripts/weapons/Other/MiniCollider.cs");     //PRTCL-995 MCC
exec("scripts/weapons/Rifles/S3S.cs");             //S3-S Combat Rifle
exec("scripts/weapons/SMGs/MP26CMDO.cs");          //MP26-CMDO SMG
exec("scripts/weapons/Melee/Plasmasabre.cs");      //Plasma Saber
exec("scripts/weapons/Shotguns/SA2400.cs");        //SA2400 Shotgun
exec("scripts/weapons/Pistols/DesertEagle.cs");    //Desert Eagle
exec("scripts/weapons/Equipment/Javelin.cs");      //Javelin
exec("scripts/weapons/Shotguns/SCD343.cs");        //SCD343 Shotgun
exec("scripts/weapons/Rifles/M4A1.cs");            //M4A1 Assault Rifle
exec("scripts/weapons/Rifles/PulseRifle.cs");      //Pulse Rifle
exec("scripts/weapons/SMGs/PulseSMG.cs");          //Pulse SMG
exec("scripts/weapons/SMGs/P90.cs");               //P90 SMG
exec("scripts/weapons/Rifles/ALSWPSniper.cs");     //ALSWP Sniper Rifle
exec("scripts/weapons/Other/PlasmaTorpedo.cs");    //Plasma Torpedo Cannon
exec("scripts/weapons/Pistols/M93.cs");            //M93 Pistol
exec("scripts/weapons/Pistols/CrimsonHawk.cs");    //Crimson Hawk Pistol
exec("scripts/weapons/Equipment/Stinger.cs");      //Stinger
exec("scripts/weapons/MGs/MRXX.cs");               //MRXX ZC4 Machine Gun
exec("scripts/weapons/Shotguns/Model1887.cs");     //Model 1887 Shotgun
exec("scripts/weapons/Other/AcidCannon.cs");       //Zombie Lord/Demon Lord Acid Cannon
exec("scripts/weapons/Other/NapalmLauncher.cs");   //ZH7C8 Napalm Launcher

BuildDeconList();  //build decon. list (con tool)

exec("scripts/TWM2/Bosses/LordVardison.cs"); //load him last

echo("Loading custom scripts");
exec("scripts/Customize/CustomScripts.cs");

schedule(5500, 0, "establishPGDConnection");

cleanChallenges();
$ChallengeIndex = 0;
schedule(7000, 0, "downloadChallenges");

echo("********************************************");
echo("********************************************");
echo("************* EXECUTE COMPLETE *************");
echo("********************************************");
echo("********************************************");

//POST LOAD TASKS.....
//OrderStreaks();
