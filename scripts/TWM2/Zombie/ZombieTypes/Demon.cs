datablock PlayerData(DemonZombieArmor) : LightMaleHumanArmor
{
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

function DZombiemovetotarget(%zombie){
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
	   schedule(4000, %zombie, "Zsetjump", %zombie);
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
	   schedule(4000, %zombie, "Zsetjump", %zombie);
	}
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
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "DZombiemovetotarget", %zombie);
}

function DZombieFire(%zombie,%closestclient){
   %pos = %zombie.getMuzzlePoint(4);
   %tpos = %closestclient.getWorldBoxCenter();
   %tvel = %closestclient.getvelocity();
   %vec = vectorsub(%tpos,%pos);
   %dist = vectorlen(%vec);
   %velpredict = vectorscale(%tvel,(%dist / 50));
   %vector = vectoradd(%vec,%velpredict);
   %ndist = vectorlen(%vector);
   %upvec = "0 0 "@((%ndist / 50) * (%ndist / 50) * 2);
   %vector = vectornormalize(vectoradd(%vector,%upvec));
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
}
