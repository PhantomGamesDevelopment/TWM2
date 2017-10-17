datablock PlayerData(RapierZombieArmor) : LightMaleBiodermArmor {
	maxDamage = 1.0;
	minImpactSpeed = 50;
	speedDamageScale = 0.015;
	maxEnergy =  80;
	repairRate = 0.0033;
	energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

	rechargeRate = 0.256;
	jetForce = 25.22 * 130 * 1.5;
	underwaterJetForce = 25.22 * 130 * 1.5;
	underwaterVertJetFactor = 1.5;
	jetEnergyDrain =  1.0;
	underwaterJetEnergyDrain =  0.6;
	minJetEnergy = 1;
	maxJetHorizontalPercentage = 0.8;

	boundingBox = "2.0 2.0 1.2";

	damageScale[$DamageType::M1700] = 2.0;
	damageScale[$DamageType::Missile] = 100.0;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock ShapeBaseImageData(ZWingImage) {
	shapeFile = "flag.dts";
	mountPoint = 1;

	offset = "0 0 0"; // L/R - F/B - T/B
	rotation = "0 1 0 90"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ZWingImage2) {
	shapeFile = "flag.dts";
	mountPoint = 1;

	offset = "0 0 0"; // L/R - F/B - T/B
	rotation = "0 -1 0 90"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ZWingAltImage) {
	shapeFile = "flag.dts";
	mountPoint = 1;

	offset = "0 0 0"; // L/R - F/B - T/B
	rotation = "-0.5 2 0 35"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ZWingAltImage2) {
	shapeFile = "flag.dts";
	mountPoint = 1;

	offset = "0 0 0"; // L/R - F/B - T/B
	rotation = "-0.5 -2 0 35"; // L/R - F/B - T/B
};

function RapierZombieArmor::AI(%datablock, %zombie) {
	//No special AI here, we'll let %block.move() handle it...
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%zombie.setHeat(999);
	%zombie.setActionThread("scoutRoot",true);
	%datablock.move(%zombie);
	%datablock.schedule(%zombie.updateTimeFrequency, "AI", %zombie);
}

function RapierZombieArmor::move(%datablock, %zombie) {
	%pos = %zombie.getworldboxcenter();

	%targetParams = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
	%targetClient = getWord(targetParams, 0);
	%distance = getWord(%targetParams, 1);	
	%target = %targetClient.player;
	if(%distance <= $zombie::detectDist) {
		if(%zombie.wingset == 1){ 
			%zombie.wingset = 0;
			%Zombie.mountImage(ZWingImage, 3);
			%Zombie.mountImage(ZWingImage2, 4);
		}
		else{
			%zombie.wingset = 1;
			%Zombie.mountImage(ZWingaltImage, 3);
			%Zombie.mountImage(ZWingaltImage2, 4);
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
		if(%zombie.iscarrying == 1) {
			%vector = vectorscale(%zombie.getForwardVector(), (%zombie.speed / 2));
			%vector = getword(%vector, 0)@" "@getword(%vector, 1)@" "@($Zombie::RapierUpwardScaling * 1.5);
			%zombie.applyImpulse(%zombie.getposition(), %vector);
		}
		else {
			%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());

			%z = Getword(%vector,2);
			%spd = vectorLen(%zombie.getVelocity());
			%fallpoint = 0.05 - (%spd / 630);
			if(%distance <= 15 || %z > (0.25 + %fallpoint) || %z < (-1 * (0.25 + %fallpoint))) {
				if(%z < 0) {
					%upvec = ($Zombie::RapierUpwardScaling * (%z - (%spd / 130)));
				}
				if(%z >= 0) {
					%upvec = ($Zombie::RapierUpwardScaling * (%z + 1));
				}
				if(%spd <= 5) {
					%vector = vectorScale(%vector,3);
				}
			}
			else {
				%upvec = $Zombie::RapierUpwardScaling * (%z + 1.2);
				%spdmod = 1;
			}
			if(%z < 0) {
				%z = %z * -1;
			}

			%Zz = getWord(%zombie.getVelocity(), 2);
			if(%Zz <= -40) {
				%result = containerRayCast(%pos, vectoradd(%pos, vectorScale("0 0 1", %Zz * 2)), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %zombie);
				if(%result) {
					%upvec = $Zombie::RapierUpwardScaling * 5;
				}
			}

			%vector = vectorscale(%vector, (%zombie.speed * (1 - %z)));
			%x = Getword(%vector,0);
			%y = Getword(%vector,1);
			%vector = %x@" "@%y@" "@%upvec;
			%zombie.applyImpulse(%pos, %vector);
		}
	}
}

function RapierZombieArmor::zCarryLoop(%datablock, %zombie, %target, %count) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		%zombie.iscarrying = 0;
		%target.grabbed = 0;
		return;
	}
	if(!isObject(%target) || %target.getState() $= "dead") {
		%zombie.iscarrying = 0;
		%target.grabbed = 0;
		return;
	}
	if(%count == 50) {
		%chance = getRandom(1, 3);
		if(%chance == 3) {
			%target.damage(0, %target.getPosition(), 10.0, $DamageType::Zombie);
		}
		else {
			%target.isFTD = 1;
			if(%target.getMountedImage($Backpackslot) !$= "") {
				%target.throwPack();
			}
			%target.finishingfall = schedule(5000, 0, "eval", ""@%target@".isFTD = 0; "@%target@".grabbed = 0;");
		}
		%zombie.iscarrying = 0;
		return;
	}
	%target.setPosition(vectoradd(%zombie.getPosition(),"0 0 -4"));
	%target.setVelocity(%zombie.getVelocity());
	%count++;
	%zombie.killingplayer = %datablock.schedule(100, "zCarryLoop", %zombie, %target, %count);
}