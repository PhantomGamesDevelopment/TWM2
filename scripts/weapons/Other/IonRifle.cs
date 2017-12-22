datablock ItemData(IonRifle) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "weapon_sniper.dts";
	image        = IonRifleImage;
	mass         = 1;
	elasticity   = 0.2;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName = "a LUX Ion Rifle";

	computeCRC = true;
};

datablock ShapeBaseImageData(IonRifleImage) {
	className = WeaponImage;
	shapeFile = "weapon_sniper.dts";
	item = IonRifle;
	projectile = ShockBeam;
	projectileType = SniperProjectile;
	armThread = looksn;

	MedalRequire = 9;

	usesEnergy = true;
	minEnergy = 6;

	stateName[0]                     = "Activate";
	stateTransitionOnTimeout[0]      = "ActivateReady";
	stateSound[0]                    = SniperRifleSwitchSound;
	stateTimeoutValue[0]             = 0.5;
	stateSequence[0]                 = "Activate";

	stateName[1]                     = "ActivateReady";
	stateTransitionOnLoaded[1]       = "Ready";
	stateTransitionOnNoAmmo[1]       = "NoAmmo";

	stateName[2]                     = "Ready";
	stateTransitionOnNoAmmo[2]       = "NoAmmo";
	stateTransitionOnTriggerDown[2]  = "CheckWet";

	stateName[3]                     = "Fire";
	stateTransitionOnTimeout[3]      = "Reload";
	stateTimeoutValue[3]             = 0.5;
	stateFire[3]                     = true;
	stateAllowImageChange[3]         = false;
	stateSequence[3]                 = "Fire";
	stateScript[3]                   = "onFire";

	stateName[4]                     = "Reload";
	stateTransitionOnTimeout[4]      = "Ready";
	stateTimeoutValue[4]             = 0.5;
	stateAllowImageChange[4]         = false;

	stateName[5]                     = "CheckWet";
	stateTransitionOnWet[5]          = "DryFire";
	stateTransitionOnNotWet[5]       = "Fire";

	stateName[6]                     = "NoAmmo";
	stateTransitionOnAmmo[6]         = "Reload";
	stateTransitionOnTriggerDown[6]  = "DryFire";
	stateSequence[6]                 = "NoAmmo";

	stateName[7]                     = "DryFire";
	stateSound[7]                    = SniperRifleDryFireSound;
	stateTimeoutValue[7]             = 0.5;
	stateTransitionOnTimeout[7]      = "Ready";
};

function IonRifleImage::OnFire(%data, %obj, %slot) {
	ServerPlay3D(thunderCrash2, %obj.getPosition());
	%pos = %obj.getMuzzlePoint(%slot);
	%vec = %obj.getMuzzleVector(%slot);
	%res = containerRayCast(%pos,vectorAdd(%pos,vectorScale(%vec,2000)), $TypeMasks::PlayerObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType,%obj);
	%HObj = getWord(%res, 0);
	if(%HObj !$= "" && %HObj != 0) {
		if(!(%HObj.getType() & ($TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType))) {
			%cn = %HObj.getDatablock().getClassName();
			if(%cn $= "PlayerData" || strstr(%cn, "Vehicle") != -1) {
				%HObj.getDataBlock().damageObject(%HObj, %obj, %HObj.getPosition(), 0.45, $DamageType::Lightning);
			}
		}
	}
	if (%res) {
		%hitLoc = getWords(%res,1,3);
	}
	else {
		%hitLoc = vectorAdd(%pos,vectorScale(%vec,2000));
	}
	%p = discharge(%pos,%vec);
	%p.setEnergyPercentage(1);
	createLifeLight(%hitLoc,1,1000);
	addToShock(%p);
	%p.schedule(1000,"delete");
	zap(0,25,%hitLoc);
	ServerPlay3D(thunderCrash2, %hitLoc);
}
