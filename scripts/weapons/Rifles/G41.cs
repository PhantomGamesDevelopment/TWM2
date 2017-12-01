datablock TracerProjectileData(G41Bullet) {
	doDynamicClientHits = true;

	directDamage        = 0.44;
	directDamageType    = $DamageType::G41;
	explosion           = "ChaingunExplosion";
	splash              = ChaingunSplash;
	HeadMultiplier      = 1.1;
	LegsMultiplier      = 0.35;

	ImageSource         = "G41RifleImage";

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

datablock ItemData(G41RifleAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "Some G41 Rifle Bullets";

	computeCRC = true;
};

datablock ShapeBaseImageData(G41RifleImage) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	mass = 10;
	item = G41Rifle;
	ammo = G41RifleAmmo;
	projectile = G41Bullet;
	projectileType = TracerProjectile;
	emap = true;

	//ClipStuff
	ClipName = "G41RifleClip";
	ClipPickupName["G41RifleClip"] = "some G41 Clip Cartons";
	ShowsClipInHud = 1;
	ClipReloadTime = 5;
	ClipReturn = 20;
	InitialClips = 7;
	//
	//Challenges
	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "G41 Hunter\t75\t150\tNone";
	Challenge[2] = "G41 Expert\t150\t250\tGrip";
	Challenge[3] = "G41 Master\t350\t500\tLaser";
	Challenge[4] = "G41 God\t600\t1000\tSilencer";
	Challenge[5] = "G41 Bronze Commendation\t2500\t10000\tNone";
	Challenge[6] = "G41 Silver Commendation\t5000\t25000\tNone";
	Challenge[7] = "G41 Gold Commendation\t10000\t50000\tNone";
	Challenge[8] = "G41 Titan Commendation\t25000\t75000\tNone";
	Upgrade[1] = "Grip";
	Upgrade[2] = "Laser";
	Upgrade[3] = "Silencer";
	GunName = "G41 Semi-Automatic Rifle";
	//

	RankRequire = $TWM2::RankRequire["G41"];

	casing              = ShellDebris;
	shellExitDir        = "1.0 0.3 1.0";
	shellExitOffset     = "0.15 -0.56 -0.1";
	shellExitVariance   = 15.0;
	shellVelocity       = 3.0;

	projectileSpread = 2.0 / 1000.0;

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
	stateEmitterTime[3] = 0.2;
	//stateSound[3] = S3FireSound;

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateTimeoutValue[4] = 0.15;
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

datablock ItemData(G41Rifle) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "weapon_sniper.dts";
	image        = G41RifleImage;
	mass         = 1.0;
	elasticity   = 0.0;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "an G41 Semi-Automatic Rifle";

	computeCRC = true;
	emap = true;
};

function G41RifleImage::onFire(%data, %obj, %slot) {
	%p = Parent::onFire(%data, %obj, %slot);
	if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
		//zero sound :p
	}
	else {
		ServerPlay3d(S3FireSound, %obj.getPosition());
	}
}