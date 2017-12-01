datablock PlayerData(SummonerZombieArmor) : LightMaleHumanArmor {
	runForce = 60.20 * 90;
	runEnergyDrain = 0.0;
	minRunEnergy = 10;
	maxForwardSpeed = 9;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;

	jumpForce = 14.0 * 90;

	maxDamage = 2.8;
	minImpactSpeed = 35;
	shapeFile = "light_male.dts";
	jetEmitter = BiodermArmorJetEmitter;
	jetEffect =  BiodermArmorJetEffect;

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = LightBiodermFootprint;
	decalOffset = 0.3;

	waterBreathSound = WaterBreathBiodermSound;

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function SummonerZombiemovetotarget(%zombie){
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

    if(%zombie.SumTicks $= "") {
       %zombie.SumTicks = 19; //summon right away Pl0x
    }
    %zombie.SumTicks++;
    if(%zombie.SumTicks > 20) {
       %zombie.SumTicks = 0;
       %Ct = GetRandom(1,3);
       %type = GetRandom(1, 8);
       if(%type > 5) {
          %type += 3;
          if(%type == 10) { //summoners don;t summon more summoners
             %type = 12;
          }
       }
       %SumPos = vectorAdd(VectorAdd(TWM2Lib_MainControl("getRandomPosition", 20 TAB 1), "0 0 7"), %zombie.getPosition());
       %c = CreateEmitter(%SumPos, NightmareGlobeEmitter, "0 0 1");
       %c.schedule(((%Ct * 1000) + 500), "delete");
       for(%i = 1; %i <= %ct; %i++) {
          %time = %i * 1000;
		  schedule(%time, 0, "TWM2Lib_Zombie_Core", "SpawnZombie", "zSpawnCommand", %type, %SumPos);
       }
    }

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%fmultiplier = $Zombie::ForwardSpeed;
	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1)
	   %fmultiplier = (%fmultiplier * 4);
	%vector = vectorscale(%vector, %Fmultiplier);
	%upvec = "150";
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
   %zombie.moveloop = schedule(500, %zombie, "SummonerZombiemovetotarget", %zombie);
}

