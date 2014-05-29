//LORD \/ARDISON & Dark Archmage Vardison
//THIS BOSS WILL MURDER OCCULTBADBOY
//Yeah, he really will
datablock LinearFlareProjectileData(SuperlaserProjectile) {
   scale               = "15.0 15.0 15.0";
   faceViewer          = false;
   directDamage        = 1.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 30.0;
   kickBackStrength    = 1000.0;
   radiusDamageType    = $DamageType::Explosion;

   explosion[0]           = "HyperDevCannonExplosion2";
   explosion[1]           = "SatchelMainExplosion";
   splash              = PlasmaSplash;
   baseEmitter         = HyperDevCannonBaseEmitter;


   dryVelocity       = 200.0;
   wetVelocity       = 200;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 10000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 9;
   size[1]           = 10;
   size[2]           = 11;


   numFlares         = 400;
   flareColor        = "0.0 1.0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.75 0.25";

};

//
datablock ParticleData(ShadowBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;

   useInvAlpha = true;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\

   sizes[0]      = 2.5;
   sizes[1]      = 2.7;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadowBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "ShadowBaseParticle";
};

datablock ParticleData(SmallShadowBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 9999999999999;
   lifetimeVarianceMS   = 9999999999999;

   useInvAlpha = true;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\

   sizes[0]      = 0.5;
   sizes[1]      = 0.7;
   sizes[2]      = 1.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(SmallShadowBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "SmallShadowBaseParticle";
};

datablock ParticleData(JetShadowParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 2500;
	lifetimeVarianceMS   = 0;

	textureName          = "particleTest";

	useInvAlpha = true;
	spinRandomMin = -160.0;
	spinRandomMax = 160.0;

	animateTexture = true;
	framesPerSec = 15;

	animTexName[0]       = "special/Explosion/exp_0016";
	animTexName[1]       = "special/Explosion/exp_0018";
	animTexName[2]       = "special/Explosion/exp_0020";
	animTexName[3]       = "special/Explosion/exp_0022";
	animTexName[4]       = "special/Explosion/exp_0024";
	animTexName[5]       = "special/Explosion/exp_0026";
	animTexName[6]       = "special/Explosion/exp_0028";
	animTexName[7]       = "special/Explosion/exp_0030";
	animTexName[8]       = "special/Explosion/exp_0032";

    colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
    colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
    colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
	sizes[0]      = 2.5;
	sizes[1]      = 1.25;
	sizes[2]      = 0.625;
	times[0]      = 0.0;
	times[1]      = 0.7;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(JetShadowEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 5;
   thetaMin         = 22.5;
   thetaMax         = 45;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   spinRandomMin   = "200";
   spinRandomMax   = "-200";
   overrideAdvances = false;
   particles = "JetShadowParticle";
};

datablock ParticleData(LaserBallGlobeSmoke) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 0.0;
   lifetimeMS = 1000;
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

datablock ParticleEmitterData(MiniShadowBallEmitter) {
   ejectionPeriodMS = 0.3;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 2;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "LaserBallGlobeSmoke";
};

datablock LinearFlareProjectileData(ShadowBomb) : FlamethrowerBolt {
   baseEmitter        = ShadowBaseEmitter;
   fizzleTimeMS      = 14000;
   lifetimeMS        = 10000; // z0dd - ZOD, 4/25/02. Was 6000
};

datablock SeekerProjectileData(VardisonLaserBallMissile) : YvexNightmareMissile {
   baseEmitter         = ShadowBaseEmitter;
};

function VardisonLaserBallMissile::OnExplode(%data, %proj, %pos, %mod) {
   //LaserBall
   %ball = CreateEmitter(%pos, "MiniShadowBallEmitter", "0 0 0 0");
   %ball.schedule(10000, "Delete");
   LaserBallStrike(vectorAdd(%pos, "0 0 3"), 0);
}

function LaserBallStrike(%position, %count) {
   %count++;
   if(%count > 100) {
      %p = new LinearFlareProjectile() {
	     dataBlock        = VardisonSubShadowBomb;
         initialDirection = "0 0 -1";
	     initialPosition  = %position;
      };
      return; //stop here
   }
   else {
      if(%count % 3 == 0) {     //multiples of 3 == strike
         %p = new TracerProjectile() {
	        dataBlock        = PlasmaCannonMainProj;
            initialDirection = vectorAdd(%postition, getRandomPosition(25,0));
	        initialPosition  = %position;
         };
      }
   }
   schedule(100, 0, "LaserBallStrike", %position, %count);
}

datablock LinearFlareProjectileData(VardisonSubShadowBomb) : DMPlasma {
   explosion = MortarExplosion;
   dryVelocity       = 500.0; // z0dd - ZOD, 4/25/02. Was 50. Velocity of projectile out of water
   wetVelocity       = -1;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 14000;
   lifetimeMS        = 10000; // z0dd - ZOD, 4/25/02. Was 6000
};

//ARMOR DBs
datablock PlayerData(VardisonStage1Armor) : LightMaleHumanArmor {
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 300.0;
   minImpactSpeed = 35;
   shapeFile = "light_male.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock PlayerData(DarkArchmageVardisonArmor) : VardisonStage1Armor {
   maxDamage = 1000.0;
   minImpactSpeed = 35;
   shapeFile = "light_male.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock FlyingVehicleData(VardisonStage2Flyer) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_bomber.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_air_bomber.dts";
   debris = MeShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.15;
   density = 1.0;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = false;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = MeVehicleExplosion;
	explosionDamage = 1.0;
	explosionRadius = 10.0;

   maxDamage = 50.0;
   destroyedLevel = 50.0;

   HDAddMassLevel = 49.9;
   HDMassImage = LflyerHDMassImage;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 5000;      // Afterburner and any energy weapon pool
   rechargeRate = 4;

   minDrag = 22;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 50;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 1;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.8;      // Dampen control input so you don't` whack out at very slow speeds


   // Maneuvering
   maxSteeringAngle = 4.5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 4;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 5250;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 675;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 3000;      // Steering jets (how much you heel over when you turn)
   rollForce = 1;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 2.5;        // Height off the ground at rest
   createHoverHeight = 1;  // Height off the ground when created
   maxForwardSpeed = 165;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 2500;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 40;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 10;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 1.25;

   // Rigid body
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 20;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 70;      // The speed your contrail shows up at.
   trailEmitter = JetShadowEmitter;
   forwardJetEmitter = JetShadowEmitter;
   downJetEmitter = JetShadowEmitter;

   //
   jetSound = ScoutFlyerThrustSound;
   engineSound = ScoutFlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0;
   mediumSplashSoundVelocity = 15.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 1.0;

   damageEmitter[0] = MeLightDamageSmoke;
   damageEmitter[1] = MeHeavyDamageSmoke;
   damageEmitter[2] = MeDamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.4;
   damageLevelTolerance[1] = 0.75;
   numDmgEmitterAreas = 1;

   //
   max[chaingunAmmo] = 2000;
   max[MissileLauncherAmmo] = 200;
   max[MortarAmmo] = 200;

   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   ShieldDamageScale[$DamageType::Bullet] = 0.01;  //I deny you shrike n0bs

   minMountDist = 7;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Lord Vardison';
   targetTypeTag = '';
   sensorData = SSTurretBaseSensorObj;
   sensorRadius = SSTurretBaseSensorObj.detectRadius;
   sensorColor = "9 9 255";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

   numWeapons = 3;

   replaceTime = 90;
};

datablock PlayerData(VardisonStage3Armor) : LightMaleHumanArmor {
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 500.0;
   minImpactSpeed = 35;
   shapeFile = "TR2Heavy_Male.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;
   
   boundingBox = "5 5 10";

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::PlasmaCannon] = 0.001;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function StartDAVardison(%pos) {
   %Vardison = new player() {
      Datablock = "DarkArchmageVardisonArmor";
   };
   %Cpos = vectorAdd(%pos, "0 0 5");
   MessageAll('MsgVardison', "\c4"@$TWM2::BossName["DAVardison"]@": Rise forces of darkness, as our enemies shall face their demise");

   %command = "DAVardisonmovetotarget";
   %Vardison.ticks = 0;
   InitiateBoss(%Vardison, "DAVardison");

   %Vardison.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["DAVardison"]@""); // <- To Hosts, Enjoy, You can
                                      //Change the Demon Names now!!!
   %Vardison.target = createTarget(%Vardison, %zname, "", "Derm3", '', %Vardison.team, PlayerSensor);
   setTargetSensorData(%Vardison.target, PlayerSensor);
   setTargetSensorGroup(%Vardison.target, 30);
   setTargetName(%Vardison.target, addtaggedstring(%zname));
   setTargetSkin(%Vardison.target, 'Inferno');
   //
   %Vardison.setPosition(%cpos);
   %Vardison.canjump = 1;
   %Vardison.hastarget = 1;
   MissionCleanup.add(%Vardison);
   schedule(1000, %Vardison, %command, %Vardison);

   DAVardisonAttacks(%Vardison);
}

function StartVardison1(%pos) {

	%Vardison = new player(){
	   Datablock = "VardisonStage1Armor";
	};
	%Cpos = vectorAdd(%pos, "0 0 5");
    MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": "@$TWM2::BossName["Vardison"]@", Checking into duty, and about to slaughter some fuckin' enemies!!!");

	%command = "Vardison1movetotarget";
    %Vardison.ticks = 0;
    InitiateBoss(%Vardison, "Vardison1");

   %Vardison.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["Vardison"]@""); // <- To Hosts, Enjoy, You can
                                      //Change the Demon Names now!!!
   %Vardison.target = createTarget(%Vardison, %zname, "", "Derm3", '', %Vardison.team, PlayerSensor);
   setTargetSensorData(%Vardison.target, PlayerSensor);
   setTargetSensorGroup(%Vardison.target, 30);
   setTargetName(%Vardison.target, addtaggedstring(%zname));
   setTargetSkin(%Vardison.target, 'Inferno');
   //
   %Vardison.setPosition(%cpos);
   %Vardison.canjump = 1;
   %Vardison.hastarget = 1;
   MissionCleanup.add(%Vardison);
   schedule(1000, %Vardison, %command, %Vardison);

   VardisonAttacks(%Vardison);
}

function StartVardison2(%pos) {
   %StartPos = VectorAdd(%pos, "0 0 100");
	%team = 30;
	%rotation = "1 0 0 0";
    %skill = 10;

   %Drone = new FlyingVehicle() {
      dataBlock    = VardisonStage2Flyer;
      position     = %StartPos;
      rotation     = %rotation;
      team         = %team;
   };
   MissionCleanUp.add(%Drone);
   
   MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": HA! I'm nowhere near finished with you, Lets take this to the skies.. shall we.");

   setTargetSensorGroup(%Drone.getTarget(), %team);

   %Drone.isdrone = 1;
   %drone.dodgeGround = 0;

   %drone.isace = 1;

   %drone.skill = 0.2 + (%skill / 12.5);

   schedule(100, 0, "DroneForwardImpulse", %drone); //special impulse
   schedule(101, 0, "DronefindTarget", %drone);
   schedule(102, 0, "DroneScanGround", %drone);

   InitiateBoss(%drone, "Vardison2");
   VardisonDroneAttacks(%drone);
}

function StartVardison3(%pos) {
	%Vardison = new player(){
	   Datablock = "VardisonStage3Armor";
	};
	%Cpos = vectorAdd(%pos, "0 0 5");
    MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Now you will see the full power of a shadow demon!!!");

	%command = "Vardison3movetotarget";
    %Vardison.ticks = 0;
    InitiateBoss(%Vardison, "Vardison3");

   %Vardison.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["Vardison"]@""); // <- To Hosts, Enjoy, You can
                                      //Change the Demon Names now!!!
   %Vardison.target = createTarget(%Vardison, %zname, "", "Derm3", '', %Vardison.team, PlayerSensor);
   setTargetSensorData(%Vardison.target, PlayerSensor);
   setTargetSensorGroup(%Vardison.target, 30);
   setTargetName(%Vardison.target, addtaggedstring(%zname));
   setTargetSkin(%Vardison.target, 'Inferno');
   //
   %Vardison.setTransform(%cpos);
   %Vardison.canjump = 1;
   %Vardison.hastarget = 1;
   MissionCleanup.add(%Vardison);
   schedule(1000, %Vardison, %command, %Vardison);
   
   VardisonDemonAttacks(%Vardison);
   
   //SpawnVardHelper(%Vardison, vectorAdd(%Vardison.getPosition(), "15 0 100"));

}

function DAVardisonmovetotarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["DAVardison"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 15) {
          if(!%closestClient.vardKilling) {
             %closestClient.vardKilling = 1;
             DoVardisonSuperCloseKill(%Demon, %closestClient, 0);
             MessageAll('MsgVardison', "\c4"@$TWM2::BossName["DAVardison"]@": Die.... Human....");
          }
       }
	   if(%Demon.hastarget != 1){
	      //LZDoYell(%Demon);
	      %Demon.hastarget = 1;
       }

       %vector = ZgetFacingDirection(%Demon,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%Demon.applyImpulse(%pos, %vector);
   }
   else if(%Demon.hastarget == 1){
	%Demon.hastarget = 0;
	%Demon.DemonRmove = schedule(100, %Demon, "ZSetRandomMove", %Demon);
   }
   %Demon.moveloop = schedule(500, %Demon, "DAVardisonmovetotarget", %Demon);
}

function Vardison1movetotarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 15) {
          if(!%closestClient.vardKilling) {
             %closestClient.vardKilling = 1;
             DoVardisonSuperCloseKill(%Demon, %closestClient, 0);
             MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Die.... Human....");
          }
       }
	   if(%Demon.hastarget != 1){
	      //LZDoYell(%Demon);
	      %Demon.hastarget = 1;
       }

       %vector = ZgetFacingDirection(%Demon,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%Demon.applyImpulse(%pos, %vector);
   }
   else if(%Demon.hastarget == 1){
	%Demon.hastarget = 0;
	%Demon.DemonRmove = schedule(100, %Demon, "ZSetRandomMove", %Demon);
   }
   %Demon.moveloop = schedule(500, %Demon, "Vardison1movetotarget", %Demon);
}

function Vardison3movetotarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 10) {
          %closestClient.scriptKill(0);
          MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": DIE!!!!!!");
       }
	   if(%Demon.hastarget != 1){
	      %Demon.hastarget = 1;
       }

       %vector = ZgetFacingDirection(%Demon,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed*1.8);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%Demon.applyImpulse(%pos, %vector);
   }
   else if(%Demon.hastarget == 1){
	%Demon.hastarget = 0;
	%Demon.DemonRmove = schedule(100, %Demon, "ZSetRandomMove", %Demon);
   }
   %Demon.moveloop = schedule(500, %Demon, "Vardison3movetotarget", %Demon);
}

//ATTACKS

function DoVardisonSuperCloseKill(%source, %target, %count) {
   %count++;
   if(!isObject(%source) || %source.getState() $= "dead") {
      %target.setMoveState(false);
      return;
   }
   %source.setMoveState(true);
   %target.setMoveState(true);
   %target.clearInventory(); //ha, no guns for You!
   //lift
   if(%count <= 15) {
      %ZPos = %count * 0.025;
	  %newpos = vectoradd(%target.getPosition(),"0 0 "@%ZPos@"");
	  %target.setTransform(%newpos);
	  %target.setvelocity("0 0 0");
   }
   else if(%count == 16) {
      //MessageAll('MsgDIE', "\c4"@%source.client.namebase@": You're so.... weak...");
	  %newpos = vectoradd(%target.getPosition(),"0 0 "@%ZPos * -1@"");
	  %target.setTransform(%newpos);
	  %target.setvelocity("0 0 0");
   }
   else if(%count == 17) {
      %target.setvelocity("1000 1000 1000");
      %target.blowup();//BAM!
      ServerPlay3d(BOVHitSound, %target.getPosition());
      ServerPlay3d(BOVHitSound, %target.getPosition());
      ServerPlay3d(BOVHitSound, %target.getPosition());
      %target.damage(%source, %target.getposition(), 9999, $DamageType::BladeOfVengance);
      %source.setMoveState(false);
      return;
   }
   schedule(100, 0, "DoVardisonSuperCloseKill", %source, %target, %count);
}

function ShadowBomb::onExplode(%data, %proj, %pos, %mod) {
   %vec = %proj.spdvec;
   %vec = getword(%vec, 0)@" "@getword(%vec, 1)@" 0";
   %vec = vectorNormalize(%vec);
   %vec = vectorscale(%vec, 30);
   %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %proj);
   for(%i = 0; %i < 3; %i++) {
      if(%result)
	     schedule((5 * %i), 0, "Napalm2FindNewDir", %pos, %vec, %proj.sourceobject, 0, 0);
	  else {
	     %rndvec = (getRandom(1, 15) - 7.5)@" "@(getRandom(1, 15) - 7.5)@" "@((getRandom() * 5) + 5);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new LinearFlareProjectile() {
	   	    dataBlock        = VardisonSubShadowBomb;
	   	    initialDirection = "0 0 -1";
	   	    initialPosition  = %newvec;
	   	    sourceObject     = %proj.sourceobject;
            SourceSlot       = 5;
	     };
	     %p.sourceobject = %proj.sourceobject;
	     %p.vector = %vec;
	     %p.count = 1;
         if(%proj.maxExplode $= "") {
            %p.maxExplode = 15;
         }
         else {
            %p.maxExplode = %proj.maxExplode;
         }
      }
   }
   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
}

function VardisonSubShadowBomb::onExplode(%data, %proj, %pos, %mod) {
   if(%proj.count < %proj.maxExplode) { //holy... christ
      %vec = vectorscale(vectornormalize(%proj.vector), 24);
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %proj);
      if(%result)
         schedule(5, 0, "Napalm2FindNewDir", %pos, %vec, %proj.sourceobject, %proj.count, 0);
	  else {
	     %rndvec = (getRandom(1, 10) - 5)@" "@(getRandom(1, 10) - 5)@" "@((getRandom() * 5) + 5);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new LinearFlareProjectile() {
		    dataBlock        = VardisonSubShadowBomb;
		    initialDirection = "0 0 -1";
		    initialPosition  = %newvec;
		    sourceObject     = %proj.sourceobject;
   		    sourceSlot       = 5;
         };
         %p.sourceobject = %proj.sourceobject;
	     %p.vector = %vec;
	     %p.count = %proj.count + 1;
      }
   }
   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
}

function Napalm2FindNewDir(%pos, %vec, %source, %count, %count2) {
   if(%count2 == 2) {
	  %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	  %newvec = vectoradd(%pos,%rndvec);
	  %p = new LinearFlareProjectile() {
	     dataBlock        = VardisonSubShadowBomb;
	     initialDirection = "0 0 -1";
	     initialPosition  = %newvec;
	     sourceObject     = %source;
         sourceSlot       = 5;
	  };
	  %p.sourceobject = %source;
	  %p.vector = %vec;
	  %p.count = %count+1;
	  return;
   }
   if(%count2 == 1) {
      %vec = vectorscale(%vec,-1);
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, 0);
	  if(!(%result)){
	     %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new LinearFlareProjectile() {
		    dataBlock        = VardisonSubShadowBomb;
		    initialDirection = "0 0 -1";
		    initialPosition  = %newvec;
		    sourceObject     = %source;
   		    sourceSlot       = 5;
	     };
	     %p.sourceobject = %source;
	     %p.vector = %vec;
	     %p.count = %count+1;
	     return;
      }
   }
   else {
      %chance = getrandom(1,4);
      if(%chance <= 2){
	     %nv2 = (getword(%vec, 0) * -1);
	     %nv1 = getword(%vec, 1);
	     %vec = %nv1@" "@%nv2@" 0";
	  }
      else {
	     %nv2 = getword(%vec, 0);
	     %nv1 = (getword(%vec, 1) * -1);
	     %vec = %nv1@" "@%nv2@" 0";
	  }
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, 0);
	  if(!(%result)){
	     %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new LinearFlareProjectile() {
		    dataBlock        = VardisonSubShadowBomb;
		    initialDirection = "0 0 -1";
		    initialPosition  = %newvec;
		    sourceObject     = %source;
   		    sourceSlot       = 5;
	     };
	     %p.sourceobject = %source;
	     %p.vector = %vec;
	     %p.count = %count+1;
	     return;
      }
   }
   %count2++;
   schedule(2, 0, "Napalm2FindNewDir", %pos, %vec, %source, %count, %count2);
}

function GOVDoFlameCano(%g, %target) {
   if(!isObject(%g) || %g.getState() $= "dead") {
      return;
   }
   %g.setPosition(VectorAdd(%target.getPosition(), "0 0 70"));
   %Pad = new StaticShape() {
      dataBlock = DeployedSpine;
      scale = ".1 .1 1";
      position = VectorAdd(%target.getPosition(), "0 0 69");
   };
   %g.setMoveState(true);
   %Pad.setCloaked(true);
   %Pad.schedule(3000, "setPosition", vectorSub(%Pad.getPosition(), "0 0 10"));
   %Pad.schedule(4000, "setPosition", vectorSub(%Pad.getPosition(), "0 0 20"));
   %Pad.schedule(5000, "setPosition", vectorSub(%Pad.getPosition(), "0 0 30"));
   %Pad.schedule(6000, "setPosition", vectorSub(%Pad.getPosition(), "0 0 40"));
   %g.schedule(6500, "SetMoveState", false);
   %pad.schedule(6500, "Delete");
   //The Vector Crap
   schedule(2500,0,"DropFlameCano2", %g, %target);
}

function DropFlameCano2(%g, %target) {
   if(!isObject(%g) || %g.getState() $= "dead") {
      return;
   }
   //First, Specify All Directions
   %vec[1] = vectorscale(vectornormalize("1 0 0"), 24);  // +X 0Y
   %vec[2] = vectorscale(vectornormalize("1 1 0"), 24);  // +X +Y
   %vec[3] = vectorscale(vectornormalize("1 -1 0"), 24); // +X -Y
   %vec[4] = vectorscale(vectornormalize("-1 0 0"), 24); // -X 0Y
   %vec[5] = vectorscale(vectornormalize("-1 1 0"), 24); // -X +Y
   %vec[6] = vectorscale(vectornormalize("-1 -1 0"), 24); //-X -Y
   %vec[7] = vectorscale(vectornormalize("0 1 0"), 24);  // 0X +Y
   %vec[8] = vectorscale(vectornormalize("0 -1 0"), 24); // 0X -Y
   //Oh.. long crap
   for(%i = 1; %i <= 8; %i++) {
      %p = new LinearFlareProjectile() {
	      	dataBlock        = ShadowBomb;
	    	initialDirection = "0 0 -30";
	    	initialPosition  = vectorAdd(%g.getPosition(), "0 0 -3");
	    	sourceObject     = %g;
   	    	sourceSlot       = 5;
      };
      %p.vector = %vec[%i];
      %p.count = 1;
      %p.MaxExplode = 15;
   }
}

//The evilness Begins Here
function DAVardisonAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   schedule(23500, 0, "DAVardisonAttacks", %boss);
   %attack = getRandom(1, 10);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "NMM", %target);
         }
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
         }
      case 3:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 2);
         }
      case 4:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            GOVDoFlameCano(%boss, %target);
         }
      case 5:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 4);
         }
      case 6:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            for(%i = 0; %i < 10; %i++) {
               %timeInt = %i * 200;
               schedule(%timeInt, 0, VardisonAttack, %boss, "ShadowBombLaunchAbove", %target SPC 4);
            }
         }
      case 7:
         %boss.setMoveState(true);
         %vS[0] = "10 10 0";
         %vS[1] = "-10 10 0";
         %vS[2] = "10 -10 0";
         %vS[3] = "-10 -10 0";
         for(%i = 0; %i < 4; %i++) {
            CreateDemon(vectorAdd(%boss.getPosition(), %vS[%i]));
         }
         %boss.schedule(5000, setMoveState, false);
      case 8:
         %boss.setMoveState(true);
         //four charge-up beams
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         //the actual attack
         for(%i = 0; %i < 50; %i++) {
            %timeAtt = 5000 + (%i *150);
            //--------------------------
            %vec = %boss.GetMuzzleVector(4);
            %pos = %boss.GetMuzzlePoint(4);
            schedule(%timeAtt, 0, VardisonAttack, %boss, "HyperspeedPlasmaBolt", %pos TAB %vec);
         }
         %boss.schedule(12500, setMoveState, false);
      case 9:
         VardisonAttack(%boss, "LinearFlameWall");
      case 10:
         VardisonAttack(%boss, "SeekingRapiers", %target);
   }
}

function VardisonAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   schedule(23500, 0, "VardisonAttacks", %boss);
   %attack = getRandom(1, 8);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "NMM", %target);
         }
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
         }
      case 3:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 2);
         }
      case 4:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            GOVDoFlameCano(%boss, %target);
         }
      case 5:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 4);
         }
      case 6:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            for(%i = 0; %i < 10; %i++) {
               %timeInt = %i * 200;
               schedule(%timeInt, 0, VardisonAttack, %boss, "ShadowBombLaunchAbove", %target SPC 4);
            }
         }
      case 7:
         %boss.setMoveState(true);
         %vS[0] = "10 10 0";
         %vS[1] = "-10 10 0";
         %vS[2] = "10 -10 0";
         %vS[3] = "-10 -10 0";
         for(%i = 0; %i < 4; %i++) {
            CreateDemon(vectorAdd(%boss.getPosition(), %vS[%i]));
         }
         %boss.schedule(5000, setMoveState, false);
      case 8:
         %boss.setMoveState(true);
         //four charge-up beams
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         //the actual attack
         for(%i = 0; %i < 50; %i++) {
            %timeAtt = 5000 + (%i *150);
            //--------------------------
            %vec = %boss.GetMuzzleVector(4);
            %pos = %boss.GetMuzzlePoint(4);
            schedule(%timeAtt, 0, VardisonAttack, %boss, "HyperspeedPlasmaBolt", %pos TAB %vec);
         }
         %boss.schedule(12500, setMoveState, false);
   }
}

function VardisonDroneAttacks(%boss) {
   if(!isObject(%boss)) {
      return;
   }
   schedule(10000, 0, "VardisonDroneAttacks", %boss);
   %attack = getRandom(1,3);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            VardisonAttack(%boss, "NMM", %target.player);
         }
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
         }
      case 3:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "SuperLaser", %target);
         }
   }
}

function VardisonDemonAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   //create a mini-demon
   CreateDemon(vectorAdd(%boss.getPosition(), getRandomPosition(10, 1)));
   //
   %boss.setMoveState(true);
   schedule(15000, 0, "VardisonDemonAttacks", %boss);
   %attack = getRandom(1,8);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
            schedule(2500, 0, VardisonAttack, %boss, "LBM", %target);
            schedule(3500, 0, VardisonAttack, %boss, "LBM", %target);
            schedule(5000, 0, VardisonAttack, %boss, "LBM", %target);
            schedule(5100, 0, VardisonAttack, %boss, "LBM", %target);
            %boss.schedule(5100, "SetMoveState", false);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I've got some missiles for you "@getTaggedString(%target.client.name)@".");
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            VardisonAttack(%boss, "NMM", %target.player);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": It's time to invoke darkness upon "@getTaggedString(%target.name)@".");
            %boss.schedule(1000, "SetMoveState", false);
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 3:
         setgravity(-1000);
         MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I'll disorient you all!");
         schedule(3000, 0, "SetGravity", 1000);
         schedule(7500, 0, "SetGravity", -20);
         %boss.schedule(7500, "SetMoveState", false);
         //%boss.InvokeLoop = InvokeStillwallLoop(%boss);
         schedule(7500, 0, "Cancel", %boss.InvokeLoop);
      case 4:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LaserDrop", %target);
            %boss.schedule(3000, "SetMoveState", false);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Your time has come "@getTaggedString(%target.client.name)@".");
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 5:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            for(%i = 0; %i < 25; %i++) {
               schedule(50+(%i*150), 0, VardisonAttack, %boss, "SuperLaser", %target);
            }
            %boss.schedule(10000, "SetMoveState", false);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": BLAAAAHAAHAHAHAAHA!!!");
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 6:
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %target = ClientGroup.getObject(%i);
            if(isObject(%target.player)) {
               VardisonAttack(%boss, "NMM", %target.player);
            }
         }
         MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": All must suffer!!!");
         %boss.schedule(1000, "SetMoveState", false);
         return;
      case 7:
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %target = ClientGroup.getObject(%i);
            if(isObject(%target.player)) {
               %target = %target.player;
               for(%l = 0; %l < 25; %l++) {
                  schedule(50+(%l*150), 0, VardisonAttack, %boss, "SuperLaser", %target);
               }
            }
         }
         MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Everyone DIES NOW!!!!");
         %boss.schedule(10000, "SetMoveState", false);
         return;
      case 8:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            for(%i = 0; %i < 15; %i++) {
               %time = %i * 150;
               %mType = getRandom(0, 1);
               switch(%mType) {
                  case 0:
                     schedule(%time, 0, VardisonAttack, %boss, "NMM", %target.player);
                  case 1:
                     schedule(%time, 0, VardisonAttack, %boss, "LBM", %target.player);
                  default:
                     schedule(%time, 0, VardisonAttack, %boss, "NMM", %target.player);
               }
            }
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": "@getTaggedString(%target.name)@" Will Feel the Power of My Missiles.");
            %boss.schedule(1000, "SetMoveState", false);
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
   }
}

function VardisonAttack(%boss, %att, %arg) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   switch$(%att) {
      case "ShadowBombDirect":
         %target = getWord(%arg, 0);
         %detCt = getWord(%arg, 1);
         if(!isObject(%target) || %target.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%target.getPosition(),%boss.getPosition()));
         %p = new LinearFlareProjectile() {
            dataBlock        = ShadowBomb;
            initialDirection = vectorScale(%vec, 10);
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         %p.maxExplode = %detCt;
         MissionCleanup.add(%p);
         
      case "ShadowBombLaunchAbove":
         %target = getWord(%arg, 0);
         %detCt = getWord(%arg, 1);
         if(!isObject(%target) || %target.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%target.getPosition(), vectorAdd(%boss.getPosition(), "0 0 35")));
         %p = new LinearFlareProjectile() {
            dataBlock        = ShadowBomb;
            initialDirection = vectorScale(%vec, 10);
            initialPosition  = vectorAdd(%boss.getPosition(), "0 0 35");
            sourceSlot       = 4;
         };
         %p.maxExplode = %detCt;
         %p.sourceObject = %boss;
         MissionCleanup.add(%p);
         
      case "HyperspeedPlasmaBolt":
         %boss.playShieldEffect("1 1 1");
         %pos = getField(%arg, 0);
         %dir = getField(%arg, 1);
         %p = new TracerProjectile() {
	        dataBlock        = PlasmaCannonMainProj;
            initialDirection = %dir;
	        initialPosition  = %pos;
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
         
      case "LaserDrop":
         %toDie = %arg;
         if(!isObject(%toDie) || %toDie.getState() $= "dead") {
            return;
         }
         %p = new LinearFlareProjectile() {
            dataBlock        = HyperDevestatorBeam;
            initialDirection = "0 0 -10";
            initialPosition  = vectoradd(%target.getPosition(), "0 0 500");
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
         
      case "SuperLaser":
         %toDie = %arg;
         if(!isObject(%toDie) || %toDie.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%toDie.getPosition(), %boss.getPosition()));
         %p = new LinearFlareProjectile() {
            dataBlock        = SuperlaserProjectile;
            initialDirection = %vec;
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
         
      case "NMM":
         %target = %arg;
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %boss.getPosition()));
         %p = new SeekerProjectile() {
            dataBlock        = YvexNightmareMissile;
            initialDirection = %vec;
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %target.getWorldBoxCenter();
         };
         %beacon.team = 0;
         %beacon.setTarget(0);
         MissionCleanup.add(%p);
         MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%target,%beacon,%p);
         
      case "LBM":
         %target = %arg;
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %boss.getPosition()));
         %p = new SeekerProjectile() {
            dataBlock        = VardisonLaserBallMissile;
            initialDirection = %vec;
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %target.getWorldBoxCenter();
         };
         %beacon.team = 0;
         %beacon.setTarget(0);
         MissionCleanup.add(%p);
         MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%target,%beacon,%p);
         
      case "LinearFlameWall":
         %fVec = %boss.getEyeVector();
         %fPos = %boss.getEyePosition();
         %lPos = vectorAdd(%fPos, vectorScale(%fVec, 10));
         %vec = vectorScale(%fVec, 24);
         //drop a line fire hire
         %p = new TracerProjectile() {
            dataBlock        = napalmSubExplosion;
            initialDirection = "0 0 -30";
            initialPosition  = vectorAdd(%lPos, "0 0 3");
            sourceSlot       = 5;
            maxCount = 15;
         };
         %p.sourceObject = %g;
         %p.vector = %vec;
         %p.count = 1;
         
      case "SeekingRapiers":
         %target = %arg;
         %iVec[0] = "1 0 0";
         %iVec[1] = "0 1 0";
         %iVec[2] = "-1 0 0";
         %iVec[3] = "0 -1 0";
         for(%i = 0; %i < 4; %i++) {
            createSeekingProjectile("RapierShieldForwardProjectile", "LinearFlareProjectile", %boss.getPosition(), %iVec[%i], %boss, %target, 3000);
         }
   }
}

function InvokeStillwallLoop(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   %boss.setVelocity("0 0 0");
   %boss.InvokeLoop = schedule(100, 0, "InvokeStillwallLoop", %boss);
}








//==============================================================================
datablock SeekerProjectileData(VardisonMiniDemonSpawner) : VardisonNightmareMissile {
   baseEmitter         = ShadowBaseEmitter;
};

function VardisonMiniDemonSpawner::OnExplode(%data, %proj, %pos, %mod) {
   //LaserBall
   %ball = CreateEmitter(%pos, "MiniShadowBallEmitter", "0 0 0 0");
   %ball.schedule(1000, "Delete");
   %Fire = CreateEmitter(%pos, "burnEmitter", "0 0 0 0");
   %Fire.schedule(2500, "Delete");
   CreateDemonAT(vectorAdd(%pos, "0 0 3"));
}

datablock PlayerData(MiniDemonArmor) : LightMaleHumanArmor {
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 2.8;
   minImpactSpeed = 1000;
   shapeFile = "bioderm_medium.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::PlasmaCannon] = 0.001;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock GrenadeProjectileData(MiniDemonBlaster) {
   projectileShapeName = "plasmabolt.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.45;
   damageRadius        = 5.0; // z0dd - ZOD, 8/13/02. Was 20.0
   radiusDamageType    = $DamageType::Demon;
   kickBackStrength    = 1500;

   explosion           = "MortarExplosion";
   underwaterExplosion = "MortarExplosion";
   velInheritFactor    = 0;
   splash              = PlasmaSplash;
   depthTolerance      = 100.0;

   baseEmitter         = DemonFBSmokeEmitter;
   bubbleEmitter       = DemonFBSmokeEmitter;

   grenadeElasticity = 0;
   grenadeFriction   = 0.4;
   armingDelayMS     = -1; // z0dd - ZOD, 4/14/02. Was 2000

   gravityMod        = 0.4;  // z0dd - ZOD, 5/18/02. Make mortar projectile heavier, less floaty
   muzzleVelocity    = 125.0; // z0dd - ZOD, 8/13/02. More velocity to compensate for higher gravity. Was 63.7
   drag              = 0;
   sound	     = PlasmaProjectileSound;

   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1 0.75 0.25";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "1 0.75 0.25";
};

function CreateDemon(%pos) {
   %p = new SeekerProjectile() {
      dataBlock        = VardisonMiniDemonSpawner;
      initialDirection = "0 0 -10";
      initialPosition  = vectorAdd(%pos, "0 0 500");
      //sourceObject     = %boss;
      //sourceSlot       = 4;
   };
}

function CreateDemonAT(%Pos) {
   %Demon = new player(){
      Datablock = "MiniDemonArmor";
   };
   %Demon.setTransform(%Pos);
   %Demon.type = 1;
   %Demon.canjump = 1;
   %Demon.hastarget = 1;
   %Demon.isBoss = 1;     //grant boss-like ability

   %Demon.team = 30;

   %Demon.target = createTarget(%Demon, "Shadow Warrior", "", "Derm3", '', %Demon.team, PlayerSensor);
   setTargetSensorData(%Demon.target, PlayerSensor);
   setTargetSensorGroup(%Demon.target, 30);
   setTargetName(%Demon.target, addtaggedstring("Shadow Warrior"));
   setTargetSkin(%Demon.target, 'Horde');

   MissionCleanup.add(%Demon);
   schedule(1000, %Demon, "MiniDemonMoveToTarget", %Demon);
}

function MiniDemonMoveToTarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   %Demon.counter++;
   if(%Demon.counter >= 5) {
      %Demon.counter = 0;
      %Demon.canFire = 1;
   }
   if(%closestDistance <= $Zombie::detectDist) {
      if(%Demon.hastarget != 1){
	     %Demon.hastarget = 1;
	   }
	   %upvec = "250";
	   %fmultiplier = $Zombie::FForwardSpeed;

       %vector = ZgetFacingDirection(%Demon,%closestClient,%pos);
       
	   %vector = vectorscale(%vector, %Fmultiplier);
	   %x = Getword(%vector,0);
	   %y = Getword(%vector,1);
	   %z = Getword(%vector,2);
	   if(%z >= "1200" && %Demon.canjump == 1){
	      %Demon.setvelocity("0 0 0");
	      %upvec = (%upvec * 8);
	      %x = (%x * 0.5);
	      %y = (%y * 0.5);
	      %Demon.canjump = 0;
	      schedule(2500, %Demon, "Zsetjump", %Demon);
	   }
    
       if(%Demon.canFire) {
          MiniDemonFire(%Demon, %closestclient);
       }

	   %vector = %x@" "@%y@" "@%upvec;
	   %Demon.applyImpulse(%pos, %vector);
   }
   else if(%Demon.hastarget == 1) {
      %Demon.hastarget = 0;
      %Demon.DemonRmove = schedule(100, %Demon, "ZSetRandomMove", %Demon);
   }
   %Demon.moveloop = schedule(500, %Demon, "MiniDemonMoveToTarget", %Demon);
}

function MiniDemonFire(%demon, %closestclient){
   %pos = %demon.getMuzzlePoint(4);
   %tpos = %closestclient.getWorldBoxCenter();
   %tvel = %closestclient.getvelocity();
   %vec = vectorsub(%tpos,%pos);
   %dist = vectorlen(%vec);
   %velpredict = vectorscale(%tvel,(%dist / 125));
   %vector = vectoradd(%vec,%velpredict);
   %ndist = vectorlen(%vector);
   %upvec = "0 0 "@((%ndist / 125) * (%ndist / 125) * 2);
   %vector = vectornormalize(vectoradd(%vector,%upvec));
   
      %p = new GrenadeProjectile() {
	      dataBlock        = MiniDemonBlaster;
	      initialDirection = %vector;
	      initialPosition  = %pos;
	      sourceObject     = %demon;
	      sourceSlot       = 4;
      };

   %demon.canFire = 0;
}
