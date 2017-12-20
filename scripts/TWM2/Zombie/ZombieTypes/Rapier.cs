$TWM2::ArmorHasCollisionFunction[RapierZombieArmor] = true;

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

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::Missile] = 100.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;

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

function RapierZombieArmor::armorCollisionFunction(%datablock, %zombie, %colPlayer) {
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
	
	%total = %colPlayer.infected ? (%baseDamage * %multiplier) : %baseDamage;	
	
	%chance = getRandom(1, 3);
	if(%chance != 3) {
		%zombie.iscarrying = true;
		%colPlayer.grabbed = true;
		%colPlayer.damage(0, %colPlayer.getPosition(), %baseDamage, $DamageType::Zombie);
		%zombie.killingPlayer = %datablock.zCarryLoop(%zombie, %colPlayer, 0);
	}
	else {
		%colPlayer.damage(0, %colPlayer.getPosition(), %total, $DamageType::Zombie);
		if(%causeInfect) {
			%colPlayer.Infected = 1;
			%colPlayer.InfectedLoop = schedule(10, %colPlayer, "TWM2Lib_Zombie_Core", "InfectLoop", %colPlayer, "impact");	
		}
	}
}

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
		TWM2Lib_Zombie_Core("playZAudio", %zombie, 20, 12);
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