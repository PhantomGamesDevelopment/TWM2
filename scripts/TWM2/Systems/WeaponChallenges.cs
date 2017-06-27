//WEAPON CHALLENGES
//
//These are specific tasks you need to do with a certain weapon, each completed
//chalenge unlocks an xp award, along with a form of upgrade to your weapon
function GetWeaponChallenges(%Image, %number) {
   if(%Image.HasChallenges) {
      %taskName = getField(%Image.Challenge[%number], 0);
      %taskReq = getField(%Image.Challenge[%number], 1);
      %taskXPGive = getField(%Image.Challenge[%number], 2);
      %taskReward = getField(%Image.Challenge[%number], 3);
      return %TaskName TAB %taskReq TAB %taskXPGive TAB %taskReward;
   }
}

function UpdateVehicleKillFile(%client, %VDB) {
   if(%VDB $= "") {  //Phantom139: Ha!! No more invalid Files!!!
      //echo("No Vehicle Datablock");
      return;
   }
   if(%client $= "") {  //Phantom139: Ha! No More UE on Invalid File
      //echo("No Client");
      return;
   }
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   %scriptController = %client.TWM2Core;
   %scriptController.vehicleKills[%VDB]++;
   //%scriptController.save(%file);
   CheckForVComplete(%client, %VDB);
}

function UpdateWeaponKillFile(%client, %image) {
   if(%image $= "") {  //Phantom139: Ha!! No more invalid Files!!!
      //echo("");
      return;
   }
   if(%client $= "") {  //Phantom139: Ha! No More UE on Invalid File
      //echo("");
      return;
   }
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   %scriptController = %client.TWM2Core;
   %scriptController.weaponKills[%image]++;
   //%scriptController.save(%file);
   CheckForComplete(%client, %image);
}

function CheckForVComplete(%client, %VDB) {
   %kills = GetVKills(%client, %VDB);
   //Medals for Vehicles
   if(%VDB $= "HarbingerGunship") {
      if(%kills >= 250) {
         AwardClient(%client, "20");
      }
   }
   if(%VDB $= "AC130") {
      if(%kills >= 250) {
         AwardClient(%client, "26");
      }
   }
}

function CheckForComplete(%client, %image) {
   %kills = GetKills(%client, %image);
   //medals
   if(%image $= "rapierShieldImage") {
      if(%kills >= 25) {
         AwardClient(%client, 31);
      }
      return; //rapier Shield has no challenges or other stuff, stop here
   }
   if(%image $= "flamerImage") {
      if(%kills >= 250) {
         AwardClient(%client, "21");
      }
   }
   if(%image $= "RPGImage") {
      if(%kills >= 1 && ServerReturnMonthDate() $= "0704") {
         CompleteNWChallenge(%client, "IndepRPG");
      }
   }
   if(%image $= "JavelinImage") {
      if(%kills >= 1 && ServerReturnMonthDate() $= "1231") {
         CompleteNWChallenge(%client, "NewYearsEve");
      }
   }
   //first off, do we have any challenges?
   if(!%image.HasChallenges) {    //nope, stop here
      return;
   }
   else {
      %ct = %image.ChallengeCt; //get the amount
      //now the actual part
      for(%i = 1; %i <= %ct; %i++) {
         %ch[%i] = %image.Challenge[%i];
         %KillNeed[%i] = getField(%ch[%i], 1);
         //We meet this, award time
         if(%kills >= %KillNeed[%i]) {
            if(%client.CheckChallengeCompletion(%image, %i)) {
               //already given, continue
            }
            else {
               CompleteChallenge(%client, %i, %image);
               return;
            }
         }
      }
   }
}

function GetKills(%client, %image) {
   %scriptController = %client.TWM2Core;
   return %scriptController.weaponKills[%image];
}

function GetVKills(%client, %VDB) {
   %scriptController = %client.TWM2Core;
   return %scriptController.vehicleKills[%VDB];
}

function GameConnection::CheckChallengeCompletion(%client, %image, %num) {
   %scriptController = %client.TWM2Core;
   if(%scriptController.challengeComplete[%image, %num] == 1) {
      return true;
   }
   else {
      return false;
   }
}

function CompleteChallenge(%client, %num, %image) {
   if(%client.CheckChallengeCompletion(%image, %num)) {
      return;
   }
   %scriptController = %client.TWM2Core;
   //
   %taskName = getField(%Image.Challenge[%num], 0);
   %taskXPGive = getField(%Image.Challenge[%num], 2);
   %taskReward = getField(%Image.Challenge[%num], 3);
   //
   GainExperience(%client, %taskXPGive, "Challenge "@%taskName@" Completed ");
   BottomPrint(%client, "CHALLEGNE COMPLETE: "@%taskName@" \n +"@%taskXPGive@"XP, Reward: "@%taskReward@"", 2, 3);
   MessageClient(%client, 'MsgSound', "~wfx/Bonuses/Nouns/General.wav");
   MessageAll('msgComplete', "\c5"@%client.namebase@" completed weapon challenge "@%taskName@"");
   //
   %scriptController.challengeComplete[%image, %num] = 1;
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   //%scriptController.save(%file);
   SaveClientFile(%client);
   echo("TWM2: Client "@%client@", "@%client.nambase@", Completed Challenge "@%taskname@", File Updated.");
}

function GenerateCompletedChallegnesMenu(%client, %tag, %index) {
   %count = DatablockGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %db = DatablockGroup.GetObject(%i);
      if(%db.getName().getClassname() $= "ItemData") {
         if(%db.getName().classname $= "Weapon") {
            %Image = %db.getName().image;
            if(%Image.HasChallenges) {
               if(DoMedalCheck(%client, %image) == 1 && CanUseRankedWeapon(%image, %client) == 1) {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tCompletedSub\t"@%Image@"\t1>"@%Image.GunName@"</a>");
                  %index++;
               }
               else {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "Weapon Locked");
                  %index++;
               }
            }
         }
      }
   }
   return %index;
}

function GenerateWeaponChallegnesMenu(%client, %tag, %index) {
   %count = DatablockGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %db = DatablockGroup.GetObject(%i);
      if(%db.getName().getClassname() $= "ItemData") {
         if(%db.getName().classname $= "Weapon") {
            %Image = %db.getName().image;
            if(%Image.HasChallenges) {
               %kills = GetKills(%client, %image);
               if(%kills $= "") {
                  %kills = 0;
               }
               if(DoMedalCheck(%client, %image) == 1 && CanUseRankedWeapon(%image, %client) == 1) {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tWeaponTasksSub\t"@%Image@"\t1>"@%Image.GunName@"</a> - Kills: "@%kills@"");
                  %index++;
               }
               else {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "Weapon Locked");
                  %index++;
               }
            }
         }
      }
   }
   return %index;
}

//SUB MENU        GetWeaponChallenges(%Image, %number)
function GenerateCompletedSubMenu(%client, %tag, %index, %image) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tDeActivateUpgrades\t"@%Image@"\t1>-Disable All</a>");
   %index++;
   for(%i = 1; %i <= %Image.ChallengeCt; %i++) {
      if(%client.CheckChallengeCompletion(%image, %i)) {
         for(%j = 1; %j <= %Image.ChallengeCt; %j++) {
            if(%Image.Upgrade[%j] !$= "") {
               if(getField(GetWeaponChallenges(%Image, %i), 3) $= %Image.Upgrade[%j]) {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tActivateUpgrade\t"@%Image@"\t"@%Image.Upgrade[%j]@"\t1>-UPGRADE: "@%Image.Upgrade[%j]@"</a>");
                  %index++;
               }
            }
         }
      }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   return %index;
}

function GenerateWChallengeSubMenu(%client, %tag, %index, %image) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%image.gunname@" - Kills: "@GetKills(%client, %image)@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   for(%i = 1; %i <= %Image.ChallengeCt; %i++) {
      %Field = GetWeaponChallenges(%image, %i);
      //
      %taskName = getField(%Field, 0);
      %taskReq = getField(%Field, 1);
      //
      if(!%client.CheckChallengeCompletion(%image, %i)) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%taskName@" - Earn "@%taskReq@" Kills with this weapon");
         %index++;
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>"@%taskName@"</color> - Earn "@%taskReq@" Kills with this weapon");
         %index++;
      }
   }
   return %index;
}


//UPGRADES
datablock TargetProjectileData(PointerLaser) {
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "1.0 0.0 0.0";

   startBeamWidth			= 0.02; //0.02
   pulseBeamWidth 	   = 0.025; //0.025
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 400.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "special/expFlare";
   beacon               = true;
};

function checkstatusonlaser(%player) {
   if(%player.getState() $= "Dead" || !%player || !isObject(%player)) {
      if(isObject(%player.LTGT)) {
         %player.LTGT.delete();
      }
      return;
   }
   schedule(1000,0,"checkstatusonlaser",%player);
}

function GameConnection::UpgradeOn(%client, %upgrade, %image) {
   if(%client.Upgrade[%upgrade, %image] == 1) {
      return true;
   }
   else {
      return false;
   }
}

//Handy Function, I love thee
function GameConnection::DisableAllUpgrades(%client, %image) {
   %x = 1;
   if(%image $= "PulsePhaserImage") {
      %client.player.unmountImage(5);
      %client.player.unmountImage(6);
   }
   if(%image $= "PistolImage") {
      %client.player.unmountImage(5);
   }
   while(%image.Upgrade[%x] !$= "") {
      if(isObject(%client.player.LTGT)) {
         %client.player.LTGT.delete();
      }
      %client.Upgrade[%image.Upgrade[%x], %image] = 0;
      %x++;
   }
}

function GameConnection::ActivateUpgrade(%client, %image, %upgrade) {
   %client.Upgrade[%upgrade, %image] = 1;
   %client.weaponUpgrade[%image] = %upgrade;
   if(isObject(%client.player) && %client.player.getState() !$= "dead") {
      %CurImage = %client.player.getMountedImage($WeaponSlot).getName();
      if(%CurImage $= %image) {
         if(%upgrade $= "Laser") {
            %t = new TargetProjectile() {
                dataBlock        = "PointerLaser";
                initialDirection = %client.player.getMuzzleVector($WeaponSlot);
                initialPosition  = %client.player.getMuzzlePoint($WeaponSlot);   //Helmet Battery
                sourceObject     = %client.player;
                sourceSlot       = 0;  //from ze gun?
                vehicleObject    = 0;
            };
            MissionCleanup.add(%t);
            %client.player.LTGT = %t;
            checkstatusonlaser(%client.player);
         }
         else if(%upgrade $= "Phaser Blades") {
            %client.player.mountImage(Phaser2Image, 5);
            %client.player.mountImage(Phaser3Image, 6);
         }
         else if(%upgrade $= "Excessive Duality") {
            %client.player.mountImage(PistolImage2, 5);
         }
      }
   }
}

function PerformUpgradeCheck(%img, %plyr) {
   %x = 1;
   while(%img.Upgrade[%x] !$= "") {
      if(%plyr.client.UpgradeOn(%img.Upgrade[%x], %img)) {
         %plyr.client.ActivateUpgrade(%img, %img.Upgrade[%x]);
      }
      %x++;
   }
}

function PerformUpgradeDisable(%img, %plyr) {
   //echo(%img);
   if(%img $= "PulsePhaserImage") {
      %plyr.unmountImage(5);
      %plyr.unmountImage(6);
   }
   if(%img $= "PistolImage") {
      %plyr.unmountImage(5);
   }
   if(isObject(%plyr.LTGT)) {
      %plyr.LTGT.delete();
   }
}
