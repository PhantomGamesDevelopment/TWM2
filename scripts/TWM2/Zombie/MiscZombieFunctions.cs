$zombie::detectDist = 9999;
$zombie::FallDieHeight = -500;
//$Zombie::TurningSpeed = 100;
$Zombie::ForwardSpeed = 750;
$Zombie::FForwardSpeed = 1500;
$Zombie::LForwardSpeed = 4000;
$Zombie::DForwardSpeed = 1200;
$Zombie::RForwardSpeed = 1500;

//************************************************************
//*******************MISC Zomb Functions**********************
//************************************************************

function ZSetRandomMove(%zombie){
   if (!isobject(%zombie))
	return;
   %RX = getrandom(-10, 10);
   %RY = getrandom(-10, 10);
   %RZ = "0";
   %vector = %RX@" "@%RY@" "@%RZ;
   %zombie.direction = vectornormalize(%vector);
   %zombie.Mnum = getrandom(1, 20);
   %zombie.zombieRmove = schedule(500, %zombie, "ZrandommoveLoop", %zombie);
}

function ZrandommoveLoop(%zombie){
   if (!isobject(%zombie))
	return;
   if (%Zombie.getState() $= "dead")
	return;
   if (%zombie.hastarget == 1){
	%zombie.direction = "";
	return;
   }
   if (%zombie.Mnum >= 1){
	%X = getword(%zombie.direction, 1);
	%Y = (getword(%zombie.direction, 0) * -1);
	%none = 0;
	%vector = %X@" "@%Y@" "@%none;
	%zombie.setRotation(fullrot("0 0 0",%vector));
	if(%zombie.type == 1)
	   %speed = ($zombie::forwardspeed);
	else if(%zombie.type == 2)
	   %speed = ($zombie::Fforwardspeed * 0.6);
	else if(%zombie.type == 4)
	   %speed = ($zombie::Dforwardspeed * 0.75);
	else if(%zombie.type == 3)
	   %speed = ($zombie::Lforwardspeed * 0.8);
	%vector = vectorscale(%zombie.direction, %speed);
	%zombie.applyimpulse(%zombie.getposition(), %vector);
	%zombie.Mnum = (%zombie.Mnum - 1);
	%zombie.zombieRmove = schedule(500, %zombie, "ZrandommoveLoop", %zombie);
   }
   else if(%zombie.Mnum == 0)
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
}

function InfectLoop(%obj){
   if($TWM::PlayingHellJump) {
      return;
   }
   if (%obj.getState() $= "dead") {
	  return;
   }
   if(!%obj.infected || %obj.isboss) {
      return;
   }
   if(%obj.client !$= "") {
      if(%obj.client.isActivePerk("No-Infect Armor")) {
         %obj.playShieldEffect("1 1 1");
         %obj.infected = 0;
         return;
      }
   }
   if(isObject(%obj)){
	if(%obj.beats $= "")
	   zombieAttackImpulse(%obj,0);
	if(%obj.beats < 15)
         %obj.setWhiteOut(%obj.beats * 0.05);
	else
	   %obj.setDamageFlash(1);
	if(%obj.beats == 15){
	   %obj.canZkill = 1;
	}
	if(%obj.beats >=15)
	   serverPlay3d("ZombieMoan",%obj.getWorldBoxCenter());
	else if (%obj.beats >= 10)
	   playDeathCry(%obj);
	else
	   playPain(%obj);
	if(%obj.beats == 20){
	   if($host::canZombie $= "")
		$host::canZombie = 0;
	   if($host::canZombie == 1)
	      makePersonZombie(%obj.getTransform(),%obj.client);
	   else
		%obj.damage(0, %obj.getposition(), 10.0, $DamageType::Zombie);
	   return;
	}
      %obj.beats++;
	%obj.infectedDamage = schedule(2000, %obj, "InfectLoop", %obj);
   }
}

function Zsetjump(%zombie){
   %zombie.canjump = 1;
}

//************************************************************
//*****************Zomb Attack Stuff**************************
//************************************************************

function ChargeEmitter(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.chargecount >= 2){
   	%charge2 = new ParticleEmissionDummy()
   	{
   	   position = %zombie.getMuzzlePoint(6);
   	   dataBlock = "defaultEmissionDummy";
   	   emitter = "burnEmitter";
      };
	MissionCleanup.add(%charge2);
	%charge2.schedule(100, "delete");
   }
   if(%zombie.chargecount <= 7){
   	%charge = new ParticleEmissionDummy()
   	{
   	   position = %zombie.getMuzzlePoint(5);
   	   dataBlock = "defaultEmissionDummy";
   	   emitter = "burnEmitter";
      };
	MissionCleanup.add(%charge);
	%charge.schedule(100, "delete");
   }
   if(%zombie.chargecount <= 9){
      %zombie.Fire = schedule(100, %zombie, "ChargeEmitter", %zombie);
	%zombie.chargecount++;
   }
   else
	%zombie.chargecount = 0;
}


//************************************************************
//*******************Zomb AI Stuff****************************
//************************************************************

function ZombieLookforTarget(%zombie){
   %wbpos = %zombie.getworldboxcenter();
   %z = getWord(%wbpos, 2);
   if(%z < $zombie::FallDieHeight) {
      %zombie.scriptKill(0);
   }
   %count = ClientGroup.getCount();
   %closestClient = -1;
   %closestDistance = 32767;
   for(%i = 0; %i < %count; %i++)
   {
	%cl = ClientGroup.getObject(%i);
	if(isObject(%cl.player)){
	   %testPos = %cl.player.getWorldBoxCenter();
	   %distance = vectorDist(%wbpos, %testPos);
	   if (%distance > 0 && %distance < %closestDistance && canAttackPlayer(%cl))
	   {
	   	%closestClient = %cl;
	   	%closestDistance = %distance;
	   }
	}
   }
   return %closestClient SPC %closestDistance;
}

//conditionals, verifies that the zombies can attack this specific player
function canAttackPlayer(%client) {
   if(!%client.player.isFTD && !%client.player.iszombie && !%client.player.stealthed && !%client.player.isGoingToDie) {
      return true;
   }
   else {
      return false;
   }
}

function ZgetFacingDirection(%zombie,%closestClient,%pos){
   if(isObject(%closestClient)) {
      %clpos = %closestClient.getPosition();
   }
   else {
      %clpos = TWM2Lib_MainControl("RMPG");
   }
   %vector = vectorNormalize(vectorSub(%clpos, %pos));
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %nv1 = %v2;
   %nv2 = (%v1 * -1);
   %none = 0;
   %vector2 = %nv1@" "@%nv2@" "@%none;
   %zombie.setRotation(fullrot("0 0 0",%vector2));

   return %vector;
}

//-----------------------------------------------------------
function getRandomZombieType(%list) {
   %count = getWordCount(%list);
   %choice = getRandom(0, %count);
   
   %output = getWord(%list, %choice);
   if(%output $= "") {
      return 1;
   }
   else {
      return %output;
   }
}


//DEATH
datablock AudioProfile(ZombieDeathSound1)
{
   filename    = "voice/Derm3/avo.deathcry_01.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ZombieDeathSound2)
{
   filename    = "voice/Derm2/avo.deathcry_01.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ZombieDeathSound3)
{
   filename    = "voice/Derm1/avo.deathcry_01.wav";
   description = AudioClose3d;
   preload = true;
};
