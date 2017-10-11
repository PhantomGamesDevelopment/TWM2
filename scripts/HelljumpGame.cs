// DisplayName = Helljump

//--- GAME RULES BEGIN ---
// Players are dropped into combat zones where they must survive
// Endless waves of zombies. Reinforcements after 5 strikes, 5 strikes
// make 1 group. 5 Groups make 1 wave. Bonus strike after a wave is
// complete. Ammo is scarse, so be careful.
//--- GAME RULES END ---

//HELLJUMP GAME
//COPYRIGHT 2009-2010, PHANTOM GAMES DEVELOPMENT
//ALL RIGHTS RESERVED

//The Code.. to end all :p

//Zombie Spawn Area (ignore Z-Coord)
$HellJump::ZSpawnZoneCt["FlatlandBigH"] = 1;
$HellJump::ZSpawnZone["FlatlandBigH", 1] = "3000 0 100";

$HellJump::ZSpawnZoneCt["slapmydashH"] = 1;
$HellJump::ZSpawnZone["slapmydashH", 1] = "3000 -700 100";

$HellJump::ZSpawnZoneCt["FrozenNight"] = 4;
$HellJump::ZSpawnZone["FrozenNight", 1] = "-2125 2314 130";
$HellJump::ZSpawnZone["FrozenNight", 2] = "-2329 2121 150";
$HellJump::ZSpawnZone["FrozenNight", 3] = "-2246 2431 80";
$HellJump::ZSpawnZone["FrozenNight", 4] = "-2369 2490 145";

$HellJump::ZSpawnZoneCt["HelljumpIsland"] = 1;
$HellJump::ZSpawnZone["HelljumpIsland", 1] = "1290 -1225 275";

//Define Initial Spawn Zone
$HellJump::SpawnGraph["FlatlandBigH"] = "52 0 101";
$HellJump::SpawnGraph["slapmydashH"] = "-64.7988 -267.13 131.439";
$HellJump::SpawnGraph["FrozenNight"] = "30 26 80";
$HellJump::SpawnGraph["HelljumpIsland"] = "-791.454 499.031 540.0";
//
//Define drop points (dont worry about Z, the code always uses the transport's current height)
$HellJump::DropPos["FlatlandBigH"] = "3000 0 1000";
$HellJump::DropPos["HelljumpIsland"] = "1080 -1210 1000";
$HellJump::DropPos["slapmydashH"] = "3000 -700 1000";
$HellJump::DropPos["FrozenNight"] = "-2274 2288 1000";
//


function HelljumpGame::pickPlayerSpawn(%game, %client, %respawn) {
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
   %loc = $HellJump::SpawnGraph[$CurrentMission];
   %position = vectorAdd(%loc,VectorAdd(TWM2Lib_MainControl("getRandomPosition", 20 TAB 1), "0 0 10"));
   return %position;//return %game.pickTeamSpawn(%client.team);
}

function HelljumpGame::AIInit(%game) {
	//call the default AIInit() function
	AIInit();
}

function HelljumpGame::allowsProtectedStatics(%game) {
	return true;
}

function HelljumpGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation) {
   Parent::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLocation);
   if(%clVictim !$= "") { //actual player died
      if(%game.CheckModifier("Titan") == 1) {
         //end the bonus round!
         %game.KillAllZombies();
         //don't continue on, if the player is going solo, we let him continue
         BottomPrintAll("Bonus Strike Over...");
         return;
      }
      %clVictim.CantRespawn = 1; //gotta wait until next round / Wave
       CenterPrint(%clVictim, "<color:FF0000>You are out until the next Group.",2,3);
//       forceObserver( %game, %clVictim, "AdminForce" ); //not needed
       Echo("Helljump: "@%clVictim.namebase@" has been killed.");
       %game.CheckForLivingPlayers();
   }
}

function HelljumpGame::CheckForLivingPlayers(%game) {
   if($Helljump::Preparing) {
      return;
   }
   $HellJump::LivingCount = 0;
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
      $HellJump::LivingCount++;
      }
   }
   //Display:
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      messageClient(%cl, 'MsgSPCurrentObjective2', "", "[Players: "@$HellJump::LivingCount@"][SCORE: Team: "@$HellJump::Score["Team"]@" You: "@$HellJump::Score[%cl]@"]");
   }
   if($HellJump::LivingCount <= 0) {
   //Everyone is Dead, or nobody is in the server, game over
      %game.gameOver();
      reloadCurrentMission();
      //cycleMissions();
   }
}

function reloadCurrentMission() {
   loadMission( $CurrentMission, $CurrentMissionType );
}

function HelljumpGame::KillAllZombies(%game) {
   Echo("Helljump: Cleaning Zombies");
   %count = MissionCleanup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %obj = MissionCleanup.getObject(%i);
      if(isObject(%obj)) {
         if(%obj.iszombie) {
            %obj.scriptkill($DamageType::admin);  //They all went on crack and died XD
         }
      }
   }
}

function HelljumpGame::AllowAllRespawn(%game) {
   Echo("Helljump: Players Can now respawn.");
   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      %cl.CantRespawn = 0;
      CenterPrint(%cl, "<color:FF0000>You can now respawn, but hurry, you have 15 seconds.",2,3);
   }
   %game.schedule(15000, "ExpireRespawn");
   messageAll('MsgSPCurrentObjective2' ,"", "You Can Now Spawn");
}

function HelljumpGame::ExpireRespawn(%game) {
   Echo("Helljump: Players Can no longer respawn.");
   %count = ClientGroup.getCount();
   for (%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject( %i );
      %cl.CantRespawn = 0;
      CenterPrint(%cl, "<color:FF0000>The Respawn Period Has Expired.",2,3);
   }
   %game.CheckForLivingPlayers();
}

function HelljumpGame::spawnPlayer( %game, %client, %respawn ) {
    if(%client.CantRespawn) {
       CenterPrint(%client, "<color:FF0000>You are out until the next Group.",2,3);
//       forceObserver( %game, %client, "AdminForce" ); //Eh, for now
       return;
    }
	Parent::spawnPlayer( %game, %client, %respawn );
    %game.CheckForLivingPlayers();
    //board an availiable transport
    %game.checkTrans(%client);
}

function HelljumpGame::clientJoinTeam( %game, %client, %team, %respawn )
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

function HelljumpGame::assignClientTeam(%game, %client, %respawn ) {

   %client.team = 1;
   %client.lastTeam = 1;

   // Assign the team skin:
   if ( %client.isAIControlled() ) {
      if ( %leastTeam & 1 ) {
	     %client.skin = addTaggedString( "basebot" );
	     setTargetSkin( %client.target, 'basebot' );
      }
   }
   else
      setTargetSkin( %client.target, %game.getTeamSkin(%client.team) );
      //setTargetSkin( %client.target, %client.skin );

   // might as well standardize the messages
   //messageAllExcept( %client, -1, 'MsgClientJoinTeam', '\c1%1 joined %2.', %client.name, $teamName[%leastTeam], %client, %leastTeam );
   //messageClient( %client, 'MsgClientJoinTeam', '\c1You joined the %2 team.', $client.name, $teamName[%client.team], %client, %client.team );
//   messageAllExcept( %client, -1, 'MsgClientJoinTeam', '\c1%1 joined %2.', %client.name, %game.getTeamName(%client.team), %client, %client.team );
//   messageClient( %client, 'MsgClientJoinTeam', '\c1You joined the %2 team.', %client.name, %game.getTeamName(%client.team), %client, %client.team );

   updateCanListenState( %client );

   logEcho(%client.nameBase@" (cl "@%client@") joined team "@%client.team);
}

function HelljumpGame::clientChangeTeam(%game, %client, %team, %fromObs) {
   if(%client.CantRespawn) {
      if(!%fromObs) {    //:D they tried to change teams
         CenterPrint(%client, "<color:FF0000>You are out until the next Group. \n Although that was a good try :D",2,3);
          return;
      }
      else {
         CenterPrint(%client, "<color:FF0000>You are out until the next Group.",2,3);
         return;
      }
   }
   %game.removeFromTeamRankArray(%client);

   %pl = %client.player;
   if(isObject(%pl))
   {
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

function HelljumpGame::clientMissionDropReady(%game, %client) {
   %count = ClientGroup.getCount();
   if($MatchStarted && $CountdownStarted && %count == 1) { //Game started, first player is in
      messageClient(%client, 'MsgClient', "\c5Welcome To Helljump, You are the first player, Restarting game!");
      %client.CantRespawn = 0; //they will get the first life
      %game.GameOver();
      reloadCurrentMission();
      //CycleMissions();
   }
   else if($MatchStarted && $CountdownStarted && %count > 1) { // Game in progress, multiple players
      %game.CheckForLivingPlayers();
      messageClient(%client, 'MsgClient', "\c5Welcome To Helljump, Please wait for the next Group to begin.");
      %client.CantRespawn = 1;
   }
   $HellJump::Score[%client] = 0;
   messageClient(%client, 'MsgClientReady',"", "SinglePlayerGame");
   defaultGame::clientMissionDropReady(%game, %client);
   //
   if($HellJump::HellClass[%client] $= "") {
      $HellJump::HellClass[%client] = "HellJumper";
   }
   //
}

function HelljumpGame::onAIRespawn(%game, %client) {
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

function HelljumpGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement) {
	if (%game.testKill(%clVictim, %clKiller)) { //verify victim was an enemy
		%game.awardScoreKill(%clKiller);
		%game.awardScoreDeath(%clVictim);
	}
	else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
		%game.awardScoreSuicide(%clVictim);
}

function HelljumpGame::timeLimitReached(%game) {
	logEcho("game over (timelimit)");
	%game.gameOver();
	cycleMissions();
}

function HelljumpGame::startMatch(%game) {
   $Host::nozombs = 0; //Allow Zombs
   $HellJump::CanSpawnZombies = 1;
   // IN ORDER OF APPEARANCE
   $HellJump::CurrentStrike = 1;
   $HellJump::CurrentGroup = 1;
   $HellJump::CurrentWave = 1;
   //
   $HellJump::Zombiecount = 0; //Start at 0              `
   $HellJump::LivingCount = 0; //Start at 0
   $Host::RankSystem = 0;
   
   $HellJump::Score["Team"] = 0;
   $HellJump::BonusStrike = 0; //used for spawning

   $TWM::PlayingHellJump = 1;
   $HellJump::Game = %game; //This allows us to expand into other functions
   DefaultGame::startMatch(%game);
   messageAll('MsgSPCurrentObjective1' ,"", "Initial Startup - Prepare To Drop");

   %group = NameToID("HellJumpGroup");
   if(%group == -1) {
      %game.HellGroup = new SimGroup(HellJumpGroup);   //used for hell jump
   }
   setSensorGroupCount(31);
   %game.AllowAllRespawn();
   %game.StartWave(1);
}

function HelljumpGame::SaveScores(%game) {
   if (!IsFile("Server/Helljump/Scores/"@formattimestring("hh:nn a mm-dd-yy")@".txt")){
      new fileobject(HellScore);
      HellScore.openforwrite("Server/Helljump/Scores/"@formattimestring("hh-nn-a-mm-dd-yy")@".txt");
      HellScore.writeline("Scores for this round.");
      HellScore.writeline("Wave "@$HellJump::CurrentWave@", Group "@$HellJump::CurrentGroup@", Strike "@$HellJump::CurrentStrike@".");
      HellScore.writeline("Team Score: "@$HellJump::Score["Team"]@".");
	  for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
         %client = ClientGroup.getObject(%i);
         HellScore.writeline(""@%client.namebase@"'s Score: "@$HellJump::Score[%client]@".");
      }
      HellScore.close();
      HellScore.delete();
   }
   else {
      new fileobject(HellScore);
      HellScore.openforappend("Server/Helljump/Scores/"@formattimestring("hh-nn-a-mm-dd-yy")@".txt");
      HellScore.writeline("Scores for this round.");
      HellScore.writeline("Wave "@$HellJump::CurrentWave@", Group "@$HellJump::CurrentGroup@", Strike "@$HellJump::CurrentStrike@".");
      HellScore.writeline("Team Score: "@$HellJump::Score["Team"]@".");
	  for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
         %client = ClientGroup.getObject(%i);
         HellScore.writeline(""@%client.namebase@"'s Score: "@$HellJump::Score[%client]@".");
      }
      HellScore.close();
      HellScore.delete();
   }
}

function HelljumpGame::gameOver(%game) {
    %game.AllowAllRespawn();
    %game.SaveScores();
	DefaultGame::gameOver(%game);

    messageAll('MsgGameOver', "Game Over: Wave "@$HellJump::CurrentWave@", Group "@$HellJump::CurrentGroup@", Strike "@$HellJump::CurrentStrike@". Final Score: "@$HellJump::Score["Team"]@".~wvoice/announcer/ann.gameover.wav" );
	messageAll('MsgClearObjHud', "");

    //Reset the Vars
    $TWM::PlayingHellJump = 0;

    $HellJump::CurrentStrike = 1;
    $HellJump::CurrentGroup = 1;
    $HellJump::CurrentWave = 1;
    
    $HellJump::Score["Team"] = 0;

    $HellJump::Zombiecount = 0;
    $HellJump::LivingCount = 0;

    $Host::RankSystem = 1;

   %group = NameToID("HellJumpGroup");
   if(%group != -1) {
      %group.delete();
   }

	for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
		%client = ClientGroup.getObject(%i);
        $HellJump::Score[%client] = 0;
	}
 
    %game.toggleModifiers("All Off", 1);
}

function HelljumpGame::UpdateClientScoreBar(%game, %client) {
   %alv = $HellJump::LivingCount;
   for(%i = 0; %i < ClientGroup.getcount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      messageClient(%cl, 'MsgSPCurrentObjective2', "", "[Players: "@$HellJump::LivingCount@"][SCORE: Team: "@$HellJump::Score["Team"]@" You: "@$HellJump::Score[%cl]@"]");
   }
}













//TRANSPORT CODE


//This function may be kinda old-style logic wise, but the game should calculate fast enough, that it won't
//really matter if two people spawn at about the same time, and even if we end up with two transports... hey!
//we just saved having to spawn another!
function HelljumpGame::checkTrans(%game, %client) {
   %group = NameToID("HellJumpGroup");
   if(%group == -1) {
      %game.HellGroup = new SimGroup(HellJumpGroup);   //used for hell jump
      %group = %game.HellGroup;
   }
   if(!isObject(%group.getObject(0))) { //no transports yet, so lets spawn #1!
      %trans = %game.SpawnTransport(""@getWords(%client.player.getPosition(), 0, 1)@" "@getRandom(1500,2500)@""); //set the height
      //add the guy to it.
      %client.player.getDataBlock().onCollision(%client.player, %trans, 2);
      %client.player.setMoveState(true); //ha! no running now
      //Added 2.0 - PEople who jump early will be re-added to the slot
      %client.player.saveSlot = 2;
      SafeSpotLoop(%client, %obj, 2);
      %client.jumpDownYet = 0;
      //
      %game.GiveGuns(%client.player);
      return;
   }
   for(%i = 0; %i < %group.getCount(); %i++) {
      %obj = %group.getObject(%i);
      //check the current seats
	  for(%i = 2; %i < 6; %i++){
	     if(!isObject(%obj.getMountNodeObject(%i))){
         //open seat... MINE!
            %client.player.getDataBlock().onCollision(%client.player, %obj, %i);
            //Added 2.0 - PEople who jump early will be re-added to the slot
            %client.player.saveSlot = %i;
            SafeSpotLoop(%client, %obj, %i);
            %client.jumpDownYet = 0;
            //
            %client.player.setMoveState(true); //ha! no running now
            %game.GiveGuns(%client.player);
            return;
         }
         else {
            if(%i == 5) {
               %obj.allSeatsTaken = 1;
            }
         }
      }
      //drats! all seats are gone on that one!
      if(%obj.allSeatsTaken) {
         %i++;
         //we will now check for another transport
         if(!isObject(%group.getObject(%i))) {
            //this one does not exist, so we will need a new one!
            %trans = %game.SpawnTransport(""@getWords(%client.player.getPosition(), 0, 1)@" "@getRandom(1500,2500)@""); //set the height
            //now, we add the player to the first slot on this one
            %client.player.getDataBlock().onCollision(%client.player, %trans, 2);
            %client.player.setMoveState(true); //ha! no running now
            //Added 2.0 - PEople who jump early will be re-added to the slot
            %client.player.saveSlot = 2;
            SafeSpotLoop(%client, %obj, 2);
            %client.jumpDownYet = 0;
            //
            %game.GiveGuns(%client.player);
         }
         else {
            %i--; //the ++ will hit.
            //lets hold for the next loop
         }
      }
   }
}

function HelljumpGame::SpawnTransport(%game, %pos) {
   %phantom = new FlyingVehicle() {
       datablock = "HAPCFlyer";
       team = 1;
       position = %pos;
       Invincible = true;
   };
   %phantom.DroppedYet = 0;
   HellJumpGroup.add(%phantom);
   //15 seconds ends the re-spawn period.
   %goto = $HellJump::DropPos[$CurrentMission];
   %xy = getWords(%goto, 0, 1);
   %z = getWord(%pos, 2);
   %fina = ""@%xy@" "@%z@"";
   %game.schedule(15000, "MoveTransport", %phantom, %fina, 0);
   return %phantom;
}

function HelljumpGame::SpawnAmmoTransport(%game, %pos, %targ) {
   %phantom = new FlyingVehicle() {
       datablock = "HAPCFlyer";
       team = 1;
       position = %pos;
   };
   %phantom.DroppedYet = 0;
   %phantom.Invincible = true;
   MissionCleanup.add(%phantom);
   //ammo transports go right away
   if(%targ $= "") {
      %goto = $HellJump::DropPos[$CurrentMission];
      %xy = getWords(%goto, 0, 1);
      %z = getWord(%pos, 2);
      %targ = ""@%xy@" "@%z@"";
   }
   else {
      %xy = getWords(%targ, 0, 1);
      %z = getWord(%pos, 2);
      %targ = ""@%xy@" "@%z@"";
   }
   %game.schedule(50, "MoveTransport", %phantom, %targ, 1);
   return %phantom;
}

function HellJumpGame::getTransFacingDirection(%game, %trans, %targPos) {
   if(!isObject(%trans)) {
      return;
   }
   %pos = %trans.getworldboxcenter();
   %vector = vectorNormalize(vectorSub(%targPos, %pos));
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %v3 = getword(%vector, 2);
   %nv1 = %v2;
   %nv2 = (%v1 * -1);
   %vector2 = %nv1@" "@%nv2@" "@%v3;
   %trans.setRotation(fullrot("0 0 0",%vector2));

   return %vector;
}

function HelljumpGame::MoveTransport(%game, %transport, %goto, %ammo) {
   if(!isObject(%transport)) {
      return;
   }
   %game.schedule(200, "MoveTransport", %transport, %goto, %ammo);
   //not there yet
   if(!%ammo) {
      if(vectorDist(%transport.getPosition(), %goto) > 700) {
         if(vectorLen(%transport.getVelocity()) < 500) {
            %transport.applyImpulse(%transport.getPosition(),vectorScale(%transport.getForwardVector(), 1000 * 1.3));
            %game.getTransFacingDirection(%transport, %goto);
         }
      }
      else {
         %transport.applyImpulse(%transport.getPosition(),vectorScale(%transport.getForwardVector(), 700));
         if(!%transport.DroppedYet) {
            %transport.DroppedYet = 1;
            %transport.schedule(12500, "SetCloaked", true);
            %transport.schedule(15000, "Delete");

            %frd = %transport.getForwardVector();
            %right = vectorNormalize(vectorSub(%transport.getEdge("1 0 0"),%transport.getEdge("-1 0 0")));
            for(%i = 2; %i < 6; %i++){
	           if(%transport.getMountNodeObject(%i)){
		          %plyr = %transport.getMountNodeObject(%i);
                  %plyr.setMoveState(true); //ha! no running now
		          %pod = schedule((2000 * %i), 0, "MakeDropPod", %frd, %right, %plyr, %transport, %i);
                  BottomPrint(%plyr.client, "Helljumpers!!! Prepare for drop!", 3, 1);
	           }
            }
         }
      }
   }
   else {
      if(vectorDist(%transport.getPosition(), %goto) > 400) {
         if(vectorLen(%transport.getVelocity()) < 500) {
            %transport.applyImpulse(%transport.getPosition(),vectorScale(%transport.getForwardVector(), 1000 * 1.3));
            %game.getTransFacingDirection(%transport, %goto);
         }
      }
      else {
         %transport.applyImpulse(%transport.getPosition(),vectorScale(%transport.getForwardVector(), 700));
         if(!%transport.DroppedYet) {
            %transport.DroppedYet = 1;
            %transport.schedule(12500, "SetCloaked", true);
            %transport.schedule(15000, "Delete");
            
            %frd = %transport.getForwardVector();
            %right = vectorNormalize(vectorSub(%transport.getEdge("1 0 0"),%transport.getEdge("-1 0 0")));
            for(%i = 2; %i < 6; %i++){
               %pod = schedule((2000 * %i), 0, "MakeAmmoPod", %frd, %right, %transport, %i);
            }
         }
      }
   }
}

function SafeSpotLoop(%client, %obj, %slot) {
   if(!isObject(%obj)) {
      return;
   }
   if(!isObject(%client.player) || %client.player.getState() $= "Dead") {
      return;
   }
   if(%client.jumpDownYet) {
      return; //we have been dropped!
   }
   //the goodies
   if(%obj.getMountNodeObject(%slot) != %client.player) {
      %client.player.getDataBlock().onCollision(%client.player, %obj, %slot);
      messageClient(%client, 'msgAlert', "\c3Helljump: Do not jump out of the transport! you will be dropped automatically! ~wfx/misc/warning_beep.wav");
   }
   schedule(150, 0, "SafeSpotLoop", %client, %obj, %slot);
}

















//Scoring System
//Defines:

//Bonus Strike Multiplier: 3X
//Demonic Multiplier: 1.5X
//Where's My Head Multiplier: 2X

//Normal Zombie: 1Pt
//Ravenger Zombie: 3Pt
//Zombie Lord: 10Pt
//Demon Zombie: 5Pt
//Air Rapier Zombie: 7Pt
//Demon Lord Zombie: 40Pt
//Shifter Zombie: 7Pt
//Sniper Zombie: 15Pt
//Volatile Ravenger: 10Pt
//Ultra Demon Zombie: 20Pt

function HelljumpGame::CheckScoreModifiers(%game) {
   %mod = 1;
   if(%game.CheckModifier("Titan") == 1) {
      %mod *= 3;
   }
   if(%game.CheckModifier("Demonic") == 1) {
      %mod *= 1.5;
   }
   if(%game.CheckModifier("WheresMyHead") == 1) {
      %mod *= 2;
   }
   return %mod;
}

//Simple award system, works well!
function HelljumpGame::AwardGamePoints(%game, %client, %points) {
   //first Thing first, get the modifiers
   %mod = %game.CheckScoreModifiers();
   //Lets set two score sets
   $HellJump::Score[%client] += (%points * %mod);
   $HellJump::Score["Team"] += (%points * %mod);
   if($HellJump::Score[%client] >= 1000) {
      awardClient(%client, 17);
      CompleteNWChallenge(%client, "OneK");
   }
   if($HellJump::Score[%client] >= 5000) {
      CompleteNWChallenge(%client, "FiveK");
   }
   if($HellJump::Score[%client] >= 10000) {
      CompleteNWChallenge(%client, "TenK");
   }
   //
   if($HellJump::Score["Team"] >= 7500) {
      CompleteNW_allPlayers("PointsSurge");
   }
   if($HellJump::Score["Team"] >= 25000) {
      CompleteNW_allPlayers("PointsJackpot");
   }
}

function HelljumpGame::OnZombieDeath(%game, %killer, %victim) {
   %ZT = %victim.type;
   switch(%ZT) {
      case 1:
         %game.AwardGamePoints(%killer, 1);
      case 2:
         %game.AwardGamePoints(%killer, 5);
      case 3:
         %game.AwardGamePoints(%killer, 10);
      case 4:
         %game.AwardGamePoints(%killer, 5);
      case 12:
         %game.AwardGamePoints(%killer, 10);
      case 13:
         %game.AwardGamePoints(%killer, 10);
      case 14:
         %game.AwardGamePoints(%killer, 20);
      case 15:
         %game.AwardGamePoints(%killer, 75);
         CompleteNWChallenge(%killer, "DownBoy");
   }
   //
   %game.UpdateClientScoreBar(%killer);
   //%game.CheckNextStrike();
}

//NEW BEACONS
//AMMO DROP
datablock ItemData(AmmoDropCaller)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = AmmoDropCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;

};

datablock ShapeBaseImageData(AmmoDropCallerImage)
{
   className = WeaponImage;

   shapeFile = "stackable3s.dts";
   item = AmmoDropCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function AmmoDropCallerImage::OnFire(%data, %obj, %slot) {
   %obj.client.HasAmmoDrop = 0;
   GainExperience(%obj.client, 500, "Ammo Drop Called In ");

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(AmmoDropCaller, 0, true);
   Game.SpawnAmmoTransport(VectorAdd("0 0 1500",$HellJump::SpawnGraph[$CurrentMission]), %obj.getPosition());
}

function AmmoDropCallerImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   bottomPrint(%obj.client, "<spush><font:Sui Generis:14>>>>Killstreak Beacon<<<<spop>\n<spush><font:Arial:14>Ammo Drop.<spop>",2,2);
}

//Full Team Respawn
datablock ItemData(FullTeamRespawnCaller)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = FullTeamRespawnCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;

};

datablock ShapeBaseImageData(FullTeamRespawnCallerImage)
{
   className = WeaponImage;

   shapeFile = "stackable3s.dts";
   item = FullTeamRespawnCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function FullTeamRespawnCallerImage::OnFire(%data, %obj, %slot) {
   %obj.client.HasFullTeamRespawn = 0;
   GainExperience(%obj.client, 1500, "Full Team Respawn Activated ");
   
   MessageAll('MsgRespawn', "\c5"@%obj.client.namebase@" has activated a full team respawn beacon, you may now respawn.");

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(FullTeamRespawnCaller, 0, true);
   Game.AllowAllRespawn(); //yeah, we can just use this :)
   
   CompleteNWChallenge(%obj.client, "LifeGiver");
}

function FullTeamRespawnCallerImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   bottomPrint(%obj.client, "<spush><font:Sui Generis:14>>>>Killstreak Beacon<<<<spop>\n<spush><font:Arial:14>Full Team Respawn.<spop>",2,2);
}


















//SPAWN SYSTEM
//Waves are the largest, and thus are called at short intervals.
function HelljumpGame::SpawnZombies(%game, %pos, %type) {
   if(!isObject(%game) || !$TWM::PlayingHellJump) {
      error("UE BLOCKED");
      return;
   }
   %c = CreateEmitter(%pos, NightmareGlobeEmitter, "0 0 1");
   %c.schedule(1000, "delete");
   schedule(500, 0, "TWM2Lib_Zombie_Core", "SpawnZombie", "zSpawnCommand", %type, %pos);
}

function HelljumpGame::StartWave(%game, %wave) {
   //lets start the group.
   //Group start at 1!
   $HellJump::CurrentWave = %wave;
   %game.schedule(15000, "StartGroup", 1);
   messageAll('MsgSPCurrentObjective1' ,"", "[W"@$HellJump::CurrentWave@"|G"@$HellJump::CurrentGroup@"|S"@$HellJump::CurrentStrike@"] | Zombies Left: "@$HellJump::Zombiecount@"");
   if(%wave >= 2) {
      CompleteNW_allPlayers("WaveDefeater");
   }
}

function HelljumpGame::StartGroup(%game, %group) {
   //activate the modifiers for these:
   //Allow Respawn:
   //
   if($HellJump::CurrentWave == 1) {
      if(%group >= 2) {
         CompleteNW_allPlayers("GroupBuster");
      }
   }
   else {
      CompleteNW_allPlayers("GroupBuster");
   }
   %game.schedule(20000, "SpawnAmmoTransport", VectorAdd("0 0 1500",$HellJump::SpawnGraph[$CurrentMission]));
   %game.AllowAllRespawn();
   $HellJump::CurrentGroup = %group;
   switch($HellJump::CurrentWave) {
      case 1:
         if(%group == 3) {
            %game.ToggleModifiers("Super-Lunge", 1);
         }
      case 2:
         if(%group == 1) {
            %game.ToggleModifiers("Super-Lunge", 1);
         }
         if(%group == 2) {
            %game.ToggleModifiers("It BURNS!", 1);
         }
         if(%group == 4) {
            %game.ToggleModifiers("Oh Lordy", 1);
         }
         if(%group == 5) {
            %game.ToggleModifiers("The Destiny", 1);
         }
      case 3:
         if(%group == 1) {
            %game.ToggleModifiers("Super-Lunge", 1);
            %game.ToggleModifiers("It BURNS!", 1);
            %game.ToggleModifiers("Oh Lordy", 1);
            %game.ToggleModifiers("The Destiny", 1);
         }
         if(%group == 4) {
            %game.ToggleModifiers("Kamakaziiiii", 1);
         }
      case 4:
         if(%group == 1) {
            %game.ToggleModifiers("Super-Lunge", 1);
            %game.ToggleModifiers("It BURNS!", 1);
            %game.ToggleModifiers("Oh Lordy", 1);
            %game.ToggleModifiers("The Destiny", 1);
            %game.ToggleModifiers("Kamakaziiiii", 1);
         }
         if(%group == 2) {
            %game.ToggleModifiers("Demonic", 1);
         }
         if(%group == 3) {
            %game.ToggleModifiers("Where's My Head", 1);
         }
      case 5:
         if(%group == 1) {
            %game.ToggleModifiers("Super-Lunge", 1);
            %game.ToggleModifiers("It BURNS!", 1);
            %game.ToggleModifiers("Oh Lordy", 1);
            %game.ToggleModifiers("The Destiny", 1);
            %game.ToggleModifiers("Kamakaziiiii", 1);
            %game.ToggleModifiers("Demonic", 1);
            %game.ToggleModifiers("Where's My Head", 1);
            %game.ToggleModifiers("Scrambler", 1);
         }
         if(%group == 5) {
            %game.ToggleModifiers("You can't see me", 1);
         }
      default:
         %game.ToggleModifiers("Super-Lunge", 1);
         %game.ToggleModifiers("It BURNS!", 1);
         %game.ToggleModifiers("Oh Lordy", 1);
         %game.ToggleModifiers("The Destiny", 1);
         %game.ToggleModifiers("Kamakaziiiii", 1);
         %game.ToggleModifiers("Demonic", 1);
         %game.ToggleModifiers("Where's My Head", 1);
         %game.ToggleModifiers("Scrambler", 1);
         %game.ToggleModifiers("You can't see me", 1);
   }
   //bonus strike
   if(%group == 6) {  //6th group, AKA, bonus strike
      %game.schedule(15000, "StartBonusStrike");
      BottomPrintAll("Wave Complete \n Keep Up The Good Work!", 2, 2);
   }                     //bonus complete
   else if(%group == 7) {
      %game.schedule(30000, "StartWave", $HellJump::CurrentWave + 1);
   }
   //in 15 seconds, start the strike
   else {
      %game.schedule(15000, "StartStrike", 1);
   }
   messageAll('MsgSPCurrentObjective1' ,"", "[W"@$HellJump::CurrentWave@"|G"@$HellJump::CurrentGroup@"|S"@$HellJump::CurrentStrike@"] | Zombies Left: "@$HellJump::Zombiecount@"");
}

function HelljumpGame::GoNextStrike(%game) {
   $HellJump::CurrentStrike++;
   CenterPrintAll("Enemy Reinforcements", 2, 2);
   %game.schedule(15000, "StartStrike", $HellJump::CurrentStrike);
   if(!$HellJump::BonusStrike) {
      if($HellJump::CurrentStrike == 6) { //bonus strike is considered a special group
         $HellJump::CurrentStrike = 1;
         %game.StartGroup($HellJump::CurrentGroup + 1);
         BottomPrintAll("Group Complete \n Keep Up The Good Work!", 2, 2);
      }
   }
   else {
      $HellJump::CurrentStrike = 1;
      $HellJump::BonusStrike = 0;
      %game.ToggleModifiers("All Off", 1);
      %game.StartGroup($HellJump::CurrentGroup + 1);
      BottomPrintAll("Bonus Strike Over \n Dead Silence, Prepare for the next Wave!", 2, 2);
   }
}

function HelljumpGame::DefineProperSpawnPos(%game, %loc) {
   %zCheck = GetTerrainHeight(%loc);
   %zProper = %zCheck + 10;
   %p1 = TWM2Lib_MainControl("getRandomPosition", 40 TAB 1);
   %p2 = vectorAdd(%loc, %p1);
   %zCheck2 = GetTerrainHeight(%p2);
   %zProperf = %zCheck2 + 10;
   %p3 = ""@getWords(%p2,0,1)@" "@%zProperf@"";
   echo(%p3);
   return %p3;
}

function HelljumpGame::StartBonusStrike(%game) {
   $HellJump::BonusStrike = 1;
   BottomPrintAll("Bonus Strike \n Good Luck...", 2, 2);
   %game.schedule(2500, "ToggleModifiers", "All On", 1);
   //lets spawn us som zombahs
   $HellJump::Zombiecount = 40;
   for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
      %time = 1000 * getRandom(1, 60);
      %type = getRandom(13,13);
      %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
      %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
   }
   messageAll('MsgSPCurrentObjective1' ,"", "BONUS WAVE");
}

function HelljumpGame::StartStrike(%game, %strike) {
   //set the current strike
   $HellJump::CurrentStrike = %strike;
   //holy :P
   %wv = $HellJump::CurrentWave;
   %gr = $HellJump::CurrentGroup;
   switch(%wv) {
      case 1:
         switch(%gr) {
            case 1:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,1);
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,1);
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,1);
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,2);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,2);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 2:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,2);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,3);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,3);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,3);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,3);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 3:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,3);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = getRandom(1,4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 4:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 5:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 4);
                        if(%type == 2)
                           %type = 13;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 5);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 5);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 5);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
         }
      //once we hit 2, it's the same
      case 2:
         switch(%gr) {
            case 1:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 5);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 5);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 5);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 2:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 3:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 6);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 4:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 5:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
         }
      default:
         switch(%gr) {
            case 1:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 2:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 3:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 4:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
            case 5:
               switch(%strike) {
                  case 1:
                     $HellJump::Zombiecount = 20;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 2:
                     $HellJump::Zombiecount = 25;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 3:
                     $HellJump::Zombiecount = 30;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 4:
                     $HellJump::Zombiecount = 35;
                     for(%i = 0; %i < $HellJump::Zombiecount; %i++) {
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                  case 5:
                     $HellJump::Zombiecount = 40;
                     for(%i = 0; %i < $HellJump::Zombiecount - 1; %i++) {  //spawn 39 zombs, 1 dlors
                        %time = 1000 * getRandom(1, 60);
                        %type = GetRandom(1, 7);
                        if(%type == 2)
                           %type = 13;
                        if(%type == 5)
                           %type = 2;
                        if(%type == 6)
                           %type = 12;
                        if(%type == 7)
                           %type = 14;
                        %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                        %game.schedule(%time, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), %type);
                     }
                     %zone = getRandom(1, $HellJump::ZSpawnZoneCt[$CurrentMission]);
                     %game.schedule(1000, SpawnZombies, %game.DefineProperSpawnPos($HellJump::ZSpawnZone[$CurrentMission, %zone]), 15);
               }
         }
   }
   messageAll('MsgSPCurrentObjective1' ,"", "[W"@$HellJump::CurrentWave@"|G"@$HellJump::CurrentGroup@"|S"@$HellJump::CurrentStrike@"] | Zombies Left: "@$HellJump::Zombiecount@"");
}

//Score menu
function HelljumpGame::updateScoreHud(%game, %client, %tag){
   if (%client.SCMPage $= "")
      %client.SCMPage = 1;
   if (%client.SCMPage $= "SM")
      return;
   $TagToUseForScoreMenu = %tag;
   messageClient( %client, 'ClearHud', "", %tag, 0 );
   messageClient( %client, 'SetScoreHudHeader', "", "" );
   messageClient( %client, 'SetScoreHudHeader', "", "E.V.A.<rmargin:600><just:right><a:gamelink\tNAC\t1>Close</a>" );
   messageClient( %client, 'SetScoreHudSubheader', "", "Hell-Jump Command Hud" );
   //scoreCmdMainMenu(%game,%client,%tag,%client.SCMPage);
   if(%client.hasMedal(17)) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tHellClass\t1>Hell-Class Selection</a>");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPersControl\t1>Settings/Perks</a>");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Weapon Challenges/Upgrades</a>');
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tNAC\t1>Exit</a>");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "-*-*-Module Locked-*-*-");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPersControl\t1>Settings/Perks</a>");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Weapon Challenges/Upgrades</a>');
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tNAC\t1>Exit</a>");
      %index++;
   }
}

function HelljumpGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5){
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
   //end
   echo("[F2] "@%client.namebase@": "@%arg1@", "@%arg2@", "@%arg3@", "@%arg4@", "@%arg5@".");
   switch$ (%arg1) {
   
      case "GTP":
         %game.updateScoreHud(%client, %tag);
         return;

      case "PersControl":
         %client.SCMPage = "SM";
         messageClient( %client, 'SetScoreHudSubheader', "", "Personal Settings" );
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Select An Option");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, 'Killstreak Superweapons - Special Streaks For Helljump');
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPerks\t1>Perks</a>');
         %index++;
         //messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Weapon Challenges</a>');
         //%index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tUpdateSettings\t1>Save Settings</a>');
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
         %index++;
         messageClient( %client, 'ClearHud', "", %tag, %index );
         return;

      case "UpdateSettings":
         UpdateSettings(%client);
         MessageClient(%client, 'msgSaved', "\c5Settings Saved");
         %game.processGameLink(%client, "NAC");
         return;
   
      case "ChooseClass":
           %client.SCMPage = "SM";
           messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tNAC\t1>Close EVA</a>" );
           messageClient( %client, 'SetScoreHudSubheader', "", "Hell-Class Selection" );
           $HellJump::HellClass[%client] = %arg2;
           CompleteNWChallenge(%client, "ClassExtravaganza");
           messageClient( %client, 'SetLineHud', "", "New Class: "@$HellJump::HellClass[%client]@"" );
           %index++;
           messageClient( %client, 'SetLineHud', "", "New Class Weapons: "@$HellClass::Info[$HellJump::HellClass[%client]]@"" );
           %index++;
           messageClient( %client, 'SetLineHud', "", "" );
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tNAC\t1>Exit</a>');
           %index++;
           //
           if(!%client.jumpDownYet) {
              %game.GiveGuns(%client.player);
           }
           return;

      case "HellClass":
           %client.SCMPage = "SM";
           messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tNAC\t1>Close EVA</a>" );
           messageClient( %client, 'SetScoreHudSubheader', "", "Hell-Class Selection" );
           messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Class: "@$HellJump::HellClass[%client]@" - "@$HellClass::Info[$HellJump::HellClass[%client]]@"");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "NOTE: All changes will apply on your next re-spawn");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "Please Choose a Hell-Class:");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tHellJumper\t1>HellJumper</a> - "@$HellClass::Info["HellJumper"]@"");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tLight Sniper\t1>Light Sniper</a> - "@$HellClass::Info["Light Sniper"]@"");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tExplosives\t1>Explosives</a> - "@$HellClass::Info["Explosives"]@"");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tRSA\t1>RSA Commando</a> - "@$HellClass::Info["RSA"]@"");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           if(%client.hasMedal(9)) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tStormlord\t1>Stormlord</a> - "@$HellClass::Info["Stormlord"]@"");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, "");
              %index++;
           }
           else {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "*Locked* Requires The Thunda-Struck Medal");
              %index++;
           }
           if(%client.hasMedal(1)) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tClose Combat\t1>Close Combat</a> - "@$HellClass::Info["Close Combat"]@"");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, "");
              %index++;
           }
           else {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "*Locked* Requires The He Fell Again Medal");
              %index++;
           }
           if(%client.hasMedal(13)) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tShadow Guard\t1>Shadow Guard</a> - "@$HellClass::Info["Shadow Guard"]@"");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, "");
              %index++;
           }
           else {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "*Locked* Requires The Source Of All Evil Medal");
              %index++;
           }
           if(%client.hasMedal(20)) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tCollider Fanatic\t1>Collider Fanatic</a> - "@$HellClass::Info["Collider Fanatic"]@"");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, "");
              %index++;
           }
           else {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "*Locked* Requires The Hazing Medal");
              %index++;
           }
           if(%client.hasMedal(21)) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tFlamer\t1>Flamer</a> - "@$HellClass::Info["Flamer"]@"");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, "");
              %index++;
           }
           else {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "*Locked* Requires The Glorious Fire Medal");
              %index++;
           }
           if(%client.CheckNWChallengeCompletion("NewYearsEve")) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tChooseClass\tJavelin\t1>Javelin</a> - "@$HellClass::Info["Javelin"]@"");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, "");
              %index++;
           }
           else {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "*Locked* Requires The New Years Eve Fireworks Challenge");
              %index++;
           }
           messageClient( %client, 'SetLineHud', "", %tag, %index, "");
           %index++;
           messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tNAC\t1>Exit</a>');
           %index++;
           return;

        //other
        case "ActivateUpgrade":
             %image = %arg2;
             %upgrade = %arg3;
             %client.DisableAllUpgrades(%image); //disable all first
             %client.ActivateUpgrade(%image, %upgrade);
             %game.processGameLink(%client, "CompletedSub");
             return;
             
        case "DeActivateUpgrades":
             %image = %arg2;
             %client.DisableAllUpgrades(%image); //disable all
             %game.processGameLink(%client, "CompletedSub");
             return;

        case "CompletedSub":
             %image = %arg2;
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Completed Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Upgrade To Use");
             %index++;
             %index = GenerateCompletedSubMenu(%client, %tag, %index, %image);
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "CompletedChallenge":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Completed Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Weapon");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateCompletedChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "WeaponTasksSub":
             %image = %arg2;
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "In-Complete Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Challenges:");
             %index++;
             %index = GenerateWChallengeSubMenu(%client, %tag, %index, %image);
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "WeaponsTasks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "In-Complete Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Weapon");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateWeaponChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "OtherTasks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "In-Complete Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Challenge");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             //%index = GenerateChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "WeaponChallenge":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Weapon Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select An Option");
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tCompletedChallenge\t1>Completed Challenges & Upgrades</a>');
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponsTasks\t1>Specific Weapon Challenges</a>');
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOtherTasks\t1>Additional Challenges</a>');
             %index++;
             //
             //%index = CreatePerkMenu(%client, %tag, %index);
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;
             
        case "Perks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "PERKS" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "--- ACTIVE PERKS ---");
             %index++;
             GetActivePerks(%client);  //Reload This First
             messageClient( %client, 'SetLineHud', "", %tag, %index, "PRIMARY: "@%client.Perk[1]@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "SECONDARY: "@%client.Perk[2]@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "TERTIARY: "@%client.Perk[3]@"");
             %index++;
             //
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "--- AVAILIABLE PERKS ---");
             %index++;
             //
             %index = CreatePerkMenu(%client, %tag, %index);
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;

        case "PerkStatus":
           DisableAllPerkGroup(%client, $Perk::PerkToGroup[%arg2]);
           SetPerkStatus(%client, %arg2, 1);
           %game.processGameLink(%client, "Perks");
           return;
        //
        default:
           %client.notFirstUse = 1;
   }
   closeScoreHudFSERV(%client);
}

function HelljumpGame::GiveGuns(%game, %player) {
   //
   %class = $HellJump::HellClass[%player.client];
   //
   switch$ (%class) {
      case "HellJumper":
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(S3SRifle, 1, true);
         %player.setInventory(S3SRifleAmmo, 30, true);
         %player.ClipCount[S3SRifleImage.ClipName] = S3SRifleImage.InitialClips;
         %player.setInventory(Mp26CMDO, 1, true);
         %player.setInventory(Mp26CMDOAmmo, 50, true);
         %player.ClipCount[Mp26CMDOImage.ClipName] = Mp26CMDOImage.InitialClips;
         %player.setInventory(Pistol, 1, true);
         %player.setInventory(PistolAmmo, 50, true);  //I'm nice for your first clip :)
         %player.ClipCount[PistolImage.ClipName] = PistolImage.HellClipCount;
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(Mp26CMDO);
      case "Light Sniper":
         %player.setArmor("Light");
         %player.clearInventory();
         %player.setInventory(R700SniperRifle, 1, true);
         %player.setInventory(R700SniperRifleAmmo, 10, true);
         %player.ClipCount[R700SniperRifleImage.ClipName] = R700SniperRifleImage.InitialClips;
         %player.setInventory(Shocklance, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(R700SniperRifle);
      case "Explosives":
         %player.setArmor("Heavy");
         %player.clearInventory();
         %player.setInventory(RPG, 1, true);
         %player.setInventory(RPGAmmo, 1, true);
         %player.ClipCount[RPGImage.ClipName] = 25; //have fun with your RPGs
         %player.setInventory(Pistol, 1, true);
         %player.setInventory(PistolAmmo, 50, true);  //I'm nice for your first clip :)
         %player.ClipCount[PistolImage.ClipName] = PistolImage.HellClipCount;
         %player.setInventory(MG42, 1, true);
         %player.setInventory(MG42Ammo, 200, true);
         %player.ClipCount[MG42Image.ClipName] = MG42Image.InitialClips;
         %player.setInventory(Javelin, 1, true);
         %player.setInventory(JavelinAmmo, 1, true);
         %player.ClipCount[JavelinImage.ClipName] = 10;
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(RPG);
      case "Stormlord":
         %player.setArmor("Microburst");
         %player.clearInventory();
         %player.setInventory(S3Rifle, 1, true);
         %player.setInventory(S3RifleAmmo, 10, true);
         %player.ClipCount[S3RifleImage.ClipName] = S3RifleImage.InitialClips;
         %player.setInventory(IonRifle, 1, true);
         %player.setInventory(Shocklance, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(StaticGrenade,5);
         %player.use(IonRifle);
      case "Close Combat":
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(Wp400, 1, true);
         %player.setInventory(Wp400Ammo, 5, true);
         %player.ClipCount[Wp400Image.ClipName] = Wp400Image.InitialClips;
         %player.setInventory(Pg700, 1, true);
         %player.setInventory(Pg700Ammo, 45, true);
         %player.ClipCount[Pg700Image.ClipName] = Pg700Image.InitialClips;
         %player.setInventory(LD06Savager, 1, true);
         %player.setInventory(LD06SavagerAmmo, 10, true);  //I'm nice for your first clip :)
         %player.ClipCount[LD06SavagerImage.ClipName] = LD06SavagerImage.InitialClips;
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(Wp400);
      case "Shadow Guard":
         %player.setArmor("ShadowCommando"); //commando!
         %player.clearInventory();
         %player.setInventory(ShadowRifle, 1, true);
         %player.setInventory(S3Rifle, 1, true);
         %player.setInventory(S3RifleAmmo, 10, true);
         %player.ClipCount[S3RifleImage.ClipName] = S3RifleImage.InitialClips;
         %player.setInventory(LD06Savager, 1, true);
         %player.setInventory(LD06SavagerAmmo, 10, true);  //I'm nice for your first clip :)
         %player.ClipCount[LD06SavagerImage.ClipName] = LD06SavagerImage.InitialClips;
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(ShadowRifle);
      case "RSA":
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(lasergun, 1, true);
         %player.setInventory(lasergunAmmo, 10, true);
         %player.ClipCount[lasergunImage.ClipName] = 7;
         %player.setInventory(S3SRifle, 1, true);
         %player.setInventory(S3SRifleAmmo, 30, true);
         %player.ClipCount[S3SRifleImage.ClipName] = S3SRifleImage.InitialClips;
         %player.setInventory(PulsePhaser, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(lasergun);
      case "Flamer":
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(flamer, 1, true);
         %player.setInventory(flamerAmmo, 100, true);
         %player.ClipCount[flamerImage.ClipName] = 999;
         %player.setInventory(Pistol, 1, true);
         %player.setInventory(PistolAmmo, 50, true);  //I'm nice for your first clip :)
         %player.ClipCount[PistolImage.ClipName] = PistolImage.HellClipCount;
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(flamer);
      case "Javelin":
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(Javelin, 1, true);
         %player.setInventory(JavelinAmmo, 1, true);
         %player.ClipCount[JavelinImage.ClipName] = 999;
         %player.setInventory(Deagle, 1, true);
         %player.setInventory(DeagleAmmo, 7, true);  //I'm nice for your first clip :)
         %player.ClipCount[DeagleImage.ClipName] = DeagleImage.InitialClips;
         %player.setInventory(TargetingLaser, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(Javelin);
      default:
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(S3SRifle, 1, true);
         %player.setInventory(S3SRifleAmmo, 30, true);
         %player.ClipCount[S3SRifleImage.ClipName] = S3SRifleImage.InitialClips;
         %player.setInventory(Mp26CMDO, 1, true);
         %player.setInventory(Mp26CMDOAmmo, 50, true);
         %player.ClipCount[Mp26CMDOImage.ClipName] = Mp26CMDOImage.InitialClips;
         %player.setInventory(Pistol, 1, true);
         %player.setInventory(PistolAmmo, 50, true);  //I'm nice for your first clip :)
         %player.ClipCount[PistolImage.ClipName] = PistolImage.HellClipCount;
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(Mp26CMDO);
   }
}

//HellClass info(1.6)
$HellClass::Info["HellJumper"] = "Commando Armor - S3-S Rifle, MP26 CMDO, Colt Pistol";
$HellClass::Info["Light Sniper"] = "Scout Armor - R700 Sniper, X-81 Shocklance";
$HellClass::Info["Explosives"] = "Support Armor - RPG-7, MG42 Machine Gun, Javelin, Colt Pistol";
$HellClass::Info["RSA"] = "Commando Armor - RSA Laser Rifle, S3-S Rifle, ES-77 Pulse Phaser";
$HellClass::Info["Stormlord"] = "Microburst Armor - S3 Rifle, LUST Ion Rifle, X-81 Shocklance";
$HellClass::Info["Close Combat"] = "Commando Armor - Wp400 Shotgun, Pg700 SMG, LD06 Savager";
$HellClass::Info["Shadow Guard"] = "S.Commando Armor - Sh4d3 Shadow Rifle, S3 Rifle, LD06 Savager";
//Special Classes (most of these have 1 weapon that cannot be obtained in a drop pod and a pistol.
$HellClass::Info["Collider Fanatic"] = "Commando Armor - PRTCL-995 MCC, Colt Pistol";
$HellClass::Info["Flamer"] = "Commando Armor - A|V|X Flamethrower (Unlimited), Colt Pistol";
$HellClass::Info["Javelin"] = "Commando Armor - Javelin (Unlimited), Desert Eagle Pistol";