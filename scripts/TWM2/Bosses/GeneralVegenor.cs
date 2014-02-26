function SpawnVegenor(%position) {
   %Zombie = new player(){
      Datablock = "VegenorZombieArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["Vegenor"]@": Time to engage the enemy soldiers!!!");

   %command = "Vegenormovetotarget";
   %zombie.ticks = 0;
   InitiateBoss(%zombie, "Vengenor");
   VegenorAttack_FUNC("Summon", %zombie);
   VegenorAttack(%zombie);

   %Zombie.team = 30;
   %zname = $TWM2::BossName["Vegenor"]; // <- To Hosts, Enjoy, You can
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

function Vegenormovetotarget(%zombie){
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
      MessageAll('msgAntiFall', "\c4"@$TWM2::BossName["Vegenor"]@": Fuck Falling!!!!");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
       //Random Repeling
       if(%closestDistance < 25) {
          %zombie.playShieldEffect("1 1 1");
          %RepChance = getRandom(1, 5);
          if(%RepChance == 3) {
             RepelZombie(%closestClient, %zombie);
          }
          //FIAH!!!
          else {
             %vec = vectorsub(%closestClient.getworldboxcenter(),vectorAdd(%zombie.getPosition(), "0 0 1"));
             %vec = vectoradd(%vec, vectorscale(%closestClient.getvelocity(),vectorlen(%vec)/100));
             %p = new LinearFlareProjectile() {
                dataBlock        = FlamethrowerBolt; //burn :)
                initialDirection = %vec;
                initialPosition  = vectorAdd(%zombie.getPosition(), "0 0 1");
                sourceObject     = %zombie;
                sourceSlot       = 0;
             };
          }
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
   %zombie.moveloop = schedule(500, %zombie, "Vegenormovetotarget", %zombie);
}

function VegenorAttack_FUNC(%att, %args) {
   switch$(%att) {
      case "Summon":
         %z = getWord(%args, 0);
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
         //schedule the next one
         schedule(30000, 0, "VegenorAttack_FUNC", "Summon", %z);
         //--------------------
         %type = getRandomZombieType("1 2 3 4 5 9 12 13 14");
         %msg = getrandom(1,2);
         switch(%msg) {
            case 1:
               messageall('MsgSummon',"\c4"@$TWM2::BossName["Vegenor"]@": Attack the enemy");
            case 2:
               messageall('MsgSummon',"\c4"@$TWM2::BossName["Vegenor"]@": Hunt them all down");
         }
         for(%i = 0; %i < 6; %i++) {
            %pos = vectoradd(%z.getPosition(), getRandomPosition(10,1));
            %fpos = vectoradd("0 0 5",%pos);
            StartAZombie(%fpos, %type);
         }
         %z.setMoveState(true);
         %z.setActionThread($Zombie::RAAMThread, true);
         %z.schedule(3500, "setMoveState", false);
      case "SetFire":
         %t = getWord(%args, 0);
         if(%t.maxfirecount $= "") {
            %t.maxfirecount = 0;
         }
         %t.maxfirecount = %t.maxfirecount + (30 * (0.05/0.2));
         if(%t.onfire == 0 || %t.onfire $= ""){
            %t.onfire = 1;
            schedule(10, %t, "burnloop", %t);
         }
      case "FlameMissileSingle":
         %Tobj = getWord(%args, 0);
         %vec = vectorNormalize(vectorSub(%Tobj.getPosition(), %z.getPosition()));
         %p = new SeekerProjectile() {
            dataBlock        = VegenorFireMissile;
            initialDirection = %vec;
            initialPosition  = %z.getMuzzlePoint(4);
            sourceObject     = %z;
            sourceSlot       = 4;
         };
         %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %Tobj.getWorldBoxCenter();
         };
         %beacon.team = 0;
         %beacon.setTarget(0);
         MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%Tobj, %beacon,%p);
      case "MeteorDrop":
         %t = getWord(%args, 0);
         %fpos = vectoradd(%t.getposition(), getRandomposition(50, 0));
         %pos2 = vectoradd(%fpos, "0 0 700");
         schedule(500, 0, spawnprojectile, VegenorFireMeteor, GrenadeProjectile, %pos2, "0 0 -10");
   }
}

function VegenorAttack(%z) {
   if(!isobject(%z) || %z.getState() $= "dead") {
      return;
   }
   schedule(30000, 0, "VegenorAttack", %z);
   %z.setVelocity("0 0 0");
   //Attacks
   %AttackNum = getRandom(1, 4);
   switch(%AttackNum) {
      case 1:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": Flame on "@getTaggedString(%target.name)@"!");
            VegenorAttack_FUNC("SetFire", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": heh, "@getTaggedString(%target.name)@" is already dead!");
         }
      case 2:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": Lets insert some fire into your life, "@getTaggedString(%target.name)@"!");
            VegenorAttack_FUNC("FlameMissileSingle", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": Hiding from me "@getTaggedString(%target.name)@"?");
         }
      case 3:
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": Fire Missiles For ALL!!");
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %cl = ClientGroup.getObject(%i);
            if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
               VegenorAttack_FUNC("FlameMissileSingle", %cl.player);
            }
         }
      case 4:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": Hey "@getTaggedString(%target.name)@", LOOK UP!!!");
            VegenorAttack_FUNC("MeteorDrop", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": Hiding does not beat me "@getTaggedString(%target.name)@"!!!");
         }
      default:
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Vegenor"]@": I shall wait!!!");
   }
}
