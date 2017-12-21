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
function YvexNightmareMissile::OnExplode(%data, %proj, %pos, %mod) {
	%source = %proj.SourceObject;
	InitContainerRadiusSearch(%proj.getPosition(), 6, $TypeMasks::PlayerObjectType);
	while ((%potentialTarget = ContainerSearchNext()) != 0) {
		%cl = %potentialTarget.client;
		if(%cl !$= "") {
			Yvexnightmareloop(%source, %cl);
		}
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

	damageScale[$DamageType::M1700] = 3.0;
	damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
};

//CREATION
function SpawnYvex(%position) {
	%Zombie = new player(){
		Datablock = "YvexZombieArmor";
	};
	%Cpos = vectorAdd(%position, "0 0 5");
	MessageAll('MsgYvexreturn', "\c4"@$TWM2::BossNameInternal["Yvex"]@": Did you miss me? Because... I WANT MY REVENGE!!!");

	%command = "Yvexmovetotarget";
	%zombie.ticks = 0;
	InitiateBoss(%zombie, "Yvex");

	YvexAttack_FUNC("ZombieSummon", %zombie);
	YvexAttacks(%zombie);

	%Zombie.team = 30;
	%zname = $TWM2::BossName["Yvex"]; // <- To Hosts, Enjoy, You can
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
		MessageAll('msgYvexAttack', "\c4"@$TWM2::BossNameInternal["Yvex"]@": I shall not fall to my end!");
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
	else if(%zombie.hastarget == 1) {
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
               messageall('YvexMsg',"\c4"@$TWM2::BossNameInternal["Yvex"]@": Enlisted for revenge... ATTACK");
            case 2:
               messageall('YvexMsg',"\c4"@$TWM2::BossNameInternal["Yvex"]@": Attack my soldiers.. REVENGE is ours");
            case 3:
               messageall('YvexMsg',"\c4"@$TWM2::BossNameInternal["Yvex"]@": Take out the enemy, ALL OF THEM!");
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
         MessageAll('msgWTFH', "\c4"@$TWM2::BossNameInternal["Yvex"]@": DIE!!!");
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
   	     createMissileSeekingProjectile("YvexZombieMakerMissile", %t, %z, %z.getMuzzlePoint(4), %vec, 4, 100);
      
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
   	     createMissileSeekingProjectile("YvexNightmareMissile", %t, %z, %z.getMuzzlePoint(4), %vec, 4, 100);
      
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
         MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Yvex"]@": Let the revenge begin, "@getTaggedString(%targ.name)@".");
      case 2:
         MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Yvex"]@": Taste my vengance... "@getTaggedString(%targ.name)@".");
      case 3:
         MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Yvex"]@": Sleep Forever... "@getTaggedString(%targ.name)@".");
      default:
         MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Yvex"]@": This Nightmare will lock you forever "@getTaggedString(%targ.name)@"!");
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

   BottomPrint(%viewer,"You are locked in "@$TWM2::BossNameInternal["Yvex"]@"'s Nightmare.",5,1);
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
      messageall('msgkillcurse', "\c5"@getTaggedString(%targetObject.client.name)@" Took a fatal Hit from "@$TWM2::BossNameInternal["Yvex"]@"'s Dark Energy");
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
