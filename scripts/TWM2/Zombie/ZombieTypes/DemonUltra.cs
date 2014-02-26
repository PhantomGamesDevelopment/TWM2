datablock PlayerData(DemonUltraZombieArmor) : LightMaleHumanArmor
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
   shapeFile = "heavy_male.dts";
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

function UDemonZombiemovetotarget(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
	if(%zombie.hastarget != 1 && %closestdistance >= 10 && %closestdistance <= 150){
	   DzombieFire(%zombie,%closestclient);
	   %zombie.canjump = 0;
	   schedule(1700, %zombie, "Zsetjump", %zombie);
	}
	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
       serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	if (%closestdistance >= 10 && %closestdistance <= 150 && %zombie.canjump == 1){
	   DzombieFire(%zombie,%closestclient);
	   %zombie.canjump = 0;
	   schedule(1700, %zombie, "Zsetjump", %zombie);
	}
	%vector = vectorscale(%vector, $Zombie::DForwardSpeed*2.4);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed / 3 * 2))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
    createBiodermProjection(%zombie);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "UDemonZombiemovetotarget", %zombie);
}
