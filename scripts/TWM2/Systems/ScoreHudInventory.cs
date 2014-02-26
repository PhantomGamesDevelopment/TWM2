//ScoreHudInventroy.cs
//Phantom139

//For TWM2, and other mods, A rework of the inventory system to be ran through the
//score menu, for easier use and classification of weapons, packs, and armors

//one can simply disable it through this G-Var
//$ScoreHudInventory::Active = 1;

package scoreHudInventory {
   function DefaultGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5) {
      %tag = $TagToUseForScoreMenu;
      messageClient( %client, 'ClearHud', "", %tag, 1 );
      //
      switch$(%arg1) {
         case "inventoryWindow":
            %index = buildInventoryWindow(%client, %tag, %index);
            return;
            
         case "setScoreInv":
            %subZone = %arg2;
            switch$(%subZone) {
               case "Armor":
                  %client.scoreHudInv[Armor] = %arg3;
               case "Weapon":
                  //pull the current settings
                  %int = 1;
                  while(isSet(%client.scoreHudInv[Weapon, %int])) {
                     if(%client.scoreHudInv[Weapon, %int] $= %client.scoreHudInv[Weapon, %arg3]) {
                        %client.scoreHudInv[Weapon, %int] = "";
                     }
                     %int++;
                  }
                  %slot = %arg3;
                  %client.scoreHudInv[Weapon, %slot] = %arg4;
                  //now do a post set check
                  %xSlot = 1;
                  while(isSet(%client.scoreHudInv[Weapon, %xSlot])) {
                     //no two may co-exist, IE: be the same
                     %iSlot = 1;
                     while(isSet(%client.scoreHudInv[Weapon, %iSlot])) {
                        if(%client.scoreHudInv[Weapon, %iSlot] $= %client.scoreHudInv[Weapon, %xSlot]) {
                           if(%iSlot != %xSlot) {
                              //remove iSlot, proceed
                              %client.scoreHudInv[Weapon, %iSlot] = "";
                           }
                        }
                        %iSlot++;
                     }
                     %xSlot++;
                  }
               case "Pistol":
                  %client.scoreHudInv[Pistol] = %arg3;
               case "Melee":
                  %client.scoreHudInv[Melee] = %arg3;
               case "Pack":
                  %client.scoreHudInv[Pack] = %arg3;
               case "Grenade":
                  %client.scoreHudInv[Grenade] = %arg3;
               case "Mine":
                  %client.scoreHudInv[Mine] = %arg3;
               case "Ability":
                  %client.scoreHudInv[Ability] = %arg3;
               default:
                  error("Unknown Call to setScoreInv: "@%arg2@"/"@%arg3@"/"@%arg4@"");
               //
            }
            Game.processGameLink(%client, "inventoryWindow");
            return;
      }
      parent::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5);
   }
   
   function pushScoreInventory(%client) {
      messageClient(%client, 'OpenHud', "", 'scoreScreen' SPC "inventoryWindow");
      messageClient(%client, 'CloseHud', "", 'inventoryScreen' SPC "inventoryScreen");
      Game.processGameLink(%client, "inventoryWindow");
   }
   
   function InventoryScreen::updateHud( %this, %client, %tag ) {
      if($ScoreHudInventory::Active == 1) {
         pushScoreInventory(%client);
         return;
      }
      else {
         parent::updateHud( %this, %client, %tag );
      }
   }
   
   function GameConnection::onConnect( %client, %name, %raceGender, %skin, %voice, %voicePitch ) {
      Parent::onConnect( %client, %name, %raceGender, %skin, %voice, %voicePitch );
      %client.scoreHudInv[Armor] = "Purebuild";
   }
   
   //modification to the game's inventory buy functions
   function buyFavorites(%client) {
      if($ScoreHudInventory::Active == 1) {
         if(!isObject(%client.player)) {
            return;
         }
         if(%client.player.isZombie) {
            return;
         }
         if (%client.isJailed) {
		    return;
         }
	     if (!%client.isAdmin && !%client.isSuperAdmin) {
	        if ($Host::Purebuild == 1) {
			   %client.scoreHudInv[Armor] = "Purebuild";
            }
		    else {
			   if (%client.scoreHudInv[Armor] $= "Purebuild") {
				  %client.scoreHudInv[Armor] = "Scout";
               }
		    }
	     }
         // don't forget -- for many functions, anything done here also needs to be done
         // below in buyDeployableFavorites !!!
         %client.player.clearInventory();
         %client.setWeaponsHudClearAll();
         %cmt = $CurrentMissionType;

         %curArmor = %client.player.getDatablock();
         %curDmgPct = getDamagePercent(%curArmor.maxDamage, %client.player.getDamageLevel());

         // armor
         %client.armor = $NameToInv[%client.scoreHudInv[Armor]];
         %client.player.setArmor( %client.armor );
         %newArmor = %client.player.getDataBlock();

         %client.player.setDamageLevel(%curDmgPct * %newArmor.maxDamage);
         %weaponCount = 0;

         DoPerksStuff(%client, %client.player);
         //

         // weapons
         %armor = getArmorDatablock(%client, $NameToInv[%client.scoreHudInv[Armor]]);
         %slotCount = %armor.MaxWeapons;
         if(%client.IsActivePerk("OverKill") == 1) {
            %slotCount++;
         }
         for(%i = 1; %i <= %slotCount; %i++) {
            %inv = $NameToInv[%client.scoreHudInv[Weapon, %i]];

            if( %inv !$= "" ) {
               %weaponCount++;
               %client.player.setInventory( %inv, 1 );
               %WImg = %inv.Image;
               if(%WImg.ClipName !$= "") { //apply clips
                  if(%client.IsActivePerk("Bandolier")) {
                     %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips * 2;
                  }
                  else {
                     %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips;
                  }
               }
            }
            // z0dd - ZOD, 9/13/02. Streamlining.
            if ( %inv.image.ammo !$= "" ) {
               %client.player.setInventory( %inv.image.ammo, 400 );
            }
         }
         %client.player.weaponCount = %weaponCount;
         //
         
         //pistol
         %Pinv = $NameToInv[%client.scoreHudInv[Pistol]];

         if( %Pinv !$= "" ) {
            %client.player.setInventory( %Pinv, 1 );
            %WImg = %Pinv.Image;
            if(%WImg.ClipName !$= "") { //apply clips
               if(%client.IsActivePerk("Bandolier")) {
                  %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips * 2;
               }
               else {
                  %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips;
               }
            }
         }
         if ( %Pinv.image.ammo !$= "" ) {
            %client.player.setInventory( %Pinv.image.ammo, 400 );
         }
         
         //melee
         %meleeinv = $NameToInv[%client.scoreHudInv[Melee]];

         if( %meleeinv !$= "" ) {
            %client.player.setInventory( %meleeinv, 1 );
         }
         
         //pack/deployable/ect/you get the point :P
         %pCh = $NameToInv[%client.scoreHudInv[Pack]];
         if(%pCh !$= "") {
            %client.player.setInventory( %pCh, 1 );
            // if this pack is a deployable that has a team limit, warn the purchaser
            // if it's a deployable turret, the limit depends on the number of players (deployables.cs)
	        if (isDeployableTurret(%pCh)) {
		       %maxDep = countTurretsAllowed(%pCh);
            }
            else {
		       %maxDep = $TeamDeployableMax[%pCh];
            }
	        if(%maxDep !$= "") {
		       %depSoFar = $TeamDeployedCount[%client.player.team, %pCh];
		       %packName = %client.favorites[%client.packIndex];

		       if(Game.numTeams > 1) {
                  %msTxt = "Your team has "@%depSoFar@" of "@%maxDep SPC %packName@"s deployed.";
               }
		       else {
			      %msTxt = "You have deployed "@%depSoFar@" of "@%maxDep SPC %packName@"s.";
               }
		       messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
            }
         }
         //
         
         //Grenade
         %nadeinv = $NameToInv[%client.scoreHudInv[Grenade]];

         if( %nadeinv !$= "" ) {
            %client.player.setInventory( %nadeinv, %armor.max[%nadeinv] );
         }
         
         //Mine
         %Mineinv = $NameToInv[%client.scoreHudInv[Mine]];

         if( %Mineinv !$= "" ) {
            %client.player.setInventory( %Mineinv, %armor.max[%Mineinv] );
         }
         
         //
         // miscellaneous stuff -- Repair Kit, Beacons, Targeting Laser
         if ( !($InvBanList[%cmt, RepairKit]) )
            %client.player.setInventory( RepairKit, 1 );
         if ( !($InvBanList[%cmt, Beacon]) )
            %client.player.setInventory( Beacon, 400 );
         if ( !($InvBanList[%cmt, TargetingLaser]) )
            %client.player.setInventory( TargetingLaser, 1 );

         // ammo pack pass -- hack! hack!
         if( %pCh $= "AmmoPack" ) {
            invAmmoPackPass(%client);
         }
         // give admins the Super Chaingun
         GiveTWM2Weapons(%client); //includes SW's and admin stuff
         // TODO - temporary - remove
	     if (%client.forceArmor !$= "") {
		    %client.player.setArmor(%client.forceArmor);
         }
      }
      else {
         parent::buyFavorites(%client);
      }
   }
   
   function buyDeployableFavorites(%client) {
      if($ScoreHudInventory::Active == 1) {
         if(%client.player.isZombie) {
            return;
         }
	     if (%client.isJailed) {
            return;
         }
	     if (!%client.isAdmin && !%client.isSuperAdmin) {
            if ($Host::Purebuild == 1) {
               %client.scoreHudInv[Armor] = "Purebuild";
            }
            else {
               if (%client.scoreHudInv[Armor] $= "Purebuild") {
			      %client.scoreHudInv[Armor] = "Scout";
               }
		    }
         }
         %player = %client.player;
         %prevPack = %player.getMountedImage($BackpackSlot);
         %player.clearInventory();
         %client.setWeaponsHudClearAll();
         %cmt = $CurrentMissionType;

         // players cannot buy armor from deployable inventory stations
         %armor = getArmorDatablock(%client, $NameToInv[%client.scoreHudInv[Armor]]);
         %slotCount = %armor.MaxWeapons;
         if(%client.IsActivePerk("OverKill") == 1) {
            %slotCount++;
         }
         %weapCount = 0;
         for(%i = 1; %i <= %slotCount; %i++) {
            %inv = $NameToInv[%client.scoreHudInv[Weapon, %i]];
            if (!($InvBanList[DeployInv, %inv])) {
               %player.setInventory( %inv, 1 );
               // increment weapon count if current armor can hold this weapon
               if(%player.getDatablock().max[%inv] > 0) {
                  %weapCount++;
               }

               %WImg = %inv.Image;
               if(%WImg.ClipName !$= "") { //apply clips
                  if(%client.IsActivePerk("Bandolier")) {
                     %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips * 2;
                  }
                  else {
                     %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips;
                  }
               }

               // z0dd - ZOD, 9/13/02. Streamlining
               if ( %inv.image.ammo !$= "" ) {
                  %player.setInventory( %inv.image.ammo, 400 );
               }

               if(%weapCount >= %player.getDatablock().maxWeapons) {
                  break;
               }
            }
         }
         %player.weaponCount = %weapCount;
         //Update Pistol
         %inv = $NameToInv[%client.scoreHudInv[Pistol]];
         if (!($InvBanList[DeployInv, %inv]) ) {
            %player.setInventory( %inv, 1 );

            %WImg = %inv.Image;
            if(%WImg.ClipName !$= "") { //apply clips
               if(%client.IsActivePerk("Bandolier")) {
                  %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips * 2;
               }
               else {
                  %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips;
               }
            }

            // z0dd - ZOD, 9/13/02. Streamlining
            if ( %inv.image.ammo !$= "" ) {
               %player.setInventory( %inv.image.ammo, 400 );
            }
         }
         //Update Melee
         %meleeinv = $NameToInv[%client.scoreHudInv[Melee]];
         if ( !($InvBanList[DeployInv, %meleeinv]) ) {
            %player.setInventory( %meleeinv, 1 );
            // z0dd - ZOD, 9/13/02. Streamlining
            if ( %meleeinv.image.ammo !$= "" ) {
               %player.setInventory( %meleeinv.image.ammo, 400 );
            }
         }
         // give player the grenades and mines they chose, beacons, and a repair kit
         %nadeinv = $NameToInv[%client.scoreHudInv[Grenade]];
         if ( !($InvBanList[DeployInv, %nadeinv]) ) {
            %player.setInventory( %nadeinv, 30 );
         }
         %mineinv = $NameToInv[%client.scoreHudInv[Mine]];
         if ( !($InvBanList[DeployInv, %mineinv]) ) {
            %player.setInventory( %mineinv, 30 );
         }
         
         if ( !($InvBanList[DeployInv, Beacon]) && !($InvBanList[%cmt, Beacon]) )
            %player.setInventory( Beacon, 400 );
         if ( !($InvBanList[DeployInv, RepairKit]) && !($InvBanList[%cmt, RepairKit]) )
            %player.setInventory( RepairKit, 1 );
         if ( !($InvBanList[DeployInv, TargetingLaser]) && !($InvBanList[%cmt, TargetingLaser]) )
            %player.setInventory( TargetingLaser, 1 );

	     // pack - any changes here must be added to dep below!
	     // players cannot buy deployable station packs from a deployable inventory station
	     %packChoice = $NameToInv[%client.scoreHudInv[Pack]];
	     if ( !($InvBanList[DeployInv, %packChoice]) )
	        %player.setInventory( %packChoice, 1 );

	     // if this pack is a deployable that has a team limit, warn the purchaser
	     // if it's a deployable turret, the limit depends on the number of players (deployables.cs)
	     if (isDeployableTurret(%packChoice)) {
		    %maxDep = countTurretsAllowed(%packChoice);
         }
	     else {
		    %maxDep = $TeamDeployableMax[%packChoice];
         }
	     if((%maxDep !$= "") && (%packChoice !$= "InventoryDeployable")) {
		    %depSoFar = $TeamDeployedCount[%client.player.team, %packChoice];
		    %packName = %client.favorites[%client.packIndex];

		    if(Game.numTeams > 1) {
               %msTxt = "Your team has "@%depSoFar@" of "@%maxDep SPC %packName@"s deployed.";
            }
		    else {
			   %msTxt = "You have deployed "@%depSoFar@" of "@%maxDep SPC %packName@"s.";
            }
		    messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
         }
         if(%packChoice $= "AmmoPack") {
            invAmmoPackPass(%client);
         }
         GiveTWM2Weapons(%client); //includes SW's and admin stuff
      }
      else {
         Parent::buyDeployableFavorites(%client);
      }
   }
   
   function getAmmoStationLovin(%client) {
      if($ScoreHudInventory::Active == 1) {
         //error("Much ammo station lovin applied");
         %cmt = $CurrentMissionType;
         // weapons
         %counter = %client.player.weaponSlotCount;
         if(%client.IsActivePerk("OverKill")) {
            %counter++;
         }
         for(%i = 0; %i < %counter; %i++) {
            %weapon = %client.player.weaponSlot[%i];
            // z0dd - ZOD, 9/13/02. Streamlining
            if ( %weapon.image.ammo !$= "" ) {
               %client.player.setInventory( %weapon.image.ammo, 400 );
            }

            %WImg = %weapon.Image;
            if(%WImg.ClipName !$= "") { //apply clips
               if(%client.IsActivePerk("Bandolier")) {
                  %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips * 2;
               }
               else {
                  %client.player.ClipCount[%WImg.ClipName] = %WImg.InitialClips;
               }
            }
         }
         // miscellaneous stuff -- Repair Kit, Beacons, Targeting Laser
         if ( !($InvBanList[%cmt, RepairKit]) )
            %client.player.setInventory( RepairKit, 1 );
         if ( !($InvBanList[%cmt, Beacon]) )
            %client.player.setInventory( Beacon, 400 );
         if ( !($InvBanList[%cmt, TargetingLaser]) )
            %client.player.setInventory( TargetingLaser, 1 );

         %client.player.setInventory( Grenade, 0 );
         %client.player.setInventory( ConcussionGrenade, 0 );
         %client.player.setInventory( CameraGrenade, 0 );
         %client.player.setInventory( FlashGrenade, 0 );
         %client.player.setInventory( FlareGrenade, 0 );

         // player should get the last type they purchased
         %grenType = %client.player.lastGrenade;

         // if the player hasnt been to a station they get regular grenades
         if(%grenType $= "") {
            //error("no gren type, using default...");
            %grenType = Grenade;
         }
         if ( !($InvBanList[%cmt, %grenType]) )
            %client.player.setInventory( %grenType, 30 );

         if( %client.player.getMountedImage($BackpackSlot) $= "AmmoPack") {
            invAmmoPackPass(%client);
         }
      }
      else {
         parent::getAmmoStationLovin(%client);
      }
   }
   
   //End
   
   //THIS FUNCTION HERE IS THE BRAINS OF THE SYSTEM
   //APPLY ALL MAJOR EDITS HERE
   function buildInventoryWindow(%client, %tag, %index) {
      %client.SCMPage = "SM";
      messageClient( %client, 'SetScoreHudSubheader', "", "INVENTORY" );
      
      //Required vars
      %cmt = $CurrentMissionType;
	  %sO = %client.TWM2Core;
      %xpHas = %sO.xp;
      %prestige = %sO.officer;
      if(%sO.officer $= "") {
         %sO.officer = 0;
         %prestige = %sO.officer;
      }
      //
      
      
      //fully customizable inventory system, separate, yet better
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Score Hud Inventory System: Version 1.0, By Phantom139");
      %index++;
      //* BEGIN ARMORS
      messageClient( %client, 'SetLineHud', "", %tag, %index, "CURRENT ARMOR: "@%client.scoreHudInv[Armor]@"");
      %index++;
      //
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Select Armor: ");
      %index++;
      //build the armor list, 3 armors per line
      for (%y = 0; $InvArmor[%y] !$= ""; %y++) {
         if ($InvArmor[%y] !$= %client.scoreHudInv[Armor]) {
            %armorList = %armorList TAB $InvArmor[%y];
         }
      }
      if($TWM::PlayingInfection) {
         %armorList = InfectionArmors(%client, %armorList);
      }
      else {
         %armorList = updateArmorList(%client, %armorList);
      }
      //
      %armors = getFieldCount(%armorList);
      %arSC = 0;
      for(%i = 0; %i < %armors; %i++) {
         %armorString[%arSC] = %armorString[%arSC]@" * <a:gamelink\tsetScoreInv\tArmor\t"@getField(%armorList, %i)@">"@getField(%armorList, %i)@"</a>";
         if(%i % 3 == 0) {
            %arSC++;
         }
      }
      //
      %arCT = 0;
      while(isSet(%armorString[%arCt])) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@%armorString[%arCt]@"");
         %index++;
         %arCT++;
      }
      //* END ARMORS
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* WEAPONS * ");
      %index++;
      //* BEGIN WEAPONS
         // * Slot Count Determined by the armor, see this line :D
      %armor = getArmorDatablock(%client, $NameToInv[%client.scoreHudInv[Armor]]);
      %slotCount = %armor.MaxWeapons;
      if(%client.IsActivePerk("OverKill") == 1) {
         %slotCount++;
      }
      // EX: S3 Rifle - [Slot 1] * Slot 2 * Slot 3
      %wepGroup = 0;
      while(isSet($SHI::WeaponGroup[%wepGroup])) {
         %weapCounter = 0;
         %weap = 0;
         while(isSet($InvWeapon[%weap])) {
            if($SHI::BlockToClass[$NameToInv[$InvWeapon[%weap]]] $= $SHI::WeaponGroup[%wepGroup]) {
               if(%armor.max[$NameToInv[$InvWeapon[%weap]]]) {
                  %weapCounter++;
               }
            }
            %weap++;
         }
         if(%weapCounter >= 1) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:CC0099><Font:Arial Bold:18>* "@$SHI::WeaponGroup[%wepGroup]@" *");
            %index++;
            //
            %weap = 0;
            while(isSet($InvWeapon[%weap])) {
               if($SHI::BlockToClass[$NameToInv[$InvWeapon[%weap]]] $= $SHI::WeaponGroup[%wepGroup]) {
                  %WInv = $NameToInv[$InvWeapon[%weap]];
                  if(%armor.max[%WInv]) {
                     if(%WInv.Image.RankRequire $= "") {
                        %xpNeed = 0;
                     }
                     else {
                        %xpNeed = $Ranks::MinPoints[%WInv.Image.RankRequire];
                     }
                     //
                     if(%WInv.Image.PrestigeRequire $= "") {
                        %prestigeNeed = 0;
                     }
                     else {
                        %prestigeNeed = %WInv.Image.PrestigeRequire;
                     }
                     //
                     if(%prestigeNeed $= "" || %prestigeNeed == 0) {
                        if(%xpNeed $= "" || %xpNeed == 0) {
                           if(%WInv.Image.MedalRequire) {
                              %canUse = DoMedalCheck(%client, %WInv.Image);
                              if(%canUse) {
                                 %slotString = "";
                                 for(%i = 1; %i <= %slotCount; %i++) {
                                    if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                       %slotString = %slotString @" [SLOT "@%i@"]";
                                    }
                                    else {
                                       %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@%i@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                    }
                                 }
                                 messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                 %index++;
                              }
                           }
                           else {
                              %slotString = "";
                              for(%i = 1; %i <= %slotCount; %i++) {
                                 if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                    %slotString = %slotString @" [SLOT "@%i@"]";
                                 }
                                 else {
                                    %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                 }
                              }
                              messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                              %index++;
                           }
                        }
                        else {
                           if (%xpHas >= %xpNeed && (isSet(%WInv.Image.RankRequire) ? %client.TWM2Core.rankNumber >= %WInv.Image.RankRequire : true )){
                              if(%WInv.Image.MedalRequire) {
                                 %canUse = DoMedalCheck(%client, %WInv.Image);
                                 if(%canUse) {
                                    %slotString = "";
                                    for(%i = 1; %i <= %slotCount; %i++) {
                                       if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                          %slotString = %slotString @" [SLOT "@%i@"]";
                                       }
                                       else {
                                          %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                       }
                                    }
                                    messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                    %index++;
                                 }
                              }
                              else {
                                 %slotString = "";
                                 for(%i = 1; %i <= %slotCount; %i++) {
                                    if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                       %slotString = %slotString @" [SLOT "@%i@"]";
                                    }
                                    else {
                                       %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                    }
                                 }
                                 messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                 %index++;
                              }
                           }
                        }
                     }
                     else {
                        if(%prestige >= %prestigeNeed) {
                           if(%xpNeed $= "" || %xpNeed == 0) {
                              if(%WInv.Image.MedalRequire) {
                                 %canUse = DoMedalCheck(%client, %WInv.Image);
                                 if(%canUse) {
                                    %slotString = "";
                                    for(%i = 1; %i <= %slotCount; %i++) {
                                      if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                          %slotString = %slotString @" [SLOT "@%i@"]";
                                       }
                                       else {
                                          %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                       }
                                    }
                                    messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                    %index++;
                                 }
                              }
                              else {
                                 %slotString = "";
                                 for(%i = 1; %i <= %slotCount; %i++) {
                                    if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                       %slotString = %slotString @" [SLOT "@%i@"]";
                                    }
                                    else {
                                       %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                    }
                                 }
                                 messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                 %index++;
                              }
                           }
                           else {
                              if (%xpHas >= %xpNeed && (isSet(%WInv.Image.RankRequire) ? %client.TWM2Core.rankNumber >= %WInv.Image.RankRequire : true )){
                                 if(%WInv.Image.MedalRequire) {
                                    %canUse = DoMedalCheck(%client, %WInv.Image);
                                    if(%canUse) {
                                       %slotString = "";
                                       for(%i = 1; %i <= %slotCount; %i++) {
                                         if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                             %slotString = %slotString @" [SLOT "@%i@"]";
                                          }
                                          else {
                                             %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                          }
                                       }
                                       messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                       %index++;
                                    }
                                 }
                                 else {
                                    %slotString = "";
                                    for(%i = 1; %i <= %slotCount; %i++) {
                                      if(%client.scoreHudInv[Weapon, %i] $= $InvWeapon[%weap]) {
                                          %slotString = %slotString @" [SLOT "@%i@"]";
                                       }
                                       else {
                                          %slotString = %slotString @" <a:gamelink\tsetScoreInv\tWeapon\t"@(%i)@"\t"@$InvWeapon[%weap]@">SLOT "@%i@"</a> ";
                                       }
                                    }
                                    messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@$InvWeapon[%weap]@" - "@%slotString@"");
                                    %index++;
                                 }
                              }
                           }
                        }
                     }
                  //}
                  }
               }
               %weap++;
            }
         }
         %wepGroup++;
      }
      //* END WEAPONS
      //* BEGIN PISTOL
      if(!%client.IsActivePerk("OverKill")) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* PISTOL * ");
         %index++;
         for ( %y = 0; $InvPistol[%y] !$= ""; %y++ ) {
            %PistolInv = $NameToInv[$InvPistol[%y]];
            if(%PistolInv.Image.RankRequire $= "") {
               %xpNeed = 0;
            }
            else {
               %xpNeed = $Ranks::MinPoints[%PistolInv.Image.RankRequire];
            }
            //
            if(%PistolInv.Image.PrestigeRequire $= "") {
               %prestigeNeed = 0;
            }
            else {
               %prestigeNeed = %PistolInv.Image.PrestigeRequire;
            }
            //
            if(%PistolInv.Image.MedalRequire) {
               %canUse = DoMedalCheck(%client, %PistolInv.Image);
            }
            else {
               %canUse = 1;
            }
            if(%canUse) {
               //XP Check Here
               if(%prestige >= %prestigeNeed) {
                  if(%xpHas > %xpNeed && (isSet(%PistolInv.Image.RankRequire) ? %client.TWM2Core.rankNumber >= %PistolInv.Image.RankRequire : true )) {
                     if(%client.scoreHudInv[Pistol] $= $InvPistol[%y]) {
                        messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>["@$InvPistol[%y]@"]");
                        %index++;
                     }
                     else {
                        messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16><a:gamelink\tsetScoreInv\tPistol\t"@$InvPistol[%y]@">"@$InvPistol[%y]@"</a>");
                        %index++;
                     }
                  }
               }
            }
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* PISTOL SLOT IS DISABLED -> OVERKILL PERK * ");
         %index++;
      }
      //* END PISTOL
      //* BEGIN MELEE
      if(%client.scoreHudInv[Armor] !$= "Purebuild") {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* MELEE * ");
         %index++;
         for ( %y = 0; $InvMelee[%y] !$= ""; %y++ ) {
            %meleeInv = $NameToInv[$InvMelee[%y]];
            if(%meleeInv.Image.MedalRequire) {
               %canUse = DoMedalCheck(%client, %meleeInv.Image);
            }
            else {
               %canUse = 1;
            }

            if(%canUse) {
               if(%client.scoreHudInv[Melee] $= $InvMelee[%y]) {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>["@$InvMelee[%y]@"]");
                  %index++;
               }
               else {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16><a:gamelink\tsetScoreInv\tMelee\t"@$InvMelee[%y]@">"@$InvMelee[%y]@"</a>");
                  %index++;
               }
            }
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* MELEE SLOT IS DISABLED -> PUREBUILD ARMOR * ");
         %index++;
      }
      //* END MELEE
      //* BEGIN PACK/DEPLOYABLE
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* PACK * ");
      %index++;
      for ( %y = 0; $InvPack[%y] !$= ""; %y++ ) {
         %PInv = $NameToInv[$InvPack[%y]];
         if (%armor.max[%PInv] && !$InvBanList[%cmt, %PInv]) {
            %packList = %packList TAB $InvPack[%y];
         }
      }
      %packs = getFieldCount(%packList);
      %arSC = 0;
      for(%i = 0; %i < %packs; %i++) {
         if(%client.scoreHudInv[Pack] $= getField(%packList, %i)) {
            %packString[%arSC] = %packString[%arSC]@" * ["@getField(%packList, %i)@"]";
         }
         else {
            %packString[%arSC] = %packString[%arSC]@" * <a:gamelink\tsetScoreInv\tPack\t"@getField(%packList, %i)@">"@getField(%packList, %i)@"</a>";
         }
         if(%i % 3 == 0) {
            %arSC++;
         }
      }
      //
      %arCT = 0;
      while(isSet(%packString[%arCt])) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@%packString[%arCt]@"");
         %index++;
         %arCT++;
      }
      //
//      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tsetScoreInv\tPack\t"@$InvPack[%y]@">"@$InvPack[%y]@"</a>");
//      %index++;
      if(%client.scoreHudInv[Armor] $= "Purebuild") {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* BUILDER PACK * ");
         %index++;
         //
         for ( %y = 0; $InvDep[%y] !$= ""; %y++ ) {
            %PInv = $NameToInv[$InvDep[%y]];
            if (%armor.max[%PInv] && !$InvBanList[%cmt, %PInv]) {
               %DepList = %DepList TAB $InvDep[%y];
            }
         }
         %packs = getFieldCount(%DepList);
         %arSC = 0;
         for(%i = 0; %i < %packs; %i++) {
            if(%client.scoreHudInv[Pack] $= getField(%DepList, %i)) {
               %DepString[%arSC] = %DepString[%arSC]@" * ["@getField(%DepList, %i)@"]";
            }
            else {
               %DepString[%arSC] = %DepString[%arSC]@" * <a:gamelink\tsetScoreInv\tPack\t"@getField(%DepList, %i)@">"@getField(%DepList, %i)@"</a>";
            }
            if(%i % 3 == 0) {
               %arSC++;
            }
         }
         //
         %arCT = 0;
         while(isSet(%DepString[%arCt])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16>"@%DepString[%arCt]@"");
            %index++;
            %arCT++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* BUILDER PACK IS DISABLED -> NON-PUREBUILD ARMOR * ");
         %index++;
      }
      //* END PACK/DEPLOYABLE
      //* BEGIN GRENADE
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* GRENADE * ");
      %index++;
      for (%y = 0; $InvGrenade[%y] !$= ""; %y++) {
         %GInv = $NameToInv[$InvGrenade[%y]];
         if (%armor.max[%GInv] && !$InvBanList[%cmt, %GInv]) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16><a:gamelink\tsetScoreInv\tGrenade\t"@$InvGrenade[%y]@">"@$InvGrenade[%y]@"</a>");
            %index++;
         }
      }
      //* END GRENADE
      //* BEGIN MINE
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* MINE * ");
      %index++;
      for (%y = 0; $InvMine[%y] !$= ""; %y++) {
         %MInv = $NameToInv[$InvMine[%y]];
         if (%armor.max[%MInv] && !$InvBanList[%cmt, %MInv]) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<Font:Arial Bold:16><a:gamelink\tsetScoreInv\tMine\t"@$InvMine[%y]@">"@$InvMine[%y]@"</a>");
            %index++;
         }
      }
      //* END MINE
      //* BEGIN ABILITY
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* ARMOR ABILITY * ");
      %index++;
      //* END ABILITY
      //* SAVE SLOTS
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:990099><Font:Arial Bold:24>* SAVE SETTINGS * ");
      %index++;
      //
      return %index;
   }
};
deactivatePackage(scoreHudInventory);
activatePackage(scoreHudInventory);

//SHI -> Score Hud Inventory

//New Inventory List Functions
//All Inventory itself is still handled by inventoryHud.cs
$SHI::WeaponGroup[0] = "Rifles";
$SHI::WeaponGroup[1] = "Sniper Rifles";
$SHI::WeaponGroup[2] = "SMGs";
$SHI::WeaponGroup[3] = "MGs";
$SHI::WeaponGroup[4] = "Shotguns";
$SHI::WeaponGroup[5] = "Explosives";
$SHI::WeaponGroup[6] = "Other";
$SHI::WeaponGroup[7] = "Construction";
//
$SHI::BlockToClass[S3Rifle] = "Rifles";
$SHI::BlockToClass[G41Rifle] = "Rifles";
$SHI::BlockToClass[M4A1] = "Rifles";
$SHI::BlockToClass[lasergun] = "Rifles";
$SHI::BlockToClass[ShadowRifle] = "Rifles";
$SHI::BlockToClass[IonRifle] = "Rifles";
$SHI::BlockToClass[PulseRifle] = "Rifles";
$SHI::BlockToClass[G17SniperRifle] = "Sniper Rifles";
$SHI::BlockToClass[M1SniperRifle] = "Sniper Rifles";
$SHI::BlockToClass[R700SniperRifle] = "Sniper Rifles";
$SHI::BlockToClass[ALSWPSniperRifle] = "Sniper Rifles";
$SHI::BlockToClass[MP26] = "SMGs";
$SHI::BlockToClass[Pg700] = "SMGs";
$SHI::BlockToClass[MiniChaingun] = "SMGs";
$SHI::BlockToClass[P90] = "SMGs";
$SHI::BlockToClass[PulseSMG] = "SMGs";
$SHI::BlockToClass[RP432] = "MGs";
$SHI::BlockToClass[MRXX] = "MGs";
$SHI::BlockToClass[MG42] = "MGs";
$SHI::BlockToClass[M1700] = "Shotguns";
$SHI::BlockToClass[Wp400] = "Shotguns";
$SHI::BlockToClass[SA2400] = "Shotguns";
$SHI::BlockToClass[SCD343] = "Shotguns";
$SHI::BlockToClass[Model1887] = "Shotguns";
$SHI::BlockToClass[MissileLauncher] = "Explosives";
$SHI::BlockToClass[Stinger] = "Explosives";
$SHI::BlockToClass[Javelin] = "Explosives";
$SHI::BlockToClass[RPG] = "Explosives";
$SHI::BlockToClass[PlasmaTorpedo] = "Explosives";
$SHI::BlockToClass[IonLauncher] = "Other";
$SHI::BlockToClass[flamer] = "Other";
$SHI::BlockToClass[ConcussionGun] = "Other";
$SHI::BlockToClass[MiniColliderCannon] = "Other";
$SHI::BlockToClass[ConstructionTool] = "Construction";
$SHI::BlockToClass[MergeTool] = "Construction";
$SHI::BlockToClass[EditTool] = "Construction";
$SHI::BlockToClass[EditorGun] = "Construction";
