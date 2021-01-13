$TWM2::ArmorHasCollisionFunction[DemonZombieArmor] = true;

datablock PlayerData(DemonZombieArmor) : LightMaleHumanArmor {
	boundingBox = "1.63 1.63 2.6";
	maxDamage = 4.0;
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
	damageScale[$DamageType::plasma] = 0.001;
	damageScale[$DamageType::Napalm] = 0.001;
	damageScale[$DamageType::Burn] = 0.001;
	damageScale[$DamageType::Fire] = 0.001;	
	damageScale[$DamageType::CrimsonHawk] = 1.9;
	damageScale[$DamageType::AcidCannon] = 3.0;
	damageScale[$DamageType::deserteagle] = 2.5;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

function DemonZombieArmor::armorCollisionFunction(%datablock, %zombie, %colPlayer) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	if(!isObject(%colPlayer) || %colPlayer.getState() $= "dead") {
		return;
	}
	//Check to make sure we're not hitting another zombie / boss
	if(%colPlayer.isBoss || %colPlayer.isZombie || %colPlayer.rapierShield) {
		return;
	}
	//Damage.
	%causeInfect = %zombie.damage_infectOnTouch;
	%baseDamage = %zombie.damage_amountOnTouch;
	%multiplier = %zombie.damage_alreadyInfectedMultiplier;
	
	//Phantom139 (11/20): Demons light players on fire, need to check for .onfire instead of the .infected flag.
	%total = %colPlayer.onfire ? (%baseDamage * %multiplier) : %baseDamage;
	
	%pushVector = vectorscale(%colPlayer.getvelocity(), 1000);
	%colPlayer.applyimpulse(%colPlayer.getposition(), %pushVector);
	if(%causeInfect) {
		//Phantom139: Demon Zombies now cause burns instead of infects
		//%colPlayer.Infected = 1;
		//%colPlayer.InfectedLoop = schedule(10, %colPlayer, "TWM2Lib_Zombie_Core", "InfectLoop", %colPlayer);
		%colPlayer.maxfirecount += (75 * (%total / 0.5));
		if(%colPlayer.onfire == 0 || %colPlayer.onfire $= ""){
			%colPlayer.onfire = 1;
			schedule(10, %colPlayer, "burnloop", %colPlayer);
		}		
	}
	%colPlayer.damage(0, %colPlayer.getPosition(), %total, $DamageType::Zombie);	
}

function DemonZombieArmor::AI(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%zPos = %zombie.getPosition();
	if(%zombie.hasTarget) {		
		if(!isObject(%zombie.targetedPlayer) || %zombie.targetedPlayer.getState() $= "dead") {
			%zombie.targetedPlayer = 0;
			%zombie.hasTarget = 0;
			%datablock.schedule(500, "AI", %zombie);
			return;			
		}
		%tPos = %zombie.targetedPlayer.getPosition();		
		%distance = vectorDist(%zPos, %tPos);
		if(%distance > $Zombie::DemonZombieFireBombMinRange && %distance <= $Zombie::DemonZombieFireBombMaxRange) {
			if(%zombie.canFireWeapon) {
				%datablock.zFire(%zombie, %zombie.targetedPlayer);
			}
		}
		%datablock.move(%zombie);
	}
	else {
		%targetParams = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
		%target = getWord(targetParams, 0);
		%distance = getWord(%targetParams, 1);	
		if(isObject(%target.player)) {
			//Got a target player... let's go!
			if(%distance <= $zombie::detectDist) {
				%zombie.hasTarget = 1;
				%zombie.targetedPlayer = %target.player;
			}
			%datablock.move(%zombie);
		}
		//Nothing to hunt... random movement...
		if(!%zombie.hasTarget) {
			%zombie.zombieRmove = schedule(500, %zombie, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %zombie);
		}	
	}
	%datablock.schedule(500, "AI", %zombie);
}

function DemonZombieArmor::move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}	
	%pos = %zombie.getWorldBoxCenter();
	%upvec = "150";
	%chance = (getrandom() * 20);
	if(%chance >= 19) {
		serverPlay3d("ZombieMoan", %zombie.getWorldBoxCenter());	
	}	
	%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.targetedPlayer.getPosition());
	%distance = vectorDist(%pos, %zombie.targetedPlayer.getPosition());
	if(%distance <= $Zombie::LungeDistance && %zombie.canjump == 1) {
		%vector = vectorScale(%vector, 4);
	}	
	%vector = vectorScale(%vector, %zombie.speed);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= (%zombie.speed / 3 * 2)) {
		%upvec = (%upvec * 5);
	}
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);	
}

function DemonZombieArmor::zFire(%datablock, %zombie, %targetObject) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	if(!isObject(%targetObject)) {
		return;
	}
	TWM2Lib_Zombie_Core("setZFlag", %zombie, "canFireWeapon", 0);
	%pos = %zombie.getMuzzlePoint(4);
	%tpos = %targetObject.getWorldBoxCenter();
	%tvel = %targetObject.getvelocity();
	%vec = vectorsub(%tpos, %pos);
	%dist = vectorlen(%vec);
	%velpredict = vectorscale(%tvel, (%dist / 50));
	%vector = vectoradd(%vec, %velpredict);
	%ndist = vectorlen(%vector);
	%upvec = "0 0 "@((%ndist / 50) * (%ndist / 50) * 2);
	%vector = vectornormalize(vectoradd(%vector, %upvec));
	if(Game.CheckModifier("ItBurns") == 1) {
		%p = new GrenadeProjectile() {
			dataBlock        = DemonFlamingFireball;
			initialDirection = %vector;
			initialPosition  = %pos;
			sourceObject     = %zombie;
			sourceSlot       = 4;
		};
	}
	else {
		%p = new GrenadeProjectile() {
			dataBlock        = DemonFireball;
			initialDirection = %vector;
			initialPosition  = %pos;
			sourceObject     = %zombie;
			sourceSlot       = 4;
		};
	}	
	schedule($Zombie::DemonZombieFireBombCooldown, 0, "TWM2Lib_Zombie_Core", "setZFlag", %zombie, "canFireWeapon", 1);
}