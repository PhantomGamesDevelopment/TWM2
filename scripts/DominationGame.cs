// DisplayName = Domination

//--- GAME RULES BEGIN ---
// Three Points are on the map
// Secure a point to gain score
// The more points secured, the faster the point gain
//--- GAME RULES END ---

$InvBanList[Domination, "RPG"] = 1;

$DominGame::Rounds = 5;
$DominGame::MaxScore = 350;

datablock StaticShapeData(DominationObjective) {
	className = "StaticShape";
	shapeFile = "flag.dts";

	maxDamage      = 0.5;
	destroyedLevel = 0.5;
	disabledLevel  = 0.3;

	explosion      = HandGrenadeExplosion;
	expDmgRadius = 1.0;
	expDamage = 0.05;
	expImpulse = 200;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag = 'Control Point';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
    needsPower = true;

    collidable = false;
};

function DominationGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function DominationGame::allowsProtectedStatics(%game) {
	return true;
}

function DominationGame::clientMissionDropReady(%game, %client) {
	messageClient(%client, 'MsgMissionDropInfo', '\c0You are in mission %1 (%2).', $MissionDisplayName, $MissionTypeDisplayName, $ServerName );
    messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
	DefaultGame::clientMissionDropReady(%game, %client);
}

function DominationGame::onAIRespawn(%game, %client) {
   //add the default task
	if (!%client.defaultTasksAdded) {
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

function DominationGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function DominationGame::timeLimitReached(%game) {
	logEcho("game over (timelimit)");
    %game.Intermit();
}

function DominationGame::scoreLimitReached(%game) {
	logEcho("game over (scorelimit)");
	%game.gameOver();
	cycleMissions();
}

function DominationGame::gameOver(%game) {
	//call the default
	DefaultGame::gameOver(%game);
    $TWM2::PlayingDomin = 0;
    $FissionEndsGame = 0;
    
    $Ion::StopIon = 0;
    
    $TeamWins[1] = 0;
    $TeamWins[2] = 0;

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

function DominationGame::vehicleDestroyed(%game, %vehicle, %destroyer) {

}

function DominationGame::startMatch(%game) {
    DefaultGame::StartMatch(%game);
    $Domination::TeamScore[1] = 0;
    $Domination::TeamScore[2] = 0;

    $TeamWins[1] = 0;
    $TeamWins[2] = 0;
    
    $Ion::StopIon = 1;
    
    $TWM2::PlayingDomin = 1;
    $DominGame::RoundNumber = 1;
    $FissionEndsGame = 1;
    Game.NumTeams = 2;
    for(%i = 0; %i < ClientGroup.getCount(); %i++) {
       %cl = ClientGroup.getObject(%i);
       CenterPrint(%cl, "<Font:Arial Bold:18><just:center>DOMINATION", 5, 2);
       if(isObject(%cl.player)) {
          %cl.player.setMoveState(true);
          %cl.player.schedule(5000, "setMoveState", false);
       }
    }
    %game.InitialSetup($CurrentMission);
    %game.AddTeamScore();
    
    setSensorGroupCount(7);
}

function DominationGame::Intermit(%game) {
   $DominGame::RoundNumber++;
   $Domination::TeamScore[1] = 0;
   $Domination::TeamScore[2] = 0;
   
   %game.flag1.Controller = 0;
   %game.flag2.Controller = 0;
   %game.flag3.Controller = 0;
   
   %game.flag1.CapCount = 0;
   %game.flag2.CapCount = 0;
   %game.flag3.CapCount = 0;
   
   if($DominGame::RoundNumber > $DominGame::Rounds) {
      %game.schedule(10000, "GameOver");
      CycleMissions();
   }
   else {
      MessageAll('msgInter', "\c5Domination: INTERMISSION, ROUND "@$DominGame::RoundNumber@" BEGINNING");
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         CenterPrint(%cl, "<Font:Arial Bold:18><just:center>DOMINATION \n Round "@$DominGame::RoundNumber@"", 5, 2);
         if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
            %cl.player.setMoveState(true);
            %cl.player.schedule(5000, "setMoveState", false);
            %goto = %game.pickTeamSpawn(%cl.team);
            %cl.player.setPosition(%goto);
         }
      }
   }
}

function DominationGame::InitialSetup(%game, %map) {
   %game.flag1 = new StaticShape() {
      dataBlock = DominationObjective;
      position = $DominationGame::ObjectiveLocation1[%map];
   };
   %game.flag2 = new StaticShape() {
      dataBlock = DominationObjective;
      position = $DominationGame::ObjectiveLocation2[%map];
   };
   %game.flag3 = new StaticShape() {
      dataBlock = DominationObjective;
      position = $DominationGame::ObjectiveLocation3[%map];
   };
   MissionCleanup.add(%game.flag1);
   MissionCleanup.add(%game.flag2);
   MissionCleanup.add(%game.flag3);

   %name[1] = $dFlag[1];
   %name[2] = $dFlag[2];
   %name[3] = $dFlag[3];
   for(%i = 1; %i < 4; %i++) {
      if($Domination::Flag[%i, $CurrentMission] !$= "") {
         %name[%i] = $Domination::Flag[%i, $CurrentMission];
      }
   }

   %game.flag1.Name = %name[1];
   %game.flag2.Name = %name[2];
   %game.flag3.Name = %name[3];
   
   %game.flag1.Controller = 0;
   %game.flag2.Controller = 0;
   %game.flag3.Controller = 0;
   
   %game.flag1.CapCount = 0;
   %game.flag2.CapCount = 0;
   %game.flag3.CapCount = 0;
   
   %game.ScanFlagArea(%game.flag1);
   %game.ScanFlagArea(%game.flag2);
   %game.ScanFlagArea(%game.flag3);
   
   %game.WPLoop(%game.flag1);
   %game.WPLoop(%game.flag2);
   %game.WPLoop(%game.flag3);
}

function DominationGame::ScanFlagArea(%game, %area) {
   InitContainerRadiusSearch(%area.getPosition(), 25, $TypeMasks::PlayerObjectType);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(%potentialTarget.team != %area.Controller) {
         %enemyPlayers++;
      }
      else {
         %friendlyPlayers++;
      }
      //
      %cl = %potentialTarget.client;
      if(%friendlyPlayers >= 1 && %enemyPlayers >= 1) {
         bottomPrint(%potentialTarget.client, "AREA CONTESTED", 1, 2);
      }
      else if(%friendlyPlayers == 0 && %enemyPlayers >= 1) {
         bottomPrint(%potentialTarget.client, "CAPTURING AREA ("@%area.CapCount@" / 30)", 1, 2);
         %game.CaptureArea(%area, %potentialTarget, %potentialTarget.team);
      }
   }
   %game.schedule(500, "ScanFlagArea", %area);
}

function DominationGame::CaptureArea(%game, %area, %player, %team) {
   %area.CapCount++;
   if(%area.CapCount > 30) {
      %area.Controller = %team;
      MessageAll("\c5DOMINATON: Point "@%area.name@" captured by team "@%team@".");
      bottomPrint(%player.client, "Area "@%area.name@" captured, +20 XP", 3, 2);
      GainExperience(%player.client, 20, "Domination Territory Captured ");
      CompleteNWChallenge(%player.client, "ZoneCapture");
      recordAction(%player.client, "AREACAP");
      %area.CapCount = 0;
      return;
   }
}

function DominationGame::BuildFlagControlString(%game, %team) {
   %string = "";
   if(%game.flag1.Controller == %team) {
      %string = ""@%string@"[Alpha]";
   }
   if(%game.flag2.Controller == %team) {
      %string = ""@%string@" [Bravo]";
   }
   if(%game.flag3.Controller == %team) {
      %string = ""@%string@" [Charlie]";
   }
   //
   //they have all 3!
   if(%string $= "[Alpha] [Bravo] [Charlie]") {
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl.team == %team) {
            messageClient(%cl, 'msgDominated', "\c5TEAM: All Positions Locked Down, Hold Those Positions!!!");
            CompleteNWChallenge(%cl, "ABC");
         }
         else {
            messageClient(%cl, 'msgDominated', "\c5TEAM: We Are Being DOMINATED, Take Those Positions!!!");
         }
      }
   }
   return %string;
}

function DominationGame::AddTeamScore(%game) {
   if(%game.flag1.Controller != 0)
      $Domination::TeamScore[%game.flag1.Controller]++;
   if(%game.flag2.Controller != 0)
      $Domination::TeamScore[%game.flag2.Controller]++;
   if(%game.flag3.Controller != 0)
      $Domination::TeamScore[%game.flag3.Controller]++;
      
   MessageAll('MsgSPCurrentObjective1', "", "TEAM 1 ["@$TeamWins[1]@"/5]: ("@%game.BuildFlagControlString(1)@"), "@$Domination::TeamScore[1]@" / "@$DominGame::MaxScore@"");
   MessageAll('MsgSPCurrentObjective2', "", "TEAM 2 ["@$TeamWins[2]@"/5]: ("@%game.BuildFlagControlString(2)@"), "@$Domination::TeamScore[2]@" / "@$DominGame::MaxScore@"");
     
   %game.schedule(3000, "AddTeamScore");
   %game.CheckIntermit();
}

function DominationGame::CheckIntermit(%game) {
   if($Domination::TeamScore[1] >= $DominGame::MaxScore) {
      MessageTeam(1, 'MsgWin', "DOMINATION: Excellent Work, We Have Dominated!!!");
      MessageTeam(2, 'MsgWin', "DOMINATION: We have been Dominated, Try Harder Next Time");
      $teamScore[1] += 10000;
      $TeamWins[1]++;
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl.team == 1) {
            recordAction(%cl, "DOMWIN");
            switch($TeamWins[1]) {
               case 1:
                  CompleteNWChallenge(%cl, "MatchSet");
               case 3:
                  CompleteNWChallenge(%cl, "3For5");
               case 5:
                  CompleteNWChallenge(%cl, "Undefeatable");
            }
         }
      }
      %game.Intermit();
   }
   else if($Domination::TeamScore[2] >= $DominGame::MaxScore) {
      MessageTeam(1, 'MsgWin', "DOMINATION: We have been Dominated, Try Harder Next Time");
      MessageTeam(2, 'MsgWin', "DOMINATION: Excellent Work, We Have Dominated!!!");
      $teamScore[2] += 10000;
      $TeamWins[2]++;
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl.team == 2) {
            recordAction(%cl, "DOMWIN");
            switch($TeamWins[2]) {
               case 1:
                  CompleteNWChallenge(%cl, "MatchSet");
               case 3:
                  CompleteNWChallenge(%cl, "3For5");
               case 5:
                  CompleteNWChallenge(%cl, "Undefeatable");
            }
         }
      }
      %game.Intermit();
   }
   else if($Domination::TeamScore[1] == $DominGame::MaxScore && $Domination::TeamScore[2] == $DominGame::MaxScore) {
      MessageTeam(1, 'MsgWin', "DOMINATION: It's a DRAW!!!");
      MessageTeam(2, 'MsgWin', "DOMINATION: It's a DRAW!!!");
      %game.Intermit();
   }
}

function DominationGame::pickTeamSpawn(%game, %team) {
   if(%team == 1) {
      %pos = vectorAdd($DominationGame::SpawnLocation1[$CurrentMission], TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
      %pos = vectorAdd(%pos,"0 0 5");
      return %pos;
   }
   else if(%team == 2) {
      %pos = vectorAdd($DominationGame::SpawnLocation2[$CurrentMission], TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
      %pos = vectorAdd(%pos,"0 0 5");
      return %pos;
   }
}

function DominationGame::WPLoop(%game, %area) {
   if(!isObject(%area)) {
      return; //no console spamz 4 you
   }
   if(isObject(%area.wp)) {
      %area.wp.delete();
   }
   %area.wp = new WayPoint() {
       position = %area.getPosition();
       dataBlock = "WayPointMarker";
       team = %area.Controller;
       name = ""@%area.name@"";
   };
   MissionCleanup.add(%area.wp);
   %game.schedule(500, "WPLoop", %area);
}

//MissionLists
$DominationGame::SpawnLocation1["EngelamHimmel"] = "126.7 14.7 181";
$DominationGame::SpawnLocation2["EngelamHimmel"] = "-282 16 181";
$DominationGame::ObjectiveLocation1["EngelamHimmel"] = "-176.82 32.36 180.016";
$DominationGame::ObjectiveLocation2["EngelamHimmel"] = "-100 0 251";
$DominationGame::ObjectiveLocation3["EngelamHimmel"] = "-22.25 32 180.016";

$DominationGame::SpawnLocation1["SideSwipe"] = "-83 -270 354";
$DominationGame::SpawnLocation2["SideSwipe"] = "-82.3 -114 354";
$DominationGame::ObjectiveLocation1["SideSwipe"] = "-72 -216.75 354";
$DominationGame::ObjectiveLocation2["SideSwipe"] = "-83.6 -196.2 397";
$DominationGame::ObjectiveLocation3["SideSwipe"] = "-92.2 -163.13 354";

$DominationGame::SpawnLocation1["HarbingerTower"] = "810.5 -401.1 102";
$DominationGame::SpawnLocation2["HarbingerTower"] = "814.2 -428.7 198";
$DominationGame::ObjectiveLocation1["HarbingerTower"] = "799.4 -402.1 113";
$DominationGame::ObjectiveLocation2["HarbingerTower"] = "820.7 -413.5 155";
$DominationGame::ObjectiveLocation3["HarbingerTower"] = "815 -401.7 180";

$DominationGame::SpawnLocation1["Skyline"] = "-59.0467 -419.333 791.448";
$DominationGame::SpawnLocation2["Skyline"] = "-60.0561 -256.818 814.83";
$DominationGame::ObjectiveLocation1["Skyline"] = "-59.73 -280.06 773.08";
$DominationGame::ObjectiveLocation2["Skyline"] = "-61.15 -366.13 740.925";
$DominationGame::ObjectiveLocation3["Skyline"] = "-59.26 -394.66 773.08";

$DominationGame::SpawnLocation1["GeometricOrder"] = "-172.325 -396.557 159.9";
$DominationGame::SpawnLocation2["GeometricOrder"] = "-10.7 -573.29 159.9";
$DominationGame::ObjectiveLocation1["GeometricOrder"] = "4.23 -600.6 159.9";
$DominationGame::ObjectiveLocation2["GeometricOrder"] = "-79.9 -487.06 159.9";
$DominationGame::ObjectiveLocation3["GeometricOrder"] = "-185.68 -559.78 160.45";





























function GenerateDominationChallengeMenu(%client, %tag, %index) {
   if(%client.CheckNWChallengeCompletion("ZoneCapture")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Zone Conquerer - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Zone Conquerer - Capture an Area.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("ABC")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Alpha Bravo Charlie - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Alpha Bravo Charlie - Secure All Three Areas at one Time.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("MatchSet")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Match Set - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Match Set - Win a Round Of Domination.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("3For5")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Three For Five - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Three For Five - Win 3 Rounds Of Domination.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Undefeatable")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Undefeatable - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Undefeatable - Go Undefeated in a full game of Domination.");
      %index++;
   }
   //
   return %index;
}
