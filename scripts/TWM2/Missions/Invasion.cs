package TWM2Mission_Invasion {
   function TWM2MissionClass::initiateSettings(%group) {
      %group.commandName = "Command";
      %group.failMessage = "Your squad has been eliminated, mission failed";
      %group.BonusCompleteMessage = "Excellent work soldiers, You've held back a major storm!";
      %group.CompleteMessageNoTime = "All enemy dropships neutralized, good work team.";
      %group.bonusEXP = 2500;
      %group.completionEXP = 2000;
   }

   function TWM2MissionClass::OnTimeZero(%group) {
      %group.MessageMissionGroup("\c5"@%group.commandName@": Enemy dropships have been eliminated, good work team!");
      for(%i = 1; %i <= %group.participants; %i++) {
         %spF = game.pickPlayerSpawn( %group.participant[%i], false );
         %group.participant[%i].player.setPosition(%spF);
      
         AwardClient(%group.participant[%i], 37);
      }
      %group.AddMissionTime(10); //surviving = success with reward
      %group.CompleteMission();
   }

   function TWM2MissionClass::StartTWM2Mis(%group) {
      %sp = "19528 17981 105";
      for(%i = 1; %i <= %group.participants; %i++) {
         %spF = vectorAdd(%sp, getRandomPosition(5, 1));
         %group.participant[%i].player.setPosition(%spF);
         //
         %player = %group.participant[%i].player;
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(S3SRifle, 1, true);
         %player.setInventory(S3SRifleAmmo, 30, true);
         %player.ClipCount[S3SRifleImage.ClipName] = mfloor(S3SRifleImage.InitialClips * 1.5);
         %player.setInventory(Mp26CMDO, 1, true);
         %player.setInventory(Mp26CMDOAmmo, 50, true);
         %player.ClipCount[Mp26CMDOImage.ClipName] = mfloor(Mp26CMDOImage.InitialClips * 1.5);
         %player.setInventory(pistol, 1, true);
         %player.setInventory(pistolAmmo, 10, true);
         %player.ClipCount[pistolImage.ClipName] = pistolImage.InitialClips;
         %player.setInventory(TargetingLaser, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(S3Rifle);
      }
      //
      spawnDropshipWave(%group, %sp);
      schedule(30000, 0, spawnDropshipWave, %group, %sp);
      schedule(60000, 0, spawnDropshipWave, %group, %sp);
      schedule(90000, 0, spawnDropshipWave, %group, %sp);
      schedule(120000, 0, spawnDropshipWave, %group, %sp);
      schedule(150000, 0, spawnDropshipWave, %group, %sp);
      schedule(180000, 0, spawnDropshipWave, %group, %sp);
      schedule(190000, 0, spawnDropshipWave, %group, %sp);
   }

   function spawnHunterDropship_Mission(%position, %dropPosition, %dropType) {
      %dropT = %dropType SPC %dropType SPC %dropType SPC %dropType;
      //spawn dropship
      %drop = new FlyingVehicle() {
          dataBlock  = "HunterDropship";
          position = %position;
          respawn    = "0";
          teamBought = 30;
          team = 30;
      };
      MissionCleanup.add(%drop);
      //attach waypoint, spawn pilot/passengers
      %wraith = StartAZombie(vectorAdd(%position, "0 0 100"), 15);
      %wraith.isInTheMission = 1;
      %drop.mountObject(%wraith, 0);
      for(%i = 0; %i < getWordCount(%dropT); %i++) {
         %z = StartAZombie(vectorAdd(%position, "0 0 100"), getWord(%dropT, %i));
         %z.isInTheMission = 1;
         if(isObject(%z)) {
            %drop.mountObject(%z, %i+2);
            //%z.mountObject(%drop, %i+2);
         }
      }
      dropshipMarkerLoop(%drop);
      //engage dropship AI
      BeginDropshipAI(%drop, %dropPosition);
   }

   function spawnDropshipWave(%group, %start) {
      if(!isObject(%group)) {
         return;
      }
      %pos[0] = vectorAdd(%start, "2000 0 400");
      %pos[1] = vectorAdd(%start, "-2000 0 400");
      %pos[2] = vectorAdd(%start, "0 2000 400");
      %pos[3] = vectorAdd(%start, "0 -2000 400");
   
      spawnHunterDropship_Mission(%pos[0], %start, 1);
      schedule(2500, 0, "spawnHunterDropship_Mission", %pos[1], %start, 1);
      schedule(5000, 0, "spawnHunterDropship_Mission", %pos[2], %start, 1);
      schedule(7500, 0, "spawnHunterDropship_Mission", %pos[3], %start, 1);
   }
};
