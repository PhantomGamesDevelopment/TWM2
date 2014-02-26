//--------------------------------------
// Semi-Automatic Shotgun (SA2400)
// By. DarknessOfLight
//--------------------------------------

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------


datablock DebrisData( SemiAutoShellDebris )
{
   shapeName = "weapon_chaingun_ammocasing.dts";

   lifetime = 7.0;

   minSpinSpeed = 600.0;
   maxSpinSpeed = 800.0;

   elasticity = 0.8;
   friction = 0.3;

   numBounces = 5;

   fade = true;
   staticOnMaxBounce = true;
   snapOnMaxBounce = true;
};


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------

datablock TracerProjectileData( SA2400Slug )
{
   doDynamicClientHits = true;

   directDamage        = 0.3;
   directDamageType    = $DamageType::SA2400;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   
   HeadMultiplier      = 1.9;

   kickBackStrength  = 428.0;
   sound 				= ChaingunProjectile;

   ImageSource         = "SA2400Image";

   dryVelocity       = 2561.0;
   wetVelocity       = 586.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 40.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 40.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 37.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal1;
   decalData[2] = ChaingunDecal1;
   decalData[3] = ChaingunDecal1;
   decalData[4] = ChaingunDecal1;
   decalData[5] = ChaingunDecal1;
};
//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData( SA2400Ammo )
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some chaingun ammo";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData( SA2400Image )
{
   className = WeaponImage;
   shapeFile = "weapon_grenade_launcher.dts";
   item      = SA2400;
   ammo 	 = SA2400Ammo;
   
   offset = "0 0 0";
   emap = true;

   casing              = SemiAutoShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 30.0;

   projectile = SA2400Slug;
   projectileType = TracerProjectile;
   
   //ClipStuff
   ClipName = "SA2400Clip";
   ClipPickupName["SA2400Clip"] = "some SA2400 Slugs";
   ShowsClipInHud = 1;
   ClipReloadTime = 6;
   ClipReturn = 21;
   InitialClips = 8;
   ReloadSingle = 1;
   SingleShotAdd = 7;
   //
   RankRequire = $TWM2::RankRequire["SA2400"];
   
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "SA2400 Killer\t50\t100\tNone";
   Challenge[2] = "SA2400 Extremist\t100\t150\tNone";
   Challenge[3] = "SA2400 Expert\t250\t250\tNone";
   Challenge[4] = "SA2400 Master\t500\t500\tNone";
   Challenge[5] = "SA2400 God\t1000\t1000\tNone";
   Challenge[6] = "SA2400 Bronze Commendation\t2500\t10000\tNone";
   Challenge[7] = "SA2400 Silver Commendation\t5000\t25000\tNone";
   Challenge[8] = "SA2400 Gold Commendation\t10000\t50000\tNone";
   Challenge[9] = "SA2400 Titan Commendation\t25000\t75000\tNone";
   GunName = "SA2400 Shotgun";
   //
   
   projectileSpread = 3.0 / 1000.0;
//----------------------------------------------------------------------------\\
//State Data\\
//----------------------------------------------------------------------------\\
   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateSequence[0] = "Activate";
   stateTimeoutValue[0] = 0.2;
   stateSound[0] = ChaingunSwitchSound;
   //----------------------------------------------------\\
   stateName[1] = "Ready";
   stateSequence[1] = "Ready";
   stateTimeoutValue[1] = 0.2;
   stateTransitionOnTriggerDown[1] = "FireFirstRound";
   stateTransitionOnNoAmmo[1] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[2] = "Reload";
   stateSequence[2] = "Reload";
   stateTransitionOnTimeout[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTimeoutValue[2] = 4.0;
   stateAllowImageChange[2] = false;
   stateSound[2] = DiscDryFireSound;
   //----------------------------------------------------\\
   stateName[3] = "NoAmmo";
   stateSequence[3] = "NoAmmo";
   stateTimeoutValue[3] = 0.2;
   stateTransitionOnTriggerDown[3] = "DryFire";
   stateTransitionOnAmmo[3] = "Reload";
   //----------------------------------------------------\\
   stateName[4] = "DryFire";
   stateSequence[4] = "DryFire";
   stateTransitionOnTimeout[4] = "NoAmmo";
   stateTimeoutValue[4] = 0.5;
   stateSound[4] = DiscSwitchSound;
   //----------------------------------------------------\\
   stateName[5] = "FireFirstRound";
   stateSequence[5] = "Fire";
   stateRecoil[5] = LightRecoil;
   stateTransitionOnTimeout[5] = "FireSecondRound";
   stateTransitionOnTriggerUp[5] = "ReadySecondRound";
   stateTransitionOnNoAmmo[5] = "NoAmmo";
   stateTimeoutValue[5] = 0.2;
   stateFire[5] = true;
   stateEjectShell[5] = true;
   stateAllowImageChange[5] = false;
   stateSound[5] = ShotgunFireSound;
   stateScript[5] = "onFire";
   //----------------------------------------------------\\
   stateName[6] = "FireSecondRound";
   stateSequence[6] = "Fire";
   stateRecoil[6] = LightRecoil;
   stateTransitionOnTimeout[6] = "FireThirdRound";
   stateTransitionOnTriggerUp[6] = "ReadyThirdRound";
   stateTransitionOnNoAmmo[6] = "NoAmmo";
   stateTimeoutValue[6] = 0.2;
   stateFire[6] = true;
   stateEjectShell[6] = true;
   stateAllowImageChange[6] = false;
   stateSound[6] = ShotgunFireSound;
   stateScript[6] = "onFire";
   //----------------------------------------------------\\
   stateName[7] = "FireThirdRound";
   stateSequence[7] = "Fire";
   stateRecoil[7] = LightRecoil;
   stateTransitionOnTimeout[7] = "FireFourthRound";
   stateTransitionOnTriggerUp[7] = "ReadyFourthRound";
   stateTransitionOnNoAmmo[7] = "NoAmmo";
   stateTimeoutValue[7] = 0.2;
   stateFire[7] = true;
   stateEjectShell[7] = true;
   stateAllowImageChange[7] = false;
   stateSound[7] = ShotgunFireSound;
   stateScript[7] = "onFire";
   //----------------------------------------------------\\
   stateName[8] = "FireFourthRound";
   stateSequence[8] = "Fire";
   stateRecoil[8] = LightRecoil;
   stateTransitionOnTimeout[8] = "FireFifthRound";
   stateTransitionOnTriggerUp[8] = "ReadyFifthRound";
   stateTransitionOnNoAmmo[8] = "NoAmmo";
   stateTimeoutValue[8] = 0.2;
   stateFire[8] = true;
   stateEjectShell[8] = true;
   stateAllowImageChange[8] = false;
   stateSound[8] = ShotgunFireSound;
   stateScript[8] = "onFire";
   //----------------------------------------------------\\
   stateName[9] = "FireFifthRound";
   stateSequence[9] = "Fire";
   stateRecoil[9] = LightRecoil;
   stateTransitionOnTimeout[9] = "FireSixthRound";
   stateTransitionOnTriggerUp[9] = "ReadySixthRound";
   stateTransitionOnNoAmmo[9] = "NoAmmo";
   stateTimeoutValue[9] = 0.2;
   stateFire[9] = true;
   stateEjectShell[9] = true;
   stateAllowImageChange[9] = false;
   stateSound[9] = ShotgunFireSound;
   stateScript[9] = "onFire";
   //----------------------------------------------------\\
   stateName[10] = "FireSixthRound";
   stateSequence[10] = "Fire";
   stateRecoil[10] = LightRecoil;
   stateTransitionOnTimeout[10] = "FireSeventhRound";
   stateTransitionOnTriggerUp[10] = "ReadySeventhRound";
   stateTransitionOnNoAmmo[10] = "NoAmmo";
   stateTimeoutValue[10] = 0.2;
   stateFire[10] = true;
   stateEjectShell[10] = true;
   stateAllowImageChange[10] = false;
   stateSound[10] = ShotgunFireSound;
   stateScript[10] = "onFire";
   //----------------------------------------------------\\
   stateName[11] = "FireSeventhRound";
   stateSequence[11] = "Fire";
   stateRecoil[11] = LightRecoil;
   stateTransitionOnTimeout[11] = "Reload";
   stateTransitionOnTriggerUp[11] = "Reload";
   stateTransitionOnNoAmmo[11] = "NoAmmo";
   stateTimeoutValue[11] = 0.2;
   stateFire[11] = true;
   stateEjectShell[11] = true;
   stateAllowImageChange[11] = false;
   stateSound[11] = ShotgunFireSound;
   stateScript[11] = "onFire";
   //----------------------------------------------------\\
   stateName[12] = "ReadySecondRound";
   stateSequence[12] = "ReadySecondRound";
   stateTransitionOnTriggerDown[12] = "FireSecondRound";
   stateTransitionOnNoAmmo[12] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[13] = "ReadyThirdRound";
   stateSequence[13] = "ReadyThirdRound";
   stateTransitionOnTriggerDown[13] = "FireThirdRound";
   stateTransitionOnNoAmmo[13] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[14] = "ReadyFourthRound";
   stateSequence[14] = "ReadyFourthRound";
   stateTransitionOnTriggerDown[14] = "FireFourthRound";
   stateTransitionOnNoAmmo[14] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[15] = "ReadyFifthRound";
   stateSequence[15] = "ReadyFifthRound";
   stateTransitionOnTriggerDown[15] = "FireFifthRound";
   stateTransitionOnNoAmmo[15] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[16] = "ReadySixthRound";
   stateSequence[16] = "ReadySixthRound";
   stateTransitionOnTriggerDown[16] = "FireSixthRound";
   stateTransitionOnNoAmmo[16] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[17] = "ReadySeventhRound";
   stateSequence[17] = "ReadySeventhRound";
   stateTransitionOnTriggerDown[17] = "FireSeventhRound";
   stateTransitionOnNoAmmo[17] = "NoAmmo";
   //----------------------------------------------------\\
   stateName[18] = "ActivateReady";
   stateTransitionOnLoaded[18] = "Ready";
   stateTransitionOnNoAmmo[18] = "NoAmmo";
//----------------------------------------------------------------------------\\
//----------------------------------------------------------------------------\\
};

datablock ItemData( SA2400 )
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_grenade_launcher.dts";
   image        = SA2400Image;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a Semi-Automatic Shotgun";

   computeCRC = true;
   emap = true;
};
