//Ghost of Fire
//Phantom139
//Ported From TWM1

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
         %nP = getRandomPosition(55, 0);
         %nP2 = vectorAdd(%np, "0 0 100");
         %fP = vectorAdd(%cP, %nP2);
         GOFAttack_FUNC("FireBlast", %g TAB %cP);
         %g.setTransform(%fP);
         messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": ehehehehe.. Burn out...");
         
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
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Lets see how you dodge this, "@getTaggedString(%target.name)@".");
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
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Flame Storm "@getTaggedString(%target.name)@", cooked up nicely for you.");
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
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Engage Dictator Strike!!!");
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
            messageall('TheFireMsg',"\c4"@$TWM2::BossName["GoF"]@": Clensic Flames Will Persue You "@getTaggedString(%target.name)@", MANY FLAMES!");
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
