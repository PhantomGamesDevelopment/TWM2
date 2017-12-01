datablock AudioProfile(G17FireSound) {
	filename    = "fx/weapons/cg_metal4.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock TracerProjectileData(G17Bullet){
	doDynamicClientHits = true;

	directDamage        = 0.55;
	directDamageType    = $DamageType::G17;
	explosion           = "ChaingunExplosion";
	splash              = ChaingunSplash;
	HeadMultiplier      = 1.5;
	LegsMultiplier      = 0.35;

	HeadShotKill        = 1;

	ImageSource         = "G17SniperRifleImage";

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

datablock ItemData(G17SniperRifleAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "Some G17 Sniper Rifle Ammo";

	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(G17SniperRifleImage) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	mass = 10;
	item = G17SniperRifle;
	ammo = G17SniperRifleAmmo;
	projectile = G17Bullet;
	projectileType = TracerProjectile;
	emap = true;

	armThread = looksn;

	//ClipStuff
	ClipName = "G17Clip";
	ClipPickupName["G17Clip"] = "A Few Boxes Of G17 Sniper Bullets";
	ShowsClipInHud = 1;
	ClipReloadTime = 4;
	ClipReturn = 1;
	InitialClips = 10;
	//
	//Challenges
	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "G17 Hunter\t100\t150\tNone";
	Challenge[2] = "G17 Expert\t200\t250\tNone";
	Challenge[3] = "G17 Master\t500\t500\tLaser";
	Challenge[4] = "G17 God\t1000\t1000\tSilencer";
	Challenge[5] = "G17 Bronze Commendation\t2500\t10000\tNone";
	Challenge[6] = "G17 Silver Commendation\t5000\t25000\tNone";
	Challenge[7] = "G17 Gold Commendation\t10000\t50000\tNone";
	Challenge[8] = "G17 Titan Commendation\t25000\t75000\tNone";
	Upgrade[1] = "Laser";
	Upgrade[2] = "Silencer";
	GunName = "G17 Sniper Rifle";
	//

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
	//stateSound[3] = "G17FireSound";

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateTimeoutValue[4] = 0.7;
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

datablock ItemData(G17SniperRifle) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "weapon_sniper.dts";
	image        = G17SniperRifleImage;
	mass         = 1.0;
	elasticity   = 0.0;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "a G17 Sniper Rifle";

	computeCRC = true;
	emap = true;
};

//Two images make the scope
datablock ShapeBaseImageData(G17ScopeImage) : G17SniperRifleImage {
	shapeFile = "turret_belly_barrell.dts";
	offset = "0 0.25 0.25";
	rotation = "1 0 0 180";
};

datablock ShapeBaseImageData(G17ScopeImage2) : G17SniperRifleImage {
	shapeFile = "turret_belly_barrell.dts";
	offset = "0 0.3 0.25";
	rotation = "1 0 0 0";
};

function G17SniperRifleImage::onMount(%this,%obj,%slot) {
	if(!$TWM2::AllowSnipers) {
		bottomPrint(%obj.client, "The host has disabled sniper weapons.", 2, 2);
		%obj.throwweapon(1);
		%obj.throwweapon(0);
	}
	Parent::onMount(%this, %obj, %slot);
	%obj.mountImage(G17ScopeImage, 5);
	%obj.mountImage(G17ScopeImage2, 6);
}

function G17SniperRifleImage::onUnmount(%this,%obj,%slot) {
	Parent::onUnmount(%this, %obj, %slot);
	%obj.unmountImage(5);
	%obj.unmountImage(6);
}

function G17SniperRifleImage::onFire(%data, %obj, %slot) {
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
		ServerPlay3d(G17FireSound, %obj.getPosition());
	}
}