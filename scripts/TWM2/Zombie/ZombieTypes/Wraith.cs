datablock PlayerData(WraithZombieArmor) : LightMaleHumanArmor {
	boundingBox = "1.63 1.63 2.6";
	maxDamage = 4.0;
	minImpactSpeed = 35;
	shapeFile = "bioderm_heavy.dts";

	//shields mo-fo's
	shieldHealthCharge = 0.05;
	maxShieldLevel = 4.0;

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
	damageScale[$DamageType::deserteagle] = 2.5;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

//WRAITH ZOMBIE AI.
function StartWraithAI(%zombie) {
   if(!isobject(%zombie)) {
      return;
   }
   if(%zombie.getState() $= "dead") {
	  return;
   }
   //split into sectored if's
   // * Are My Shields Low, or Down?
   %zombie.setImageTrigger(0, false);
   if(%zombie.getShieldPercent() <= 25) {
      schedule(500, 0, "StartWraithAI", %zombie);
      WraithZombieAI_Retreat(%zombie);
      return;
   }
   // * Is There Enemies?
   %closestClient = ZombieLookForTarget(%zombie);
   if(isSet(getWord(%closestClient, 0))) {
      //   ** Do I attack?
      if(getWord(%closestClient, 1) < 60) {
         //determine short range attack style
         //all of them have their own offset of AI, so this function is temporarily halted
         %style = getRandom(1, 3);
         switch(%style) {
            case 1:
               //1: Spiker Assault
               WraithAttack_SpikerAssault(%zombie);
               return;
            case 2:
               //2: Rush Based
               WraithAttack_RushBased(%zombie);
               return;
            case 3:
               //3: Steady
               WraithAttack_Steady(%zombie);
               return;
         }
      }
      else {
         //move into range (< 80)
         WraithMoveIntoRange(%zombie, %closestClient);
      }
   //
   }
   //fast AI updates, all handled from this core, attack styles are the exception
   schedule(500, 0, "StartWraithAI", %zombie);
}

function WraithZombieAI_Retreat(%zombie) {
   //nearest player...
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;

   if(%closestDistance < 250) {    //runz0rs
      %TPos = %closestClient.getPosition();
      %tvel = %closestclient.getvelocity();
      %vec = vectorsub(%tpos,%pos);
      %dist = vectorlen(%vec);
      %velpredict = vectorscale(%tvel,(%dist / 50));
      %vector = vectoradd(%vec,%velpredict);
      %vector = vectornormalize(%vector);
      %x = getWord(%vector, 0);
      %y = getWord(%vector, 1);
      %finX = %x;
      %finY = %y * -1;
      %finalVec1 = %finX SPC %finY SPC 0;
      %finalVec = VectorScale(%finalVec1, $Zombie::DForwardSpeed*2);
      %zombie.setRotation(fullrot("0 0 0",%finalVec1));
      //Z is unimportant
      %zombie.applyImpulse(%pos, %finalVec);
   }
}

function WraithMoveIntoRange(%zombie, %closestClient) {
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist && %closestDistance > 60){

	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
    serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed / 3 * 2))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
}

//Spiker Assault
//My weapon.... will ensure they die..
//Basically, spams a shitload of spiker spikes, while slowly moving to the nearest enemy
function WraithAttack_SpikerAssault(%zombie) {
   if(!isobject(%zombie)) {
      return;
   }
   if(%zombie.getState() $= "dead") {
	  return;
   }
   if(%zombie.getShieldPercent() <= 25) {
      //Oh dammit, this isn't working, RUN! and re-consider!
      WraithZombieAI_Retreat(%zombie);
      StartWraithAI(%zombie);
      return;
   }
   //***************************************************
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
    if(%closestdistance > 65) {
       //too far away, reconsider now
       StartWraithAI(%zombie);
       return;
    }
	if(%zombie.hastarget != 1 && %closestdistance >= 10 && %closestdistance <= 50){
       %zombie.setImageTrigger(0, true);
	}
	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
    serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	if (%closestdistance >= 10 && %closestdistance <= 50) {
       %zombie.setImageTrigger(0, true); //FIRE!!!!
	}
	%vector = vectorscale(%vector, $Zombie::DForwardSpeed/7);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= (($Zombie::DForwardSpeed / 3 * 2)))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   //
   schedule(100, %zombie, "WraithAttack_SpikerAssault", %zombie);
   //
}

//Rush Based
//I join the ranks of my fellow soldiers... and we shall overcome
//Zombie prefers smash attack over using it's spiker
function WraithAttack_RushBased(%zombie) {
   if(!isobject(%zombie)) {
      return;
   }
   if(%zombie.getState() $= "dead") {
	  return;
   }
   if(%zombie.getShieldPercent() <= 25) {
      //Oh dammit, this isn't working, RUN! and re-consider!
      WraithZombieAI_Retreat(%zombie);
      StartWraithAI(%zombie);
      return;
   }
   //***************************************************
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
    if(%closestdistance > 120) {
       //too far away, reconsider now
       StartWraithAI(%zombie);
       return;
    }
	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
    serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed/5);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= (($Zombie::DForwardSpeed / 3 * 2)))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   //
   schedule(100, %zombie, "WraithAttack_RushBased", %zombie);
   //
}

//Steady
//It's like I'm a statue, but only worse, because I fire at hostiles who go by
//Prefers to stay behind, but in range of hostiles, and shoot at them, will go aggreesive if approached
function WraithAttack_Steady(%zombie) {
   if(!isobject(%zombie)) {
      return;
   }
   if(%zombie.getState() $= "dead") {
	  return;
   }
   if(%zombie.getShieldPercent() <= 25) {
      //Oh dammit, this isn't working, RUN! and re-consider!
      WraithZombieAI_Retreat(%zombie);
      StartWraithAI(%zombie);
      return;
   }
   //***************************************************
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
    if(%closestdistance > 65) {
       //too far away, reconsider now
       StartWraithAI(%zombie);
       return;
    }
	if(%zombie.hastarget != 1 && %closestdistance > 25 && %closestdistance <= 65){
       %zombie.setImageTrigger(0, true);
	}
	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
    serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	if (%closestdistance > 25 && %closestdistance <= 65) {
       %zombie.setImageTrigger(0, true); //FIRE!!!!
	}
    //
    if(%closestDistance <= 25) {
	   %vector = vectorscale(%vector, $Zombie::DForwardSpeed/7);
	   %upvec = "150";
	   %x = Getword(%vector,0);
	   %y = Getword(%vector,1);
	   %z = Getword(%vector,2);
	   if(%z >= (($Zombie::DForwardSpeed / 3 * 2)))
	      %upvec = (%upvec * 5);
	   %vector = %x@" "@%y@" "@%upvec;
	   %zombie.applyImpulse(%pos, %vector);
    }
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   //
   schedule(100, %zombie, "WraithAttack_Steady", %zombie);
   //
}
