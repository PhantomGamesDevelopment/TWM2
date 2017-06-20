package TWM2Mission_EnemyAc130Above {
   function TWM2MissionClass::initiateSettings(%group) {
      %group.commandName = "Command";
      %group.failMessage = "Your squad has been eliminated, mission failed";
      %group.BonusCompleteMessage = "Excellent work soldiers, you showed those assholes who's in charge!";
      %group.CompleteMessageNoTime = "The enemy AC-130 has been neutralized, good job. return to base.";
      %group.bonusEXP = 2500;
      %group.completionEXP = 2000;
   }

   function TWM2MissionClass::OnTimeZero(%group) {
      %group.MessageMissionGroup("\c5"@%group.commandName@": The enemy AC-130 is leaving the area, mission acomplished");
      //%group.AddMissionTime(10);
      for(%i = 1; %i <= %group.participants; %i++) {
         %spF = game.pickPlayerSpawn( %group.participant[%i], false );
         %group.participant[%i].player.setPosition(%spF);

         AwardClient(%group.participant[%i], 33);
      }

      %group.CompleteMission();
   }

   function TWM2MissionClass::StartTWM2Mis(%group) {
      %missionPosCenter = "8000 9000 105";
      //Spawn the building
   
      //
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8036.76 9462.89 100.25";rotation = "0.66138 0.353776 0.661377 141.035";scale = "0.125 0.166666 32";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8047.85 9465.1 100";rotation = "4.7988e-06 -4.11945e-06 1 213.714";scale = "3.8739 4.99873 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8045.71 9475.85 100.25";rotation = "-0.797515 -0.426599 -0.426594 102.854";scale = "0.125 0.166666 32";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8037.11 9462.96 100.25";rotation = "-0.797515 -0.426599 -0.426594 102.854";scale = "0.125 0.166666 32";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8058.67 9466.9 100.25";rotation = "-0.919162 0.278512 0.278519 94.824";scale = "0.125 0.166666 29.9826";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8063.25 9474.5 100.5";rotation = "0 0 1 211.042";scale = "3.992 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8066.11 9475.42 100.5";rotation = "0 0 -1 5.85046";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "6";set2 = "8";packBlock = "StingerAmmo";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8056.73 9478.72 106.048";rotation = "0.656598 0.371165 0.656594 139.273";scale = "0.976 0.166666 30.0194";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8061.15 9486 104.096";rotation = "0.870536 0.492104 6.23787e-07 180";scale = "3.24015 0.166666 7.192";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8067.37 9474.22 100.5";rotation = "0 0 -1 6.42179";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "6";set2 = "7";packBlock = "Stinger";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8063.32 9474.19 100.25";rotation = "0.192695 0.693856 0.693853 201.814";scale = "4.12805 0.166666 32.0056";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8063.38 9474.72 108.25";rotation = "0.656598 0.371165 0.656594 139.273";scale = "0.125 5.3325 30.0194";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "SolarPanel";position = "8071.83 9473.28 105.04";rotation = "-0.371166 0.656597 0.656594 139.273";scale = "1 1 1";team = "1";ownerGUID = "2000343";deployed = "1";powerFreq = "1";};setTargetSensorGroup(%building.getTarget(),1);TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();%building.setSelfPowered();setTargetName(%building.target,addTaggedString("[ON]  Frequency" SPC %obj.powerFreq));%building.playThread($AmbientThread,"ambient");
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.66 9479.12 100.5";rotation = "0 0 1 123.326";scale = "0.125 0.166666 20";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8064.61 9491.78 100.5";rotation = "0 0 1 211.042";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8065.9 9491 108";rotation = "0.26759 0.963533 1.22137e-06 180";scale = "0.62472 0.166666 6.1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8074.3 9476.9 100.5";rotation = "0 0 -1 58.9579";scale = "3.9981 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8070.25 9488.4 104.45";rotation = "0.26759 0.963533 1.22137e-06 180";scale = "3.16162 0.166666 7.9";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedDecoration9";position = "8055.02 9500.28 100";rotation = "0 0 1 212.035";scale = "1 1 1";team = "1";ownerGUID = "2000343";needsfit = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8014.06 9472.58 102.289";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "3.86835 1.19267 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8078.1 9483.66 104.7";rotation = "0.258497 0.930784 0.258496 94.1066";scale = "0.125 0.166666 30.9974";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.52 9478.91 104.328";rotation = "-0.428806 0.795143 -0.428804 103.021";scale = "0.125 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.5 9478.92 105.191";rotation = "-0.428806 0.795143 -0.428804 103.021";scale = "0.30676 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.5 9478.92 107.445";rotation = "-0.428806 0.795143 -0.428804 103.021";scale = "0.247501 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8078.1 9483.66 108.25";rotation = "0.258497 0.930784 0.258496 94.1066";scale = "0.125 0.166666 30.9974";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.52 9478.91 108.19";rotation = "-0.428806 0.795143 -0.428804 103.021";scale = "0.125 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.5 9478.92 109.47";rotation = "-0.428806 0.795143 -0.428804 103.021";scale = "0.515005 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8074.85 9485.6 108";rotation = "0.26759 0.963533 1.22137e-06 180";scale = "1.89069 0.166666 6.1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedDecoration8";position = "8062 9501.65 100";rotation = "0 0 1 122.683";scale = "1 1 1";team = "1";ownerGUID = "2000343";needsfit = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.45 9479.26 104.338";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.125 0.166666 29.966";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.46 9479.28 105.164";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.28801 0.166666 29.966";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedDecoration6";position = "8057.43 9504.67 100";rotation = "0 0 1 212.667";scale = "1 1 1";team = "1";ownerGUID = "2000343";needsfit = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8057.43 9504.67 103.3";rotation = "0 0 1 218.336";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "7";set2 = "8";packBlock = "JavelinAmmo";};%building.setCloaked(true);TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.46 9479.28 107.492";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.250012 0.166666 29.966";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.45 9479.26 108.168";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.125 0.166666 29.966";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8009.88 9465.77 100.5";rotation = "0 0 1 123.326";scale = "0.125 0.166666 20";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8018.46 9479.28 109.459";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.520505 0.166666 29.966";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedDecoration7";position = "8053.24 9506.69 100";rotation = "0 0 -1 87.9301";scale = "1 1 1";team = "1";ownerGUID = "2000343";needsfit = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8010.9 9484.2 104.088";rotation = "0.286739 0.958009 1.21436e-06 180";scale = "2.9892 0.166666 7.176";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8038.76 9507.65 100.5";rotation = "0 0 1 118.629";scale = "0.125 0.166666 8";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8002.34 9482.02 104.308";rotation = "-0.795142 -0.428807 -0.428806 103.02";scale = "3.16805 0.166666 29.966";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8013.1 9482.46 108.168";rotation = "0.207056 0.691784 -0.691782 156.604";scale = "3.1244 0.166666 30.9484";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8014.48 9472.31 100.25";rotation = "-0.356303 0.660697 0.660702 140.777";scale = "4.11938 0.166666 31.9628";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8005.95 9468.65 100.751";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8005.59 9468.89 101.282";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8040.21 9510.3 100.5";rotation = "0 0 1 118.629";scale = "0.125 0.166666 7";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8035.37 9509.78 102.25";rotation = "-0.940626 0.240021 0.240026 93.5044";scale = "1.87402 1.16667 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8005.12 9469.2 101.766";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8038.54 9507.77 100.25";rotation = "-0.386926 0.65203 0.652032 137.695";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8038.54 9507.77 104.25";rotation = "0.240018 0.940629 0.240018 93.505";scale = "0.125 0.166666 14.9922";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8042.47 9514.45 100.25";rotation = "-0.454554 0.766004 -0.454553 105.096";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8004.81 9469.4 102.287";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8038.88 9507.87 104.25";rotation = "0.652032 0.386924 0.65203 137.695";scale = "0.125 0.166666 13.9883";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8004.55 9469.57 102.86";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8004.14 9469.84 103.319";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8032.61 9512.55 104.25";rotation = "-0.386919 0.652033 -0.652033 222.306";scale = "0.560377 0.166666 14.9913";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8003.76 9470.09 103.766";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8041.5 9512.15 102.25";rotation = "-0.386925 0.652028 0.652034 137.695";scale = "0.993457 1.16667 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8003.32 9470.38 103.817";rotation = "0.207055 0.691782 0.691785 203.397";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8006.27 9485.36 104.558";rotation = "0 0 1 158.069";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "6";set2 = "8";packBlock = "StingerAmmo";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8003.28 9469.81 102.284";rotation = "0.207056 0.691782 0.691784 203.397";scale = "3.74347 1.189 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8005.53 9484.17 104.558";rotation = "0 0 1 139.212";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "6";set2 = "8";packBlock = "StingerAmmo";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8009.67 9465.91 104.317";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.125 0.166666 29.9478";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8003.55 9470.24 106.257";rotation = "-0.920898 0.275631 0.275636 94.7163";scale = "3.74347 1.12667 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8031.74 9511.48 100.5";rotation = "0 0 1 208.629";scale = "0.125 0.166666 8";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8038.53 9516.02 100.25";rotation = "0.177564 0.695872 -0.695869 159.863";scale = "2.00136 0.166666 13.9883";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8004.56 9482.77 104.558";rotation = "0 0 1 133.509";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "6";set2 = "7";packBlock = "Stinger";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8009.67 9465.91 108.197";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.125 0.166666 29.9478";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8009.66 9465.89 109.473";rotation = "0.275632 0.920899 0.275631 94.7162";scale = "0.5135 0.166666 29.9478";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8003.95 9484.49 108.207";rotation = "-0.795142 -0.428807 -0.428806 103.02";scale = "1.66624 0.166666 5.0086";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8005.72 9487.63 100.5";rotation = "0 0 1 123.326";scale = "0.125 0.166666 20";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8042.35 9514.23 100.5";rotation = "0 0 1 118.629";scale = "0.125 0.166666 8";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8003.59 9481.59 104.558";rotation = "0 0 1 115.788";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "6";set2 = "7";packBlock = "Stinger";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.94 9481.41 107.582";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.67 9481 106.942";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.44 9480.66 106.439";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.24 9480.34 105.934";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.03 9480.02 105.333";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8000.17 9472.42 104.317";rotation = "0.207056 0.691784 0.691782 203.396";scale = "1.74176 0.166666 5.64626";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8000.76 9479.61 104.808";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.54 9480.82 102.279";rotation = "-0.356301 0.660698 0.660702 140.778";scale = "3.86835 1.186 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8033.32 9514.89 102.25";rotation = "-0.766004 -0.454555 -0.454552 105.095";scale = "1.74854 1.16667 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "7997.09 9474.49 104.308";rotation = "0.660701 0.356304 0.660698 140.778";scale = "0.125 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8035.21 9517.84 104.25";rotation = "-0.454554 0.766004 -0.454553 105.096";scale = "0.125 0.166666 13.9883";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8038.96 9516.36 102.25";rotation = "-0.940626 0.240021 0.240026 93.5044";scale = "1.87402 1.16667 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8001.13 9481.09 106.257";rotation = "-0.795143 -0.428806 -0.428804 103.02";scale = "3.86835 1.133 1";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8042.13 9514.35 100.25";rotation = "-0.386926 0.65203 0.652032 137.695";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8035.55 9517.94 104.25";rotation = "0.695871 -0.177567 0.695869 200.138";scale = "0.125 0.166666 14.9922";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "7997.09 9474.49 108.207";rotation = "0.660701 0.356304 0.660698 140.778";scale = "0.125 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "7997.1 9474.49 109.479";rotation = "0.660701 0.356304 0.660698 140.778";scale = "0.51075 0.166666 30.9468";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8035.33 9518.06 100.5";rotation = "0 0 1 208.629";scale = "0.125 0.166666 8";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "7996.95 9474.28 100.5";rotation = "0 0 1 123.326";scale = "0.125 0.166666 20";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8069.66 9516.38 100.5";rotation = "0 0 1 210.3";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8069.42 9516.52 102.432";rotation = "0.252855 0.933878 0.252855 93.9169";scale = "0.966 0.166666 29.9736";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8069.44 9516.51 108.25";rotation = "0.252855 0.933878 0.252855 93.9169";scale = "0.125 0.166666 29.9736";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8066.08 9518.75 104.114";rotation = "0.188038 0.694492 0.694494 201.3";scale = "2.02012 0.166666 5.3064";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8070.65 9518.4 100.5";rotation = "0 0 1 210.3";scale = "0.125 1.32505 6.804";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8056.28 9524.2 100.5";rotation = "0 0 1 210.3";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8059.29 9522.73 100.984";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8058.8 9523.01 101.584";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8058.44 9523.23 101.976";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8058.01 9523.48 102.559";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8057.61 9523.71 103.074";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8057.22 9523.94 103.552";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8056.88 9524.14 104.114";rotation = "0.188038 0.694492 0.694494 201.3";scale = "0.125 0.166666 4";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8075.11 9525.7 105.951";rotation = "-0.445587 0.77647 -0.445586 104.344";scale = "1.02451 0.166666 21.0544";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8080.18 9517 100.25";rotation = "0.776469 0.445585 -0.445589 104.344";scale = "1.05726 0.166666 17.0477";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8075.1 9525.68 108.25";rotation = "-0.445587 0.77647 -0.445586 104.344";scale = "0.125 0.166666 21.0544";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8072.66 9521.01 100.25";rotation = "-0.376007 0.655224 0.65521 138.786";scale = "2.88203 0.166666 32";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8059.92 9529.99 104.136";rotation = "-0.77647 -0.44559 -0.445583 104.344";scale = "1.97186 0.166666 30.018";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8056.4 9524.41 102.436";rotation = "0.65522 0.376005 0.655216 138.787";scale = "0.967755 0.166666 21.0376";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8061.71 9533.5 108.25";rotation = "-0.445587 0.77647 -0.445586 104.344";scale = "0.125 0.166666 21.0376";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8083.77 9506.81 100.25";rotation = "0.188039 0.694495 0.694491 201.299";scale = "4.11753 0.166666 31.9386";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8074.25 9524.2 100.5";rotation = "0 0 1 210.3";scale = "0.125 1.13073 6.804";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8063.7 9530.36 106.154";rotation = "0.694493 -0.188045 0.694492 201.299";scale = "0.625005 0.166666 23.8676";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DispenserDep";position = "8073.55 9525.75 104.386";rotation = "0 0 -1 56.6959";scale = "1.5 1.5 0.5";team = "1";ownerGUID = "2000343";powerFreq = "1";set1 = "7";set2 = "7";packBlock = "Javelin";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();schedule(100, 0, "respawnpack", %building);
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8088.71 9503.94 108.25";rotation = "0.188039 0.69449 0.694496 201.299";scale = "1.00168 0.166666 30.862";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8075.23 9525.9 100.5";rotation = "0 0 1 210.3";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8098.73 9516.6 104.5";rotation = "-0.445587 0.77647 -0.445586 104.344";scale = "2 0.166666 31.9658";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8075.02 9526.02 102.451";rotation = "0.252855 0.933878 0.252855 93.9169";scale = "0.975258 0.166666 29.9994";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8075.01 9526.03 108.25";rotation = "0.252855 0.933878 0.252855 93.9169";scale = "0.125 0.166666 29.9994";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8061.84 9533.72 100.5";rotation = "0 0 1 210.3";scale = "0.125 0.166666 16";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8083.5 9522.56 108.25";rotation = "0.376003 -0.655217 0.65522 138.786";scale = "0.998218 0.166666 23.8784";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      %building = new (StaticShape) () {datablock = "DeployedSpine";position = "8098.39 9516.47 104.5";rotation = "0.252855 0.933878 0.252855 93.9169";scale = "2 0.166666 31.9254";team = "1";ownerGUID = "2000343";needsfit = "1";powerFreq = "1";};TWM2MissionAspectsGroup.add(%building);%building.setSelfPowered();
      TWM2PowerCheck();
      //
   
      //Move The Players
      %sp = "8045 9471 105";
      for(%i = 1; %i <= %group.participants; %i++) {
         %spF = vectorAdd(%sp, TWM2Lib_MainControl("getRandomPosition", 5 TAB 1));
         %group.participant[%i].player.setPosition(%spF);
         //
         %player = %group.participant[%i].player;
         %player.setArmor("Medium"); //commando!
         %player.clearInventory();
         %player.setInventory(MissileLauncher, 1, true);
         %player.setInventory(MissileLauncherAmmo, 5, true);
         //%player.setInventory(pistol, 1, true);
         //%player.setInventory(pistolAmmo, 10, true);  //I'm nice for your first clip :)
         //%player.ClipCount[pistolImage.ClipName] = pistolImage.InitialClips;
         %player.ClipCount[StingerImage.ClipName] = 15;
         %player.ClipCount[JavelinImage.ClipName] = 15;
         //
         %player.setInventory(TargetingLaser, 1, true);
         %player.setInventory(RepairKit,3);
         %player.setInventory(Grenade,5);
         %player.use(MissileLauncher);
      }
      //Spawn The Evil AC-130 of doom
      %obj = new FlyingVehicle() {
         dataBlock    = AC130;
         position     = vectoradd(%missionPosCenter, "700 700 600");
         rotation     = "0 0 0 1";
         team         = 2;
      };
      TWM2MissionAspectsGroup.add(%obj);
      %obj.TurretObject.barrel = "Chain";
      SwitchACGunLoop(%obj); //randomly switch weapons
   
      ACAboveMissionLoop(%group, %obj);

      %obj.isHarbinsWrathShip = 1;
      %obj.isUltrAlly = 1; // ah what the heck, you should get 1000 XP for blowing one of these
                        // bastages up.

      setTargetSensorGroup(%obj.getTarget(), 2);

      %obj.GoPoint = 1;
      %obj.CanFlare = 1;
      GunshipForwardImpulse(%obj);
      %obj.ScanLoop = GetNextGunshipPoint(%obj);

      schedule(15*60*1000, 0, "EndGunship", %obj, "");
   }

   function ACAboveMissionLoop(%group, %obj) {
      if(!isObject(%obj)) {

         for(%i = 1; %i <= %group.participants; %i++) {
            AwardClient(%group.participant[%i], 34);
         }
   
         %group.CompleteMission();
         return;
      }
      schedule(1000, 0, "ACAboveMissionLoop", %group, %obj);
   }
};
