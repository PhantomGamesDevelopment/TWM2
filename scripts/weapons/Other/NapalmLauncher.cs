datablock ItemData(NapalmAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_grenade.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "some Napalm";
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------

datablock ShapeBaseImageData(NapalmImage) {
	className = WeaponImage;
	shapeFile = "ammo_grenade.dts";
	item = Napalm;
	ammo = NapalmAmmo;
	offset = "0 0 0";
	armThread = lookms;
	emap = true;

	projectileSpread = 0;

	projectile = NapalmShot;
	projectileType = LinearProjectile;

	RankRequire = $TWM2::RankRequire["NapalmLauncher"];
	MedalRequire = 27;

	//ClipStuff
	ClipName = "NapalmClip";
	ClipPickupName["NapalmClip"] = "some napalm fuel clusters";
	ShowsClipInHud = 1;
	ClipReloadTime = 5;
	ClipReturn = 1;
	InitialClips = 6;

	//Challenges
	HasChallenges = 1;
	ChallengeCt = 9;
	Challenge[1] = "Napalm Novice\t25\t100\tNone";
	Challenge[2] = "Napalm Hunter\t50\t150\tNone";
	Challenge[3] = "Napalm Expert\t100\t250\tNone";
	Challenge[4] = "Napalm Master\t250\t500\tNone";
	Challenge[5] = "Napalm God\t500\t1000\tNone";
	Challenge[6] = "Napalm Bronze Commendation\t2500\t10000\tNone";
	Challenge[7] = "Napalm Silver Commendation\t5000\t25000\tNone";
	Challenge[8] = "Napalm Gold Commendation\t10000\t50000\tNone";
	Challenge[9] = "Napalm Titan Commendation\t25000\t75000\tNone";
	GunName = "ZH7C8 Napalm Launcher";
	//

	// State Data
	stateName[0]                     = "ActivateReady";
	stateTransitionOnLoaded[0]       = "Activate";
	stateTransitionOnNoAmmo[0]       = "NoAmmo";

	stateName[1]                     = "Activate";
	stateTransitionOnTimeout[1]      = "Ready";
	stateTimeoutValue[1]             = 0.5;
	stateSequence[1]                 = "Activated";
	stateSound[1]                    = PlasmaSwitchSound;

	stateName[2]                     = "Ready";
	stateTransitionOnNoAmmo[2]       = "NoAmmo";
	stateTransitionOnTriggerDown[2]  = "Fire";
	stateSequence[2]                 = "DiscSpin";
	//   stateSound[2]                    = DiscLoopSound;

	stateName[3]                     = "Fire";
	stateTransitionOnTimeout[3]      = "Reload";
	stateTimeoutValue[3]             = 0.3;
	stateFire[3]                     = true;
	stateRecoil[3]                   = LightRecoil;
	stateAllowImageChange[3]         = false;
	stateSequence[3]                 = "Fire";
	stateScript[3]                   = "onFire";
	stateSound[3]                    = MortarFireSound;

	stateName[4]                     = "Reload";
	stateTransitionOnNoAmmo[4]       = "NoAmmo";
	stateTransitionOnTimeout[4]      = "Ready";
	stateTimeoutValue[4]             = 2.2; // 0.25 load, 0.25 spinup
	stateAllowImageChange[4]         = false;
	stateSequence[4]                 = "Reload";
	//   stateSound[4]                    = DiscReloadSound;

	stateName[5]                     = "NoAmmo";
	stateTransitionOnAmmo[5]         = "Reload";
	stateSequence[5]                 = "NoAmmo";
	stateTransitionOnTriggerDown[5]  = "DryFire";

	stateName[6]                     = "DryFire";
	stateSound[6]                    = GrenadeDryFireSound;
	stateTimeoutValue[6]             = 0.3;
	stateTransitionOnTimeout[6]      = "NoAmmo";
};

datablock ItemData(Napalm) {
	className = Weapon;
	catagory = "Spawn Items";
	shapeFile = "ammo_grenade.dts";
	image = NapalmImage;
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "a Napalm Gun";
	emap = true;
};