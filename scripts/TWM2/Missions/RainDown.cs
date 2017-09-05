package TWM2Mission_RainDown {
   function TWM2MissionClass::initiateSettings(%group) {
      %group.commandName = "AC-130 Pilot";
      %group.failMessage = "Our mission has failed, we'll get them next time";
      %group.BonusCompleteMessage = "Nice run gunner! let's head back home!";
      %group.CompleteMessageNoTime = "Good work, We've neutralized the enemy forces just in time.";
      %group.bonusEXP = 2500;
      %group.completionEXP = 5000;
   }

   function TWM2MissionClass::OnTimeZero(%group) {
      MessageClient(%group.Participant[1], 'msgShip', "\c5"@%group.commandName@": We're low on fuel, we need to turn back now.");
      %group.EndTWM2Mission();
   }

   function RainDownMissionLoop(%group, %zgroup) {
      if(%group.InProgress == 0 || %group.InProgress $= "") {
         echo("complete.");
         return;
      }
      %living = 0;
      for(%i = 0; %i < %zGroup.getCount(); %i++) {
         %zombie = %zGroup.getObject(%i);
         if(%zombie.isZombie && %zombie.isInTheMission) {
            if(!isObject(%zombie) || %zombie.getState() $= "dead") {

            }
            else {
               %living++;
            }
         }
      }
      //
      if(%living == 0) {
		 CompleteNWChallenge(%group.participant[1], "ExpertGunner");
         AwardClient(%group.participant[1], 32);
         %group.CompleteMission();
      }
      schedule(1000, 0, "RainDownMissionLoop", %group, %zgroup);
   }

   function TWM2MissionClass::StartTWM2Mis(%group) {
      %missionPosCenter = "5400 12000 110";
      for(%i = 0; %i < 15; %i++) {
         %posx = vectorAdd(%missionPosCenter, TWM2Lib_MainControl("getRandomPosition", 25 TAB 1));
         %zombie = StartAZombie(%posx, 1);
         %zombie.isInTheMission = 1;
      }
      RainDownMissionLoop(%group, MissionCleanup);
   
      //
      %client = %group.Participant[1];
   
      %obj = new FlyingVehicle() {
         dataBlock    = AC130;
         position     = vectoradd(%missionPosCenter, "0 700 400");
         rotation     = "0 0 0 1";
         team         = %client.team;
      };
      MissionCleanUp.add(%obj);
      %obj.TurretObject.barrel = "Chain";
      %obj.TurretObject.schedule(2000, SetFrozenState, false);
      %obj.TurretObject.schedule(2000, SetMoveState, false);

      %obj.isHarbinsWrathShip = 1;
      %obj.isUltrAlly = 1;

      setTargetSensorGroup(%obj.getTarget(), %client.team);

      %obj.GoPoint = 1;
      %obj.CanFlare = 1;
      GunshipForwardImpulse(%obj);
      %obj.ScanLoop = GetNextGunshipPoint(%obj);

      schedule(180*1000, 0, "EndGunship", %obj, %client);
      %client.schedule(1000, "setControlObject", %obj.turretObject);
      commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%obj.turretObject,%client.player));
      %client.gunshipControlLoop = schedule(1000, 0, "GunshipControlLoop", %client, %obj);
      messageClient(%client, 'msgControls', "\c3GUNSHIP: Press the [Mine] key to toggle weapons");
      %client.player.lastTransformStuff = %client.player.getTransform();
      %client.player.setPosition(VectorAdd(%x SPC %y SPC 0,$Prison::JailPos));
   }
};
