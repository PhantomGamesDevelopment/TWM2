// Pulse Phaser Weapon
datablock AudioProfile(PPWFireSound) {
	filename    = "fx/powered/turret_outdoor_fire.wav";
	description = AudioDefault3d;
	preload = true;
};

datablock LinearFlareProjectileData(phaserBolt) {
	directDamage        = 0.26;   
	directDamageType    = $DamageType::ES77PulsePhaser;
	explosion           = "BlasterExplosion";
	kickBackStrength  = 0.0;

	dryVelocity       = 165.0;
	wetVelocity       = 40.0;
	velInheritFactor  = 0.5;
	fizzleTimeMS      = 700;
	lifetimeMS        = 800;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = false;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 3000;

	ImageSource         = "PulsePhaserImage";

	numFlares         = 20;
	size              = 0.10;
	flareColor        = "0 1 0";
	flareModTexture   = "flaremod";
	flareBaseTexture  = "flarebase";

	sound = BlasterProjectileSound;

	hasLight    = true;
	lightRadius = 3.0;
	lightColor  = "1 1 1";
};

datablock LinearFlareProjectileData(TrevorPhaserBolt) {
	directDamage        = 0.3;
	directDamageType    = $DamageType::ES77PulsePhaser;
	explosion           = "BlasterExplosion";
	kickBackStrength  = 0.0;

	dryVelocity       = 160.0;
	wetVelocity       = 40.0;
	velInheritFactor  = 0.5;
	fizzleTimeMS      = 700;
	lifetimeMS        = 800;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = false;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 3000;

	ImageSource         = "PulsePhaserImage";

	numFlares         = 20;
	size              = 0.10;
	flareColor        = "0.5 0.1 0.9";
	flareModTexture   = "flaremod";
	flareBaseTexture  = "flarebase";

	sound = BlasterProjectileSound;

	hasLight    = true;
	lightRadius = 3.0;
	lightColor  = "1 1 1";
};

datablock ShapeBaseImageData(PulsePhaserImage) {
	className = WeaponImage;
	shapeFile = "weapon_energy.dts";
	item = PulsePhaser;
	projectile = phaserBolt;
	projectileType = LinearFlareProjectile;

	HasChallenges = 1;
	ChallengeCt = 10;
	Challenge[1] = "Phaser Killer\t50\t100\tNone";
	Challenge[2] = "Phaser Hunter\t150\t250\tNone";
	Challenge[3] = "Phaser Expert\t250\t500\tLaser";
	Challenge[4] = "Phaser Master\t500\t1000\tTriple Burst";
	Challenge[5] = "Phaser God\t1000\t2000\tShotgun";
	Challenge[6] = "Phaser Legend\t1500\t5000\tPhaser Blades";
	Challenge[7] = "Phaser Bronze Commendation\t3000\t10000\tNone";
	Challenge[8] = "Phaser Silver Commendation\t6000\t25000\tNone";
	Challenge[9] = "Phaser Gold Commendation\t12500\t50000\tNone";
	Challenge[10] = "Phaser Titan Commendation\t35000\t75000\tNone";
	Upgrade[1] = "Laser";
	Upgrade[2] = "Triple Burst";
	Upgrade[3] = "Shotgun";
	Upgrade[4] = "Phaser Blades";
	GunName = "ES-77 Pulse Phaser";

	RankRequire = $TWM2::RankRequire["PulsePhaser"];

	usesEnergy = true;
	fireEnergy = 4;
	minEnergy = 4;

	stateName[0] = "Activate";
	stateTransitionOnTimeout[0] = "ActivateReady";
	stateTimeoutValue[0] = 0.5;
	stateSequence[0] = "Activate";
	stateSound[0] = BlasterSwitchSound;

	stateName[1] = "ActivateReady";
	stateTransitionOnLoaded[1] = "Ready";
	stateTransitionOnNoAmmo[1] = "NoAmmo";

	stateName[2] = "Ready";
	stateTransitionOnNoAmmo[2] = "NoAmmo";
	stateTransitionOnTriggerDown[2] = "Fire";

	stateName[3] = "Fire";
	stateTransitionOnTimeout[3] = "Reload";
	stateTimeoutValue[3] = 0.3;
	stateFire[3] = true;
	stateRecoil[3] = NoRecoil;
	stateAllowImageChange[3] = false;
	stateSequence[3] = "Fire";
	stateSound[3] = PPWFireSound;
	stateScript[3] = "onFire";

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateAllowImageChange[4] = false;
	stateSequence[4] = "Reload";

	stateName[5] = "NoAmmo";
	stateTransitionOnAmmo[5] = "Reload";
	stateSequence[5] = "NoAmmo";
	stateTransitionOnTriggerDown[5] = "DryFire";

	stateName[6] = "DryFire";
	stateTimeoutValue[6] = 0.3;
	stateSound[6] = BlasterDryFireSound;
	stateTransitionOnTimeout[6] = "Ready";
};

datablock ItemData(PulsePhaser) {
	className = Weapon;
	catagory = "Spawn Items";
	shapeFile = "weapon_energy.dts";
	image = PulsePhaserImage;
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "a Pulse Phaser";
};

datablock ShapeBaseImageData(Phaser2Image) : PulsePhaserImage {
	shapeFile = "weapon_ELF.dts";
	offset = "0 0.3 0.2";
	rotation = "0 1 0 180";
};

datablock ShapeBaseImageData(Phaser3Image) : PulsePhaserImage {
	shapeFile = "weapon_ELF.dts";
	offset = "0 .5 0";
};

function PulsePhaserImage::onMount(%this, %obj, %slot) {
	Parent::onMount(%this, %obj, %slot);
	if(%obj.client.UpgradeOn("Phaser Blades", %this.getName())) {
		%obj.mountImage(Phaser2Image, 5);
		%obj.mountImage(Phaser3Image, 6);
	}
}

function PulsePhaserImage::onUnmount(%this,%obj,%slot) {
	%obj.unmountImage(5);
	%obj.unmountImage(6);
	Parent::onUnmount(%this, %obj, %slot);
}


function PulsePhaserImage::onFire(%data,%obj,%slot) {
	if(%obj.cannotFire) {
		return;
	}
	%vector = %obj.getMuzzleVector(%slot);
	%mp = %obj.getMuzzlePoint(%slot);
	if(%obj.client.UpgradeOn("Triple Burst", %data.getName())) {
		%p1 = Parent::OnFire(%data, %obj, %slot);

		%p2 = Schedule(100, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType,
		vectorAdd(%obj.getPosition(), "-.45 0 1.4"), %vector, %obj);
		%p2.WeaponImageSource = "PulsePhaserImage";
		schedule(100, 0, "ServerPlay3D", "PPWFireSound", %obj.getPosition());

		%p3 = Schedule(250, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType,
		vectorAdd(%obj.getPosition(), "-.45 0 1.4"), %vector, %obj);
		%p3.WeaponImageSource = "PulsePhaserImage";
		schedule(250, 0, "ServerPlay3D", "PPWFireSound", %obj.getPosition());

		ResetFireRestrict(%obj, 1);
		schedule(1350, 0, "ResetFireRestrict", %obj, 0);
	}
	else if(%obj.client.UpgradeOn("Shotgun", %data.getName())) {
		for (%i=0; %i < 3; %i++) {
			%x = (getRandom() - 0.5) * 2 * 3.1415926 * (11/1000);
			%y = (getRandom() - 0.5) * 2 * 3.1415926 * (11/1000);
			%z = (getRandom() - 0.5) * 2 * 3.1415926 * (11/1000);
			%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
			%newvector = MatrixMulVector(%mat, %vector);

			%p = new (%data.projectileType)() {
				dataBlock        = %data.projectile;
				initialDirection = %newvector;
				initialPosition  = %mp;
				sourceObject     = %obj;
				damageFactor	 = 1;
				sourceSlot       = %slot;
			};

			%p.WeaponImageSource = "PulsePhaserImage";
		}
		ResetFireRestrict(%obj, 1);
		schedule(1200, 0, "ResetFireRestrict", %obj, 0);
	}
	else if(%obj.client.UpgradeOn("Phaser Blades", %data.getName())) {
		serverPlay3D("BomberTurretFireSound", %obj.getPosition());
		%p = new (%data.projectileType)() {
			dataBlock        = TrevorPhaserBolt;
			initialDirection = %vector;
			initialPosition  = %mp;
			sourceObject     = %obj;
			damageFactor	 = 1;
			sourceSlot       = %slot;
		};
		%p.WeaponImageSource = "PulsePhaserImage";
	}
	else {
		%p = Parent::OnFire(%data,%obj,%slot);
	}
}

function ResetFireRestrict(%plyr, %onOff) {
	%plyr.cannotFire = %onOff;
}