//NWChallengeIndex.cs
//By: Robert C. Fritzen (Phantom139)
//TWM2 3.9.1 Update
//Non-Weapon Challenge Index (Also known as General Tasks, or any challenges that aren't weapon image specific)

//This system requires the use of three global variables, defined below
//$Challenge::Category[%id] = "F2 Menu Name\tF2 Menu Description\tRequired Rank # or Officer #";
//$Challenge::Challenge[%catID, %id] = "Name";
//$Challenge::Info[%name] = "Name\tCategoryID\tEXP Reward\tOther Reward (Text Only)\tDescription (Requirement)";

//If you want to make a multi-tier challenge, then all you need to do is use the same name with an incrementing value
// starting from 1.. for example: UAV1, UAV2, UAV3.
//If you need all of these challenges to show, then you must set $Challenge::IsNotMultiTier[%name] = true;
// However, you should get into the habbit of using the $Challenge::IsNotMultiTier[%name] = true; flag for all challenges
// that you make that are not multi-tier. This will make the menu generation code run faster...

//Additionally, you can flag challenges for additional requirements or other neat little things, here's an example
// of a challenge that is hidden until unlocked
//$Challenge::SetHidden[%catID, %id] = true;
//$Challenge::HiddenMessage[%catID, %id] = "=== CLASSIFIED ===";

//To make a challenge require another challenge to be completed:
//$Challenge::RequiresChallenge[%catID, %id] = "Name";

//You can also embed additional rank requirements for challenges inside of a menu, for example the killstreak menu
// has two officer killstreak challenges:
//$Challenge::SetRequirement[%catID, %id] = "Officer 15";

//For development purposes, challenges can also be disabled here:
//$Challenge::FlagDisabled[%name] = true;


//Phantom Games Development
//see DChalg.cs
$Challenge::Category[1] = "PGD Challenges\tDaily challenges issued by PGD\t-1";

//Killstreaks
$Challenge::Category[2] = "Killstreak Challenges\tTasks for calling in advanced support\t13";

$Challenge::Challenge[2, 0] = "UAV1";
$Challenge::Info["UAV1"] = "UAV Expert I\t2\t500\tNone\tCall in 30 UAV Recon Satellites";
$Challenge::Challenge[2, 1] = "UAV2";
$Challenge::Info["UAV2"] = "UAV Expert II\t2\t1000\tNone\tCall in 75 UAV Recon Satellites";
$Challenge::Challenge[2, 2] = "UAV3";
$Challenge::Info["UAV3"] = "UAV Expert III\t2\t2500\tNone\tCall in 100 UAV Recon Satellites";
$Challenge::Challenge[2, 3] = "Airstrike1";
$Challenge::Info["Airstrike1"] = "Airstrike Expert I\t2\t1000\tNone\tCall in 25 Airstrikes";
$Challenge::Challenge[2, 4] = "Airstrike2";
$Challenge::Info["Airstrike2"] = "Airstrike Expert II\t2\t5000\tNone\tCall in 65 Airstrikes";
$Challenge::Challenge[2, 5] = "Airstrike3";
$Challenge::Info["Airstrike3"] = "Airstrike Expert III\t2\t10000\tNone\tCall in 125 Airstrikes";
$Challenge::Challenge[2, 6] = "UAMS1";
$Challenge::Info["UAMS1"] = "UAMS Expert I\t2\t1000\tNone\tCall in 25 UAMS Strikes";
$Challenge::Challenge[2, 7] = "UAMS2";
$Challenge::Info["UAMS2"] = "UAMS Expert II\t2\t5000\tNone\tCall in 65 UAMS Strikes";
$Challenge::Challenge[2, 8] = "UAMS3";
$Challenge::Info["UAMS3"] = "UAMS Expert III\t2\t10000\tNone\tCall in 125 UAMS Strikes";
$Challenge::Challenge[2, 9] = "Helicopter1";
$Challenge::Info["Helicopter1"] = "Helicopter Expert I\t2\t2500\tNone\tCall in 25 Combat Helicopters";
$Challenge::Challenge[2, 10] = "Helicopter2";
$Challenge::Info["Helicopter2"] = "Helicopter Expert II\t2\t10000\tNone\tCall in 65 Combat Helicopters";
$Challenge::Challenge[2, 11] = "Helicopter3";
$Challenge::Info["Helicopter3"] = "Helicopter Expert III\t2\t12500\tCombat Helicopter Schematics\tCall in 125 Combat Helicopters";
$Challenge::Challenge[2, 12] = "Harrier1";
$Challenge::Info["Harrier1"] = "Harrier Expert I\t2\t2500\tNone\tCall in 20 Plasma Harrier Airstrikes";
$Challenge::Challenge[2, 13] = "Harrier2";
$Challenge::Info["Harrier2"] = "Harrier Expert II\t2\t5000\tNone\tCall in 55 Plasma Harrier Airstrikes";
$Challenge::Challenge[2, 14] = "Harrier3";
$Challenge::Info["Harrier3"] = "Harrier Expert III\t2\t12500\tPlasma Harrier Schematics\tCall in 110 Plasma Harrier Airstrikes";
$Challenge::Challenge[2, 15] = "SatNuke1";
$Challenge::Info["SatNuke1"] = "OLS Expert I\t2\t5000\tNone\tCall in 25 Orbital Laser Strikes";
$Challenge::Challenge[2, 16] = "SatNuke2";
$Challenge::Info["SatNuke2"] = "OLS Expert II\t2\t10000\tNone\tCall in 65 Orbital Laser Strikes";
$Challenge::Challenge[2, 17] = "SatNuke3";
$Challenge::Info["SatNuke3"] = "OLS Expert III\t2\t25000\tNone\tCall in 125 Orbital Laser Strikes";
$Challenge::Challenge[2, 18] = "NapalmHarrier1";
$Challenge::Info["NapalmHarrier1"] = "Napalm Airstrike Expert I\t2\t5000\tNone\tCall in 20 Napalm Airstrikes";
$Challenge::Challenge[2, 19] = "NapalmHarrier2";
$Challenge::Info["NapalmHarrier2"] = "Napalm Airstrike Expert II\t2\t10000\tNone\tCall in 55 Napalm Airstrikes";
$Challenge::Challenge[2, 20] = "NapalmHarrier3";
$Challenge::Info["NapalmHarrier3"] = "Napalm Airstrike Expert III\t2\t25000\tF41 Strike Fighter Schematics\tCall in 110 Napalm Airstrikes";
$Challenge::Challenge[2, 21] = "GunHeli1";
$Challenge::Info["GunHeli1"] = "Gunship Helicopter Expert I\t2\t5000\tNone\tCall in 20 Gunship Helicopters";
$Challenge::Challenge[2, 22] = "GunHeli2";
$Challenge::Info["GunHeli2"] = "Gunship Helicopter Expert II\t2\t10000\tNone\tCall in 55 Gunship Helicopters";
$Challenge::Challenge[2, 23] = "GunHeli3";
$Challenge::Info["GunHeli3"] = "Gunship Helicopter Expert III\t2\t25000\tGunship Helicopter Schematics\tCall in 110 Gunship Helicopters";
$Challenge::Challenge[2, 24] = "SBomber1";
$Challenge::Info["SBomber1"] = "Stealth Bomber Expert I\t2\t5000\tNone\tCall in 20 Stealth Bombers";
$Challenge::Challenge[2, 25] = "SBomber2";
$Challenge::Info["SBomber2"] = "Stealth Bomber Expert II\t2\t10000\tNone\tCall in 50 Stealth Bombers";
$Challenge::Challenge[2, 26] = "SBomber3";
$Challenge::Info["SBomber3"] = "Stealth Bomber Expert III\t2\t25000\tNone\tCall in 100 Stealth Bombers";
$Challenge::Challenge[2, 27] = "Gunship1";
$Challenge::Info["Gunship1"] = "Harbinger's Wrath Expert I\t2\t5000\tNone\tCall in 15 Harbinger Gunships";
$Challenge::Challenge[2, 28] = "Gunship2";
$Challenge::Info["Gunship2"] = "Harbinger's Wrath Expert II\t2\t10000\tNone\tCall in 35 Harbinger Gunships";
$Challenge::Challenge[2, 29] = "Gunship3";
$Challenge::Info["Gunship3"] = "Harbinger's Wrath Expert III\t2\t25000\tAC-130 Vehicle Schematics\tCall in 75 Harbinger Gunships";
$Challenge::Challenge[2, 30] = "ACGunship1";
$Challenge::RequiresChallenge[2, 30] = "Gunship3";
$Challenge::Info["ACGunship1"] = "AC-130 Expert I\t2\t5000\tNone\tCall in 15 AC-130 Gunners";
$Challenge::Challenge[2, 31] = "ACGunship2";
$Challenge::RequiresChallenge[2, 31] = "Gunship3";
$Challenge::Info["ACGunship2"] = "AC-130 Expert II\t2\t10000\tNone\tCall in 35 AC-130 Gunners";
$Challenge::Challenge[2, 32] = "ACGunship3";
$Challenge::RequiresChallenge[2, 32] = "Gunship3";
$Challenge::Info["ACGunship3"] = "AC-130 Expert III\t2\t25000\tBragging Rights... lol.\tCall in 75 AC-130 Gunners";
$Challenge::Challenge[2, 33] = "Apache1";
$Challenge::Info["Apache1"] = "Apache Gunner Expert I\t2\t5000\tNone\tCall in 15 Apache Gunners";
$Challenge::Challenge[2, 34] = "Apache2";
$Challenge::Info["Apache2"] = "Apache Gunner Expert II\t2\t10000\tNone\tCall in 35 Apache Gunners";
$Challenge::Challenge[2, 35] = "Apache3";
$Challenge::Info["Apache3"] = "Apache Gunner Expert III\t2\t25000\tApache Helicopter Schematics\tCall in 75 Apache Gunners";
$Challenge::Challenge[2, 36] = "Centaur1";
$Challenge::Info["Centaur1"] = "Centaur Artillery Expert I\t2\t10000\tNone\tCall in 10 Artillery Strikes";
$Challenge::Challenge[2, 37] = "Centaur2";
$Challenge::Info["Centaur2"] = "Centaur Artillery Expert II\t2\t25000\tNone\tCall in 25 Artillery Strikes";
$Challenge::Challenge[2, 38] = "Centaur3";
$Challenge::Info["Centaur3"] = "Centaur Artillery Expert III\t2\t50000\tNone\tCall in 50 Artillery Strikes";
$Challenge::Challenge[2, 39] = "EMP1";
$Challenge::Info["EMP1"] = "EMP Expert I\t2\t10000\tNone\tCall in 5 Mass EMP's";
$Challenge::Challenge[2, 40] = "EMP2";
$Challenge::Info["EMP2"] = "EMP Expert II\t2\t25000\tNone\tCall in 10 Mass EMP's";
$Challenge::Challenge[2, 41] = "EMP3";
$Challenge::Info["EMP3"] = "EMP Expert III\t2\t50000\tNone\tCall in 25 Mass EMP's";
$Challenge::Challenge[2, 42] = "Nuke1";
$Challenge::Info["Nuke1"] = "Nuke Expert I\t2\t10000\tNone\tCall in 5 Nukes";
$Challenge::Challenge[2, 43] = "Nuke2";
$Challenge::Info["Nuke2"] = "Nuke Expert II\t2\t25000\tNone\tCall in 10 Nukes";
$Challenge::Challenge[2, 44] = "Nuke3";
$Challenge::Info["Nuke3"] = "Nuke Expert III\t2\t50000\tZ-Bomb Killstreak\tCall in 25 Nukes";
$Challenge::Challenge[2, 45] = "Fission1";
$Challenge::SetRequirement[2, 45] = "Officer 1";
$Challenge::Info["Fission1"] = "Fission Bomb Expert I\t2\t25000\tNone\tCall in a Fission Bomb";
$Challenge::Challenge[2, 46] = "Fission2";
$Challenge::SetRequirement[2, 46] = "Officer 1";
$Challenge::Info["Fission2"] = "Fission Bomb Expert II\t2\t50000\tNone\tCall in 2 Fission Bombs";
$Challenge::Challenge[2, 47] = "Fission3";
$Challenge::SetRequirement[2, 47] = "Officer 1";
$Challenge::Info["Fission3"] = "Fission Bomb Expert III\t2\t75000\tNone\tCall in 5 Fission Bombs";
$Challenge::Challenge[2, 48] = "PulseStar1";
$Challenge::SetRequirement[2, 48] = "Officer 9";
$Challenge::Info["PulseStar1"] = "PulseStar Expert I\t2\t5000\tNone\tCall in 15 PulseStar Shield Systems";
$Challenge::Challenge[2, 49] = "PulseStar2";
$Challenge::SetRequirement[2, 49] = "Officer 9";
$Challenge::Info["PulseStar2"] = "PulseStar Expert II\t2\t10000\tNone\tCall in 30 PulseStar Shield Systems";
$Challenge::Challenge[2, 50] = "PulseStar3";
$Challenge::SetRequirement[2, 50] = "Officer 9";
$Challenge::Info["PulseStar3"] = "PulseStar Expert III\t2\t25000\tNone\tCall in 50 PulseStar Shield Systems";
$Challenge::Challenge[2, 51] = "LOAS1";
$Challenge::SetRequirement[2, 51] = "Officer 15";
$Challenge::Info["LOAS1"] = "LOAS Expert I\t2\t25000\tNone\tCall in 5 Low Orbit Orbital Strikes (LOAS)";
$Challenge::Challenge[2, 52] = "LOAS2";
$Challenge::SetRequirement[2, 52] = "Officer 15";
$Challenge::Info["LOAS2"] = "LOAS Expert II\t2\t50000\tNone\tCall in 10 Low Orbit Orbital Strikes (LOAS)";
$Challenge::Challenge[2, 53] = "LOAS3";
$Challenge::SetRequirement[2, 53] = "Officer 15";
$Challenge::Info["LOAS3"] = "LOAS Expert III\t2\t75000\tNone\tCall in 15 Low Orbit Orbital Strikes (LOAS)";

//Bosses
$Challenge::Category[3] = "Boss Challenges\tTasks for eliminating the toughest enemies in TWM2\t18";

$Challenge::Challenge[3, 0] = "Yvex1";
$Challenge::Info["Yvex1"] = "Nightmarish Enterprise\t3\t1000\tNone\tDefeat Lord Yvex 3 Times";
$Challenge::Challenge[3, 1] = "Yvex2";
$Challenge::Info["Yvex2"] = "Darkness Rising\t3\t2500\tNone\tDefeat Lord Yvex 5 Times";
$Challenge::Challenge[3, 2] = "Yvex3";
$Challenge::Info["Yvex3"] = "Shadowy Desecration\t3\t5000\tNone\tDefeat Lord Yvex 10 Times";
$Challenge::Challenge[3, 3] = "CWS1";
$Challenge::Info["CWS1"] = "Fortress In The Sky\t3\t1000\tNone\tDefeat Colonel Windshear 3 Times";
$Challenge::Challenge[3, 4] = "CWS2";
$Challenge::Info["CWS2"] = "Aerieal Nightmare\t3\t2500\tNone\tDefeat Colonel Windshear 5 Times";
$Challenge::Challenge[3, 5] = "CWS3";
$Challenge::Info["CWS3"] = "Harbinger's Bane\t3\t5000\tNone\tDefeat Colonel Windshear 10 Times";
$Challenge::Challenge[3, 6] = "GOL1";
$Challenge::Info["GOL1"] = "Envious Lightning\t3\t1500\tNone\tDefeat The Ghost Of Lightning 3 Times";
$Challenge::Challenge[3, 7] = "GOL2";
$Challenge::Info["GOL2"] = "The Shocking Truth\t3\t3000\tNone\tDefeat The Ghost Of Lightning 5 Times";
$Challenge::Challenge[3, 8] = "GOL3";
$Challenge::Info["GOL3"] = "Severe Thunderstorm\t3\t6500\tNone\tDefeat The Ghost Of Lightning 10 Times";
$Challenge::Challenge[3, 9] = "GOF1";
$Challenge::Info["GOF1"] = "Purifier\t3\t5000\tNone\tDefeat The Ghost Of Fire";
$Challenge::Challenge[3, 10] = "GOF2";
$Challenge::Info["GOF2"] = "Inceneration Ender\t3\t10000\tNone\tDefeat The Ghost Of Fire 3 Times";
$Challenge::Challenge[3, 11] = "GOF3";
$Challenge::Info["GOF3"] = "Mt. Death Depleter\t3\t20000\tNone\tDefeat The Ghost Of Fire 5 Times";
$Challenge::Challenge[3, 12] = "Veg1";
$Challenge::Info["Veg1"] = "Flaming Revolt\t3\t1500\tNone\tDefeat General Vegenor 3 Times";
$Challenge::Challenge[3, 13] = "Veg2";
$Challenge::Info["Veg2"] = "Burning Frenzy\t3\t3000\tNone\tDefeat General Vegenor 5 Times";
$Challenge::Challenge[3, 14] = "Veg3";
$Challenge::Info["Veg3"] = "Firestorm Ender\t3\t6500\tNone\tDefeat General Vegenor 10 Times";
$Challenge::Challenge[3, 15] = "LRog1";
$Challenge::Info["LRog1"] = "Revenge Halter\t3\t2500\tNone\tDefeat Lord Rog 2 Times";
$Challenge::Challenge[3, 16] = "LRog2";
$Challenge::Info["LRog2"] = "Return to Returner\t3\t5000\tNone\tDefeat Lord Rog 4 Times";
$Challenge::Challenge[3, 17] = "LRog3";
$Challenge::Info["LRog3"] = "Payback's A Bitch\t3\t10000\tNone\tDefeat Lord Rog 7 Times";
$Challenge::Challenge[3, 18] = "Ins1";
$Challenge::Info["Ins1"] = "El Shipitor\t3\t2500\tNone\tDefeat Major Insignia 2 Times";
$Challenge::Challenge[3, 19] = "Ins2";
$Challenge::Info["Ins2"] = "No Gravity, No Problem\t3\t5000\tNone\tDefeat Major Insignia 4 Times";
$Challenge::Challenge[3, 20] = "Ins3";
$Challenge::Info["Ins3"] = "Gravitational Influx\t3\t10000\tNone\tDefeat Major Insignia 7 Times";
$Challenge::Challenge[3, 21] = "Stormrider1";
$Challenge::Info["Stormrider1"] = "Clear Skies\t3\t2500\tNone\tDefeat Commander Stormrider 3 Times";
$Challenge::Challenge[3, 22] = "Stormrider2";
$Challenge::Info["Stormrider2"] = "Shootdown Master\t3\t5000\tNone\tDefeat Commander Stormrider 5 Times";
$Challenge::Challenge[3, 23] = "Stormrider3";
$Challenge::Info["Stormrider3"] = "Harbinger Fighter Demolisher\t3\t10000\tNone\tDefeat Commander Stormrider 10 Times";
$Challenge::Challenge[3, 24] = "Trev1";
$Challenge::Info["Trev1"] = "Warstation Breaker\t3\t2500\tNone\tDefeat Lordranius Trevor 2 Times";
$Challenge::Challenge[3, 25] = "Trev2";
$Challenge::Info["Trev2"] = "Armor Demolisher\t3\t5000\tNone\tDefeat Lordranius Trevor 4 Times";
$Challenge::Challenge[3, 26] = "Trev3";
$Challenge::Info["Trev3"] = "Bunker Buster Expert\t3\t10000\tNone\tDefeat Lordranius Trevor 7 Times";
$Challenge::Challenge[3, 27] = "Vard1";
$Challenge::Info["Vard1"] = "Shining Star\t3\t3500\tNone\tDefeat Lord Vardison";
$Challenge::Challenge[3, 28] = "Vard2";
$Challenge::Info["Vard2"] = "Glare The Dark\t3\t7000\tNone\tDefeat Lord Vardison 3 Times";
$Challenge::Challenge[3, 29] = "Vard3";
$Challenge::Info["Vard3"] = "Outevil The Wicked\t3\t12500\tNone\tDefeat Lord Vardison 5 Times";
$Challenge::Challenge[3, 30] = "VardEasy";
$Challenge::IsNotMultiTier["VardEasy"] = true;
$Challenge::Info["VardEasy"] = "The Standard Experience\t3\t7000\tNone\tDefeat Lord Vardison on Easy Difficulty";
$Challenge::Challenge[3, 31] = "VardNorm";
$Challenge::IsNotMultiTier["VardNorm"] = true;
$Challenge::Info["VardNorm"] = "Demon Hunter\t3\t15000\tNone\tDefeat Lord Vardison on Medium Difficulty";
$Challenge::Challenge[3, 32] = "VardHard";
$Challenge::IsNotMultiTier["VardHard"] = true;
$Challenge::Info["VardHard"] = "Master Demon Slayer\t3\t25000\tNone\tDefeat Lord Vardison on Hard Difficulty";
$Challenge::Challenge[3, 33] = "VardWtf";
$Challenge::IsNotMultiTier["VardWtf"] = true;
$Challenge::Info["VardWtf"] = "God of the Shadow Realm\t3\t50000\tNone\tAgainst all odds, slay WTF difficulty Lord Vardison";
$Challenge::Challenge[3, 34] = "ShadeLord1";
$Challenge::Info["ShadeLord1"] = "Night Stalker\t3\t5000\tNone\tDefeat The Shade Lord";
$Challenge::Challenge[3, 35] = "ShadeLord2";
$Challenge::Info["ShadeLord2"] = "Shadow Embracer\t3\t10000\tNone\tDefeat The Shade Lord Twice";
$Challenge::Challenge[3, 36] = "ShadeLord3";
$Challenge::Info["ShadeLord3"] = "Dawnlight Encarnate\t3\t20000\tNone\tDefeat The Shade Lord for the Third Time";
$Challenge::Challenge[3, 37] = "Proficient1";
$Challenge::Info["Proficient1"] = "Proficient I\t3\t15000\tNone\tEarn 25 Boss Proficiency Marks";
$Challenge::Challenge[3, 38] = "Proficient2";
$Challenge::Info["Proficient2"] = "Proficient II\t3\t25000\tNone\tEarn 50 Boss Proficiency Marks";
$Challenge::Challenge[3, 39] = "Proficient3";
$Challenge::Info["Proficient3"] = "Proficient III\t3\t50000\tNone\tEarn 100 Boss Proficiency Marks";
$Challenge::Challenge[3, 40] = "MasterProficiency";
$Challenge::IsNotMultiTier["MasterProficiency"] = true;
$Challenge::Info["MasterProficiency"] = "Master of Proficiency\t3\t35000\tNone\tEarn 50 Individual Boss Proficieny Marks";

//Wargames
$Challenge::Category[4] = "Wargames Challenges\tTasks for eliminating enemy players in various ways\t23";

$Challenge::Challenge[4, 0] = "Slayer1";
$Challenge::Info["Slayer1"] = "Slayer I\t4\t1000\tNone\tKill 100 Enemy Players";
$Challenge::Challenge[4, 1] = "Slayer2";
$Challenge::Info["Slayer2"] = "Slayer II\t4\t2500\tNone\tKill 250 Enemy Players";
$Challenge::Challenge[4, 2] = "Slayer3";
$Challenge::Info["Slayer3"] = "Slayer III\t4\t5000\tNone\tKill 500 Enemy Players";
$Challenge::Challenge[4, 3] = "Slayer4";
$Challenge::Info["Slayer4"] = "Slayer IV\t4\t7500\tNone\tKill 750 Enemy Players";
$Challenge::Challenge[4, 4] = "Slayer5";
$Challenge::Info["Slayer5"] = "Slayer V\t4\t10000\tNone\tKill 1000 Enemy Players";
$Challenge::Challenge[4, 5] = "Defectionator1";
$Challenge::Info["Defectionator1"] = "Defectionator I\t4\t2500\tNone\tKill 100 \"Zombified\" Players";
$Challenge::Challenge[4, 6] = "Defectionator2";
$Challenge::Info["Defectionator2"] = "Defectionator II\t4\t5000\tNone\tKill 250 \"Zombified\" Players";
$Challenge::Challenge[4, 7] = "Defectionator3";
$Challenge::Info["Defectionator3"] = "Defectionator III\t4\t10000\tNone\tKill 500 \"Zombified\" Players";
$Challenge::Challenge[4, 8] = "Infectionator1";
$Challenge::Info["Infectionator1"] = "Infectionator I\t4\t2500\tNone\tConvert 50 Players to the Zombie Horde";
$Challenge::Challenge[4, 9] = "Infectionator2";
$Challenge::Info["Infectionator2"] = "Infectionator II\t4\t5000\tNone\tConvert 100 Players to the Zombie Horde";
$Challenge::Challenge[4, 10] = "Infectionator3";
$Challenge::Info["Infectionator3"] = "Infectionator III\t4\t10000\tNone\tConvert 250 Players to the Zombie Horde";
$Challenge::Challenge[4, 11] = "HSHoncho1";
$Challenge::Info["HSHoncho1"] = "Headshot Honcho I\t4\t2500\tNone\tEliminate 100 Enemy Players with Headshots";
$Challenge::Challenge[4, 12] = "HSHoncho2";
$Challenge::Info["HSHoncho2"] = "Headshot Honcho II\t4\t5000\tNone\tEliminate 200 Enemy Players with Headshots";
$Challenge::Challenge[4, 13] = "HSHoncho3";
$Challenge::Info["HSHoncho3"] = "Headshot Honcho III\t4\t10000\tNone\tEliminate 300 Enemy Players with Headshots";
$Challenge::Challenge[4, 14] = "VehMans1";
$Challenge::Info["VehMans1"] = "Vehicular Manslaughter I\t4\t2500\tNone\tEliminate 50 Enemy Players with a vehicle";
$Challenge::Challenge[4, 15] = "VehMans2";
$Challenge::Info["VehMans2"] = "Vehicular Manslaughter II\t4\t5000\tNone\tEliminate 100 Enemy Players with a vehicle";
$Challenge::Challenge[4, 16] = "VehMans3";
$Challenge::Info["VehMans3"] = "Vehicular Manslaughter III\t4\t10000\tNone\tEliminate 250 Enemy Players with a vehicle";
$Challenge::Challenge[4, 17] = "Assassin";
$Challenge::IsNotMultiTier["Assassin"] = true;
$Challenge::Info["Assassin"] = "Assassinator\t4\t5000\tNone\tBackstab an enemy player using the Blade of Vengeance";
$Challenge::Challenge[4, 18] = "CompletelyUnexpected";
$Challenge::IsNotMultiTier["CompletelyUnexpected"] = true;
$Challenge::Info["CompletelyUnexpected"] = "That Was... Unexpected\t4\t50000\tNone\tEliminate General Rog by backstabbing him with the Blade of Vengence";
$Challenge::Challenge[4, 19] = "Uncomprehendable";
$Challenge::IsNotMultiTier["Uncomprehendable"] = true;
$Challenge::Info["Uncomprehendable"] = "Uncomprehendable\t4\t100000\tPure shock?\tGet killed in a fighter, and have the driverless vehicle run down your killer";

//Survivor
$Challenge::Category[5] = "Survivor Challenges\tTasks related to surviving the living world\t23";
$Challenge::Challenge[5, 0] = "IntoTheFire1";
$Challenge::Info["IntoTheFire1"] = "Into The Fire I\t5\t5000\tNone\tComplete 5 Flashpoint Operations";
$Challenge::Challenge[5, 1] = "IntoTheFire2";
$Challenge::Info["IntoTheFire2"] = "Into The Fire II\t5\t10000\tNone\tComplete 10 Flashpoint Operations";
$Challenge::Challenge[5, 2] = "IntoTheFire3";
$Challenge::Info["IntoTheFire3"] = "Into The Fire III\t5\t15000\tNone\tComplete 25 Flashpoint Operations";
$Challenge::Challenge[5, 3] = "MaximizedPotential";
$Challenge::Info["MaximizedPotential"] = "Maximized Potential\t5\t10000\tNone\tReach the maximum hazard level (5) in a living world session";
$Challenge::IsNotMultiTier["MaximizedPotential"] = true;
$Challenge::Challenge[5, 4] = "TheThunderdome";
$Challenge::Info["TheThunderdome"] = "The Thunderdome\t5\t7500\tNone\tEnter a boss arena during a living world session";
$Challenge::IsNotMultiTier["TheThunderdome"] = true;
$Challenge::Challenge[5, 5] = "HVTDown";
$Challenge::Info["HVTDown"] = "HVT Down\t5\t10000\tNone\tComplete a boss arena encounter during a living world session";
$Challenge::IsNotMultiTier["HVTDown"] = true;
$Challenge::Challenge[5, 6] = "ReaperOfReapers";
$Challenge::Info["ReaperOfReapers"] = "Reaper Of Reapers\t5\t5000\tNone\tComplete the 'Aerieal Reaping' flashpoint operation.";
$Challenge::IsNotMultiTier["ReaperOfReapers"] = true;
$Challenge::Challenge[5, 7] = "MineNow";
$Challenge::Info["MineNow"] = "Mine Now\t5\t5000\tNone\tComplete the 'Intel Fishing' flashpoint operation.";
$Challenge::IsNotMultiTier["MineNow"] = true;
$Challenge::Challenge[5, 8] = "YouLookLost";
$Challenge::Info["YouLookLost"] = "You Look Lost\t5\t5000\tNone\tComplete the 'Sentry Patrol' flashpoint operation.";
$Challenge::IsNotMultiTier["YouLookLost"] = true;
$Challenge::Challenge[5, 9] = "DigElsewhere";
$Challenge::Info["DigElsewhere"] = "Go Dig Elsewhere\t5\t5000\tNone\tComplete the 'Resource Drill' flashpoint operation.";
$Challenge::IsNotMultiTier["DigElsewhere"] = true;
$Challenge::Challenge[5, 10] = "BegoneFoulPortal";
$Challenge::Info["BegoneFoulPortal"] = "Begone! Foul Portal!\t5\t5000\tNone\tComplete the 'Shadow Gate' flashpoint operation.";
$Challenge::IsNotMultiTier["BegoneFoulPortal"] = true;
$Challenge::Challenge[5, 11] = "HuntersAsPrey";
$Challenge::Info["HuntersAsPrey"] = "Hunters As Prey\t5\t5000\tNone\tComplete the 'Air Raid' flashpoint operation.";
$Challenge::IsNotMultiTier["HuntersAsPrey"] = true;
$Challenge::Challenge[5, 12] = "ThroughTheFlare";
$Challenge::SetHidden[5, 12] = true;
$Challenge::HiddenMessage[5, 12] = "<color:BB0000>=== CLASSIFIED: DELTA OPS CLEARANCE LEVEL ===";
$Challenge::Info["ThroughTheFlare"] = "Through The Flare\t5\t15000\tNLS-22 Acid Sniper\tFind and kill the Flareguide Mini-Boss during a living world session";
$Challenge::IsNotMultiTier["ThroughTheFlare"] = true;

//Zombie Slaying
$Challenge::Category[6] = "Zombie Slaying Challenges\tTasks for eliminating combatants of the zombie horde\t28";

$Challenge::Challenge[6, 0] = "NormHunter1";
$Challenge::Info["NormHunter1"] = "Frontline Buster I\t6\t2500\tNone\tSlay 2,500 Zombies (Normal Type)";
$Challenge::Challenge[6, 1] = "NormHunter2";
$Challenge::Info["NormHunter2"] = "Frontline Buster II\t6\t5000\tNone\tSlay 5,000 Zombies (Normal Type)";
$Challenge::Challenge[6, 2] = "NormHunter3";
$Challenge::Info["NormHunter3"] = "Frontline Buster III\t6\t10000\tNone\tSlay 10,000 Zombies (Normal Type)";
$Challenge::Challenge[6, 3] = "RavHunter1";
$Challenge::Info["RavHunter1"] = "Speed Kills I\t6\t2500\tNone\tSlay 1,000 Ravager Zombies";
$Challenge::Challenge[6, 4] = "RavHunter2";
$Challenge::Info["RavHunter2"] = "Speed Kills II\t6\t5000\tNone\tSlay 2,500 Ravager Zombies";
$Challenge::Challenge[6, 5] = "RavHunter3";
$Challenge::Info["RavHunter3"] = "Speed Kills III\t6\t10000\tNone\tSlay 5,000 Ravager Zombies";
$Challenge::Challenge[6, 6] = "LordHunter1";
$Challenge::Info["LordHunter1"] = "The Bigger They Are I\t6\t2500\tNone\tSlay 1,000 Zombie Lords";
$Challenge::Challenge[6, 7] = "LordHunter2";
$Challenge::Info["LordHunter2"] = "The Bigger They Are II\t6\t5000\tNone\tSlay 2,000 Zombie Lords";
$Challenge::Challenge[6, 8] = "LordHunter3";
$Challenge::Info["LordHunter3"] = "The Bigger They Are III\t6\t10000\tNone\tSlay 3,000 Zombie Lords";
$Challenge::Challenge[6, 9] = "DemonHunter1";
$Challenge::Info["DemonHunter1"] = "Fire Retardant I\t6\t2500\tNone\tSlay 1,000 Demon Zombies";
$Challenge::Challenge[6, 10] = "DemonHunter2";
$Challenge::Info["DemonHunter2"] = "Fire Retardant II\t6\t5000\tNone\tSlay 2,500 Demon Zombies";
$Challenge::Challenge[6, 11] = "DemonHunter3";
$Challenge::Info["DemonHunter3"] = "Fire Retardant III\t6\t10000\tNone\tSlay 5,000 Demon Zombies";
$Challenge::Challenge[6, 12] = "AirRapHunter1";
$Challenge::Info["AirRapHunter1"] = "Bat Slayer I\t6\t2500\tNone\tSlay 1,500 Air Rapier Zombies";
$Challenge::Challenge[6, 13] = "AirRapHunter2";
$Challenge::Info["AirRapHunter2"] = "Bat Slayer II\t6\t5000\tNone\tSlay 3,500 Air Rapier Zombies";
$Challenge::Challenge[6, 14] = "AirRapHunter3";
$Challenge::Info["AirRapHunter3"] = "Bat Slayer III\t6\t10000\tNone\tSlay 6,000 Air Rapier Zombies";
$Challenge::Challenge[6, 15] = "DLordHunter1";
$Challenge::Info["DLordHunter1"] = "Hellspawn Erradicator I\t6\t2500\tNone\tSlay 500 Demon Lord Zombies";
$Challenge::Challenge[6, 16] = "DLordHunter2";
$Challenge::Info["DLordHunter2"] = "Hellspawn Erradicator II\t6\t5000\tNone\tSlay 1,000 Demon Lord Zombies";
$Challenge::Challenge[6, 17] = "DLordHunter3";
$Challenge::Info["DLordHunter3"] = "Hellspawn Erradicator III\t6\t10000\tNone\tSlay 1,500 Demon Lord Zombies";
$Challenge::Challenge[6, 18] = "ShifterHunter1";
$Challenge::Info["ShifterHunter1"] = "Anti-Warp I\t6\t2500\tNone\tSlay 1,500 Shifter Zombies";
$Challenge::Challenge[6, 19] = "ShifterHunter2";
$Challenge::Info["ShifterHunter2"] = "Anti-Warp II\t6\t5000\tNone\tSlay 3,000 Shifter Zombies";
$Challenge::Challenge[6, 20] = "ShifterHunter3";
$Challenge::Info["ShifterHunter3"] = "Anti-Warp III\t6\t10000\tNone\tSlay 6,000 Shifter Zombies";
$Challenge::Challenge[6, 21] = "SummonerHunter1";
$Challenge::Info["SummonerHunter1"] = "Horde Halter I\t6\t2500\tNone\tSlay 1,000 Zombie Summoners";
$Challenge::Challenge[6, 22] = "SummonerHunter2";
$Challenge::Info["SummonerHunter2"] = "Horde Halter II\t6\t5000\tNone\tSlay 2,500 Zombie Summoners";
$Challenge::Challenge[6, 23] = "SummonerHunter3";
$Challenge::Info["SummonerHunter3"] = "Horde Halter III\t6\t10000\tNone\tSlay 5,000 Zombie Summoners";
$Challenge::Challenge[6, 24] = "SniperHunter1";
$Challenge::Info["SniperHunter1"] = "Scope Breaker I\t6\t2500\tNone\tSlay 1,000 Sniper Zombies";
$Challenge::Challenge[6, 25] = "SniperHunter2";
$Challenge::Info["SniperHunter2"] = "Scope Breaker II\t6\t5000\tNone\tSlay 2,500 Sniper Zombies";
$Challenge::Challenge[6, 26] = "SniperHunter3";
$Challenge::Info["SniperHunter3"] = "Scope Breaker III\t6\t10000\tNone\tSlay 5,000 Sniper Zombies";
$Challenge::Challenge[6, 27] = "UDemHunter1";
$Challenge::Info["UDemHunter1"] = "Runner Down I\t6\t2500\tNone\tSlay 1,000 Ultra Demon Zombies";
$Challenge::Challenge[6, 28] = "UDemHunter2";
$Challenge::Info["UDemHunter2"] = "Runner Down II\t6\t5000\tNone\tSlay 2,500 Ultra Demon Zombies";
$Challenge::Challenge[6, 29] = "UDemHunter3";
$Challenge::Info["UDemHunter3"] = "Runner Down III\t6\t10000\tNone\tSlay 5,000 Ultra Demon Zombies";
$Challenge::Challenge[6, 30] = "VRavHunter1";
$Challenge::Info["VRavHunter1"] = "C4 Coming Through I\t6\t2500\tNone\tSlay 1,000 Volatile Ravager Zombies";
$Challenge::Challenge[6, 31] = "VRavHunter2";
$Challenge::Info["VRavHunter2"] = "C4 Coming Through II\t6\t5000\tNone\tSlay 2,500 Volatile Ravager Zombies";
$Challenge::Challenge[6, 32] = "VRavHunter3";
$Challenge::Info["VRavHunter3"] = "C4 Coming Through III\t6\t10000\tNone\tSlay 5,000 Volatile Ravager Zombies";
$Challenge::Challenge[6, 33] = "SSHunter1";
$Challenge::Info["SSHunter1"] = "De-Flakerizer I\t6\t2500\tNone\tSlay 1,000 Slingshot Zombies";
$Challenge::Challenge[6, 34] = "SSHunter2";
$Challenge::Info["SSHunter2"] = "De-Flakerizer II\t6\t5000\tNone\tSlay 2,500 Slingshot Zombies";
$Challenge::Challenge[6, 35] = "SSHunter3";
$Challenge::Info["SSHunter3"] = "De-Flakerizer III\t6\t10000\tNone\tSlay 5,000 Slingshot Zombies";
$Challenge::Challenge[6, 36] = "WraithHunter1";
$Challenge::Info["WraithHunter1"] = "Anti Spec-Ops I\t6\t2500\tNone\tSlay 500 Wraith Zombies";
$Challenge::Challenge[6, 37] = "WraithHunter2";
$Challenge::Info["WraithHunter2"] = "Anti Spec-Ops II\t6\t5000\tNone\tSlay 750 Wraith Zombies";
$Challenge::Challenge[6, 38] = "WraithHunter3";
$Challenge::Info["WraithHunter3"] = "Anti Spec-Ops III\t6\t10000\tNone\tSlay 1,000 Wraith Zombies";
$Challenge::Challenge[6, 39] = "ShieldBreaker";
$Challenge::Info["ShieldBreaker"] = "Shield Breaker\t6\t1000\tNone\tDestroy a Zombie Lord's Defensive Barrier";
$Challenge::IsNotMultiTier["ShieldBreaker"] = true;

//Drone Hunter
$Challenge::Category[7] = "Drone Hunter Challenges\tTasks related to scrapping Harbinger Sentry Drones\t33";

$Challenge::Challenge[7, 0] = "DroningOn1";
$Challenge::Info["DroningOn1"] = "Droning On I\t7\t5000\tNone\tEliminate 500 Harbinger Sentry Drones (Any Type)";
$Challenge::Challenge[7, 1] = "DroningOn2";
$Challenge::Info["DroningOn2"] = "Droning On II\t7\t10000\tNone\tEliminate 1000 Harbinger Sentry Drones (Any Type)";
$Challenge::Challenge[7, 2] = "DroningOn3";
$Challenge::Info["DroningOn3"] = "Droning On III\t7\t15000\tNone\tEliminate 2500 Harbinger Sentry Drones (Any Type)";
$Challenge::Challenge[7, 3] = "DroningOn4";
$Challenge::Info["DroningOn4"] = "Droning On IV\t7\t25000\tNone\tEliminate 7500 Harbinger Sentry Drones (Any Type)";
$Challenge::Challenge[7, 4] = "DroningOn5";
$Challenge::Info["DroningOn5"] = "Droning On V\t7\t50000\tNone\tEliminate 25000 Harbinger Sentry Drones (Any Type)";
$Challenge::Challenge[7, 5] = "GunEmDown1";
$Challenge::Info["GunEmDown1"] = "Gun Em' Down I\t7\t2500\tNone\tEliminate 100 H1-CMS Drones";
$Challenge::Challenge[7, 6] = "GunEmDown2";
$Challenge::Info["GunEmDown2"] = "Gun Em' Down II\t7\t5000\tNone\tEliminate 250 H1-CMS Drones";
$Challenge::Challenge[7, 7] = "GunEmDown3";
$Challenge::Info["GunEmDown3"] = "Gun Em' Down III\t7\t7500\tNone\tEliminate 500 H1-CMS Drones";
$Challenge::Challenge[7, 8] = "AAInfantry1";
$Challenge::Info["AAInfantry1"] = "Anti-Anti Infantry Scrapper I\t7\t2500\tNone\tEliminate 100 H2-APDS Drones";
$Challenge::Challenge[7, 9] = "AAInfantry2";
$Challenge::Info["AAInfantry2"] = "Anti-Anti Infantry Scrapper II\t7\t5000\tNone\tEliminate 250 H2-APDS Drones";
$Challenge::Challenge[7, 10] = "AAInfantry3";
$Challenge::Info["AAInfantry3"] = "Anti-Anti Infantry Scrapper III\t7\t7500\tNone\tEliminate 500 H2-APDS Drones";
$Challenge::Challenge[7, 11] = "TheyGotTheRockets1";
$Challenge::Info["TheyGotTheRockets1"] = "They've Got The Rockets I\t7\t2500\tNone\tEliminate 100 H3-MSEDS Drones";
$Challenge::Challenge[7, 12] = "TheyGotTheRockets2";
$Challenge::Info["TheyGotTheRockets2"] = "They've Got The Rockets II\t7\t5000\tNone\tEliminate 250 H3-MSEDS Drones";
$Challenge::Challenge[7, 13] = "TheyGotTheRockets3";
$Challenge::Info["TheyGotTheRockets3"] = "They've Got The Rockets III\t7\t7500\tNone\tEliminate 500 H3-MSEDS Drones";

//Sabotage
$Challenge::Category[8] = "Sabotage Challenges\tTasks related to the Sabotage game mode\t40";

$Challenge::Challenge[8, 0] = "BombDisarmed";
$Challenge::IsNotMultiTier["BombDisarmed"] = true;
$Challenge::Info["BombDisarmed"] = "Bomb Disarmed\t8\t500\tNone\tDisarm an enemy bomb";
$Challenge::Challenge[8, 1] = "BombPlanted";
$Challenge::IsNotMultiTier["BombPlanted"] = true;
$Challenge::Info["BombPlanted"] = "Bomb Planted\t8\t2500\tNone\tArm the bomb at the objective";
$Challenge::Challenge[8, 2] = "BombDetonated";
$Challenge::IsNotMultiTier["BombDetonated"] = true;
$Challenge::Info["BombDetonated"] = "Bomb Detonated\t8\t3000\tNone\tWin a Round Of Sabotage";
$Challenge::Challenge[8, 3] = "3For5Sabo";
$Challenge::IsNotMultiTier["3For5Sabo"] = true;
$Challenge::Info["3For5Sabo"] = "Three For Five\t8\t4500\tBomb Shadower Perk\tWin 3 of the 5 rounds in a Sabotage match";
$Challenge::Challenge[8, 4] = "BaseDestroyer";
$Challenge::IsNotMultiTier["BaseDestroyer"] = true;
$Challenge::Info["BaseDestroyer"] = "Base Destroyer\t8\t5000\tNone\tGo Undefeated in a full game of Sabotage";

//Domination
$Challenge::Category[9] = "Domination Challenges\tTasks related to the Domination game mode\t40";

$Challenge::Challenge[9, 0] = "ZoneCapture";
$Challenge::IsNotMultiTier["ZoneCapture"] = true;
$Challenge::Info["ZoneCapture"] = "Zone Conquerer\t9\t500\tNone\tCapture a domination point";
$Challenge::Challenge[9, 1] = "ABC";
$Challenge::IsNotMultiTier["ABC"] = true;
$Challenge::Info["ABC"] = "Alpha Bravo Charlie\t9\t2500\tNone\tAt any point in the game, hold all three points";
$Challenge::Challenge[9, 2] = "MatchSet";
$Challenge::IsNotMultiTier["MatchSet"] = true;
$Challenge::Info["MatchSet"] = "Match Set\t9\t3000\tNone\tWin a Round Of Domination";
$Challenge::Challenge[9, 3] = "3For5";
$Challenge::IsNotMultiTier["3For5"] = true;
$Challenge::Info["3For5"] = "Three For Five\t9\t4500\tNone\tWin 3 of the 5 rounds in a Domination match";
$Challenge::Challenge[9, 4] = "Undefeatable";
$Challenge::IsNotMultiTier["Undefeatable"] = true;
$Challenge::Info["Undefeatable"] = "Undefeatable\t9\t5000\tNone\tGo Undefeated in a full game of Domination";

//Horde 3
$Challenge::Category[10] = "Horde 3 Challenges\tTasks related to surviving the waves of the zombie horde\t40";

$Challenge::Challenge[10, 0] = "15For15";
$Challenge::IsNotMultiTier["15For15"] = true;
$Challenge::Info["15For15"] = "15 For 15\t10\t15000\tNone\tComplete Wave 15";
$Challenge::Challenge[10, 1] = "Milestone25";
$Challenge::IsNotMultiTier["Milestone25"] = true;
$Challenge::Info["Milestone25"] = "Milestone 25\t10\t25000\tNone\tComplete Wave 25";
$Challenge::Challenge[10, 2] = "ArmyOf50Stopped";
$Challenge::IsNotMultiTier["ArmyOf50Stopped"] = true;
$Challenge::Info["ArmyOf50Stopped"] = "Army Of 50 Stopped\t10\t50000\tSecond Chance Perk\tComplete Horde 3 (All 50 Waves)";
$Challenge::Challenge[10, 3] = "Angel";
$Challenge::IsNotMultiTier["Angel"] = true;
$Challenge::Info["Angel"] = "Angel\t10\t500\tNone\tRevive a fallen teammate in Horde";
$Challenge::Challenge[10, 4] = "ZBomber";
$Challenge::IsNotMultiTier["ZBomber"] = true;
$Challenge::Info["ZBomber"] = "Z-Bomber\t10\t2000\tNone\tCall in a Z-Bomb While Playing Horde";
$Challenge::Challenge[10, 5] = "FirstBlood";
$Challenge::IsNotMultiTier["FirstBlood"] = true;
$Challenge::Info["FirstBlood"] = "First Blood\t10\t10000\tNone\tKill the first zombie that spawns in a Horde 3 game";
$Challenge::Challenge[10, 6] = "SpeedSlayer";
$Challenge::IsNotMultiTier["SpeedSlayer"] = true;
$Challenge::Info["SpeedSlayer"] = "Speed Slayer\t10\t20000\tNone\tBe the featured first killer 10 times in a single game";
$Challenge::Challenge[10, 7] = "HighScorer";
$Challenge::IsNotMultiTier["HighScorer"] = true;
$Challenge::Info["HighScorer"] = "High Scorer\t10\t25000\tNone\tBe the featured high scorer 10 times in a single game";

//Helljump
$Challenge::Category[11] = "Helljump Challenges\tTasks related to performing spec-ops Helljump operations\t40";

$Challenge::Challenge[11, 0] = "GroupBuster";
$Challenge::IsNotMultiTier["GroupBuster"] = true;
$Challenge::Info["GroupBuster"] = "Group Buster\t11\t5000\tNone\tComplete A Group";
$Challenge::Challenge[11, 1] = "WaveDefeater";
$Challenge::IsNotMultiTier["WaveDefeater"] = true;
$Challenge::Info["WaveDefeater"] = "Wave Defeater\t11\t50000\tNone\tComplete A Wave";
$Challenge::Challenge[11, 2] = "OneK";
$Challenge::IsNotMultiTier["OneK"] = true;
$Challenge::Info["OneK"] = "1K Soldier\t11\t10000\tNone\tEarn 1,000 Points (Solo Score)";
$Challenge::Challenge[11, 3] = "FiveK";
$Challenge::IsNotMultiTier["FiveK"] = true;
$Challenge::Info["FiveK"] = "5K Soldier\t11\t25000\tNone\tEarn 5,000 Points (Solo Score)";
$Challenge::Challenge[11, 4] = "TenK";
$Challenge::IsNotMultiTier["TenK"] = true;
$Challenge::Info["TenK"] = "10K Soldier\t11\t50000\tNone\tEarn 10,000 Points (Solo Score)";
$Challenge::Challenge[11, 5] = "PointsSurge";
$Challenge::IsNotMultiTier["PointsSurge"] = true;
$Challenge::Info["PointsSurge"] = "Points Surge\t11\t25000\tNone\tEarn 7,500 Points (Team Score)";
$Challenge::Challenge[11, 6] = "PointsJackpot";
$Challenge::IsNotMultiTier["PointsJackpot"] = true;
$Challenge::Info["PointsJackpot"] = "Points Jackpot\t11\t50000\tNone\tEarn 25,000 Points (Team Score)";
$Challenge::Challenge[11, 7] = "DownBoy";
$Challenge::IsNotMultiTier["DownBoy"] = true;
$Challenge::Info["DownBoy"] = "Down Boy... Down\t11\t5000\tNone\tKill the wraith zombie on Strike 5";
$Challenge::Challenge[11, 8] = "ClassExtravaganza";
$Challenge::IsNotMultiTier["ClassExtravaganza"] = true;
$Challenge::Info["ClassExtravaganza"] = "Class Extravaganza\t11\t100\tNone\tUse a hellclass";
$Challenge::Challenge[11, 9] = "LifeGiver";
$Challenge::IsNotMultiTier["LifeGiver"] = true;
$Challenge::Info["LifeGiver"] = "Giver of Life\t11\t7500\tNone\tUse a Full Team Respawn beacon";

//From The Top
$Challenge::Category[12] = "Operation Challenges\tTasks related to performing group operations\t49";

$Challenge::Challenge[12, 0] = "SimonSays";
$Challenge::IsNotMultiTier["SimonSays"] = true;
$Challenge::Info["SimonSays"] = "Simon Says\t12\t1000\tNone\tOrder an operation";
$Challenge::Challenge[12, 1] = "FromTheTop";
$Challenge::IsNotMultiTier["FromTheTop"] = true;
$Challenge::Info["FromTheTop"] = "From The Top\t12\t1000\tNone\tJoin an operation fireteam";
$Challenge::Challenge[12, 2] = "NaturalLeader";
$Challenge::IsNotMultiTier["NaturalLeader"] = true;
$Challenge::Info["NaturalLeader"] = "Natural Leader\t12\t2500\tNone\tOrder an operation, and have another player join the fireteam";
$Challenge::Challenge[12, 3] = "GoldStar";
$Challenge::IsNotMultiTier["GoldStar"] = true;
$Challenge::Info["GoldStar"] = "Gold Star\t12\t1000\tNone\tComplete an operation inside the time window";
$Challenge::Challenge[12, 4] = "Faster";
$Challenge::IsNotMultiTier["Faster"] = true;
$Challenge::Info["Faster"] = "Faster!\t12\t250\tNone\tComplete an operation, but miss the time window";
$Challenge::Challenge[12, 5] = "EpicFailure";
$Challenge::IsNotMultiTier["EpicFailure"] = true;
$Challenge::Info["EpicFailure"] = "Epic Failure\t12\t5\tNone\tYou failed.... :)";
$Challenge::Challenge[12, 6] = "ExpertGunner";
$Challenge::IsNotMultiTier["ExpertGunner"] = true;
$Challenge::Info["ExpertGunner"] = "Expert AC-130 Gunner\t12\t25000\tNone\tComplete Operation 'Rain Down'";
$Challenge::Challenge[12, 7] = "Survivalist";
$Challenge::IsNotMultiTier["Survivalist"] = true;
$Challenge::Info["Survivalist"] = "Survivalist\t12\t25000\tNone\tComplete Operation 'Surrounded'";
$Challenge::Challenge[12, 8] = "Invisibreh";
$Challenge::IsNotMultiTier["Invisibreh"] = true;
$Challenge::Info["Invisibreh"] = "Shhh.. I'm Invisibreh\t12\t25000\tNone\tComplete 'Enemy AC-130 Above' by hiding";
$Challenge::Challenge[12, 9] = "WeakGunship";
$Challenge::IsNotMultiTier["WeakGunship"] = true;
$Challenge::Info["WeakGunship"] = "I have you now!\t12\t25000\tNone\tComplete 'Enemy AC-130 Above' by explosive force";
$Challenge::Challenge[12, 10] = "InvasionBuster";
$Challenge::IsNotMultiTier["InvasionBuster"] = true;
$Challenge::Info["InvasionBuster"] = "Invade THIS!\t12\t25000\tNone\tComplete Operation 'Invasion'";
$Challenge::Challenge[12, 11] = "SurvivalistExtreme";
$Challenge::IsNotMultiTier["SurvivalistExtreme"] = true;
$Challenge::Info["SurvivalistExtreme"] = "Extreme Survivalist\t12\t50000\tNone\tComplete Operation 'Surrounded 2.0'";

//Prestige
$Challenge::Category[13] = "Officer Challenges\tTasks related to officer ranks and advanced progression\tOfficer 1";

$Challenge::Challenge[13, 0] = "Prestige1";
$Challenge::SetHidden[13, 0] = true;
$Challenge::HiddenMessage[13, 0] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige1"] = true;
$Challenge::Info["Prestige1"] = "Instructive Officer\t13\t100\tNone\tReach Officer Level 1";

$Challenge::Challenge[13, 1] = "Prestige2";
$Challenge::SetHidden[13, 1] = true;
$Challenge::HiddenMessage[13, 1] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige2"] = true;
$Challenge::Info["Prestige2"] = "Excelling Officer\t13\t250\tNone\tReach Officer Level 2";

$Challenge::Challenge[13, 2] = "Prestige3";
$Challenge::SetHidden[13, 2] = true;
$Challenge::HiddenMessage[13, 2] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige3"] = true;
$Challenge::Info["Prestige3"] = "Champion Officer\t13\t350\tNone\tReach Officer Level 3";

$Challenge::Challenge[13, 3] = "Prestige4";
$Challenge::SetHidden[13, 3] = true;
$Challenge::HiddenMessage[13, 3] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige4"] = true;
$Challenge::Info["Prestige4"] = "Prestigious Officer\t13\t500\tNone\tReach Officer Level 4";

$Challenge::Challenge[13, 4] = "Prestige5";
$Challenge::SetHidden[13, 4] = true;
$Challenge::HiddenMessage[13, 4] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige5"] = true;
$Challenge::Info["Prestige5"] = "Supreme Officer\t13\t1000\tNone\tReach Officer Level 5";

$Challenge::Challenge[13, 5] = "Prestige6";
$Challenge::SetHidden[13, 5] = true;
$Challenge::HiddenMessage[13, 5] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige6"] = true;
$Challenge::Info["Prestige6"] = "Glorious Officer\t13\t2500\tNone\tReach Officer Level 6";

$Challenge::Challenge[13, 6] = "Prestige7";
$Challenge::SetHidden[13, 6] = true;
$Challenge::HiddenMessage[13, 6] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige7"] = true;
$Challenge::Info["Prestige7"] = "Ultimate Officer\t13\t5000\tNone\tReach Officer Level 7";

$Challenge::Challenge[13, 7] = "Prestige8";
$Challenge::SetHidden[13, 7] = true;
$Challenge::HiddenMessage[13, 7] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige8"] = true;
$Challenge::Info["Prestige8"] = "Shadowing Officer\t13\t7500\tNone\tReach Officer Level 8";

$Challenge::Challenge[13, 8] = "Prestige9";
$Challenge::SetHidden[13, 8] = true;
$Challenge::HiddenMessage[13, 8] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige9"] = true;
$Challenge::Info["Prestige9"] = "Phantom Officer\t13\t10000\tNone\tReach Officer Level 9";

$Challenge::Challenge[13, 9] = "Prestige10";
$Challenge::SetHidden[13, 9] = true;
$Challenge::HiddenMessage[13, 9] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige10"] = true;
$Challenge::Info["Prestige10"] = "Brutal Officer\t13\t10000\tNone\tReach Officer Level 10";

$Challenge::Challenge[13, 10] = "Prestige11";
$Challenge::SetHidden[13, 10] = true;
$Challenge::HiddenMessage[13, 10] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige11"] = true;
$Challenge::Info["Prestige11"] = "Vengeful Officer\t13\t10000\tNone\tReach Officer Level 11";

$Challenge::Challenge[13, 11] = "Prestige12";
$Challenge::SetHidden[13, 11] = true;
$Challenge::HiddenMessage[13, 11] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige12"] = true;
$Challenge::Info["Prestige12"] = "Spectral Officer\t13\t10000\tNone\tReach Officer Level 12";

$Challenge::Challenge[13, 12] = "Prestige13";
$Challenge::SetHidden[13, 12] = true;
$Challenge::HiddenMessage[13, 12] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige13"] = true;
$Challenge::Info["Prestige13"] = "Noble Officer\t13\t10000\tNone\tReach Officer Level 13";

$Challenge::Challenge[13, 13] = "Prestige14";
$Challenge::SetHidden[13, 13] = true;
$Challenge::HiddenMessage[13, 13] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige14"] = true;
$Challenge::Info["Prestige14"] = "Masterful Officer\t13\t10000\tNone\tReach Officer Level 14";

$Challenge::Challenge[13, 14] = "Prestige15";
$Challenge::SetHidden[13, 14] = true;
$Challenge::HiddenMessage[13, 14] = "<color:BB0000>=== CLASSIFIED: REQUIRES CLEARANCE ===";
$Challenge::IsNotMultiTier["Prestige15"] = true;
$Challenge::Info["Prestige15"] = "Rising Harbinger\t13\t10000\tNone\tReach The Highest Officer Level (15)";

$Challenge::Challenge[13, 15] = "GameEnder";
$Challenge::IsNotMultiTier["GameEnder"] = true;
$Challenge::Info["GameEnder"] = "Game Ender\t13\t5000\tNone\tUse a Fission Bomb to end a game";
$Challenge::Challenge[13, 16] = "CastleWalls";
$Challenge::IsNotMultiTier["CastleWalls"] = true;
$Challenge::Info["CastleWalls"] = "Castle Walls\t13\t15000\tNone\tAbsorb 100 enemy rounds with a single PulseStar Shield System";
$Challenge::Challenge[13, 17] = "OrbitalDeath";
$Challenge::IsNotMultiTier["OrbitalDeath"] = true;
$Challenge::Info["OrbitalDeath"] = "Orbital Death Dealer\t13\t20000\tNone\tEliminate 15 enemy players or 50 zombies with a single LOAS strike";

//Master Slayer
$Challenge::Category[14] = "Master Slayer Challenges\tTasks related to defeating the bosses in a specific manner\tOfficer 5";
$Challenge::IsNotMultiTier["Dawnlight"] = true;
$Challenge::Info["Dawnlight"] = "Dawnlight\t14\t25000\tNone\tInflict 7500 Damage to Lord Yvex using the Aegis of Dawn in a single encounter";
$Challenge::Challenge[14, 0] = "Dawnlight";
$Challenge::IsNotMultiTier["FirmlyGrounded"] = true;
$Challenge::Info["FirmlyGrounded"] = "Firmly Grounded\t14\t25000\tNone\tDefeat the Ghost of Lightning, completing all current circuit sequences and overloads";
$Challenge::Challenge[14, 1] = "FirmlyGrounded";
$Challenge::IsNotMultiTier["Extinguished"] = true;
$Challenge::Info["Extinguished"] = "Extinguished\t14\t25000\tNone\tDefeat General Vegenor using only the Sonic Cannon";
$Challenge::Challenge[14, 2] = "Extinguished";
$Challenge::IsNotMultiTier["OnlySilence"] = true;
$Challenge::Info["OnlySilence"] = "There is Only Silence\t14\t25000\tNone\tDefeat Lord Rog without ever being killed by a meteor impact attack";
$Challenge::Challenge[14, 3] = "OnlySilence";
$Challenge::IsNotMultiTier["NewtonsLaws"] = true;
$Challenge::Info["NewtonsLaws"] = "Newton's Laws\t14\t25000\tNone\tDefeat Major Insignia with the Gravity Axe";
$Challenge::Challenge[14, 4] = "NewtonsLaws";
$Challenge::IsNotMultiTier["SequentialForces"] = true;
$Challenge::Info["SequentialForces"] = "Sequential Forces\t14\t25000\tNone\tDestroy Trevor's War Platform with the overload mechanic";
$Challenge::Challenge[14, 5] = "SequentialForces";
$Challenge::IsNotMultiTier["VolcanicRetribution"] = true;
$Challenge::Info["VolcanicRetribution"] = "Volcanic Retribution\t14\t25000\tNone\tDeflect a Volcanic Attack back at the Ghost of Fire";
$Challenge::Challenge[14, 6] = "VolcanicRetribution";
$Challenge::IsNotMultiTier["PhantomsFury"] = true;
$Challenge::Info["PhantomsFury"] = "Phantom's Fury\t14\t25000\tNone\tInflict 7500 Damage to Lord Vardison with the Shadow Encarnate Lance";
$Challenge::Challenge[14, 7] = "PhantomsFury";
$Challenge::IsNotMultiTier["Daybreak"] = true;
$Challenge::Info["Daybreak"] = "Daybreak\t14\t25000\tNone\tInflict 10000 Damage to the Shade Lord with Daybreak";
$Challenge::Challenge[14, 8] = "Daybreak";

//Overpromotion
$Challenge::Category[15] = "Overpromotion Entry\tComplete all tasks to unlock overpromotion entry permissions\tMaxRank";
$Challenge::IsNotMultiTier["OPTask1"] = true;
$Challenge::Info["OPTask1"] = "TASK I\t15\t0\tNone\tDefeat all TWM2 bosses at least once";
$Challenge::Challenge[15, 0] = "OPTask1";
$Challenge::IsNotMultiTier["OPTask2"] = true;
$Challenge::Info["OPTask2"] = "TASK II\t15\t0\tNone\tEarn a total of 250 individual boss proficiency marks";
$Challenge::Challenge[15, 1] = "OPTask2";
$Challenge::IsNotMultiTier["OPTask3"] = true;
$Challenge::Info["OPTask3"] = "TASK III\t15\t0\tNone\tComplete a Titan Commendation Challenge for any weapon in TWM2";
$Challenge::Challenge[15, 2] = "OPTask3";
$Challenge::IsNotMultiTier["OPTask4"] = true;
$Challenge::Info["OPTask4"] = "TASK IV\t15\t0\tNone\tDefeat Lord Vardison on WTF difficulty";
$Challenge::Challenge[15, 2] = "OPTask4";

//Events
$Challenge::Category[16] = "Special Event Challenges\tTasks for playing TWM2 during special events\t-1";

$Challenge::Challenge[16, 0] = "NewYearsEve";
$Challenge::Info["NewYearsEve"] = "New Years Eve Fireworks\t16\t1500\tJavelin Hellclass\tGet a Javelin Kill on New Year's Eve";
$Challenge::IsNotMultiTier["NewYearsEve"] = true;
$Challenge::Challenge[16, 1] = "NewYears";
$Challenge::Info["NewYears"] = "New Years Fireworks\t16\t1500\tNone\tCall in a Nuclear Strike on New Year's Day";
$Challenge::IsNotMultiTier["NewYears"] = true;
$Challenge::Challenge[16, 2] = "GunshipMall";
$Challenge::Info["GunshipMall"] = "Gunship to the Mall\t16\t2500\tNone\tCall in a Gunship Killstreak on Christmas Mall 2009";
$Challenge::IsNotMultiTier["GunshipMall"] = true;
$Challenge::Challenge[16, 3] = "IndepRPG";
$Challenge::Info["IndepRPG"] = "Independance RPG\t16\t1500\tNone\tScore an RPG Kill on the Fourth of July";
$Challenge::IsNotMultiTier["IndepRPG"] = true;
$Challenge::Challenge[16, 4] = "SoulsticeBombard";
$Challenge::Info["SoulsticeBombard"] = "Soulstice Bombard\t16\t1500\tNone\tCall in artillery on one of the soulstices (6/21 or 12/21)";
$Challenge::IsNotMultiTier["SoulsticeBombard"] = true;

//CORE

//non weapon challenges
//started TWM2 2.0
//These vary from using killstreaks, to shooting down things.
//much more to do with these challenges

//Core Functions
function GameConnection::AllowedToDoNW(%client, %name) {
	if($Challenge::FlagDisabled[%name]) {
		return 0;
	}
	%scriptController = %client.TWM2Core;
	%xp = getCurrentEXP(%client);
	%taskCate = getField($Challenge::Info[%name], 1);
	if(%taskCate $= "") {
		error("AllowedToDoNW: Invalid challenge category for "@%name@", system shows: "@%taskCate@" ("@%Challenge::Info[%name]@")");
		return 0;
	}
	%categoryReq = getField($Challenge::Category[%taskCate], 2);
	if(getWord(%categoryReq, 0) $= "Officer") {
		return %scriptController.officer >= getWord(%categoryReq, 1);
	}
	else if(getWord(%categoryReq, 0) $= "MaxRank") {
		return %scriptController.officer >= 15 && %scriptController.millionxp >= 3;
	}
	else {
		if(%categoryReq == -1) {
			return 1;
		}
		else {
			return %xp >= $Rank::MinPoints[%categoryReq];
		}
	}
}

function GameConnection::CheckNWChallengeCompletion(%client, %name) {
	%scriptController = %client.TWM2Core;
	if(%scriptController.challengeComplete[%name] == 1) {
		return true;
	}
	else {
		return false;
	}
}

function CompleteNW_allPlayers(%name) {
	for(%i = 0; %i < ClientGroup.getCount(); %i++) {
		%client = ClientGroup.getObject(%i);
		CompleteNWChallenge(%client, %name);
	}
}

function CompleteNWChallenge(%client, %name) {
	if(%client $= "" || !%client) {
		return;
	}
	if(%client.CheckNWChallengeCompletion(%name)) {
		return;
	}
	if(!%client.AllowedToDoNW(%name)) {
		return;
	}
	//
	%scriptController = %client.TWM2Core;
	%taskName = getField($Challenge::Info[%name], 0);
	%taskXPGive = getField($Challenge::Info[%name], 2);
	%taskReward = getField($Challenge::Info[%name], 3);
	//
	GainExperience(%client, %taskXPGive, "Challenge "@%taskName@" Completed ");
	BottomPrint(%client, "CHALLENGE COMPLETE: "@%taskName@" \n +"@%taskXPGive@"XP, Reward: "@%taskReward@"", 2, 3);
	MessageClient(%client, 'MsgSound', "~wfx/Bonuses/Nouns/General.wav");
	MessageAll('msgComplete', "\c5"@%client.namebase@" completed challenge "@%taskName@"");
	//
	%scriptController.challengeComplete[%name] = 1;
	%file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave\t";
	SaveClientFile(%client);
	echo("TWM2: Client "@%client@", "@%client.nambase@", Completed Challenge "@%taskname@", File Updated.");
}

function fetchChallengeSubID(%name) {
	%challenge = $Challenge::Info[%name];
	if(%challenge $= "") {
		error("fetchChallengeSubID(): Cannot find challenge "@%name);
		return -1;
	}
	%primaryID = getField(%challenge, 1);
	for(%i = 0; $Challenge::Challenge[%primaryID, %i] !$= ""; %i++) {
		if($Challenge::Challenge[%primaryID, %i] $= %name) {
			return %i;
		}
	}
	warn("fetchChallengeSubID(): There is an invalid field in the NWChallengeIndex, cannot find "@%name@" under "@%primaryID);
	return -1;
}