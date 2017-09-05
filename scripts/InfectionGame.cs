// DisplayName = TWM Infection

//--- GAME RULES BEGIN ---
// Humans vs. Zombies
// 15 Seconds into the Game, A Human becomes a zombie
// The Zombie Tries To Infect all the humans
// Humans Killed By The Zombie, Become Zombies
// Zombies Can Choose What Zombie To Use (Normal, Demon, Lord)
//--- GAME RULES END ---

$InvBanList[Infection, "GrappleHook"] = 1;

$InfectionGame::Min2Alphas = 7;
$InfectionGame::Min3Alphas = 10;
$InfectionGame::Rounds = 5;

// spam fix
function InfectionGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function InfectionGame::allowsProtectedStatics(%game) {
	return true;
}

function InfectionGame::pickTeamSpawn(%game, %team) {
   %pos = vectorAdd($InfectionGame::SpawnLocation[$CurrentMission], TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
   %pos = vectorAdd(%pos,"0 0 5");
   return %pos;
}


function InfectionGame::clientMissionDropReady(%game, %client) {
    $InfectionGame::Score[%client] = 0;
    $InfectionGame::ClientZombie[%client] = "Norm";
    messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
	DefaultGame::clientMissionDropReady(%game, %client);
}

function InfectionGame::onAIRespawn(%game, %client)
{
   //add the default task
	if (! %client.defaultTasksAdded)
	{
		%client.defaultTasksAdded = true;
	   %client.addTask(AIPickupItemTask);
	   %client.addTask(AIUseInventoryTask);
	   %client.addTask(AITauntCorpseTask);
		%client.addTask(AIEngageTurretTask);
		%client.addTask(AIDetectMineTask);
		%client.addTask(AIBountyPatrolTask);
		%client.bountyTask = %client.addTask(AIBountyEngageTask);
	}

   //set the inv flag
   %client.spawnUseInv = true;
}

function InfectionGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function InfectionGame::timeLimitReached(%game) {
	logEcho("game over (timelimit)");
	%game.gameOver();
	cycleMissions();
}

function InfectionGame::scoreLimitReached(%game) {
	logEcho("game over (scorelimit)");
	%game.gameOver();
	cycleMissions();
}

function InfectionGame::startMatch(%game) {
    activatePackage(InfectionGamePackage);
    $TWM::PlayingInfection = 1;
    $TWM::TeamWars = 1;
    $Host::RankSystem = 0;
    $InfectionGame::TimeTilInfect = 15;
    $InfectionGame::RoundNumber = 1;
    $InfectionGame::ZombieTier = 1;
    $InfectionGame::Intermit = 1;
    
    $InfectionGame::NoRespawning = 0;
    messageAll('MsgSPCurrentObjective1', "Selecting Alpha Zombie");
    messageAll('MsgSPCurrentObjective2', "Alpha Zombie Selected In: "@$InfectionGame::TimeTilInfect@" Seconds.");
    %game.StartTimeUntilInfect($InfectionGame::TimeTilInfect);
    DefaultGame::StartMatch(%game);
    Game.NumTeams = 1;
    //Disable Killstreaks and Perks
    for(%i = 0; %i < ClientGroup.getCount(); %i++) {
       %client = ClientGroup.getObject(%i);
       DisableAllPerkGroup(%client, 1);
       DisableAllPerkGroup(%client, 2);
       DisableAllPerkGroup(%client, 3);
       %client.DisableAllKillstreaks();
       messageClient(%client, 'msgOffline', "\c5INFECTION: All Killstreaks and Perks Disabled.");
    }
}

function InfectionGame::TryInfectAnother(%game) {
   %selected = ClientGroup.getObject(GetRandom()*ClientGroup.getCount());
   if($InfectionGame::IsAlpha[%selected] || %selected.team == 0) {
      //do not pick observers or already infected
      return %game.TryInfectAnother();
   }
   return %selected;
}

function InfectionGame::StartTimeUntilInfect(%game, %time) {
   %time--;
   if(%time <= 0) {
      if (ClientGroup.getCount() <= 1) {
         MessageAll('MsgError',"\c5Insufficient Players, Need At Least 2");
         $InfectionGame::TimeTilInfect = 15;
         %time = 15;
         %game.schedule(1000, "StartTimeUntilInfect", %time);
         return;
      }
      %ZString = "";
      if(ClientGroup.getCount() >= $InfectionGame::Min3Alphas) {
         for(%x = 0; %x < 3; %x++) {
            %selected = ClientGroup.getObject(GetRandom()*ClientGroup.getCount());
            if($InfectionGame::IsAlpha[%selected]) {   //If this one is selected
               %selected = %game.TryInfectAnother();   //grab a different client
            }
            $InfectionGame::Infected[%selected] = 1;
            $InfectionGame::IsAlpha[%selected] = 1;
      //      Game.clientChangeTeam( %selected, 2, 0 );
            $InfectionGame::ClientZombie[%selected] = "Demon"; //we start them as demonz :)
            if(isObject(%selected.player)) {
               %targetlastpos = %selected.player.getworldboxcenter();
               makePersonZombie(%targetlastpos, %selected, 4);
            }
            %ZString = ""@%ZString@" "@%selected.namebase@"";
         }
         for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		    %client = ClientGroup.getObject(%i);
            messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Alpha Zombies:"@%ZString@"");
            messageClient(%client, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%client]@" | TIER 1.");
         }
         $InfectionGame::Intermit = 0;
         %game.timeUpdateLoop = %game.schedule(1, DoTierUpgrades);
         return;
      }
      else if(ClientGroup.getCount() >= $InfectionGame::Min2Alphas && ClientGroup.getCount() < $InfectionGame::Min3Alphas) {
         for(%x = 0; %x < 2; %x++) {
            %selected = ClientGroup.getObject(GetRandom()*ClientGroup.getCount());
            if($InfectionGame::IsAlpha[%selected]) {   //If this one is selected
               %selected = %game.TryInfectAnother();   //grab a different client
            }
            $InfectionGame::Infected[%selected] = 1;
            $InfectionGame::IsAlpha[%selected] = 1;
      //      Game.clientChangeTeam( %selected, 2, 0 );
            $InfectionGame::ClientZombie[%selected] = "Demon"; //we start them as demonz :)
            if(isObject(%selected.player)) {
               %targetlastpos = %selected.player.getworldboxcenter();
               makePersonZombie(%targetlastpos, %selected, 4);
            }
            %ZString = ""@%ZString@" "@%selected.namebase@"";
         }
         for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		    %client = ClientGroup.getObject(%i);
            messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Alpha Zombies:"@%ZString@"");
            messageClient(%client, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%client]@" | TIER 1.");
         }
         $InfectionGame::Intermit = 0;
         %game.timeUpdateLoop = %game.schedule(1, DoTierUpgrades);
         return;
      }
      else {
         %selected = ClientGroup.getObject(GetRandom()*ClientGroup.getCount());
         $InfectionGame::Infected[%selected] = 1;
         $InfectionGame::IsAlpha[%selected] = 1;
   //      Game.clientChangeTeam( %selected, 2, 0 );
         $InfectionGame::ClientZombie[%selected] = "Demon"; //we start them as demonz :)
         if(isObject(%selected.player)) {
            %targetlastpos = %selected.player.getworldboxcenter();
            makePersonZombie(%targetlastpos, %selected, 4);
         }
         for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		    %client = ClientGroup.getObject(%i);
            messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Alpha Zombie: "@%selected.namebase@"");
            messageClient(%client, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%client]@" | TIER 1.");
         }
         messageClient(%selected, 'MsgSPCurrentObjective1' ,"", "You are The Alpha Zombie");
         $InfectionGame::Intermit = 0;
         
         %game.timeUpdateLoop = %game.schedule(1, DoTierUpgrades);
         return;
      }
   }
   %game.schedule(1000, "StartTimeUntilInfect", %time);
}

function InfectionGame::CheckPlayersAndLMS(%game) {
   %living = 0;
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %client = ClientGroup.getObject(%i);
      if(!$InfectionGame::Infected[%client]) {
         if(%client.team != 0)
            %living++;
      }
   }
   if(%living == 0) {
      if($InfectionGame::RoundNumber >= $InfectionGame::Rounds) {
         //%game.gameOver();
         //CycleMissions();
         $InfectionGame::NoRespawning = 1;
         $InfectionGame::RoundNumber = 1;
         MessageAll('MsgAdminForce', "\c5Infection: The Game has completed! Momentarily a vote will be initiated, vote yes to change maps, or no to stay on it.");
         messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Game Over: Map Cycle Vote");

         cancel(%game.timeUpdateLoop);
         %game.timeinProgress = 0;
         %game.schedule(3500, "startMapCycleVote");
      }
      else {
         $InfectionGame::RoundNumber++;
         %game.Intermission();
      }
   }
}

function InfectionGame::startMapCycleVote(%game) {
   if(%game.schedulevote !$="") {
      cancel(%game.ScheduleVote);
      %Game.scheduleVote = "";
      %Game.kickClient = "";
      clearVotes();
      %count = clientgroup.getcount();
      for(%i = 0; %i < %count; %i++) {
         messageClient(clientgroup.getobject(%i), 'closeVoteHud', "");
      }
      messageAll('MsgAdminForce', "\c2SERVER: Current vote canceled to allow Infection Map Cycle Vote.");
   }
   
   for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ ) {
      %cl = ClientGroup.getObject( %idx );
      if ( !%cl.isAIControlled() ) {
         messageClient( %cl, 'VoteStarted', '\c2SERVER: Change Maps? Vote Yes to change, or no to restart this map at Round 1.');
         %clientsVoting++;
      }
   }
   for ( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
      %cl = ClientGroup.getObject( %clientIndex );
      if ( !%cl.isAIControlled() ) {
         messageClient(%cl, 'openVoteHud', "", %clientsVoting, ($Host::VotePassPercent / 100));
      }
   }
   clearVotes();
   %Game.voteType = "MapCycleVote";
   %Game.scheduleVote = %game.schedule( ($Host::VoteTime * 1000), "MapCycleVoteEval", false);
}

function InfectionGame::MapCycleVoteEval(%game, %isVP) {
   if (%game.scheduleVote !$= "") {
      %votesFor = 0;
      %votesAgainst = 0;
      for (%player = 0; %player < ClientGroup.GetCount(); %player++) {
         %client = ClientGroup.getObject(%player);
         if ( %client.vote !$= "" ) {
            if ( %client.vote ) {
               %votesFor++;
            }
            else {
               %votesAgainst++;
            }
         }
      }
      //who wins?!!?
      if (%VotesFor > %votesAgainst) {
         messageAll('MsgVotePassed', '\c1Map changes by vote');
         %game.gameOver();
         CycleMissions();
         $InfectionGame::NoRespawning = 0;
      }
      else {
         messageAll('MsgVoteFailed', '\c2Current map continues. Starting Round 1.');
         
         $InfectionGame::NoRespawning = 0;
         %count = ClientGroup.getCount();
         for(%i = 0; %i < %count; %i++) {
            %client = ClientGroup.getObject(%i);
            $InfectionGame::Infected[%client] = 0;
            $InfectionGame::IsAlpha[%client] = 0;
            $InfectionGame::Intermit = 1;
            $InfectionGame::ClientZombie[%client] = "Norm";

            messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Intermission");
            if(isObject(%client.player)) {
               %client.player.scriptKill(0); // no damage type = no infect :)
            }
         }
         $InfectionGame::TimeTilInfect = 30;
         $InfectionGame::ZombieTier = 1;
         %game.StartTimeUntilInfect($InfectionGame::TimeTilInfect);
      }
      // Housekeeping time..
      for(%cl = 0; %cl < ClientGroup.getCount(); %cl++) {
         %client = ClientGroup.getObject(%cl);
         %client.vote = "";
         Game.voteType = "";
         messageClient(%client, 'clearVoteHud', "");
         messageClient(%client, 'closeVoteHud', "");
      }
      Game.scheduleVote = "";
   }
}

function InfectionGame::Intermission(%game) {
   MessageAll('MsgComplete',"\c5Intermission, Beginning Round "@$InfectionGame::RoundNumber@" of "@$InfectionGame::Rounds@" in 30 Seconds.");
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %client = ClientGroup.getObject(%i);
      $InfectionGame::Infected[%client] = 0;
      $InfectionGame::IsAlpha[%client] = 0;
      $InfectionGame::ClientZombie[%client] = "Norm";
      $InfectionGame::Intermit = 1;

//      Game.clientChangeTeam( %client, 1, 0 );

      messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Intermission");
      if(isObject(%client.player)) {
         %client.player.scriptKill(0); // no damage type = no infect :)
      }
   }
   $InfectionGame::TimeTilInfect = 30;
   $InfectionGame::ZombieTier = 1;
   %game.StartTimeUntilInfect($InfectionGame::TimeTilInfect);
   
   cancel(%game.timeUpdateLoop);
   %game.timeinProgress = 0;
}

function InfectionGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation) {
   Parent::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation);
   if(%clVictim !$= "") {
      if($InfectionGame::Intermit == 1) {
         CenterPrint(%clVictim, "<color:FF0000>ALERT: INTERMISSION\nIn Active Game Time, you Would be Infected, Be Advised!.",2,3);
         return;
      }
      if($InfectionGame::Infected[%clKiller] || (%damageType $= $DamageType::Zombie || %damageType $= $DamageType::Suicide || %damageType $= $DamageType::FellOff)) {
         if(!$InfectionGame::Infected[%clVictim]) {
            $InfectionGame::Infected[%clVictim] = 1;
            $InfectionGame::ClientZombie[%clVictim] = "Norm";
//            Game.clientChangeTeam( %client, 2, 0 );
            CenterPrint(%clVictim, "<color:FF0000>You have been infected.",2,3);
            Echo("Infection: "@%clVictim.namebase@" has been infected.");
            $InfectionGame::Score[%clKiller] += 10;
            messageClient(%clKiller, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%clKiller]@"");
         }
         else {
            CenterPrint(%clKiller, "<color:FF0000>Blargh, Don't kill your brothers!!! \n -5 Points",2,3);
            $InfectionGame::Score[%clKiller] -= 5;
            messageClient(%clKiller, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%clKiller]@"");
         }
      }
      else {
         if($InfectionGame::Infected[%clVictim]) {
            $InfectionGame::Score[%clKiller]++;
            messageClient(%clKiller, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%clKiller]@"");
         }
         else {
            CenterPrint(%clKiller, "<color:FF0000>Don't Kill The Living!!! \n -5 Points",2,3);
            $InfectionGame::Score[%clKiller] -= 5;
            messageClient(%clKiller, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%clKiller]@"");
         }
      }
      %game.CheckPlayersAndLMS();
   }
}

function InfectionGame::DoTierUpgrades(%game) {
   %game.timeinProgress++;
   
   %timeToNext = (5*60 + (5*60*($InfectionGame::ZombieTier-1))) - (%game.timeinProgress);
   %min = mFloor(%timeToNext / 60);
   %sec = %timeToNext % 60;
   if(%sec < 10) {
      %sec = "0"@%sec;
   }
   
   for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
      %client = ClientGroup.getObject(%i);
      //messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Alpha Zombie: "@%selected.namebase@"");
      if($InfectionGame::ZombieTier < 4) {
         messageClient(%client, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%client]@" | TIER "@$InfectionGame::ZombieTier@" ["@%min@":"@%sec@"].");
      }
      else {
         messageClient(%client, 'MsgSPCurrentObjective2' ,"", "Score: "@$InfectionGame::Score[%client]@" | TIER "@$InfectionGame::ZombieTier@".");
      }
   }
   
   if(%game.timeinProgress >= (5*60) && $InfectionGame::ZombieTier <= 1) {
      $InfectionGame::ZombieTier = 2;
      CenterPrintAll("TIER 2 ZOMBIE ARMORS UNLOCKED!", 3, 3);
   }
   if(%game.timeinProgress >= (10*60) && $InfectionGame::ZombieTier <= 2) {
      $InfectionGame::ZombieTier = 3;
      CenterPrintAll("TIER 3 ZOMBIE ARMORS UNLOCKED!", 3, 3);
   }
   if(%game.timeinProgress >= (15*60) && $InfectionGame::ZombieTier <= 3) {
      $InfectionGame::ZombieTier = 4;
      CenterPrintAll("TIER 4 ZOMBIE ARMORS UNLOCKED!\nHIGHEST TIER ACHIEVED!", 3, 3);
   }
   
   %game.timeUpdateLoop = %game.schedule(1000, DoTierUpgrades);
}

function InfectionArmors(%client, %armorList) {
   if ( %client.favorites[0] !$= "Scout") {
      %armorList = %armorList TAB "Scout";
   }
   return %armorList;
}

function InfectionGame::forceObserver( %game, %client, %reason ) {
   //if($InfectionGame::IsAlpha[%client]) {
   //   %game.Intermission();
   //}
   DefaultGame::forceObserver( %game, %client, %reason );
}

function InfectionGame::gameOver(%game) {
	//call the default
    deactivatePackage(InfectionGamePackage);
    $TWM::PlayingInfection = 0;
    $TWM::TeamWars = 0;
    $Host::RankSystem = 1;
	DefaultGame::gameOver(%game);

	//send the winner message
	%winner = "";
	messageAll('MsgClearObjHud', "");
 
	for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		%client = ClientGroup.getObject(%i);
        //reset all vars
        $InfectionGame::Infected[%client] = 0;
        $InfectionGame::IsAlpha[%client] = 0;
        $InfectionGame::Score[%client] = 0;
  
		%game.resetScore(%client);
	}
}

function InfectionGame::leaveMissionArea(%game, %playerData, %player) {
   if(%player.getState() $= "Dead")
      return;

   %player.client.outOfBounds = true;
   %player.OutsideKill = schedule(7500,0,"KillOutside", %player);
   messageClient(%player.client, 'LeaveMissionArea', '\c1You left the mission area, Return or be killed.~wfx/misc/warning_beep.wav');

}

function InfectionGame::enterMissionArea(%game, %playerData, %player) {
   if(%player.getState() $= "Dead")
      return;

   cancel(%player.OutsideKill);
   %player.client.outOfBounds = false;
   messageClient(%player.client, 'EnterMissionArea', '\c1You are back in the mission area.');
}

function KillOutside(%p) {
   if(isObject(%p) && %p.getState() !$= "dead") {
   %p.scriptKill(0);
   messageClient(%p.client, 'die', '\c1You were killed for being outside of the mission area.');
   MessageAll('Dead',"\c0"@%p.client.namebase@" was killed for leaving the mission area for too long.");
   }
}

function InfectionGame::ResetScore(%client) {

}

package InfectionGamePackage {
   function GameConnection::onDrop(%client, %reason) { //no changes made
      if($InfectionGame::IsAlpha[%client]) {
         Game.Intermission();
      }
      parent::onDrop(%client, %reason);
   }
};

function InfectionGame::spawnPlayer( %game, %client, %respawn ) {
    if($InfectionGame::NoRespawning) {
       CenterPrint(%client, "<color:FF0000>The Game is Currently in Intermission.",2,3);
       return;
    }

	Parent::spawnPlayer( %game, %client, %respawn );
}



$ZArmor[0, 1] = "Norm\t0\tNormal Zombie\tAbility: Jet Key To Lunge";
$ZArmor[1, 1] = "Demon\t0\tDemon Zombie\tAbility: Jet Key To Throw Fireball";
$ZArmor[2, 1] = "Lord\t1\tZombie Lord\tAbilities: Jet Key To Fire, Mine Key To Lift";

$ZArmor[0, 2] = "Norm\t0\tNormal Zombie\tAbility: Jet Key To Lunge";
$ZArmor[1, 2] = "Rav\t0\tRavenger Zombie\tAbility: Jet Key To Speed Forward/Lunge";
$ZArmor[2, 2] = "Lord\t0\tZombie Lord\tAbilities: Jet Key To Fire, Mine Key To Lift";
$ZArmor[3, 2] = "Demon\t0\tDemon Zombie\tAbility: Jet Key To Throw Fireball";
$ZArmor[4, 2] = "Rap\t1\tAir Rapier Zombie\tAbility: Jet Key Flies";

$ZArmor[0, 3] = "Norm\t0\tNormal Zombie\tAbility: Jet Key To Lunge";
$ZArmor[1, 3] = "Rav\t0\tRavenger Zombie\tAbility: Jet Key To Speed Forward/Lunge";
$ZArmor[2, 3] = "Lord\t0\tZombie Lord\tAbilities: Jet Key To Fire, Mine Key To Lift";
$ZArmor[3, 3] = "Demon\t0\tDemon Zombie\tAbility: Jet Key To Throw Fireball";
$ZArmor[4, 3] = "Rap\t0\tAir Rapier Zombie\tAbility: Jet Key Flies";
$ZArmor[5, 3] = "VolRav\t0\tVolatile Ravenger Zombie\tAbility: Jet Key To Detonate C4";
$ZArmor[6, 3] = "UDem\t1\tUltra Demon Zombie\tAbility: Jet Key To Fire, Mine To Charge Forward";
$ZArmor[7, 3] = "Wraith\t1\tWraith Spec-Ops Zombie\tAbility: Jet Key To Charge, Armed with MP26-CMDO";

$ZArmor[0, 4] = "Norm\t0\tNormal Zombie\tAbility: Jet Key To Lunge";
$ZArmor[1, 4] = "Rav\t0\tRavenger Zombie\tAbility: Jet Key To Speed Forward/Lunge";
$ZArmor[2, 4] = "Lord\t0\tZombie Lord\tAbilities: Jet Key To Fire, Mine Key To Lift";
$ZArmor[3, 4] = "Demon\t0\tDemon Zombie\tAbility: Jet Key To Throw Fireball";
$ZArmor[4, 4] = "Rap\t0\tAir Rapier Zombie\tAbility: Jet Key Flies";
$ZArmor[5, 4] = "VolRav\t0\tVolatile Ravenger Zombie\tAbility: Jet Key To Detonate C4";
$ZArmor[6, 4] = "UDem\t0\tUltra Demon Zombie\tAbility: Jet Key To Fire, Mine To Charge Forward";
$ZArmor[7, 4] = "Wraith\t0\tWraith Spec-Ops Zombie\tAbility: Jet Key To Charge, Armed with MP26-CMDO";
$ZArmor[8, 4] = "Rog\t1\tGeneral Rog\tAbilities: Jet Key To fire zMG42, Shielded, Armed with BoV";

//Score Hud
$ScoreHudMaxVisible = 19;
function InfectionGame::updateScoreHud(%game, %client, %tag) {
   messageClient( %client, 'SetScoreHudHeader', "", "" );
   messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tGTP\t1>Infection - Command Hud</a><rmargin:600><just:right><a:gamelink\tNAC\t1>Close</a>" );
   messageClient( %client, 'SetScoreHudSubheader', "", "Infection Player Settings" );
   //The Menu
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Settings:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Zombie Armor [TIER "@$InfectionGame::ZombieTier@"]");
   %index++;
   %i = 0;
   while(isSet($ZArmor[%i, $InfectionGame::ZombieTier])) {
      %z = $ZArmor[%i, $InfectionGame::ZombieTier];
      if(getField(%z, 1) $= "1") {
         if($InfectionGame::IsAlpha[%client]) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSetZArmor\t"@getField(%z, 0)@"\t"@%i@">"@getField(%z, 2)@"</a> - "@getField(%z, 3));
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSetZArmor\t"@getField(%z, 0)@"\t"@%i@">"@getField(%z, 2)@"</a> - "@getField(%z, 3));
         %index++;
      }
      %i++;
   }
   //end
   messageClient( %client, 'ClearHud', "", %tag, %index );
}

function InfectionGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5){
%tag = $TagToUseForScoreMenu;
messageClient( %client, 'ClearHud', "", %tag, 1 );
//Stuff
if(%arg1 $= "")
%arg1 = "Null";
if(%arg2 $= "")
%arg2 = "Null";
if(%arg3 $= "")
%arg3 = "Null";
if(%arg4 $= "")
%arg4 = "Null";
if(%arg5 $= "")
%arg5 = "Null";

echo("[F2] "@%client.namebase@": "@%arg1@", "@%arg2@", "@%arg3@", "@%arg4@", "@%arg5@".");
    switch$ (%arg1){
        case "NULL":
             echo("Null Arg");
             return;
   
        case "GTP":
             %game.updateScoreHud(%client, %tag); //Infection Calls This
             %client.SCMPage = %arg2;
             return;

        case "NAC":
             closeScoreHudFSERV(%client);
             return;
             
        case "SetZArmor":
             echo("Set Z Armor");
             %armor = %arg2;

             if(getField($ZArmor[%arg3, $InfectionGame::ZombieTier], 1) $= "1") {
                if(!$InfectionGame::IsAlpha[%client]) {
                   MessageClient(%client, 'MsgNo', "\c5INFECTION: Alpha Zombie Status Required.");
                   closeScoreHudFSERV(%client);
                   return;
                }
             }

             $InfectionGame::ClientZombie[%client] = %armor;
             MessageClient(%client, 'MsgNo', "\c5INFECTON: Zombie Armor Set To "@%armor@"");
             %game.updateScoreHud(%client, %tag); //Infection Calls This
             return;
   }
}





























$InfectionGame::SpawnLocation["EngelamHimmel"] = "126.7 14.7 181";
$InfectionGame::SpawnLocation["DerGott"] = "0.0299911 -3.61698 155";
$InfectionGame::SpawnLocation["Strikers2"] = "-9.76935 149.965 105";
$InfectionGame::SpawnLocation["Oasis2"] = "-70.435 -46.3061 116.136";
$InfectionGame::SpawnLocation["MrykWood2"] = "-314 -7.5 103.543";
$InfectionGame::SpawnLocation["Skyline"] = "-58.8556 -420.953 750.438";
$InfectionGame::SpawnLocation["GeometricOrder"] = "-172.325 -396.557 159.9";
