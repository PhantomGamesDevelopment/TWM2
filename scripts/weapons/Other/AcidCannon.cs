// Acid Cannon
datablock AudioProfile(AcidCannonPreFireSound) {
   filename    = "fx/weapons/targetinglaser_paint.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(NerfBallExplosionSound) {
   filename = "fx/weapons/cg_water2.wav";
   description = AudioClosest3d;
   preload = true;
};

//acid bomb
datablock ParticleData(ZAcidBombParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.5;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 4000;
   lifetimeVarianceMS   = 500;

   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName          = "special/bubbles";

   colors[0]     = "0.0 1.0 0.5 0.3";
   colors[1]     = "0.0 1.0 0.4 0.2";
   colors[2]     = "0.0 1.0 0.3 0.1";

   sizes[0]      = 3.0;
   sizes[1]      = 2.3;
   sizes[2]      = 2.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ZAcidBombExplosionEmitter) {
   lifetimeMS       = 2000;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 3.0;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "ZAcidBombParticle";
};

datablock ExplosionData(ZAcidBombExplosion) {
   emitter[0]   = ZAcidBombExplosionEmitter;
   soundProfile = NerfBallExplosionSound;
};
//

datablock ParticleData(ZAcidBallParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.5;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 200;

   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName          = "special/bubbles";

   colors[0]     = "0.0 1.0 0.5 0.3";
   colors[1]     = "0.0 1.0 0.4 0.2";
   colors[2]     = "0.0 1.0 0.3 0.1";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ZAcidBallExplosionEmitter) {
   lifetimeMS       = 200;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 3.0;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "ZAcidBallParticle";
};

datablock ExplosionData(ZAcidBallExplosion) {
   emitter[0]   = ZAcidBallExplosionEmitter;
   soundProfile = NerfBallExplosionSound;
};

datablock TracerProjectileData(LZombieAcidBall) {
   doDynamicClientHits = true;

   projectileShapeName = "";
   directDamage        = 0.0;
   directDamageType    = $DamageType::ZAcid;
   hasDamageRadius     = true;
   indirectDamage      = 0.24;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::ZAcid;
   sound          	   = BlasterProjectileSound;
   explosion           = ZAcidBallExplosion;
   
   ImageSource         = "AcidCannonImage";

   dryVelocity       = 100.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 1000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;

   tracerLength    = 5;
   tracerAlpha     = false;
   tracerMinPixels = 3;
   tracerColor     = "0 1 0 1";
	tracerTex[0]  	 = "special/landSpikeBolt";
	tracerTex[1]  	 = "special/landSpikeBoltCross";
	tracerWidth     = 0.5;
   crossSize       = 0.79;
   crossViewAng    = 0.990;
   renderCross     = true;
   emap = true;
};

datablock TracerProjectileData(EliteRapierAcidBomb) {
   doDynamicClientHits = true;

   projectileShapeName = "";
   directDamage        = 0.0;
   directDamageType    = $DamageType::ZAcid;
   hasDamageRadius     = true;
   indirectDamage      = 0.6;
   damageRadius        = 7.5;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::ZAcid;
   sound          	   = BlasterProjectileSound;
   explosion           = ZAcidBombExplosion;

   ImageSource         = "AcidCannonImage";

   dryVelocity       = 100.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 1000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;

   tracerLength    = 10;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = "0 1 0 1";
	tracerTex[0]  	 = "special/landSpikeBolt";
	tracerTex[1]  	 = "special/landSpikeBoltCross";
	tracerWidth     = 0.8;
   crossSize       = 0.79;
   crossViewAng    = 0.990;
   renderCross     = true;
   emap = true;
};


datablock TracerProjectileData(SSZombieAcidBall) {
   doDynamicClientHits = true;

   projectileShapeName = "";
   directDamage        = 0.0;
   directDamageType    = $DamageType::ZAcid;
   hasDamageRadius     = true;
   indirectDamage      = 0.4;
   damageRadius        = 7.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::ZAcid;
   sound          	   = BlasterProjectileSound;
   explosion           = ZAcidBallExplosion;

   dryVelocity       = 400.0;
   wetVelocity       = 400.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 4000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;

   tracerLength    = 5;
   tracerAlpha     = false;
   tracerMinPixels = 3;
   tracerColor     = "0 1 0 1";
	tracerTex[0]  	 = "special/landSpikeBolt";
	tracerTex[1]  	 = "special/landSpikeBoltCross";
	tracerWidth     = 0.5;
   crossSize       = 0.79;
   crossViewAng    = 0.990;
   renderCross     = true;
   emap = true;
};

datablock ShapeBaseImageData(AcidCannonImage) {
   className = WeaponImage;
   shapeFile = "weapon_energy.dts";
   item = AcidCannon;
   projectile = LZombieAcidBall;
   projectileType = TracerProjectile;
   
   HasChallenges = 0;
   //RankRequire = $TWM2::RankRequire["PulsePhaser"];
   //No rank required, this weapon is picked up from killed zombies

   usesEnergy = true;
   fireEnergy = 4;
   minEnergy = 4;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.3;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   //stateSound[3] = PPWFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 4.2;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = BlasterDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

datablock ItemData(AcidCannon) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = AcidCannonImage;
   mass = 1;
   alwaysAllowPickup = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "an Acid Cannon";
};

function AcidCannonImage::onMount(%this, %obj, %slot) {
   Parent::onMount(%this, %obj, %slot);
}

function AcidCannonImage::onUnmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
}

function AcidCannonImage::onFire(%data,%obj,%slot) {
   AcidCannonAttack(%obj, %slot, 0);
}

function AcidCannonAttack(%object, %slot, %chargecount) {
   %muzzleVec = %object.getMuzzleVector(%slot);
   %muzzlePt = %object.getMuzzlePoint(%slot);
   if(%chargecount <= 5) {
      if(%chargecount == 1) {
         serverPlay3D(AcidCannonPreFireSound, %muzzlePt);
      }
      %charge = new ParticleEmissionDummy() {
         position = %muzzlePt;
   	     dataBlock = "defaultEmissionDummy";
   	     emitter = "BurnEmitter";
      };
      MissionCleanup.add(%charge);
      %charge.schedule(100, "delete");
   }
   if(%chargecount == 7) {
      %p = new TracerProjectile() {
         dataBlock        = LZombieAcidBall;
  	     initialDirection = %muzzleVec;
         initialPosition  = %muzzlePt;
         sourceObject     = %object;
         sourceSlot       = 0;
      };
      MissionCleanup.add(%p);
      serverPlay3D(CentaurArtilleryFireSound, %muzzlePt);
   }
   if(%chargecount == 9) {
      %p = new TracerProjectile() {
         dataBlock        = LZombieAcidBall;
  	     initialDirection = %muzzleVec;
         initialPosition  = %muzzlePt;
         sourceObject     = %object;
         sourceSlot       = 0;
      };
      MissionCleanup.add(%p);
      serverPlay3D(CentaurArtilleryFireSound, %muzzlePt);
   }
   //
   if(%chargecount <= 9) {
      %chargecount++;
      schedule(100, 0, "AcidCannonAttack", %object, %slot, %chargecount);
   }
}
