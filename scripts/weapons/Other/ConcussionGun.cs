//IDEA By Signal360
datablock ParticleData(ConcGunSmokeParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;   // rises slowly
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 700;  // lasts 2 second
   lifetimeVarianceMS   = 150;   // ...more or less

   textureName          = "special/Lightning2Frame2";

   useInvAlpha = false;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0 0 1 1.0";
   colors[1]     = "0 1 1 1.0";
   colors[2]     = "0 1 1 0.0";

   sizes[0]      = 0.25;
   sizes[1]      = 1.0;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ConcGunSmokeEmitter) {
   ejectionPeriodMS = 15;
   periodVarianceMS = 5;

   ejectionVelocity = 1.25;
   velocityVariance = 0.50;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   particles = "ConcGunSmokeParticle";
};

datablock GrenadeProjectileData(ConcGunGrenade) {
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::Grenade;
   kickBackStrength    = 1500;
   bubbleEmitTime      = 1.0;
   
   ImageSource         = "ConcussionGunImage";

   sound               = GrenadeProjectileSound;
   explosion           = "ConcussionGrenadeExplosion";
   underwaterExplosion = "ConcussionGrenadeExplosion";
   velInheritFactor    = 0.5;
   splash              = GrenadeSplash;

   baseEmitter         = ConcGunSmokeEmitter;
   bubbleEmitter       = ConcGunSmokeEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 1000;
   muzzleVelocity    = 47.00;
   drag = 0.1;
};


//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(ConcussionGunAmmo) {
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_grenade.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some grenade launcher ammo";

   computeCRC = true;
   emap = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(ConcussionGun) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_mortar.dts";
   image = ConcussionGunImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a concussion gun";

   computeCRC = true;

};

datablock ShapeBaseImageData(ConcussionGunImage) {
   className = WeaponImage;
   shapeFile = "weapon_mortar.dts";
   item = ConcussionGun;
   ammo = ConcussionGunAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = ConcGunGrenade;
   projectileType = GrenadeProjectile;
   
   //ClipStuff
   ClipName = "ConcussionGunClip";
   ClipPickupName["ConcussionGunClip"] = "some Concussion Shells";
   ShowsClipInHud = 1;
   ClipReloadTime = 8;
   ClipReturn = 1;
   InitialClips = 4;
   
   GunName = "Concussion Gun";
   //
   RankRequire = $TWM2::RankRequire["ConcussionGun"];

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = MortarSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.4;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = MortarFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.5;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = GrenadeReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = GrenadeDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

function ConcGunGrenade::onExplode(%data, %proj, %pos, %mod) {
   Parent::OnExplode(%data, %proj, %pos, %mod);
   %TargetSearchMask = $TypeMasks::PlayerObjectType;
   InitContainerRadiusSearch(%pos, %data.damageRadius, %TargetSearchMask);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(%potentialTarget.isAlive()) {
         if(((%potentialTarget.client.team == %proj.sourceObject.client.team) && $TeamDamage) || %potentialTarget.client.team != %proj.sourceObject.client.team) {
            %potentialTarget.throwWeapon(1);
            %potentialTarget.throwWeapon(0);
            %potentialTarget.setWhiteout(4.0);
            %potentialTarget.setMoveState(true);
            %potentialTarget.schedule(4000, "SetMoveState", false);
            %potentialTarget.setActionThread("death4",true);
            ZappyLoop(%potentialTarget, 0, 7);
         }
      }
   }
}

function ZappyLoop(%potentialTarget, %time, %maxTime) {
   if(%time > %maxTime) {
      return;
   }
   if(%potentialTarget.isAlive()) {
      %time++;
      %potentialTarget.zapObject();
      schedule(750, 0, "ZappyLoop", %potentialTarget, %time, %maxTime);
   }
}
