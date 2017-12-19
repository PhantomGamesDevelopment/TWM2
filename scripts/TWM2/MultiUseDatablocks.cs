//
// MultiUseDatablocks.cs
// TWM2 3.9.2
// Phantom139
//
// Contains datablock instances that are shared across multiple script files in the mod to allow for removal of re-definitions
//  Any datablock that requires use in multiple weapons, or in special instance objects should be defined in this file as it is
//  loaded first in the mod instances.

//**********************************************
// AUDIO DATABLOCKS
//**********************************************
datablock AudioProfile(ZombieMoan) {
	filename    = "fx/environment/growl3.wav";
	description = AudioClose3d;
	preload = true;
};

datablock AudioProfile(ZombieHOWL) {
	filename    = "fx/environment/Yeti_Howl1.wav";
	description = AudioBomb3d;
	preload = true;
};

//**********************************************
// PARTICLE DATABLOCKS
//**********************************************
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

datablock ParticleData(ThrowerBaseParticle) {
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

	colors[0]     = "1.0 0.6 0.4 1.0";
	colors[1]     = "1.0 0.5 0.2 1.0";
	colors[2]     = "1.0 0.25 0.1 0.0";

	sizes[0]      = 0.5;
	sizes[1]      = 0.7;
	sizes[2]      = 1.0;

	times[0]      = 0.0;
	times[1]      = 0.7;
	times[2]      = 1.0;
};

datablock ParticleData(DemonFBSmokeParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 2500;  
	lifetimeVarianceMS   = 500;

	textureName          = "particleTest";

	useInvAlpha =     true;

	spinRandomMin = -60.0;
	spinRandomMax = 60.0;

	colors[0]     = "0.5 0.5 0.5 0.5";
	colors[1]     = "0.4 0.4 0.4 0.2";
	colors[2]     = "0.3 0.3 0.3 0.0";
	
	sizes[0]      = 0.5;
	sizes[1]      = 1.75;
	sizes[2]      = 3.0;
	
	times[0]      = 0.0;
	times[1]      = 0.5;
	times[2]      = 1.0;
};

datablock ParticleData(InflictionNightmareGlobeSmoke) {
	dragCoefficient = 50;/////////-----------------------
	gravityCoefficient = 0.0;
	inheritedVelFactor = 1.0;
	constantAcceleration = 0.0;
	lifetimeMS = 5050;
	lifetimeVarianceMS = 0;
	useInvAlpha = true;
	spinRandomMin = -360.0;
	spinRandomMax = 360.0;
	textureName = "particleTest";

	colors[0] = "0.5 0.1 0.9 1.0";
	colors[1] = "0.5 0.1 0.9 1.0";
	colors[2] = "0.5 0.1 0.9 1.0";
	colors[3] = "0.5 0.1 0.9";

	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 1.0;
	sizes[3] = 1.0;

	times[0] = 0.0;
	times[1] = 0.33;
	times[2] = 0.66;
	times[3] = 1.0;

	mass = 0.7;
	elasticity = 0.2;
	friction = 1;
	computeCRC = true;
	haslight = true;
	lightType = "PulsingLight";
	lightColor = "0.2 0.0 0.5 1.0";
	lightTime = "200";
	lightRadius = "2.0";
};

datablock ParticleData(NightmareGlobeSmoke) {
	dragCoefficient = 50;/////////-----------------------
	gravityCoefficient = 0.0;
	inheritedVelFactor = 1.0;
	constantAcceleration = 0.0;
	lifetimeMS = 5050;
	lifetimeVarianceMS = 0;
	useInvAlpha = true;
	spinRandomMin = -360.0;
	spinRandomMax = 360.0;
	textureName = "particleTest";
	
	colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
	colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
	colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
	colors[3] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
	
	sizes[0] = 1.0;
	sizes[1] = 1.0;
	sizes[2] = 1.0;
	sizes[3] = 1.0;
	
	times[0] = 0.0;
	times[1] = 0.33;
	times[2] = 0.66;
	times[3] = 1.0;
	
	mass = 0.7;
	elasticity = 0.2;
	friction = 1;
	computeCRC = true;
	haslight = true;
	lightType = "PulsingLight";
	lightColor = "0.2 0.0 0.5 1.0";
	lightTime = "200";
	lightRadius = "2.0";
};

datablock ParticleData(GreenEmitParticle) {
	dragCoeffiecient     = 1;
	gravityCoefficient   = -0.3;   // rises slowly
	inheritedVelFactor   = 0;

	lifetimeMS           =  300;
	lifetimeVarianceMS   =  0;
	useInvAlpha          =  false;
	spinRandomMin        = 0.0;
	spinRandomMax        = 0.0;

	animateTexture = false;

	textureName = "flareBase"; // "special/Smoke/bigSmoke"

	colors[0]     = "0 1 0";
	colors[1]     = "0 1 0";
	colors[2]     = "0 1 0";

	sizes[0]      = 0.8;
	sizes[1]      = 0.8;
	sizes[2]      = 0.8;

	times[0]      = 0.0;
	times[1]      = 1.0;
	times[2]      = 5.0;
};

datablock ParticleData(PurpleNightmareEmitParticle) {
   dragCoeffiecient     = 1;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           =  300;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = 0.0;
   spinRandomMax        = 0.0;

   animateTexture = false;

   textureName = "flareBase"; // "special/Smoke/bigSmoke"

   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9";

   sizes[0]      = 0.4;
   sizes[1]      = 0.4;
   sizes[2]      = 0.4;

   times[0]      = 0.0;
   times[1]      = 1.0;
   times[2]      = 5.0;

};

//**********************************************
// PARTICLE EMITTER DATABLOCKS
//**********************************************
datablock ParticleEmitterData(NMMissileBaseEmitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 1.5;
	velocityVariance = 0.3;

	thetaMin         = 0.0;
	thetaMax         = 30.0;

	particles = "NMMissileBaseParticle";
};

datablock ParticleEmitterData(NightmareGlobeEmitter) {
	ejectionPeriodMS = 0.1;
	periodVarianceMS = 0;
	
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	
	ejectionOffset = 5;
	thetaMin = 0;
	thetaMax = 180;
	
	overrideAdvances = false;
	particles = "NightmareGlobeSmoke";
};

datablock ParticleEmitterData(InfNightmareGlobeEmitter) {
	ejectionPeriodMS = 0.1;
	periodVarianceMS = 0;
	
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	
	ejectionOffset = 5;
	thetaMin = 0;
	thetaMax = 180;
	
	overrideAdvances = false;
	particles = "InflictionNightmareGlobeSmoke";
};

datablock ParticleEmitterData(PulseGreenEmitter) {
	ejectionPeriodMS = 2;
	periodVarianceMS = 1;

	ejectionVelocity = 10;
	velocityVariance = 0;

	thetaMin         = 89.0;
	thetaMax         = 90.0;

	orientParticles = false;

	particles = "GreenEmitParticle";
};

datablock ParticleEmitterData(YvexSniperEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;

   ejectionVelocity = 10;
   velocityVariance = 0;

   thetaMin         = 89.0;
   thetaMax         = 90.0;

   orientParticles = false;

   particles = "PurpleNightmareEmitParticle";
};

datablock ParticleEmitterData(DemonFBSmokeEmitter) {
	ejectionPeriodMS = 7;
	periodVarianceMS = 0;

	ejectionVelocity = 0.75;  // A little oomph at the back end
	velocityVariance = 0.2;

	thetaMin         = 0.0;
	thetaMax         = 180.0;

	particles = "DemonFBSmokeParticle";
};

datablock ParticleEmitterData(ThrowerBaseEmitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 1.5;
	velocityVariance = 0.3;

	thetaMin         = 0.0;
	thetaMax         = 30.0;

	particles = "ThrowerBaseParticle";
};

//**********************************************
// PROJECTILE DATABLOCKS
//**********************************************
datablock GrenadeProjectileData(DemonFireball) {
	projectileShapeName = "plasmabolt.dts";
	emitterDelay        = -1;
	directDamage        = 0.0;
	hasDamageRadius     = true;
	indirectDamage      = 0.4;
	damageRadius        = 5.0; // z0dd - ZOD, 8/13/02. Was 20.0
	radiusDamageType    = $DamageType::zombie;
	kickBackStrength    = 1500;

	explosion           = "PlasmaBoltExplosion";
	underwaterExplosion = "PlasmaBoltExplosion";
	velInheritFactor    = 0;
	splash              = PlasmaSplash;
	depthTolerance      = 100.0;

	baseEmitter         = DemonFBSmokeEmitter;
	bubbleEmitter       = DemonFBSmokeEmitter;

	grenadeElasticity = 0;
	grenadeFriction   = 0.4;
	armingDelayMS     = -1; // z0dd - ZOD, 4/14/02. Was 2000

	gravityMod        = 0.4;  // z0dd - ZOD, 5/18/02. Make mortar projectile heavier, less floaty
	muzzleVelocity    = 50.0; // z0dd - ZOD, 8/13/02. More velocity to compensate for higher gravity. Was 63.7
	drag              = 0;
	sound	     = PlasmaProjectileSound;

	hasLight    = true;
	lightRadius = 10;
	lightColor  = "1 0.75 0.25";

	hasLightUnderwaterColor = true;
	underWaterLightColor = "1 0.75 0.25";
};

datablock GrenadeProjectileData(DemonFlamingFireball) {
	projectileShapeName = "plasmabolt.dts";
	emitterDelay        = -1;
	directDamage        = 0.0;
	hasDamageRadius     = true;
	indirectDamage      = 0.4;
	damageRadius        = 5.0; // z0dd - ZOD, 8/13/02. Was 20.0
	radiusDamageType    = $DamageType::Fire;
	kickBackStrength    = 1500;

	explosion           = "PlasmaBoltExplosion";
	underwaterExplosion = "PlasmaBoltExplosion";
	velInheritFactor    = 0;
	splash              = PlasmaSplash;
	depthTolerance      = 100.0;

	baseEmitter         = ThrowerBaseEmitter;
	bubbleEmitter       = ThrowerBaseEmitter;

	grenadeElasticity = 0;
	grenadeFriction   = 0.4;
	armingDelayMS     = -1; // z0dd - ZOD, 4/14/02. Was 2000

	gravityMod        = 0.4;  // z0dd - ZOD, 5/18/02. Make mortar projectile heavier, less floaty
	muzzleVelocity    = 50.0; // z0dd - ZOD, 8/13/02. More velocity to compensate for higher gravity. Was 63.7
	drag              = 0;
	sound	     = PlasmaProjectileSound;

	hasLight    = true;
	lightRadius = 10;
	lightColor  = "1 0.75 0.25";

	hasLightUnderwaterColor = true;
	underWaterLightColor = "1 0.75 0.25";
};

datablock SeekerProjectileData(DMMissile) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.5;
	damageRadius        = 5.0;
	radiusDamageType    = $DamageType::Zombie;
	kickBackStrength    = 2000;

	explosion           = "MissileExplosion";
	splash              = MissileSplash;
	velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
								 // is cranked up to full so the missile doesn't start
								 // out behind the player when the player is moving
								 // very quickly - bramage

	baseEmitter         = MortarSmokeEmitter;
	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;
	bubbleEmitter       = GrenadeBubbleEmitter;
	bubbleEmitTime      = 1.0;

	exhaustEmitter      = MissileLauncherExhaustEmitter;
	exhaustTimeMs       = 300;
	exhaustNodeName     = "muzzlePoint1";

	lifetimeMS          = 10000; // z0dd - ZOD, 4/14/02. Was 6000
	muzzleVelocity      = 10.0;
	maxVelocity         = 35.0; // z0dd - ZOD, 4/14/02. Was 80.0
	turningSpeed        = 23.0;
	acceleration        = 15.0;

	proximityRadius     = 2.5;

	terrainAvoidanceSpeed = 10;
	terrainScanAhead      = 7;
	terrainHeightFail     = 1;
	terrainAvoidanceRadius = 3;

	flareDistance = 40;
	flareAngle    = 20;
	minSeekHeat   = 0.0;

	sound = MissileProjectileSound;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.2 0.05 0";

	useFlechette = true;
	flechetteDelayMs = 250;
	casingDeb = FlechetteDebris;

	explodeOnWaterImpact = false;
};

datablock LinearFlareProjectileData(DMPlasma) {
	doDynamicClientHits = true;

	directDamage        = 0;
	directDamageType    = $DamageType::Zombie;
	hasDamageRadius     = true;
	indirectDamage      = 0.8;  // z0dd - ZOD, 4/25/02. Was 0.5
	damageRadius        = 15.0;
	kickBackStrength    = 1500;
	radiusDamageType    = $DamageType::Zombie;
	explosion           = MortarExplosion;
	splash              = PlasmaSplash;

	dryVelocity       = 85.0; // z0dd - ZOD, 4/25/02. Was 50. Velocity of projectile out of water
	wetVelocity       = -1;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 4000;
	lifetimeMS        = 2500; // z0dd - ZOD, 4/25/02. Was 6000
	explodeOnDeath    = true;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = -1;

	activateDelayMS = 100;

	scale             = "3.0 3.0 3.0";
	numFlares         = 30;
	flareColor        = "0.1 0.3 1.0";
	flareModTexture   = "flaremod";
	flareBaseTexture  = "flarebase";
};

datablock SeekerProjectileData(BossMissiles) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.1;
	damageRadius        = 6.0;
	radiusDamageType    = $DamageType::MissileTurret;
	kickBackStrength    = 500;

	flareDistance = 200;
	flareAngle    = 30;
	minSeekHeat   = 0.0;

	explosion           = "MissileExplosion";
	velInheritFactor    = 1.0;

	splash              = MissileSplash;
	baseEmitter         = MortarSmokeEmitter;
	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;

	lifetimeMS          = 15000; // z0dd - ZOD, 4/14/02. Was 6000
	muzzleVelocity      = 12.0;
	maxVelocity         = 225.0; // z0dd - ZOD, 4/14/02. Was 80.0
	turningSpeed        = 50.0;
	acceleration        = 100.0;

	proximityRadius     = 4;

	terrainAvoidanceSpeed = 100;
	terrainScanAhead      = 50;
	terrainHeightFail     = 50;
	terrainAvoidanceRadius = 150;

	useFlechette = true;
	flechetteDelayMs = 225;
	casingDeb = FlechetteDebris;
};

//YvexNightmareMissile: OnExplode() function located in scripts/TWM2/Bosses/LordYvex.cs
datablock SeekerProjectileData(YvexNightmareMissile){
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.8;
	damageRadius        = 8.0;
	radiusDamageType    = $DamageType::Missile;
	kickBackStrength    = 2000;

	explosion           = "MissileExplosion";
	splash              = MissileSplash;
	velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
								 // is cranked up to full so the missile doesn't start
								 // out behind the player when the player is moving
								 // very quickly - bramage

	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;
	bubbleEmitter       = GrenadeBubbleEmitter;
	bubbleEmitTime      = 1.0;

	exhaustEmitter      = MissileLauncherExhaustEmitter;
	exhaustTimeMs       = 300;
	exhaustNodeName     = "muzzlePoint1";

	lifetimeMS          = 30000;
	muzzleVelocity      = 10.0;
	maxVelocity         = 150.0;
	turningSpeed        = 110.0;
	acceleration        = 350.0;

	proximityRadius     = 3;

	terrainAvoidanceSpeed         = 180;
	terrainScanAhead              = 25;
	terrainHeightFail             = 12;
	terrainAvoidanceRadius        = 100;

	flareDistance = 200;
	flareAngle    = 30;

	sound = MissileProjectileSound;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.2 0.05 0";

	useFlechette = true;
	flechetteDelayMs = 550;
	casingDeb = FlechetteDebris;

	explodeOnWaterImpact = false;

	baseEmitter         = NMMissileBaseEmitter;
};

//YvexZombieMakerMissile: OnExplode() function located in scripts/TWM2/Bosses/LordYvex.cs
datablock SeekerProjectileData(YvexZombieMakerMissile) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.5;
	damageRadius        = 5.0;
	radiusDamageType    = $DamageType::Zombie;
	kickBackStrength    = 2000;

	explosion           = "MissileExplosion";
	splash              = MissileSplash;
	velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
								 // is cranked up to full so the missile doesn't start
								 // out behind the player when the player is moving
								 // very quickly - bramage

	baseEmitter         = MortarSmokeEmitter;
	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;
	bubbleEmitter       = GrenadeBubbleEmitter;
	bubbleEmitTime      = 1.0;

	exhaustEmitter      = MissileLauncherExhaustEmitter;
	exhaustTimeMs       = 300;
	exhaustNodeName     = "muzzlePoint1";

	lifetimeMS          = 30000; // z0dd - ZOD, 4/14/02. Was 6000
	muzzleVelocity      = 30.0;
	maxVelocity         = 35.0; // z0dd - ZOD, 4/14/02. Was 80.0
	turningSpeed        = 23.0;
	acceleration        = 15.0;

	proximityRadius     = 2.5;

	terrainAvoidanceSpeed = 10;
	terrainScanAhead      = 7;
	terrainHeightFail     = 1;
	terrainAvoidanceRadius = 3;

	flareDistance = 40;
	flareAngle    = 20;
	minSeekHeat   = 0.0;

	sound = MissileProjectileSound;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.2 0.05 0";

	useFlechette = true;
	flechetteDelayMs = 250;
	casingDeb = FlechetteDebris;

	explodeOnWaterImpact = false;
};


//**********************************************
// MISC DATABLOCKS
//**********************************************
datablock StaticShapeData(SubBeacon) {
	shapeFile = "turret_muzzlepoint.dts";
	targetNameTag = 'beacon';
	isInvincible = true;

	dynamicType = $TypeMasks::SensorObjectType;
};