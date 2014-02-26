datablock ParticleData(NMMissileBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;

   useInvAlpha = false;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9 1.0";

   sizes[0]      = 2.5;
   sizes[1]      = 2.7;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NMMissileBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "NMMissileBaseParticle";
};

datablock LinearFlareProjectileData(ShadowRifleBolt) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.02;
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Shadow;

   explosion           = "BlasterExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = NMMissileBaseEmitter;
   
   ImageSource         = "ShadowRifleImage";

   dryVelocity       = 120.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 250;
   lifetimeMS        = 8000;
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
   flareColor        = "1 0.18 0.03";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = FlamethrowerFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

datablock ItemData(ShadowRifle) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_sniper.dts";
   image        = ShadowRifleImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a shadow rifle";

   computeCRC = true;

};

datablock ShapeBaseImageData(ShadowRifleImage) {
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   item = ShadowRifle;
   projectile = ShadowRifleBolt;
   projectileType = LinearFlareProjectile;

   MedalRequire = 1;

   usesEnergy = true;
   minEnergy = 6;

   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "ActivateReady";
   stateSound[0]                    = SniperRifleSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateSequence[0]                 = "Activate";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnLoaded[1]       = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckWet";

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 0.5;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";

   stateName[4]                     = "Reload";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 2.5;
   stateAllowImageChange[4]         = false;

   stateName[5]                     = "CheckWet";
   stateTransitionOnWet[5]          = "DryFire";
   stateTransitionOnNotWet[5]       = "Fire";

   stateName[6]                     = "NoAmmo";
   stateTransitionOnAmmo[6]         = "Reload";
   stateTransitionOnTriggerDown[6]  = "DryFire";
   stateSequence[6]                 = "NoAmmo";

   stateName[7]                     = "DryFire";
   stateSound[7]                    = SniperRifleDryFireSound;
   stateTimeoutValue[7]             = 0.5;
   stateTransitionOnTimeout[7]      = "Ready";
};

function ShadowRifleBolt::onCollision(%data,%projectile,%targetObject,%modifier,%position,%normal) {
   Parent::OnCollision(%data,%projectile,%targetObject,%modifier,%position,%normal);
   %cn = %targetObject.getClassName();
   if(%cn $= "Player") {
      if($Host::TeamDamageOn == 0) {
         if(%targetObject.team == %projectile.sourceObject.team) {
            return;
         }
      }
      if(%targetObject.beingSapped) {
         return;
      }
      if(%targetObject.isBoss) {
         %targetObject.playShieldEffect("1 1 1");
         %vec = vectorsub(%projectile.sourceObject.getworldboxcenter(),vectorAdd(%targetObject.getPosition(), "0 0 1"));
         %vec = vectoradd(%vec, vectorscale(%projectile.sourceObject.getvelocity(),vectorlen(%vec)/100));
         %p = new LinearFlareProjectile() {
            dataBlock        = ShadowRifleBolt; //burn :)
            initialDirection = %vec;
            initialPosition  = vectorAdd(%targetObject.getPosition(), "0 0 1");
            sourceObject     = %targetObject;
            sourceSlot       = 0;
         };
         messageClient(%projectile.sourceObject.client, 'MsgWarn', "WARNING: The boss deflects the pulse back at you!");
         return;
      }
      else {
         %targetObject.beingSapped = 1;
         SapLoop(%projectile.sourceObject, %targetObject);
      }
   }
}

function SapLoop(%source, %target) {
   if(!%source.isAlive()) {
      return;
   }
   if(!%target.isAlive()) {
      createBlood(%target);
      return;
   }
   %target.zapObject();
   %target.getDatablock().damageObject(%target, %source, %source.getPosition, 0.05, $DamageType::Shadow);
   %target.lastDamagedImage = "ShadowRifleImage";
   %source.applyRepair(0.05);
   schedule(500, 0, "SapLoop", %source, %target);
}
