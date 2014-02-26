datablock PlayerData(SSZombieArmor) : LightMaleBiodermArmor
{
   maxDamage = 1.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;

   damageScale[$DamageType::M1700] = 2.0;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock ShapeBaseImageData(SSZombImage2) {
   shapeFile = "turret_aa_large.dts";
   offset = "0.4 0.0 0.2";
   rotation = "0 0 0 1";
   emap = true;
};

datablock ShapeBaseImageData(SSZombImage3) {
   shapeFile = "turret_aa_large.dts";
   offset = "-0.8 0.0 0.2";
   rotation = "0 0 0 1";
   emap = true;
};

function SSZombiemovetotarget(%zombie){
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
       %finalVec = VectorScale(%finalVec, $Zombie::DForwardSpeed * 3.5);
       //Z is unimportant
       %zombie.applyImpulse(%pos, %finalVec);
    }
    else {
	   if(%zombie.hastarget != 1 && %closestdistance >= 50){
	      SlingshotFire(%zombie,%closestclient);
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
	      SlingshotFire(%zombie,%closestclient);
	      %zombie.canjump = 0;
	      schedule(4000, %zombie, "Zsetjump", %zombie);
	   }
       if(%closestdistance > 275) {    //only move toward my foe, if he is too
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
   %zombie.moveloop = schedule(500, %zombie, "SSZombiemovetotarget", %zombie);
}

function SlingshotFire(%zombie, %target) {
   if(isobject(%zombie) && isobject(%target)){ //Must be a living pilot
      if(%Zombie.firstFired == 1){
         %vec = vectorsub(vectorAdd(%target.getworldboxcenter(), "0 0 1"),%zombie.getMuzzlePoint(4));
         %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/1000));
         %zombie.firstFired = 2;
   	     %p = new TracerProjectile() {
   	   	    dataBlock        = SSZombieAcidBall;
   	   	    initialDirection = %vec;
   	   	    initialPosition  = %zombie.getMuzzlePoint(4);
   	   	    sourceObject     = %zombie;
   	   	    sourceSlot       = 6;
   	     };
         %zombie.Fire = schedule(250, %zombie, "SlingshotFire", %zombie, %target);
      }
      else if(%Zombie.firstFired == 2){
         %vec = vectorsub(vectorAdd(%target.getworldboxcenter(), "0 0 1"),%zombie.getMuzzlePoint(5));
	     %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/1000));
         %zombie.firstFired = 3;
   	     %p = new TracerProjectile() {
   	   	    dataBlock        = SSZombieAcidBall;
   	   	    initialDirection = %vec;
   	   	    initialPosition  = %zombie.getMuzzlePoint(5);
   	   	    sourceObject     = %zombie;
   	   	    sourceSlot       = 6;
   	     };
         %zombie.Fire = schedule(250, %zombie, "SlingshotFire", %zombie, %target);
      }
      else if(%Zombie.firstFired == 3){
         %vec = vectorsub(vectorAdd(%target.getworldboxcenter(), "0 0 1"),%zombie.getMuzzlePoint(5));
	     %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/1000));
         %zombie.firstFired = 0;
         %zombie.nomove = 0;
   	     %p = new TracerProjectile() {
   	   	    dataBlock        = SSZombieAcidBall;
   	   	    initialDirection = %vec;
   	   	    initialPosition  = %zombie.getMuzzlePoint(5);
   	   	    sourceObject     = %zombie;
   	   	    sourceSlot       = 6;
   	     };
      }
   	  else {
         %vec = vectorsub(vectorAdd(%target.getworldboxcenter(), "0 0 1"),%zombie.getMuzzlePoint(5));
	     %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/1000));
   	     %p = new TracerProjectile() {
   	   	    dataBlock        = SSZombieAcidBall;
   	   	    initialDirection = %vec;
   	   	    initialPosition  = %zombie.getMuzzlePoint(5);
   	   	    sourceObject     = %zombie;
   	   	    sourceSlot       = 5;
         };
	     %zombie.firstFired = 1;
   	     %zombie.Fire = schedule(250, %zombie, "SlingshotFire", %zombie, %target);
      }
   }
   else {
      %zombie.firstFired = 0;
	  %zombie.nomove = 0;
   }
}
