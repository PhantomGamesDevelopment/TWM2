// DisplayName = Construction

//--- GAME RULES BEGIN ---
// Build
//--- GAME RULES END ---

// spam fix
function ConstructionGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function ConstructionGame::allowsProtectedStatics(%game) {
	return true;
}

function ConstructionGame::clientMissionDropReady(%game, %client) {
	messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
    messageClient(%client, 'MsgSPCurrentObjective1' ,"", "Welcome to TWM2!");
    messageClient(%client, 'MsgSPCurrentObjective2' ,"", "Phantom139, DoL, Signal360");
	messageClient(%client, 'MsgMissionDropInfo', '\c0You are in mission %1 (%2).', $MissionDisplayName, $MissionTypeDisplayName, $ServerName );
	DefaultGame::clientMissionDropReady(%game, %client);
}

function ConstructionGame::onAIRespawn(%game, %client)
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

function ConstructionGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function ConstructionGame::timeLimitReached(%game) {
	logEcho("game over (timelimit)");
	%game.gameOver();
	cycleMissions();
}

function ConstructionGame::scoreLimitReached(%game) {
	logEcho("game over (scorelimit)");
	%game.gameOver();
	cycleMissions();
}

function ConstructionGame::gameOver(%game) {
	//call the default
	DefaultGame::gameOver(%game);

	//send the winner message
	%winner = "";
	if ($teamScore[1] > $teamScore[2])
		%winner = %game.getTeamName(1);
	else if ($teamScore[2] > $teamScore[1])
		%winner = %game.getTeamName(2);

	if (%winner $= 'Storm')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.stowins.wav" );
	else if (%winner $= 'Inferno')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.infwins.wav" );
	else if (%winner $= 'Starwolf')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.swwin.wav" );
	else if (%winner $= 'Blood Eagle')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.bewin.wav" );
	else if (%winner $= 'Diamond Sword')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.dswin.wav" );
	else if (%winner $= 'Phoenix')
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.pxwin.wav" );
	else
		messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.gameover.wav" );

	messageAll('MsgClearObjHud', "");
	for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		%client = ClientGroup.getObject(%i);
		%game.resetScore(%client);
	}
	for(%j = 1; %j <= %game.numTeams; %j++)
		$TeamScore[%j] = 0;
}

function ConstructionGame::vehicleDestroyed(%game, %vehicle, %destroyer) {
}


function ConstructionGame::ToggleModifiers(%game, %modifier, %toggleTo) {
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

function ConstructionGame::CheckModifier(%game, %mod) {
   return $HellJump::Modifier[""@%mod@""];
}

function ConstructionGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation) {
   DefaultGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation);
   DoTWM2MissionChecks(%clVictim);
}


