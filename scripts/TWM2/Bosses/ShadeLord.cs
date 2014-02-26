//SHADE LORD
function ShadeLordSword::OnExplode(%data, %proj, %pos, %mod) {
   %source = %proj.SourceObject;
   InitContainerRadiusSearch(%pos, 6, $TypeMasks::PlayerObjectType);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(%potentialTarget != %source) {
         serverPlay3D(BOVHitSound,%potentialTarget.getPosition());
         MessageAll('msgDeath', "\c0"@%potentialTarget.client.namebase@" was killed by the shadowy forces of death.");
         %potentialTarget.blowUp();
         %potentialTarget.scriptKill();
         createBlood(%potentialTarget);
         //===========================
         %potentialTarget.schedule(750, Blowup);
         schedule(750, 0, createBlood, %potentialTarget);
         %potentialTarget.schedule(1250, Blowup);
         schedule(1250, 0, createBlood, %potentialTarget);
         
         schedule(1250, 0, doReturnMissile, %potentialTarget, %source);
      }
   }
   
   if(isObject(%proj.targetedPlayer) && %proj.targetedPlayer.getState() !$= "dead") {
      %proj.targetedPlayer.setMoveState(false); //free to move.
   }
}

function doReturnMissile(%ini, %src) {
   %final = vectorAdd(%ini.getPosition(), vectorAdd(getRandomPosition(70, 1), "0 0 250"));
   %vec = vectorNormalize(vectorSub(%final, %ini.getPosition()));
   %p = new SeekerProjectile() {
      dataBlock        = ShadeLordSword;
      initialDirection = %vec;
      initialPosition  = %ini.getPosition();
   };
   %p.sourceObject = %src;
}

//
function SpawnShadeLord(%position) {
   %Boss = new player(){
      Datablock = "ShadeLordArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": Take your stand, and prepare to face your destined fate of death!");
   schedule(3000, 0, MessageAll, 'MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": And so it begins... Let's see how you face your fears...");
   schedule(3500, 0, shadeLordToggleCondition, %Boss, 1);

   %command = "shadelorddomove";

   %Boss.ticks = 0;
   InitiateBoss(%Boss, "ShadeLord");

   %Boss.team = 30;
   %zname = $TWM2::BossName["ShadeLord"]; // <- To Hosts, Enjoy, You can
                                      //Change the Zombie Names now!!!
                                      
   $ShadeLordBoss::AllowedNighttime = 1;
   %Boss.target = createTarget(%Boss, %zname, "", "Derm3", '', %Boss.team, PlayerSensor);
   setTargetSensorData(%Boss.target, PlayerSensor);
   setTargetSensorGroup(%Boss.target, 30);
   setTargetName(%Boss.target, addtaggedstring(%zname));
   setTargetSkin(%Boss.target, 'Horde');
   //
   %Boss.setTransform(%cpos);
   %Boss.canjump = 1;
   %Boss.hastarget = 1;
   MissionCleanup.add(%Boss);
   schedule(7500, %Boss, %command, %Boss);
}

function ShadeLordToggleCondition(%Boss, %on) {
   if(!isObject(%Boss) || %Boss.getState() $= "dead") {
      return;
   }
   cancel(%boss.attacks);
   if(%on) {
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      %Boss.setMoveState(true);
      %Boss.setActionThread("cel4",true);
      %Boss.schedule(3500, "SetMoveState", false);
      skyVeryDark();

      %boss.attacks = ShadeLordDarkAttacks(%Boss);
   }
   else {
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
      %Boss.setMoveState(true);
      %Boss.setActionThread("death1",true);
      %Boss.schedule(3000, "setActionThread", "cel4", true);
      %Boss.schedule(4500, "SetMoveState", false);
      skyDusk();
      
      cancel(%boss.antiSky);
      cancel(%boss.randomFX);
      
      %boss.antiSky = "";
      %boss.randomFX = "";
      %boss.attacks = schedule(4500, 0, "ShadeLordLightAttacks", %Boss);
   }
}

function ShadeStormAntiSky(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "Dead") {
      return;
   }
   if(!$ShadeLordBoss::AllowedNighttime) {
      return;
   }
   %killHeight = getWord(%boss.getPosition(), 2) + 50;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
         if(getWord(%cl.player.getPosition(), 2) >= %killHeight) {
            ShadeDropKill(%boss, %cl.player);
         }
      }
   }
   %boss.antiSky = schedule(2500, 0, "ShadeStormAntiSky", %boss);
}

function ShadeStormFX(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "Dead") {
      return;
   }
   %bPos = %boss.getPosition();
   %start1 = vectorAdd(%bPos, "300 -300 50");
   %go = vectorAdd(%bPos, "-300 300 50");
   %interval = 15;
   for(%i = 0; %i < 20; %i++) {
      %neg = %i % 2 == 0 ? 1 : -1;
      %start = vectorAdd(%start1, %neg*%interval*%i@" 0 0");
      %vec = vectorNormalize(vectorSub(%go,%start));
      %p = new SeekerProjectile() {
         dataBlock        = ShadeLordSword;
         initialDirection = %vec;
         initialPosition  = %start;
      };
      %p.sourceObject = %boss;
   }
   
   %boss.randomFX = schedule(getRandom(10000, 25000), 0, ShadeStormFX, %boss);
}

function ShadeLordDarkAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "Dead") {
      return;
   }

   if(isObject(%boss.dayCloak)) {
      %boss.dayCloak.delete();
   }

   if(%boss.randomFX $= "") {
      %boss.randomFX = ShadeStormFX(%boss);
   }
   if(%boss.antiSky $= "") {
      %boss.antiSky = ShadeStormAntiSky(%boss);
   }
   
   %attack = getRandom(1, 3);
   switch(%attack) {
      case 1:
         MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": SHALDORVAAAAAAAAAAAAAAH!!!!!!!");
         ShadeLordPerformScream(%boss);
      case 2:
         ShadeLordStormDescendAttack(%boss, 0);
         MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": Descend Mighty Shade Storm, Destroy all who dare oppose us!");
      case 3:
         %target = FindValidTarget(%z);
         if(isObject(%target.player)) {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["ShadeLord"]@": Come forth my shade, Destroy "@getTaggedString(%target.name)@"!");
            %boss.setMoveState(true);
            %boss.schedule(5000, setMoveState, false);
            %boss.setActionThread($Zombie::RogThread,true);
            //
            %bPos = %boss.getPosition();
            %start1 = vectorAdd(%bPos, "300 -300 50");
            %go = vectorAdd(%bPos, "-300 300 50");
            %interval = 15;
            for(%i = 0; %i < 20; %i++) {
               %neg = %i % 2 == 0 ? 1 : -1;
               %start = vectorAdd(%start1, %neg*%interval*%i@" 0 0");
               %vec = vectorNormalize(vectorSub(%go,%start));
               %p = new SeekerProjectile() {
                   dataBlock        = ShadeLordSword;
                   initialDirection = %vec;
                   initialPosition  = %start;
               };
               %p.sourceObject = %boss;
               %p.targetedPlayer = %target.player;
               %beacon = new BeaconObject() {
                  dataBlock = "SubBeacon";
                  beaconType = "vehicle";
                  position = %target.player.getWorldBoxCenter();
               };
               %beacon.team = 0;
               %beacon.setTarget(0);
               MissionCleanup.add(%beacon);
               %p.setObjectTarget(%beacon);
               DemonMotherMissileFollow(%target.player,%beacon,%p);
            }
            //
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["ShadeLord"]@": Hiding in death does not save you "@getTaggedString(%target.name)@"");
         }
   }
   
   %boss.attacks = schedule(25000, 0, "ShadeLordDarkAttacks", %boss);
}

function ShadeLordLightAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "Dead") {
      return;
   }

   if(isObject(%boss.shadeStorm)) {
      %boss.shadeStorm.delete();
   }
   if(!isObject(%boss) || !%boss.getState() $= "dead") {
      if(isObject(%boss.dayCloak)) {
         %boss.dayCloak.delete();
      }
      if(isObject(%boss.shadeStorm)) {
         %boss.shadeStorm.delete();
      }
      return;
   }

   %attack = getRandom(1, 1);
   switch(%attack) {
      case 1:
         MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": SHALDORVAAAAAAAAAAAAAAH!!!!!!!");
         ShadeLordPerformScream(%boss);
      case 2:
         MessageAll('MsgBossEvilness', "\c4"@$TWM2::BossName["ShadeLord"]@": Come forth, and return to me my power of the shadows!");
         ShadeLordBeginHealSequ(%boss, 0);
   }
   %boss.attacks = schedule(25000, 0, "ShadeLordLightAttacks", %boss);
}

///////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////
function ShadeLordBeginHealSequ(%boss, %count) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   
   if(%count == 0) {
      %boss.setMoveState(true);
      %boss.setPosition(vectorAdd(%boss.getPosition(), getRandomPosition(700, 1)));
      cancel(%boss.moveLoop);
   }
   if(%count < 25) {
      %boss.setDamageLevel(%boss.getDamageLevel() - 1.0);
      createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
   }
   else {
      %boss.schedule(3000, setMoveState, false);
      %boss.moveLoop = schedule(3000, %boss, "shadelorddomove", %boss);
      return;
   }
   schedule(200, 0, "ShadeLordBeginHealSequ", %boss, %count++);
}

function ShadeLordStormDescendAttack(%boss, %count) {
   if(%count == 0) {
      cancel(%boss.moveLoop);
      %boss.rapierShield = 1;
      %boss.setMoveState(true);
      //
      if(isObject(%boss.shadeStorm)) {
         %boss.shadeStorm.delete();
      }
   }
   else if(%count > 0 && %count <= 25) {
      %pos = "0 0 "@ 250 - (10 * %count);
      if(isObject(%boss.shadeStorm)) {
         %boss.shadeStorm.delete();
      }
      %boss.shadeStorm = new ParticleEmissionDummy(){
         position = vectoradd(%boss.getPosition(), %pos);
         dataBlock = "defaultEmissionDummy";
   	     emitter = "ShadeStormEmitter";            //ShadeStormEmitter
      };
      //%boss.shadeStorm.setPosition(vectorAdd(%boss.getPosition(), %pos));
   }
   else if(%count == 26) {
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
            ShadeDropKill(%boss, %cl.player);
         }
      }
      
      if(isObject(%boss.shadeStorm)) {
         %boss.shadeStorm.delete();
      }
      %boss.shadeStorm = new ParticleEmissionDummy(){
         position = vectoradd(%Demon.getPosition(), "0 0 0.5");
         dataBlock = "defaultEmissionDummy";
   	     emitter = "ShadeStormEmitter";            //ShadeStormEmitter
      };
   }
   else if(%count > 26 && %count < 40) {
      if(isObject(%boss.shadeStorm)) {
         %boss.shadeStorm.delete();
      }
      %boss.shadeStorm = new ParticleEmissionDummy(){
         position = vectoradd(%boss.getPosition(), "0 0 1.5");
         dataBlock = "defaultEmissionDummy";
   	     emitter = "ShadeStormEmitter";            //ShadeStormEmitter
      };
   }
   else if(%count == 40) {
      %boss.rapierShield = 0;
      %boss.setMoveState(false);
      %boss.moveLoop = schedule(3000, %boss, "shadelorddomove", %boss);
      
      //flash all
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
            %cl.player.setWhiteout(1.0);
         }
      }
      return;
   }
   
   %count++;
   schedule(300, 0, "ShadeLordStormDescendAttack", %boss, %count);
}

function ShadeLordPerformScream(%boss) {
   cancel(%boss.moveloop);
   %boss.setMoveState(true);
   %boss.schedule(5000, setMoveState, false);
   //create emitter
   %screamEmit = new ParticleEmissionDummy(){
      position = vectoradd(%boss.getPosition(),"0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "ShadeLordScreamEmitter";            //ShadeStormEmitter
   };
   %screamEmit.schedule(5000, "delete");
   
   //knock down and throw weapons in radius.
   %TargetSearchMask = $TypeMasks::PlayerObjectType;
   InitContainerRadiusSearch(%boss.getPosition(), 200, %TargetSearchMask);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(isSet(%potentialTarget.client)) {
         //throw guns, knock down.
         %potentialTarget.setActionThread("death1", true);
         %potentialTarget.throwweapon(1);
         %potentialTarget.throwweapon(0);
         %potentialTarget.setMoveState(true);
         %potentialTarget.schedule(3000, setMoveState, false);
      }
   }
   //
   
   %boss.moveLoop = schedule(5000, %boss, "shadelorddomove", %boss);
}


///////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////
function ShadeLordDoDeath(%boss) {
   %boss.RapierShield = 1;
   %boss.inDeath = 1;
   if(isObject(%boss.dayCloak)) {
      %boss.dayCloak.delete();
   }
   if(isObject(%boss.shadeStorm)) {
      %boss.shadeStorm.delete();
   }
   //set on fire
   %fire = new ParticleEmissionDummy(){
      position = vectoradd(%boss.getPosition(),"0 0 0.5");
      dataBlock = "defaultEmissionDummy";
   	  emitter = "BurnEmitter";
   };
   MissionCleanup.add(%fire);
   %fire.schedule(5000, delete);
   //
   %Boss.setMoveState(true);
   %boss.setActionThread("death1", true);
   %boss.schedule(5000, "blowup");
   %boss.schedule(5000, "scriptkill");
   schedule(4999, 0, eval, ""@%boss@".rapierShield = 0;");
   
   Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
   Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
   Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
   Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
   Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
   Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
}

function shadelorddomove(%Demon){
   if(!isobject(%Demon) || %Demon.getState() $= "Dead") {
      if(isObject(%Demon.dayCloak)) {
         %Demon.dayCloak.delete();
      }
      if(isObject(%Demon.shadeStorm)) {
         %Demon.shadeStorm.delete();
      }
      return;
   }
 
   if(%demon.getDamageLeftPct() < 0.005) {
      ShadeLordDoDeath(%Demon);
   }
 
   if(%demon.getDamageLeftPct() < 0.4) {
      if($ShadeLordBoss::AllowedNighttime == 1) {
         $ShadeLordBoss::AllowedNighttime = 0;
         ShadeLordToggleCondition(%Demon, 0);
         MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": No, You will not break the barrier of dark!");
      }
   }
   else {
      if($ShadeLordBoss::AllowedNighttime == 0) {
         $ShadeLordBoss::AllowedNighttime = 1;
         ShadeLordToggleCondition(%Demon, 1);
         MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": Awaken, mighty storm of shade, bring forth the doom of our foes!");
      }
   }

   if(isObject(%Demon.dayCloak) && !%Demon.inDeath) {
      %Demon.dayCloak.delete();
      %Demon.dayCloak = new ParticleEmissionDummy(){
         position = vectoradd(%Demon.getPosition(),"0 0 0.5");
         dataBlock = "defaultEmissionDummy";
   	     emitter = "dayCloakEmitter";            //ShadeStormEmitter
      };
      MissionCleanup.add(%Demon.dayCloak);
   }
   else {
      if($ShadeLordBoss::AllowedNighttime == 0) {
         %Demon.dayCloak = new ParticleEmissionDummy(){
            position = vectoradd(%Demon.getPosition(),"0 0 0.5");
            dataBlock = "defaultEmissionDummy";
   	        emitter = "dayCloakEmitter";            //ShadeStormEmitter
         };
      }
   }
   
   
   if(isObject(%Demon.shadeStorm)) {
      %Demon.shadeStorm.delete();
      %Demon.shadeStorm = new ParticleEmissionDummy(){
         position = vectoradd(%Demon.getPosition(),"0 0 250");
         dataBlock = "defaultEmissionDummy";
   	     emitter = "ShadeStormEmitter";            //ShadeStormEmitter
      };
   }
   else {
      if($ShadeLordBoss::AllowedNighttime == 1) {
         %Demon.shadeStorm = new ParticleEmissionDummy(){
            position = vectoradd(%Demon.getPosition(),"0 0 250");
            dataBlock = "defaultEmissionDummy";
   	        emitter = "ShadeStormEmitter";            //ShadeStormEmitter
         };
      }
   }
   
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["ShadeLord"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 10) {
          ShadeDropKill(%Demon, %closestClient);
          MessageAll('MsgVardison', "\c4"@$TWM2::BossName["ShadeLord"]@": Feel The Vengeance of the Shadows "@getTaggedString(%closestClient.client.name)@".");
          %closestClient.setMoveState(true);
          ShadeLordRandomTeleport(%Demon);
       }
	   if(%Demon.hastarget != 1){
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
   %Demon.moveloop = schedule(500, %Demon, "shadelorddomove", %Demon);
}

function ShadeDropKill(%boss, %target) {
   %incoming = vectorAdd(%target.getPosition(), vectorAdd(getRandomPosition(70, 1), "0 0 250"));
   %vec = vectorNormalize(vectorSub(%target.getPosition(),%incoming));
   %p = new SeekerProjectile() {
      dataBlock        = ShadeLordSword;
      initialDirection = %vec;
      initialPosition  = %incoming;
   };
   %p.sourceObject = %boss;
   %p.targetedPlayer = %target;
   %beacon = new BeaconObject() {
      dataBlock = "SubBeacon";
      beaconType = "vehicle";
      position = %target.getWorldBoxCenter();
   };
   %beacon.team = 0;
   %beacon.setTarget(0);
   MissionCleanup.add(%beacon);
   %p.setObjectTarget(%beacon);
   DemonMotherMissileFollow(%target,%beacon,%p);
}

function ShadeLordRandomTeleport(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   
   %newPosition = vectorAdd(%boss.getPosition(), getRandomPosition(150, 1));
   %boss.setPosition(%newPosition);
}
