$TWM2::ArmorHasCollisionFunction[LordZombieArmor] = false;

datablock AudioProfile(ZLordFootSound) {
	filename    = "fx/weapons/grenade_explode_UW.wav";
	description = AudioBomb3d;
	preload = true;
};

datablock AudioProfile(HZLordFootSound) {
	filename    = "fx/weapons/SpinFusor_explode_UW.wav";
	description = AudioBomb3d;
	preload = true;
};

datablock PlayerData(LordZombieArmor) : HeavyMaleBiodermArmor {
	shapefile = "TR2medium_male.dts";
	mass = 500;
	maxDamage = 12.5;
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

	damageScale[$DamageType::RP432] = 3.5;
	damageScale[$DamageType::MG42] = 4.0;
	damageScale[$DamageType::MRXX] = 4.5;
	damageScale[$DamageType::PTorpedo] = 10.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;
	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;	
	damageScale[$DamageType::AcidCannon] = 3.0;
	damageScale[$DamageType::deserteagle] = 2.5;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock ShapeBaseImageData(ZHead) {
	shapeFile = "bioderm_heavy.dts";
	emap = false;
	mountPoint = 1;
	offset = "0 0.25 -1.5";
	rotation = "1 0 0 15";
};

datablock ShapeBaseImageData(ZBack) {
	shapeFile = "bioderm_medium.dts";
	emap = false;
	mountPoint = 1;
	offset = "0 0.25 -1.25";
	rotation = "-1 0 0 10";
};

datablock ShapeBaseImageData(ZDummyslotImg) {
	shapeFile = "turret_muzzlepoint.dts";
	emap = false;
};

datablock ShapeBaseImageData(ZDummyslotImg2) {
	shapeFile = "turret_muzzlepoint.dts";
	emap = false;
	offset = "-1.5 0 0";
};

datablock ShapeBaseImageData(zLordPhotonCannonImg) {
	shapeFile = "turret_fusion_large.dts";
	emap = false;
	offset = "-1.5 0 1.0";
};

datablock ForceFieldBareData(ZombieLordDefensiveBarrier) {
	className = "forcefield";
	fadeMS = 1000;
	baseTranslucency = $noPassTrans;
	powerOffTranslucency = $noPassTrans / $dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.0 1.0 0.0";
	powerOffColor = "0.0 0.0 0.0";
	targetNameTag = 'Zombie Lord Defensive Barrier';
	targetTypeTag = 'ZLordDefensiveBarrier';
	texture[0] = "skins/forcef1";
	texture[1] = "skins/forcef2";
	texture[2] = "skins/forcef3";
	texture[3] = "skins/forcef4";
	texture[4] = "skins/forcef5";
	framesPerSec = 1; // 10
	numFrames = 5;
	scrollSpeed = 15;
	umapping = 0.01; // 1.0
	vmapping = 0.01; // 0.15
	needsPower = false;	
};

function ZombieLordDefensiveBarrier::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType) {
	//Apply the damage to the zombie lord's energy...
	%zombie = %targetObject.sourceZombie;
	%zombie.shieldHealth -= %amount;
	//Flash the zombie lord...
	%zombie.lastShieldDamageSource = %sourceObject;
	%zombie.playShieldEffect("1 1 1");
}

datablock LinearFlareProjectileData(ZLPhotonCannonProjectile) {
	scale               = "4 4 4";//6
	sound      = PlasmaProjectileSound;

	faceViewer          = true;
	directDamage        = 0.0;
	hasDamageRadius     = true;
	indirectDamage      = 2.5;
	damageRadius        = 10.0;
	kickBackStrength    = 10000;
	radiusDamageType    = $DamageType::Photon; //obviously change this

	explosion           = "PhotonMissileExplosion";
	underwaterExplosion = "PhotonMissileExplosion";
	splash              = BlasterSplash;

	dryVelocity       = 500.0;
	wetVelocity       = 500.0;
	velInheritFactor  = 0.6;
	fizzleTimeMS      = 10000;
	lifetimeMS        = 10500;
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


// Zombie Lord
// TWM2 3.9.2
//  - Old Behavior:
//   - Heavy anti-ground assault unit that would grab and punch enemy infantry
//   - Armed with an anti-infantry acid cannon weapon
//  - New Behavior:
//   - Heavy anti-tank unit. Still grabs enemy infantry.
//   - Has preferential targeting, will try to target any available enemy ground armor before infantry.
//   - Armed with a photon cannon to hit tanks hard.
//   - When not engaged with enemy armor, will try to attack infantry. Uses weapon a lot less, but will sometimes fire.
//   - The zombie lord may sometimes engage a energy barrier to prevent enemy projectile weapons from striking itself and nearby allies.

function LordZombieArmor::AI(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		if(isObject(%zombie.shieldObject)) {
			%zombie.shieldObject.delete();
		}
		return;
	}
	%zPos = %zombie.getPosition();
	//Are we currently in the firing weapon state?
	if(%zombie.firingWeapon) {
		%datablock.schedule(500, "AI", %zombie);
		return;
	}	
	//Check if we are currently shielding
	if(%zombie.usingShield) {
		//Is the shield broken?
		if(%zombie.shieldHealth <= 0) {
			//Yes...
			%zombie.shieldObject.delete();
			%zombie.usingShield = 0;
			if(isObject(%zombie.lastShieldDamageSource)) {
				%srcPlayer = %zombie.lastShieldDamageSource.sourceObject;
				if(isObject(%srcPlayer.client)) {
					CompleteNWChallenge(%srcPlayer.client, "ShieldBreaker");
				}
			}
			//Initialize the cooldown, note: a broken shield cooldown is 10 seconds longer than the non-broken cooldown
			TWM2Lib_Zombie_Core("setZFlag", %zombie, "canShield", 0);
			schedule(35000, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canShield", 1);			
		}
		%zombie.setActionThread($Zombie::RogThread, true);
		//Still going, reduce energy counter and check if we're out of energy
		%zombie.shieldEnergy--;
		if(%zombie.shieldEnergy <= 0) {
			%zombie.shieldObject.delete();
			%zombie.usingShield = 0;
			TWM2Lib_Zombie_Core("setZFlag", %zombie, "canShield", 0);
			schedule(25000, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canShield", 1);			
		}
		%datablock.schedule(500, "AI", %zombie);
		return;
	}
	//Am I engaged with something?
	if(%zombie.hasTarget) {
		if(!isObject(%zombie.targetedPlayer) || %zombie.targetedPlayer.getState() $= "dead") {
			%zombie.targetIsTank = 0;
			%zombie.targetedPlayer = 0;
			%zombie.hasTarget = 0;
			%zombie.movePoint = 0;
			%zombie.movingToPosition = 0;
			%datablock.schedule(500, "AI", %zombie);
			return;			
		}		
		//Is it a tank?
		%tPos = %zombie.targetedPlayer.getPosition();
		if(%zombie.targetIsTank) {
			//If I'm not close enough, move up a bit
			if(!%zombie.movingToPosition) {
				if(vectorDist(%zPos, %tPos) > 200) {
					//If I'm not close enough to the target, then I need to move into range...
					%direction = vectorNormalize(vectorSub(%zPos, %tPos));
					%zombie.movePoint = vectorScale(%direction, 150);
					%zombie.movingToPosition = true;
				}
				//If I'm in range, then I need to determine my course of action
				else {
					//Fire...
					if(%zombie.canFireWeapon) {
						//LOS?
						%direction = vectorNormalize(vectorSub(%tPos, %zPos));
						%dVec = vectorAdd(%zPos, vectorscale(%direction, 200));
						%rInfo = containerRaycast(%zPos, %tPos, $AllObjMask, %zombie);
						if(getWord(%rInfo, 0) == %zombie.targetedPlayer || getWord(%rInfo, 0) == %zombie.targetedPlayer.client.vehicleMounted) {
							//Target acquired... Fire
							%datablock.zFire(%zombie, %zombie.targetedPlayer.client.vehicleMounted);
						}
						else {
							//Something's in the way... let's try moving...
							%random = vectorAdd(%zPos, TWM2Lib_MainControl("getRandomPosition", "20\t1"));
							%zombie.movePoint = %random;
							%zombie.movingToPosition = true;
						}
					}
					else {
						//Shield?
						if(%zombie.canShield) {
							%datablock.makeShield(%zombie);
							%zombie.setActionThread($Zombie::RogThread, true);
						}
						else {
							//For now, I can't fire or shield, move up a bit...
							%datablock.move(%zombie);
						}
					}
				}
			}
			else {
				//I'm currently moving to the target position, so continue moving...
				%datablock.move(%zombie);
			}
		}
		//If it's not a tank, it's a player... which is a bit easier...
		else {
			//Check the distance to the target, if we're close enough, grab it...
			%tPos = %zombie.targetedPlayer.getPosition();
			%distance = vectorDist(%zPos, %tPos);
			%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.targetedPlayer.getPosition());
			if(%distance <= $Zombie::LordGrabDistance && %zombie.canjump == 1) {
				%vec = vectoradd(%pos,vectorScale(%vector,($Zombie::LordGrabDistance - 1.6)));
				%mask = $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::PlayerObjectType;
				%searchResult = containerRayCast(vectoradd(%pos,vectorScale(%vector,1.6)), %vec, %mask, %zombie);
				if(%searchResult) {
					%searchObj = getWord(%searchResult, 0);
					if(%searchObj $= %zombie.targetedPlayer) {
						%chance = getrandom(1,5);
						if(%chance == 1) {
							%dir = %zombie.getEyeVector();
							%dir = vectornormalize(getword(%dir, 0)@" "@getword(%dir, 1)@" 0");
							%dir = vectoradd(vectorscale(%dir, 7500),"0 0 1000");
							%zombie.targetedPlayer.applyimpulse(%clpos, %dir);
							%zombie.targetedPlayer.damage(0, %clpos, 0.8, $DamageType::ZombieL);
						}
						else {
							%zombie.setvelocity("0 0 0");
							TWM2Lib_Zombie_Core("setZFlag", %zombie, "canJump", 0);
							schedule($Zombie::BaseJumpCooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canJump", 1);
							%datablock.zLordLift(%zombie, %zombie.targetedPlayer, 0);
						}
					}
				}			
			}
			//Should I fire?
			if(%zombie.canFireWeapon && %distance > $Zombie::ZombieLordPhotonMinRange) {
				if(getRandom(1, 20) == 1) {
					//Fire!
					%datablock.zFire(%zombie, %zombie.targetedPlayer);
				}
			}
			//Should I shield?
			if(%zombie.canShield && %distance > 40) {
				if(getRandom(1, 35) == 1) {
					%datablock.makeShield(%zombie);
					%zombie.setActionThread($Zombie::RogThread, true);				
				}
			}
			%datablock.move(%zombie);
		}
	}
	else {
		//Scan for tanks
		%targetParams = TWM2Lib_Zombie_Core("lookForTarget", %zombie, true, true);
		%target = getWord(targetParams, 0);
		%distance = getWord(%targetParams, 1);	
		if(isObject(%target.player)) {
			//I see a tank, I will destroy that tank...
			if(%distance <= $zombie::detectDist) {
				%zombie.hasTarget = 1;
				%zombie.targetIsTank = true;
				%zombie.targetedPlayer = %target.player;
			}
		}
		//Scan for infantry
		%targetParams = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
		%target = getWord(targetParams, 0);
		%distance = getWord(%targetParams, 1);	
		if(isObject(%target.player)) {
			//Got a target player... let's go!
			if(%distance <= $zombie::detectDist) {
				%zombie.hasTarget = 1;
				%zombie.targetedPlayer = %target.player;
			}
		}
		//Nothing to hunt... random movement...
		if(!%zombie.hasTarget) {
			%zombie.zombieRmove = schedule(500, %zombie, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %zombie);
		}
	}
	%datablock.schedule(500, "AI", %zombie);
}

function LordZombieArmor::Move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}	
	%upvec = "150";
	%pos = %zombie.getWorldBoxCenter();
	%chance = (getrandom() * 20);
	%mult = 1;
	if(%chance >= 19) {
		serverPlay3d("ZombieMoan", %zombie.getWorldBoxCenter());	
	}
	if(%zombie.movingToPosition) {
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.movePoint);
	}
	else {
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.targetedPlayer.getPosition());
		if(vectorDist(%pos, %zombie.targetedPlayer.getPosition()) <= ($Zombie::LordGrabDistance + $Zombie::LungeDistance)) {
			%zombie.setvelocity("0 0 0");
			%mult = 2.5;
		}		
	}
	//Scale to speed
	%vector = vectorScale(%vector, %zombie.speed);
	%vector = vectorScale(%vector, %mult);
	%x = getWord(%vector, 0);
	%y = getWord(%vector, 1);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);	
}

function LordZombieArmor::zFire(%datablock, %zombie, %targetObject) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	if(!isObject(%targetObject)) {
		return;
	}
	TWM2Lib_Zombie_Core("setZFlag", %zombie, "firingWeapon", 1);
	TWM2Lib_Zombie_Core("setZFlag", %zombie, "canFireWeapon", 0);
	%vec = vectorsub(%targetObject.getWorldBoxCenter(), %zombie.getMuzzlePoint(7));
	%vec = vectoradd(%vec, vectorScale(%targetObject.getVelocity(), vectorlen(%vec)/100));
	%p = new LinearFlareProjectile() {
		dataBlock        = ZLPhotonCannonProjectile;
		initialDirection = %vec;
		initialPosition  = %zombie.getMuzzlePoint(7);
		sourceObject     = %zombie;
		sourceSlot       = 7;
	};	
	MissionCleanup.add(%p);
	schedule(2000, 0, "TWM2Lib_Zombie_Core", "setZFlag", %zombie, "firingWeapon", 0);
	%cooldown = $Zombie::ZombieLordPhotonCooldown;
	if(Game.CheckModifier("OhLordy") == 1) {
		%cooldown *= 0.5;
	}
	schedule(%cooldown, 0, "TWM2Lib_Zombie_Core", "setZFlag", %zombie, "canFireWeapon", 1);
}

function LordZombieArmor::makeShield(%datablock, %zombie) {
	if(isObject(%zombie) || %zombie.getState() $= "dead") {
		return false;
	}
	if(!%zombie.canShield) {
		return false;
	}
	%zPos = %zombie.getPosition();
	%eyePoint = posFromTransform(%zombie.getEyeTransform());
	%eVec = vectorNormalize(%zombie.getEyeVector());
	//Raycast out 25m from the eye line and make sure nothing's blocking us...
	%shieldPos = vectorAdd(%zPos, vectorScale(%eVec, 25));	
	%rInfo = containerRaycast(%zPos, %shieldPos, $AllObjMask, %zombie);
	if(getWord(%rInfo, 0) != 0 && isObject(getWord(%rInfo, 0))) {
		//Something is blocking our view, cancel shield.
		return false;
	}
	//Next, find the first object we hit below the point that can serve as the shield base.
	%mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType;
	%tH = getTerrainHeight(%shieldPos);
	%shieldSpawnPos = getWord(%shieldPos, 0) @ " " @ getWord(%shieldPos, 1) @ " " @ %tH;
	%lookVector = vectorSub(%shieldSpawnPos, %eyePoint);
	%searchResult = containerRayCast(%eyePoint, %lookVector, %mask, 0);
	if(getWord(%searchResult, 0) == 0) {
		//Nothing to mount to.
		return false;
	}
	//Fetch some important parameters
	%terrPt = posFromRaycast(%searchResult);
	%terrNrm = normalFromRaycast(%searchResult);
	%intAngle = getTerrainAngle(%terrNrm);  // getTerrainAngle() function found in staticShape.cs
	%rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 0 1"));
	if (getWord(%terrNrm, 2) == 1 || getWord(%terrNrm, 2) == -1) {
		%rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 1 0"));
	}
	%rotation = %rotAxis @ " " @ %intAngle;	
	//We've got it all, deploy...
	%zombie.usingShield = 1;
	%zombie.shieldHealth = $Zombie::ZombieLordShieldHealth;
	%zombie.shieldEnergy = $Zombie::ZombieLordShieldEnergy;
	TWM2Lib_Zombie_Core("setZFlag", %zombie, "canShield", 0);
	//Create the shield
	%shieldObject = new (ForceFieldBare)() {
		dataBlock = ZombieLordDefensiveBarrier;
		scale = "1 1 1";
		rotation = %rotation;
	};
	MissionCleanup.add(%shieldObject);	
	%zombie.shieldObject = %shieldObject;
	//Grow it over 3 seconds...
	%shieldObject.schedule(500, setRealSize, "3 1 3");
	%shieldObject.schedule(500, setTransform, %shieldObject.getTransform());
	%shieldObject.schedule(1000, setRealSize, "6 1 6");
	%shieldObject.schedule(1000, setTransform, %shieldObject.getTransform());	
	%shieldObject.schedule(1500, setRealSize, "8 1 8");
	%shieldObject.schedule(1500, setTransform, %shieldObject.getTransform());	
	%shieldObject.schedule(2000, setRealSize, "10 1 10");
	%shieldObject.schedule(2000, setTransform, %shieldObject.getTransform());		
	%shieldObject.schedule(2500, setRealSize, "12.5 1 12.5");
	%shieldObject.schedule(2500, setTransform, %shieldObject.getTransform());
	%shieldObject.schedule(3000, setRealSize, "15 1 15");
	%shieldObject.schedule(3000, setTransform, %shieldObject.getTransform());	
}

function LordZombieArmor::zLordLift(%datablock, %zombie, %target, %count) {
	if(%count == 0) {
		%zombie.setMoveState(true);
	}
	if(%target.getState() $= "dead" || %zombie.getState() $= "dead"){
		%zombie.setMoveState(false);
		return;
	}
	if(!isobject(%target)) {
		%zombie.setMoveState(false);
		return;
	}
	%pos = %zombie.getWorldBoxCenter();
	%Tpos = %target.getWorldBoxCenter();
	%ZtoT = vectorsub(%tpos, %pos);
	if (%count <= 12) {
		%newpos = vectoradd(%ZtoT, vectoradd(%pos, "0 0 -0.6"));
		%target.setTransform(%newpos);
		%target.setvelocity("0 0 0");
	}
	else {
		%killtype = getrandom(1, 2);
		if(%killtype == 1) {
			%closestwall = 20;
			%nv2 = (getword(%ZtoT, 0) * -1);
			%nv1 = getword(%ZtoT, 1);
			%vector1 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"), 20);
			%nvector1 = vectoradd(%tpos, %vector1);
			%nv2 = getword(%ZtoT, 0);
			%nv1 = (getword(%ZtoT, 1) * -1);
			%vector2 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"), 20);
			%nvector2 = vectoradd(%tpos, %vector2);
			%searchresultR = containerRayCast(%tpos, %nvector1, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
			%searchresultL = containerRayCast(%tpos, %nvector2, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
			if(%searchresultR) {
				%Hpos = getword(%searchresultR, 1)@" "@getword(%searchresultR, 2)@" "@getword(%searchresultR, 3);
				%distance = vectordist(%Tpos, %Hpos);
				if(%distance <= %closestwall){
					%closestwall = %distance;
					%vector = vectoradd(vectorscale(%vector1, 1500), "0 0 100");
				}
			}
			if(%searchresultL) {
				%Hpos = getword(%searchresultL, 1)@" "@getword(%searchresultL, 2)@" "@getword(%searchresultL, 3);
				%distance = vectordist(%Tpos, %Hpos);
				if(%distance <= %closestwall) {
					%closestwall = %distance;
					%vector = vectoradd(vectorscale(%vector2, 1500), "0 0 100");
				}
			}
			if(%closestwall == 20) {
				%x = getword(%ZtoT, 0);
				%y = getword(%ZtoT, 1);
				%outvec = vectorscale(vectornormalize(%x@" "@%y@" 0"), 50);
				%vector = vectoradd("0 0 -15000", %outvec);
			}
			%target.applyimpulse(%target.getposition(), %vector);
		}
		else if(%killtype == 2) {
			%target.infected = 1;
			%target.damage(0, %target.getposition(), 10.0, $DamageType::ZombieL);
		}
		%count = 0;
		%zombie.setMoveState(false);
		return;
	}
	%count++;
	%zombie.killingplayer = %datablock.schedule(150, "zLordLift", %zombie, %closestclient, %count);
}
