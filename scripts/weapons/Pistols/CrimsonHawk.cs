//--------------------------------------------------------------------------
// Projectile
//--------------------------------------------------------------------------
datablock TracerProjectileData(CHawkBullet) {
	doDynamicClientHits = true;

	directDamage        = 0.17;   
	directDamageType    = $DamageType::CrimsonHawk; //obviously change this

	explosion           = "BlasterExplosion";
	splash              = ChaingunSplash;

	sound = ShrikeBlasterProjectileSound;

	dryVelocity       = 425.0;
	wetVelocity       = 100.0;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 400;
	lifetimeMS        = 450;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = false;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 3000;

	ImageSource         = "CrimsonHawkImage";

	tracerLength    = 45.0;
	tracerAlpha     = false;
	tracerMinPixels = 6;
	tracerColor     = "1.0 0.0 0.0 1.0";
	tracerTex[0]  	 = "special/shrikeBolt";
	tracerTex[1]  	 = "special/shrikeBoltCross";
	tracerWidth     = 0.55;
	crossSize       = 0.99;
	crossViewAng    = 0.990;
	renderCross     = true;
};

datablock ItemData(CrimsonHawkAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_chaingun.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "some .77mm bullets";

	computeCRC = true;
};

datablock ItemData(CrimsonHawk) {
	className = Weapon;
	catagory = "Spawn Items";
	shapeFile = "weapon_energy_vehicle.dts";
	image = CrimsonHawkImage;
	mass = 2;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "a Crimson Hawk Pistol";
};

datablock ShapeBaseImageData(CrimsonHawkImage) {
	className = WeaponImage;
	shapeFile = "weapon_energy_vehicle.dts";
	item = CrimsonHawk;
	offset = "0 0 0";
	rotation = "0 1 0 180";
	mass = 4;

	ammo = CrimsonHawkAmmo;

	projectile = CHawkBullet;
	projectileType = tracerProjectile;
	projectileSpread = 7.0 / 1000.0;

	//ClipStuff
	ClipName = "CrimsonHawkClip";
	ClipPickupName["CrimsonHawkClip"] = "Some .77MM Clips";
	ShowsClipInHud = 0;
	ClipReloadTime = 4;
	ClipReturn = 15;
	InitialClips = 950;
	HellClipCount = 10;
	//

	//RankRequire = $TWM2::RankRequire["CrimsonHawk"];
	PrestigeRequire = 5;

	//Challenges
	HasChallenges = 1;
	ChallengeCt = 9;
	Challenge[1] = "Crimson Hawk Novice\t25\t100\tNone";
	Challenge[2] = "Crimson Hawk Hunter\t50\t150\tGrip";
	Challenge[3] = "Crimson Hawk Expert\t100\t250\tLaser";
	Challenge[4] = "Crimson Hawk Master\t250\t500\tSilencer";
	Challenge[5] = "Crimson Hawk God\t500\t1000\tNone";
	Challenge[6] = "Crimson Hawk Bronze Commendation\t2500\t10000\tNone";
	Challenge[7] = "Crimson Hawk Silver Commendation\t7500\t25000\tNone";
	Challenge[8] = "Crimson Hawk Gold Commendation\t25000\t50000\tNone";
	Challenge[9] = "Crimson Hawk Titan Commendation\t50000\t75000\tNone";
	Upgrade[1] = "Grip";
	Upgrade[2] = "Laser";
	Upgrade[3] = "Silencer";
	GunName = "Crimson Hawk";
	//

	stateName[0] = "Activate";
	stateTransitionOnTimeout[0] = "ActivateReady";
	stateTimeoutValue[0] = 0.5;
	stateSequence[0] = "Activate";
	stateSound[0] = PlasmaSwitchSound;

	stateName[1] = "ActivateReady";
	stateTransitionOnLoaded[1] = "Ready";
	stateTransitionOnNoAmmo[1] = "NoAmmo";

	stateName[2] = "Ready";
	stateTransitionOnNoAmmo[2] = "NoAmmo";
	stateTransitionOnTriggerDown[2] = "Fire";

	stateName[3] = "Fire";
	stateTransitionOnTimeout[3] = "Fire2";
	stateTimeoutValue[3] = 0.1;
	stateFire[3] = true;
	stateRecoil[3] = LightRecoil;
	stateAllowImageChange[3] = false;
	stateScript[3] = "onFire";
	stateEmitterTime[3] = 0.1;

	stateName[4] = "Fire2";
	stateTransitionOnTimeout[4] = "Fire3";
	stateTimeoutValue[4] = 0.1;
	stateFire[4] = true;
	stateRecoil[4] = LightRecoil;
	stateAllowImageChange[4] = false;
	stateScript[4] = "onFire";
	stateEmitterTime[4] = 0.1;

	stateName[5] = "Fire3";
	stateTransitionOnTimeout[5] = "Fire4";
	stateTimeoutValue[5] = 0.1;
	stateFire[5] = true;
	stateRecoil[5] = LightRecoil;
	stateAllowImageChange[5] = false;
	stateScript[5] = "onFire";
	stateEmitterTime[5] = 0.1;

	stateName[6] = "Fire4";
	stateTransitionOnTimeout[6] = "Fire5";
	stateTimeoutValue[6] = 0.1;
	stateFire[6] = true;
	stateRecoil[6] = LightRecoil;
	stateAllowImageChange[6] = false;
	stateScript[6] = "onFire";
	stateEmitterTime[6] = 0.1;

	stateName[7] = "Fire5";
	stateTransitionOnTimeout[7] = "Reload";
	stateTimeoutValue[7] = 0.1;
	stateFire[7] = true;
	stateRecoil[7] = LightRecoil;
	stateAllowImageChange[7] = false;
	stateScript[7] = "onFire";
	stateEmitterTime[7] = 0.1;
	//stateSound[3] = PistolFireSound;

	stateName[8] = "Reload";
	stateTransitionOnNoAmmo[8] = "NoAmmo";
	stateTransitionOnTimeout[8] = "Ready";
	stateTimeoutValue[8] = 0.1;
	stateAllowImageChange[8] = false;
	stateSequence[8] = "Reload";

	stateName[9] = "NoAmmo";
	stateTransitionOnAmmo[9] = "Reload";
	stateSequence[9] = "NoAmmo";
	stateTransitionOnTriggerDown[9] = "DryFire";

	stateName[10]       = "DryFire";
	stateSound[10]      = ChaingunDryFireSound;
	stateTimeoutValue[10]        = 1.5;
	stateTransitionOnTimeout[10] = "NoAmmo";
};

function CrimsonHawkImage::onMount(%this,%obj,%slot) {
	Parent::onMount(%this, %obj, %slot);
}

function CrimsonHawkImage::onFire(%data, %obj, %slot) {
	%p = Parent::onFire(%data, %obj, %slot);
	if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
		//zero sound :p
	}
	else {
		ServerPlay3d(PistolFireSound, %obj.getPosition());
	}
}
