datablock TracerProjectileData(MG42Bullet) {
	doDynamicClientHits = true;

	directDamage        = 0.063;
	directDamageType    = $DamageType::MG42;
	explosion           = "ChaingunExplosion";
	splash              = ChaingunSplash;
	HeadMultiplier      = 1.5;
	LegsMultiplier      = 0.5;

	ImageSource         = "MG42Image";

	kickBackStrength  = 50.0;
	sound 				= ChaingunProjectile;

	dryVelocity       = 3000.0;
	wetVelocity       = 1000.0;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 3000;
	lifetimeMS        = 3000;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = false;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 3000;

	tracerLength    = 40.0;
	tracerAlpha     = false;
	tracerMinPixels = 6;
	tracerColor     = 10.0/255.0 @ " " @ 30.0/255.0 @ " " @ 240.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.2;
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

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------
datablock ItemData(MG42Ammo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 3;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "some mg42 rounds";

	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(MG42Image) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	item      = MG42;
	ammo 	 = MG42Ammo;
	projectile = MG42Bullet;
	projectileType = TracerProjectile;
	emap = true;
	mass = 26;

	offset = "0.08 0.22 0.15"; // L/R - F/B - T/B

	casing              = ShellDebris;
	shellExitDir        = "0.5 0 1";
	shellExitOffset     = "0.0 0.75 0.0";
	shellExitVariance   = 5.0;
	shellVelocity       = 4.5;


	//ClipStuff
	ClipName = "MG42Clip";
	ClipPickupName["MG42Clip"] = "Some MG42 Clip Boxes";
	ShowsClipInHud = 1;
	ClipReloadTime = 7;
	ClipReturn = 200;
	InitialClips = 4;
	//
	//Challenges
	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "MG42 Hunter\t100\t250\tNone";
	Challenge[2] = "MG42 Expert\t250\t500\tGrip";
	Challenge[3] = "MG42 Master\t750\t1000\tNone";
	Challenge[4] = "MG42 God\t1500\t2000\tSilencer";
	Challenge[5] = "MG42 Bronze Commendation\t3000\t10000\tNone";
	Challenge[6] = "MG42 Silver Commendation\t6000\t25000\tNone";
	Challenge[7] = "MG42 Gold Commendation\t12500\t50000\tNone";
	Challenge[8] = "MG42 Titan Commendation\t35000\t75000\tNone";
	Upgrade[1] = "Grip";
	Upgrade[2] = "Silencer";
	GunName = "MG42 Machine Gun";
	//

	RankRequire = $TWM2::RankRequire["MG42"];

	projectileSpread = 12.5 / 1000.0;

	//--------------------------------------
	stateName[0]             = "Activate";
	stateSequence[0]         = "Activate";
	stateSound[0]            = ChaingunSwitchSound;
	stateAllowImageChange[0] = false;
	//
	stateTimeoutValue[0]        = 0.5;
	stateTransitionOnTimeout[0] = "Ready";
	stateTransitionOnNoAmmo[0]  = "NoAmmo";

	//--------------------------------------
	stateName[1]       = "Ready";
	stateSpinThread[1] = Stop;
	//
	stateTransitionOnTriggerDown[1] = "Spinup";
	stateTransitionOnNoAmmo[1]      = "NoAmmo";

	//--------------------------------------
	stateName[2]               = "NoAmmo";
	stateTransitionOnAmmo[2]   = "Ready";
	stateSpinThread[2]         = Stop;
	stateTransitionOnTriggerDown[2] = "DryFire";

	//--------------------------------------
	stateName[3]         = "Spinup";
	stateSpinThread[3]   = SpinUp;
	stateSound[3]        = ChaingunSpinupSound;
	//
	stateTimeoutValue[3]          = 0.1;
	stateWaitForTimeout[3]        = false;
	stateTransitionOnTimeout[3]   = "Fire";
	stateTransitionOnTriggerUp[3] = "Spindown";

	//--------------------------------------
	stateName[4]             = "Fire";
	stateSequence[4]            = "Fire";
	stateSequenceRandomFlash[4] = true;
	stateSpinThread[4]       = FullSpeed;
	//stateSound[4]            = ChaingunFireSound;
	stateAllowImageChange[4] = false;
	stateScript[4]           = "onFire";
	stateFire[4]             = true;
	stateEjectShell[4]       = true;
	stateTimeoutValue[4]          = 0.02;
	stateTransitionOnTimeout[4]   = "Fire";
	stateTransitionOnTriggerUp[4] = "Spindown";
	stateTransitionOnNoAmmo[4]    = "EmptySpindown";

	//--------------------------------------
	stateName[5]       = "Spindown";
	stateSound[5]      = ChaingunSpinDownSound;
	stateSpinThread[5] = SpinDown;
	//
	stateTimeoutValue[5]            = 0.1;
	stateWaitForTimeout[5]          = true;
	stateTransitionOnTimeout[5]     = "Ready";
	stateTransitionOnTriggerDown[5] = "Spinup";

	//--------------------------------------
	stateName[6]       = "EmptySpindown";
	stateSound[6]      = ChaingunSpinDownSound;
	stateSpinThread[6] = SpinDown;
	//
	stateTimeoutValue[6]        = 1.0;
	stateTransitionOnTimeout[6] = "NoAmmo";

	//--------------------------------------
	stateName[7]       = "DryFire";
	stateSound[7]      = ChaingunDryFireSound;
	stateTimeoutValue[7]        = 0.5;
	stateTransitionOnTimeout[7] = "NoAmmo";
};

datablock ItemData(MG42) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "turret_tank_barrelchain.dts";
	image        = MG42Image;
	mass         = 1.0;
	elasticity   = 0.2;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "a MG42 machine gun";

	computeCRC = true;
	emap = true;
};

datablock ShapeBaseImageData(MG42Image1) : MG42Image {
	className = WeaponImage;

	shapeFile = "weapon_missile.dts";
	rotation = "0 0 1 180";
	offset = "0.01 0.04 0.0"; // L/R - F/B - T/B
};

function MG42Image::onMount(%this,%obj,%slot) {
	Parent::onMount(%this, %obj, %slot);
	%obj.mountImage(MG42Image1, 3);
}

function MG42Image::onUnmount(%this,%obj,%slot) {
	Parent::onUnmount(%this, %obj, %slot);
	%obj.unmountImage(3);
}

function MG42Image::onFire(%data, %obj, %slot) {
	%p = Parent::onFire(%data, %obj, %slot);
	if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
		//zero sound :p
	}
	else {
		ServerPlay3d(ChaingunFireSound, %obj.getPosition());
	}
}