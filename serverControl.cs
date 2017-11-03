//SERVER CONTROL SCRIPT
//YOU Can Modify anything In This Script

$TWM2::DisabledMissionTypes = "";  //Horde\tHorde4

$ScoreHudInventory::Active = 0; //0 returns the inv. hud to the normal one

//This global variable can disable the features of PGD Connect on the server. If the service
// goes down at any time, please set this to 1. All PGD Connect Servive info may be viewed on
// the PGD fourms (http://forums.phantomdev.net)
$TWM2::PGDConnectDisabled = 0;
$TWM2::PGDCredentials = "";
$TWM2::RestartOnEmpty = 0;

//CHAT BOT
//Now you can control 'Cynthia', or whatever you wish on naming it
$ChatBot::Name = "Cynthia";   //The Name of the Bot
$ChatBot::Interactions = 1;   //Does the bot chat with you?    (coming in >2.9)
$ChatBot::Itterations = 1;    //Does the bot perform functions?
$ChatBot::AdminCmds = 1;      //Does the bot perform admin commands?
$ChatBot::AntiCurse = 0;      //Is the curse Filter on?
$ChatBot::KickWarnings = 3;   //Default warnings until kick
$ChatBot::MuteWarnings = 1;   //Default warnings until mute
$ChatBot::WarningDecTime = 3; //Time in minutes to warning decrease time
//End

$TWM2::AllyBotsOn = 1; //Enable Ally Bots in the Construction Game Mode

setPerfCounterEnable(0);     //leave this, reduces lag

$TWM2::HostGUID = "SetMeUp"; //Server console to get your GUID

$TWM2::AllowBossVotes = 1; //0 to disable
$TWM2::AllowCMVotes = 1;   //0 to disable change mission votes

$TWM2::HeadshotKill = 1; //Rifle Weapons (Not G41) that HS will insta kill

$TWM2::CloseWhenDone = 0; //set 1 To Close Server if no players are in it (After at least 1 joins)

$TWM2::UnmannedGunship = 0; //1 sets Harbinger's Wrath to call in a auto-turret
                            //this should be 0, it's more fun to BE THE GUNNER :p
$TWM2::GunshipControlTime = 120; //Time in seconds for gunship control, Default 120

$TWM2::UseRankTags = 0; //Set to 1 to have green tags representing client rank attached
                        //to players, enable via the host /RankTags command

$TWM2::AllowSnipers = 1; //Set to 0 to disable, hosts can use /ToggleSniper

//Killstreaks
//Example:
//$Killstreak::Kills["UAV", 0] = 3;
//"UAV" represents the streak, 0 means player kills, 1 for zombie kills
//Specifying -1 will make the killstreak unavailiable for that case, and
//Don't specify 0, the streak will be unreachable. Also take in mind the
//Hardline perk, which makes killstreaks require 1 less kill, so basically
//keep the lowest possible to 2.

//UAV Recon
$Killstreak::Kills["UAV", 0] = 3;
$Killstreak::Kills["UAV", 1] = -1;              //UAV does not detect zombies
//Airstrike
$Killstreak::Kills["Airstrike", 0] = 4;
$Killstreak::Kills["Airstrike", 1] = 20;
//UAMS Guided Missile Strike
$Killstreak::Kills["UAMS", 0] = 5;
$Killstreak::Kills["UAMS", 1] = 25;
//Combat Helicopter
$Killstreak::Kills["Heli", 0] = 6;
$Killstreak::Kills["Heli", 1] = 28;
//Harrier Airstrike
$Killstreak::Kills["Harrier", 0] = 7;
$Killstreak::Kills["Harrier", 1] = 30;
//Satellite Strike (Orbital Laser Strike)
$Killstreak::Kills["SatBomb", 0] = 8;
$Killstreak::Kills["SatBomb", 1] = 33;
//Gunship Helicopter
$Killstreak::Kills["Gunship", 0] = 9;
$Killstreak::Kills["Gunship", 1] = 34;
//Stealth Bomber
$Killstreak::Kills["SlthBomb", 0] = 10;
$Killstreak::Kills["SlthBomb", 1] = 35;
//Harbinger's Wrath
$Killstreak::Kills["Harbins", 0] = 12;
$Killstreak::Kills["Harbins", 1] = 45;
//Apache Gunner
$Killstreak::Kills["ChopperGunner", 0] = 13;
$Killstreak::Kills["ChopperGunner", 1] = 47;
//Ac-130 Gunner
$Killstreak::Kills["AC130", 0] = 15;
$Killstreak::Kills["AC130", 1] = 49;
//Centaur Artillery Bombardment
$Killstreak::Kills["Artillery", 0] = 17;
$Killstreak::Kills["Artillery", 1] = 50;
//Mass EMP
$Killstreak::Kills["MassEMP", 0] = 20;
$Killstreak::Kills["MassEMP", 1] = -1;              //Mass EMP does not effect zombies
//Arrow IV Nuke Strike
$Killstreak::Kills["Nuke", 0] = 25;
$Killstreak::Kills["Nuke", 1] = -1;
//Z-Bomb
$Killstreak::Kills["ZBomb", 1] = 100;
//Fission Bomb
$Killstreak::Kills["Fission", 0] = 45;
//Napalm Airstrike
$Killstreak::Kills["Napalm", 0] = 8;
$Killstreak::Kills["Napalm", 1] = 34;
//


$Killstreak::Setting = 1; //Killstreak Setting:
                          //1. TWM2 1.9 Method (3 streaks + Officer Level/Player) *DEFAULT*
                          //2. TWM2 < 1.9 Method (All Streaks Unlockable [W/Medal])
                          //3. TWM 2.0 > Method (X Streaks/Player, X Set Below)
                          //4. No Streaks
$Killstreak::StreaksPerPlayer = 3;
                          //This will only apply is method 3 is enabled above

$Weather::UseConstantConditionMonitor = 1; //Enable the changing weather script
$Weather::DefaultZipLocation = 61084;      //The Zip Code of that location

$Weather::HourlyUpdate = 1;                //Updates every 'x' hour(s), 1 by default
                                           //NOTE: The script will disregard anything < 1

//Default Domination area names, change them as you will,
//use $Domination::Flag[#,MisName] = "Name"; in a mission file to make mission specific tags
$dFlag[1] = "Alpha";
$dFlag[2] = "Bravo";
$dFlag[3] = "Charlie";

//XP Stuffz
$TWM2::TeamKillDeduct = 10; //Lose this Much For TK
$TWM2::KillXPGain = 15;      //Gain for killing PLAYERS not zombies

$TWM2::MaxZombies = 150; //You can set this now too :)
$TWM2::CanSpawnZ = 1;    //0 Disables Zombie Spawning

$TWM2::ZombieName[1] = "Zombie";
$TWM2::ZombieName[2] = "Ravager Zombie";
$TWM2::ZombieName[3] = "Zombie Lord";
$TWM2::ZombieName[4] = "Demon Zombie";
$TWM2::ZombieName[5] = "Air Rapier Zombie";
$TWM2::ZombieName[6] = "Demon Lord Zombie";
$TWM2::ZombieName[7] = "Lord Yvex";                   //Yvex
$TWM2::ZombieName[8] = CollapseEscape("\c7Lord Rog"); //Rog
$TWM2::ZombieName[9] = "Shifter Zombie";
$TWM2::ZombieName[10] = "Zombie Summoner";
$TWM2::ZombieName[11] = "Sniper Zombie";
$TWM2::ZombieName[12] = "Ultra Demon Zombie";
$TWM2::ZombieName[13] = "Volatile Ravager";
$TWM2::ZombieName[14] = "Slingshot AA Zombie";
$TWM2::ZombieName[15] = "Wraith Zombie";
$TWM2::ZombieName[16] = "General Rog";
$TWM2::ZombieName[17] = "Elite Demon";

$TWM2::BossName["Windshear"] = "Colonel Windshear";
$TWM2::BossName["GoL"] = "The Ghost Of Lightning";
$TWM2::BossName["Vegenor"] = "General Vegenor";
$TWM2::BossName["Insignia"] = "Major Insignia";
$TWM2::BossName["Vardison"] = "Lord Vardison";
$TWM2::BossName["Trevor"] = "Lordranius Trevor";
$TWM2::BossName["GoF"] = CollapseEscape("\c7The Ghost Of Fire");
$TWM2::BossName["Stormrider"] = "Commander Stormrider";

//lower left names
$TWM2::BossName["Yvex"] = "Lord Yvex";
$TWM2::BossName["LordRog"] = "Lord Rog";
$TWM2::BossName["CnlWindshear"] = "Colonel Windshear";
$TWM2::BossName["Vegenor"] = "General Vegenor";
$TWM2::BossName["Insignia"] = "Major Insignia";
$TWM2::BossName["Vardison"] = "Lord Vardison";
$TWM2::BossName["Trevor"] = "Lordranius Trevor";
$TWM2::BossName["GhostOfFire"] = "The Ghost Of Fire";
$TWM2::BossName["Stormrider"] = "Cmdr. Stormrider";
$TWM2::BossName["GhostOfLightning"] = "The Ghost Of Lightning";
$TWM2::BossName["Vardison1"] = "Lord Vardison";
$TWM2::BossName["Vardison2"] = "Lord Vardison";
$TWM2::BossName["Vardison3"] = "Lord Vardison";
$TWM2::BossName["ShadeLord"] = "The Shade Lord";
                                                    //-----OFFICIAL VALUES-----\\
$TWM2::ZombieXPAward[1] = 1;                        //           1
$TWM2::ZombieXPAward[2] = 10;                       //           10
$TWM2::ZombieXPAward[3] = 20;                       //           20
$TWM2::ZombieXPAward[4] = 5;                        //           5
$TWM2::ZombieXPAward[5] = 10;                       //           10
$TWM2::ZombieXPAward[6] = 100;                      //           100
$TWM2::ZombieXPAward[9] = 3;                        //           3
$TWM2::ZombieXPAward[10] = 65;                      //           65
$TWM2::ZombieXPAward[11] = 10;                      //           10
$TWM2::ZombieXPAward[12] = 25;                      //           25
$TWM2::ZombieXPAward[13] = 7;                       //           7
$TWM2::ZombieXPAward[14] = 20;                      //           20
$TWM2::ZombieXPAward[15] = 50;                      //           50
$TWM2::ZombieXPAward[16] = 1000;                    //          1000
$TWM2::ZombieXPAward[17] = 25;                      //           25

$TWM2::BossXPAward["Yvex"] = 10000;                 //          10000
$TWM2::BossXPAward["CnlWindshear"] = 12500;         //          12500
$TWM2::BossXPAward["CnlWindshearAlly"] = 1000;      //           1000
$TWM2::BossXPAward["GhostOfLightning"] = 20000;     //          20000
$TWM2::BossXPAward["Vengenor"] = 25000;             //          25000
$TWM2::BossXPAward["Trevor"] = 25000;               //          25000
$TWM2::BossXPAward["LordRog"] = 30000;              //          30000
$TWM2::BossXPAward["Insignia"] = 35000;             //          35000
$TWM2::BossXPAward["Stormrider"] = 45000;           //          45000
$TWM2::BossXPAward["GhostOfFire"] = 65000;          //          65000
$TWM2::BossXPAward["ShadeLord"] = 70000;            //          70000
$TWM2::BossXPAward["Vardison3"] = 75000;            //          75000


$TWM2::HarbingerSentryName[1] = "H1-CMS Drone";
$TWM2::HarbingerSentryName[2] = "H2-APDS Drone";
$TWM2::HarbingerSentryName[3] = "H3-MSEDS Drone";
$TWM2::HarbingerSentryName[4] = "HX-SHADOW Drone";

                                                    //-----OFFICIAL VALUES-----\\
$TWM2::HarbingerDroneXPAward[1] = 20;               //            20
$TWM2::HarbingerDroneXPAward[2] = 50;               //            50
$TWM2::HarbingerDroneXPAward[3] = 175;              //            175
$TWM2::HarbingerDroneXPAward[4] = 300;              //            300

$Host::UseDevelopersList = 1;

$Host::RankSystem = 1;

$Host::UseGlobalBanList = 1;

$TWM2::UseGoreMod = 0; //set this to 1 for bl00d

$Server::MOTD = "Welcome To Total Warfare Mod 2 : Advanced Warfare, Please Enjoy your time, but behave, Advertisers and spam bots are not accepted here and will be punshised with kick/ban.";

//This is center printed when a client enters the game window (Observer)
$Host::ServerPopup = "<just:center><color:ffffff>http://www.phantomdev.net \n Home of TWM2 \n Hosted by: Phantom139";

$TWM2::FFAMode = 0; // the /tkToggle command Sets This too

$Phantom::CSSEnabled = 1;
// CONTENT SAVING SYSTEM

//Obviously don't make this some insanely large number, you'r server will never be able
//to count all the slots in reasonable time (I'd say anything over 150-200 is bad)
$TWM2::PlayerSaveSlots = 15;     //start with this
$TWM2::AdminSaveSlots = 5;       //Admins get 5 additional slots
$TWM2::SuperAdminSaveSlots = 10; //SAs get 10 more than admins

$TWM::CSSTimeSave = 5;
//Time in Minutes to re-allow a building save

$TWM::CSSTimeLoad = 5;
//Time in Minutes to re-allow a building load

$TWM2::RemoveOrphansTime = 180;
//Time in seconds after a player leaves that his/her deployables is deleted


//Weapon Rank Requirements
$TWM2::RankRequire["G41"] = 4;
$TWM2::RankRequire["R700"] = 32;
$TWM2::RankRequire["Pg700"] = 13;
$TWM2::RankRequire["M1"] = 23;
$TWM2::RankRequire["MiniChaingun"] = 28;
$TWM2::RankRequire["Wp400"] = 18;
$TWM2::RankRequire["RSALaserRifle"] = 40;
$TWM2::RankRequire["PulsePhaser"] = 36;
$TWM2::RankRequire["MG42"] = 54;
$TWM2::RankRequire["ConcussionGun"] = 41;
$TWM2::RankRequire["SA2400"] = 34;
$TWM2::RankRequire["DesertEagle"] = 57;
$TWM2::RankRequire["Centaur"] = 46;
$TWM2::RankRequire["RPG7"] = 58;
$TWM2::RankRequire["Javelin"] = 51;
$TWM2::RankRequire["M4A1"] = 15;
$TWM2::RankRequire["SCD343"] = 59;
$TWM2::RankRequire["P90"] = 56;
$TWM2::RankRequire["ALSWP"] = 49;
$TWM2::RankRequire["Stinger"] = 30;
$TWM2::RankRequire["MRXX"] = 35;
$TWM2::RankRequire["NapalmLauncher"] = 42;

$TWM2::ArmorReq["Shadow"] = 35;

//Chat Commands
//You can enable/disable them here
//Public Commands
$TWM2::AllowedCMD["Help"] = 1;
$TWM2::AllowedCMD["cmdHelp"] = 1;
$TWM2::AllowedCMD["nameSlot"] = 1;
$TWM2::AllowedCMD["me"] = 1;
$TWM2::AllowedCMD["me1"] = 1;
$TWM2::AllowedCMD["me2"] = 1;
$TWM2::AllowedCMD["me3"] = 1;
$TWM2::AllowedCMD["me4"] = 1;
$TWM2::AllowedCMD["me5"] = 1;
$TWM2::AllowedCMD["r"] = 1;
$TWM2::AllowedCMD["giveCard"] = 1;
$TWM2::AllowedCMD["takeCard"] = 1;
$TWM2::AllowedCMD["bf"] = 1;
$TWM2::AllowedCMD["getScale"] = 1;
$TWM2::AllowedCMD["getObj"] = 1;
$TWM2::AllowedCMD["pm"] = 1;
$TWM2::AllowedCMD["open"] = 1;
$TWM2::AllowedCMD["openDoor"] = 1;
$TWM2::AllowedCMD["setPass"] = 1;
$TWM2::AllowedCMD["setSpawn"] = 1;
$TWM2::AllowedCMD["clearSpawn"] = 1;
$TWM2::AllowedCMD["delMyPieces"] = 1;
$TWM2::AllowedCMD["name"] = 1;
$TWM2::AllowedCMD["scale"] = 1;
$TWM2::AllowedCMD["objMove"] = 1;
$TWM2::AllowedCMD["del"] = 1;
$TWM2::AllowedCMD["givePieces"] = 1;
$TWM2::AllowedCMD["power"] = 1;
$TWM2::AllowedCMD["hover"] = 1;
$TWM2::AllowedCMD["moveAll"] = 1;
$TWM2::AllowedCMD["radius"] = 1;
$TWM2::AllowedCMD["objPower"] = 1;
$TWM2::AllowedCMD["idea"] = 0; //<- No longer needed
$TWM2::AllowedCMD["timer"] = 1;
$TWM2::AllowedCMD["setRot"] = 1;
$TWM2::AllowedCMD["setNudge"] = 1;
$TWM2::AllowedCMD["getGUID"] = 1;
$TWM2::AllowedCMD["voteBoss"] = 1;
$TWM2::AllowedCMD["loadRank"] = 1;
$TWM2::AllowedCMD["saveRank"] = 1;
$TWM2::AllowedCMD["uload"] = 1;
$TWM2::AllowedCMD["usave"] = 1;
//Rank System Commands
$TWM2::AllowedCMD["checkStats"] = 1;
$TWM2::AllowedCMD["top5"] = 1;
$TWM2::AllowedCMD["top10"] = 1;
//Admin Commands
$TWM2::AllowedCMD["adminCMDS"] = 1;
$TWM2::AllowedCMD["moveMe"] = 1;
$TWM2::AllowedCMD["moveTo"] = 1;
$TWM2::AllowedCMD["kill"] = 1;
$TWM2::AllowedCMD["goto"] = 1;
$TWM2::AllowedCMD["summon"] = 1;
$TWM2::AllowedCMD["removePieces"] = 1;
$TWM2::AllowedCMD["giveOrphans"] = 1;
$TWM2::AllowedCMD["forcePieces"] = 1;
$TWM2::AllowedCMD["myName"] = 1;
$TWM2::AllowedCMD["setName"] = 1;
$TWM2::AllowedCMD["cancelVote"] = 1;
$TWM2::AllowedCMD["A"] = 1;
$TWM2::AllowedCMD["getPos"] = 1;
$TWM2::AllowedCMD["bp"] = 1;
$TWM2::AllowedCMD["cp"] = 1;
$TWM2::AllowedCMD["confiscate"] = 1;
$TWM2::AllowedCMD["gag"] = 1;
$TWM2::AllowedCMD["passVote"] = 1;
$TWM2::AllowedCMD["getDBs"] = 1;           //[CORE HOSTS] Recommend these two are disabled
$TWM2::AllowedCMD["giveGun"] = 1;          //[CORE HOSTS] Recommend these two are disabled
$TWM2::AllowedCMD["twoTeams"] = 1;
$TWM2::AllowedCMD["slap"] = 1;
$TWM2::AllowedCMD["freeze"] = 1;
//Zombie Commands
$TWM2::AllowedCMD["ZCmds"] = 1;
$TWM2::AllowedCMD["buyZPack"] = 1;
$TWM2::AllowedCMD["spawnZ"] = 1;
$TWM2::AllowedCMD["makeZ"] = 1;
$TWM2::AllowedCMD["cure"] = 1;
$TWM2::AllowedCMD["killZombies"] = 1;
//Super Admin Commands
$TWM2::AllowedCMD["saCMDS"] = 1;
$TWM2::AllowedCMD["TkToggle"] = 1;
$TWM2::AllowedCMD["SA"] = 1;
$TWM2::AllowedCMD["MakeSA"] = 1;
$TWM2::AllowedCMD["blowVehs"] = 1;
$TWM2::AllowedCMD["startBoss"] = 1;
$TWM2::AllowedCMD["makePRG"] = 1;
$TWM2::AllowedCMD["override"] = 1;
$TWM2::AllowedCMD["giveWS"] = 1;
$TWM2::AllowedCMD["giveKSSW"] = 1;
$TWM2::AllowedCMD["turrets"] = 1;
$TWM2::AllowedCMD["jail"] = 1;
$TWM2::AllowedCMD["megaSlap"] = 1;
$TWM2::AllowedCMD["zap"] = 1;
//DEV/HOST commands can be used no matter what, thus we ignore them.
