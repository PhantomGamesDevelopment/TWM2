//Phantom139 Note: The sniper zombie prefers to stay away from players, it does not infect on contact, but instead runs away
$TWM2::ArmorHasCollisionFunction[SniperZombieArmor] = false;
$TWM2::ArmorHasCollisionFunction[FlareguideSniperZombieArmor] = false;

datablock PlayerData(SniperZombieArmor) : LightMaleHumanArmor {
	boundingBox = "1.63 1.63 2.6";
	maxDamage = 2.5;
	minImpactSpeed = 35;
	shapeFile = "bioderm_heavy.dts";

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

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

datablock PlayerData(FlareguideSniperZombieArmor) : SniperZombieArmor {
	boundingBox = "1.63 1.63 2.6";
	maxDamage = 20.0;
	minImpactSpeed = 35;
	shapeFile = "bioderm_heavy.dts";

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = HeavyBiodermFootprint;
	decalOffset = 0.4;

	waterBreathSound = WaterBreathBiodermSound;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

datablock ShapeBaseImageData(ZSniperImage1) {
	shapeFile = "weapon_sniper.dts";
	emap = true;
	armThread = looksn;
};

datablock ShapeBaseImageData(ZSniperImage2) {
	shapeFile = "weapon_targeting.dts";
	offset = "0.0 1.0 0.41";
	rotation = "90 0 0 90";
	armThread = looksn;
	emap = true;
};

datablock ShapeBaseImageData(ZSniperImage3) {
	shapeFile = "weapon_elf.dts";
	offset = "0.0 0.3 0";
	rotation = "1 0 0 90";
	armThread = looksn;
	emap = true;
};

function SniperZombiemovetotarget(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){

    if(%closestDistance < 50) {    //runz0rs
       %TPos = %closestClient.getPosition();
       %tvel = %closestclient.getvelocity();
       %vec = vectorsub(%tpos,%pos);
       %dist = vectorlen(%vec);
       %velpredict = vectorscale(%tvel,(%dist / 50));
       %vector = vectoradd(%vec,%velpredict);
       %vector = vectornormalize(%vector);
       %x = getWord(%vector, 0);
       %y = getWord(%vector, 1);
       %finX = %x * -1;
       %finY = %y * -1;
       %finalVec = %finX SPC %finY SPC 0;
       %finalVec = VectorScale(%finalVec, $Zombie::DForwardSpeed * 3);
       //Z is unimportant
       %zombie.applyImpulse(%pos, %finalVec);
    }
    else {
	   if(%zombie.hastarget != 1 && %closestdistance >= 50){
	      SniperZombieFire(%zombie,%closestclient);
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

       if (%closestdistance >= 50 && %zombie.canjump == 1){
	      SniperZombieFire(%zombie,%closestclient);
	      %zombie.canjump = 0;
	      schedule(4000, %zombie, "Zsetjump", %zombie);
	   }
       if(%closestdistance > 175) {    //only move toward my foe, if he is too
	      %vector = vectorscale(%vector, $Zombie::DForwardSpeed); //far away
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
   }
   else if(%zombie.hastarget == 1){
      %zombie.hastarget = 0;
      %zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "SniperZombiemovetotarget", %zombie);
}

function SniperZombieFire(%zombie,%closestclient){
   %num = getRandom(250, 1000);
   %vec = vectorsub(VectorAdd(%closestclient.getPosition(), "0 0 2.2"),%zombie.getMuzzlePoint(4));
   %accuracy = (vectorlen(%vec) / %num);
   %vec = vectoradd(%vec, vectorscale(%closestclient.getvelocity(), %accuracy));
   %p = new TracerProjectile() { //TWM2 Sniper zombies use ALSWP Snipers :P
	dataBlock        = SniperZombieAcidShot;
	initialDirection = %vec;
	initialPosition  = %zombie.getMuzzlePoint(4);
	sourceObject     = %zombie;
	sourceSlot       = 4;
   };
   ServerPlay3D(ALSWPFireSound, %zombie.getPosition());
}


function FlareguideSniperZombieAcidShot::onExplode(%data, %proj, %pos, %mod) {
	Parent::OnExplode(%data, %proj, %pos, %mod);
	//Create the mini-pulses
	for (%i = 0; %i < 6; %i++) {
		%x = getRandom(-3, 3);
		%y = getRandom(-3, 3);
		%z = 5;
		%vec = %x SPC %y SPC %z;
		%vec = VectorScale(%vec, 200);
		%p = new (GrenadeProjectile)() {
			dataBlock = FlareguideSniperBurstRound;
			initialDirection = %vec;
			initialPosition = %pos;
		};
		MissionCleanup.add(%p);
		%p.sourceObject = %proj.sourceObject;
		return;
	}	
}