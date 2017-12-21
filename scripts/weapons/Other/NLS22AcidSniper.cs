datablock TracerProjectileData(M1Bullet) {
	doDynamicClientHits = true;

	directDamage        = 0.65;
	directDamageType    = $DamageType::M1;
	explosion           = "ChaingunExplosion";
	splash              = ChaingunSplash;
	HeadMultiplier      = 1.5;
	LegsMultiplier      = 0.35;

	HeadShotKill        = 1;

	ImageSource         = "NLS22AcidSniperImage";

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

datablock ItemData(NLS22AcidSniperAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "Some NLS-22 Acid Sniper Canisters";

	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(NLS22AcidSniperImage) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	mass = 10;
	item = NLS22AcidSniper;
	ammo = NLS22AcidSniperAmmo;
	projectile = M1Bullet;
	projectileType = TracerProjectile;
	emap = true;

	armThread = looksn;

	//ClipStuff
	ClipName = "NLS22AcidSniperClip";
	ClipPickupName["NLS22AcidSniperClip"] = "A Few Boxes Of NLS-22 Acid Canisters";
	ShowsClipInHud = 1;
	ClipReloadTime = 4;
	ClipReturn = 4;
	InitialClips = 5;
	//
	//Challenges
	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "NLS-22 Hunter\t100\t150\tNone";
	Challenge[2] = "NLS-22 Expert\t200\t250\tNone";
	Challenge[3] = "NLS-22 Master\t500\t500\tNone";
	Challenge[4] = "NLS-22 God\t1000\t1000\tNone";
	Challenge[5] = "NLS-22 Bronze Commendation\t2500\t10000\tNone";
	Challenge[6] = "NLS-22 Silver Commendation\t5000\t25000\tNone";
	Challenge[7] = "NLS-22 Gold Commendation\t10000\t50000\tNone";
	Challenge[8] = "NLS-22 Titan Commendation\t25000\t75000\tNone";
	GunName = "NLS-22 Acid Sniper Rifle";
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
	stateSound[3] = "M1FireSound";

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateTimeoutValue[4] = 0.9;
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

datablock ItemData(NLS22AcidSniper) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "weapon_sniper.dts";
	image        = NLS22AcidSniperImage;
	mass         = 1.0;
	elasticity   = 0.0;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "a NLS-22 Acid Sniper Rifle";

	computeCRC = true;
	emap = true;
};

//Two images make the scope
datablock ShapeBaseImageData(NLS22AcidSniperScopeImage) : NLS22AcidSniperImage {
	shapeFile = "turret_belly_barrell.dts";
	offset = "0 0.25 0.25";
	rotation = "1 0 0 180";
};

datablock ShapeBaseImageData(NLS22AcidSniperScopeImage2) : NLS22AcidSniperImage {
	shapeFile = "turret_belly_barrell.dts";
	offset = "0 0.3 0.25";
	rotation = "1 0 0 0";
};

datablock ShapeBaseImageData(NLS22AcidSniperBarrelImage) : NLS22AcidSniperImage {
	shapeFile = "weapon_elf.dts";
	offset = "0 0.3 0";
	rotation = "1 0 0 0";
};

function NLS22AcidSniperImage::onMount(%this,%obj,%slot) {
	if(!$TWM2::AllowSnipers) {
		bottomPrint(%obj.client, "The host has disabled sniper weapons.", 2, 2);
		%obj.throwweapon(1);
		%obj.throwweapon(0);
	}
	Parent::onMount(%this, %obj, %slot);
	%obj.mountImage(NLS22AcidSniperScopeImage, 5);
	%obj.mountImage(NLS22AcidSniperScopeImage2, 6);
	%obj.mountImage(NLS22AcidSniperBarrelImage, 7);
}

function NLS22AcidSniperImage::onUnmount(%this,%obj,%slot) {
	Parent::onUnmount(%this, %obj, %slot);
	%obj.unmountImage(5);
	%obj.unmountImage(6);
	%obj.unmountImage(7);
}

function M1SniperRifleImage::onFire(%data, %obj, %slot) {
	if(!$TWM2::AllowSnipers) {
		bottomPrint(%obj.client, "The host has disabled sniper weapons.", 2, 2);
		%obj.throwweapon(1);
		%obj.throwweapon(0);
	}
	%p = Parent::onFire(%data, %obj, %slot);
}