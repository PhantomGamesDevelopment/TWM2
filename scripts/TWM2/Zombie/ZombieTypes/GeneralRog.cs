//ROG
$Rog::RapierShieldMaxDist = 95;

datablock PlayerData(ROGZombieArmor) : LightMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 50.0;
   minImpactSpeed = 35;
   shapeFile = "bioderm_heavy.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;
};

datablock ShapeBaseImageData(ZMG42BaseImage) {
   shapeFile = "weapon_sniper.dts";
   emap = true;
};

datablock ShapeBaseImageData(ROGSAWImage1) {
   shapeFile = "weapon_missile.dts";
   rotation = "0 0 1 180";
   offset = "0.01 0.04 0.0"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ROGSAWImage2) {
   shapeFile = "ammo_mortar.dts";
   rotation = "0 0 1 90";
   offset = "-0.06 -0.23 0.25"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ROGSAWImage3) {
   shapeFile = "ammo_chaingun.dts";
   offset = "0.08 0.4 -0.13"; // L/R - F/B - T/B
};

datablock ParticleData(spikerParticle) {
	dragCoefficient      = 0.0;
    windCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = -0.2;
	constantAcceleration = -10;

	lifetimeMS           = 50;
	lifetimeVarianceMS   = 0;

	textureName          = "particletest";

	useInvAlpha = false;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;

	colors[0]     = "1.0 0.51 0.51 1.0";
	colors[1]     = "1.0 0.51 0.51 1.0";
	colors[2]     = "1.0 0.51 0.51 1.0";
	sizes[0]      = 0.08;
	sizes[1]      = 0.08;
	sizes[2]      = 0.02;
	times[0]      = 0.0;
	times[1]      = 0.5;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(spikerEmitter) {
	ejectionPeriodMS = 0.01;
	periodVarianceMS = 0;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin         = 0.0;
	thetaMax         = 0.0;

    particles = "spikerParticle";
};

datablock LinearFlareProjectileData(zMG42Projectile) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.15;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::MG42;

   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.5;
   LegsMultiplier      = 0.5;

   baseEmitter        = spikerEmitter;

   dryVelocity       = 3000.0;
   wetVelocity       = 1000.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;

   numFlares         = 35;
   flareColor        = "1 0.18 0.03";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

function GeneralRogmovetotarget(%zombie){
   if(!isobject(%zombie)) {
      return;
   }
   if(%zombie.getState() $= "dead") {
      return;
   }
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);

   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
	  if (%closestdistance >= 10 && %closestdistance <= 500 && %zombie.canjump == 1 && !%zombie.preRapierShield && %zombie.canFireMG){
         RogFire(%zombie, %closestclient);
	     %zombie.canjump = 0;
	     schedule(4000, %zombie, "Zsetjump", %zombie);
	  }
      //Sword Mount / Unmount
      if(%closestDistance < 12 && %zombie.canDoImageSwitch) {
         if(!%zombie.ImageMounted) {
            %zombie.unMountImage(5);
            %zombie.unMountImage(6);
            %zombie.unMountImage(7);
            %zombie.unMountImage(8);

            %zombie.ImageMounted = 1;
            %zombie.mountImage(BoVSwing, 7);
            ServerPlay3D(BlasterSwitchSound, %zombie.getPosition());
         }
      }
      else {
         %zombie.unMountImage(7);
         %zombie.ImageMounted = 0;

         %zombie.mountImage(ZMG42BaseImage, 5);
         %zombie.mountImage(ROGSAWImage1, 6);
         %zombie.mountImage(ROGSAWImage2, 7);
         %zombie.mountImage(ROGSAWImage3, 8);
         
         ServerPlay3D(ChaingunSwitchSound, %zombie.getPosition());
      }
      if(%zombie.hastarget != 1){
         serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
         %zombie.hastarget = 1;
      }
      %chance = (getrandom() * 20);
      if(%chance >= 19) {
         serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());
      }
      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);
      
	  if (%closestdistance >= 10 && %closestdistance <= 500 && %zombie.canjump == 1 && !%zombie.preRapierShield && %zombie.canFireMG){
	     RogFire(%zombie, %closestclient);
	     %zombie.canjump = 0;
	     schedule(4000, %zombie, "Zsetjump", %zombie);
	     return;
	  }
      
      %vector = vectorscale(%vector, $Zombie::DForwardSpeed);
      %upvec = "150";
	  %x = Getword(%vector,0);
	  %y = Getword(%vector,1);
	  %z = Getword(%vector,2);
	  if(%z >= ($Zombie::DForwardSpeed)) {
         %upvec = (%upvec * 5);
      }
      %vector = %x@" "@%y@" "@%upvec;
	  %zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	  %zombie.hastarget = 0;
	  %zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "GeneralRogmovetotarget", %zombie);
}

function RogFire(%zombie,%target) {
   if(!isobject(%zombie) || %zombie.getState() $= "dead") {
      return;
   }
   if(!isobject(%target) || %target.getState() $= "dead") {
      %zombie.shotsfired = 100;
      %zombie.setMoveState(false); //unfreeze him
      %zombie.canDoImageSwitch = 1;
      schedule(7500, 0, RapierShieldApply, %zombie);
      schedule(10000,0,"eval",""@%zombie@".shotsFired=0;");
      return;
   }
   if(%zombie.shotsfired >= 100) {
      %zombie.setMoveState(false); //unfreeze him
      RapierShieldApply(%zombie);
      %zombie.canDoImageSwitch = 1;
      return;
   }
   %zombie.canDoImageSwitch = 0;
   %zombie.rapiershield = 0;
   %zombie.setMoveState(true); //freeze him
   %pos = %zombie.getMuzzlePoint(5);
   %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(5));
   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));

   %x = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %nvec = MatrixMulVector(%mat, %vec);

   %zombie.shotsfired++;
   %p = new LinearFlareProjectile() {
      dataBlock        = zMG42Projectile;
	  initialDirection = %nvec;
      initialPosition  = %pos;
      sourceObject     = %zombie;
	  sourceSlot       = 5;
   };
   schedule(50,0,"RogFire",%zombie,%target);
   if(%zombie.shotsfired == 1) {
      schedule(20000,0,"eval",""@%zombie@".shotsFired=0;");
   }
}

//------------------------------------------

datablock ParticleData(RapierParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.65;
   inheritedVelFactor   = 0.00;

   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 500;

   textureName          = "special/redbump2";

   colors[0]     = "85 26 139 1.0";
   colors[1]     = "85 26 139 1.0";
   colors[2]     = "85 26 139 0.0";

   sizes[0]      = 0.6;
   sizes[1]      = 0.6;
   sizes[2]      = 0.6;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(RapierEmitter) {
   ejectionPeriodMS = 25;
   periodVarianceMS = 5;

   ejectionVelocity = 1.0;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "RapierParticle";
};

datablock LinearFlareProjectileData(RapierShieldForwardProjectile) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.05;
   hasDamageRadius     = true;
   indirectDamage      = 0.15;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::RapierShield;

   explosion           = "";
   splash              = PlasmaSplash;

   baseEmitter        = RapierEmitter;

   dryVelocity       = 25.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 250;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.18 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = PlasmaProjectileSound;
   fireSound    = FlamethrowerFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.94";
};

function RapierShieldApply(%object, %counter) {
   if(!isObject(%object) || %object.getState() $= "dead") {
      return;
   }
   if(%counter $= "") {
      %counter = 0;
   }
   %object.state["preShield"] = 1;
   //------------------------------------------
   if(%counter < 100) {
      %newPosition = RapierShieldMath(%object, 1, %counter);
      //create the effect, schedule a quick delete
      %s = createEmitter(%newPosition, RapierEmitter, "0 0 0");
      %s.schedule(350, "Delete");
      schedule(75, 0, RapierShieldApply, %object, %counter++);
   }
   else {
      %object.rapierShield = 1;
      %object.state["preShield"] = 0;
      %object.state["canUseMG"] = 1;
      
      if(%object.state["shieldRecharge"]) {
         schedule(4000, 0, eval, ""@%object@".state[\"shieldRecharge\"] = 0;");
         schedule(4000, 0, eval, ""@%object@".state[\"canShieldAttack\"] = 1;");
      }
      else {
         schedule(450, 0, eval, ""@%object@".state[\"canShieldAttack\"] = 1;");
      }
      
      RapierShieldLoop(%object);
   }
}

function RapierShieldLoop(%object) {
   if(!isObject(%object) || %object.getState() $= "dead") {
      return;
   }
   if(!%object.rapierShield) {
      %object.state["canUseMG"] = 0;
      %object.state["canShieldAttack"] = 0;
      return;
   }
   if(%object.cantRestore) {
      return;
   }
   //====================================================
   //We want singular rotation around each of the effects
   //A basic rotation index should work perfectly here
   %s = createEmitter(vectoradd(%object.getPosition(), "0 0 2.5"), RapierEmitter, "1 0 0");
   %s.schedule(100, "Delete");
   //shield effect, schedule next loop
   %object.playShieldEffect("1 1 1");
   schedule(100, 0, "RapierShieldLoop", %object);
}

function RapierShieldAttack(%object, %val, %proj) {
   //echo(%object TAB %val TAB %totalTime);
   if(!isObject(%object) || %object.getState() $= "dead") {
      return;
   }
   if(!%val) {
      if(%object.state["shieldRecharge"]) {
         return; //we already are recharging, do not do it again
      }
      %object.state["shieldRecharge"] = 1;
      %object.state["canShieldAttack"] = 0;
      %object.setMoveState(false);
      //break off the shield now.
      cancel(%object.SALoop);
      //delete the projectile if it exists
      if(isObject(%object.rapierProjectile)) {
         %object.rapierProjectile.delete();
      }
      //return it
      schedule(4000, 0, RapierShieldApply, %object);
      return;
   }
   else {
      if(!%object.state["canShieldAttack"]) {
         return;
      }
      if(%object.state["shieldRecharge"]) {
         return;
      }
   
      %object.setMoveState(true);
      %object.setActionThread("cel1", true);
      %object.rapierShield = 0;
   
      if(%proj != 0) {
         %cPos = %object.rapierProjectile.position;
      }
      else {
         %cPos = vectoradd(%object.getEyePoint(), vectorscale(%object.getEyeVector(), 1.5)); //out 1.5M
         %object.rapierProjectile = new (linearflareprojectile)() {
            dataBlock        = RapierShieldForwardProjectile;
            initialDirection = %object.getEyeVector();
            initialPosition  = %cPos;
            sourceslot = %object;
         };
         %object.rapierProjectile.sourceobject = %object;
         MissionCleanup.add(%object.rapierProjectile);
      }

      %targetDirection = RapierShieldMath(%object, 2, %object.rapierProjectile);
      //create the projectile
      %object.rapierProjectile = new (linearflareprojectile)() {
         dataBlock        = RapierShieldForwardProjectile;
         initialDirection = %targetDirection;
         initialPosition  = %cPos;
         sourceslot = %object;
      };
      %object.rapierProjectile.sourceobject = %object;
      MissionCleanup.add(%object.rapierProjectile);
      //continuity stuff
      %object.SALoop = schedule(80, 0, "RapierShieldAttack", %object, %val, %object.rapierProjectile);
   }
}

function RapierShieldMath(%object, %type, %arguments) {
   if(!isObject(%object) || %object.getState() $= "dead") {
      return;
   }
   
   switch(%type) {
      case 1:
         //shield descend
         //%arguments: counter
         %counter = getField(%arguments, 0);
         %currentPos = vectorAdd(%object.getPosition(), "0 0 100");
         
         %z = getWord(%currentPos, 2);
         %zN = %z - (2*%counter);
         
         %newPos = getWord(%object.getPosition(), 0) SPC getWord(%object.getPosition(), 1) SPC (getWord(%object.getPosition(), 2) + %zN);
         return %newPos;
      case 2:
         //attack calculation
         //%arguments: %proj
         %proj = getField(%arguments, 0);
         %projpos = %proj.position;

         %projdir = %proj.initialDirection;

         if(!isobject(%proj)) {
            %object.SALoop = RapierShieldAttack(%object, 0, 0);
            return;
         }
         else {
            //do radius damage, let TWM2 handle the damage cancelation for friendly object
            %proj.getDatablock().onExplode(%proj, %projpos, 0);
            %proj.delete();
         }
         if(!isobject(%object)) {
            return;
         }

         //obtain the projectile direction
         %dirX = getWord(%projdir, 0);
         %dirY = getWord(%projdir, 1);
         %dirZ = getWord(%projdir, 2);
         
         //obtain the raycast target position
         %pos        = %object.getEyePoint();
         %vec        = %object.getEyeVector();
         %targetpos  = vectoradd(%pos, vectorscale(%vec, $Rog::RapierShieldMaxDist));
         
         %mask       = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::TurretObjectType | $TypeMasks::ItemObjectType;
         
         %ray        = containerraycast(%pos, %targetpos, %mask, %object);
         %targPoint  = getWords(%ray, 1, 3);
         if(%targPoint $= "") {
            %targPoint = vectoradd(%object.getEyePoint(), vectorscale(%object.getEyeVector(), $Rog::RapierShieldMaxDist));
         }

         %projdir = vectornormalize(vectorsub(%targPoint, %projpos));
         %newDirX = getWord(%projdir, 0);
         %newDirY = getWord(%projdir, 1);
         %newDirZ = getWord(%projdir, 2);

         %subX = %dirX - %newDirX;
         %finalDirX = ((%subX / 8) * -1) + %dirX;  //turn angle -> lower (inc), higher (dec)
         %subY = %dirY - %newDirY;
         %finalDirY = ((%subY / 10) * -1) + %dirY;  //turn angle -> lower (inc), higher (dec)
         %subZ = %dirZ - %newDirZ;
         %finalDirZ = ((%subZ / 10) * -1) + %dirZ;  //turn angle -> lower (inc), higher (dec)

         %newDirection = %finalDirX SPC %finalDirY SPC %finalDirZ;

         return %newDirection;
      case 3:
         //point of return rise
         //%arguments: %time
   }
}

//------------------------------------------
