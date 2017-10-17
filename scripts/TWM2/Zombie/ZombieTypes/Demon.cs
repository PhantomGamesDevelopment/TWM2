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

	damageScale[$DamageType::M1700] = 2.0;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

function DemonZombieArmor::AI(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%zPos = %zombie.getPosition();
	if(%zombie.hasTarget) {		
		if(!isObject(%zombie.targetedPlayer) || %zombie.targetedPlayer.getState() $= "dead") {
			%zombie.targetedPlayer = 0;
			%zombie.hasTarget = 0;
			%datablock.schedule(%zombie.updateTimeFrequency, "AI", %zombie);
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
			%zombie.zombieRmove = schedule(%zombie.updateTimeFrequency, %zombie, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %zombie);
		}	
	}
	%datablock.schedule(%zombie.updateTimeFrequency, "AI", %zombie);
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