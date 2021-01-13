datablock PlayerData(VolatileRavagerZombieArmor) : LightMaleBiodermArmor {
	maxDamage = 1.0;
	minImpactSpeed = 50;
	speedDamageScale = 0.015;

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;
	damageScale[$DamageType::AcidCannon] = 3.0;
	damageScale[$DamageType::deserteagle] = 2.5;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock ShapeBaseImageData(ZExplosivePack) {
	shapeFile = "pack_upgrade_satchel.dts";
	emap = false;
};

function VRavZombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
	if(%zombie.hastarget != 1){
	   %zombie.hastarget = 1;
	}
	%zombie.setActionThread("scoutRoot",true);
	%upvec = "250";
    if(Game.CheckModifier("Kamakazi") == 1) {
	   %fmultiplier = $Zombie::FForwardSpeed * 0.5 * 5;
    }
    else {
       %fmultiplier = $Zombie::FForwardSpeed * 0.5;
    }

    //ka-booma :)
    if(%closestDistance < 9) {
       if(%zombie.isAlive()) {
          if(Game.CheckModifier("TheDestiny") == 1) {
             ServerPlay3D("SatchelChargeExplosionSound", %zombie.getPosition());
             %c4 = new Item() {
                datablock = SatchelChargeThrown;
                position = %zombie.getPosition();
                scale = ".1 .1 .1";
             };
             MissionCleanup.add(%c4);
             schedule(770, 0, "C4GoBoom", %c4);
             return;
          }
          else {
             ServerPlay3D("SatchelChargeExplosionSound", %zombie.getPosition());
             %c4 = new Item() {
                datablock = C4Deployed;
                position = %zombie.getPosition();
                scale = ".1 .1 .1";
             };
             MissionCleanup.add(%c4);
             schedule(770, 0, "C4GoBoom", %c4);
             return;
          }
       }
    }

	//moanStuff
	%chance = (getrandom() * 50);
   	if(%chance >= 49) {
       %chance = (getRandom() * 12);
       if(%chance <= 11)
	      serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());
       else
	      serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
    }

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	//Move Stuff
	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1 && getword(%vector, 2) <= "0.8" ){
	   %zombie.setvelocity("0 0 0");
	   %fmultiplier = (%fmultiplier * 2);
	   %upvec = (%upvec * 3.5);
	   %zombie.canjump = 0;
	   schedule(2000, %zombie, "Zsetjump", %zombie);
	}
	%vector = vectorscale(%vector, %Fmultiplier);
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= "1200" && %zombie.canjump == 1){
	   %zombie.setvelocity("0 0 0");
	   %upvec = (%upvec * 8);
	   %x = (%x * 0.5);
	   %y = (%y * 0.5);
	   %zombie.canjump = 0;
	   schedule(2500, %zombie, "Zsetjump", %zombie);
	}

	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   	%zombie.setActionThread("ski",true);
   }
   %zombie.moveloop = schedule(500, %zombie, "VRavZombiemovetotarget", %zombie);
}
