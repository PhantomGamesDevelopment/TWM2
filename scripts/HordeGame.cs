// DisplayName = Horde 3

//--- GAME RULES BEGIN ---
// Horde Returns To TWM With a Few New Rules
// 5:00 Initial Time To Build up and Fortify
// Zombies dont all spawn at once, but come
// at random intervals.
//
// Now With Changes (Horde 3)
//--- GAME RULES END ---

//HORDE 3 GAME
//COPYRIGHT 2009-2010, PHANTOM GAMES DEVELOPMENT
//ALL RIGHTS RESERVED

//Pos - FLBig - 52 0 101
//Pos - Flatdash - 64 265 179
$HordeGame::SpawnGraph["FlatlandBigH"] = "52 0 101";
$HordeGame::SpawnGraph["slapmydashH"] = "-64.7988 -267.13 131.439";
$HordeGame::SpawnGraph["FrozenNight"] = "30 26 80";
function HordeGame::pickPlayerSpawn(%game, %client, %respawn) {
   if (isobject(%client.spawnpoint)) {
      if (%client.spawnpoint.team == %client.team) {
         if ( (%client.spawnpoint.ispersonal != 1) || (%client==%client.spawnpoint.owner) ) {
            if (%client.spawnpoint.getdatablock().isspawnpoint==1) {
               return vectoradd(%client.spawnpoint.getposition(),"0 0 1.5") SPC "0 0 0 1";
            }
         }
         else {
            messageclient(%client,'',"\c3The spawn point you selected is a personal spawn point, spawning you at default spawn location.");
         }
      }
      else {
         messageclient(%client,'',"\c3The spawn point you selected is not on your team, spawning you at default spawn location.");
      }
   }
   if ( (!isobject(%client.spawnpoint)) && (%client.spawnpoint != 0) && (%client.spawnpoint !$= "") ) {
      messageclient(%client,'',"\c3The spawn point you selected does not exist, spawning you at default spawn location.");
   }

   // place this client on his own team, '%respawn' does not ever seem to be used
   //we no longer care whether it is a respawn since all spawns use same points.
   %loc = $HordeGame::SpawnGraph[$CurrentMission];
   %position = vectorAdd(%loc,VectorAdd(TWM2Lib_MainControl("getRandomPosition", 20 TAB 1), "0 0 10"));
   return %position;//return %game.pickTeamSpawn(%client.team);
}

function MakeReviveString() {
   %string = "";
   %revives = $HordeGame::RevivesLeft;
   for(%i = 1; %i <= 5; %i++) {
     if(%i <= $HordeGame::RevivesLeft) {
        %string = ""@%string@"[R]";
     }
     else {
        %string = ""@%string@"[X]";
     }
   }
   return %string;
}

function HordeGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function HordeGame::allowsProtectedStatics(%game) {
	return true;
}

//Score Menu Fix
function HordeGame::updateScoreHud(%game, %client, %tag) {
   ConstructionGame::updateScoreHud(%game, %client, %tag);
}

function HordeGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5){
  ConstructionGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5);
}
//

function HordeServerStatuser() {
   %count = ClientGroup.getCount();
   if(!%count || %count == 0 ||%count $= "") {   //no players..
      HordeServerSetStatus("Standby");
   }
   else {
      HordeServerSetStatus("Active");
   }
   schedule(10000,0,"HordeServerStatuser");
}

function HordeServerSetStatus(%status) {
   if(%status $= "Standby" && $HordeGame::GameStatus $= "Active") {
      $HordeGame::GameStatus = "Standby";
      Echo("Horde: Server switching to STANDBY mode");
   }
   else if(%status $= "Active" && $HordeGame::GameStatus $= "Standby") {
      $HordeGame::GameStatus = "Active";
      Echo("Horde: Server switching to ACTIVE mode");
   }
}

function HordeGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation) {
   Parent::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation);
   if(%clVictim !$= "" && !$Horde::Preparing) { //actual player died
      %clVictim.CantRespawn = 1; //gotta wait until next round / Wave
      CenterPrint(%clVictim, "<color:FF0000>You are out until the next Wave.",2,3);
       Echo("Horde: "@%clVictim.namebase@" has been killed.");
       HordeCheckForLivingPlayers(%game);
   }
}

function HordeCheckForLivingPlayers(%game) {
   if($Horde::Preparing) {
      return;
   }
   $HordeGame::LivingCount = 0;
   %count = ClientGroup.getCount();
   if(%count == 0) {   //no players, this fixed the constant game over stuff
      return;
   }
   for (%i = 0; %i < %count; %i++) {
   %cl = ClientGroup.getObject( %i );
      if(!isObject(%cl.player) || %cl.player.getstate() $= "dead") {
         //Do nothing
      }
      else {
         $HordeGame::LivingCount++;
      }
   }
   %game.UpdateClientScoreBar();
   if($HordeGame::LivingCount <= 0) {
   //Everyone is Dead, or nobody is in the server, game over
      %game.gameOver();
      cycleMissions();
   }
}

function HordeGame::UpdateClientScoreBar(%game) {
   %alv = $HordeGame::LivingCount;
   %s = MakeReviveString();
   for(%i = 0; %i < ClientGroup.getcount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      messageClient(%cl, 'MsgSPCurrentObjective1', "", "Wave "@$HordeGame::CurrentWave@" | Zombies Left: "@$HordeGame::Zombiecount@" | TEAM: "@$HordeGame::Score["Team"]@"");
      messageClient(%cl, 'MsgSPCurrentObjective2', "", "Players Alive: "@$HordeGame::LivingCount@" | "@%s@" | SCORE: "@$HordeGame::Score[%cl]@"");
   }
}

function HordeKillAllZombies() {
   Echo("Horde: Cleaning Zombies");
   %count = MissionCleanup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %obj = MissionCleanup.getObject(%i);
      if(isObject(%obj)) {
         if(%obj.iszombie) {
            %obj.scriptkill($DamageType::admin);  //They all went on crack and died XD
         }
         else {
            continue;
         }
      }
   }
}

function HordeAllowAllRespawn() {
   Echo("Horde: Players Can now respawn.");
   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      %cl.CantRespawn = 0;
      CenterPrint(%cl, "<color:FF0000>You can now respawn, but hurry, you have 15 seconds.",2,3);
   }
   messageAll('MsgSPCurrentObjective2' ,"", "You Can Now Spawn");
}

function LockOutCLsFromRespawn(%game) {
   Echo("Horde: Players Can no longer respawn.");
   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      %cl.CantRespawn = 1;
      bottomPrint(%cl, "<color:FF0000>The respawn timer has exipred.",2,3);
   }
   HordeCheckForLivingPlayers(%game); // Do this to see if anyone exists.
}

function HordeShowWaveBegin() {
   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      messageClient(%cl, 'MsgClient', "~wfx/misc/lightning_impact.wav");
      BottomPrint(%cl, "<color:FF0000>Horde \n Wave "@$HordeGame::CurrentWave@" \n Begins",5,3);
   }
}

function HordeGame::spawnPlayer( %game, %client, %respawn ) {
    if(%client.CantRespawn && !%client.IsActivePerk("Second Chance")) {
       CenterPrint(%client, "<color:FF0000>You are out until the next Wave.",2,3);
       return;
    }
    if(%client.IsActivePerk("Second Chance") && %client.CantRespawn) {
       if($HordeGame::RevivesLeft == 0) {
          CenterPrint(%client, "<color:FF0000>Second Chance: No Team Revives Left.",2,3);
          return;
       }
       else {
          $HordeGame::RevivesLeft--;
          %game.UpdateClientScoreBar();
       }
       messageAll('msgBack', "\c5HORDE: "@%client.namebase@" exerts second chance and respawns.");
    }
	Parent::spawnPlayer( %game, %client, %respawn );
    HordeCheckForLivingPlayers(%game);
}

function HordeGame::clientJoinTeam( %game, %client, %team, %respawn )
{
	// for multi-team games played with a single team
	if (Game.numTeams == 1) {
		%game.assignClientTeam( %client );
		// Spawn the player:
		%game.spawnPlayer( %client, %respawn );
		return;
	}

//error("DefaultGame::clientJoinTeam");
   if ( %team < 1 || %team > %game.numTeams )
      return;

   if( %respawn $= "" )
      %respawn = 1;

   %client.team = %team;
   %client.lastTeam = %team;
   setTargetSkin( %client.target, %game.getTeamSkin(%team) );
   setTargetSensorGroup( %client.target, %team );
   %client.setSensorGroup( %team );

   // Spawn the player:
   %game.spawnPlayer( %client, %respawn );

//   messageAllExcept( %client, -1, 'MsgClientJoinTeam', '\c1%1 joined %2.', %client.name, %game.getTeamName(%team), %client, %team );
//   messageClient( %client, 'MsgClientJoinTeam', '\c1You joined the %2 team.', $client.name, %game.getTeamName(%client.team), %client, %client.team );

   updateCanListenState( %client );

   logEcho(%client.nameBase@" (cl "@%client@") joined team "@%client.team);
	if ($Host::Prison::Enabled == true) {
		if (%client.isJailed)
			// If player should manage to get out of jail, re-spawn and re-start sentence time
			jailPlayer(%client,false,mAbs(%cl.jailTime));
	}
}

function HordeGame::assignClientTeam(%game, %client, %respawn ) {
   %numPlayers = ClientGroup.getCount();
   for(%i = 0; %i <= %game.numTeams; %i++)
      %numTeamPlayers[%i] = 0;

   for(%i = 0; %i < %numPlayers; %i = %i + 1)
   {
      %cl = ClientGroup.getObject(%i);
      if(%cl != %client)
	 %numTeamPlayers[%cl.team]++;
   }
   %leastPlayers = %numTeamPlayers[1];
   %leastTeam = 1;
   for(%i = 2; %i <= %game.numTeams; %i++)
   {
      if( (%numTeamPlayers[%i] < %leastPlayers) ||
	 ( (%numTeamPlayers[%i] == %leastPlayers) &&
	 ($teamScore[%i] < $teamScore[%leastTeam] ) ))
      {
	 %leastTeam = %i;
	 %leastPlayers = %numTeamPlayers[%i];
      }
   }

   %client.team = %leastTeam;
   %client.lastTeam = %team;

   // Assign the team skin:
   if ( %client.isAIControlled() )
   {
      if ( %leastTeam & 1 )
      {
	 %client.skin = addTaggedString( "basebot" );
	 setTargetSkin( %client.target, 'basebot' );
      }
      else
      {
	 %client.skin = addTaggedString( "basebbot" );
	 setTargetSkin( %client.target, 'basebbot' );
      }
   }
   else
      setTargetSkin( %client.target, %game.getTeamSkin(%client.team) );

   updateCanListenState( %client );

   logEcho(%client.nameBase@" (cl "@%client@") joined team "@%client.team);
}

function HordeGame::clientChangeTeam(%game, %client, %team, %fromObs) {
   if(%client.CantRespawn) {
      if(!%fromObs) {    //:D they tried to change teams
          CenterPrint(%client, "<color:FF0000>You are out until the next Wave. \n Although that was a good try :D",2,3);
          return;
      }
      else {
         CenterPrint(%client, "<color:FF0000>You are out until the next Wave.",2,3);
         return;
      }
   }
   %game.removeFromTeamRankArray(%client);

   %pl = %client.player;
   if(isObject(%pl)) {
      if(%pl.isMounted())
	     %pl.getDataBlock().doDismount(%pl);
      %pl.scriptKill(0);
   }

   // reset the client's targets and tasks only
   clientResetTargets(%client, true);

   // give this client a new handle to disassociate ownership of deployed objects
   if( %team $= "" && (%team > 0 && %team <= %game.numTeams))
   {
      if( %client.team == 1 )
	 %client.team = 2;
      else
	 %client.team = 1;
   }
   else
      %client.team = %team;

   // Set the client's skin:
   if (!%client.isAIControlled())
      setTargetSkin( %client.target, %game.getTeamSkin(%client.team) );
   setTargetSensorGroup( %client.target, %client.team );
   %client.setSensorGroup( %client.team );

   // Spawn the player:
   %client.lastSpawnPoint = %game.pickPlayerSpawn( %client );

   %game.createPlayer( %client, %client.lastSpawnPoint, $MatchStarted );

   if($MatchStarted)
      %client.setControlObject(%client.player);
   else
   {
      %client.camera.getDataBlock().setMode(%client.camera, "pre-game", %client.player);
      %client.setControlObject(%client.camera);
   }
   %game.onClientEnterObserverMode(%client);
   updateCanListenState( %client );

   // MES - switch objective hud lines when client switches teams
   messageClient(%client, 'MsgCheckTeamLines', "", %client.team);
   logEcho(%client.nameBase@" (cl "@%client@") switched to team "@%client.team);

}

function HordeRepairAndRearm(%cl) {
   if(isObject(%cl.player)) {
      buyfavorites(%cl);
      %cl.player.ApplyRepair(20.0);
   }
}

function HordeGame::clientMissionDropReady(%game, %client) {
   $HordeGame::Score[%client] = 0;
   %count = ClientGroup.getCount();
   if(!$Horde::Preparing) {
      if($MatchStarted && $CountdownStarted && %count == 1) { //Game started, first player is in
         messageClient(%client, 'MsgClient', "\c5Welcome To Horde, You are the first player, BEGIN!");
         %client.CantRespawn = 0; //they will get the first life
         $HordeGame::GameStatus = "Active"; //Force the server into Active Mode
      }
      else if($MatchStarted && $CountdownStarted && %count > 1) { // Game in progress, multiple players
         HordeCheckForLivingPlayers(%game); //we may have to restart the game
         messageClient(%client, 'MsgClient', "\c5Welcome To Horde, Please wait for the next wave to begin.");
         %client.CantRespawn = 1;
      }
   }
   messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
   defaultGame::clientMissionDropReady(%game, %client);
}

function HordeGame::onAIRespawn(%game, %client) {
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

function HordeGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function HordeGame::timeLimitReached(%game) {
    $TWM::HordeCalled = 1; //The Host Will Cycle
	logEcho("game over (timelimit)");
	%game.gameOver();
	cycleMissions();
}

function HordeNextWave(%game, %wave) {
   if($HordeGame::RevivesLeft < 5) {
      $HordeGame::RevivesLeft++;
   }
   $HordeGame::CurrentWave = %wave;
   HordeKillAllZombies(); //Cleans up the server
   if($HordeGame::CurrentWave == 51) { //Victory
      for (%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         CompleteNWChallenge(%cl, "ArmyOf50Stopped");
      }
  	  %game.gameOver();
      cycleMissions();
      return;
   }
   else if($HordeGame::CurrentWave == 26) {
      for (%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         CompleteNWChallenge(%cl, "Milestone25");
      }
   }
   else if($HordeGame::CurrentWave == 16) {
      for (%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         CompleteNWChallenge(%cl, "15For15");
      }
   }
   //
   //
   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      //
      HordeRepairAndRearm(%cl);
      //
      if($HordeGame::CurrentWave < 10) {
         %XPPerc = 0.25;
      }
      else if($HordeGame::CurrentWave >= 10 && $HordeGame::CurrentWave < 20) {
         %XPPerc = 0.5;
      }
      else if($HordeGame::CurrentWave >= 20 && $HordeGame::CurrentWave < 35) {
         %XPPerc = 0.75;
      }
      else {
         %XPPerc = 1;
      }
      if($HordeGame::CurrentWave <= 1 || $HordeGame::CurrentWave $= "") {

      }
      else {
         %XPGain = MFloor((((($HordeGame::CurrentWave - 1) * 500) * %XPPerc) / $HostGamePlayerCount) / 8);
         GainExperience(%cl, %XPGain, "Wave "@$HordeGame::CurrentWave - 1@" Complete ");
      }
      //
   }
   Echo("Horde: Players Moving into Wave "@%wave@".");
   HordeShowWaveBegin();
   HordeAllowAllRespawn(); // Go!
   schedule(15000,0,"LockOutCLsFromRespawn", %game);
   schedule(30000,0,"StartHordeZombies", $CurrentMission, %wave);
   $HordeGame::Zombiecount = 0;
   $HordeGame::NextWave = %wave + 1;
   $HordeGame::CanSpawnZombies = 1;
   $HordeGame::TeamKillsWave = 0;
   $HordeGame::TeamScoreWave = 0;
   DoWaveHighlights();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      %cl.waveKills = 0;
      %cl.waveScore = 0;
   }
   $Horde::Preparing = 0;
   %game.UpdateClientScoreBar();
}

function HordeGame::startMatch(%game) {
   $Host::nozombs = 0; //Allow Zombs
   $HordeGame::GameStatus = "Standby"; //Always Start In Standby
   $HordeGame::CanSpawnZombies = 1;
   $HordeGame::CurrentWave = 1;
   $HordeGame::NextWave = 2;
   $HordeGame::Zombiecount = 0; //Start at 0              `
   $HordeGame::LivingCount = 0; //Start at 0
   $HordeGame::RevivesLeft = 5; //Start With 5
   $Horde::Preparing = 1;
   
   $HordeGame::Score["Team"] = 0;
   $HordeGame::TeamKillsWave = 0;
   $HordeGame::TeamKills = 0;
   $HordeGame::TeamScoreWave = 0;
   
   for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
      %client = ClientGroup.getObject(%i);
      $HordeGame::Score[%client] = 0;
	  $HordeGame::FirstWaveKills[%client] = 0;
	  $HordeGame::HighestWaveScoreCount[%client] = 0;
   }

   $HordeGame::InitialGoTime = 300;
   Game.CountDownToGame();

   $TWM::PlayingHorde = 1;
   $TWM::HordeCalled = 1; //The Host Will Cycle
   $HordeGame::Game = %game; //This allows us to expand into other functions
   DefaultGame::startMatch(%game);
   HordeServerStatuser();
   messageAll('MsgSPCurrentObjective1' ,"", "Initial Startup");

   setSensorGroupCount(31);
}

function HordeGame::CountDownToGame(%game) {
   $HordeGame::InitialGoTime--;
   %time = $HordeGame::InitialGoTime;
   %min = mFloor(%time / 60);
   %sec = %time % 60;
   if(%sec < 10) {
      %sec = "0"@%sec;
   }
   messageAll('MsgSPCurrentObjective2' ,"", "Time Left: "@%min@":"@%sec@"");
   if(%time <= 0) {
      HordeNextWave(%game, 1);
      return;
   }
   %game.schedule(1000, "CountDownToGame");
}

function HordeGame::scoreLimitReached(%game) {
    $TWM::HordeCalled = 1; //The Host Will Cycle
	logEcho("game over (scorelimit)");
	%game.gameOver();
	cycleMissions();
}

function HordeGame::gameOver(%game) {
    //Dont end in standby, nobody is alive
    if($HordeGame::GameStatus $= "Standby") {
      return;
    }
	//call the default
    HordeAllowAllRespawn();
	DefaultGame::gameOver(%game);
 
//	cycleMissions();

	//send the winner message
	%winner = "";
    if($HordeGame::Zombiecount >= 1) {
       %winner = "Zombies";
    }
    else {
       %winner = "Humans";
    }

	if (%winner $= "Zombies")
		messageAll('MsgGameOver', "The Zombies have overrun the humans.~wvoice/announcer/ann.stowins.wav" );
    else
		messageAll('MsgGameOver', "The Humans have Survived the horde.~wvoice/announcer/ann.gameover.wav" );

    Echo("Horde: Game over, Winners : "@%winner@" on Wave "@$HordeGame::CurrentWave@".");

	messageAll('MsgClearObjHud', "");

    //Reset the Vars
    $TWM::PlayingHorde = 0;
    $TWM::HordeCalled = 1; //The Host Will Cycle
    $HordeGame::CurrentWave = 1;
    $HordeGame::NextWave = 2;
    $HordeGame::Zombiecount = 0;
    $HordeGame::LivingCount = 0;
    $HordeGame::CanSpawnZombies = 1;

	for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		%client = ClientGroup.getObject(%i);
		%game.resetScore(%client);
	}
	for(%j = 1; %j <= %game.numTeams; %j++)
		$TeamScore[%j] = 0;
}


// Working on this
function HordeGame::sendDebriefing( %game, %client ) {
   messageClient( %client, 'MsgDebriefResult', "", "<just:center>Game Over at wave "@$HordeGame::CurrentWave@"." );
   if($HordeGame::LivingCount > 0) {
      messageClient( %client, 'MsgDebriefResult', "", '<just:center>The humans have defeated the horde.' );
   }
   else {
      messageClient( %client, 'MsgDebriefResult', "", '<just:center>The zombie horde has overrun the humans.' );
   }
}

function HordeGame::vehicleDestroyed(%game, %vehicle, %destroyer) {
}

//Useful
function CheckLegitHordeMission(%mission) {
   if(%mission $= "FlatlandBigH") {
      return 1;
   }
   else if(%mission $= "slapmydashH") {  //flatdash
      return 1;
   }
   else if(%mission $= "FrozenNight") {  //flatdash
      return 1;
   }
   else {
      return 0;
   }
}


//ZOMBIE SPAWNING
//VARS (By mission)

//UPDATE!
//This system is now universal for any map
//Just pop in the mission name into these vars:
//$HordeGame::ZombiePtCnts["name"] = Spawn Positions; <- ammount of spawns
//$HordeGame::ZombieStartPt["name", Spawn position] = "X Y Z"; <- Coordinates of a spawn area
//After you make those entires, add an else if statement to the check
//function above to allow horde to be played on that map
// -Phantom139

//FL Big
$HordeGame::ZombiePtCnts["FlatlandBigH"] = 4;
$HordeGame::ZombieStartPt["FlatlandBigH", 1] = "0 150 110";
$HordeGame::ZombieStartPt["FlatlandBigH", 2] = "0 -150 110";
$HordeGame::ZombieStartPt["FlatlandBigH", 3] = "150 0 110";
$HordeGame::ZombieStartPt["FlatlandBigH", 4] = "-150 0 110";
//Flatdash
$HordeGame::ZombiePtCnts["slapmydashH"] = 4;
$HordeGame::ZombieStartPt["slapmydashH", 1] = "-39 101 208";
$HordeGame::ZombieStartPt["slapmydashH", 2] = "213 -325 140";
$HordeGame::ZombieStartPt["slapmydashH", 3] = "-72 -528 138";
$HordeGame::ZombieStartPt["slapmydashH", 4] = "-342 -272 138";
//Frozen Night
$HordeGame::ZombiePtCnts["FrozenNight"] = 4;
$HordeGame::ZombieStartPt["FrozenNight", 1] = "165 -29.3 80";
$HordeGame::ZombieStartPt["FrozenNight", 2] = "-14 -94 81";
$HordeGame::ZombieStartPt["FrozenNight", 3] = "-82 88.5 81";
$HordeGame::ZombieStartPt["FrozenNight", 4] = "94 209 186";
//

function HordeSpawnZombies(%pos, %type) {
   if(!isObject(Game) || !$TWM::PlayingHorde) {
      error("UE BLOCKED");
      return;
   }
   %c = CreateEmitter(%pos, NightmareGlobeEmitter, "0 0 1");
   %c.schedule(1000, "delete");
   schedule(500, 0, "StartAZombie", %pos, %type);
}

function StartHordeZombies(%mission, %wave) {
   //Prevent zombies from spawning in Standby Mode
   //Note: Whats the point, noone is in here.
   if(!$TWM::PlayingHorde) {
      return;
   }
   if($HordeGame::GameStatus $= "Standby") {
   Echo("Horde: Not Spawning Zombies, Server in StandBy Mode, Trying again in 60 Seconds");
   schedule(60000,0,"StartHordeZombies", %mission, %wave);
   return;
   }
   //Block multiple waves from spawning.
   if(!$HordeGame::CanSpawnZombies) {
      return;
   }
   if(CheckLegitHordeMission(%mission) == 1) {
      switch(%wave) {
         case 1:
            for(%i = 0; %i < 20; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 1);
            }
         case 2:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 1);
            }
         case 3:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 1);
            }
         case 4:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 1);
            }
         case 5:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 1);
            }
         case 6:
            for(%i = 0; %i < 20; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,2);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 7:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,2);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 8:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,2);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 9:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,2);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 10:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,2);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 11:
            for(%i = 0; %i < 20; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,3);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 12:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,3);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 13:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,3);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 14:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,3);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 15:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,3);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 16:
            for(%i = 0; %i < 20; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,4);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 17:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,4);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 18:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,4);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 19:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,4);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 20:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,4);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 21:
            for(%i = 0; %i < 20; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,5);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 22:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,5);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 23:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,5);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 24:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = getRandom(1,5);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         //The Rapier wave :D
         case 25:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 5);
            }
         case 26:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 6);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 27:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 6);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 28:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 6);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 29:
            for(%i = 0; %i < 45; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 7);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 30:
            for(%i = 0; %i < 50; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 7);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 31:
            for(%i = 0; %i < 20; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 7);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 32:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 7);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 33:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 7);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 34:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 35:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 36:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 37:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 38:
            for(%i = 0; %i < 35; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 39:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 40:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         //The Lord wave :D
         case 41:
            for(%i = 0; %i < 25; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 3);
            }
         //The Lord wave, 2.0 :D
         case 42:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 3);
            }
         //Slasher Wave
         case 43:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 11);
            }
         case 44:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 45:
            for(%i = 0; %i < 40; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 46:
            for(%i = 0; %i < 45; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         case 47:
            for(%i = 0; %i < 50; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %type = GetRandom(1, 8);
               if(%type > 5) {
                  %type += 3;
                  if(%type == 10) { //summoners don;t summon more summoners
                     %type = 12;
                  }
               }
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, %type);
            }
         //The sniper wave :p
         case 48:
            for(%i = 0; %i < 30; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 9);
            }
         //The ravie wave, V2.0 :p
         case 49:
            for(%i = 0; %i < 45; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 2);
            }
         //ZOMG!!!! Demon LORDS!!! RUN
         case 50:
            for(%i = 0; %i < 15; %i++) {
               %pt = getRandom(1,$HordeGame::ZombiePtCnts[%mission]);
               %final = vectoradd($HordeGame::ZombieStartPt[%mission, %pt], TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
               $HordeGame::Zombiecount++;
               %time = 1000 * getRandom(1, 60);
               schedule(%time, 0, "HordeSpawnZombies", %final, 6);
            }
         default:
         error("Horde: Wave Error, Wave "@%wave@" is unknown on "@%mission@"");
      }
   }
   else {
      error("Horde: Map Error, Wave "@%wave@" is unknown on "@%mission@"");
      error("Horde: Check function CheckLegitHordeMission, map "@%mission@" is not specified");
   }
   $HordeGame::CanSpawnZombies = 0;
   Game.UpdateClientScoreBar();
   echo("Horde: Spawn Zombies Called, "@$HordeGame::Zombiecount@" Spawned");
}

//
function HordeGame::ToggleModifiers(%game, %modifier, %toggleTo) {
   switch$(%modifier) {
      case "Titan":
         %ModifierDesc = "Death is quite costly... it ends the bonus strike";
         $HellJump::Modifier["Titan"] = %toggleTo;
      case "Super-Lunge":
         %ModifierDesc = "Normal Zombies lunge at 3X normal distance";
         $HellJump::Modifier["SuperLunge"] = %toggleTo;
      case "Kamakaziiiii":
         %ModifierDesc = "Volatile Ravenger's move at 5X Speed... be cautious...";
         $HellJump::Modifier["Kamakazi"] = %toggleTo;
      case "Where's My Head":
         %ModifierDesc = "Zombies cannot be killed by a headshot";
         $HellJump::Modifier["WheresMyHead"] = %toggleTo;
      case "You can't see me":
         %ModifierDesc = "Normal zombies are now cloaked... mwuhahaha!!!";
         $HellJump::Modifier["YouCantSeeMe"] = %toggleTo;
      case "Oh Lordy":
         %ModifierDesc = "Zombie lords shoot 4 pulses instead of 2";
         $HellJump::Modifier["OhLordy"] = %toggleTo;
      case "It BURNS!":
         %ModifierDesc = "Demon Zombie Fireballs now cause Burns";
         $HellJump::Modifier["ItBurns"] = %toggleTo;
      case "The Destiny":
         %ModifierDesc = "Volatile Ravengers explosive power is doubled";
         $HellJump::Modifier["TheDestiny"] = %toggleTo;
      case "Scrambler":
         %ModifierDesc = "Zombie lords jam helicopter signals blocking you from calling them in";
         $HellJump::Modifier["Scrambler"] = %toggleTo;
      case "Demonic":
         %ModifierDesc = "All zombies take 50% of normal damage, thus doubling their HP";
         $HellJump::Modifier["Demonic"] = %toggleTo;
      case "All On":
         %ModifierDesc = "All Modifiers on";
         $HellJump::Modifier["SuperLunge"] = 1;
         $HellJump::Modifier["Kamakazi"] = 1;
         $HellJump::Modifier["WheresMyHead"] = 1;
         $HellJump::Modifier["YouCantSeeMe"] = 1;
         $HellJump::Modifier["OhLordy"] = 1;
         $HellJump::Modifier["ItBurns"] = 1;
         $HellJump::Modifier["TheDestiny"] = 1;
         $HellJump::Modifier["Scrambler"] = 1;
         $HellJump::Modifier["Demonic"] = 1;
         %game.schedule(2100, "ToggleModifiers", "Titan", 1);
      case "All Off":
         %ModifierDesc = "All Modifiers Off";
         $HellJump::Modifier["SuperLunge"] = 0;
         $HellJump::Modifier["Kamakazi"] = 0;
         $HellJump::Modifier["WheresMyHead"] = 0;
         $HellJump::Modifier["YouCantSeeMe"] = 0;
         $HellJump::Modifier["OhLordy"] = 0;
         $HellJump::Modifier["ItBurns"] = 0;
         $HellJump::Modifier["TheDestiny"] = 0;
         $HellJump::Modifier["Scrambler"] = 0;
         $HellJump::Modifier["Demonic"] = 0;
         $HellJump::Modifier["Titan"] = 0;
   }
   if(%modifier !$= "All On" && %modifier !$= "All Off") {
      if(%toggleTo == 1) {
         %toDisp = "On";
      }
      else {
         %toDisp = "Off";
      }
      //and now lets display our message
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         bottomPrint(%cl, ""@%modifier@" - "@%toDisp@" \n "@%ModifierDesc@"", 2, 2);
         messageClient(%cl, 'MsgClient', "\c5HELLJUMP: "@%modifier@" - "@%toDisp@" : "@%ModifierDesc@"");
      }
   }
   else {
      //and now lets display our message
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         bottomPrint(%cl, ""@%modifier@" \n "@%ModifierDesc@"", 2, 2);
         messageClient(%cl, 'MsgClient', "\c5HELLJUMP: "@%modifier@" : "@%ModifierDesc@"");
      }
   }
}

function HordeGame::CheckModifier(%game, %mod) {
   return $HellJump::Modifier[""@%mod@""];
}

function HordeGame::AwardGamePoints(%game, %client, %points) {
   $HordeGame::Score[%client] += %points;
   $HordeGame::Score["Team"] += %points;
   $HordeGame::TeamScoreWave += %points;
   //
   %client.waveScore += %points;
}

function HordeGame::OnZombieDeath(%game, %killer, %victim) {
   %ZT = %victim.type;
   switch(%ZT) {
      case 1:
         %game.AwardGamePoints(%killer, 1);
      case 2:
         %game.AwardGamePoints(%killer, 3);
      case 3:
         %game.AwardGamePoints(%killer, 10);
      case 4:
         %game.AwardGamePoints(%killer, 5);
      case 5:
         %game.AwardGamePoints(%killer, 7);
      case 6:
         %game.AwardGamePoints(%killer, 40);
      case 9:
         %game.AwardGamePoints(%killer, 7);
      case 10:
         %game.AwardGamePoints(%killer, 15);
      case 12:
         %game.AwardGamePoints(%killer, 10);
      case 13:
         %game.AwardGamePoints(%killer, 20);
   }
   %game.UpdateClientScoreBar();
   // For the Wave Highlights.
   %killer.waveKills++;
   $HordeGame::TeamKillsWave++;
   $HordeGame::TeamKills++;
   if($HordeGame::TeamKillsWave == 1) {
      if($HordeGame::Zombiecount <= 1) {
         $HordeGame::TeamKillsWave = 0;
      }
      else {
         CenterPrintAll("<just:center>Wave Highlight \n"@%killer.namebase@" Scores the First Kill!" , 3, 3);
		 if($HordeGame::CurrentWave == 1) {
			CompleteNWChallenge(%killer, "FirstBlood");
		 }
		 $HordeGame::FirstWaveKills[%killer]++;
		 if($HordeGame::FirstWaveKills[%killer] >= 10) {
			CompleteNWChallenge(%killer, "SpeedSlayer");
		 }
      }
   }
}

function DoWaveHighlights() {
   %wave = $HordeGame::CurrentWave - 1;
   if(%wave != 0) {
      MessageAll('MsgHighlights', "\c5Wave "@%wave@" Highlights");
      //
      %highestScore = 0;
      %highestKills = 0;
      %highestScoreCL = -1;
      %highestKills = -1;
      //
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         MessageAll('MsgDumpScore', "\c3"@%cl.namebase@": "@%cl.waveScore@" Points with "@%cl.waveKills@" Kills.");
         //
         if(%cl.waveScore > %highestScore) {
            %highestScore = %cl.waveScore;
            %highestScoreCL = %cl;
         }
         //
         if(%cl.waveKills > %highestKills) {
            %highestKills = %cl.waveKills;
            %highestKillsCL = %cl;
         }
      }
	  //
	  $HordeGame::HighestWaveScoreCount[%highestKillsCL]++;
      if($HordeGame::FirstWaveKills[%highestKillsCL] >= 10) {
         CompleteNWChallenge(%highestKillsCL, "HighScorer");
      }	  
      //
      CenterPrintAll("<just:center>Best Stats For Wave "@%wave@""@
      "\nHighest Scorer: "@%highestScoreCL.namebase@" with "@%highestScore@" Points"@
      "\nMost Kills: "@%highestKillsCL.namebase@" with "@%highestKills@" Kills", 5 ,3);
   }
}

function HordeGame::leaveMissionArea(%game, %playerData, %player) {
   if(%player.getState() $= "Dead")
      return;
   if(%player.client.inKillstreak)    //killstreaks give special perms for not being in mission area
      return;

   %player.client.outOfBounds = true;


   messageClient(%player.client, 'LeaveMissionArea', '\c1You left the mission area. Return or die...~wfx/misc/warning_beep.wav');
   %player.killSched = %player.schedule(5000, "ScriptKill", $DamageType::Admin);
   %player.killMsg = schedule(5000, 0, 'MessageAll', 'msgAdminForce', "\c3COMMAND: "@%player.client.namebase@" has been killed for desertion!!!");

}

function HordeGame::enterMissionArea(%game, %playerData, %player) {
   if(%player.getState() $= "Dead")
      return;

   cancel(%player.killSched);
   cancel(%player.killMsg);

   %player.client.outOfBounds = false;
   messageClient(%player.client, 'EnterMissionArea', '\c1You are back in the mission area.');
}