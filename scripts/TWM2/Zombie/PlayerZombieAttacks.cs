function PlayerLordFire1(%Player) {
   if(%Player.recharging) {
      return;
   }
   Schedule(1000,0,PlayerLordFire2,%Player);
   Schedule(1500,0,PlayerLordFire2,%Player);
   %charge = new ParticleEmissionDummy() {
   	   position = %player.getMuzzlePoint(5);
   	   dataBlock = "defaultEmissionDummy";
   	   emitter = "FlameEmitter";
   };
   MissionCleanup.add(%charge);
   %Player.recharging = 1;
   %charge.schedule(1000, "delete");
   schedule(7000,0,"ResetZombieCharge",%player);
}

function PlayerLordFire2(%player) {
   %p = new TracerProjectile() {
  	  dataBlock        = ZombieAcidPulse;
      initialDirection = %player.getMuzzleVector(6);
      initialPosition  = %player.getMuzzlePoint(6);
  	  sourceObject     = %player;
  	  sourceSlot       = 6;
   };
   MissionCleanup.add(%p);
}

function ResetZombieCharge(%player) {
   %player.recharging = 0;
}

function playerLiftTarget(%zombie,%count){
   %pos         = %zombie.getMuzzlePoint($WeaponSlot);
   %vec         = %zombie.getMuzzleVector($WeaponSlot);
   %targetpos   = vectoradd(%pos,vectorscale(%vec,5));
   %obj         = containerraycast(%pos,%targetpos,$typemasks::playerobjecttype,%zombie);
   %closestclient = getword(%obj,0);
   if (%obj < 1){
      return;
   }
   if(%count == 0) {
	  %zombie.canmove = 0;
   }
   if(%closestclient.getState() $= "dead" || %Zombie.getState() $= "dead") {
	  %zombie.canmove = 1;
	  return;
   }
   %target = %closestclient;
   if(!isobject(%target)){
	  %zombie.canmove = 1;
	  return;
   }
   %pos = %zombie.getworldboxcenter();
   %Tpos = %target.getworldboxcenter();
   %ZtoT = vectorsub(%tpos,%pos);
   if (%count <= 12){
	  %newpos = vectoradd(%ZtoT,vectoradd(%pos,"0 0 -0.6"));
	  %target.setTransform(%newpos);
	  %target.setvelocity("0 0 0");
   }
   else {
	  %killtype = getrandom(1,2);
	  if(%killtype == 1){
	     %closestwall = 20;
	     %nv2 = (getword(%ZtoT, 0) * -1);
	     %nv1 = getword(%ZtoT, 1);
	     %vector1 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"),20);
	     %nvector1 = vectoradd(%tpos,%vector1);
	     %nv2 = getword(%ZtoT, 0);
	     %nv1 = (getword(%ZtoT, 1) * -1);
	     %vector2 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"),20);
	     %nvector2 = vectoradd(%tpos,%vector2);
	     %searchresultR = containerRayCast(%tpos, %nvector1, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
	     %searchresultL = containerRayCast(%tpos, %nvector2, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
	     if(%searchresultR) {
		    %Hpos = getword(%searchresultR,1)@" "@getword(%searchresultR,2)@" "@getword(%searchresultR,3);
            %distance = vectordist(%Tpos, %Hpos);
		    if(%distance <= %closestwall){
		       %closestwall = %distance;
		       %vector = vectoradd(vectorscale(%vector1,1500),"0 0 100");
		    }
         }
	     if(%searchresultL){
            %Hpos = getword(%searchresultL,1)@" "@getword(%searchresultL,2)@" "@getword(%searchresultL,3);
		    %distance = vectordist(%Tpos, %Hpos);
		    if(%distance <= %closestwall){
		       %closestwall = %distance;
		       %vector = vectoradd(vectorscale(%vector2,1500),"0 0 100");
		    }
	     }
         if(%closestwall == 20){
	        %x = getword(%ZtoT, 0);
	   	    %y = getword(%ZtoT, 1);
		    %outvec = vectorscale(vectornormalize(%x@" "@%y@" 0"),50);
		    %vector = vectoradd("0 0 -15000",%outvec);
	     }
         %target.applyimpulse(%target.getposition(),%vector);
      }
      else if(%killtype == 2) {
         %target.infected = 1;
         %target.damage(%zombie, %target.getposition(), 10.0, $DamageType::ZombieL);
      }
      %count = 0;
      %zombie.canmove = 1;
      return;
   }
   %count++;
   %zombie.killingplayer = schedule(150, %zombie, "Llifttarget", %zombie, %closestclient, %count);
}

function playerRogFire(%player) {
   if(%player.recharging) {
      return;
   }
   if(!isobject(%player) || %player.getState() $= "dead")
      return;
   if(%player.shotsfired >= 100) {
      %player.setMoveState(false); //unfreeze him
      %player.recharging = 1;
      %player.shotsfired = 0;
      schedule(7500, 0, RapierShieldApply, %player);
      schedule(10000, 0, "eval", ""@%player@".recharging = 0;");
      return;
   }
   %player.rapiershield = 0;
   %player.setMoveState(true); //freeze him
   %pos = %player.getMuzzlePoint(5);
   %vec = %player.getMuzzleVector(5);

   %x = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %nvec = MatrixMulVector(%mat, %vec);
   %player.shotsfired++;

   %p = new LinearFlareProjectile(){
      dataBlock        = zMG42Projectile;
      initialDirection = %nvec;
	  initialPosition  = %pos;
	  sourceObject     = %player;
	  sourceSlot       = 5;
   };
   schedule(50, 0, "playerRogFire", %player);
}

function PlayerDemonLordFlyAttack(%obj,%target){
   if(!isObject(%obj)) {
	  return;
   }
   if(%obj.getState() $= "dead") {
	  return;
   }
   if(%obj.chargecount $= "") {
	  %obj.chargecount = 0;
   }
   if(%obj.chargecount <= 9) {
	  FaceTarget(%obj,%target);
	  %obj.setvelocity("0 0 10");
	  %obj.chargecount++;
	  schedule(100, 0, "PlayerDemonLordFlyAttack",%obj,%target);
   }
   else if(%obj.chargecount == 10) {
	  FaceTarget(%obj,%target);
	  %obj.setvelocity("0 0 5");
	  %vector = vectorSub(%target.getPosition(),%obj.getPosition());
	  %nVec = vectorNormalize(%vector);
	  %vector = vectorAdd(%vector,vectorscale(%nvec,-4));
	  %obj.attackpos = vectorAdd(%obj.getPosition(),%vector);
	  %obj.attackdir = %nVec;
//	  echo(%obj.getPosition() SPC %target.getPosition() SPC %obj.attackpos SPC %obj.attackdir);
	  %obj.startFade(400, 0, true);
	  %obj.chargecount++;
	  schedule(400, 0, "PlayerDemonLordFlyAttack",%obj,%target);
   }
   else if(%obj.chargecount >= 11){
	  %obj.startFade(500, 0, false);
	  %obj.setPosition(%obj.attackpos);
	  %obj.setvelocity(vectorscale(%obj.attackdir,25));
	  %obj.justmelee = 1;
	  %obj.chargecount = 0;
//	  echo(%obj.getPosition() SPC %target.getPosition() SPC %obj.attackpos SPC %obj.attackdir);
	  %obj.attackpos = "";
	  %obj.attackdir = "";
   }
}

function PlayerDemonLordPlasmaAttack(%obj, %target) {
   if(!isObject(%obj)) {
	  return;
   }
   if(%obj.getState() $= "dead") {
	  return;
   }
   if(%obj.chargecount $= "") {
	  %obj.chargecount = 0;
   }
   if(%obj.chargecount <= 9) {
	  %obj.setVelocity("0 0 10");
	  %obj.chargecount++;
	  schedule(100, 0, "PlayerDemonLordPlasmaAttack", %obj, %target);
   }
   else {
	  %obj.setVelocity("0 0 3");
	  %vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
   	  %p = new LinearFlareProjectile() {
	     dataBlock        = DMPlasma;
	     initialDirection = %vec;
	     initialPosition  = %obj.getMuzzlePoint(4);
	     sourceObject     = %obj;
	     sourceSlot       = 4;
   	  };
	  %obj.chargecount = 0;
	  %obj.justshot = 1;
   }
}

function PlayerDemonLordFireRainAttack(%obj,%target){
   if(!isObject(%obj)) {
	  return;
   }
   if(%obj.getState() $= "dead") {
	  return;
   }
   if(%obj.chargecount $= "") {
	  %obj.chargecount = 0;
   }
   if(%obj.chargecount == 0) {
	  FaceTarget(%obj, %target);
	  for(%i = 0; %i < 10; %i++){
	     %pos = %obj.getPosition();
         %x = getRandom(0,10) - 5;
         %y = getRandom(0,10) - 5;
         %vec = vectorAdd(%pos,%x SPC %y SPC "5");
	     %searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -10"), $TypeMasks::TerrainObjectType, %obj);

         %charge = new ParticleEmissionDummy() {
   	        position = posFromRaycast(%searchresult);
   	        dataBlock = "defaultEmissionDummy";
   	        emitter = "FlameEmitter";
         };
         MissionCleanup.add(%charge);
         %charge.schedule(1500, "delete");
      }
      %obj.chargecount++;
	  schedule(1000, 0, "PlayerDemonLordFireRainAttack",%obj,%target);
   }
   else {
	  %x = (getRandom() * 2) - 1;
	  %y = (getRandom() * 2) - 1;
	  %z = getRandom();
	  %vec = vectorNormalize(%x SPC %y SPC %z);
	  %pos = vectorAdd(%target.getPosition(),vectorScale(%vec, 20));
	  for(%i = 0;%i < 10;%i++){
	     %x = getRandom(0,14) - 7;
         %y = getRandom(0,14) - 7;
         %spwpos = vectorAdd(%pos,%x SPC %y SPC "2");
   	     %p = new GrenadeProjectile() {
		    dataBlock        = DemonFireball;
		    initialDirection = vectorScale(%vec,-1);
		    initialPosition  = %spwpos;
		    sourceObject     = %obj;
		    sourceSlot       = 4;
         };
      }
      %obj.justshot = 1;
	  %obj.chargecount = 0;
   }
}

function PlayerSummon(%player, %count) {
   if(!isobject(%player) || %player.getState() $= "dead") {
      return;
   }
   %type = GetRandom(1, 9);
   if(%type > 5) {
      %type += 3;
      if(%type == 10) { //summoners don;t summon more summoners
         %type = 13;
      }
   }
   for(%i = 0; %i < %count; %i++) {
      %pos = vectoradd(%player.getPosition(), TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
      %fpos = vectoradd("0 0 5",%pos);
      TWM2Lib_Zombie_Core("SpawnZombie", "zSpawnCommand", %type, %fpos);
   }
   %player.setMoveState(true);
   %player.setActionThread($Zombie::RogThread,true);
   %player.schedule(3500, "setMoveState", false);
}

function PlayerLRAbilities(%Player) {
   if(!isobject(%Player) || %Player.getState() $= "dead") {
   return;
   }
   %z = %player; //< Im lazy =-P
   %cl = %z.client;
   %z.setVelocity("0 0 0");
   %rand = getRandom(1,8);
   switch(%rand) {
      case 1:
      %target = FindValidTarget(%z);
      %att = 0;
      while(%target.player == %Player) {
      %att++;
         if(%att > 7) {
            %player.RecentAttack["RandAttack"] = 0;
            return;
         }
         else {
            %target = FindValidTarget(%z);
         }
      }
      if(isObject(%target.player) && !%target.ignoredbyZombs) {
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Launch!");
         %SPos1 = vectorAdd(%z.getPosition(),"1 0 0");
         %SPos2 = vectorAdd(%z.getPosition(),"-1 0 0");
   	      %p1 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 1";
           initialPosition  = %SPos1;
   	   	   sourceObject     = %z;
   	   	   sourceSlot       = 6;
   	      };
   	      %p2 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 1";
           initialPosition  = %SPos2;
   	   	   sourceObject     = %z;
   	   	   sourceSlot       = 6;
   	      };
          MissionCleanup.add(%p1);
          MissionCleanup.add(%p2);
          schedule(5000,0,"MissileDrop",%p1, %target.player);
          schedule(5000,0,"MissileDrop",%p2, %target.player);
       }
      case 2:
      %target = FindValidTarget(%z);
      if(isObject(%target.player) && !%target.ignoredbyZombs) {
         %z.laserStormSount = 0;
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Laser Storm Begin!");
         LRLaserStorm(%z, %target.player);
       }
      case 3:
      %target = FindValidTarget(%z);
      if(isObject(%target.player) && !%target.ignoredbyZombs) {
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Metros Maul!");
         %fpos = vectoradd(%target.player.getposition(),TWM2Lib_MainControl("getRandomPosition", 50 TAB 0));
         %pos2 = vectoradd(%fpos,"0 0 700");
         schedule(500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(1000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(1500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
       }
      case 4:
      %target = FindValidTarget(%z);
      if(isObject(%target.player) && !%target.ignoredbyZombs) {
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Storm Begins!");
         %SPos1 = vectorAdd(%z.getPosition(),"1 0 0");
         %SPos2 = vectorAdd(%z.getPosition(),"-1 0 0");
   	      %p1 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 10";
           initialPosition  = %SPos1;
   	   	   sourceObject     = %z;
   	   	   sourceSlot       = 6;
   	      };
   	      %p2 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 10";
           initialPosition  = %SPos2;
   	   	   sourceObject     = %z;
   	   	   sourceSlot       = 6;
   	      };
          MissionCleanup.add(%p1);
          MissionCleanup.add(%p2);
          schedule(5000,0,"MissileDrop",%p1, %target.player);
          schedule(5000,0,"MissileDrop",%p2, %target.player);
          schedule(1000,0,"FireMoreMissiles", %z, %target.player);
          schedule(2000,0,"FireMoreMissiles", %z, %target.player);
       }
      case 5:
         %z.setMoveState(true);
         schedule(7000,0,"unfreezeZombieobject",%z);
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         %c = createEmitter(%z.position,FlashLEmitter,"1 0 0");      //Rotate it
         schedule(1000,0,"killit",%c);
         InitContainerRadiusSearch(%z.getPosition(),200,%TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0){
            if (%potentialTarget.getPosition() != %z.getPosition() && !%potentialtarget.client.ignoredbyZombs) {
               %potentialTarget.staticTicks = 0;
               SDischLoop(%potentialTarget);
            }
         }
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Static Discharge!");
      case 6:
         %z.setMoveState(true);
         schedule(7000,0,"unfreezeZombieobject",%z);
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         %c = createEmitter(%z.position,FlashLEmitter,"1 0 0");      //Rotate it
         schedule(1000,0,"killit",%c);
         InitContainerRadiusSearch(%z.getPosition(),200,%TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0){
            if (%potentialTarget.getPosition() != %z.getPosition() && !%potentialtarget.client.ignoredbyZombs) {
               %potentialTarget.staticTicks = 0;
               SDischLoop(%potentialTarget);
            }
         }
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Static Discharge!");
      case 7:
      %target = FindValidTarget(%z);
      if(isObject(%target.player) && !%target.ignoredbyZombs) {
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Metros EXTREMITY!!!!");
         %fpos = vectoradd(%target.player.getposition(), TWM2Lib_MainControl("getRandomPosition", 50 TAB 0));
         %pos2 = vectoradd(%fpos,"0 0 700");
         schedule(500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(1000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(1500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(2000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(2500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(3000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(3500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(4000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(4500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(5000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(5500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(6000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(6500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(7000,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
         schedule(7500,0,spawnprojectile,JTLMeteorStormFireball,GrenadeProjectile,%pos2,"0 0 -10");
       }
      default:
      %target = FindValidTarget(%z);
      if(isObject(%target.player) && !%target.ignoredbyZombs) {
         MessageAll('MessageAll', "\c4"@getTaggedString(%cl.name)@": Launch!");
         %SPos1 = vectorAdd(%z.getPosition(),"1 0 0");
         %SPos2 = vectorAdd(%z.getPosition(),"-1 0 0");
   	      %p1 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 1";
           initialPosition  = %SPos1;
   	   	   sourceObject     = %z;
   	   	   sourceSlot       = 6;
   	      };
   	      %p2 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 1";
           initialPosition  = %SPos2;
   	   	   sourceObject     = %z;
   	   	   sourceSlot       = 6;
   	      };
          MissionCleanup.add(%p1);
          MissionCleanup.add(%p2);
          schedule(5000,0,"MissileDrop",%p1, %target.player);
          schedule(5000,0,"MissileDrop",%p2, %target.player);
       }
   }
}
