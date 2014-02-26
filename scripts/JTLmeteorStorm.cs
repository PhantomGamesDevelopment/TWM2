// JTLmeteorStorm.cs
//
// This script (C) 2002 by JackTL
//
// Use, modify, but give credit
//
// Functions:
//
// JTLMeteorStorm(obj,forcePlayer[1/0],maxRad,numFb,dropAlt,dropAltVariance,dbName,dbType,timeOutMS,randomRot[1/0],randomPulse[1/0],maxPulse,speedVec,offsetSpeedVec[1/0])
//

datablock ParticleData(JTLMeteorStormFireballParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = -0.2;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 700;
	lifetimeVarianceMS   = 0;

	textureName          = "particleTest";

	useInvAlpha = false;
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

	colors[0]     = "1.0 0.7 0.5 1.0";
	colors[1]     = "1.0 0.5 0.2 1.0";
	colors[2]     = "1.0 0.25 0.1 0.0";
	sizes[0]      = 40.0;
	sizes[1]      = 15.0;
	sizes[2]      = 7.5;
	times[0]      = 0.0;
	times[1]      = 0.2;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(JTLMeteorStormFireballEmitter) {
	ejectionPeriodMS = 5;
	periodVarianceMS = 1;

	ejectionVelocity = 0.25;
	velocityVariance = 0.0;

	thetaMin         = 0.0;
	thetaMax         = 30.0;

	particles = "JTLMeteorStormFireballParticle";
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------

datablock ParticleData(MetExplosionSmoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.30;   // rises slowly
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 3500;
   lifetimeVarianceMS   = 400;

   useInvAlpha =  true;
   spinRandomMin = -100.0;
   spinRandomMax =  100.0;

   textureName = "special/Smoke/bigSmoke";

   colors[0]     = "0.7 0.7 0.7 1.0";
   colors[1]     = "0.2 0.2 0.2 1.0";
   colors[2]     = "0.2 0.2 0.2 1.0";
   colors[3]     = "0.2 0.2 0.2 1.0";
   colors[4]     = "0.1 0.1 0.1 0.0";
   colors[5]     = "0.1 0.1 0.1 0.0";
   sizes[0]      = 15.0;
   sizes[1]      = 18.0;
   sizes[2]      = 21.0;
   sizes[3]      = 24.0;
   sizes[4]      = 27.0;
   sizes[5]      = 30.0;
   times[0]      = 0.0;
   times[1]      = 0.333;
   times[2]      = 0.666;
   times[3]      = 1.0;
   times[4]      = 1.333;
   times[5]      = 1.666;
};

datablock ParticleEmitterData(MetExplosionSmokeEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionOffset = 10.0;

   ejectionVelocity = 15.0;
   velocityVariance = 5.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 1500;

   particles = "MetExplosionSmoke";
};

datablock ShockwaveData(MetShockwave)
{
   width = 30.0;
   numSegments = 32;
   numVertSegments = 6;
   velocity = 500;
   acceleration = 100.0;
   lifetimeMS = 500;
   height = 12.0;
   verticalCurve = 0.5;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.4 1.0 0.4 0.50";
   colors[1] = "0.4 1.0 0.4 0.25";
   colors[2] = "0.4 1.0 0.4 0.0";

   mapToTerrain = true;
   orientToNormal = false;
   renderBottom = false;
};

datablock ExplosionData(MetSubExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   delayMS = 0;
   offset = 0.0;
   playSpeed = 0.15;

   sizes[0] = "35.0 35.0 35.0";
   sizes[1] = "35.0 35.0 35.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(MetExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;

   particleEmitter = MetExplosionSmokeEmitter;

   shockwave = MetShockwave;
   shockwaveOnTerrain = false;

   subExplosion[0] = MetSubExplosion;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "55.0 55.0 55.0";
   sizes[1] = "55.0 55.0 55.0";
   times[0] = 0.0;
   times[1] = 1.5;
};

//---------------------------------------
//   Projectile
//---------------------------------------

datablock GrenadeProjectileData(JTLMeteorStormFireball) {
	projectileShapeName  = "plasmabolt.dts";
	scale                = "40.0 40.0 40.0";
	emitterDelay         = -1;
	directDamage         = 0;
	directDamageType     = $DamageType::Meteor;
	hasDamageRadius      = true; // true;
	indirectDamage       = 5; // 0.5;
	damageRadius         = 150.0;
	radiusDamageType     = $DamageType::Meteor;
	kickBackStrength     = 5000;
	explosion            = MetExplosion;
	splash               = PlasmaSplash;
	baseEmitter          = JTLMeteorStormFireballEmitter;
	armingDelayMS        = 50;
	grenadeElasticity    = 0.15;
	grenadeFriction      = 0.4;
	drag                 = 0.1;
	gravityMod           = 0.0;
	sound                = GrenadeProjectileSound;

	hasLight    = true;
	lightRadius = 20.0;
	lightColor  = "1 1 0.5";
};

function JTLMeteorStormFireball::onExplode(%data,%proj,%pos,%mod) {
	// Ass-ume gravity does not change vec.. (gravityMod = 0.0) :P
	%vec = vectorNormalize(%proj.initialDirection);
	if (%data.hasDamageRadius)
		RadiusExplosion(%proj,vectorAdd(%pos,vectorScale(%vec,-0.1)),%data.damageRadius,%data.indirectDamage,%data.kickBackStrength,%proj.sourceObject,%data.radiusDamageType);
	%pPos = %proj.getPosition();
	%surface = containerRayCast(vectorAdd(%pPos,vectorScale(%vec,-0.1)),vectorAdd(%pPos,vectorScale(%vec,1)),-1,%proj);
	%tObj = firstWord(%surface);
	if (isObject(%tObj))
		%tObj.damage(%proj,%pPos,0.4,%proj.getDataBlock().directDamageType);
//	ionStormBeam(vectorAdd(%pPos,"0 0" SPC $IonStorm::Height));
}

function JTLMeteorStorm (%obj,%forcePlayer,%maxRad,%numFb,%dropAlt,%dropAltVariance,%dbName,%dbType,%timeOutMS,%randomRot,%randomPulse,%maxPulse,%speedVec,%offsetSpeedVec,%createFB,%pos,%target) {
	%pi = 3.1415926535897932384626433832795; // Whoa..
	if (%createFB) {
		if (%randomRot)
			%rot = "0 0 1" SPC getRandom() * (%pi * 2);
		else
			%rot = "1 0 0 0";
		%fb = new (%dbType) (JTLMeteor) {
			dataBlock        = %dbName;
			position         = %pos; // Needed for non-projectile types
			initialPosition  = %pos;
			initialDirection = %speedVec;
//			sourceObject     = 0;
			sourceSlot       = 0;
			vehicleObject    = 0;
		};
		if (isObject(%target) && $JTLMeteorStormSeek == 1 && %dbType $= "SeekerProjectile")
			%fb.setObjectTarget(%target);
		%fb.setRotation(%rot);
		if (%randomPulse) {
			%pulse = getRandom() * %maxPulse;
			%iPos = vectorNormalize((getRandom() * 2) - 1 SPC (getRandom() * 2) - 1 SPC (getRandom() * 2) - 1);
			%iPos = vectorAdd(%pos,%iPos);
			%iVec = vectorScale(vectorNormalize(getRandom() SPC getRandom() SPC getRandom()),%pulse);
// Fix this, not for projectiles
			%fb.applyImpulse(%iPos,%iVec);
		}
		if (%dbType $= "Item")
			%fb.setVelocity(%speedVec); // Needed for non-projectile types
		MissionCleanup.add(%fb);
		if (%timeOutMS) {
			%fb.schedule(%timeOutMS,setDamageState,Destroyed);
			%fb.schedule(%timeOutMS+1000,delete);
		}
		return;
	}

	if (%forcePlayer) {
		%obj = %obj.player;
	}
	else {
		if (%obj.getClassName() $= "GameConnection" || %obj.getClassName() $= "AIConnection") {
			%obj2 = %obj.getControlObject();
			if (isObject(%obj2))
				%obj = %obj2;
		}
	}

	if (isObject(%obj)) {
		if (%maxRad < 1)
			%maxRad = 50;
		if (%numFb < 1)
			%numFb = 100;
		if (%dropAlt < 1)
			%dropAlt = 100;
		if (%dropAltVariance < 1)
			%dropAltVariance = 500;
		if (!isObject(%dbName))
			%dbName = "JTLMeteorStormFireball";
		if (%dbType $= "" || %dbType $= "0")
			%dbType = "GrenadeProjectile";
		if (%speedVec $= "" || %speedVec $= "0")
			%speedVec = "0 0 -2";
		if (%maxPulse < 1)
			%maxPulse = 4000;
		%p = %obj.getWorldBoxCenter();
		%x = getWord(%p,0);
		%y = getWord(%p,1);
		%z = getWord(%p,2);
		for (%i = 0; %i < %numFb; %i++) {
			%dVec = getRandom() * %pi * 2;
			%dRad = getRandom() * %maxRad;
			%dX =mSin(%dVec) * %dRad;
			%dY =mCos(%dVec) * %dRad;
			%dZ =%dropAlt + (getRandom() * %dropAltVariance);
			if (%offsetSpeedVec) {
				%v2 = vectorCross(vectorNormalize(%speedVec),"1 0 0");
				%v3 = vectorCross(vectorNormalize(%speedVec),%v2);
				%dPos = vectorAdd(%p,vectorScale(vectorNormalize(%speedVec),-%dZ));
				%dPos = vectorAdd(%dPos,vectorScale(%v2,%dX));
				%dPos = vectorAdd(%dPos,vectorScale(%v3,%dY));
			}
			else {
				%dX = %x + %dX;
				%dY = %y + %dY;
				%dZ = %z + %dZ;
				%dPos = %dX SPC %dY SPC %dZ;
			}
			JTLMeteorStorm(0,0,0,0,0,0,%dbName,%dbType,%timeOutMS,%randomRot,%randomPulse,%maxPulse,%speedVec,0,true,%dPos,%obj);
		}
	}
	else {
		error("-JTLMeteorStorm- no valid object.");
		error("Usage: JTLMeteorStorm(obj,forcePlayer[1/0],maxRad,numFb,dropAlt,dropAltVariance,dbName,dbType,timeOutMS,randomRot[1/0],randomPulse[1/0],maxPulse,speedVec,offsetSpeedVec[1/0])");
	}
}
