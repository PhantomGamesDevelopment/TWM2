$KillstreakCount = 17;
//Format: "Name\tPlayer Kills\tZombie Kills\tStreak Description"
$Killstreak[1] = "UAV\t"@$Killstreak::Kills["UAV", 0]@"\t"@$Killstreak::Kills["UAV", 1]@"\tCall in a UAV Recon to scout the enemy position";
$Killstreak[2] = "Airstrike\t"@$Killstreak::Kills["Airstrike", 0]@"\t"@$Killstreak::Kills["Airstrike", 1]@"\tCall in 4 Thundersword bombers to carbet bomb an area";
$Killstreak[3] = "UAMS\t"@$Killstreak::Kills["UAMS", 0]@"\t"@$Killstreak::Kills["UAMS", 1]@"\tCall in a UAMS to launch 3 hornet missiles and a guided missile";
$Killstreak[4] = "Helicopter\t"@$Killstreak::Kills["Heli", 0]@"\t"@$Killstreak::Kills["Heli", 1]@"\tCall in an combat helicopter for one minute";
$Killstreak[5] = "Harrier Airstrike\t"@$Killstreak::Kills["Harrier", 0]@"\t"@$Killstreak::Kills["Harrier", 1]@"\tAirstrike with a hovering plasma harrier.";
$Killstreak[6] = "Satellite Strike\t"@$Killstreak::Kills["SatBomb", 0]@"\t"@$Killstreak::Kills["SatBomb", 1]@"\tObliterate a large area with an orbital laser strike.";
$Killstreak[7] = "Gunship Helicopter\t"@$Killstreak::Kills["Gunship", 0]@"\t"@$Killstreak::Kills["Gunship", 1]@"\tCalls in a heavily armed helicopter.";
$Killstreak[8] = "Stealth Bomber\t"@$Killstreak::Kills["SlthBomb", 0]@"\t"@$Killstreak::Kills["SlthBomb", 1]@"\tCalls in a stealth bomber to carpet bomb an area.";
$Killstreak[9] = "Harbinger's Wrath\t"@$Killstreak::Kills["Harbins", 0]@"\t"@$Killstreak::Kills["Harbins", 1]@"\tBe the gunner of a harbinger gunship";
$Killstreak[10] = "Apache Gunner\t"@$Killstreak::Kills["ChopperGunner", 0]@"\t"@$Killstreak::Kills["ChopperGunner", 1]@"\tBe the gunner of an apache helicopter";
$Killstreak[11] = "AC-130 Gunner\t"@$Killstreak::Kills["AC130", 0]@"\t"@$Killstreak::Kills["AC130", 1]@"\tBe the gunner of an AC-130";
$Killstreak[12] = "Centaur Bombardment\t"@$Killstreak::Kills["Artillery", 0]@"\t"@$Killstreak::Kills["Artillery", 1]@"\tCall in a proton collider bombardment";
$Killstreak[13] = "Mass EMP\t"@$Killstreak::Kills["MassEMP", 0]@"\t"@$Killstreak::Kills["MassEMP", 1]@"\tEMP the entire enemy team for 3 minutes.";
$Killstreak[14] = "Arrow IV Nuke Strike\t"@$Killstreak::Kills["Nuke", 0]@"\t"@$Killstreak::Kills["Nuke", 1]@"\t350M Tactical Nuke.";
$Killstreak[15] = "Z-Bomb\t-1\t"@$Killstreak::Kills["ZBomb", 1]@"\tWipe out all zombies (not bosses) in a flash.";
$Killstreak[16] = "Fission Bomb\t"@$Killstreak::Kills["Fission", 0]@"\t-1\t(Matches) End the game with an explosive bang.";
$Killstreak[17] = "Napalm Airstrike\t"@$Killstreak::Kills["Napalm", 0]@"\t"@$Killstreak::Kills["Napalm", 1]@"\tQuick destructive airstrike with remaining fire.";


function GetStreakDescrip(%val) {
   %desc = getField($Killstreak[%val], 3);
   return %desc;
}

function StreakValToName(%val) {
   %name = getField($Killstreak[%val], 0);
   return %name;
}

function GetRequiredKills(%client, %streakVal, %plZ) {
   if(!%client) {
      return;
   }
   if(%plZ == 1) {     //zombie
      %streakStr = $Killstreak[%streakVal];
      %ZKills = getField(%streakStr, 2);
      if(%ZKills < 0) {
         return 0;
      }
      else {
         if(%client.IsActivePerk("Hardline")) {
            %ZKills--;
         }
         return %ZKills;
      }
   }
   else {
      %streakStr = $Killstreak[%streakVal];
      %PlKills = getField(%streakStr, 1);
      if(%PlKills < 0) {
         return 0;
      }
      else {
         if(%client.IsActivePerk("Hardline")) {
            %PlKills--;
         }
         return %PlKills;
      }
   }
}

function GameConnection::OnUseKillstreak(%client, %ID) {
   recordAction(%client, "KSCC", %ID@"\t1");
}

//Handles Player Based Killstreaks
function DoKillstreakChecks(%client) {
   %player = %client.player;
   for(%str = 1; %str <= $KillstreakCount; %str++) {
      %needed = GetRequiredKills(%client, %str, 0);
      if(%needed && %str != 15) {
         if(%player.killsinarow == GetRequiredKills(%client, %str, 0)) {
            %client.AwardKillstreak(%str, 0);
         }
      }
   }
}

//Handles Zombie Based Killstreaks
function DoZKillstreakChecks(%client) {
   if($TWM::PlayingHellJump) {
      %player = %client.player;
      //Special Case Killstreaks, ignored by system
      if(%player.zombiekillsinarow == 10) {
         %client.AwardKillstreak(2);
      }
      if(%player.zombiekillsinarow == 15) {
         %client.AwardKillstreak(3);
      }
      if(%player.zombiekillsinarow == 25) {
         %client.AwardKillstreak(4);
      }
      if(%player.zombiekillsinarow == 30) {
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Ammo Drop Standing by.");
         %client.HasAmmoDrop = 1; //heh, now we can use it if we die
         %player.setInventory(AmmoDropCaller, 1, true);
      }
      if(%player.zombiekillsinarow == 45) {
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Full Team Respawn now available for your use.");
         %client.HasFullTeamRespawn = 1; //heh, now we can use it if we die
         %player.setInventory(FullTeamRespawnCaller, 1, true);
         %player.zombiekillsinarow = 0;
      }
      return;
   }
   %player = %client.player;
   for(%str = 1; %str <= $KillstreakCount; %str++) {
      %needed = GetRequiredKills(%client, %str, 1);
      if(%needed && %str != 16) {
         if(%player.zombiekillsinarow == GetRequiredKills(%client, %str, 1)) {
            %client.AwardKillstreak(%str, 1);
         }
      }
   }
}

function GameConnection::HasKillstreak(%client, %streakVal) {
   //checks
   switch(%streakVal) {
      case 1 or 2 or 4 or 13: //the default 3
         //UAV
         //Airstrike
         //Heli
         //Mass EMP - Added 2.6
         return 1;
      case 3:
         //UAMS
         if(%client.hasMedal(3) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 5:
         //Harriers
         if(%client.hasMedal(12) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 6:
         //Satellite Bombardment
         if(%client.hasMedal(13) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 7:
         //Gunship Heli
         if(%client.hasMedal(1) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 8:
         //Stealth Bomber
         if(%client.hasMedal(9) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 9:
         //Harbinger's Wrath
         if(%client.hasMedal(20) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 10:
         //Apache Gunner
         if(%client.hasMedal(8) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 11:
         //AC130 Gunner
         if(%client.hasMedal(26) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 12:
         //Centaur Bombardment
         if(%client.hasMedal(15) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 14:
         //Nuke
         if(%client.hasMedal(4) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 15:
         //ZBomb
         if(%client.CheckNWChallengeCompletion("Nuke3") == 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 16:
         //Fission Bomb
         if(%client.TWM2Core.officer >= 1) {
            return 1;
         }
         else {
            return 0;
         }
      case 17:
         if(%client.hasMedal(27) == 1) {
            return 1;
         }
         else {
            return 0;
         }
      default:
         error("Invalid streak passed to GameConnection::HasKillstreak "@%streakVal@"");
         return 0;
   }
}

function GameConnection::GetActiveStreakCount(%client) {
   %ct = 0;
   for(%i = 1; %i <= $KillstreakCount; %i++) {
      if(%client.isActiveStreak(%i)) {
         %ct++;
      }
   }
   return %ct;
}

function GameConnection::getMaxActiveStreaks(%client) {
   if($Killstreak::Setting == 1) {
      return 3 + %client.TWM2Core.officer;
   }
   else if($Killstreak::Setting == 2) {
      return 0; //Menu does not show
   }
   else if($Killstreak::Setting == 3) {
      return $Killstreak::StreaksPerPlayer;
   }
   else if($Killstreak::Setting == 4) {
      return 0; //Menu does not show
   }
}

function GameConnection::setStreakStatus(%client, %val, %stat) {
   if(!%client.HasKillstreak(%val)) {
      messageClient(%client, 'msgTooMany', "\c5TWM2: You cannot use this kill streak.");
      return;
   }
   if(%stat == 1) {    //Activate streak
      if(%client.GetActiveStreakCount() == %client.getMaxActiveStreaks()) {
         messageClient(%client, 'msgTooMany', "\c5TWM2: You already have all "@%client.getMaxActiveStreaks()@" Killstreaks active.");
         return;
      }
      else {
         %client.KillstreakOn[%val] = 1;
         messageClient(%client, 'msgKSOn', "\c5TWM2: Killstreak "@StreakValToName(%val)@" activated ("@%client.GetActiveStreakCount()@"/"@%client.getMaxActiveStreaks()@").");
      }
   }
   else {              //De-activate streak
      %client.KillstreakOn[%val] = 0;
      messageClient(%client, 'msgKSOff', "\c5TWM2: Killstreak "@StreakValToName(%val)@" deactivated ("@%client.GetActiveStreakCount()@"/"@%client.getMaxActiveStreaks()@").");
   }
}

function GameConnection::isActiveStreak(%client, %val) {
   //anti-hack
   if(!%client.HasKillstreak(%val)) {
      return 0;
   }
   //------
   if(%client.KillstreakOn[%val] == 1) {
      return 1;
   }
   else {
      return 0;
   }
}

function GiveTWM2Weapons(%client) {
    if(%client.HasAmmoDrop) {
       %client.player.setInventory(AmmoDropCaller, 1, true);
    }
    if(%client.HasFullTeamRespawn) {
       %client.player.setInventory(FullTeamRespawnCaller, 1, true);
    }
    if(%client.ksListInstance.count() > 0) {
       %client.player.setInventory(KillstreakBeacon, 1, true);
    }
    if(!%client.isconfiscated) {
	   if (%client.isAdmin) {
          %client.player.setInventory(SuperChaingun,1,true);
	   }
    }
}

function GameConnection::AwardKillstreak(%client, %streakVal, %plz) {
   if(%plz $= "") {
      %plz = 1;
   }
   //the switchith
   if($Killstreak::Setting == 4) {
      return;
   }
   if(!%client.isActiveStreak(%streakVal) && ($Killstreak::Setting != 2) && !$TWM::PlayingHelljump) {
      return;
   }
   if(!%client.ksListInstance) {
      %client.ksListInstance = initList();
   }
   %client.player.setInventory(KillstreakBeacon, 1, true);
   %cAmt = 0;
   switch(%streakVal) {
      case 1:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: UAV Recon at Your Disposal.");
         if(%client.ksListInstance.find("UAV") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("UAV"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("UAV", "UAV "@%cAmt+1);
      case 2:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Airstrike Standing By.");
         if(%client.ksListInstance.find("Airstrike") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("Airstrike"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("Airstrike", "Airstrike "@%cAmt+1);
      case 3:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Guided Missile Strike Standing By.");
         if(%client.ksListInstance.find("GM") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("GM"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("GM", "GM "@%cAmt+1);
      case 4:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Helicopter at your disposal.");
         if(%client.ksListInstance.find("AIHeli") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("AIHeli"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("AIHeli", "AIHeli "@%cAmt+1);
      case 5:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Plasma Harrier Strike at your disposal.");
         if(%client.ksListInstance.find("Harrier") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("Harrier"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("Harrier", "Harrier "@%cAmt+1);
      case 6:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Satellite Strike at your disposal.");
         if(%client.ksListInstance.find("OLS") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("OLS"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("OLS", "OLS "@%cAmt+1);
      case 7:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Gunship Helicopter at your disposal.");
         if(%client.ksListInstance.find("AIGunHeli") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("AIGunHeli"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("AIGunHeli", "AIGunHeli "@%cAmt+1);
      case 8:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Stealth Bomber at your disposal.");
         if(%client.ksListInstance.find("Stealth") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("Stealth"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("Stealth", "Stealth "@%cAmt+1);
      case 9:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Harbinger's Wrath Standing By.");
         if(%client.ksListInstance.find("HarbWrath") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("HarbWrath"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("HarbWrath", "HarbWrath "@%cAmt+1);
      case 10:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Apache Gunner Standing By.");
         if(%client.ksListInstance.find("Apache") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("Apache"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("Apache", "Apache "@%cAmt+1);
      case 11:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: AC-130 Gunner Standing By.");
         if(%client.ksListInstance.find("AC130") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("AC130"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("AC130", "AC130 "@%cAmt+1);
      case 12:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Centaur Bombardment Standing By.");
         if(%client.ksListInstance.find("Artillery") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("Artillery"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("Artillery", "Artillery "@%cAmt+1);
      case 13:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Mass EMP Standing By.");
         if(%client.ksListInstance.find("EMP") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("EMP"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("EMP", "EMP "@%cAmt+1);
      case 14:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Nuclear Strike Standing By.");
         if(%client.ksListInstance.find("NukeStrike") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("NukeStrike"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("NukeStrike", "NukeStrike "@%cAmt+1);
      case 15:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Zombie Bomb Standing By.");
         if(%client.ksListInstance.find("ZBomb") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("ZBomb"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("ZBomb", "ZBomb "@%cAmt+1);
      case 16:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Fission Bomb Ready... Obliterate everyone!!!");
         if(%client.ksListInstance.find("FBomb") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("FBomb"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("FBomb", "FBomb "@%cAmt+1);
      case 17:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Napalm Airstrike at your disposal.");
         if(%client.ksListInstance.find("Napalm") != -1) {
            %cAmt = getWord(getField(%client.ksListInstance.find("Napalm"), 0), 1);
         }
         %client.ksListIntance.advancedAdd("Napalm", "Napalm "@%cAmt+1);
   }
   if(%plz == 0) {
      if(%client.IsHighestPLStreak(%streakVal)) {
         %client.player.killsinarow = 0; //reset for moar killstreaks!
      }
   }
   else {
      if(!$TWM::PlayingHellJump) {
         if(%client.IsHighestZStreak(%streakVal)) {
            %client.player.zombiekillsinarow = 0; //reset for moar killstreaks!
         }
      }
   }
}

//Modified 12-17-09 to take into consideration of hosts changing the kill values
function GameConnection::IsHighestPLStreak(%client, %streakVal) {
   %highest = 0;
   for(%i = $KillstreakCount; %i > 0; %i--) {
      %needed = GetRequiredKills(%client, %i, 0);
      if(%client.isActiveStreak(%i)) {
         if(%needed >= %highest) {
            %highest = %needed;
            %highestStr = %i;
         }
      }
   }
   //
   if(%streakVal == %highestStr) {
      return 1;
   }
   else {
      return 0;
   }
}

//Modified 12-17-09 to take into consideration of hosts changing the kill values
function GameConnection::IsHighestZStreak(%client, %streakVal) {
   %highest = 0;
   for(%i = $KillstreakCount; %i > 0; %i--) {
      %needed = GetRequiredKills(%client, %i, 1);
      if(%client.isActiveStreak(%i)) {
         if(%needed >= %highest) {
            %highest = %needed;
            %highestStr = %i;
         }
      }
   }
   //
   if(%streakVal == %highestStr) {
      echo("Streaks Reset for "@%client@" at "@%streakVal@"");
      return 1;
   }
   else {
      return 0;
   }
}
//And now the opposite of it :P
//This function isn't being used.... yet.
function GameConnection::IsLowestStreak(%client, %streakVal) {
   for(%i = 1; %i <= $KillstreakCount; %i++) {
      if(%client.isActiveStreak(%i)) {
         %lowest = %i;   //<-- This is the curent lowest active streak
         if(%lowest == %streakVal) {   //if this is the one we are checking
            return 1;   //it must be the lowest
         }
         else {
            return 0;  //otherwise, it is a higher one
         }
      }
   }
}

function GameConnection::GatherActiveStreaks(%client) {
   %count = 0;
   %str = "";
   //first part gathers the active streaks
   for(%i = 1; %i <= $KillstreakCount; %i++) {
      if(%client.isActiveStreak(%i)) {
         %count++;
         %streak[%count] = %i;
      }
   }
   //second part outputs the proper amount of string values
   for(%x = 1; %x <= %count; %x++) {
      %str = ""@%str@""@%streak[%x]@"\t";
   }
   return %str;
}

function GameConnection::DisableAllKillstreaks(%client) {
   //first part gathers the active streaks
   for(%i = 1; %i <= $KillstreakCount; %i++) {
      if(%client.isActiveStreak(%i)) {
         %client.setStreakStatus(%i, 0);
      }
   }
}




//now the menu
function GenerateKillstreakMenu(%client, %tag, %index) {
   if($Killstreak::Setting == 2) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Streak settings are on TWM2 1.8 and below.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "All streaks will be earned if unlocked.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Streaks You Will Get:");
      %index++;
      for(%i = 1; %i <= $KillstreakCount; %i++) {
         if(%client.HasKillstreak(KS_Attenuate(%i))) {
            if(StreakValToName(KS_Attenuate(%i)) $= "Z-Bomb") {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 1)@" Zombie Kills: "@GetStreakDescrip(KS_Attenuate(%i))@"");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 0)@" Kills ("@GetRequiredKills(%client, KS_Attenuate(%i), 1)@"): "@GetStreakDescrip(KS_Attenuate(%i))@"");
               %index++;
            }
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>**Streak Locked**");
            %index++;
         }
      }
   }
   else if($Killstreak::Setting == 4) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "The server host has disabled killstreaks.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Active Killstreaks");
      %index++;
      for(%i = 1; %i <= $KillstreakCount; %i++) {
         if(%client.isActiveStreak(KS_Attenuate(%i))) {
            if(StreakValToName(KS_Attenuate(%i)) $= "Z-Bomb") {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 1)@" Zombie Kills: "@GetStreakDescrip(KS_Attenuate(%i))@"");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 0)@" Kills ("@GetRequiredKills(%client, KS_Attenuate(%i), 1)@"): "@GetStreakDescrip(KS_Attenuate(%i))@"");
               %index++;
            }
         }
      }
      //
      messageClient( %client, 'SetLineHud', "", %tag, %index, "");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Activate/Deactivate Streaks");
      %index++;
      for(%i = 1; %i <= $KillstreakCount; %i++) {
         if(%client.isActiveStreak(KS_Attenuate(%i))) {
            if(StreakValToName(KS_Attenuate(%i)) $= "Z-Bomb") {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 1)@" Z-Kills: <a:gamelink\tSetStreakStat\t"@KS_Attenuate(%i)@"\t0>[DEACTIVATE]</a>");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 0)@" Kills ("@GetRequiredKills(%client, KS_Attenuate(%i), 1)@"): "@GetStreakDescrip(KS_Attenuate(%i))@": <a:gamelink\tSetStreakStat\t"@KS_Attenuate(%i)@"\t0>[DEACTIVATE]</a>");
               %index++;
            }
         }
         else {
            if(%client.HasKillstreak(KS_Attenuate(%i))) {
               if(%client.GetActiveStreakCount() >= %client.getMaxActiveStreaks()) {
                  if(StreakValToName(KS_Attenuate(%i)) $= "Z-Bomb") {
                     messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 1)@" Z-Kills: [MSA]");
                     %index++;
                  }
                  else {
                     messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 0)@" Kills ("@GetRequiredKills(%client, KS_Attenuate(%i), 1)@"): "@GetStreakDescrip(KS_Attenuate(%i))@": [MSA]");
                     %index++;
                  }
               }
               else {
                  if(StreakValToName(KS_Attenuate(%i)) $= "Z-Bomb") {
                     messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 1)@" Z-Kills: <a:gamelink\tSetStreakStat\t"@KS_Attenuate(%i)@"\t1>[ACTIVATE]</a>");
                     %index++;
                  }
                  else {
                     messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>"@StreakValToName(KS_Attenuate(%i))@", "@GetRequiredKills(%client, KS_Attenuate(%i), 0)@" Kills ("@GetRequiredKills(%client, KS_Attenuate(%i), 1)@"): "@GetStreakDescrip(KS_Attenuate(%i))@": <a:gamelink\tSetStreakStat\t"@KS_Attenuate(%i)@"\t1>[ACTIVATE]</a>");
                     %index++;
                  }
               }
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:12>**Streak Locked**");
               %index++;
            }
         }
      }
   }
   return %index;
}

/////////////////////
function GenerateStreakChallengeMenu(%client, %tag, %index) {
   if(%client.CheckNWChallengeCompletion("UAV1")) {
      if(%client.CheckNWChallengeCompletion("UAV2")) {
         if(%client.CheckNWChallengeCompletion("UAV3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert III - Call in 150 UAV Recon Satellites");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert II - Call in 75 UAV Recon Satellites");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert I - Call in 30 UAV Recon Satellites");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Airstrike1")) {
      if(%client.CheckNWChallengeCompletion("Airstrike2")) {
         if(%client.CheckNWChallengeCompletion("Airstrike3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert III - Call in 125 Airstrikes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert II - Call in 65 Airstrikes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert I - Call in 25 Airstrikes");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("UAMS1")) {
      if(%client.CheckNWChallengeCompletion("UAMS2")) {
         if(%client.CheckNWChallengeCompletion("UAMS3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert III - Call in 125 Missile Strikes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert II - Call in 65 Missile Strikes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert I - Call in 25 Missile Strikes");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Helicopter1")) {
      if(%client.CheckNWChallengeCompletion("Helicopter2")) {
         if(%client.CheckNWChallengeCompletion("Helicopter3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert III - Call in 125 Combat Helicopters");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert II - Call in 65 Combat Helicopters");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert I - Call in 25 Combat Helicopters");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Harrier1")) {
      if(%client.CheckNWChallengeCompletion("Harrier2")) {
         if(%client.CheckNWChallengeCompletion("Harrier3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert III - Call in 110 Plasma Harrier Airstrikes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert II - Call in 55 Plasma Harrier Airstrikes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert I - Call in 20 Plasma Harrier Airstrikes");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("SatNuke1")) {
      if(%client.CheckNWChallengeCompletion("SatNuke2")) {
         if(%client.CheckNWChallengeCompletion("SatNuke3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert III - Call in 125 Orbital Laser Strikes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert II - Call in 65 Orbital Laser Strikes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert I - Call in 25 Orbital Laser Strikes");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("NapalmHarrier1")) {
      if(%client.CheckNWChallengeCompletion("NapalmHarrier2")) {
         if(%client.CheckNWChallengeCompletion("NapalmHarrier3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert III - Call in 110 Napalm Airstrikes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert II - Call in 55 Napalm Airstrikes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert I - Call in 20 Napalm Airstrikes");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("GunHeli1")) {
      if(%client.CheckNWChallengeCompletion("GunHeli2")) {
         if(%client.CheckNWChallengeCompletion("GunHeli3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert III - Call in 110 Gunship Helicopters");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert II - Call in 55 Gunship Helicopters");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert I - Call in 20 Gunship Helicopters");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("SBomber1")) {
      if(%client.CheckNWChallengeCompletion("SBomber2")) {
         if(%client.CheckNWChallengeCompletion("SBomber3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert III - Call in 100 Stealth Bombers");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert II - Call in 50 Stealth Bombers");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert I - Call in 20 Stealth Bombers");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Gunship1")) {
      if(%client.CheckNWChallengeCompletion("Gunship2")) {
         if(%client.CheckNWChallengeCompletion("Gunship3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert III - Call in 75 Harbinger Gunships");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert II - Call in 35 Harbinger Gunships");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert I - Call in 15 Harbinger Gunships");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Apache1")) {
      if(%client.CheckNWChallengeCompletion("Apache2")) {
         if(%client.CheckNWChallengeCompletion("Apache3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Expert III - Call in 75 Apache Gunners");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Expert II - Call in 35 Apache Gunners");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Expert I - Call in 15 Apache Gunners");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Gunship3")) {
      if(%client.CheckNWChallengeCompletion("ACGunship1")) {
         if(%client.CheckNWChallengeCompletion("ACGunship2")) {
            if(%client.CheckNWChallengeCompletion("ACGunship3")) {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert - Challenge Set Complete");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert III - Call in 75 AC130's");
               %index++;
            }
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert II - Call in 35 AC130's");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert I - Call in 15 AC130's");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked - Requires Harbinger Gunship Expert III.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Centaur1")) {
      if(%client.CheckNWChallengeCompletion("Centaur2")) {
         if(%client.CheckNWChallengeCompletion("Centaur3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert III - Call in 50 Artillery Strikes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert II - Call in 25 Artillery Strikes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert I - Call in 10 Artillery Strikes");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("EMP1")) {
      if(%client.CheckNWChallengeCompletion("EMP2")) {
         if(%client.CheckNWChallengeCompletion("EMP3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert III - Call in 25 Mass EMP's");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert II - Call in 10 Mass EMP's");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert I - Call in 5 Mass EMP's");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Nuke1")) {
      if(%client.CheckNWChallengeCompletion("Nuke2")) {
         if(%client.CheckNWChallengeCompletion("Nuke3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert - Challenge Set Complete");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert III - Call in 25 Nukes");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert II - Call in 10 Nukes");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert I - Call in 5 Nukes");
      %index++;
   }
   //
   if(%client.TWM2Core.Officer >= 1) {
      if(%client.CheckNWChallengeCompletion("Fission1")) {
         if(%client.CheckNWChallengeCompletion("Fission2")) {
            if(%client.CheckNWChallengeCompletion("Fission3")) {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert - Challenge Set Complete");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert III - Call in 5 Fission Bombs");
               %index++;
            }
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert II - Call in 2 Fission Bombs");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert I - Call in 1 Fission Bomb");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked - Requires Instructive Officer Rank (Off. Rank 1)");
      %index++;
   }
   return %index;
}

function KS_Attenuate(%index) {
   if(isSet($KS_Attenuate[%index])) {
      return $KS_Attenuate[%index];
   }
   else {
      //begin list sort by player kills
      for (%i = 1; %i <= $KillstreakCount; %i++) {
         $KS_Attenuate[%i] = %i;
      }
      %swap = true;
      while(%swap) {
         %swap = false;
         for (%i = $KillstreakCount; %i >= 1; %i--) {
            %killI = getField($Killstreak[%i], 1);
            %killX = getField($Killstreak[%i+1], 1);
            if(%killI > %killX && ($KS_Attenuate[%i] < $KS_Attenuate[%i+1])) {
               $KS_Attenuate[%i+1] = %i;
               $KS_Attenuate[%i] = %i+1;
               %swap = true;
            }
         }
      }
      //
      return $KS_Attenuate[%index];
   }
}
schedule(2500, 0, KS_Attenuate, 1);

//dev. function
function clearAttenuate() {
   for (%i = 1; %i < $KillstreakCount; %i++) {
      $KS_Attenuate[%i] = "";
   }
}
