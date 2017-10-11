datablock PlayerData(ZombieArmor) : LightMaleHumanArmor {
	runForce = 60.20 * 90;
	runEnergyDrain = 0.0;
	minRunEnergy = 10;
	maxForwardSpeed = 9;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;

	jumpForce = 14.0 * 90;

	maxDamage = 2.8;
	minImpactSpeed = 35;
	shapeFile = "bioderm_medium.dts";
	jetEmitter = BiodermArmorJetEmitter;
	jetEffect =  BiodermArmorJetEffect;

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = LightBiodermFootprint;
	decalOffset = 0.3;

	waterBreathSound = WaterBreathBiodermSound;

	damageScale[$DamageType::M1700] = 3.0;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function ZombieArmor::AI(%datablock, %zombie) {
	//Normal zombies do not employ any "AI" other than target and move, fork off to main move function
	%datablock.MoveToTarget(%zombie);
}

function ZombieArmor::MoveToTarget(%datablock, %zombie) {
	if(!isobject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%pos = %zombie.getWorldBoxCenter();
	%closestClient = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
	%closestDistance = getWord(%closestClient, 1);
	%closestClient = getWord(%closestClient, 0).Player;
	if(%closestDistance <= $zombie::detectDist) {
		if(%zombie.hastarget != 1) {
			%zombie.hastarget = 1;
		}
		%chance = (getrandom() * 20);
		if(%chance >= 19) {
			%chance = (getRandom() * 12);
			if(%chance <= 11) {
				serverPlay3d("ZombieMoan", %zombie.getWorldBoxCenter());
			}
			else {
				serverPlay3d("ZombieHOWL", %zombie.getWorldBoxCenter());
			}
		}
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %closestClient, %pos);
	
		if(Game.CheckModifier("SuperLunge") == 1) {
			%ld = $Zombie::LungeDistance * 5;
		}
		else {
			%ld = $Zombie::LungeDistance;
		}
		if(%closestDistance <= %ld && %zombie.canjump == 1) {
			%vector = vectorScale(%vector, (%zombie.speed * 4));
		}
		%vector = vectorScale(%vector, %zombie.speed);
		%upvec = "150";
		if(%closestDistance <= %ld && %zombie.canjump == 1) {
			%upvec *= 2;
			TWM2Lib_Zombie_Core("setZFlag", %zombie, "canJump", 0);
			schedule($Zombie::BaseJumpCooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canJump", 1);
		}
		%x = Getword(%vector, 0);
		%y = Getword(%vector, 1);
		%z = Getword(%vector, 2);
		if(%z >= 600) {
			%upvec = (%upvec * 5);
		}
		%vector = %x@" "@%y@" "@%upvec;
		%zombie.applyImpulse(%pos, %vector);
	}
	else if(%zombie.hastarget == 1) {
		%zombie.hastarget = 0;
		%zombie.zombieRmove = schedule($Zombie::SpeedUpdateTime, %zombie, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %zombie);
	}
	%zombie.moveloop = %datablock.schedule($Zombie::SpeedUpdateTime, %datablock, "MoveToTarget", %zombie);
}
