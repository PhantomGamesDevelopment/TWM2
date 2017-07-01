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
//Coming Soon 3.9.2
//$Killstreak[18] = "PulseStar Shield System\t"@$Killstreak::Kills["PulseStar", 0]@"\t"@$Killstreak::Kills["PulseStar", 1]@"\tAirdrop an advanced Harbinger shield system.";
//$Killstreak[19] = "LOAS\t"@$Killstreak::Kills["LOAS", 0]@"\t"@$Killstreak::Kills["LOAS", 1]@"\tControl a localized satellite to direct high powered explosive rod strikes.";

//Phantom: V3.9: Order the streaks based on killcounts...
function OrderStreaks() {
   echo("Ordering Killstreak List...");
   for(%i = 1; %i <= $KillstreakCount; %i++) {
      %cur = getField($Killstreak[%i], 1);
      
      %temp = $Killstreak[%i];
      $OrderedKillstreak[%i] = $Killstreak[%i];
      
      for(%x = 1; %x <= %i; %x++) {
         %mine = getField($Killstreak[%x], 1);
         //This streak has a lower count, sift it down
         if(%mine < %cur) {
            $OrderedKillstreak[%i] = $Killstreak[%x];
            $OrderedKillstreak_CONVINDX[%i] = %x;
            $OrderedKillstreak[%x] = %temp;
            $OrderedKillstreak_CONVINDX[%x] = %i;
         }
      }
   }
   echo("Complete...");
   for(%r = 1; %r <= $KillstreakCount; %r++) {
      echo(""@%r@": "@$OrderedKillstreak[%r]@" => "@$OrderedKillstreak_CONVINDX[%r]);
   }
}

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
    if(getWordCount(%client.streakList()) > 0) {
       %client.player.setInventory(KillstreakBeacon, 1, true);
    }
    if(!%client.isconfiscated) {
	   if (%client.isAdmin) {
          %client.player.setInventory(SuperChaingun,1,true);
	   }
    }
}

function GameConnection::streakList(%client) {
   %total = "";
   for(%i = 1; %i <= $KillstreakCount; %i++) {
      if(%client.streakCount[%i] > 0) {
         if(%total $= "") {
            %total = %i;
         }
         else {
            %total = %total @ " " @ %i;
         }
      }
   }
   return %total;
}

function GameConnection::AwardKillstreak(%client, %streakVal, %plz) {
   if(%plz $= "") {
      %plz = 1;
   }
   //the switchith
   if($Killstreak::Setting == 4) {
      return;
   }
   if(%plz != -1 && (!%client.isActiveStreak(%streakVal) && ($Killstreak::Setting != 2) && !$TWM::PlayingHelljump)) {
      return;
   }
   switch(%streakVal) {
      case 1:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: UAV Recon at Your Disposal.");
      case 2:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Airstrike Standing By.");
      case 3:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Guided Missile Strike Standing By.");
      case 4:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Helicopter at your disposal.");
      case 5:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Plasma Harrier Strike at your disposal.");
      case 6:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Satellite Strike at your disposal.");
      case 7:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Gunship Helicopter at your disposal.");
      case 8:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Stealth Bomber at your disposal.");
      case 9:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Harbinger's Wrath Standing By.");
      case 10:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Apache Gunner Standing By.");
      case 11:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: AC-130 Gunner Standing By.");
      case 12:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Centaur Bombardment Standing By.");
      case 13:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Mass EMP Standing By.");
      case 14:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Nuclear Strike Standing By.");
      case 15:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Zombie Bomb Standing By.");
      case 16:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Fission Bomb Ready... Obliterate everyone!!!");
      case 17:
         MessageClient(%client, 'MsgZKill', "\c5TWM2: Napalm Airstrike at your disposal.");
   }
   %client.streakCount[%streakVal]++;
   if(%plz == 0) {
      if(%client.IsHighestPLStreak(%streakVal)) {
         %client.player.killsinarow = 0; //reset for moar killstreaks!
      }
   }
   else if(%plz == -1) {
      //From //giveKSSW
   }
   else {
      if(!$TWM::PlayingHellJump) {
         if(%client.IsHighestZStreak(%streakVal)) {
            %client.player.zombiekillsinarow = 0; //reset for moar killstreaks!
         }
      }
   }
   %client.player.setInventory(KillstreakBeacon, 1, true);
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
