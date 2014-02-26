//DBS


//CREATION

function SpawnGhostOfLightning(%position) {
	%Ghost = new player(){
	   Datablock = "LightningGhostArmor";
	};
	%Cpos = vectorAdd(%position, "0 0 5");
    MessageAll('MsgDarkraireturn', "\c4"@$TWM2::BossName["GoL"]@": It's time to show you the shocking power of electricity");

	%command = "GoLmovetotarget";
    InitiateBoss(%Ghost, "GhostOfLightning");

   %Ghost.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["GoL"]@"");
   DoGoLAttacks(%ghost);

   %Ghost.target = createTarget(%Ghost, %zname, "", "Derm3", '', %Ghost.team, PlayerSensor);
   setTargetSensorData(%Ghost.target, PlayerSensor);
   setTargetSensorGroup(%Ghost.target, 30);
   setTargetName(%Ghost.target, addtaggedstring(%zname));
   setTargetSkin(%Ghost.target, 'Horde');
   //
   %Ghost.type = %type;
   %Ghost.setTransform(%cpos);
   %Ghost.canjump = 1;
   %Ghost.hastarget = 1;
   %Ghost.isGhost = 1;
   MissionCleanup.add(%Ghost);
   schedule(1000, %Ghost, %command, %Ghost);
}

function GoLmovetotarget(%Ghost){
   if(!isobject(%Ghost))
	return;
   if(%Ghost.getState() $= "dead")
	return;
   %pos = %Ghost.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Ghost);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Ghost.startFade(400, 0, true);
      %Ghost.startFade(1000, 0, false);
      %Ghost.setPosition(vectorAdd(%ghost.getPosition(), "0 0 500"));
      %Ghost.setVelocity("0 0 0");
      MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": I'm back!!!");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance < 20) {
      MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": I don't think so!");
      //ATTACK
         %p = new ShockLanceProjectile() {
            dataBlock        = GoLShocker;
            initialDirection = %Ghost.getMuzzleVector(0);
            initialPosition  = %Ghost.getMuzzlePoint(0);
            sourceObject     = %Ghost;
            sourceSlot       = 0;
            targetId         = %closestClient;
         };
         MissionCleanup.add(%p);
      //
      %totalDamage = 50;
      %closestClient.getDataBlock().damageObject(%closestClient, %ghost, %closestClient.getPosition(), %totalDamage, $DamageType::ShockLance);
      //
      %Ghost.startFade(400, 0, true);
      %Ghost.startFade(1000, 0, false);
      %new = VectorAdd(%Ghost.getPosition(), GetRandomPosition(50, 1));
      %new = VectorAdd(%new, "0 0 5");
      %Ghost.setPosition(%new);
      %Ghost.setVelocity("0 0 0");
   }
   if(%closestDistance <= $Zombie::detectDist){
    %Ghost.hastarget = 0;
      %vector = ZgetFacingDirection(%Ghost,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed);

	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);

	%vector = %x@" "@%y@" "@%z;
	%Ghost.applyImpulse(%pos, %vector);
   }
   else if(%Ghost.hastarget == 1){
	%Ghost.hastarget = 0;
	%Ghost.GhostRmove = schedule(100, %Ghost, "ZSetRandomMove", %Ghost);
   }
   %Ghost.moveloop = schedule(500, %Ghost, "GoLmovetotarget", %Ghost);
}

function DoGoLAttacks(%ghost) {
   if(!isObject(%ghost) || %ghost.getState() $= "dead") {
      return;
   }
   //
   %type = getRandom(1, 5);
   switch(%type) {
      case 1:
         %target = FindValidTarget(%ghost);
         if(!isObject(%target.player)) {
            schedule(35000, 0, "DoGoLAttacks", %ghost);
            MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Meh, no targets for me.");
            return;
         }
         MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Lightning Strike Away!!!");
	     %p = discharge2(%target.player.getPosition(),"0 0 -1");
	     %p.setEnergyPercentage(1);
	     addToShock(%p);
	     %p.schedule(1000,"delete");
      case 2:
         %target = FindValidTarget(%ghost);
         if(!isObject(%target.player)) {
            schedule(35000, 0, "DoGoLAttacks", %ghost);
            MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Meh, no targets for me.");
            return;
         }
         MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Watch Electricity Chase You!!!");
	     %vec = vectorNormalize(vectorSub(%target.player.getPosition(),%ghost.getPosition()));
         %p = new SeekerProjectile() {
	        dataBlock        = IonMissile;
	        initialDirection = %vec;
	        initialPosition  = %ghost.getMuzzlePoint(0);
	        sourceObject     = %ghost;
	        sourceSlot       = 4;
   	     };
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
      case 3:
         %target = FindValidTarget(%ghost);
         if(!isObject(%target.player)) {
            schedule(35000, 0, "DoGoLAttacks", %ghost);
            MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Meh, no targets for me.");
            return;
         }
         MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Die Now...");
         %target = %target.player;
         %p = new ShockLanceProjectile() {
            dataBlock        = GoLShocker;
            initialDirection = %Ghost.getMuzzleVector(0);
            initialPosition  = %Ghost.getMuzzlePoint(0);
            sourceObject     = %Ghost;
            sourceSlot       = 0;
            targetId         = %target;
         };
         MissionCleanup.add(%p);
         //
         %totalDamage = 50;
         %target.getDataBlock().damageObject(%target, %ghost, %target.getPosition(), %totalDamage, $DamageType::ShockLance);
      case 4:
         MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Its Storm Time");
         ionStorm(35 , 1000);
      case 5:
         MessageAll('msgDarkraiAttack', "\c4"@$TWM2::BossName["GoL"]@": Lightning Missiles For ALL!!");
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %cl = ClientGroup.getObject(%i);
            if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
               %vec = vectorNormalize(vectorSub(%cl.player.getPosition(),%ghost.getPosition()));
               %p = new SeekerProjectile() {
	              dataBlock        = IonMissile;
	              initialDirection = %vec;
	              initialPosition  = %ghost.getMuzzlePoint(0);
	              sourceObject     = %ghost;
	              sourceSlot       = 4;
   	           };
   	           %beacon = new BeaconObject() {
                  dataBlock = "SubBeacon";
                  beaconType = "vehicle";
                  position = %cl.player.getWorldBoxCenter();
               };
               %beacon.team = 0;
   	           %beacon.setTarget(0);
   	           MissionCleanup.add(%beacon);
               %p.setObjectTarget(%beacon);
	           DemonMotherMissileFollow(%cl.player,%beacon,%p);
            }
         }
   }
   //
   schedule(30000, 0, "DoGoLAttacks", %ghost);
}
