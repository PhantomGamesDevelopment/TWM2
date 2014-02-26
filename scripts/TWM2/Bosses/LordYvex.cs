//BLOCK FUNCTIONS
function YvexNightmareMissile::OnExplode(%data, %proj, %pos, %mod) {
   %source = %proj.SourceObject;
   InitContainerRadiusSearch(%proj.getPosition(), 6, $TypeMasks::PlayerObjectType);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      %cl = %potentialTarget.client;
      if(%cl !$= "")
         Yvexnightmareloop(%source, %cl);
   }
}

function KillerPulse::onCollision(%data,%projectile,%targetObject,%modifier,%position,%normal) {
   if (%targetObject.getClassName() $= "Player" && %targetObject.isBoss) {
      messageall('msgkillcurse', "\c5"@getTaggedString(%targetObject.client.name)@" Took a fatal Hit from "@$TWM2::ZombieName[7]@"'s Dark Energy");
      %targetObject.throwWeapon();
      %targetObject.clearinventory();
      YvexAttack_FUNC("KillLoop", %targetObject);
   }
}

function YvexZombieMakerMissile::OnExplode(%data, %proj, %pos, %mod) {
   %c = CreateEmitter(%pos, NightmareGlobeEmitter, "0 0 1");
   %rand = getRandom(1, 6);
   %c.schedule(%rand * 750, "delete");
   for(%i = 0; %i < %rand; %i++) {
      %time = %i * 750;
      %type = getRandomZombieType("1 2 3 4 5 9 12 13");
      schedule(%time, 0, "StartAZombie", vectoradd(%pos, "0 0 1"), %type);
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
      %zombie.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
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
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   
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
         %type = getRandomZombieType("1 2 3 4 5 9 12 13");
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
            %pos = vectoradd(%z.getPosition(), getRandomPosition(10,1));
            %fpos = vectoradd("0 0 5",%pos);
            StartAZombie(%fpos, %type);
         }
         %z.setMoveState(true);
         %z.setActionThread($Zombie::RAAMThread, true);
         %z.schedule(3500, "setMoveState", false);
         
      case "FireCurse":
         MessageAll('msgWTFH', "\c4"@$TWM2::ZombieName[7]@": DIE!!!");
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
   if(%viewer.nightmareticks > 30) {
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
   %viewer.player.setWhiteout(1.8);
   %viewer.player.setDamageFlash(1.5);

   %zombie.playShieldEffect("1 1 1");
   serverPlay3D(NightmareScreamSound, %viewer.player.position);
   schedule(500,0,"Yvexnightmareloop",%zombie, %viewer);
   %viewer.player.damage(0, %viewer.player.position, 0.01, $DamageType::Zombie);
   %zombie.setDamageLevel(%zombie.getDamageLevel() - 0.1);

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
