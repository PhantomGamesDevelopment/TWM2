datablock AudioProfile(R700FireSound) {
	filename    = "fx/weapons/cg_metal3.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock ParticleData(R700SmokeParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = -0.02;
	inheritedVelFactor   = 0.1;

	lifetimeMS           = 1200;
	lifetimeVarianceMS   = 100;

	textureName          = "particleTest";

	useInvAlpha = false;
	spinRandomMin = -90.0;
	spinRandomMax = 90.0;

	colors[0]     = "1 1 1";
	colors[1]     = "1 1 1";
	colors[2]     = "1 1 1";
	sizes[0]      = 1;
	sizes[1]      = 1.2;
	sizes[2]      = 1.4;
	times[0]      = 0.0;
	times[1]      = 0.1;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(R700SmokeEmitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 1.5;
	velocityVariance = 0.3;

	thetaMin         = 0.0;
	thetaMax         = 50.0;

	particles = "R700SmokeParticle";
};

datablock GrenadeProjectileData(R700Bullet) {
	projectileShapeName = "weapon_missile_projectile.dts";
	emitterDelay        = -1;
	directDamage        = 0.62;
	hasDamageRadius     = false;
	directDamageType    = $DamageType::R700;
	kickBackStrength    = 6400;
	bubbleEmitTime      = 1.0;

	HeadMultiplier      = 1.5;
	LegsMultiplier      = 0.35;

	HeadShotKill        = 1;   

	ImageSource         = "R700SniperRifleImage";

	sound               = ChaingunProjectile;
	explosion           = "ChaingunExplosion";
	underwaterExplosion = "ChaingunExplosion";
	velInheritFactor    = 0.5;
	splash              = ChaingunSplash;

	baseEmitter         = R700SmokeEmitter;
	bubbleEmitter       = R700SmokeEmitter;

	grenadeElasticity = 0.35;
	grenadeFriction   = 0.2;
	armingDelayMS     = 1;
	muzzleVelocity    = 2000.00;
	drag = 0.1;	
};

datablock ItemData(R700SniperRifleAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "Some R700 Sniper Rifle Ammo";

	computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(R700SniperRifleImage) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	mass = 10;
	item = R700SniperRifle;
	ammo = R700SniperRifleAmmo;
	projectile = R700Bullet;
	projectileType = GrenadeProjectile;
	emap = true;

	armThread = looksn;

	//ClipStuff
	ClipName = "R700Clip";
	ClipPickupName["R700Clip"] = "A Few Boxes Of R700 Sniper Bullets";
	ShowsClipInHud = 1;
	ClipReloadTime = 7;
	ClipReturn = 4;
	InitialClips = 6;
	//
	//Challenges
	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "R700 Hunter\t100\t150\tNone";
	Challenge[2] = "R700 Expert\t200\t250\tNone";
	Challenge[3] = "R700 Master\t500\t500\tLaser";
	Challenge[4] = "R700 God\t1000\t1000\tSilencer";
	Challenge[5] = "R700 Bronze Commendation\t2500\t10000\tNone";
	Challenge[6] = "R700 Silver Commendation\t5000\t25000\tNone";
	Challenge[7] = "R700 Gold Commendation\t10000\t50000\tNone";
	Challenge[8] = "R700 Titan Commendation\t25000\t75000\tNone";
	Upgrade[1] = "Laser";
	Upgrade[2] = "Silencer";
	GunName = "R700 Sniper Rifle";
	//

	RankRequire = $TWM2::RankRequire["R700"];

	casing              = ShellDebris;
	shellExitDir        = "1.0 0.3 1.0";
	shellExitOffset     = "0.15 -0.56 -0.1";
	shellExitVariance   = 15.0;
	shellVelocity       = 3.0;


	stateName[0] = "Activate";
	stateTransitionOnTimeout[0] = "ActivateReady";
	stateTimeoutValue[0] = 0.5;
	stateSequence[0] = "Activate";
	stateSound[0] = ChaingunSwitchSound;

	stateName[1] = "ActivateReady";
	stateTransitionOnLoaded[1] = "Ready";
	stateTransitionOnNoAmmo[1] = "NoAmmo";

	stateName[2] = "Ready";
	stateTransitionOnNoAmmo[2] = "NoAmmo";
	stateTransitionOnTriggerDown[2] = "CheckWet";

	stateName[3] = "Fire";
	stateTransitionOnTimeout[3] = "Reload";
	stateTimeoutValue[3] = 0.01;
	stateFire[3] = true;
	stateRecoil[3] = LightRecoil;
	stateAllowImageChange[3] = false;
	stateScript[3] = "onFire";
	//stateSound[3] = "R700FireSound";

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateTimeoutValue[4] = 1.2;
	stateAllowImageChange[4] = false;
	stateSequence[4] = "Reload";

	stateName[5] = "NoAmmo";
	stateTransitionOnAmmo[5] = "Reload";
	stateSequence[5] = "NoAmmo";
	stateTransitionOnTriggerDown[5] = "DryFire";

	stateName[6]       = "DryFire";
	stateSound[6]      = ChaingunDryFireSound;
	stateTimeoutValue[6]        = 1.0;
	stateTransitionOnTimeout[6] = "NoAmmo";

	stateName[7]       = "WetFire";
	stateSound[7]      = PlasmaFireWetSound;
	stateTimeoutValue[7]        = 1.0;
	stateTransitionOnTimeout[7] = "Ready";

	stateName[8]               = "CheckWet";
	stateTransitionOnWet[8]    = "WetFire";
	stateTransitionOnNotWet[8] = "Fire";
};

datablock ItemData(R700SniperRifle) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "weapon_sniper.dts";
	image        = R700SniperRifleImage;
	mass         = 1.0;
	elasticity   = 0.0;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "a R700 Sniper Rifle";

	computeCRC = true;
	emap = true;
};

datablock ShapeBaseImageData(R700ScopeImage) {
	className = WeaponImage;
	shapeFile = "weapon_targeting.dts";
	offset = "0.0 1.0 0.4";
	rotation = "180 0 0 90";

	ammo = R700SniperRifleAmmo;
	armThread = looksn;

	emap = true;
};


function R700SniperRifleImage::onMount(%this,%obj,%slot) {
	if(!$TWM2::AllowSnipers) {
		bottomPrint(%obj.client, "The host has disabled sniper weapons.", 2, 2);
		%obj.throwweapon(1);
		%obj.throwweapon(0);
	}
	Parent::onMount(%this, %obj, %slot);
	%obj.mountImage(R700ScopeImage, 5);
}

function R700SniperRifleImage::onUnmount(%this,%obj,%slot) {
	Parent::onUnmount(%this, %obj, %slot);
	%obj.unmountImage(5);
}

function R700SniperRifleImage::onFire(%data, %obj, %slot) {
	if(!$TWM2::AllowSnipers) {
		bottomPrint(%obj.client, "The host has disabled sniper weapons.", 2, 2);
		%obj.throwweapon(1);
		%obj.throwweapon(0);
	}
	%p = Parent::onFire(%data, %obj, %slot);
	if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
		//zero sound :p
	}
	else {
		ServerPlay3d(R700FireSound, %obj.getPosition());
	}
}