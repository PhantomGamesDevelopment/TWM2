datablock ShapeBaseImageData(MedPackImage)
{
   shapeFile = "pack_upgrade_ammo.dts";
   item = MedPack;
   mountPoint = 1;
   offset = "0 0 0";
   emap = true;

   gun = MedPackGunImage;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateSequence[1] = "fire";
   stateSound[1] = RepairPackActivateSound;
   stateTransitionOnTriggerUp[1] = "Deactivate";

   stateName[2] = "Deactivate";
   stateScript[2] = "onDeactivate";
   stateTransitionOnTimeout[2] = "Idle";   
};

datablock ItemData(MedPack)
{
   className = Pack;
   catagory = "Packs";
   shapeFile = "pack_upgrade_ammo.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "MedPackImage";
   pickUpName = "a Med pack";

   lightOnlyStatic = true;
   lightType = "PulsingLight";
   lightColor = "1 0 0 1";
   lightTime = 1200;
   lightRadius = 4;

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(MedpackImg1)
{
   shapeFile = "pack_upgrade_repair.dts";
   mountPoint = 1;
   offset = "0 -0.05 0";
   emap = true;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";
	
   stateName[1] = "Activate";
   stateSequence[1] = "fire";
   stateTransitionOnTriggerUp[1] = "Idle";
};

datablock ShapeBaseImageData(MedpackImg2)
{
   shapeFile = "repair_kit.dts";
   mountPoint = 1;
   offset = "0 -0.2 -0.18";
   emap = true;
};

datablock ShapeBaseImageData(MedpackImg2b)
{
   shapeFile = "repair_kit.dts";
   mountPoint = 1;
   offset = "0 -0.2 0.15";
   emap = true;
};

//--------------------------------------------------------------------------
// Repair Gun

datablock ShapeBaseImageData(MedPackGunImage)
{
   shapeFile = "pack_upgrade_repair.dts";
   offset = "0 0.15 0";
   rotation = "1 0 0 90";

   usesEnergy = true;
   minEnergy = 3;
   cutOffEnergy = 3.1;
   emap = true;

   repairFactorPlayer = 0.005;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.25;

   stateName[1] = "ActivateReady";
   stateScript[1] = "onActivateReady";
   stateSpinThread[1] = Stop;
   stateTransitionOnAmmo[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "ActivateReady";

   stateName[2] = "Ready";
   stateSpinThread[2] = Stop;
   stateTransitionOnNoAmmo[2] = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Repair";

   stateName[4] = "Repair";
   stateSound[4] = RepairPackFireSound;
   stateScript[4] = "onRepair";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "fire";
   stateFire[4] = true;
   stateEnergyDrain[4] = 32;
   stateTransitionOnNoAmmo[4] = "Deactivate";
   stateTransitionOnTriggerUp[4] = "Deactivate";
   stateTransitionOnNotLoaded[4] = "Validate";

   stateName[5] = "Deactivate";
   stateScript[5] = "onDeactivate";
   stateSpinThread[5] = SpinDown;
   stateSequence[5] = "activate";
   stateDirection[5] = false;
   stateTimeoutValue[5] = 0.2;
   stateTransitionOnTimeout[5] = "ActivateReady";
};

//---------------------------
//PACK STUFF
//---------------------------

function MedPackImage::onMount(%data, %obj, %node){
   %obj.mountImage(MedpackImg1, 4);
   %obj.mountImage(MedpackImg2, 7);
}

function MedPackImage::onUnmount(%data, %obj, %node) {
   if(%obj.getMountedImage($WeaponSlot))
      if(%obj.getMountedImage($WeaponSlot).getName() $= "MedPackGunImage")
         %obj.unmountImage($WeaponSlot);
   %obj.unmountImage(4);
   %obj.unmountImage(7);
}

function MedPackImage::onActivate(%data, %obj, %slot) {
   %obj.setImageTrigger(4, true);
   %obj.mountImage(MedpackImg2b, 7);
   messageClient( %obj.triggeredBy.client, 'CloseHud', "", 'scoreScreen' );
   messageClient( %obj.triggeredBy.client, 'CloseHud', "", 'inventoryScreen' );
   commandToClient(%obj.triggeredBy.client, 'StationVehicleShowHud');

   if(%obj.isPilot())
   {
      %obj.setImageTrigger(%slot, false);
      return;
   }
      if(%obj.getMountedImage($WeaponSlot).getName() !$= "MedpackGunImage") {
      messageClient(%obj.client, 'MsgRepairPackOn', '\c2Medic pack activated.');

      %obj.setArmThread(look);

      %obj.mountImage(MedPackGunImage, $WeaponSlot);
      commandToClient(%obj.client, 'setRepairReticle');
      }
}

function MedPackImage::onDeactivate(%data, %obj, %slot) {
   %obj.setImageTrigger(4, false);
   %obj.setImageTrigger(%slot, false);
   if(%obj.getMountedImage($WeaponSlot).getName() $= "MedpackGunImage")
      %obj.unmountImage($WeaponSlot);
   %obj.mountImage(MedpackImg2, 7);
}


function MedPackGunImage::onMount(%this,%obj,%slot) {
   %obj.setImageAmmo(%slot,true);
   if ( !isDemo() )
      commandToClient( %obj.client, 'setRepairPackIconOn' );
      
   %obj.UsingMedGun = 1;
}

function MedPackGunImage::onUnmount(%this,%obj,%slot)
{
   if(%obj.isreping == 1)
      MedstopRepair(%obj);

   %obj.UsingMedGun = 0;

   %obj.setImageTrigger(%slot, false);
   %obj.setImageTrigger($BackpackSlot, false);
   if ( !isDemo() )
      commandToClient( %obj.client, 'setRepairPackIconOff' );
}

function MedPackGunImage::onRepair(%this,%obj,%slot){
   %obj.isreping = 1;
   %pos = %obj.getWorldBoxCenter();
   %obj.reptargets = "";
   InitContainerRadiusSearch(%pos, 10, $TypeMasks::PlayerObjectType);
   while ((%targetObject = containerSearchNext()) != 0) {
	     if(%targetObject.getDamageLevel() > 0.0 && %targetObject.getState() !$= "dead") {
	        %obj.reptargets = %obj.reptargets @ %targetObject @" ";
         }

         if (%targetObject.infected && %targetObject.getState() !$= "dead") {
            TWM2Lib_Zombie_Core("cureInfection", %targetObject);
            %targetObject.playAudio(0, ShockLanceHitSound);
            messageclient(%obj.client, 'MsgClient', "\c2Applying Zombie Cure To "@%targetObject.client.namebase@".");
            messageclient(%targetObject.client, 'MsgClient', "\c2Zombie Cure Applied By "@%obj.client.namebase@".");
         }
         
         if (%targetObject.onfire && %targetObject.getState() !$= "dead") {
            %targetObject.onfire = 0;
            %targetObject.playAudio(0, ShockLanceHitSound);
            messageclient(%obj.client, 'MsgClient', "\c2Applying Burn Patch To "@%targetObject.client.namebase@".");
            messageclient(%targetObject.client, 'MsgClient', "\c2Burn Patch Applied By "@%obj.client.namebase@".");
         }
   }
   if(%obj.reptargets $= "" && !%targetObject.isinfected){
	messageclient(%obj.client, 'MsgClient', '\c2No targets to repair.');
   }
   Medrepair(%obj, %obj.reptargets);
}

function MedPackGunImage::onDeactivate(%this,%obj,%slot)
{
   MedstopRepair(%obj);
}

function Medrepair(%obj, %targets){
   if(%obj.isreping == 0)
	return;
  if(%targets !$= ""){
   %numtrgs = getWordCount(%targets);
   for(%i = 0; %i < %numtrgs; %i++){
	%target = getWord(%targets, %i);
	if(vectorDist(%obj.getWorldBoxCenter(), %target.getWorldBoxCenter()) <= 10 && %target.getDamageLevel() > 0.0){
	   if(%target.reping != 1){
		%target.reping = 1;
		%target.setRepairRate(%target.getRepairRate() + MedPackGunImage.repairFactorPlayer);
	   }
	}
	else{
	   if(%target.reping == 1){
		%target.reping = 0;
		%target.setRepairRate(%target.getRepairRate() - MedPackGunImage.repairFactorPlayer);
	   }
	}
   }
  }

   %pos = %obj.getWorldBoxCenter();
   %obj.reptargets = "";
   InitContainerRadiusSearch(%pos, 10, $TypeMasks::PlayerObjectType);
   while ((%targetObject = containerSearchNext()) != 0){
	if(%targetObject.getDamageLevel() > 0.0 && %targetObject.getState() !$= "dead")
	   %obj.reptargets = %obj.reptargets @ %targetObject @" ";
   }
   if(%obj.isreping == 1)
      %obj.reploop = schedule(500, 0, "Medrepair", %obj, %obj.reptargets);
}

function MedstopRepair(%obj){
   %obj.isreping = 0;
   if(%obj.reptargets !$= ""){
      %numtrgs = getWordCount(%obj.reptargets);
      for(%i = 0; %i < %numtrgs; %i++){
	   %target = getWord(%obj.reptargets, %i);
	   if(%target.reping == 1){
		%target.reping = 0;
		%target.setRepairRate(%target.getRepairRate() - MedPackGunImage.repairFactorPlayer);
	   }
      }
   }
}

//---------------------------
//REVIVE
//---------------------------

function checkrevive(%obj){
   if(isObject(%obj.lasttouchedcorpse)){
	%Tobj = %obj.lasttouchedcorpse;
    if($TWM::PlayingHorde) {
       if($HordeGame::RevivesLeft == 0) {
	      messageclient(%obj.client, 'MsgClient', "\c2HORDE: No Revives Remain In The Revive Pool.");
	      return;
       }
       if(%Tobj.infected || %Tobj.isZombie) {
	      messageclient(%obj.client, 'MsgClient', "\c2WHOA!!! We don't want to make the Undead.... UnDead AGAIN.");
	      return;
       }
    }
    if($TWM::PlayingInfection) {
       if($InfectionGame::Infected[%Tobj.client]) {
	      messageclient(%obj.client, 'MsgClient', "\c2JESUS, Trying To Revive Something That Want's Your Brains!?!?!?.");
	      return;
       }
    }
	if(%Tobj.kibbled == 1){
	   messageclient(%obj.client, 'MsgClient', "\c2This body is destroyed.");
	   return;
	}
    if(%Tobj.isBoss == 1){
	   messageclient(%obj.client, 'MsgClient', "\c2ARE YOU INSANE!?!??! YEAH... LETS JUST REVIVE THE DAMNED BOSS THAT JUST REKT YOU ABOUT 100 TIMES!!!!");
	   return;
	}
    if(%Tobj.infected || %Tobj.isZombie) {
	   messageclient(%obj.client, 'MsgClient', "\c2WHOA!!! We don't want to make the Undead.... UnDead AGAIN.");
	   return;
    }
	if(vectorDist(%obj.getPosition(),%Tobj.getPosition()) > 3){
	   messageclient(%obj.client, 'MsgClient', "\c2Must be in contact with a body to revive it.");
	   return;
	}
	%eyevec = %obj.getEyeVector();
	%eyepos = posFromTransform(%obj.getEyeTransform());
	%Tvec = vectorNormalize(vectorSub(%Tobj.getPosition(),%eyepos));
	if(vectorDist(%eyevec, %Tvec) > 0.5){
	   messageclient(%obj.client, 'MsgClient', "\c2Must be looking at body.");
	   return;
	}
	if(%Tobj.getState() !$= "dead"){
	   messageclient(%obj.client, 'MsgClient', "\c2Cannot revive, target isnt dead.");
	   return;
	}
	if(%Tobj.team != %obj.team){
	   messageclient(%obj.client, 'MsgClient', "\c2Sure thing Megatron, Lets Revive the enemy so you can kill him again =-|");
	   return;
	}
	   revive(%obj, %tobj);
   }
   else
	messageclient(%obj.client, 'MsgClient', "\c2Must be in contact with a body to revive it.");
}

function revive(%obj, %target){
   if(%target.team == 0) {
      messageclient(%obj.client, 'MsgClient', "\c2"@%target.client.namebase@" cannot be revived.");
   }
   if(%target.client.getControlObject() !$= %target.client.player){
	//necessitys
    if($TWM::PlayingHorde) {
       CompleteNWChallenge(%obj.client, "Angel");
       $HordeGame::RevivesLeft--;
       $HordeGame::LivingCount++;
       %s = MakeReviveString();
       messageAll('MsgSPCurrentObjective2' ,"", "Players Alive: "@$HordeGame::LivingCount@" | "@%s@"");
    }
    //
	%target.setDamageLevel(%target.getdatablock().maxDamage - 0.1);
      %target.client.setControlObject(%target);
	%target.revived = 1;
	Cancel(%target.ParaLoop);
	Cancel(%target.revcheck);
	%target.client.player = %target;
    Cancel(%target.player.createTheZ);
	//points and message
	%obj.client.revivecount++;
    //GainExperience(%obj.client, 25, "You have revived "@%target.client.namebase@" ");
	messageclient(%target.client, 'MsgClient', "\c2You were revived by "@%obj.client.namebase@".");

	//effects
	%target.setDamageFlash(1);
	%target.setMoveState(true);
	playDeathCry(%target);
	revivestand(%target, 0);
    buyFavorites(%target.client);

	%obj.playAudio(0, ShockLanceHitSound);
    %obj.zapObject();
    %obj.schedule(3000, "stopZap");
   }
   else
	messageclient(%obj.client, 'MsgClient', "\c2Target already has another clone in use.");
}

function revivestand(%obj, %count){
   if(%obj.getstate() $= "dead")
	return;
   if(%count <= 2){
	%obj.setActionThread("scoutRoot",true);
	%obj.setDamageFlash(0.7);
   }
   else if(%count <= 5){
	%obj.setActionThread("sitting",true);
	%obj.setDamageFlash(0.4);
   }
   else if(%count >= 6){
	%obj.setActionThread("ski",true);
	%obj.setMoveState(false);
	return;
   }
   %count++;
   schedule(500, 0, "revivestand", %obj, %count);	
}
