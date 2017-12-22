//
function CanUseWeapon(%WImg, %client) {
	if(%client.isHarb || %client.isAIControlled()) {
		return 1;
	}
	//
	if(!$TWM2::AllowSnipers) {
		if(strstr(%WImg.getName(), "SniperRifle") != -1) {
			return 0;
		}
	}
	%clientController = %client.TWM2Core;
	%WImg2 = %WImg.getName();   
	//Medal Tests
	if(%WImg2.MedalRequire) {
		if(!%client.hasMedal(%WImg2.MedalRequire)) {
			//Deny access by medal.
			return 3;
		}
	}
	//Challenge Tests
	if(%WImg2.ChallengeRequire !$= "") {
		if(!%client.CheckNWChallengeCompletion(%WImg2.ChallengeRequire)) {
			//Deny access by challenge.
			return 4;
		}
	}
	//Rank Tests
	if($Host::RankSystem == 0) {
		return 1;
	}
	else {
		//PRESTIGE CHECKS (added 2.4)
		if(%WImg2.PrestigeRequire $= "") {
			if(%WImg2.RankRequire $= "") {
				return 1;
			}
			else {
				%xpHas = getCurrentEXP(%client);
				%xpNeed = $Ranks::MinPoints[%WImg2.RankRequire];
				if(%xpNeed > %xpHas || (isSet(%WImg2.RankRequire) ? %clientController.rankNumber < %WImg2.RankRequire : false )) {
					return 0;
				}
				else {
					return 1;
				}
			}
		}
		else {
			%prestige = %clientController.officer;
			%prestigeNeed = %WImg2.PrestigeRequire;
			if(%prestige < %prestigeNeed) {
				return 2; //new case check
			}
			else {
				if(%WImg2.RankRequire $= "") {
					return 1;
				}
				else {
					%xpHas = getCurrentEXP(%client);
					%xpNeed = $Ranks::MinPoints[%WImg2.RankRequire];
					if(%xpNeed > %xpHas || (isSet(%WImg2.RankRequire) ? %clientController.rankNumber < %WImg2.RankRequire: false )) {
						return 0;
					}
					else {
						return 1;
					}
				}
			}
		}
	}
}

function PerformWeaponMountChecks(%WImg, %Plyr, %slot) {
	if(%Plyr.isZombie || %Plyr.isSoldier || %Plyr.client.isAiControlled()) {
		return; //Wraith AI
	}
	%client = %plyr.client;
	%clientController = %client.TWM2Core;
	//Perform Rank Checks
	%xpHas = getCurrentEXP(%Plyr.client);
	%xpNeed = $Ranks::MinPoints[%WImg.RankRequire];
	%prestigeNeed = %WImg.PrestigeRequire;
	
	%hideReason = %WImg.hideExistence;
	
	%weaponTestReturnCode = CanUseWeapon(%WImg, %Plyr.client);
	if(%weaponTestReturnCode != 1) {
		%Plyr.use(%WImg.Item);
		%Plyr.throwweapon(1);
		%Plyr.throwweapon(0);
		switch(%weaponTestReturnCode) {
			//Return Code 0: XP Too Low
			case 0:
				if(%hideReason == true) {
					BottomPrint(%Plyr.client, "Weapon Access Denied\nYou cannot use this weapon\nFurther information is classified", 3, 3);		
				}
				else {
					BottomPrint(%Plyr.client, "Weapon Access Denied\nRequired Rank\nYou need: "@%xpNeed - %xpHas@" More XP to use this weapon", 3, 3);			
				}
				break;
			
			//Return Code 2: Officer Level Too Low
			case 2:
				if(%hideReason == true) {
					BottomPrint(%Plyr.client, "Weapon Access Denied\nYou cannot use this weapon\nFurther information is classified", 3, 3);		
				}
				else {			
					BottomPrint(%Plyr.client, "Weapon Access Denied\nOfficer Level Required\nYou need Officer Level: "@%prestigeNeed@" to use this weapon", 3, 3);
				}
				break;
			
			//Return Code 3: Player does not have required medal
			case 3:
				if(%hideReason == true) {
					BottomPrint(%Plyr.client, "Weapon Access Denied\nYou cannot use this weapon\nFurther information is classified", 3, 3);		
				}
				else {			
					BottomPrint(%Plyr.client, "Weapon Access Denied\nMedal Required\nYou do not own the '"@$MedalName[%WImg.MedalRequire]@"' Medal", 3, 3);
				}
				break;			
			
			//Return Code 4: Player does not have required challenge
			case 4:
				if(%hideReason == true) {
					BottomPrint(%Plyr.client, "Weapon Access Denied\nYou cannot use this weapon\nFurther information is classified", 3, 3);		
				}
				else {			
					BottomPrint(%Plyr.client, "Weapon Access Denied\nChallenge Required\nYou do not own the '"@getField($Challenge::Info[%WImg.ChallengeRequire], 0)@"' Medal", 3, 3);
				}
				break;
		}
	}
}

function AttemptReload(%WImg, %Plyr, %slot) {
   if(!isPlayer(%Plyr)) {
      return;
   }
   if(!isObject(%Plyr) || %Plyr.getState() $= "dead") {
      return;
   }
   %client = %Plyr.client;
   if(%Plyr.Reloading[%WImg]) {     //no double reloads :P
      return;
   }
   if(%WImg.ClipName $= "") {
      return;
   }
   %ClipsLeft = %plyr.ClipCount[%WImg.ClipName];
   if(%ClipsLeft == 0) {
      BottomPrint(%client, "Out Of Ammo", 2, 1);
   }
   else {
      %Plyr.setInventory(%WImg.ammo, 0, true);
      if(%ClipsLeft >= 900) {
         //Helljump mod
         if($TWM2::PlayingHelljump) {
            %plyr.ClipCount[%WImg.ClipName] = %plyr.HellClipCount[%WImg.ClipName];
            %plyr.ClipCount[%WImg.ClipName]--;
         }
         else {

         }
//         echo(""@%plyr.client@" reloading unlimited clip gun");
         //Unlimited Clip Guns
         //Pistols, Ect.
      }
      else {
         %plyr.ClipCount[%WImg.ClipName]--;
      }
      %Plyr.Reloading[%WImg] = 1;
      if(%client.IsActivePerk("Clip Boxes")) {
         %ReloadTime = %WImg.ClipReloadTime - (%WImg.ClipReloadTime * 0.33);
      }
      else if(%client.IsActivePerk("Double Time")) {
         %ReloadTime = %WImg.ClipReloadTime - (%WImg.ClipReloadTime * 0.5);
      }
      else if(%client.IsActivePerk("Ammo Vet")) {
         %ReloadTime = %WImg.ClipReloadTime - (%WImg.ClipReloadTime * 0.75);
      }
      else {
         %ReloadTime = %WImg.ClipReloadTime;
      }
      if(%WImg.ReloadSingle) {   //1 reload at a time
         bottomPrint(%client, "<just:center><color:ffffff>Reloading: "@%WImg.GunName@" \n Clips Left: "@%plyr.ClipCount[%WImg.ClipName]@"", 2, 2);
         %CTR = %WImg.ClipReturn;
         %SPR = %WImg.SingleShotAdd;
         %TimeInMS = MFloor((%ReloadTime / MFloor(%CTR/%SPR)) * 1000);
         for(%i = 1; %i <= MFloor(%CTR/%SPR); %i++) {
            %time = %TimeInMS * %i;
            schedule(%time, 0, "AddShot", %WImg, %Plyr, %time, %i, %CTR, %SPR, %TimeInMS);
         }
      }
      else {
         //2.6 -> Reload Bar
         //2.9 -> Updated with this function :)
         %Plyr.ReloadBar[%WImg] = DoReloadBar(%Plyr, %WImg, %ReloadTime, 0);
         //
         schedule(%ReloadTime * 1000, 0, "ReloadWeapon", %WImg, %Plyr);
      }
   }
}

function DoReloadBar(%Plyr, %WImg, %ReloadTime, %MSTime) {
   if(%Plyr.Inv[%WImg.ammo] >= %WImg.ClipReturn) {
      ClearReloadVar(%Plyr, %WImg);
      Cancel(%Plyr.ReloadBar[%WImg]);
      return;
   }
   if(!%Plyr.Reloading[%WImg]) {
      //we obviously need to be reloading it :D
      Cancel(%Plyr.ReloadBar[%WImg]);
      return;
   }
   %client = %Plyr.client;
   //
   %next = %MSTime + 0.125;
   if(%next <= 1) {
      %time = ((%reloadTime * %next)*1000 - (%reloadTime * (%next-0.125))*1000);
      //Calculate the reload Bar
      %max_Bars = 25;
      //Remember %next is actually a percentage
      %barCount = MFloor(%max_Bars * %next);
      %SpaceCount = MFloor(%max_Bars * (1-%next));
      %string = "";
      %Spaced = "";
      //make the spaces
      for(%s = 0; %s < MFloor(%SpaceCount/2); %s++) {
         %Spaced = %Spaced SPC "";
      }
      //make the bar string
      for(%i = 0; %i < %barCount; %i++) {
         %string = ""@%string@"|";
      }
      if(%next <= 0.375) {
         %color = "<color:EE0000>";
      }
      else if(%next > 0.375 && %next <= 0.75) {
         %color = "<color:FFD700>";
      }
      else {
         %color = "<color:00CD00>";
      }
      //make the bottom print
      bottomPrint(%client, "<just:center><color:ffffff>Reloading: "@%WImg.GunName@" \n ["@%color@""@%Spaced@""@%string@""@%Spaced@"<color:ffffff>]("@%next*100@"%)", 2, 2);
      //
      %Plyr.ReloadBar[%WImg] = schedule(%time, 0, DoReloadBar, %Plyr, %WImg, %ReloadTime, %next);
   }
}

function ClearReloadVar(%Plyr, %WImg) {
   %Plyr.Reloading[%WImg] = 0;
}

function AddShot(%WImg, %Plyr, %time, %i, %CTR, %SPR, %TimeInMS) {
   //echo(%WImg SPC %Plyr SPC %time SPC %i SPC %CTR SPC %SPR SPC %TimeInMS);
   if(!isObject(%Plyr) || %Plyr.getState() $= "dead") {
      return;
   }
   if(%Plyr.Inv[%WImg.ammo] >= %WImg.ClipReturn) {
      ClearReloadVar(%Plyr, %WImg);
      return;
   }
   if(!%Plyr.Reloading[%WImg]) {
      //we obviously need to be reloading it :D
      return;
   }
   %ammoGiven = %WImg.SingleShotAdd;
   %Plyr.setInventory(%WImg.ammo, (%Plyr.Inv[%WImg.ammo] + %ammoGiven), true);
   %client = %Plyr.client;
   BottomPrint(%client, "<just:center><color:ffffff>Reloading "@%WImg.GunName@" \n ["@((%i/MFloor(%CTR/%SPR))*100)@"% /100% Reloaded]", 1, 2);
   if(%i == MFloor(%CTR/%SPR)) {
      ClearReloadVar(%Plyr, %WImg);
   }
}

function ReloadWeapon(%WImg, %Plyr) {
   if(!isObject(%Plyr) || %Plyr.getState() $= "dead") {
      return;
   }
   if(%Plyr.Inv[%WImg.ammo] >= %WImg.ClipReturn) {
      ClearReloadVar(%Plyr, %WImg);
      return;
   }
   if(!%Plyr.Reloading[%WImg]) {
      //we obviously need to be reloading it :D
      return;
   }
   %client = %Plyr.client;
   %ammoGiven = %WImg.ClipReturn;
   %Plyr.setInventory(%WImg.ammo, %ammoGiven, true);
   %Plyr.Reloading[%WImg] = 0;
   //
   if(%plyr.ClipCount[%WImg.ClipName] !$= "") {
      if(%plyr.ClipCount[%WImg.ClipName] >= 900) {
         bottomPrint(%client, "<just:center><color:ffffff>Weapon Reloaded: "@%WImg.GunName@"", 2, 1);
      }
      else {
         bottomPrint(%client, "<just:center><color:ffffff>Weapon Reloaded: "@%WImg.GunName@" \n Clips Left: "@%plyr.ClipCount[%WImg.ClipName]@"", 2, 2);
      }
   }
}

datablock ItemData(ClipBox) {
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "A clip box";

   computeCRC = true;

};

function SpawnClipBox(%pos, %type, %ct) {
   %clips = new Item() {
      dataBlock = ClipBox;
      static = false;
      rotate = false;
   };
   %clips.ClipsGive = %type;
   %clips.returnammount = %ct;
   %clips.setPosition(%pos);
}

function ClipBox::onCollision(%data, %obj, %col) {
   if(!%col.isPlayer() || %col.isZombie) {
      return;
   }
   serverPlay3D(ItemPickupSound, %col.getTransform());
   messageClient(%col.client, 'Msgclient', "\c0Picked up a clip box of "@%obj.ClipsGive.clipName@"");
   //
   %col.ClipCount[%obj.ClipsGive.clipName] += %obj.returnammount;
   //check for reload.
   if(!%obj.ClipsGive.usesEnergy) {
      if(!isSet(%col.getInventory(%obj.ClipsGive.ammo)) || %col.getInventory(%obj.ClipsGive.ammo) <= 0) {
         //AttemptReload(%obj.ClipsGive, %col, 0);
         %col.setInventory(%obj.ClipsGive.ammo, 1, true);
         //ReloadWeapon(, %col);
      }
   }
   %obj.delete();
}

function WeaponDrop(%pos, %type) {
   SpawnClipBox(%pos, %type.Image, 4);
   %gun = new Item() {
      dataBlock = %type;
      static = false;
      rotate = false;
      position = %pos;
   };
   %ammo = new Item() {
      dataBlock = %type.Image.ammo;
      static = false;
      rotate = false;
      position = %pos;
   };
}
