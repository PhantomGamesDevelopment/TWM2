datablock PlayerData(RapierZombieArmor) : LightMaleBiodermArmor
{
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

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock StaticShapeData(fakeRapierZombie) : StaticShapeDamageProfile {
	className = "player";
	shapeFile = "bioderm_light.dts"; // dmiscf.dts, alternate
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 0;
    isInvincible = true;
};

datablock ShapeBaseImageData(ZWingImage)
{
   shapeFile = "flag.dts";
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "0 1 0 90"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ZWingImage2)
{
   shapeFile = "flag.dts";
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "0 -1 0 90"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ZWingAltImage)
{
   shapeFile = "flag.dts";
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "-0.5 2 0 35"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(ZWingAltImage2)
{
   shapeFile = "flag.dts";
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "-0.5 -2 0 35"; // L/R - F/B - T/B
};

function RZombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead")
	return;

   %zombie.setHeat(999);

   %pos = %zombie.getworldboxcenter();
   %zombie.setActionThread("scoutRoot",true);
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
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
       if(%chance <= 11)
	      serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());
       else
	      serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
    }
	if(%zombie.iscarrying == 1) {
	   %vector = vectorscale(%zombie.getForwardVector(),($Zombie::RForwardSpeed / 2));
	   %vector = getword(%vector, 0)@" "@getword(%vector, 1)@" "@($zombie::Rupvec * 1.5);
	   %zombie.applyImpulse(%zombie.getposition(), %vector);
	}
    else {

    %vector = ZgetFacingDirection(%zombie, %closestClient, %pos);

	%z = Getword(%vector,2);
	%spd = vectorLen(%zombie.getVelocity());
	%fallpoint = 0.05 - (%spd / 630);
	if(%closestdistance <= 15 || %z > (0.25 + %fallpoint) || %z < (-1 * (0.25 + %fallpoint))){
	   if(%z < 0)
	      %upvec = ($zombie::Rupvec * (%z - (%spd / 130)));
	   if(%z >= 0)
		%upvec = ($zombie::Rupvec * (%z + 1));
	   if(%spd <= 5)
		%vector = vectorScale(%vector,3);
	}
	else{
	   %upvec = $zombie::Rupvec * (%z + 1.2);
	   %spdmod = 1;
	}
	if(%z < 0)
	   %z = %z * -1;

	%Zz = getWord(%zombie.getVelocity(),2);
	if(%Zz <= -40){
         %result = containerRayCast(%pos, vectoradd(%pos,vectorScale("0 0 1",%Zz * 2)), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %zombie);
	   if(%result)
		%upvec = $zombie::Rupvec * 5;
	}

	%vector = vectorscale(%vector, ($Zombie::RForwardSpeed * (1 - %z)));
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
     }
   }
   %zombie.moveloop = schedule(500, %zombie, "RZombiemovetotarget", %zombie);
}

function RkillLoop(%zombie,%target,%count){
   if(!isObject(%zombie)){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if(!isObject(%target)){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if (%Zombie.getState() $= "dead"){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if (%target.getState() $= "dead"){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if(%count == 50){
	%chance = getRandom(1,3);
	if(%chance == 3)
	   %target.damage(0, %tpos, 10.0, $DamageType::Zombie);
	else{
	   %target.isFTD = 1;
	   if(%target.getMountedImage($Backpackslot) !$= "")
   	      %target.throwPack();
   	   %target.finishingfall = schedule(5000, 0, "eval", ""@%target@".isFTD = 0; "@%target@".grabbed = 0;");
	}
	%zombie.iscarrying = 0;
	return;
   }
   %target.setPosition(vectoradd(%zombie.getPosition(),"0 0 -4"));
   %target.setVelocity(%zombie.getVelocity());
   %count++;
   %zombie.killingplayer = schedule(100, 0, "RkillLoop", %zombie, %target, %count);
}
