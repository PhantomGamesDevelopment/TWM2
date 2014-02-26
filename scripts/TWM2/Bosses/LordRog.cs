function SpawnLordRog(%position) {
   %Zombie = new player(){
      Datablock = "LordRogZombieArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('MsgDarkraireturn', "\c4"@$TWM2::ZombieName[8]@": I AM ALIVE!!! I SHALL KILL YOU ALL");

   %zombie.iszombie = 1;
   StartLRAbilities(%zombie);
   schedule(5000, 0, LordRogAttack_FUNC, "ZombieSummon", %zombie);

   %command = "LordRogmovetotarget";

   %zombie.ticks = 0;
   InitiateBoss(%zombie, "LordRog");

   %Zombie.team = 30;
   %zname = $TWM2::ZombieName[8]; // <- To Hosts, Enjoy, You can
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

function LordRogmovetotarget(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %zombie.startFade(400, 0, true);
      %zombie.startFade(1000, 0, false);
      %zombie.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %zombie.setVelocity("0 0 0");
      MessageAll('msgDarkraiAttack', "\c4"@$TWM2::ZombieName[8]@": You think I will fall to my death!?!?");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
   
       //Sword Mount / Unmount
       if(%closestDistance < 12) {
          if(!%zombie.ImageMounted) {
             %zombie.ImageMounted = 1;
             %zombie.mountImage(BoVSwing, 7);
             ServerPlay3D(BlasterSwitchSound, %zombie.getPosition());
          }
       }
       else {
          %zombie.unMountImage(7);
          %zombie.ImageMounted = 0;
       }
   
	   if(%zombie.hastarget != 1){
          serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());
	      %zombie.hastarget = 1;
       }
       %chance = (getrandom() * 20);
   	   if(%chance >= 19)
          serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter());

       %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "LordRogmovetotarget", %zombie);
}

function LordRogAttack_FUNC(%att, %args) {
   switch$(%att) {
      case "ZombieSummon":
         %z = getWord(%args, 0);
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
         schedule(30000, 0, LordRogAttack_FUNC, "ZombieSummon", %z);
         //--------------------
         %type = getRandomZombieType("1 2 3 5 9 12 13 15 17");   //omit 4 in place of 17: Demon -> Elite Demon
         messageall('RogMsg',"\c4"@$TWM2::ZombieName[8]@": Attack my target!");
         for(%i = 0; %i < 5; %i++) {
            %pos = vectoradd(%z.getPosition(), getRandomPosition(10,1));
            %fpos = vectoradd("0 0 5",%pos);
            StartAZombie(%fpos, %type);
         }
         %z.setMoveState(true);
         %z.setActionThread($Zombie::RAAMThread, true);
         %z.schedule(3500, "setMoveState", false);
         
      case "DropshipReinforce":
         %z = getWord(%args, 0);
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
      
         %type = getRandomZombieType("1 2 3 5 9 12 13 15 17");
         MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Additional Reinforcements!!! NOW!");
         %typeCaller = %type SPC %type SPC %type SPC %type;
         %callPos = vectorAdd(%z.getPosition(), "2000 0 400");
         spawnHunterDropship(%callPos, "AA", %typeCaller);
      
      case "TwinLaunch":
         %z = getWord(%args, 0);
         %target = getWord(%args, 1);
         
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
         if(!isobject(%target) || %target.getState() $= "dead") {
            return;
         }
         
         %SPos1 = vectorAdd(%z.getPosition(), "1 0 0");
         %SPos2 = vectorAdd(%z.getPosition(), "-1 0 0");
         %p1 = new SeekerProjectile() {
            dataBlock        = LordRogStiloutte;
            initialDirection = "0 0 1";
            initialPosition  = %SPos1;
            sourceObject     = %z;
            sourceSlot       = 6;
         };
         %p2 = new SeekerProjectile() {
            dataBlock        = LordRogStiloutte;
            initialDirection = "0 0 1";
            initialPosition  = %SPos2;
            sourceObject     = %z;
            sourceSlot       = 6;
         };
         MissionCleanup.add(%p1);
         MissionCleanup.add(%p2);
         schedule(5000, 0, "WindshearAttack_FUNC", "MissileDrop", %target SPC %p1);
         schedule(5000, 0, "WindshearAttack_FUNC", "MissileDrop", %target SPC %p2);
      
      case "LaunchStorm":
         %z = getWord(%args, 0);
         %target = getWord(%args, 1);
         LordRogAttack_FUNC("TwinLaunch", %z SPC %target);
         schedule(1000, 0, LordRogAttack_FUNC, "TwinLaunch", %z SPC %target);
         schedule(2000, 0, LordRogAttack_FUNC, "TwinLaunch", %z SPC %target);
      
      case "LaserStrike":
         %z = getWord(%args, 0);
         %t = getWord(%args, 1);
         if(!isObject(%t) || %t.getState() $= "dead") {
            %z.Storming = 0;
            return;
         }
         if(!isObject(%z) || %z.getState() $= "dead") {
            return;
         }
         %z.laserStormSount++;
         if(%z.laserStormSount < 40) {
            %vec = vectorsub(%t.getworldboxcenter(), %z.getMuzzlePoint(6));
            %vec = vectoradd(%vec, vectorscale(%t.getvelocity(), vectorlen(%vec)/100));
            %p = new LinearFlareProjectile() {
               dataBlock        = LaserShot;
               initialDirection = %vec;
               initialPosition  = %z.getMuzzlePoint(6);
               sourceObject     = %z;
               sourceSlot       = 6;
            };
            MissionCleanup.add(%p);
         }
         else {
            %z.Storming = 0;
            return;
         }
         schedule(100, 0, LordRogAttack_FUNC, "LaserStrike", %z SPC %t);
      
      case "MetrosMaul":
         %t = getWord(%args, 0);
         %fpos = vectoradd(%t.getposition(), getRandomposition(50,0));
         %pos2 = vectoradd(%fpos, "0 0 700");
         schedule(500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(1000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(1500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
      
      case "MeteorOblivion":
         %t = getWord(%args, 0);
         %fpos = vectoradd(%t.getposition(), getRandomposition(50, 0));
         %pos2 = vectoradd(%fpos, "0 0 700");
         schedule(500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(1000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(1500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(2000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(2500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(3000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(3500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(4000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(4500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(5000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(5500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(6000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(6500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(7000, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
         schedule(7500, 0, spawnprojectile, JTLMeteorStormFireball, GrenadeProjectile, %pos2, "0 0 -10");
      
      case "StaticDischarge":
         %z = getWord(%args, 0);
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
         %z.setMoveState(true);
         %z.schedule(7000, setMoveState, false);
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         %c = createEmitter(%z.getPosition(), FlashLEmitter, "1 0 0");      //Rotate it
         %c.schedule(1000, delete);
         InitContainerRadiusSearch(%z.getPosition(), 200, %TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0){
            if (%potentialTarget.getPosition() != %z.getPosition() && !%potentialtarget.client.ignoredbyZombs) {
               %potentialTarget.staticTicks = 0;
               LordRogAttack_FUNC("SCDLoop", %potentialTarget);
            }
         }
      
      case "SCDLoop":
         %obj = getWord(%args, 0);
         if(!isobject(%obj) || %obj.getState() $= "dead") {
            return;
         }
         if(%obj.staticTicks > 15) {
            %obj.setMoveState(false);
            return;
         }
         %c = createEmitter(%obj.getPosition(), PBCExpEmitter, "1 0 0");      //Rotate it
         %c.schedule(1000, delete);
         %obj.setMoveState(true);
         %obj.staticTicks++;
         %obj.damage(0, %obj.getPosition(), 0.05, $DamageType::Zombie);
         schedule(1000, 0, LordRogAttack_FUNC, "SCDLoop", %obj);
   }
}

function StartLRAbilities(%zombie) {
   if(!isobject(%zombie) || %zombie.getState() $= "dead") {
      return;
   }
   %z = %zombie; //< Im lazy =-P
   %z.setVelocity("0 0 0");
   %rand = getRandom(1, 9);
   switch(%rand) {
      case 1:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Launch!");
            LordRogAttack_FUNC("TwinLaunch", %z SPC %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Fools, you cannot withstand my power!");
         }

      case 2:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            %z.laserStormSount = 0;
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Laser Storm Begin!");
            LordRogAttack_FUNC("LaserStrike", %z SPC %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Fools, you cannot withstand my power!");
         }

      case 3:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Metros Maul!");
            LordRogAttack_FUNC("MetrosMaul", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Fools, you cannot withstand my power!");
         }

      case 4:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Storm Begins!");
            LordRogAttack_FUNC("LaunchStorm", %z SPC %target.player);
         }
         
      case 5 or 6:
         LordRogAttack_FUNC("StaticDischarge", %z);
         MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Static Discharge!");

      case 7:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Metros EXTREMITY!!!!");
            LordRogAttack_FUNC("MeteorOblivion", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Fools, you cannot withstand my power!");
         }
         
      case 8:
         LordRogAttack_FUNC("DropshipReinforce", %z);

      default:
         LordRogAttack_FUNC("DropshipReinforce", %z);
   }
   schedule(30000, 0, "StartLRAbilities", %zombie);
}
