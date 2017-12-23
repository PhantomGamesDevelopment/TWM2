//Phantom139 Note: The sniper zombie prefers to stay away from players, it does not infect on contact, but instead runs away
$TWM2::ArmorHasCollisionFunction[SniperZombieArmor] = false;
$TWM2::ArmorHasCollisionFunction[FlareguideSniperZombieArmor] = false;

datablock ShapeBaseImageData(ZSniperImage1) {
	shapeFile = "weapon_sniper.dts";
	emap = true;
	armThread = looksn;
};

datablock ShapeBaseImageData(ZSniperImage2) {
	shapeFile = "weapon_targeting.dts";
	offset = "0.0 1.0 0.41";
	rotation = "90 0 0 90";
	armThread = looksn;
	emap = true;
};

datablock ShapeBaseImageData(ZSniperImage3) {
	shapeFile = "weapon_elf.dts";
	offset = "0.0 0.3 0";
	rotation = "1 0 0 90";
	armThread = looksn;
	emap = true;
};

datablock PlayerData(SniperZombieArmor) : LightMaleHumanArmor {
	boundingBox = "1.63 1.63 2.6";
	maxDamage = 2.5;
	minImpactSpeed = 35;
	shapeFile = "bioderm_heavy.dts";

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = HeavyBiodermFootprint;
	decalOffset = 0.4;

	waterBreathSound = WaterBreathBiodermSound;

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;
	damageScale[$DamageType::AcidCannon] = 3.0;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

function SniperZombieArmor::AI(%datablock, %zombie) {
	//Sniper Zombie AI:
	// Unlike other zombies, the sniper zombie will continue to hunt their target until killed.
	//  The sniper zombie will employ preferential targeting against enemy snipers first
	//
	// TWM2 3.9.2: Prior to this version, the sniper would run away when approached,
	//  now, the sniper is armed with a new acid sidearm that fires quick pulses and will
	//  employ strafing moves as well
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}	
	%pos = %zombie.getWorldBoxCenter();
	//Target update.
	if(%zombie.hadTarget) {
		if(!isObject(%zombie.targetPlayer) || %zombie.targetPlayer.getState() $= "dead") {
			//Target is down for the count, let's move on.
			%zombie.hasTarget = 0;
			%zombie.CQCTarget = 0;
			%zombie.reallyFarTarget = 0;
			%zombie.targetPlayer = 0;
		}
	}
	//Do I have a target, but it's far out (IE: Running or Pilot?)
	if(%zombie.hasTarget && %zombie.reallyFarTarget) {
		//Throw a 1 in 25 chance to abort and re-search
		if(getRandom(1, 25) == 19) {
			%zombie.hasTarget = 0;
			%zombie.targetPlayer = 0;
			%zombie.CQCTarget = 0;
			%zombie.reallyFarTarget = 0;
		}
	}
	//Are there any targets?
	if(!%zombie.hasTarget) {
		%preferSniper = getRandom(1, 10);
		if(%preferSniper > 4) {
			for(%i = 0; %i < ClientGroup.getCount(%i); %i++) {
				%check = ClientGroup.getObject(%i);
				if(isObject(%check.player) && %check.player.getState() !$= "dead") {
					//Check their weapon 
					%w = %check.player.getMountedImage($WeaponSlot).getName();
					if(!strStr(strlwr(%w), "sniper")) {
						//Bingo!
						%zombie.hasTarget = 1;
						%zombie.targetPlayer = %check.player;
						break;
					}
				}
			}
			//No enemy snipers found... Look for a new target.
			if(!%zombie.hasTarget) {
				%closestClient = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
				%closestDistance = getWord(%closestClient, 1);
				if(isObject(%closestClient.player) && %closestClient.player.getState() !$= "dead") {
					%zombie.hasTarget = 1;
					%zombie.targetPlayer = %closestClient.player;
				}	
			}
		}
		else {
			%closestClient = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
			%closestDistance = getWord(%closestClient, 1);
			if(isObject(%closestClient.player) && %closestClient.player.getState() !$= "dead") {
				%zombie.hasTarget = 1;
				%zombie.targetPlayer = %closestClient.player;
			}
		}
	}
	//Do I have a target?
	if(%zombie.hasTarget) {
		//Check the distance...
		%tPos = %zombie.targetPlayer.getPosition();
		%dist = vectorDist(%pos, %tPos);
		if(%dist < $Zombie::Sniper_SidearmRange) {
			//Target is getting into sidearm range... engage close range attacks
			%zombie.reallyFarTarget = 0;
			%zombie.CQCTarget = 1;
			%datablock.Move(%zombie);
			if(%zombie.canAltFire) {
				TWM2Lib_Zombie_Core("setZFlag", %zombie, "canAltFire", 0);
				schedule($Zombie::Sniper_RifleCooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canAltFire", 1);		
				for(%i = 0; %i < $Zombie::Sniper_SidearmBurstCount; %i++) {
					%timeDelay = ($Zombie::Sniper_SidearmBurstTime / $Zombie::Sniper_SidearmBurstCount) * %i;
					%datablock.schedule(%timeDelay, "zFire_Alternate", %zombie);
				}
			}			
		}
		else if(%dist < $Zombie::Sniper_MaximumEngageRange && %dist > $Zombie::Sniper_SidearmRange) {
			//Target is in Sniper Range... engage.
			%zombie.reallyFarTarget = 0;
			%zombie.CQCTarget = 0;
			if(vectorDist(%pos, %tPos) > $Zombie::Sniper_PreferedDistanceFromTarget) {
				//I'm a bit far out myself, let's move up while we engage.
				%datablock.Move(%zombie);
			}
			if(%zombie.canFireWeapon) {
				TWM2Lib_Zombie_Core("setZFlag", %zombie, "canFireWeapon", 0);
				schedule($Zombie::Sniper_RifleCooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canFireWeapon", 1);	
				%datablock.zFire(%zombie);
			}
		}
		else {
			//Target's a bit too far out, let's move up, but flag our AI to begin thinking about a new target
			%zombie.reallyFarTarget = 1;
			%zombie.CQCTarget = 0;
			%datablock.Move(%zombie);
		}
	}
	//No targets... Let's just wait.
	%zombie.aiLoop = %datablock.schedule(%zombie.updateTimeFrequency, "AI", %zombie);
}

function SniperZombieArmor::Move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%pos = %zombie.getWorldBoxCenter();
	%tPos = %zombie.targetPlayer.getPosition();
	%upVec = "250";
	//Are we fighting in close quarters or not?
	if(%zombie.CQCTarget) {
		%zombie.setVelocity("0 0 0");
		//FaceTarget(%zombie, %target);
		TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %tPos);
		%vector = vectorNormalize(vectorSub(%tPos, %zombie.getPosition()));
		%vector = vectorscale(%vector, %zombie.speed);
		%x = Getword(%vector, 0);
		%y = Getword(%vector, 1);
		%nv1 = %y;
		%nv2 = (%x * -1);
		%vector = %nv1@" "@%nv2@" 0";
		%zombie.applyImpulse(%zombie.getPosition(), %vector);		
	}
	else {
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %tPos);
		//Scale to speed
		%vector = vectorScale(%vector, %zombie.speed);
		%x = getWord(%vector, 0);
		%y = getWord(%vector, 1);	
		%vector = %x@" "@%y@" "@%upvec;
		%zombie.applyImpulse(%pos, %vector);		
	}
}

function SniperZombieArmor::zFire(%datablock, %zombie) {
	%target = %zombie.targetPlayer;
	%num = getRandom(250, 1000);
	%vec = vectorsub(VectorAdd(%target.getPosition(), "0 0 2.2"), %zombie.getMuzzlePoint(4));
	%accuracy = (vectorlen(%vec) / %num);
	%vec = vectoradd(%vec, vectorscale(%target.getvelocity(), %accuracy));
	%p = new TracerProjectile() { 
		dataBlock        = SniperZombieAcidShot;
		initialDirection = %vec;
		initialPosition  = %zombie.getMuzzlePoint(4);
		sourceObject     = %zombie;
		sourceSlot       = 4;
	};
	ServerPlay3D(CentaurArtilleryFireSound, %zombie.getPosition());
}

function SniperZombieArmor::zFire_Alternate(%datablock, %zombie) {
	%target = %zombie.targetPlayer;
	%num = getRandom(250, 1000);
	%vec = vectorsub(VectorAdd(%target.getPosition(), "0 0 1"), %zombie.getMuzzlePoint(4));
	%accuracy = (vectorlen(%vec) / %num);
	%vec = vectoradd(%vec, vectorscale(%target.getvelocity(), %accuracy));
	%p = new TracerProjectile() { 
		dataBlock        = SniperZombieAcidSidearmShot;
		initialDirection = %vec;
		initialPosition  = %zombie.getMuzzlePoint(4);
		sourceObject     = %zombie;
		sourceSlot       = 4;
	};
	ServerPlay3D(CentaurArtilleryFireSound, %zombie.getPosition());	
}

//*****************************************************************
//*****************************************************************
// FLAREGUIDE SNIPER ZOMBIE
//
// This is contained within this file as it requries the sniper zombie
//  datablock in order to load, therefore we'll just leave it down here
//
//*****************************************************************
//*****************************************************************

datablock PlayerData(FlareguideSniperZombieArmor) : SniperZombieArmor {
	boundingBox = "1.63 1.63 2.6";
	maxDamage = 20.0;
	minImpactSpeed = 35;
	shapeFile = "bioderm_heavy.dts";

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = HeavyBiodermFootprint;
	decalOffset = 0.4;

	waterBreathSound = WaterBreathBiodermSound;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

function FlareguideSniperZombieAcidShot::onExplode(%data, %proj, %pos, %mod) {
	Parent::OnExplode(%data, %proj, %pos, %mod);
	//Create the mini-pulses
	for (%i = 0; %i < 6; %i++) {
		%x = getRandom(-3, 3);
		%y = getRandom(-3, 3);
		%z = 5;
		%vec = %x SPC %y SPC %z;
		%vec = VectorScale(%vec, 200);
		%p = new (GrenadeProjectile)() {
			dataBlock = FlareguideSniperBurstRound;
			initialDirection = %vec;
			initialPosition = %pos;
		};
		MissionCleanup.add(%p);
		%p.sourceObject = %proj.sourceObject;
		return;
	}	
}