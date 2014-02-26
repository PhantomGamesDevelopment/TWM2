datablock AudioProfile(ZLordFootSound)
{
   filename    = "fx/weapons/grenade_explode_UW.wav";
   description = AudioBomb3d;
   preload = true;
};

datablock AudioProfile(HZLordFootSound)
{
   filename    = "fx/weapons/SpinFusor_explode_UW.wav";
   description = AudioBomb3d;
   preload = true;
};

datablock PlayerData(LordZombieArmor) : HeavyMaleBiodermArmor
{
   shapefile = "TR2medium_male.dts";
   mass = 500;
   maxDamage = 18.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;
   boundingBox = "2.9 2.9 4.8";

   underwaterJetForce = 10;

   LFootSoftSound       = ZLordFootSound;
   RFootSoftSound       = ZLordFootSound;
   LFootHardSound       = HZLordFootSound;
   RFootHardSound       = HZLordFootSound;
   LFootMetalSound      = ZLordFootSound;
   RFootMetalSound      = ZLordFootSound;
   LFootSnowSound       = ZLordFootSound;
   RFootSnowSound       = ZLordFootSound;

   damageScale[$DamageType::M1700] = 1.5;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock ShapeBaseImageData(ZHead)
{
   shapeFile = "bioderm_heavy.dts";
   emap = false;
   mountPoint = 1;
   offset = "0 0.25 -1.5";
   rotation = "1 0 0 15";
};

datablock ShapeBaseImageData(ZBack)
{
   shapeFile = "bioderm_medium.dts";
   emap = false;
   mountPoint = 1;
   offset = "0 0.25 -1.25";
   rotation = "-1 0 0 10";
};

datablock ShapeBaseImageData(ZDummyslotImg)
{
   shapeFile = "turret_muzzlepoint.dts";
   emap = false;
};

datablock ShapeBaseImageData(ZDummyslotImg2)
{
   shapeFile = "turret_muzzlepoint.dts";
   emap = false;
   offset = "-1.5 0 0";
};

function LZombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead") {
      %zombie.throwweapon(1);
	  return;
   }
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist && %zombie.canmove != 0){
	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
       serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);
	if(%zombie.ATCount >= 20){
	   %zombie.ATCount = 0;
	   %zombie.nomove = 1;
   	   %zombie.Fire = schedule(1250, %zombie, "ZFire", %zombie, %closestClient);
       if(Game.CheckModifier("OhLordy") == 1) {
          %zombie.Fire = schedule(1750, %zombie, "ZFire", %zombie, %closestClient);
       }
	   %zombie.Charge = schedule(500, %zombie, "ChargeEmitter", %zombie);
	   %zombie.chargecount = 0;
	}
	if(%zombie.nomove != 1) {
	%fmultiplier = $Zombie::LForwardSpeed;
	%upvec = "150";
	if(%closestDistance <= $zombie::LKillDist && %zombie.canjump == 1){
	   %vec = vectoradd(%pos,vectorScale(%vector,($zombie::LkillDist - 1.6)));
	   %mask = $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::PlayerObjectType;
	   %searchResult = containerRayCast(vectoradd(%pos,vectorScale(%vector,1.6)), %vec, %mask, %zombie);
	   if(%searchResult){
		%searchObj = getWord(%searchResult,0);
		if(%searchObj $= %closestClient){
		   %chance = getrandom(1,5);
		   if(%chance == 1){
			%dir = %zombie.getEyeVector();
			%dir = vectornormalize(getword(%dir,0)@" "@getword(%dir,1)@" 0");
			%dir = vectoradd(vectorscale(%dir,7500),"0 0 1000");
			%closestclient.applyimpulse(%clpos,%dir);
			%closestclient.damage(0, %clpos, 0.8, $DamageType::ZombieL);
		   }
		   else{
	   		%zombie.setvelocity("0 0 0");
	   		%zombie.canjump = 0;
	   		schedule(1000, %zombie, "Zsetjump", %zombie);
	   		Llifttarget(%zombie,%closestclient,0);
		   }
		}
	   }
	}
	else if(%closestDistance <= ($zombie::LKillDist + $zombie::lungDist)){
	   %zombie.setvelocity("0 0 0");
	   %fmultiplier = (%fmultiplier * 2.5);
	}
	%vector = vectorscale(%vector, %Fmultiplier);
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= 4000)
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
	%zombie.ATCount++;
	}
   }
   else if(%zombie.hastarget == 1 && %zombie.canmove != 0){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "LZombiemovetotarget", %zombie);
}

function ZFire(%zombie, %target){
   if(isobject(%zombie) && isobject(%target)){
   	if(%Zombie.firstFired == 1){
         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(6));
	   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));
	   %zombie.firstFired = 0;
   	   %zombie.nomove = 0;
   	   %p = new TracerProjectile()
   	   {
   	   	dataBlock        = LZombieAcidBall;
   	   	initialDirection = %vec;
   	   	initialPosition  = %zombie.getMuzzlePoint(6);
   	   	sourceObject     = %zombie;
   	   	sourceSlot       = 6;
   	   };
   	}
   	else{
         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(5));
	   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));
   	   %p = new TracerProjectile()
   	   {
   	   	dataBlock        = LZombieAcidBall;
   	   	initialDirection = %vec;
   	   	initialPosition  = %zombie.getMuzzlePoint(5);
   	   	sourceObject     = %zombie;
   	   	sourceSlot       = 5;
   	   };
	   %zombie.firstFired = 1;
   	   %zombie.Fire = schedule(250, %zombie, "ZFire", %zombie, %target);
      }
   }
   else{
	%zombie.firstFired = 0;
	%zombie.nomove = 0;
   }
}

function Llifttarget(%zombie,%closestclient,%count){
   if(%count == 0)
	%zombie.canmove = 0;
   if(%closestclient.getState() $= "dead" || %Zombie.getState() $= "dead"){
	%zombie.canmove = 1;
	return;
   }
   %target = %closestclient;
   if(!isobject(%target)){
	%zombie.canmove = 1;
	return;
   }
   %pos = %zombie.getworldboxcenter();
   %Tpos = %target.getworldboxcenter();
   %ZtoT = vectorsub(%tpos,%pos);
   if (%count <= 12){
	%newpos = vectoradd(%ZtoT,vectoradd(%pos,"0 0 -0.6"));
	%target.setTransform(%newpos);
	%target.setvelocity("0 0 0");
   }
   else {
	%killtype = getrandom(1,2);
	if(%killtype == 1){
	   %closestwall = 20;
	   %nv2 = (getword(%ZtoT, 0) * -1);
	   %nv1 = getword(%ZtoT, 1);
	   %vector1 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"),20);
	   %nvector1 = vectoradd(%tpos,%vector1);
	   %nv2 = getword(%ZtoT, 0);
	   %nv1 = (getword(%ZtoT, 1) * -1);
	   %vector2 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"),20);
	   %nvector2 = vectoradd(%tpos,%vector2);
	   %searchresultR = containerRayCast(%tpos, %nvector1, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
	   %searchresultL = containerRayCast(%tpos, %nvector2, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
	   if(%searchresultR){
		%Hpos = getword(%searchresultR,1)@" "@getword(%searchresultR,2)@" "@getword(%searchresultR,3);
		%distance = vectordist(%Tpos, %Hpos);
		if(%distance <= %closestwall){
		   %closestwall = %distance;
		   %vector = vectoradd(vectorscale(%vector1,1500),"0 0 100");
		}
	   }
	   if(%searchresultL){
		%Hpos = getword(%searchresultL,1)@" "@getword(%searchresultL,2)@" "@getword(%searchresultL,3);
		%distance = vectordist(%Tpos, %Hpos);
		if(%distance <= %closestwall){
		   %closestwall = %distance;
		   %vector = vectoradd(vectorscale(%vector2,1500),"0 0 100");
		}
	   }
	   if(%closestwall == 20){
	   	%x = getword(%ZtoT, 0);
	   	%y = getword(%ZtoT, 1);
		%outvec = vectorscale(vectornormalize(%x@" "@%y@" 0"),50);
		%vector = vectoradd("0 0 -15000",%outvec);
	   }
	   %target.applyimpulse(%target.getposition(),%vector);
	}
	else if(%killtype == 2){
	   %target.infected = 1;
	   %target.damage(0, %target.getposition(), 10.0, $DamageType::ZombieL);
	}
	%count = 0;
	%zombie.canmove = 1;
	return;
   }
   %count++;
   %zombie.killingplayer = schedule(150, %zombie, "Llifttarget", %zombie, %closestclient, %count);
}
