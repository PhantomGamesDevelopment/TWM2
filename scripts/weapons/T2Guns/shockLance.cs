//--------------------------------------------------------------------------
// Shock Lance
// 
// 
//--------------------------------------------------------------------------
datablock AudioProfile(ShockLanceSwitchSound)
{
   filename    = "fx/weapons/shocklance_activate.wav";
   description = AudioClosest3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------
datablock AudioProfile(ShockLanceHitSound)
{
   filename    = "fx/weapons/shocklance_fire.WAV";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ShockLanceReloadSound)
{
   filename    = "fx/weapons/shocklance_reload.WAV";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ShockLanceDryFireSound)
{
   filename    = "fx/weapons/shocklance_dryfire.WAV";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ShockLanceMissSound)
{
   filename    = "fx/weapons/shocklance_miss.WAV";
   description = AudioExplosion3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Particle data
//--------------------------------------------------------------------------

//--------------------------------------
// Projectile
//--------------------------------------
datablock ShockLanceProjectileData(BasicShocker)
{
   directDamage        = 0.45;
   radiusDamageType    = $DamageType::ShockLance;
   kickBackStrength    = 2500;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 1800;
   boltLength = 14.0;
   extension = 14.0;            // script variable indicating distance you can shock people from
   lightningFreq = 25.0;
   lightningDensity = 3.0;
   lightningAmp = 0.25;
   lightningWidth = 0.05;

   shockwave = ShocklanceHit;
   							 
   boltSpeed[0] = 2.0;
   boltSpeed[1] = -0.5;

   texWrap[0] = 1.5;
   texWrap[1] = 1.5;

   startWidth[0] = 0.3;
   endWidth[0] = 0.6;
   startWidth[1] = 0.3;
   endWidth[1] = 0.6;
   
   texture[0] = "special/shockLightning01";
   texture[1] = "special/shockLightning02";
   texture[2] = "special/shockLightning03";
   texture[3] = "special/ELFBeam";

   emitter[0] = ShockParticleEmitter;
};

datablock ShockLanceProjectileData(S3Shocker) {
   directDamage        = 0.50;
   radiusDamageType    = $DamageType::ShockLance;
   kickBackStrength    = 2500;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 1800;
   boltLength = 30.0;
   extension = 30.0;            // script variable indicating distance you can shock people from
   lightningFreq = 25.0;
   lightningDensity = 3.0;
   lightningAmp = 0.25;
   lightningWidth = 0.05;

   shockwave = ShocklanceHit;

   boltSpeed[0] = 2.0;
   boltSpeed[1] = -0.5;

   texWrap[0] = 1.5;
   texWrap[1] = 1.5;

   startWidth[0] = 0.3;
   endWidth[0] = 0.6;
   startWidth[1] = 0.3;
   endWidth[1] = 0.6;

   texture[0] = "special/shockLightning01";
   texture[1] = "special/shockLightning02";
   texture[2] = "special/shockLightning03";
   texture[3] = "special/ELFBeam";

   emitter[0] = ShockParticleEmitter;
};

//--------------------------------------
// Rifle and item...
//--------------------------------------
datablock ItemData(ShockLance)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_shocklance.dts";
   image        = ShockLanceImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName   = "a shocklance";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(ShockLanceImage)
{
   classname = WeaponImage;
   shapeFile = "weapon_shocklance.dts";
   item = ShockLance;
   offset = "0 0 0";
   emap = true;

   projectile = BasicShocker;

   usesEnergy = true;
   missEnergy = 0;
   hitEnergy  = 15;
   minEnergy  = 15;       // needs to change to be datablock's energy drain for a hit

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateSound[0] = ShockLanceSwitchSound;
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "CheckWet";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.5;
   stateFire[3] = true;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = ShockLanceDryFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 2.0;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = ShockLanceReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Ready";

   stateName[6]                  = "DryFire";
   stateSound[6]                 = ShockLanceDryFireSound;
   stateTimeoutValue[6]          = 1.0;
   stateTransitionOnTimeout[6]   = "Ready";
   
   stateName[7] = "CheckWet";
   stateTransitionOnWet[7] = "DryFire";
   stateTransitionOnNotWet[7] = "Fire";
};

