datablock SeekerProjectileData(RPGMissile) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "grenade.dts";
	hasDamageRadius     = true;
	indirectDamage      = 1.3;  //TWM2 2.1, Increased to 1.3 from 0.8
	damageRadius        = 8.0;
	radiusDamageType    = $DamageType::RPG;
	kickBackStrength    = 3500;

	ImageSource         = "RPGImage";

	explosion           = "HandGrenadeExplosion";
	splash              = MissileSplash;
	velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
								 // is cranked up to full so the missile doesn't start
								 // out behind the player when the player is moving
								 // very quickly - bramage

	baseEmitter         = MissileSmokeEmitter;
	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;
	bubbleEmitter       = GrenadeBubbleEmitter;
	bubbleEmitTime      = 1.0;

	exhaustEmitter      = MissileLauncherExhaustEmitter;
	exhaustTimeMs       = 300;
	exhaustNodeName     = "muzzlePoint1";

	lifetimeMS          = 5000; // z0dd - ZOD, 4/14/02. Was 6000
	muzzleVelocity      = 100.0;
	maxVelocity         = 350.0; // z0dd - ZOD, 4/14/02. Was 80.0
	turningSpeed        = 54.0;
	acceleration        = 50.0;

	explodeOnDeath = true;

	proximityRadius     = 3;

	sound = MissileProjectileSound;

	hasLight    = true;
	lightRadius = 3.0;
	lightColor  = "0.2 0.05 0";

	useFlechette = true;
	flechetteDelayMs = 10;
	casingDeb = FlechetteDebris;

	explodeOnWaterImpact = true;
};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(RPGAmmo) {
	className = Ammo;
	catagory = "Ammo";
	shapeFile = "ammo_missile.dts";
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "Some RPGs";

	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(RPG) {
	className = Weapon;
	catagory = "Spawn Items";
	shapeFile = "weapon_grenade_launcher.dts";
	image = RPGImage;
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "an RPG";

	computeCRC = true;
	emap = true;
};

datablock ShapeBaseImageData(RPGImage) {
	className = WeaponImage;
	shapeFile = "weapon_grenade_launcher.dts";
	item = RPG;
	ammo = RPGAmmo;
	offset = "0 -1 0";
	armThread = lookms;
	emap = true;

	projectile = RPGMissile;
	projectileType = seekerprojectile;
	projectileSpread = 1.0 / 1000.0;

	//ClipStuff
	ClipName = "RPGClip";
	ClipPickupName["RPGClip"] = "A Few RPGs";
	ShowsClipInHud = 1;
	ClipReloadTime = 5;
	ClipReturn = 1;
	InitialClips = 7;
	//
	HasChallenges = 1;
	ChallengeCt = 9;
	Challenge[1] = "RPG Killer\t50\t100\tNone";
	Challenge[2] = "RPG Extremist\t100\t150\tNone";
	Challenge[3] = "RPG Expert\t250\t250\tNone";
	Challenge[4] = "RPG Master\t500\t500\tEnhanced Explosives";
	Challenge[5] = "RPG Legend\t1000\t1000\tSpread Neutralizer";
	Challenge[6] = "RPG Bronze Commendation\t2500\t10000\tNone";
	Challenge[7] = "RPG Silver Commendation\t5000\t25000\tNone";
	Challenge[8] = "RPG Gold Commendation\t10000\t50000\tNone";
	Challenge[9] = "RPG Titan Commendation\t25000\t75000\tNone";
	Upgrade[1] = "RPG Explosives";
	Upgrade[2] = "Spread Neutralizer";
	GunName = "RPG-7";

	RankRequire = $TWM2::RankRequire["RPG7"];

	stateName[0]                     = "Activate";
	stateTransitionOnTimeout[0]      = "ActivateReady";
	stateTimeoutValue[0]             = 2.0;
	stateSequence[0]                 = "Activate";
	stateSound[0]                    = MissileSwitchSound;

	stateName[1]                     = "ActivateReady";
	stateTransitionOnLoaded[1]       = "Ready";
	stateTransitionOnNoAmmo[1]       = "NoAmmo";

	stateName[2]                     = "Ready";
	stateTransitionOnNoAmmo[2]       = "NoAmmo";
	stateTransitionOnTriggerDown[2]  = "CheckWet";

	stateName[3]                     = "Fire";
	stateTransitionOnTimeout[3]      = "Reload";
	stateTimeoutValue[3]             = 0.4;
	stateFire[3]                     = true;
	stateRecoil[3]                   = LightRecoil;
	stateAllowImageChange[3]         = false;
	stateSequence[3]                 = "Fire";
	stateScript[3]                   = "onFire";
	stateSound[3]                    = MissileFireSound;

	stateName[4]                     = "Reload";
	stateTransitionOnNoAmmo[4]       = "NoAmmo";
	stateTransitionOnTimeout[4]      = "Ready";
	stateTimeoutValue[4]             = 0.1;
	stateAllowImageChange[4]         = false;
	stateSequence[4]                 = "Reload";
	stateSound[4]                    = MissileReloadSound;

	stateName[5]                     = "NoAmmo";
	stateTransitionOnAmmo[5]         = "Reload";
	stateSequence[5]                 = "NoAmmo";
	stateTransitionOnTriggerDown[5]  = "DryFire";

	stateName[6]                     = "DryFire";
	stateSound[6]                    = MissileDryFireSound;
	stateTimeoutValue[6]             = 1.0;
	stateTransitionOnTimeout[6]      = "ActivateReady";

	stateName[7]                     = "CheckTarget";
	stateTransitionOnNoTarget[7]     = "Fire";
	stateTransitionOnTarget[7]       = "Fire";

	stateName[8]                     = "CheckWet";
	stateTransitionOnWet[8]          = "DryFire";
	stateTransitionOnNotWet[8]       = "CheckTarget";

	stateName[9]                     = "WetFire";
	stateTransitionOnNoAmmo[9]       = "NoAmmo";
	stateTransitionOnTimeout[9]      = "Reload";
	stateTimeoutValue[9]             = 0.4;
	stateScript[9]                   = "onWetFire";
	stateAllowImageChange[9]         = false;
};

function RPGImage::OnFire(%data, %obj, %slot) {
	//   if($TWM2::PlayingSabo) {     Inv. Ban lifted TWM2 1.8
	//      MessageClient(%thrower.client, 'MsgC4IsBanned', "\c5RPGs are banned in this game mode");
	//      return;
	//   }
	%p = Parent::OnFire(%data, %obj, %slot);
	if(!%obj.client.UpgradeOn("Spread Neutralizer", %data.getName())) {
		schedule(500, 0, "RPGRandomMovement", %p);
	}
	if(%obj.client.UpgradeOn("Enhanced Explosives", %data.getName())) {
		%p.damageFactor = 5; //heh
	}
	%p.ticksleft = 10;
}

function RPGRandomMovement(%p) {
	if(!isObject(%p)) {
		return;
	}
	if(%p.ticksleft <= 0) { //time up!
		%p.getDatablock().onExplode(%p, %p.getPosition(), 1);
		%p.delete();
		return;
	}	
	%fw = %p.initialDirection; //current direction
	%x = (getRandom() - 0.5) * 2 * 3.1415926 * (25/1000);
	%y = (getRandom() - 0.5) * 2 * 3.1415926 * (25/1000);
	%z = (getRandom() - 0.5) * 2 * 3.1415926 * (10/1000);
	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
	%newvector = MatrixMulVector(%mat, %fw);
	//now lets use basic torque operations to spawn a new RPG missile going the new direction
	%pnew = new (SeekerProjectile)() {
		dataBlock        = RPGMissile;
		initialDirection = %newvector;
		initialPosition  = %p.getPosition();
		damageFactor     = %p.damageFactor;
		//sourceObject     = %p.sourceObject;  // <-- done below to prevent spawning suicidal missiles
	};
	MissionCleanup.add(%pnew);
	%pnew.sourceObject = %p.sourceObject;
	%pnew.WeaponImageSource = %p.WeaponImageSource;
	%pnew.ticksleft = %p.ticksleft - 1;
	%pnew.ImageSource = "RPGImage";
	//echo(%pnew.ticksleft);
	%p.delete();
	schedule(500, 0, "RPGRandomMovement", %pnew);
}
