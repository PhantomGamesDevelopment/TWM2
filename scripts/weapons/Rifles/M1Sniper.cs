datablock AudioProfile(M1FireSound) {
	filename    = "fx/weapons/cg_metal2.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock TracerProjectileData(M1Bullet) {
	doDynamicClientHits = true;

	directDamage        = 0.65;
	directDamageType    = $DamageType::M1;
	explosion           = "ChaingunExplosion";
	splash              = ChaingunSplash;
	HeadMultiplier      = 2.5;
	LegsMultiplier      = 0.35;

	HeadShotKill        = 1;

	ImageSource         = "M1SniperRifleImage";

	kickBackStrength  = 15.0;
	sound 		   = ChaingunProjectile;

	dryVelocity       = 3000.0;
	wetVelocity       = 2000.0;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 1000;
	lifetimeMS        = 1000;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = false;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 3000;

	tracerLength    = 20.0;
	tracerAlpha     = false;
	tracerMinPixels = 6;
	tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.09;
	crossSize       = 0.20;
	crossViewAng    = 0.990;
	renderCross     = true;

	decalData[0] = ChaingunDecal1;
	decalData[1] = ChaingunDecal2;
	decalData[2] = ChaingunDecal3;
	decalData[3] = ChaingunDecal4;
	decalData[4] = ChaingunDecal5;
	decalData[5] = ChaingunDecal6;
};

datablock ItemData(M1SniperRifleAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "Some M1 Sniper Rifle Ammo";

	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(M1SniperRifleImage) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	mass = 10;
	item = M1SniperRifle;
	ammo = M1SniperRifleAmmo;
	projectile = M1Bullet;
	projectileType = TracerProjectile;
	emap = true;

	armThread = looksn;

	//ClipStuff
	ClipName = "M1Clip";
	ClipPickupName["M1Clip"] = "A Few Boxes Of M1 Sniper Bullets";
	ShowsClipInHud = 1;
	ClipReloadTime = 4;
	ClipReturn = 5;
	InitialClips = 5;
	//
	//Challenges
	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "M1 Hunter\t100\t150\tNone";
	Challenge[2] = "M1 Expert\t200\t250\tNone";
	Challenge[3] = "M1 Master\t500\t500\tLaser";
	Challenge[4] = "M1 God\t1000\t1000\tSilencer";
	Challenge[5] = "M1 Bronze Commendation\t2500\t10000\tNone";
	Challenge[6] = "M1 Silver Commendation\t5000\t25000\tNone";
	Challenge[7] = "M1 Gold Commendation\t10000\t50000\tNone";
	Challenge[8] = "M1 Titan Commendation\t25000\t75000\tNone";
	Upgrade[1] = "Laser";
	Upgrade[2] = "Silencer";
	GunName = "M1 Sniper Rifle";
	//

	RankRequire = $TWM2::RankRequire["M1"];

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
	//stateSound[3] = "M1FireSound";

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateTimeoutValue[4] = 1.1;
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

datablock ItemData(M1SniperRifle) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "weapon_sniper.dts";
	image        = M1SniperRifleImage;
	mass         = 1.0;
	elasticity   = 0.0;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "a M1 Sniper Rifle";

	computeCRC = true;
	emap = true;
};

//Two images make the scope
datablock ShapeBaseImageData(M1ScopeImage) : M1SniperRifleImage {
	shapeFile = "turret_belly_barrell.dts";
	offset = "0 0.25 0.25";
	rotation = "1 0 0 180";
};

datablock ShapeBaseImageData(M1ScopeImage2) : M1SniperRifleImage {
	shapeFile = "turret_belly_barrell.dts";
	offset = "0 0.3 0.25";
	rotation = "1 0 0 0";
};

function M1SniperRifleImage::onMount(%this,%obj,%slot) {
	if(!$TWM2::AllowSnipers) {
		bottomPrint(%obj.client, "The host has disabled sniper weapons.", 2, 2);
		%obj.throwweapon(1);
		%obj.throwweapon(0);
	}
	Parent::onMount(%this, %obj, %slot);
	%obj.mountImage(M1ScopeImage, 5);
	%obj.mountImage(M1ScopeImage2, 6);
}

function M1SniperRifleImage::onUnmount(%this,%obj,%slot) {
	Parent::onUnmount(%this, %obj, %slot);
	%obj.unmountImage(5);
	%obj.unmountImage(6);
}

function M1SniperRifleImage::onFire(%data, %obj, %slot) {
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
		ServerPlay3d(M1FireSound, %obj.getPosition());
	}
}