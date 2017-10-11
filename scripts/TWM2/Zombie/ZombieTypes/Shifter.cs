datablock PlayerData(ShifterZombieArmor) : LightMaleHumanArmor
{
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 2.8;
   minImpactSpeed = 35;
   shapeFile = "bioderm_light.dts";
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

function ShifterZombiemovetotarget(%zombie){
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
	%chance = (getrandom() * 20);
   	if(%chance >= 19) {
       %chance = (getRandom() * 12);
       if(%chance <= 11)
	      serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());
       else
	      serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
    }

    %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%fmultiplier = $Zombie::ForwardSpeed;
	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1)
	   %fmultiplier = (%fmultiplier * 4);
	%vector = vectorscale(%vector, %Fmultiplier);
	%upvec = "150";

    //tis shift timez :)
    if(%closestDistance > 200 || (%zombie.getVelocity() == 0 && !%zombie.RecentShift)) {
		%zombie.setMoveState(true);
       	%zombie.startFade(1500, 0, true);
        %zombie.schedule(1600, "SetPosition", VectorAdd(%closestClient.getPosition(), vectorAdd("0 0 3", TWM2Lib_MainControl("getRandomPosition", "5\t1"))));
        %zombie.startFade(1750, 0, false);
		%zombie.schedule(2000, setMoveState, false);
        %zombie.RecentShift = 1;
        Schedule(10500, 0, "eval", ""@%zombie@".RecentShift=0;");
    }

	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1){
	   %upvec = %upvec * 2;
	   %zombie.canjump = 0;
	   schedule(1500, %zombie, "Zsetjump", %zombie);
	}
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= 600)
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "ShifterZombiemovetotarget", %zombie);
}
