// DisplayName = Sabotage

//--- GAME RULES BEGIN ---
// Secure The Bomb
// Destroy The Enemy Objective With The Bomb
// Protect Your Objective From The Bomb
//--- GAME RULES END ---

$SaboGame::Rounds = 5;
$Sabotage::ArmTime = 3;
$Sabotage::DefuseTime = 10;
$Sabotage::Fuse = 25;

datablock StaticShapeData(SabotageObjective) {
	className = "StaticShape";
	shapeFile = "stackable2m.dts";

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
	targetNameTag = '[Sabotage] Objective';
 
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
    needsPower = true;
};

datablock ItemData(SabotageBomb) {
   catagory = "Objectives";
   shapefile = "stackable2s.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 3;
   pickUpName = "a Bomb";
   computeCRC = true;

   lightType = "PulsingLight";
   lightColor = "0.5 0.5 0.5 1.0";
   lightTime = "1000";
   lightRadius = "3";

   isInvincible = true;
   cmdCategory = "Objectives";
   cmdIcon = CMDFlagIcon;
   cmdMiniIconName = "commander/MiniIcons/com_flag_grey";
   targetTypeTag = 'Bomb';
};

function SabotageBomb::onCollision(%data,%obj,%col) {
   if (%col.getDataBlock().className $= Armor && !%col.client.isJailed) {
      if (%col.isMounted())
         return;

      // a player hit the flag
      Game.playerTouchBomb(%col, %obj);
   }
}

function SabotageGame::playerTouchBomb(%game, %player, %bomb) {
   if(%player.getState() $= "Dead") {
      return;
   }
   if(%bomb.isArmed) {

   }
   else {
      cancel(%bomb.resetTime);
      messageClient(%player.client, 'MsgYouHasBomb', "\c5SABOTAGE: You have the BOMB, Destroy The Objective");
      %bomb.Carrier = %player;
      %bomb.hide(true);
      %bomb.startFade(0, 0, false);
      if(%player.team == 1) {
         %enemyTeam = 2;
      }
      else {
         %enemyTeam = 1;
      }
      %game.ObjectiveScan(%player, %enemyTeam);
   }
}

function SabotageGame::CheckForDisarm(%game, %bomb) {
   InitContainerRadiusSearch(%bomb.getPosition(), 25, $TypeMasks::PlayerObjectType);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(%potentialTarget.team != %bomb.ArmTeam) {
         if(%bomb.BeingDisarmed) {
            return;
         }
         %bomb.BeingDisarmed = 1;
         %game.StartDisarm(%potentialTarget, %bomb, 0);
         return;
      }
   }
   %game.schedule(500, "CheckForDisarm", %bomb);
}

function SabotageGame::ObjectiveScan(%game, %carrier, %enemyTeam) {
   if(!isObject(%carrier) || %carrier.getState() $= "Dead") {
      return;
   }
   %EnemyObj = %game.TeamObjective[%enemyTeam];
   if(vectorDist(%carrier.getPosition(), %EnemyObj.getPosition()) <= 5) {
      %game.ArmBomb(%carrier, %game.bomb, %enemyTeam, 0);
      return;
   }
   %game.schedule(500, "ObjectiveScan", %carrier, %enemyTeam);
}

function SabotageGame::ArmBomb(%game, %carrier, %bomb, %ETeam, %ct) {
   if(!isObject(%carrier) || %carrier.getState() $= "Dead") {
      return;
   }
   %EnemyObj = %game.TeamObjective[%ETeam];
   if(vectorDist(%carrier.getPosition(), %EnemyObj.getPosition()) > 12) {
      %game.schedule(500, "ObjectiveScan", %carrier, %ETeam);
   }
   else {
      BottomPrint(%carrier.client, "ARMING BOMB "@MFloor(%ct)@" / "@$Sabotage::ArmTime*2@"", 1, 2);
      //Still In Range, Keep Armin
      %ct++;
      //7 Seconds To Arm!
      if(%ct >= $Sabotage::ArmTime*2) {
         %bomb.armTeam = %carrier.team;
         recordAction(%carrier, "BOMBARM");
         %game.BombDropped(%bomb, %Carrier); //drop the bomb, cause it's armedz
         MessageAll('MsgBombArmed', "\c5SABOTAGE: The Bomb Has Been Planted!!!");
         CompleteNWChallenge(%Carrier, "BombPlanted");
         %game.BombBegin(%bomb, %ETeam, 0);
      }
      else {
         %game.schedule(500, "ArmBomb", %carrier, %bomb, %ETeam, %ct);
      }
   }
}

function SabotageGame::StartDisarm(%game, %player, %bomb, %ct) {
   if(%Player.getState() $= "dead") {
      %bomb.BeingDisarmed = 0;
      %game.CheckForDisarm(%bomb);
      return;
   }
   if(!%bomb.BeingDisarmed) {
      %game.CheckForDisarm(%bomb);
      return;
   }
   if(vectorDist(%player.getPosition(), %bomb.getPosition()) <= 7) {
      %ct++;
      BottomPrint(%player.client, "DISARMING BOMB "@MFloor(2)@" / "@$Sabotage::DefuseTime*2@"", 1, 2);
      if(%ct >= $Sabotage::DefuseTime*2) {
         cancel(%bomb.DetSchedule);
         %bomb.isArmed = 0;
         %bomb.BeingDisarmed = 0;
         %game.playerTouchBomb(%player, %bomb);
         messageAll('MsgSPCurrentObjective2', "", "Disarmed");
         recordAction(%player.client, "BOMBDIS");
         MessageAll('MsgBombArmed', "\c5SABOTAGE: The Bomb Has Been Disarmed!!!");
         CompleteNWChallenge(%player.client, "BombDisarmed");
         return;
      }
      else {
         %game.schedule(500, "StartDisarm", %player, %bomb, %ct);
      }
   }
   else {
      %game.CheckForDisarm(%bomb);
      %bomb.BeingDisarmed = 0;
   }
}

function SabotageGame::BombBegin(%game, %bomb, %ETeam, %ct) {
   %bomb.isArmed = 1;
   %game.CheckForDisarm(%bomb);
   %ct++;
   messageAll('MsgSPCurrentObjective2', "", "Fuse Time: "@MFloor(%ct/2)@"/"@$Sabotage::Fuse@"");
   if(%ct > $Sabotage::Fuse*2) {
      %game.BombExplode(%bomb, %ETeam);
   }
   else {
      %bomb.DetSchedule = %game.schedule(500, "BombBegin", %bomb, %ETeam, %ct);
   }
}

function SabotageGame::BombExplode(%game, %bomb, %ETeam) {
   $teamScore[%bomb.armTeam] += 10000; //HA!
   $TeamWins[%bomb.armTeam]++;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %bomb.armTeam) {
         recordAction(%cl, "SABWIN");
         switch($TeamWins[%bomb.armTeam]) {
            case 1:
               CompleteNWChallenge(%cl, "BombDetonated");
            case 3:
               CompleteNWChallenge(%cl, "3For5Sabo");
            case 5:
               CompleteNWChallenge(%cl, "BaseDestroyer");
         }
      }
   }
   //Boom :)
   ServerPlay3D("SatchelChargeExplosionSound", %bomb.getPosition());
   %c4 = new Item() {
      datablock = C4Deployed;
      position = %bomb.getPosition();
      scale = ".1 .1 .1";
   };
   MissionCleanup.add(%c4);
   schedule(770, 0, "C4GoBoom", %c4);
   //
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(isObject(%cl.player)) {
//         %cl.player.clearInventory();  //auto UE
         if(%cl.team != %bomb.armTeam) {
            %cl.player.setDamageFlash(100);
         }
      }
      if(%cl.team != %bomb.armTeam) {
         messageClient(%cl, 'MsgYouHasBomb', "\c5SABOTAGE: We have been defeated... try harder next time...");
      }
      else {
         messageClient(%cl, 'MsgYouHasBomb', "\c5SABOTAGE: Mission acomplished, great work soldiers. (+250XP)");
         %cl.xp += 250;
         UpdateClientRank(%cl);
      }
   }
   %game.Intermit();
}

function SabotageGame::ResetBomb(%game, %bomb) {
   MessageAll('msgWhoops', "\c5SABOTAGE: Bomb Reset.");
   %bomb.setPosition($SabotageGame::BombLocation[$CurrentMission]);
}

function SabotageGame::BombDropped(%game, %bomb, %Carrier) {
   if(!%bomb.IsArmed) {
      %bomb.resetTime = %game.schedule(45000,"ResetBomb", %bomb);
   }
   %bomb.setPosition(%Carrier.getPosition()); //I think the game does this
   %bomb.Carrier = 0;                         //But this is just to be safe
   %bomb.hide(false);
}

function SabotageGame::UpdateBombStatus(%game, %bomb) {
   if(%bomb.Carrier == 0 && !%bomb.isArmed) {
      %wp = new WayPoint() {
          position = %bomb.getWorldBoxCenter();
          dataBlock = "WayPointMarker";
          team = 0;
          name = "BOMB";
      };
      messageAll('MsgSPCurrentObjective1', "", "Bomb: Neutral");
      MissionCleanup.add(%wp);
      %wp.schedule(2999,delete);
   }
   else if(%bomb.isArmed) {
      %wp = new WayPoint() {
          position = %bomb.getWorldBoxCenter();
          dataBlock = "WayPointMarker";
          team = %bomb.armTeam;
          name = "ARMED BOMB";
      };
      messageAll('MsgSPCurrentObjective1', "", "Bomb: Armed");
      MissionCleanup.add(%wp);
      %wp.schedule(2999,delete);
   }
   else {
      if(!%bomb.Carrier.client.IsActivePerk("Bomb Shadower")) {
         %wp = new WayPoint() {
             position = %bomb.Carrier.getWorldBoxCenter();
             dataBlock = "WayPointMarker";
             team = %bomb.Carrier.Team;
             name = ""@%bomb.Carrier.client.namebase@" - BOMB";
         };
         messageAll('MsgSPCurrentObjective1', "", "Bomb: "@%bomb.carrier.client.namebase@"");
         MissionCleanup.add(%wp);
         %wp.schedule(2999,delete);
      }
      else {
         messageAll('MsgSPCurrentObjective1', "", "Bomb: Shadowed By Carrier");
      }
   }
   %game.schedule(3000, "UpdateBombStatus", %bomb);
}

function SabotageGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function SabotageGame::allowsProtectedStatics(%game) {
	return true;
}

function SabotageGame::clientMissionDropReady(%game, %client) {
	messageClient(%client, 'MsgMissionDropInfo', '\c0You are in mission %1 (%2).', $MissionDisplayName, $MissionTypeDisplayName, $ServerName );
    messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
	DefaultGame::clientMissionDropReady(%game, %client);
}

function SabotageGame::onAIRespawn(%game, %client) {
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

function SabotageGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function SabotageGame::timeLimitReached(%game) {
	logEcho("game over (timelimit)");
	%game.gameOver();
	cycleMissions();
}

function SabotageGame::scoreLimitReached(%game) {
	logEcho("game over (scorelimit)");
	%game.gameOver();
	cycleMissions();
}

function SabotageGame::gameOver(%game) {
	//call the default
	DefaultGame::gameOver(%game);
    $TWM2::PlayingSabo = 0;
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

function SabotageGame::vehicleDestroyed(%game, %vehicle, %destroyer) {

}

function SabotageGame::startMatch(%game) {
    DefaultGame::StartMatch(%game);
    $TWM2::PlayingSabo = 1;
    
    $Ion::StopIon = 1;
    
    $FissionEndsGame = 1;
    
    $TeamWins[1] = 0;
    $TeamWins[2] = 0;
    
    $SaboGame::RoundNumber = 1;
    Game.NumTeams = 2;
    for(%i = 0; %i < ClientGroup.getCount(); %i++) {
       %cl = ClientGroup.getObject(%i);
       CenterPrint(%cl, "<Font:Arial Bold:18><just:center>SABOTAGE", 5, 2);
       if(isObject(%cl.player)) {
          %cl.player.setMoveState(true);
          %cl.player.schedule(5000, "setMoveState", false);
       }
    }
    %game.InitialSetup($CurrentMission);
    setSensorGroupCount(7);
}

function SabotageGame::Intermit(%game) {
   $SaboGame::RoundNumber++;
   if($SaboGame::RoundNumber > $SaboGame::Rounds) {
      MessageAll('msgInter', "\c5SABOTAGE: GAME OVER, CHANGING MAP...");
      %game.schedule(10000, "GameOver");
      CycleMissions();
      return;
   }
   else {
      %game.bomb.IsArmed = 0;
      %game.bomb.carrier = 0;
      %game.ResetBomb(%game.Bomb);
      MessageAll('msgInter', "\c5SABOTAGE: INTERMISSION, ROUND "@$SaboGame::RoundNumber@" BEGINNING");
   }
}

function SabotageGame::InitialSetup(%game, %map) {
   //Team 1 Objective
   %game.TeamObjective[1] = new StaticShape() {
       datablock = SabotageObjective;
       position = $SabotageGame::ObjectiveLocation1[%map];
   };
   MissionCleanup.add(%game.TeamObjective[1]);
   %game.TeamObjective[1].wp = new WayPoint() {
      position = $SabotageGame::ObjectiveLocation1[%map];
      dataBlock = "WayPointMarker";
      team = 1;
      name = "TEAM 1 OBJECTIVE";
   };
   MissionCleanup.add(%game.TeamObjective[1].wp);
   %game.WPLoop(%game.TeamObjective[1].wp, 1);
   %game.TeamObjective[1].Invincible = 1; //HA! No Shootin / C4 For You

   //Team 2 Objective
   %game.TeamObjective[2] = new StaticShape() {
       datablock = SabotageObjective;
       position = $SabotageGame::ObjectiveLocation2[%map];
   };
   MissionCleanup.add(%game.TeamObjective[2]);
   %game.TeamObjective[2].wp = new WayPoint() {
      position = $SabotageGame::ObjectiveLocation2[%map];
      dataBlock = "WayPointMarker";
      team = 2;
      name = "TEAM 2 OBJECTIVE";
   };
   MissionCleanup.add(%game.TeamObjective[2].wp);
   %game.WPLoop(%game.TeamObjective[2].wp, 2);
   %game.TeamObjective[2].Invincible = 1; //HA! No Shootin / C4 For You

   %game.Bomb = new Item() {
       datablock = SabotageBomb;
       position = $SabotageGame::BombLocation[%map];
   };
   MissionCleanup.add(%game.Bomb);
   %game.UpdateBombStatus(%game.Bomb);
   messageAll('MsgSPCurrentObjective1', "", "Bomb: Neutral");
   messageAll('MsgSPCurrentObjective2', "", "-----");
}

function SabotageGame::pickTeamSpawn(%game, %team) {
   if(%team == 1) {
      if(!isObject(%game.TeamObjective[1])) {
      //stops the pre-game fall
      %pos = vectorAdd($SabotageGame::ObjectiveLocation1[$CurrentMission], TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
      %pos = vectorAdd(%pos,"0 0 5");
      }
      else {
      %pos = vectorAdd(%game.TeamObjective[1].getPosition(), TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
      %pos = vectorAdd(%pos,"0 0 4");
      }
      return %pos;
   }
   else if(%team == 2) {
      if(!isObject(%game.TeamObjective[2])) {
      //stops the pre-game fall
      %pos = vectorAdd($SabotageGame::ObjectiveLocation2[$CurrentMission], TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
      %pos = vectorAdd(%pos,"0 0 5");
      }
      else {
      %pos = vectorAdd(%game.TeamObjective[2].getPosition(), TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
      %pos = vectorAdd(%pos,"0 0 4");
      }
      return %pos;
   }
}

function SabotageGame::WPLoop(%game, %wp, %team) {
   %wp.delete();
   if(%team == 2) {
      %game.TeamObjective[2].wp = new WayPoint() {
         position = $SabotageGame::ObjectiveLocation2[$CurrentMission];
         dataBlock = "WayPointMarker";
         team = 2;
         name = "TEAM 2 OBJECTIVE";
      };
      MissionCleanup.add(%game.TeamObjective[2].wp);
      %game.schedule(5000, "WPLoop", %game.TeamObjective[2].wp, 2);
   }
   else {
      %game.TeamObjective[1].wp = new WayPoint() {
         position = $SabotageGame::ObjectiveLocation1[$CurrentMission];
         dataBlock = "WayPointMarker";
         team = 1;
         name = "TEAM 1 OBJECTIVE";
      };
      MissionCleanup.add(%game.TeamObjective[1].wp);
      %game.schedule(5000, "WPLoop", %game.TeamObjective[1].wp, 1);
   }
}

//MissionLists
$SabotageGame::ObjectiveLocation1["Strikers2"] = "1.4 148.5 14.2";
$SabotageGame::ObjectiveLocation2["Strikers2"] = "147.7 7.9 14.4";
$SabotageGame::BombLocation["Strikers2"] = "54.8 60.1 28.2";

$SabotageGame::ObjectiveLocation1["Oasis2"] = "-106.6 53.8 134";
$SabotageGame::ObjectiveLocation2["Oasis2"] = "-107.8 -125.4 133";
$SabotageGame::BombLocation["Oasis2"] = "-215.2 -44.7 109.113";

$SabotageGame::ObjectiveLocation1["MyrkWood2"] = "-297.849 -232.002 87.1634";
$SabotageGame::ObjectiveLocation2["MyrkWood2"] = "-162.307 113.314 83.2362";
$SabotageGame::BombLocation["MyrkWood2"] = "-274 -85 74.7";

$SabotageGame::ObjectiveLocation1["DerGott"] = "-2.2 -3.1 157.149";
$SabotageGame::ObjectiveLocation2["DerGott"] = "-202.631 -210.103 157.493";
$SabotageGame::BombLocation["DerGott"] = "-103.026 -95.7017 155.149";

$SabotageGame::ObjectiveLocation1["SideSwipe"] = "-83 -270 355";
$SabotageGame::ObjectiveLocation2["SideSwipe"] = "-82.3 -114 355";
$SabotageGame::BombLocation["SideSwipe"] = "-83.6 -196.2 397";

$SabotageGame::ObjectiveLocation1["HarbingerTower"] = "810.5 -401.1 102";
$SabotageGame::ObjectiveLocation2["HarbingerTower"] = "814.2 -428.7 198";
$SabotageGame::BombLocation["HarbingerTower"] = "799.4 -402.1 116";

$SabotageGame::ObjectiveLocation1["Skyline"] = "-58.8556 -420.953 750.438";
$SabotageGame::ObjectiveLocation2["Skyline"] = "-59.5177 -259.064 773.837";
$SabotageGame::BombLocation["Skyline"] = "-7.6 -339.7 774";

$SabotageGame::ObjectiveLocation1["GeometricOrder"] = "-172.325 -396.557 159.9";
$SabotageGame::ObjectiveLocation2["GeometricOrder"] = "-10.7 -573.29 159.9";
$SabotageGame::BombLocation["GeometricOrder"] = "-79.9 -487.06 165.9";

























function GenerateSabotageChallengeMenu(%client, %tag, %index) {
   if(%client.CheckNWChallengeCompletion("BombDisarmed")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Disarmed - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Disarmed - Disarm an enemy bomb.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("BombPlanted")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Planted - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Planted - Arm the bomb at the objective.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("BombDetonated")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Detonated - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Detonated - Win a Round Of Sabotage.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("3For5Sabo")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Three For Five - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Three For Five - Win 3 Rounds Of Sabotage in a match.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("BaseDestroyer")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Base Destroyer - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Base Destroyer - Go Undefeated in a full game of Sabotage.");
      %index++;
   }
   //
   return %index;
}
