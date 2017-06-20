// --------------------------------------------------------
// Deathmatch mission type
// --------------------------------------------------------

// DisplayName = Wartower

//--- GAME RULES BEGIN ---
//It's quite easy...
//Ascend the tower, kill any in your way
//--- GAME RULES END ---


$InvBanList[Wartower, "GrappleHook"] = 1;
$InvBanList[Wartower, "IonLauncher"] = 1;
$InvBanList[Wartower, "IonRifle"] = 1;
$InvBanList[Wartower, "Flame"] = 1;
$InvBanList[Wartower, "Tech"] = 1;
$InvBanList[Wartower, "Mine"] = 1;
$InvBanList[Wartower, "ConcussionGrenade"] = 1;

function WartowerGame::setUpTeams(%game)
{  
   %group = nameToID("MissionGroup/Teams");
   if(%group == -1)
      return;
   
   // create a team0 if it does not exist
   %team = nameToID("MissionGroup/Teams/team0");
   if(%team == -1)
   {
      %team = new SimGroup("team0");
      %group.add(%team);
   }

   // 'team0' is not counted as a team here
   %game.numTeams = 0;
   while(%team != -1)
   {
      // create drop set and add all spawnsphere objects into it
      %dropSet = new SimSet("TeamDrops" @ %game.numTeams);
      MissionCleanup.add(%dropSet);

      %spawns = nameToID("MissionGroup/Teams/team" @ %game.numTeams @ "/SpawnSpheres");
      if(%spawns != -1)
      {
         %count = %spawns.getCount();
         for(%i = 0; %i < %count; %i++)
            %dropSet.add(%spawns.getObject(%i));
      }

      // set the 'team' field for all the objects in this team
      %team.setTeam(0);

      clearVehicleCount(%team+1);
      // get next group
      %team = nameToID("MissionGroup/Teams/team" @ %game.numTeams + 1);
      if (%team != -1)
         %game.numTeams++;
   }

   // set the number of sensor groups (including team0) that are processed
   setSensorGroupCount(%game.numTeams + 1);
   %game.numTeams = 1;

   // allow teams 1->31 to listen to each other (team 0 can only listen to self)
   for(%i = 1; %i < 32; %i++)
      setSensorGroupListenMask(%i, 0xfffffffe);
}

function WartowerGame::initGameVars(%game)
{
   %game.SCORE_PER_KILL = 1; 
   %game.SCORE_PER_DEATH = -1;
   %game.SCORE_PER_SUICIDE = -1;
}  

function WartowerGame::allowsProtectedStatics(%game)
{
   return true;
}

function WartowerGame::equip(%game, %player) {
   buyFavorites(%player.client);
}

function WartowerGame::pickPlayerSpawn(%game, %client, %respawn) {
   %start = $WarTower::SpawnZone[$CurrentMission];
   %pos = vectorAdd(%start, TWM2Lib_MainControl("getRandomPosition", 4 TAB 1));
   return %pos;
}

function WartowerGame::clientJoinTeam( %game, %client, %team, %respawn )
{
   %game.assignClientTeam( %client );
   
   // Spawn the player:
   %game.spawnPlayer( %client, %respawn );
}

function WartowerGame::assignClientTeam(%game, %client)
{
   for(%i = 1; %i < 32; %i++)
      $WartowerTeamArray[%i] = false;

   %maxSensorGroup = 0;
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++)
   {
      %cl = ClientGroup.getObject(%i);
      if(%cl != %client)
      {
         $WartowerTeamArray[%cl.team] = true;
         if (%cl.team > %maxSensorGroup)
            %maxSensorGroup = %cl.team;
      }
   }

   //now loop through the team array, looking for an empty team
   for(%i = 1; %i < 32; %i++)
   {
      if (! $WartowerTeamArray[%i])
      {
         %client.team = %i;
         if (%client.team > %maxSensorGroup)
            %maxSensorGroup = %client.team;
         break;
      }
   }

   // set player's skin pref here
   setTargetSkin(%client.target, %client.skin);

   // Let everybody know you are no longer an observer:
   messageAll( 'MsgClientJoinTeam', '\c1%1 has joined the fray.', %client.name, "", %client, 1 );
   updateCanListenState( %client );

   //now set the max number of sensor groups...
   setSensorGroupCount(%maxSensorGroup + 1);
}

function WartowerGame::clientMissionDropReady(%game, %client)
{
   messageClient(%client, 'MsgClientReady',"", %game.class);
   messageClient(%client, 'MsgYourScoreIs', "", 0);
   messageClient(%client, 'MsgWartowerPlayerDies', "", 0);
   messageClient(%client, 'MsgWartowerKill', "", 0);
   %game.resetScore(%client);

   messageClient(%client, 'MsgMissionDropInfo', '\c0You are in mission %1 (%2).', $MissionDisplayName, $MissionTypeDisplayName, $ServerName ); 
   
   DefaultGame::clientMissionDropReady(%game, %client);
}

function WartowerGame::AIHasJoined(%game, %client)
{
   //let everyone know the player has joined the game
   //messageAllExcept(%client, -1, 'MsgClientJoinTeam', '%1 has joined the fray.', %client.name, "", %client, 1 );
}

function WartowerGame::checkScoreLimit(%game, %client)
{
   //there's no score limit in Wartower
}

function WartowerGame::createPlayer(%game, %client, %spawnLoc, %respawn)
{
   DefaultGame::createPlayer(%game, %client, %spawnLoc, %respawn);
   %client.setSensorGroup(%client.team);
   //give invincibility flag
   %client.player.setInvinc(true);
}

function WartowerGame::resetScore(%game, %client)
{
   %client.deaths = 0;
   %client.kills = 0;
   %client.score = 0;
   %client.efficiency = 0.0;
   %client.suicides = 0;
}

function WartowerGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLoc)
{
   cancel(%clVictim.player.alertThread);
   DefaultGame::onClientKilled(%game, %clVictim, %clKiller, %damageType, %implement, %damageLoc);
}

function WartowerGame::updateKillScores(%game, %clVictim, %clKiller, %damageType, %implement)
{
   if (%game.testKill(%clVictim, %clKiller)) //verify victim was an enemy
   {
      %game.awardScoreKill(%clKiller);
      messageClient(%clKiller, 'MsgWartowerKill', "", %clKiller.kills);
      %game.awardScoreDeath(%clVictim);
   }       
   else if (%game.testSuicide(%clVictim, %clKiller, %damageType))  //otherwise test for suicide
      %game.awardScoreSuicide(%clVictim);     

   messageClient(%clVictim, 'MsgWartowerPlayerDies', "", %clVictim.deaths + %clVictim.suicides);
}

function WartowerGame::recalcScore(%game, %client)
{
   %killValue = %client.kills * %game.SCORE_PER_KILL;
   %deathValue = %client.deaths * %game.SCORE_PER_DEATH;
   %suicideValue = %client.suicides * %game.SCORE_PER_SUICIDE;

   if (%killValue - %deathValue == 0)
      %client.efficiency = %suicideValue;
   else
      %client.efficiency = ((%killValue * %killValue) / (%killValue - %deathValue)) + %suicideValue;

   %client.score = mFloatLength(%client.efficiency, 1);
   messageClient(%client, 'MsgYourScoreIs', "", %client.score);
   %game.recalcTeamRanks(%client);
   %game.checkScoreLimit(%client);
}

function WartowerGame::timeLimitReached(%game)
{
   logEcho("game over (timelimit)");
   %game.gameOver();
   cycleMissions();
}

function WartowerGame::scoreLimitReached(%game)
{
   logEcho("game over (scorelimit)");
   %game.gameOver();
   cycleMissions();
}

function WartowerGame::startMatch(%game) {
   DefaultGame::startMatch(%game);
   
   $Ion::StopIon = 1;

   $TWM::PlayingWarTower = 1;
   $FissionEndsGame = 1;
}

function WartowerGame::gameOver(%game)
{
   $TWM::PlayingWarTower = 0;
   $FissionEndsGame = 0;
   //call the default
   DefaultGame::gameOver(%game);

   $Ion::StopIon = 0;

   messageAll('MsgGameOver', "Match has ended.~wvoice/announcer/ann.gameover.wav" );

   cancel(%game.timeThread);
   messageAll('MsgClearObjHud', "");
   for(%i = 0; %i < ClientGroup.getCount(); %i ++) {
      %client = ClientGroup.getObject(%i);
      %game.resetScore(%client);
   }
}

function WartowerGame::enterMissionArea(%game, %playerData, %player)
{
   %player.client.outOfBounds = false; 
   messageClient(%player.client, 'EnterMissionArea', '\c1You are back in the mission area.');
}

function WartowerGame::leaveMissionArea(%game, %playerData, %player)
{
   if(%player.getState() $= "Dead")
      return;
                                         
   %player.client.outOfBounds = true;
   messageClient(%player.client, 'LeaveMissionArea', '\c1You have left the mission area.~wfx/misc/warning_beep.wav');
}

function WartowerGame::updateScoreHud(%game, %client, %tag)
{
   ConstructionGame::updateScoreHud(%game, %client, %tag);
}

function WartowerGame::applyConcussion(%game, %player)
{
}
