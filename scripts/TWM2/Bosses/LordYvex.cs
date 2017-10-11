//LordYvex.cs
//Phantom139
//TWM2

//Contains all of the datablocks and functioning for the Lord Yvex boss

$Boss::Proficiency["Yvex", 0] = "Team Bronze\t1000\tDefeat Lord Yvex with your team dying no more than 25 times";
$Boss::ProficiencyCode["Yvex", 0] = "$TWM2::BossManager.bossKills < 25";
$Boss::Proficiency["Yvex", 1] = "Team Silver\t5000\tDefeat Lord Yvex with your team dying no more than 15 times";
$Boss::ProficiencyCode["Yvex", 1] = "$TWM2::BossManager.bossKills < 15";
$Boss::Proficiency["Yvex", 2] = "Team Gold\t10000\tDefeat Lord Yvex with your team dying no more than 10 times";
$Boss::ProficiencyCode["Yvex", 2] = "$TWM2::BossManager.bossKills < 10";
$Boss::Proficiency["Yvex", 3] = "Foolproof\t25000\tDefeat Lord Yvex without being killed by his Marvolic Pulse attack";
$Boss::ProficiencyCode["Yvex", 3] = "[bProf].pulseDeaths == 0";
$Boss::Proficiency["Yvex", 4] = "I'll Stick With Bullets\t10000\tDefeat Lord Yvex without using the Aegis of Dawn";
$Boss::ProficiencyCode["Yvex", 4] = "[bProf].aegisUses == 0";
$Boss::Proficiency["Yvex", 5] = "Sleepless\t25000\tDefeat Lord Yvex without falling victim to his nightmare once";
$Boss::ProficiencyCode["Yvex", 5] = "[bProf].totalNightmareTicks == 0";

//TWM2 3.9.2: Boss Scaling Factor
$Boss::DamageScaling["Yvex"] = 5.0;
$Boss::ScaleReduction["Yvex"] = 0.15;

//DATABLOCKS
datablock ParticleData(InflictionNightmareGlobeSmoke) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 0.0;
   lifetimeMS = 5050;
   lifetimeVarianceMS = 0;
   useInvAlpha = true;
   spinRandomMin = -360.0;
   spinRandomMax = 360.0;
   textureName = "particleTest";
   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9 1.0";
   colors[3] = "0.5 0.1 0.9";
   sizes[0] = 1.0;
   sizes[1] = 1.0;
   sizes[2] = 1.0;
   sizes[3] = 1.0;
   times[0] = 0.0;
   times[1] = 0.33;
   times[2] = 0.66;
   times[3] = 1.0;
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   computeCRC = true;
   haslight = true;
   lightType = "PulsingLight";
   lightColor = "0.2 0.0 0.5 1.0";
   lightTime = "200";
   lightRadius = "2.0";
};

datablock ParticleEmitterData(InfNightmareGlobeEmitter) {
   ejectionPeriodMS = 0.1;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 5;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "InflictionNightmareGlobeSmoke";
};


datablock ParticleData(NightmareGlobeSmoke) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 0.0;
   lifetimeMS = 5050;
   lifetimeVarianceMS = 0;
   useInvAlpha = true;
   spinRandomMin = -360.0;
   spinRandomMax = 360.0;
   textureName = "particleTest";
   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
   colors[3] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
   sizes[0] = 1.0;
   sizes[1] = 1.0;
   sizes[2] = 1.0;
   sizes[3] = 1.0;
   times[0] = 0.0;
   times[1] = 0.33;
   times[2] = 0.66;
   times[3] = 1.0;
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   computeCRC = true;
   haslight = true;
   lightType = "PulsingLight";
   lightColor = "0.2 0.0 0.5 1.0";
   lightTime = "200";
   lightRadius = "2.0";
};

datablock ParticleEmitterData(NightmareGlobeEmitter) {
   ejectionPeriodMS = 0.1;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 5;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "NightmareGlobeSmoke";
};

//Yvex STUFF.. MORE
datablock ParticleData(GreenEmitParticle) {
   dragCoeffiecient     = 1;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           =  300;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = 0.0;
   spinRandomMax        = 0.0;

   animateTexture = false;

   textureName = "flareBase"; // "special/Smoke/bigSmoke"

   colors[0]     = "0 1 0";
   colors[1]     = "0 1 0";
   colors[2]     = "0 1 0";

   sizes[0]      = 0.8;
   sizes[1]      = 0.8;
   sizes[2]      = 0.8;

   times[0]      = 0.0;
   times[1]      = 1.0;
   times[2]      = 5.0;

};

datablock ParticleEmitterData(PulseGreenEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;

   ejectionVelocity = 10;
   velocityVariance = 0;

   thetaMin         = 89.0;
   thetaMax         = 90.0;

   orientParticles = false;

   particles = "GreenEmitParticle";
};

datablock SeekerProjectileData(YvexNightmareMissile){
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.8;
   damageRadius        = 8.0;
   radiusDamageType    = $DamageType::Missile;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 30000;
   muzzleVelocity      = 10.0;
   maxVelocity         = 150.0;
   turningSpeed        = 110.0;
   acceleration        = 350.0;

   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;

   flareDistance = 200;
   flareAngle    = 30;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;

   baseEmitter         = NMMissileBaseEmitter;
};

function YvexNightmareMissile::OnExplode(%data, %proj, %pos, %mod) {
   %source = %proj.SourceObject;
   InitContainerRadiusSearch(%proj.getPosition(), 6, $TypeMasks::PlayerObjectType);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      %cl = %potentialTarget.client;
      if(%cl !$= "")
         Yvexnightmareloop(%source, %cl);
   }
}

datablock LinearFlareProjectileData(KillerPulse) {
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.00001;
   hasDamageRadius     = false;
   indirectDamage      = 0.6;
   damageRadius        = 10.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::Admin;
   indirectDamageType  = $DamageType::Admin;

   explosion           = "BlasterExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 200.0;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 30000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   baseEmitter         = PulseGreenEmitter;
   delayEmitter        = PulseGreenEmitter;
   bubbleEmitter       = PulseGreenEmitter;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.2;
   size[2]           = 0.2;


   numFlares         = 15;
   flareColor        = "0 1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 1 0";

};

datablock ParticleData(PurpleNightmareEmitParticle) {
   dragCoeffiecient     = 1;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           =  300;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = 0.0;
   spinRandomMax        = 0.0;

   animateTexture = false;

   textureName = "flareBase"; // "special/Smoke/bigSmoke"

   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9";

   sizes[0]      = 0.4;
   sizes[1]      = 0.4;
   sizes[2]      = 0.4;

   times[0]      = 0.0;
   times[1]      = 1.0;
   times[2]      = 5.0;

};

datablock ParticleEmitterData(YvexSniperEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;

   ejectionVelocity = 10;
   velocityVariance = 0;

   thetaMin         = 89.0;
   thetaMax         = 90.0;

   orientParticles = false;

   particles = "PurpleNightmareEmitParticle";
};

datablock LinearFlareProjectileData(YvexSniperShot) {
   projectileShapeName = "weapon_missile_projectile.dts";
   scale               = "3.0 5.0 3.0";
   faceViewer          = true;
   directDamage        = 0.01;
   kickBackStrength    = 4000.0;
   DirectDamageType    = $DamageType::Zombie;

   explosion           = "BlasterExplosion";

   dryVelocity       = 150.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 10000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;
   activateDelayMS = -1;

   baseEmitter = YvexSniperEmitter;

   size[0]           = 0.0;
   size[1]           = 0.0;
   size[2]           = 0.0;


   numFlares         = 0;
   flareColor        = "0.0 0.0 0.0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock SeekerProjectileData(YvexZombieMakerMissile) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Zombie;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MortarSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 30000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 30.0;
   maxVelocity         = 35.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 23.0;
   acceleration        = 15.0;

   proximityRadius     = 2.5;

   terrainAvoidanceSpeed = 10;
   terrainScanAhead      = 7;
   terrainHeightFail     = 1;
   terrainAvoidanceRadius = 3;

   flareDistance = 40;
   flareAngle    = 20;
   minSeekHeat   = 0.0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 250;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

datablock PlayerData(YvexZombieArmor) : LightMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 400.0;
   minImpactSpeed = 35;
   shapeFile = "medium_male.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::W1700] = 3.0;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
	max[SmokeGrenade]			= 0;
	max[BeaconSmokeGrenade]		= 0;
	max[Blaster]			= 0;
	max[Plasma]				= 0;
	max[PlasmaAmmo]			= 0;
	max[Disc]				= 0;
	max[DiscAmmo]			= 0;
	max[SniperRifle]			= 0;
	max[GrenadeLauncher]		= 0;
	max[GrenadeLauncherAmmo]	= 0;
	max[Mortar]				= 0;
	max[MortarAmmo]			= 0;
	max[MissileLauncher]		= 0;
	max[MissileLauncherAmmo]	= 0;
	max[Chaingun]			= 0;
	max[ChaingunAmmo]			= 0;
	max[RepairGun]			= 0;
	max[CloakingPack]			= 0;
	max[SensorJammerPack]		= 0;
	max[EnergyPack]			= 0;
	max[RepairPack]			= 0;
	max[ShieldPack]			= 0;
	max[AmmoPack]			= 0;
	max[SatchelCharge]		= 0;
	max[MortarBarrelPack]		= 0;
	max[MissileBarrelPack]		= 0;
	max[AABarrelPack]			= 0;
	max[PlasmaBarrelPack]		= 0;
	max[ELFBarrelPack]		= 0;
	max[artillerybarrelpack]	= 0;
	max[MedPack]			= 0;
	max[InventoryDeployable]	= 0;
	max[MotionSensorDeployable]	= 0;
	max[PulseSensorDeployable]	= 0;
	max[TurretOutdoorDeployable]	= 0;
	max[TurretIndoorDeployable]	= 0;
	max[FlashGrenade]			= 0;
	max[ConcussionGrenade]		= 0;
	max[FlareGrenade]			= 0;
	max[TargetingLaser]		= 0;
	max[ELFGun]				= 0;
	max[ShockLance]			= 0;
	max[CameraGrenade]		= 0;
	max[Beacon]				= 0;
	max[flamerAmmoPack]		= 0;
	max[ParachutePack]		= 0;
	max[ConstructionTool]		= 0;
	max[MergeTool]			= 0;
	max[NerfGun]			= 0;
	max[NerfBallLauncher]		= 0;
	max[NerfBallLauncherAmmo]	= 0;
	max[SuperChaingun]		= 0;
	max[SuperChaingunAmmo]		= 0;
	max[RPChaingun]			= 0;
	max[RPChaingunAmmo]		= 0;
	max[MGClip]				= 0;
	max[LSMG]				= 0;
	max[LSMGAmmo]			= 0;
	max[LSMGClip]			= 0;
	max[snipergun]			= 0;
	max[snipergunAmmo]		= 0;
	max[Bazooka]			= 0;
	max[BazookaAmmo]			= 0;
	max[BunkerBuster]				= 0;
	max[MG42]				= 0;
	max[MG42Ammo]			= 0;
	max[SPistol]			= 0;
	max[Pistol]				= 0;
	max[PistolAmmo]			= 0;
	max[Pistolclip]			= 0;
	max[flamer]				= 0;
	max[flamerAmmo]			= 0;
	max[AALauncher]			= 0;
	max[AALauncherAmmo]		= 0;
	max[melee]				= 0;
	max[SOmelee]			= 0;
	max[KriegRifle]			= 0;
	max[KriegAmmo]			= 0;
	max[Rifleclip]			= 0;
	max[Shotgun]			= 0;
	max[ShotgunAmmo]			= 0;
	max[ShotgunClip]			= 0;
	max[RShotgun]			= 0;
	max[RShotgunAmmo]			= 0;
	max[RShotgunClip]			= 0;
	max[LMissileLauncher]		= 0;
	max[LMissileLauncherAmmo]	= 0;
	max[HRPChaingun]			= 0;
	max[RPGAmmo]			= 0;
	max[RPGItem]			= 0;
	max[spineDeployable]		= 0;
	max[mspineDeployable]		= 0;
	max[wWallDeployable]		= 0;
	max[floorDeployable]		= 0;
	max[WallDeployable]		= 0;
      max[DoorDeployable]           = 0;
	max[TurretLaserDeployable]	= 0;
	max[TurretMissileRackDeployable]= 0;
	max[DiscTurretDeployable]	= 0;
	max[EnergizerDeployable]	= 0;
	max[TreeDeployable]		= 0;
	max[CrateDeployable]		= 0;
	max[DecorationDeployable]	= 0;
	max[LogoProjectorDeployable]	= 0;
	max[LightDeployable]		= 0;
	max[TripwireDeployable]		= 0;
	max[TelePadPack]			= 0;
	max[TurretBasePack]		= 0;
	max[LargeInventoryDeployable]	= 0;
	max[GeneratorDeployable]	= 0;
	max[SolarPanelDeployable]	= 0;
	max[SwitchDeployable]		= 0;
	max[MediumSensorDeployable]	= 0;
	max[LargeSensorDeployable]	= 0;
	max[SpySatelliteDeployable]	= 0;
	max[JumpadDeployable]		= 0;
	max[EscapePodDeployable]	= 0;
	max[ForceFieldDeployable]	= 0;
	max[GravityFieldDeployable]	= 0;
      max[VehiclePadPack]		= 0;
};

//CREATION
function SpawnYvex(%position) {
   %Zombie = new player(){
      Datablock = "YvexZombieArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('MsgYvexreturn', "\c4"@$TWM2::ZombieName[7]@": Did you miss me? Because... I WANT MY REVENGE!!!");

   %command = "Yvexmovetotarget";
   %zombie.ticks = 0;
   InitiateBoss(%zombie, "Yvex");
   
   YvexAttack_FUNC("ZombieSummon", %zombie);
   YvexAttacks(%zombie);
    
   %Zombie.team = 30;
   %zname = $TWM2::ZombieName[7]; // <- To Hosts, Enjoy, You can
                                      //Change the Zombie Names now!!!
   %zombie.target = createTarget(%zombie, %zname, "", "Derm3", '', %zombie.team, PlayerSensor);
   setTargetSensorData(%zombie.target, PlayerSensor);
   setTargetSensorGroup(%zombie.target, 30);
   setTargetName(%zombie.target, addtaggedstring(%zname));
   setTargetSkin(%zombie.target, 'Horde');
   //
   %zombie.type = %type;
   %Zombie.setTransform(%cpos);
   %zombie.canjump = 1;
   %zombie.hastarget = 1;
   %zombie.isZombie = 1;
   MissionCleanup.add(%Zombie);
   schedule(1000, %zombie, %command, %zombie);
}


//AI

function Yvexmovetotarget(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %zombie.startFade(400, 0, true);
      %zombie.startFade(1000, 0, false);
      %zombie.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), TWM2Lib_MainControl("getRandomPosition", 25 TAB 1)));
      %zombie.setVelocity("0 0 0");
      MessageAll('msgYvexAttack', "\c4"@$TWM2::ZombieName[7]@": I shall not fall to my end!");
   }
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
	if(%zombie.hastarget != 1){
       serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
       serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

    %zombie.ticks++;
	%vector = vectorscale(%vector, $Zombie::DForwardSpeed / 2);
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
   %zombie.moveloop = schedule(500, %zombie, "Yvexmovetotarget", %zombie);
}

//ATTACKS
function YvexAttacks(%yvex) {
   if(!isObject(%yvex) || %yvex.getState() $= "dead") {
      return;
   }
   %closestClient = ZombieLookForTarget(%yvex);
   //%closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   %closestDistance = vectorDist(%yvex.getPosition(), %closestClient.getPosition());
   
   if(%closestClient) {
      if(%closestDistance <= 150) {
         %att = getRandom(1, 3);
         switch(%att) {
            case 1:
               YvexAttack_FUNC("FireCurse", %yvex SPC %closestClient);
            case 2:
               YvexAttack_FUNC("FireSniper", %yvex SPC %closestClient);
            case 3:
               YvexAttack_FUNC("LaunchSummonMissile", %yvex SPC %closestClient);
         }
      }
      else {
         %att = getRandom(1, 3);
         switch(%att) {
            case 1:
               YvexAttack_FUNC("RiftPulse", %yvex SPC %closestClient);
            case 2:
               YvexAttack_FUNC("NightmareMissile", %yvex SPC %closestClient);
            case 3:
               YvexAttack_FUNC("LaunchSummonMissile", %yvex SPC %closestClient);
         }
      }
   }
   
   schedule(25000, 0, "YvexAttacks", %yvex);
}

function YvexAttack_FUNC(%att, %args) {
   switch$(%att) {
      case "ZombieSummon":
         %z = getWord(%args, 0);
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
         //schedule the next one
         schedule(40000, 0, "YvexAttack_FUNC", "ZombieSummon", %z);
         //--------------------
         %type = TWM2Lib_Zombie_Core("getRandomZombieType", "1 2 3 4 5 9 12 13");
         %msg = getrandom(1, 3);
         switch(%msg) {
            case 1:
               messageall('YvexMsg',"\c4"@$TWM2::ZombieName[7]@": Enlisted for revenge... ATTACK");
            case 2:
               messageall('YvexMsg',"\c4"@$TWM2::ZombieName[7]@": Attack my soldiers.. REVENGE is ours");
            case 3:
               messageall('YvexMsg',"\c4"@$TWM2::ZombieName[7]@": Take out the enemy, ALL OF THEM!");
         }
         for(%i = 0; %i < 5; %i++) {
            %pos = vectoradd(%z.getPosition(), TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
            %fpos = vectoradd("0 0 5",%pos);
            TWM2Lib_Zombie_Core("SpawnZombie", "zSpawnCommand", %type, %fpos);
         }
         %z.setMoveState(true);
         %z.setActionThread($Zombie::RogThread, true);
         %z.schedule(3500, "setMoveState", false);
         
      case "FireCurse":
         MessageAll('msgWTFH', "\c4"@$TWM2::ZombieName[7]@": DIE!!!");
         %zombie = getWord(%args, 0);
         %target = getWord(%args, 1);

         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(0));
         %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));
         %p = new LinearFlareProjectile() {
             dataBlock        = KillerPulse;
             initialDirection = %vec;
             initialPosition  = %zombie.getMuzzlePoint(0);
             sourceObject     = %zombie;
             sourceSlot       = 0;
         };
         
      case "FireSniper":
         %zombie = getWord(%args, 0);
         %target = getWord(%args, 1);
      
         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(0));
         %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));
         %p = new LinearFlareProjectile() {
             dataBlock        = YvexSniperShot;
             initialDirection = %vec;
             initialPosition  = %zombie.getMuzzlePoint(0);
             sourceObject     = %zombie;
             sourceSlot       = 0;
         };
         
      case "LaunchSummonMissile":
         %z = getWord(%args, 0);
         %t = getWord(%args, 1);
         %vec = vectorNormalize(vectorSub(%t.getPosition(),%z.getPosition()));
   	     %p = new SeekerProjectile() {
            dataBlock        = YvexZombieMakerMissile;
            initialDirection = %vec;
            initialPosition  = %z.getMuzzlePoint(4);
            sourceObject     = %z;
            sourceSlot       = 4;
         };
   	     %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %t.getWorldBoxCenter();
         };
   	     %beacon.team = 0;
   	     %beacon.setTarget(0);
   	     MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%t, %beacon, %p);
      
      case "RiftPulse":
         %t = getWord(%args, 0);
         %ct = getWord(%args, 1);
         
         if(!isObject(%t)) {
            return;
         }
         %t.setMoveState(true);
         %ct++;
         if(%ct > 30) {
            %t.setMoveState(false);
         }
         schedule(500, 0, "YvexAttack_FUNC", "RiftPulse", %t SPC %ct);
      
      case "NightmareMissile":
         %z = getWord(%args, 0);
         %t = getWord(%args, 1);
         %vec = vectorNormalize(vectorSub(%t.getPosition(),%z.getPosition()));
   	     %p = new SeekerProjectile() {
            dataBlock        = YvexNightmareMissile;
            initialDirection = %vec;
            initialPosition  = %z.getMuzzlePoint(4);
            sourceObject     = %z;
            sourceSlot       = 4;
         };
   	     %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %t.getWorldBoxCenter();
         };
   	     %beacon.team = 0;
   	     %beacon.setTarget(0);
   	     MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%t, %beacon, %p);
      
      case "KillLoop":
         %player = getWord(%args, 0);
         if(isObject(%player)) {
            %player.disablemove(true);
            if (%player.getState() $= "dead") {
               return;
            }
            %player.setActionThread("Death2");
            if(%player.beats == 1) {
               messageclient(%player.client, 'MsgClient', "\c2You feel the life slowly leave you.");
               messageclient(%player.client, 'MsgClient', "~wfx/misc/heartbeat.wav");
            }
            if(%player.beats < 10) {
               %player.setWhiteOut(%player.beats * 0.2);
            }
            else {
               %player.setDamageFlash(1);
               %player.scriptKill(0);
            }
         }
         %player.beats++;
         Schedule(600, 0, "YvexAttack_FUNC", "KillLoop", %player);
   }
}

function YvexSniperShot::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   if(!isplayer(%targetObject)) {
      return;
   }
   %targ = %targetObject.client;
   %Zombie = %projectile.sourceObject;
   %targ.nightmareticks = 0;
   Yvexnightmareloop(%zombie,%targ);
   %randMessage = getrandom(3)+1;
   switch(%randMessage) {
      case 1:
         MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[7]@": Let the revenge begin, "@getTaggedString(%targ.name)@".");
      case 2:
         MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[7]@": Taste my vengance... "@getTaggedString(%targ.name)@".");
      case 3:
         MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[7]@": Sleep Forever... "@getTaggedString(%targ.name)@".");
      default:
         MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[7]@": This Nightmare will lock you forever "@getTaggedString(%targ.name)@"!");
   }
}

function Yvexnightmareloop(%zombie,%viewer) {
   %enum = getRandom(1,5);
   switch(%enum) {
      case 1:
         %emote = "sitting";
      case 2:
         %emote = "standing";
      case 3:
         %emote = "death3";
      case 4:
         %emote = "death2";
      case 5:
         %emote = "death4";
   }
   if(!isobject(%viewer.player) || %viewer.player.getState() $= "dead") {
      %viewer.nightmared = 0;
      return;
   }
   if(!isobject(%zombie)) {
      %viewer.nightmared = 0;
      %viewer.player.setMoveState(false);
      return;
   }
   if(%viewer.nightmareticks > 10) {
      %viewer.player.setMoveState(false);
      %viewer.nightmareticks = 0;
      %viewer.nightmared = 0;
      return;
   }
   %c = createEmitter(%viewer.player.position,NightmareGlobeEmitter,"1 0 0");      //Rotate it
   MissionCleanup.add(%c); // I think This should be used
   schedule(500,0,"killit",%c);
   %viewer.nightmareticks++;
   %viewer.player.setMoveState(true);
   %viewer.nightmared = 1;
   %viewer.player.setActionThread(%emote,true);
   %viewer.player.setWhiteout(0.8);
   %viewer.player.setDamageFlash(0.5);
   
   %viewer.bossProficiency.totalNightmareTicks++;

   %zombie.playShieldEffect("1 1 1");
   serverPlay3D(NightmareScreamSound, %viewer.player.position);
   schedule(500,0,"Yvexnightmareloop",%zombie, %viewer);
   %viewer.player.damage(0, %viewer.player.position, 0.03, $DamageType::Zombie);
   %zombie.setDamageLevel(%zombie.getDamageLevel() - 0.15);

   BottomPrint(%viewer,"You are locked in "@$TWM2::ZombieName[7]@"'s Nightmare.",5,1);
   schedule(1, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem1/avo.deathcry_02.wav");
   schedule(5, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem2/avo.deathcry_02.wav");
   schedule(10, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem3/avo.deathcry_02.wav");
   schedule(15, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem4/avo.deathcry_02.wav");
   schedule(20, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem5/avo.deathcry_02.wav");
   schedule(25, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male1/avo.deathcry_02.wav");
   schedule(30, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male2/avo.deathcry_02.wav");
   schedule(35, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male3/avo.deathcry_02.wav");
   schedule(40, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male4/avo.deathcry_02.wav");
   schedule(45, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male5/avo.deathcry_02.wav");
   schedule(50, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/derm1/avo.deathcry_02.wav");
   schedule(55, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/derm2/avo.deathcry_02.wav");
   schedule(60, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/derm3/avo.deathcry_02.wav");
}

function KillerPulse::onCollision(%data,%projectile,%targetObject,%modifier,%position,%normal) {
   if (%targetObject.getClassName() $= "Player" && !%targetObject.isBoss) {
      messageall('msgkillcurse', "\c5"@getTaggedString(%targetObject.client.name)@" Took a fatal Hit from "@$TWM2::ZombieName[7]@"'s Dark Energy");
      %targetObject.throwWeapon();
      %targetObject.clearinventory();
      YvexAttack_FUNC("KillLoop", %targetObject);
	  
	  %targetObject.client.bossProficiency.pulseDeaths++;
   }
}

function YvexZombieMakerMissile::OnExplode(%data, %proj, %pos, %mod) {
   %c = CreateEmitter(%pos, NightmareGlobeEmitter, "0 0 1");
   %rand = getRandom(1, 6);
   %c.schedule(%rand * 750, "delete");
   for(%i = 0; %i < %rand; %i++) {
      %time = %i * 750;
      %type = TWM2Lib_Zombie_Core("getRandomZombieType", "1 2 3 4 5 9 12 13");
	  schedule(%time, 0, "TWM2Lib_Zombie_Core", "SpawnZombie", "zSpawnCommand", %type, vectorAdd(%pos, "0 0 1"));
   }
}
