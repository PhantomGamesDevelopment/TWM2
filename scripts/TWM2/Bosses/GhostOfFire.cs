//DBs
datablock ParticleData(GhostflameParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "particleTest";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "0 1 0 0.4";
   colors[1]     = "0 1 0 0.3";
   colors[2]     = "0 1 0 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(GhostflameEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "GhostflameParticle";
};

datablock LinearFlareProjectileData(GhostFlameboltMain)
{
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.05;
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "ThrowerExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = GhostflameEmitter;

   dryVelocity       = 50.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 250;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.18 0.03";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = FlamethrowerFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

datablock ParticleData(NapalmExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 450;
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "1 0 0";
   colors[1]     = "1 0 0";
   sizes[0]      = 0.5;
   sizes[1]      = 2;
};

datablock ParticleEmitterData(NapalmExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "NapalmExplosionParticle";
};

datablock ExplosionData(NapalmExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;
   particleEmitter = NapalmExplosionEmitter;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "3.0 3.0 3.0";
   sizes[1] = "3.0 3.0 3.0";
   times[0] = 0.0;
   times[1] = 1.5;
};

//--------------------------------------
//Napalm projectile
//--------------------------------------
datablock LinearProjectileData(NapalmShot)
{
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 20.0;
   radiusDamageType    = $DamageType::Plasma;
   kickBackStrength    = 3000;

   explosion           = "NapalmExplosion";
//   underwaterExplosion = "UnderwaterNapalmExplosion";
   velInheritFactor    = 0.5;
//   splash              = NapalmSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion

   baseEmitter         = MissileFireEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 2000;
   muzzleVelocity    = 63.7;
   drag              = 0.1;

   sound          = MortarProjectileSound;

   hasLight    = true;
   lightRadius = 4;
   lightColor  = "1.00 0.9 1.00";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "0.05 0.075 0.2";

   dryVelocity       = 90;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 2700;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

};

datablock PlayerData(GhostFireArmor) : MediumPlayerDamageProfile
{
   emap = true;

   className = Armor;
   shapeFile = "medium_male.dts";
   cameraMaxDist = 3;
   computeCRC = true;

   debrisShapeName = "debris_player.dts";
   debris = HumanRedPlayerDebris;

   canObserve = true;
   cmdCategory = "Clients";
   cmdIcon = CMDPlayerIcon;
   cmdMiniIconName = "commander/MiniIcons/com_player_grey";

   hudImageNameFriendly[0] = "gui/hud_playertriangle";
   hudImageNameEnemy[0] = "gui/hud_playertriangle_enemy";
   hudRenderModulated[0] = true;

   hudImageNameFriendly[1] = "commander/MiniIcons/com_flag_grey";
   hudImageNameEnemy[1] = "commander/MiniIcons/com_flag_grey";
   hudRenderModulated[1] = true;
   hudRenderAlways[1] = true;
   hudRenderCenter[1] = true;
   hudRenderDistance[1] = true;

   hudImageNameFriendly[2] = "commander/MiniIcons/com_flag_grey";
   hudImageNameEnemy[2] = "commander/MiniIcons/com_flag_grey";
   hudRenderModulated[2] = true;
   hudRenderAlways[2] = true;
   hudRenderCenter[2] = true;
   hudRenderDistance[2] = true;

   aiAvoidThis = true;

   minLookAngle = -1.5;
   maxLookAngle = 1.5;
   maxFreelookAngle = 3.0;

   mass = 70;
   drag = 0.3;
   maxdrag = 0.5;
   density = 10;
   maxDamage = 500.0;
   maxEnergy =  400;
   repairRate = 0.0053;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.456;
   jetForce = 21.22 * 230;
   underwaterJetForce = 25.22 * 130 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain = 4.0;
   underwaterJetEnergyDrain =  1.0;
   minJetEnergy = 10;
   maxJetHorizontalPercentage = 0.8;

   runForce = 60 * 150;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 18;
   maxBackwardSpeed = 18;
   maxSideSpeed = 18;

   maxUnderwaterForwardSpeed = 10.5;
   maxUnderwaterBackwardSpeed = 9.5;
   maxUnderwaterSideSpeed = 9.5;

   recoverDelay = 4;
   recoverRunForceScale = 0.7;

   // heat inc'ers and dec'ers
   heatDecayPerSec      = 1.0 / 5.0; // takes 4 seconds to clear heat sig.
   heatIncreasePerSec   = 1.0 / 2.0; // takes 3.0 seconds of constant jet to get full heat sig.

   jumpForce = 8.3 * 130;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpSurfaceAngle = 75;
   jumpDelay = 0;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 85;
   jumpSurfaceAngle = 85;

   minJumpSpeed = 25;
   maxJumpSpeed = 35;

   horizMaxSpeed = 70;
   horizResistSpeed = 28;
   horizResistFactor = 0.32;
   maxJetForwardSpeed = 18;

   upMaxSpeed = 80;
   upResistSpeed = 30;
   upResistFactor = 0.23;

   minImpactSpeed = 45;
   speedDamageScale = 0.006;

   jetSound = ArmorJetSound;
   wetJetSound = ArmorWetJetSound;

   jetEmitter = FlammerArmorJetEmitter; //Pyro jets
   jetEffect = HumanMediumArmorJetEffect;

   boundingBox = "1.45 1.45 2.4";
   pickupRadius = 0.75;

   // damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   //Foot Prints
   decalData   = MediumMaleFootprint;
   decalOffset = 0.35;

   footPuffEmitter = LightPuffEmitter;
   footPuffNumParts = 15;
   footPuffRadius = 0.25;

   dustEmitter = LiftoffDustEmitter;

   splash = PlayerSplash;
   splashVelocity = 4.0;
   splashAngle = 67.0;
   splashFreqMod = 300.0;
   splashVelEpsilon = 0.60;
   bubbleEmitTime = 0.4;
   splashEmitter[0] = PlayerFoamDropletsEmitter;
   splashEmitter[1] = PlayerFoamEmitter;
   splashEmitter[2] = PlayerBubbleEmitter;
   mediumSplashSoundVelocity = 10.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 5.0;

   footstepSplashHeight = 0.35;
   //Footstep Sounds
   LFootSoftSound       = LFootMediumSoftSound;
   RFootSoftSound       = RFootMediumSoftSound;
   LFootHardSound       = LFootMediumHardSound;
   RFootHardSound       = RFootMediumHardSound;
   LFootMetalSound      = LFootMediumMetalSound;
   RFootMetalSound      = RFootMediumMetalSound;
   LFootSnowSound       = LFootMediumSnowSound;
   RFootSnowSound       = RFootMediumSnowSound;
   LFootShallowSound    = LFootMediumShallowSplashSound;
   RFootShallowSound    = RFootMediumShallowSplashSound;
   LFootWadingSound     = LFootMediumWadingSound;
   RFootWadingSound     = RFootMediumWadingSound;
   LFootUnderwaterSound = LFootMediumUnderwaterSound;
   RFootUnderwaterSound = RFootMediumUnderwaterSound;
   LFootBubblesSound    = LFootMediumBubblesSound;
   RFootBubblesSound    = RFootMediumBubblesSound;
   movingBubblesSound   = ArmorMoveBubblesSound;
   waterBreathSound     = WaterBreathMaleSound;

   impactSoftSound      = ImpactMediumSoftSound;
   impactHardSound      = ImpactMediumHardSound;
   impactMetalSound     = ImpactMediumMetalSound;
   impactSnowSound      = ImpactMediumSnowSound;

   skiSoftSound         = SkiAllSoftSound;
   skiHardSound         = SkiAllHardSound;
   skiMetalSound        = SkiAllMetalSound;
   skiSnowSound         = SkiAllSnowSound;

   impactWaterEasy      = ImpactMediumWaterEasySound;
   impactWaterMedium    = ImpactMediumWaterMediumSound;
   impactWaterHard      = ImpactMediumWaterHardSound;

   groundImpactMinSpeed    = 10.0;
   groundImpactShakeFreq   = "4.0 4.0 4.0";
   groundImpactShakeAmp    = "1.0 1.0 1.0";
   groundImpactShakeDuration = 0.8;
   groundImpactShakeFalloff = 10.0;

   exitingWater         = ExitingWaterMediumSound;

   maxWeapons = 2;            // Max number of different weapons the player can have
   maxGrenades = 1;           // Max number of different grenades the player can have
   maxMines = 1;              // Max number of different mines the player can have

   damageScale[$DamageType::plasma] = 0.05;
   damageScale[$DamageType::Burn] = 0.05;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   // Inventory restrictions
    max[RepairKit]          = 4;
	max[Mine]			    = 0;
	max[ZapMine]			= 0;
	max[CrispMine]			= 5;
	max[flamerAmmoPack]		= 1;
	max[Deagle]				= 1;
	max[SPistol]			= 1;
	max[Pistol]				= 1;
	max[PistolAmmo]			= 10;
	max[Pistolclip]			= 8;
 	max[PulsePhaser]	    = 1;
	max[flamer]				= 1;
	max[flamerAmmo]			= 0;
	max[Napalm]				= 1;
	max[NapalmAmmo]			= 20;
	max[melee]				= 1;
	max[BOV]				= 1;
	max[SOmelee]			= 1;
	max[IncindinaryGrenade]	= 7;
    max[Beacon]             = 3;
   // -END

   observeParameters = "0.5 4.5 4.5";

   shieldEffectScale = "0.7 0.7 1.0";
};
//

function StartGhostFire(%pos) {
	%Ghost = new player(){
	   Datablock = "GhostFireArmor";
	};
   %Ghost.setTransform(%pos);
   %Ghost.team = 30;
   %Ghost.hastarget = 1;
   %Ghost.isGOF = 1;
   %Ghost.isBoss = 1;
   MissionCleanup.add(%Ghost);
   %Ghost.target = createTarget(%Ghost, ""@$TWM2::BossName["GoF"]@"", "", "Male3", '', %Ghost.team, PlayerSensor);
   setTargetSensorData(%Ghost.target, PlayerSensor);
   setTargetSensorGroup(%Ghost.target, 30);
   setTargetName(%Ghost.target, addtaggedstring($TWM2::BossName["GoF"]));
   
   GOFAttack_FUNC("ConsiderFlamethrower", %Ghost);
   GOFDoRandomAttacks(%ghost);
   
   InitiateBoss(%ghost, "GhostOfFire");

   schedule(500, 0, "GOFLookforTarget", %Ghost);
}

function GOFLookforTarget(%Ghost) {
   if(!isObject(%Ghost))
	return;
   if(%Ghost.getState() $= "dead")
	return;
   %pos = %Ghost.getposition();
   %wbpos = %Ghost.getworldboxcenter();
   %count = ClientGroup.getCount();
   %closestClient = -1;
   %closestDistance = 32767;
   for(%i = 0; %i < %count; %i++) {
	%cl = ClientGroup.getObject(%i);
	if(isObject(%cl.player)){
	   %testPos = %cl.player.getWorldBoxCenter();
	   %distance = vectorDist(%wbpos, %testPos);
	   if (%distance > 0 && %distance < %closestDistance) {
	   	%closestClient = %cl;
	   	%closestDistance = %distance;
	   }
	}
   }
   if(%closestClient) {
       GOFPerformMove(%Ghost,%closestClient,%closestDistance);
   }
   %Ghost.Targeting = schedule(500, %Ghost, "GOFLookforTarget", %Ghost);
}

function GOFPerformMove(%ghost,%closestClient,%closestDistance) {
   %ghost.TargetCL = %closestClient;
   %ghost.DistToTarg = %closestDistance;
   %zposition = %ghost.getPosition();
   %Zzaxis = getword(%zposition,2);
   if(%Zzaxis < $zombie::falldieheight) {
   %ghost.scriptkill($DamageType::Suicide);
   }
   %pos = %ghost.getworldboxcenter();
   %closestClient = %closestClient.Player;
   if(%closestDistance <= $zombie::detectDist){
	if(%ghost.hastarget != 1){
	   %ghost.hastarget = 1;
	}
 
    //target is coming in for an easy kill, lets tele
    if(%closestDistance < 15) {
       GOFAttack_FUNC("BurnTeleport", %ghost);
    }

	%clpos = %closestClient.getPosition();
      %vector = vectorNormalize(vectorSub(%clpos, %pos));
	%v1 = getword(%vector, 0);
	%v2 = getword(%vector, 1);
	%nv1 = %v2;
	%nv2 = (%v1 * -1);
	%none = 0;
	%vector2 = %nv1@" "@%nv2@" "@%none;
	%ghost.setRotation(fullrot("0 0 0",%vector2));

	%fmultiplier = $Zombie::ForwardSpeed;
	%vector = vectorscale(%vector, %Fmultiplier);
	%upvec = "150";

	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= 600)
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%ghost.applyImpulse(%pos, %vector);
   }
}


//ATTACKS
function GOFAttack_FUNC(%att, %args) {
   switch$(%att) {
      case "ConsiderFlamethrower":
         %g = getWord(%args, 0);
         if(!isObject(%g) || %g.getState() $= "dead") {
            return;
         }
         %target = %g.TargetCL;
         %distance = %g.DistToTarg;
         if(isObject(%target.player) && %target.player.getState() !$= "dead") {
            //We have a target, time to scan distance
            if(%distance <= 35) {
               //The idiot is in range, crisp em :)
               %vec = vectorsub(%target.player.getworldboxcenter(),vectorAdd(%g.getPosition(), "0 0 1"));
               %vec = vectoradd(%vec, vectorscale(%target.player.getvelocity(),vectorlen(%vec)/100));
               %p = new LinearFlareProjectile() {
                   dataBlock        = FlamethrowerBolt; //burn :)
                   initialDirection = %vec;
                   initialPosition  = vectorAdd(%g.getPosition(), "0 0 1");
                   sourceObject     = %g;
                   sourceSlot       = 0;
               };
               schedule(200, 0, "GOFAttack_FUNC", "ConsiderFlamethrower", %g);
            }
            else {
               schedule(750, 0, "GOFAttack_FUNC", "ConsiderFlamethrower", %g);
            }
         }
         //no target, lets use a longer loop
         else {
            schedule(750, 0, "GOFAttack_FUNC", "ConsiderFlamethrower", %g);
         }
         
      case "Fireball":
         %g = getWord(%args, 0);
         %t = getWord(%args, 1);
         if(!isObject(%t) || %t.getState() $= "dead") {
            return;
         }
         if(!isObject(%g) || %g.getState() $= "dead") {
            return;
         }
         %vec = vectorsub(%t.getworldboxcenter(), %g.getMuzzlePoint(0));
         %vec = vectoradd(%vec, vectorscale(%t.getvelocity(), vectorlen(%vec)/100));
         %p = new LinearProjectile() {
             dataBlock        = NapalmShot; //burn :)
             initialDirection = %vec;
             initialPosition  = %g.getMuzzlePoint(0);
             sourceObject     = %g;
             sourceSlot       = 0;
         };
         MissionCleanup.add(%p);
         
      case "FBSpiral":
         %g = getWord(%args, 0);
         %msDelay = getWord(%args, 1);
         if(!isObject(%g) || %g.getState() $= "dead") {
            return;
         }
         if(!isSet(%msDelay)) {
            %msDelay = 100;
         }
         %dir[0] = "1 0 0";
         %dir[1] = "1 1 0";
         %dir[2] = "0 1 0";
         %dir[3] = "-1 1 0";
         %dir[4] = "-1 0 0";
         %dir[5] = "-1 -1 0";
         %dir[6] = "0 -1 0";
         %dir[7] = "1 -1 0";
         //
         for(%i = 0; %i < 8; %i++) {
            %time = %i * %msDelay;
            schedule(%time, 0, spawnprojectile, NapalmShot, LinearProjectile, %g.getPosition(), %dir[%i], %g);
         }
         
      case "FireBlast":
         //CAREFUL!!! note the change from getWord to getField!!!
         %g = getField(%args, 0);
         %pos = getField(%args, 1);
         
         if(!isObject(%g) || %g.getState() $= "dead") {
            return;
         }
         %p = new TracerProjectile() {
            dataBlock        = napalmSubExplosion;
            initialDirection = "0 0 -10";
            initialPosition  = %pos;
            sourceObject     = %g;
            sourceSlot       = 5;
         };
         %p.vector = "0 0 -10";
         %p.count = 1;
         
      case "BurnTeleport":
         %g = getWord(%args, 0);
         if(!isObject(%g) || %g.getState() $= "dead") {
            return;
         }
         %cP = %g.getPosition();
         %nP = TWM2Lib_MainControl("getRandomPosition", 55 TAB 0);
         %nP2 = vectorAdd(%np, "0 0 100");
         %fP = vectorAdd(%cP, %nP2);
         GOFAttack_FUNC("FireBlast", %g TAB %cP);
         %g.setTransform(%fP);
         messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Experience the glorious flames..");
         
      case "Flamecano":
         %g = getWord(%args, 0);
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
            %p = new TracerProjectile() {
               dataBlock        = napalmSubExplosion;
               initialDirection = "0 0 -30";
               initialPosition  = vectorAdd(%g.getPosition(), "0 0 -3");
               sourceObject     = %g;
               sourceSlot       = 5;
            };
            %p.maxCount = 5;
            %p.vector = %vec[%i];
            %p.count = 1;
         }

      case "Ultracano":
         %g = getWord(%args, 0);
         %TPos = getWords(%args, 1);
      
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
            %p = new TracerProjectile() {
               dataBlock        = napalmSubExplosion;
               initialDirection = "0 0 -30";
               initialPosition  = vectorAdd(%TPos, "0 0 3");
               sourceSlot       = 5;
               maxCount = 15;
            };
            %p.sourceObject = %g;
            %p.vector = %vec[%i];
            %p.count = 1;
         }

      case "LaunchSeekfire":
         %g = getWord(%args, 0);
         %t = getWord(%args, 1);
         %stDelay = getWord(%args, 2);
         
         %proj = createSeekingProjectile("GhostFlameboltMain", "LinearFlareProjectile", VectorAdd(%g.getPosition(), "0 0 5"), "0 0 1", %g, %t, %stDelay);
   }
}

function GOFDoFlameCano(%g, %target) {
   if(!isObject(%g) || %g.getState() $= "dead") {
      return;
   }
   %g.setPosition(VectorAdd(%target.player.getPosition(), "0 0 70"));
   %Pad = new StaticShape() {
      dataBlock = DeployedSpine;
      scale = ".1 .1 1";
      position = VectorAdd(%target.player.getPosition(), "0 0 69");
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
   schedule(2500, 0, "GOFAttack_FUNC", "Flamecano", %g);
}

function GOFDoRandomAttacks(%g) {
   if(!isObject(%g) || %g.getState() $= "dead") {
      return;
   }
   %rand = getRandom(1,15);
   %target = FindValidTarget(%g);
   switch(%rand) {
      case 1:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("FireBlast", %g TAB %target.player.getPosition());
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Time to heat things up "@getTaggedString(%target.name)@".");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Frightened of my fire? Good.");
         }
      case 2:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("Fireball", %g SPC %target.player);
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Hey "@getTaggedString(%target.name)@", Dodge this!");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Frightened of this? Good.");
         }
      case 3:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("Fireball", %g SPC %target.player);
            schedule(400, 0, GOFAttack_FUNC, "Fireball", %g SPC %target.player);
            schedule(800, 0, GOFAttack_FUNC, "Fireball", %g SPC %target.player);
            schedule(1200, 0, GOFAttack_FUNC, "Fireball", %g SPC %target.player);
            schedule(1600, 0, GOFAttack_FUNC, "Fireball", %g SPC %target.player);
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": I have a Flame Storm cooked up nicely for "@getTaggedString(%target.name)@", enjoy...");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": I love Fire.. it's Good your scared.");
         }
      case 4:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("FireBlast", %g TAB vectorAdd(%target.player.getPosition(), "0 0 25"));
            schedule(400, 0, GOFAttack_FUNC, "FireBlast", %g TAB vectorAdd(%target.player.getPosition(), "0 0 30"));
            schedule(800, 0, GOFAttack_FUNC, "FireBlast", %g TAB vectorAdd(%target.player.getPosition(), "0 0 35"));
            schedule(1200, 0, GOFAttack_FUNC, "FireBlast", %g TAB vectorAdd(%target.player.getPosition(), "0 0 40"));
            schedule(1600, 0, GOFAttack_FUNC, "FireBlast", %g TAB vectorAdd(%target.player.getPosition(), "0 0 45"));
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Hear that, it's the sound of exploding fire ending your life...");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Frightened Of Fire? Good.");
         }
      case 5:
         if(isObject(%target.player)) {
            GOFDoFlameCano(%g, %target);
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": I Intend Every Moment... FLAMECANO!");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Oh Well, The Volcanic Explosion Can Wait.");
         }
      case 6:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 1);
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 1500);
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 3000);
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Clensic Flames Will Persue You "@getTaggedString(%target.name)@"!");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Darn, I Love Cursed Fire.");
         }
      case 7:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 1);
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 1500);
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 3000);
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 4500);
            GOFAttack_FUNC("LaunchSeekfire", %g SPC %target.player SPC 6000);
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": "@getTaggedString(%target.name)@", I present to you the gift, of cursed flames...");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Darn, I Love Mega Cursed Fire.");
         }
	  case 8:
         if(isObject(%target.player)) {
            GOFAttack_FUNC("Fireball", %g SPC %target.player);
            for(%i = 1; %i <= 9; %i++) {
               %time = %i * 100;
               schedule(%time, 0, GOFAttack_FUNC, "Fireball", %g SPC %target.player);
            }
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Lets unleash the fireballs upon "@getTaggedString(%target.name)@"!!!");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": I love Fire.. it's Good your scared.");
         }	
      case 9:
         GOFAttack_FUNC("FBSpiral", %g SPC 200);
         messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Let the flaming spinner begin!");
      case 10:
         GOFAttack_FUNC("FBSpiral", %g SPC 200);
         schedule(1000, 0, GOFAttack_FUNC, "FBSpiral", %g SPC 200);
         schedule(2000, 0, GOFAttack_FUNC, "FBSpiral", %g SPC 200);
         schedule(3000, 0, GOFAttack_FUNC, "FBSpiral", %g SPC 200);
         schedule(4000, 0, GOFAttack_FUNC, "FBSpiral", %g SPC 200);
         messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": CLENSE ALL!");
      case 11:
         GOFAttack_FUNC("Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(3000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(6000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(9000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": ERUPT!!! MY VOLCANO!!!!");
      case 12:
         GOFAttack_FUNC("Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(3000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(6000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(9000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(12000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(15000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(18000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         schedule(20000, 0, GOFAttack_FUNC, "Ultracano", %g SPC "0 0" SPC getTerrainHeight("0 0 0"));
         messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": YES YES YES!!!! SUPERVOLCANO GO!!!!!");
      case 13 or 14 or 15:
         if(isObject(%target.player)) {
            %store = %target.player.getPosition();
            GOFAttack_FUNC("Ultracano", %g SPC %store);
            schedule(3000, 0, GOFAttack_FUNC, "Ultracano", %g SPC %store);
            schedule(6000, 0, GOFAttack_FUNC, "Ultracano", %g SPC %store);
            schedule(9000, 0, GOFAttack_FUNC, "Ultracano", %g SPC %store);
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Rise Mt. Death... Cleanse "@getTaggedString(%target.name)@"!");
         }
         else {
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Mt Death can await....");
         }
   }
   schedule(25000,0,"GOFDoRandomAttacks", %g);
}
