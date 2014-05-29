function UltrDroneBattle(%pos, %radius, %number, %teamlow, %teamhigh, %maxskill, %slth){
   for(%i = 0; %i < %number; %i++){
	%startpos = vectorAdd(%pos,(getRandom(0, %radius) - (%radius / 2))@" "@(getRandom(0, %radius) - (%radius / 2))@" 0");
	%rotation = "0 0 1 "@getRandom(1,360);
	if(%teamlow != %teamhigh)
	   %team = getRandom(%teamlow,%teamhigh);
	else
	   %team = %teamlow;
	StartUltrDrone(%startpos,%rotation,%team,getRandom(1,%maxskill), %slth);
   }
}

//This sets up the drone and the functions needed to start the drone.
function StartUltrDrone(%pos, %rotation, %team, %skill, %slth){
   if(%team $= "")
	%team = 0;
   if(%pos $= "")
	%pos = "0 0 300";
   if(%rotation $= "")
	%rotation = "1 0 0 0";
   if(%skill !$= "ace"){
      if(%skill $= "" || %skill < 1)
	   %skill = 10;
      else if(%skill > 10)
	   %skill = 10;
   }
   %Drone = new FlyingVehicle()
   {
      dataBlock    = StormSeigeDrone;
      position     = %pos;
      rotation     = %rotation;
      team         = %team;
   };
   MissionCleanUp.add(%Drone);

   setTargetSensorGroup(%Drone.getTarget(), %team);

   %Drone.isdrone = 1;
   %drone.dodgeGround = 0;

   if(%slth) {
   %Drone.setCloaked(true);
   }

   if(%skill $= "ace"){
	%skill = 10;
	%drone.isace = 1;
   }

   %drone.skill = 0.2 + (%skill / 12.5);

   schedule(100, 0, "DroneForwardImpulse", %drone);
   schedule(101, 0, "DronefindTarget", %drone);
   schedule(102, 0, "DroneScanGround", %drone);

   return %drone;
}

function StartStormrider(%position) {
   %pos = vectoradd(%position, "0 0 500");
   %pos2 = vectoradd(%position, "15 0 500");
   %pos3 = vectoradd(%position, "-15 0 500");
   %drone = UltrDroneBattle(%pos, 500, 1, 6, 6, "ace", 0); //yes this bad guy is stealthed
   %d2 = DroneBattle(%pos2, 500, 1, 6, 6, 100, 0); //his Pal
   %d3 = DroneBattle(%pos3, 500, 1, 6, 6, 100, 0); //his Other Pal
   %drone.isUltr = 1;
   %drone.isBoss = 1;
   %d2.isUltrally = 1;
   %d3.isUltrally = 1;
   UltraBossAbilities(%drone);
   
   InitiateBoss(%drone, "Stormrider");
}

function UltraBossAbilities(%drone) {
   if(!isObject(%drone)) {
   return;
   }
   %drone.setCloaked(false); //disable cloak?
   %rand = getRandom(1,13);
//   %target = DroneFindNearestPilot(500,%drone); //this line for targeted abilities
   switch(%rand) {
      case 1:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
         %SPos1 = vectorAdd(%drone.getPosition(),"3 0 0");
         %SPos2 = vectorAdd(%drone.getPosition(),"-3 0 0");
   	      %p1 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 10";
           initialPosition  = %SPos1;
   	   	   sourceObject     = %drone;
   	   	   sourceSlot       = 6;
   	      };
   	      %p2 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = LordRogStiloutte;
           initialDirection = "0 0 10";
           initialPosition  = %SPos2;
   	   	   sourceObject     = %drone;
   	   	   sourceSlot       = 6;
   	      };
          MissionCleanup.add(%p1);
          MissionCleanup.add(%p2);
          schedule(1000,0,"MissileDrop",%p1, %target.player);
          schedule(1000,0,"MissileDrop",%p2, %target.player);
          MessageAll('MessageAll', "\c4Stormrider: Fire!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Heh, no targets for me!");
          }
      case 2:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
         %SPos = vectorAdd(%drone.getPosition(),"0 0 3");
   	      %p1 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = ShoulderMissile;
           initialDirection = "5 0 0";
           initialPosition  = %SPos;
   	   	   sourceObject     = %drone;
   	   	   sourceSlot       = 6;
   	      };
   	      %p2 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = ShoulderMissile;
           initialDirection = "-5 0 0";
           initialPosition  = %SPos;
   	   	   sourceObject     = %drone;
   	   	   sourceSlot       = 6;
   	      };
   	      %p3 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = ShoulderMissile;
           initialDirection = "0 5 0";
           initialPosition  = %SPos;
   	   	   sourceObject     = %drone;
   	   	   sourceSlot       = 6;
   	      };
   	      %p4 = new SeekerProjectile()
   	      {
   	   	   dataBlock        = ShoulderMissile;
           initialDirection = "0 -5 0";
           initialPosition  = %SPos;
   	   	   sourceObject     = %drone;
   	   	   sourceSlot       = 6;
   	      };
          MissionCleanup.add(%p1);
          MissionCleanup.add(%p2);
          MissionCleanup.add(%p3);
          MissionCleanup.add(%p4);
          schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %p1);
          schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %p2);
          schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %p3);
          schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %p4);

          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Have fun with these "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Bah, no targets, no fun.");
          }
      case 3:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          HeatLoop(%target.player, 0);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Lets see what happens when missiles are completely precice on you, "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": I guess it's time to start scanning.");
          }
      case 4:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          FireanotherSidewinder(%drone, %target.player, 3);
          schedule(700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(1400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(2100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(2800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(3500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(4200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(4900,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          //Rapid Shot Missiles
          schedule(5000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(5200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(5400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(5600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(5800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(6000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(6200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(6400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(6500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Taste my fury "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Aww, My missiles were ready.");
          }
      case 5:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          FireanotherSidewinder(%drone, %target.player, 3);
          schedule(700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(1400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(2100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(2800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(3500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(4200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(4900,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(5600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(6300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          schedule(7000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
          // Quick Shots
          schedule(8000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(8100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(8200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          schedule(8300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": I have missiles with your name on them "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Aww, My missile strike was ready.");
          }
      case 6:
          schedule(700,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(1400,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(2100,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(2800,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(3500,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(4200,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(4900,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(5600,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(6300,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(7000,0,"WindshearAttack_FUNC", "Flare", %drone);
          // Quick Shots
          schedule(8000,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(8100,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(8200,0,"WindshearAttack_FUNC", "Flare", %drone);
          schedule(8300,0,"WindshearAttack_FUNC", "Flare", %drone);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Hahaha, Your Missiles are worthless Now!");
      case 7:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          FireSniperShots(%drone, %target.player, 3);
          for(%i = 0; %i < 700; %i++) {
             %time = %i * 10;
             schedule(%time, 0,"FireSniperShots",%drone, %target.player);
          }
          // Quick Shots
          schedule(8000,0,"FireSniperShots",%drone, %target.player);
          schedule(8100,0,"FireSniperShots",%drone, %target.player);
          schedule(8200,0,"FireSniperShots",%drone, %target.player);
          schedule(8300,0,"FireSniperShots",%drone, %target.player);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Time to Use My CG, "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Heh, you fewls cant withstand this.");
          }
      case 8:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          FireSeekerPhotons(%drone,%target.player);
          schedule(1500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(3000,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(4500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(6000,0,"FireSeekerPhotons",%drone,%target.player);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Here, "@getTaggedString(%target.name)@", Catch!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Close up the Seekers. No Targets To hit.");
          }
      case 9:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          FireSeekerPhotons(%drone,%target.player);
          schedule(700,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(1400,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(2100,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(2800,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(3500,0,"FireSeekerPhotons",%drone,%target.player);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Try these out for size, "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Heh, No enemies in the area.");
          }
      case 10:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
          FireSeekerPhotons(%drone,%target.player);
          schedule(500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(1000,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(1500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(2000,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(2500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(3000,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(3500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(4000,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(4500,0,"FireSeekerPhotons",%drone,%target.player);
          schedule(5000,0,"FireSeekerPhotons",%drone,%target.player);
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": I have some fun plasma missiles for you, "@getTaggedString(%target.name)@"!");
          }
          else {
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Meh, No targets for my plasma seekers.");
          }
      case 11:
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Engage Stealth!");
          %drone.setCloaked(true);
      case 12:
          MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": My Buddies will handle You!");
         %SPos1 = vectorAdd(%drone.getPosition(),"15 0 0");
         %SPos2 = vectorAdd(%drone.getPosition(),"-15 0 0");
         %SPos3 = vectorAdd(%drone.getPosition(),"0 15 0");
         %SPos4 = vectorAdd(%drone.getPosition(),"0 -15 0");
         %d2 = DroneBattle(%SPos1, 500, 1, 6, 6, 100, 0); //his Pal
         %d3 = DroneBattle(%SPos2, 500, 1, 6, 6, 100, 0); //his Other Pal
         %d4 = DroneBattle(%SPos3, 500, 1, 6, 6, 100, 0); //his Pal's Pal
         %d5 = DroneBattle(%SPos4, 500, 1, 6, 6, 100, 0); //his Pal's Pal's Buddy
         %d2.isUltrally = 1;
         %d3.isUltrally = 1;
         %d4.isUltrally = 1;
         %d5.isUltrally = 1;
      default:
         %SPos1 = vectorAdd(%drone.getPosition(),"15 0 0");
         %SPos2 = vectorAdd(%drone.getPosition(),"15 0 0");
         %d2 = DroneBattle(%SPos1, 500, 1, 6, 6, 100, 0); //his Pal
         %d3 = DroneBattle(%SPos2, 500, 1, 6, 6, 100, 0); //his Other Pal
         %d2.isUltrally = 1;
         %d3.isUltrally = 1;
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Get Moving, targets to be hunted!");
   }
   schedule(30000,0,"UltraBossAbilities",%drone);
}

function FireSniperShots(%drone,%target){
if(!isObject(%drone) || !isObject(%target) || %target.getState() $= "dead") {
return;
}

   %vec = vectorsub(%target.getworldboxcenter(),%drone.getMuzzlePoint(6));
   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));

   %x = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %nvec = MatrixMulVector(%mat, %vec);

%p = new TracerProjectile()
{
dataBlock        = M1Bullet;
initialDirection = %nvec;
initialPosition  = %drone.getMuzzlePoint(6);
sourceObject     = %drone;
sourceSlot       = 6;
};
}

function FireSeekerPhotons(%drone,%target){
if(!isObject(%drone) || !isObject(%target) || %target.getState() $= "dead") {
return;
}

%proj = new (linearflareprojectile)() {
dataBlock        = PhotonMissileProj;
initialDirection = "0 0 3";
initialPosition  = vectorAdd(%drone.getPosition(), "0 0 5");
sourceObject     = %drone;
sourceSlot = 0;
};

MissionCleanup.add(%proj);
%proj.PhotonMuzVec = %drone.getMuzzleVector(5);
schedule( 100,0, "seekingprojs" , %drone, %Proj, %target);
schedule( 100,0, "PhotonShockwaveFunc" ,%drone,%Proj);
}

//Seeker Plasma Stuff
//Credit To Abrikcham
//-abirikcham@yahoo.com
datablock ShockwaveData(FakePhotonMissileShockwave)//this is the shockwave trail effect
{
width = 0.5;
numSegments = 30;
numVertSegments = 2;
velocity = 8;
verticalcurve = 0;
acceleration = -17.0;
lifetimeMS = 600;
height = 0.00001;
is2D = false;
texture[0] = "special/shockwave4";
texture[1] = "special/gradient";
texWrap = 10.0;
mapToTerrain = false;
orientToNormal = true;
renderBottom = true;
times[0] = 0.0;
times[1] = 0.5;
times[2] = 1.0;
colors[0] = "0 1 0 1";
colors[1] = "0.0 1.1 0.0 0.60";//0.4 0.1 1.0
colors[2] = "0.0 1.1 0.0 0.0";
};

datablock AudioProfile(FakePhotonMissileShockwaveSound)//sound thing for shockwave
{
	filename = "fx/misc/gridjump.wav";
	description = AudioExplosion3d;
	preload = true;
};

datablock ExplosionData(FakePhotonShockwaveExp)//dont touch thx
{
	soundProfile   = FakePhotonMissileShockwaveSound;
	faceViewer           = false;
	shockwave = FakePhotonMissileShockwave;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 3.0;
};

datablock LinearProjectileData(FakePhotonShockwaveProj)//dont touch thx
{
	projectileShapeName = "turret_muzzlepoint.dts";
	scale               = "0.1 0.1 0.1";
	faceViewer          = true;
	directDamage        = 0;
	hasDamageRadius     = false;
	indirectDamage      = 0.1;
	damageRadius        = 10;
	kickBackStrength    = 1;
	radiusDamageType    = $DamageType::Photon;
	explosion           = "FakePhotonShockwaveExp";
	dryVelocity       = 0.0001;
	wetVelocity       = 0.00001;
	velInheritFactor  = 0.0;
	lifetimeMS        = 0.00000001;
	explodeOnDeath    = true;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
};

datablock ParticleData(PhotonMissileExpPart)//explosion particle stuff ... mitzi
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 750;
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "0 1 0 1.0";
   colors[1]     = "0 1 0 0.0";
   sizes[0]      = 3;
   sizes[1]      = 2;
};

datablock ParticleEmitterData(PhotonMissileExpEmit)//explosion particle stuff ... mitzi
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PhotonMissileExpPart";
};

datablock ExplosionData(PhotonMissileExplosion)//main proj exp
{
   explosionShape = "disc_explosion.dts";
   soundProfile   = UnderwaterGrenadeExplosionSound;//plasmaexpsound orig

   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "25.0 25.0 25.0";
   camShakeDuration = 1.3;
   camShakeRadius = 25.0;

   particleEmitter = PhotonMissileExpEmit;
   particleDensity = 150;
   particleRadius = 3.5;
   faceViewer = true;
};

//**********************************
//*          Projectiles           *
//**********************************
datablock LinearFlareProjectileData(PhotonMissileProj)
{
   scale               = "4 4 4";//6
   sound      = PlasmaProjectileSound;

   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 10.0;
   kickBackStrength    = 4000;
   radiusDamageType    = $DamageType::Photon; //obviously change this

   explosion           = "PhotonMissileExplosion";
   underwaterExplosion = "PhotonMissileExplosion";
   splash              = BlasterSplash;

   dryVelocity       = 200.0;
   wetVelocity       = 200.0;
   velInheritFactor  = 0.6;
   fizzleTimeMS      = 8000;
   lifetimeMS        = 8000;
   explodeOnDeath    = true;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 15000;

   activateDelayMS = 0;
   numFlares         = 35;
   flareColor        = "0.0 1.1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   size[0]           = 1;
   size[1]           = 10;
   size[2]           = 2;


   hasLight    = true;
   lightRadius = 1.0;
   lightColor  = "0.6 1.1 0";
};

function PhotonMissileProj::onExplode(%data, %proj, %pos, %mod)// ok this is where everything is canceled... the scheds.. this is the buggy part
{
parent::onexplode(%data, %proj, %pos, %mod);
cancel(%proj.seeksched);
cancel(%proj.PhotonShockwaveSched);
cancel(%proj.seekschedcheck);
}

function seekingprojcheck(%obj,%proj, %type) {
	%searchmask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType;

    if(%type $= "") {
	InitContainerRadiusSearch(%proj.position, 100, %searchmask);
    }
    else {
	InitContainerRadiusSearch(%proj.position, 9999, %searchmask);   //For Stormrider
    }
	while ((%target = containerSearchNext()))
	{
		if(%target != %obj)
		{
			if(%target.team == %obj.team && !$TWM::TeamWars)
			{
            %proj.seekschedcheck = schedule( 50,0, "seekingprojcheck" ,%obj,%Proj,%type);
            return;
            }
			%reptarg = %target;
			seekingprojs(%obj,%proj,%reptarg);
			return;
		}
	}
%proj.seekschedcheck = schedule( 50,0, "seekingprojcheck" ,%obj,%Proj,%type);
}


function seekingprojs(%obj,%Proj,%reptarg)
{
%projpos = %proj.position;

%projdir = %proj.initialdirection;

%type = %reptarg.getClassName();

if(!isobject(%proj))
{
return;
}

	if(isobject(%proj))
	{
	%proj.delete();
	}

		if(!isobject(%obj))
		{
		return;
		}

			if(!isobject(%reptarg))
			{
			return;
			}

             if(%type $= "Player") {
				if(%reptarg.getState() $= "Dead")
				{
				return;
				}
             }

//( ... )the projs now have a max turn angle like real missile ub3r l33t;;;; nm wtf afasdf
%test = getWord(%projdir, 0);
%test2 = getWord(%projdir, 1);
%test3 = getWord(%projdir, 2);

%projdir = vectornormalize(vectorsub(%reptarg.position,%projpos));
%testa = getWord(%projdir, 0);
%testa2 = getWord(%projdir, 1);
%testa3 = getWord(%projdir, 2);

// now it's time for my mad math skills..... i used microsoft calculator to figure this one out =0... was a brainbuster for me to think of how this would work
%testthing = %test - %testa; //oh u can rename all this test stuff but make sure u get it right =/ dont break plz
%testfin = %testthing / 8;  //!!!!!!!!!! OK HERE!!!! is where the max angle thing is... increase for lower turn angle and reduce for a higher turn angle
%testfinal = %testfin * -1;   //^^^^^ *side note for the one above this* dont div by zero unless yer dumb (...) div by i think 1 if you want it to seek with a 360 max turn angle angle... kinda gay though if u do that
%testfinale = %testfinal + %test;

%testthing2 = %test2 - %testa2;
%testfin2 = %testthing2 / 10; //change here too .. this is for the y axis  btw it's best if u leave my setting of 10 on ... it's the most balanced well nm change it to what u want but you really should leave it around this number like 9ish
%testfinal2 = %testfin2 * -1;
%testfinale2 = %testfinal2 + %test2;

%testthing3 = %test3 - %testa3;
%testfin3 = %testthing3 / 10; //z- axis this one is for i think.. mmm idea... you try playing with dif max angles for xyz for maybe like a sidewinder effect =?
%testfinal3 = %testfin3 * -1;
%testfinale3 = %testfinal3 + %test3;

%haxordir = %testfinale SPC %testfinale2 SPC %testfinale3; //final dir.. .....

%proj = new (linearflareprojectile)() {
dataBlock        = PhotonMissileProj;
initialDirection = %haxordir;
initialPosition  = %projpos;
sourceslot = %obj;
};
%proj.sourceobject = %obj;
MissionCleanup.add(%proj);

%fake = new (linearprojectile)() {
dataBlock        = "FakePhotonShockwaveProj";
initialDirection = %haxordir;
initialPosition  = %projpos;
sourceSlot       = %obj;
};
%fake.sourceobject = %obj;
MissionCleanup.add(%fake);

%searchmask = $TypeMasks::ProjectileObjectType;

InitContainerRadiusSearch(%projpos, 12, %searchmask);
while ((%target = containerSearchNext()))
{
	if(%target.getdatablock().getname() $= "FlareGrenadeProj") //btw u can add other projs that will cancel out seeking linear flare
	{
	%target.delete();
	return;
	}
}

%proj.seeksched = schedule( 80,0, "seekingprojs" ,%obj,%Proj,%reptarg);
}

