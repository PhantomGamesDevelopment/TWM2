package TWM2Mission_Surrounded {
   function TWM2MissionClass::initiateSettings(%group) {
      %group.commandName = "Command";
      %group.failMessage = "Your team has been eliminated, mission failed";
      %group.BonusCompleteMessage = "Excellent work team, you held off those zombies perfectly.";
      %group.CompleteMessageNoTime = "Nice job team, head on back to base.";
      %group.bonusEXP = 2500;
      %group.completionEXP = 1000;
   }

   function TWM2MissionClass::OnTimeZero(%group) {
      %group.MessageMissionGroup("\c5"@%group.commandName@": You have survived!!! mission acomplished");
      for(%i = 1; %i <= %group.participants; %i++) {
         %spF = game.pickPlayerSpawn( %group.participant[%i], false );
         %group.participant[%i].player.setPosition(%spF);
      
         AwardClient(%group.participant[%i], 35);
		 
		 CompleteNWChallenge(%group.participant[%i], "Survivalist");
      }
      %group.AddMissionTime(10); //surviving = success with reward
      %group.CompleteMission();
   }

   function TWM2MissionClass::StartTWM2Mis(%group) {
      //
      %building = new (StaticShape) () {datablock = "GeneratorLarge";position = "9526.46 7984.89 93";rotation = "0 0 -1 21.2091";scale = "1 1 1";team = "1";ownerGUID = "2000343";deployed = "1";powerFreq = "1";isSwitchedOn = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9488.05 8001.65 100";rotation = "0 0 1 115.894";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9571.97 7971.99 100";rotation = "0 0 -1 76.7506";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9557.85 7947.1 100";rotation = "0 0 -1 40.5343";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9510.12 8024.52 100";rotation = "0 0 1 157.406";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9535.39 7934.75 100";rotation = "0 0 -1 9.23422";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9512.93 7935.66 100";rotation = "0 0 1 17.4571";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9478.97 7983.36 100";rotation = "0 0 1 91.0928";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9493.53 7946.49 100";rotation = "0 0 1 43.5003";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9528.58 8031.41 100";rotation = "0 0 1 181.104";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9550.88 8025.84 100";rotation = "0 0 1 208.169";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9572.8 8003.44 100";rotation = "0 0 -1 114.945";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      %building = new (StaticShape) () {datablock = "DeployedZSpawnBase";position = "9482.44 7961.31 100";rotation = "0 0 1 64.9402";scale = "1 1 1";team = "1";ownerGUID = "2000343";powerFreq = "1";ZType = "1";isInTheMission="1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.getDatablock().gainPower(%building);%building.ZCLoop = schedule(1000, 0, "ZcreateLoop", %building);
      TWM2PowerCheck();
      //

      %sp = "9528 7981 105";
      for(%i = 1; %i <= %group.participants; %i++) {
         %spF = vectorAdd(%sp, TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
         %group.participant[%i].player.setPosition(%spF);
         //
         %player = %group.participant[%i].player;
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(S3Rifle, 1, true);
         %player.setInventory(S3RifleAmmo, 10, true);
         %player.ClipCount[S3RifleImage.ClipName] = mfloor(S3RifleImage.InitialClips * 1.5);
         %player.setInventory(M1700, 1, true);
         %player.setInventory(M1700Ammo, 1, true);
         %player.ClipCount[M1700Image.ClipName] = mfloor(M1700Image.InitialClips * 1.5);
         %player.setInventory(pistol, 1, true);
         %player.setInventory(pistolAmmo, 10, true);
         %player.ClipCount[pistolImage.ClipName] = pistolImage.InitialClips;
         %player.setInventory(TargetingLaser, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(S3Rifle);
      }
   }
};
