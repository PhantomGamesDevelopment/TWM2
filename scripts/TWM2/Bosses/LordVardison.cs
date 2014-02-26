//LORD \/ARDISON & Dark Archmage Vardison
//THIS BOSS WILL MURDER OCCULTBADBOY
//Yeah, he really will
function VardisonLaserBallMissile::OnExplode(%data, %proj, %pos, %mod) {
   //LaserBall
   %ball = CreateEmitter(%pos, "MiniShadowBallEmitter", "0 0 0 0");
   %ball.schedule(10000, "Delete");
   LaserBallStrike(vectorAdd(%pos, "0 0 3"), 0);
}

function LaserBallStrike(%position, %count) {
   %count++;
   if(%count > 100) {
      %p = new LinearFlareProjectile() {
	     dataBlock        = VardisonSubShadowBomb;
         initialDirection = "0 0 -1";
	     initialPosition  = %position;
      };
      return; //stop here
   }
   else {
      if(%count % 3 == 0) {     //multiples of 3 == strike
         %p = new TracerProjectile() {
	        dataBlock        = PlasmaCannonMainProj;
            initialDirection = vectorAdd(%postition, getRandomPosition(25,0));
	        initialPosition  = %position;
         };
      }
   }
   schedule(100, 0, "LaserBallStrike", %position, %count);
}

function StartDAVardison(%pos) {
   %Vardison = new player() {
      Datablock = "DarkArchmageVardisonArmor";
   };
   %Cpos = vectorAdd(%pos, "0 0 5");
   MessageAll('MsgVardison', "\c4"@$TWM2::BossName["DAVardison"]@": Rise forces of darkness, as our enemies shall face their demise");

   %command = "DAVardisonmovetotarget";
   %Vardison.ticks = 0;
   InitiateBoss(%Vardison, "DAVardison");

   %Vardison.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["DAVardison"]@""); // <- To Hosts, Enjoy, You can
                                      //Change the Demon Names now!!!
   %Vardison.target = createTarget(%Vardison, %zname, "", "Derm3", '', %Vardison.team, PlayerSensor);
   setTargetSensorData(%Vardison.target, PlayerSensor);
   setTargetSensorGroup(%Vardison.target, 30);
   setTargetName(%Vardison.target, addtaggedstring(%zname));
   setTargetSkin(%Vardison.target, 'Inferno');
   //
   %Vardison.setPosition(%cpos);
   %Vardison.canjump = 1;
   %Vardison.hastarget = 1;
   MissionCleanup.add(%Vardison);
   schedule(1000, %Vardison, %command, %Vardison);

   DAVardisonAttacks(%Vardison);
}

function StartVardison1(%pos) {

	%Vardison = new player(){
	   Datablock = "VardisonStage1Armor";
	};
	%Cpos = vectorAdd(%pos, "0 0 5");
    MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": "@$TWM2::BossName["Vardison"]@", Checking into duty, and about to slaughter some fuckin' enemies!!!");

	%command = "Vardison1movetotarget";
    %Vardison.ticks = 0;
    InitiateBoss(%Vardison, "Vardison1");

   %Vardison.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["Vardison"]@""); // <- To Hosts, Enjoy, You can
                                      //Change the Demon Names now!!!
   %Vardison.target = createTarget(%Vardison, %zname, "", "Derm3", '', %Vardison.team, PlayerSensor);
   setTargetSensorData(%Vardison.target, PlayerSensor);
   setTargetSensorGroup(%Vardison.target, 30);
   setTargetName(%Vardison.target, addtaggedstring(%zname));
   setTargetSkin(%Vardison.target, 'Inferno');
   //
   %Vardison.setPosition(%cpos);
   %Vardison.canjump = 1;
   %Vardison.hastarget = 1;
   MissionCleanup.add(%Vardison);
   schedule(1000, %Vardison, %command, %Vardison);

   VardisonAttacks(%Vardison);
}

function StartVardison2(%pos) {
   %StartPos = VectorAdd(%pos, "0 0 100");
	%team = 30;
	%rotation = "1 0 0 0";
    %skill = 10;

   %Drone = new FlyingVehicle() {
      dataBlock    = VardisonStage2Flyer;
      position     = %StartPos;
      rotation     = %rotation;
      team         = %team;
   };
   MissionCleanUp.add(%Drone);
   
   MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": HA! I'm nowhere near finished with you, Lets take this to the skies.. shall we.");

   setTargetSensorGroup(%Drone.getTarget(), %team);

   %Drone.isdrone = 1;
   %drone.dodgeGround = 0;

   %drone.isace = 1;

   %drone.skill = 0.2 + (%skill / 12.5);

   schedule(100, 0, "DroneForwardImpulse", %drone); //special impulse
   schedule(101, 0, "DronefindTarget", %drone);
   schedule(102, 0, "DroneScanGround", %drone);

   InitiateBoss(%drone, "Vardison2");
   VardisonDroneAttacks(%drone);
}

function StartVardison3(%pos) {
	%Vardison = new player(){
	   Datablock = "VardisonStage3Armor";
	};
	%Cpos = vectorAdd(%pos, "0 0 5");
    MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Now you will see the full power of a shadow demon!!!");

	%command = "Vardison3movetotarget";
    %Vardison.ticks = 0;
    InitiateBoss(%Vardison, "Vardison3");

   %Vardison.team = 30;
   %zname = CollapseEscape("\c7"@$TWM2::BossName["Vardison"]@""); // <- To Hosts, Enjoy, You can
                                      //Change the Demon Names now!!!
   %Vardison.target = createTarget(%Vardison, %zname, "", "Derm3", '', %Vardison.team, PlayerSensor);
   setTargetSensorData(%Vardison.target, PlayerSensor);
   setTargetSensorGroup(%Vardison.target, 30);
   setTargetName(%Vardison.target, addtaggedstring(%zname));
   setTargetSkin(%Vardison.target, 'Inferno');
   //
   %Vardison.setTransform(%cpos);
   %Vardison.canjump = 1;
   %Vardison.hastarget = 1;
   MissionCleanup.add(%Vardison);
   schedule(1000, %Vardison, %command, %Vardison);
   
   VardisonDemonAttacks(%Vardison);
   
   //SpawnVardHelper(%Vardison, vectorAdd(%Vardison.getPosition(), "15 0 100"));

}

function DAVardisonmovetotarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["DAVardison"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 15) {
          if(!%closestClient.vardKilling) {
             %closestClient.vardKilling = 1;
             DoVardisonSuperCloseKill(%Demon, %closestClient, 0);
             MessageAll('MsgVardison', "\c4"@$TWM2::BossName["DAVardison"]@": Die.... Human....");
          }
       }
	   if(%Demon.hastarget != 1){
	      //LZDoYell(%Demon);
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
   %Demon.moveloop = schedule(500, %Demon, "DAVardisonmovetotarget", %Demon);
}

function Vardison1movetotarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 15) {
          if(!%closestClient.vardKilling) {
             %closestClient.vardKilling = 1;
             DoVardisonSuperCloseKill(%Demon, %closestClient, 0);
             MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Die.... Human....");
          }
       }
	   if(%Demon.hastarget != 1){
	      //LZDoYell(%Demon);
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
   %Demon.moveloop = schedule(500, %Demon, "Vardison1movetotarget", %Demon);
}

function Vardison3movetotarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %z = getWord(%pos, 2);
   if(%z < -300) {
      %Demon.startFade(400, 0, true);
      %Demon.startFade(1000, 0, false);
      %Demon.setPosition(vectorAdd(vectoradd(%closestclient.getPosition(), "0 0 20"), getRandomPosition(25, 1)));
      %Demon.setVelocity("0 0 0");
      MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I'm back....");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $Zombie::detectDist){
       if(%closestDistance < 10) {
          %closestClient.scriptKill(0);
          MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": DIE!!!!!!");
       }
	   if(%Demon.hastarget != 1){
	      %Demon.hastarget = 1;
       }

       %vector = ZgetFacingDirection(%Demon,%closestClient,%pos);

	%vector = vectorscale(%vector, $Zombie::DForwardSpeed*1.8);
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
   %Demon.moveloop = schedule(500, %Demon, "Vardison3movetotarget", %Demon);
}

//ATTACKS

function DoVardisonSuperCloseKill(%source, %target, %count) {
   %count++;
   if(!isObject(%source) || %source.getState() $= "dead") {
      %target.setMoveState(false);
      return;
   }
   %source.setMoveState(true);
   %target.setMoveState(true);
   %target.clearInventory(); //ha, no guns for You!
   //lift
   if(%count <= 15) {
      %ZPos = %count * 0.025;
	  %newpos = vectoradd(%target.getPosition(),"0 0 "@%ZPos@"");
	  %target.setTransform(%newpos);
	  %target.setvelocity("0 0 0");
   }
   else if(%count == 16) {
      //MessageAll('MsgDIE', "\c4"@%source.client.namebase@": You're so.... weak...");
	  %newpos = vectoradd(%target.getPosition(),"0 0 "@%ZPos * -1@"");
	  %target.setTransform(%newpos);
	  %target.setvelocity("0 0 0");
   }
   else if(%count == 17) {
      %target.setvelocity("1000 1000 1000");
      %target.blowup();//BAM!
      ServerPlay3d(BOVHitSound, %target.getPosition());
      ServerPlay3d(BOVHitSound, %target.getPosition());
      ServerPlay3d(BOVHitSound, %target.getPosition());
      %target.damage(%source, %target.getposition(), 9999, $DamageType::BladeOfVengance);
      %source.setMoveState(false);
      return;
   }
   schedule(100, 0, "DoVardisonSuperCloseKill", %source, %target, %count);
}

function GOVDoFlameCano(%g, %target) {
   if(!isObject(%g) || %g.getState() $= "dead") {
      return;
   }
   %g.setPosition(VectorAdd(%target.getPosition(), "0 0 70"));
   %Pad = new StaticShape() {
      dataBlock = DeployedSpine;
      scale = ".1 .1 1";
      position = VectorAdd(%target.getPosition(), "0 0 69");
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
   schedule(2500,0,"DropFlameCano2", %g, %target);
}

function DropFlameCano2(%g, %target) {
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
      %p = new LinearFlareProjectile() {
	      	dataBlock        = ShadowBomb;
	    	initialDirection = "0 0 -30";
	    	initialPosition  = vectorAdd(%g.getPosition(), "0 0 -3");
	    	sourceObject     = %g;
   	    	sourceSlot       = 5;
      };
      %p.vector = %vec[%i];
      %p.count = 1;
      %p.MaxExplode = 15;
   }
}

//The evilness Begins Here
function DAVardisonAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   schedule(23500, 0, "DAVardisonAttacks", %boss);
   %attack = getRandom(1, 10);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "NMM", %target);
         }
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
         }
      case 3:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 2);
         }
      case 4:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            GOVDoFlameCano(%boss, %target);
         }
      case 5:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 4);
         }
      case 6:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            for(%i = 0; %i < 10; %i++) {
               %timeInt = %i * 200;
               schedule(%timeInt, 0, VardisonAttack, %boss, "ShadowBombLaunchAbove", %target SPC 4);
            }
         }
      case 7:
         %boss.setMoveState(true);
         %vS[0] = "10 10 0";
         %vS[1] = "-10 10 0";
         %vS[2] = "10 -10 0";
         %vS[3] = "-10 -10 0";
         for(%i = 0; %i < 4; %i++) {
            CreateDemon(vectorAdd(%boss.getPosition(), %vS[%i]));
         }
         %boss.schedule(5000, setMoveState, false);
      case 8:
         %boss.setMoveState(true);
         //four charge-up beams
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         //the actual attack
         for(%i = 0; %i < 50; %i++) {
            %timeAtt = 5000 + (%i *150);
            //--------------------------
            %vec = %boss.GetMuzzleVector(4);
            %pos = %boss.GetMuzzlePoint(4);
            schedule(%timeAtt, 0, VardisonAttack, %boss, "HyperspeedPlasmaBolt", %pos TAB %vec);
         }
         %boss.schedule(12500, setMoveState, false);
      case 9:
         VardisonAttack(%boss, "LinearFlameWall");
      case 10:
         VardisonAttack(%boss, "SeekingRapiers", %target);
   }
}

function VardisonAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   schedule(23500, 0, "VardisonAttacks", %boss);
   %attack = getRandom(1, 8);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "NMM", %target);
         }
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
         }
      case 3:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 2);
         }
      case 4:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            GOVDoFlameCano(%boss, %target);
         }
      case 5:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "ShadowBombDirect", %target SPC 4);
         }
      case 6:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            for(%i = 0; %i < 10; %i++) {
               %timeInt = %i * 200;
               schedule(%timeInt, 0, VardisonAttack, %boss, "ShadowBombLaunchAbove", %target SPC 4);
            }
         }
      case 7:
         %boss.setMoveState(true);
         %vS[0] = "10 10 0";
         %vS[1] = "-10 10 0";
         %vS[2] = "10 -10 0";
         %vS[3] = "-10 -10 0";
         for(%i = 0; %i < 4; %i++) {
            CreateDemon(vectorAdd(%boss.getPosition(), %vS[%i]));
         }
         %boss.schedule(5000, setMoveState, false);
      case 8:
         %boss.setMoveState(true);
         //four charge-up beams
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         //the actual attack
         for(%i = 0; %i < 50; %i++) {
            %timeAtt = 5000 + (%i *150);
            //--------------------------
            %vec = %boss.GetMuzzleVector(4);
            %pos = %boss.GetMuzzlePoint(4);
            schedule(%timeAtt, 0, VardisonAttack, %boss, "HyperspeedPlasmaBolt", %pos TAB %vec);
         }
         %boss.schedule(12500, setMoveState, false);
   }
}

function VardisonDroneAttacks(%boss) {
   if(!isObject(%boss)) {
      return;
   }
   schedule(10000, 0, "VardisonDroneAttacks", %boss);
   %attack = getRandom(1,3);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            VardisonAttack(%boss, "NMM", %target.player);
         }
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
         }
      case 3:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "SuperLaser", %target);
         }
   }
}

function VardisonDemonAttacks(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   //create a mini-demon
   CreateDemon(vectorAdd(%boss.getPosition(), getRandomPosition(10, 1)));
   //
   %boss.setMoveState(true);
   schedule(15000, 0, "VardisonDemonAttacks", %boss);
   %attack = getRandom(1,8);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LBM", %target);
            schedule(2500, 0, VardisonAttack, %boss, "LBM", %target);
            schedule(3500, 0, VardisonAttack, %boss, "LBM", %target);
            schedule(5000, 0, VardisonAttack, %boss, "LBM", %target);
            schedule(5100, 0, VardisonAttack, %boss, "LBM", %target);
            %boss.schedule(5100, "SetMoveState", false);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I've got some missiles for you "@getTaggedString(%target.client.name)@".");
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 2:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            VardisonAttack(%boss, "NMM", %target.player);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": It's time to invoke darkness upon "@getTaggedString(%target.name)@".");
            %boss.schedule(1000, "SetMoveState", false);
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 3:
         setgravity(-1000);
         MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": I'll disorient you all!");
         schedule(3000, 0, "SetGravity", 1000);
         schedule(7500, 0, "SetGravity", -20);
         %boss.schedule(7500, "SetMoveState", false);
         //%boss.InvokeLoop = InvokeStillwallLoop(%boss);
         schedule(7500, 0, "Cancel", %boss.InvokeLoop);
      case 4:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            VardisonAttack(%boss, "LaserDrop", %target);
            %boss.schedule(3000, "SetMoveState", false);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Your time has come "@getTaggedString(%target.client.name)@".");
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 5:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            %target = %target.player;
            for(%i = 0; %i < 25; %i++) {
               schedule(50+(%i*150), 0, VardisonAttack, %boss, "SuperLaser", %target);
            }
            %boss.schedule(10000, "SetMoveState", false);
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": BLAAAAHAAHAHAHAAHA!!!");
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
      case 6:
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %target = ClientGroup.getObject(%i);
            if(isObject(%target.player)) {
               VardisonAttack(%boss, "NMM", %target.player);
            }
         }
         MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": All must suffer!!!");
         %boss.schedule(1000, "SetMoveState", false);
         return;
      case 7:
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %target = ClientGroup.getObject(%i);
            if(isObject(%target.player)) {
               %target = %target.player;
               for(%l = 0; %l < 25; %l++) {
                  schedule(50+(%l*150), 0, VardisonAttack, %boss, "SuperLaser", %target);
               }
            }
         }
         MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": Everyone DIES NOW!!!!");
         %boss.schedule(10000, "SetMoveState", false);
         return;
      case 8:
         %target = FindValidTarget(%boss);
         if(isObject(%target.player)) {
            for(%i = 0; %i < 15; %i++) {
               %time = %i * 150;
               %mType = getRandom(0, 1);
               switch(%mType) {
                  case 0:
                     schedule(%time, 0, VardisonAttack, %boss, "NMM", %target.player);
                  case 1:
                     schedule(%time, 0, VardisonAttack, %boss, "LBM", %target.player);
                  default:
                     schedule(%time, 0, VardisonAttack, %boss, "NMM", %target.player);
               }
            }
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["Vardison"]@": "@getTaggedString(%target.name)@" Will Feel the Power of My Missiles.");
            %boss.schedule(1000, "SetMoveState", false);
            return;
         }
         %boss.schedule(1, "SetMoveState", false);
   }
}

function VardisonAttack(%boss, %att, %arg) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   switch$(%att) {
      case "ShadowBombDirect":
         %target = getWord(%arg, 0);
         %detCt = getWord(%arg, 1);
         if(!isObject(%target) || %target.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%target.getPosition(),%boss.getPosition()));
         %p = new LinearFlareProjectile() {
            dataBlock        = ShadowBomb;
            initialDirection = vectorScale(%vec, 10);
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         //%p.maxExplode = %detCt;
         MissionCleanup.add(%p);
         
      case "ShadowBombLaunchAbove":
         %target = getWord(%arg, 0);
         %detCt = getWord(%arg, 1);
         if(!isObject(%target) || %target.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%target.getPosition(), vectorAdd(%boss.getPosition(), "0 0 35")));
         %p = new LinearFlareProjectile() {
            dataBlock        = ShadowBomb;
            initialDirection = vectorScale(%vec, 10);
            initialPosition  = vectorAdd(%boss.getPosition(), "0 0 35");
            sourceSlot       = 4;
         };
         %p.maxExplode = %detCt;
         %p.sourceObject = %boss;
         MissionCleanup.add(%p);
         
      case "HyperspeedPlasmaBolt":
         %boss.playShieldEffect("1 1 1");
         %pos = getField(%arg, 0);
         %dir = getField(%arg, 1);
         %p = new TracerProjectile() {
	        dataBlock        = PlasmaCannonMainProj;
            initialDirection = %dir;
	        initialPosition  = %pos;
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
         
      case "LaserDrop":
         %toDie = %arg;
         if(!isObject(%toDie) || %toDie.getState() $= "dead") {
            return;
         }
         %p = new LinearFlareProjectile() {
            dataBlock        = HyperDevestatorBeam;
            initialDirection = "0 0 -10";
            initialPosition  = vectoradd(%target.getPosition(), "0 0 500");
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
         
      case "SuperLaser":
         %toDie = %arg;
         if(!isObject(%toDie) || %toDie.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%toDie.getPosition(), %boss.getPosition()));
         %p = new LinearFlareProjectile() {
            dataBlock        = SuperlaserProjectile;
            initialDirection = %vec;
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
         
      case "NMM":
         %target = %arg;
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %boss.getPosition()));
         %p = new SeekerProjectile() {
            dataBlock        = YvexNightmareMissile;
            initialDirection = %vec;
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %target.getWorldBoxCenter();
         };
         %beacon.team = 0;
         %beacon.setTarget(0);
         MissionCleanup.add(%p);
         MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%target,%beacon,%p);
         
      case "LBM":
         %target = %arg;
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %boss.getPosition()));
         %p = new SeekerProjectile() {
            dataBlock        = VardisonLaserBallMissile;
            initialDirection = %vec;
            initialPosition  = %boss.getPosition();
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %target.getWorldBoxCenter();
         };
         %beacon.team = 0;
         %beacon.setTarget(0);
         MissionCleanup.add(%p);
         MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%target,%beacon,%p);
         
      case "LinearFlameWall":
         %fVec = %boss.getEyeVector();
         %fPos = %boss.getEyePosition();
         %lPos = vectorAdd(%fPos, vectorScale(%fVec, 10));
         %vec = vectorScale(%fVec, 24);
         //drop a line fire hire
         %p = new TracerProjectile() {
            dataBlock        = napalmSubExplosion;
            initialDirection = "0 0 -30";
            initialPosition  = vectorAdd(%lPos, "0 0 3");
            sourceSlot       = 5;
            maxCount = 15;
         };
         %p.sourceObject = %g;
         %p.vector = %vec;
         %p.count = 1;
         
      case "SeekingRapiers":
         %target = %arg;
         %iVec[0] = "1 0 0";
         %iVec[1] = "0 1 0";
         %iVec[2] = "-1 0 0";
         %iVec[3] = "0 -1 0";
         for(%i = 0; %i < 4; %i++) {
            createSeekingProjectile("RapierShieldForwardProjectile", "LinearFlareProjectile", %boss.getPosition(), %iVec[%i], %boss, %target, 3000);
         }
   }
}

function InvokeStillwallLoop(%boss) {
   if(!isObject(%boss) || %boss.getState() $= "dead") {
      return;
   }
   %boss.setVelocity("0 0 0");
   %boss.InvokeLoop = schedule(100, 0, "InvokeStillwallLoop", %boss);
}

//==============================================================================
function VardisonMiniDemonSpawner::OnExplode(%data, %proj, %pos, %mod) {
   //LaserBall
   %ball = CreateEmitter(%pos, "MiniShadowBallEmitter", "0 0 0 0");
   %ball.schedule(1000, "Delete");
   %Fire = CreateEmitter(%pos, "burnEmitter", "0 0 0 0");
   %Fire.schedule(2500, "Delete");
   CreateDemonAT(vectorAdd(%pos, "0 0 3"));
}

function CreateDemon(%pos) {
   %p = new SeekerProjectile() {
      dataBlock        = VardisonMiniDemonSpawner;
      initialDirection = "0 0 -10";
      initialPosition  = vectorAdd(%pos, "0 0 500");
      //sourceObject     = %boss;
      //sourceSlot       = 4;
   };
}

function CreateDemonAT(%Pos) {
   %Demon = new player(){
      Datablock = "MiniDemonArmor";
   };
   %Demon.setTransform(%Pos);
   %Demon.type = 1;
   %Demon.canjump = 1;
   %Demon.hastarget = 1;
   %Demon.isBoss = 1;     //grant boss-like ability

   %Demon.team = 30;

   %Demon.target = createTarget(%Demon, "Shadow Warrior", "", "Derm3", '', %Demon.team, PlayerSensor);
   setTargetSensorData(%Demon.target, PlayerSensor);
   setTargetSensorGroup(%Demon.target, 30);
   setTargetName(%Demon.target, addtaggedstring("Shadow Warrior"));
   setTargetSkin(%Demon.target, 'Horde');

   MissionCleanup.add(%Demon);
   schedule(1000, %Demon, "MiniDemonMoveToTarget", %Demon);
}

function MiniDemonMoveToTarget(%Demon){
   if(!isobject(%Demon))
	return;
   if(%Demon.getState() $= "dead")
	return;
   %pos = %Demon.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%Demon);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   %Demon.counter++;
   if(%Demon.counter >= 5) {
      %Demon.counter = 0;
      %Demon.canFire = 1;
   }
   if(%closestDistance <= $Zombie::detectDist) {
      if(%Demon.hastarget != 1){
	     %Demon.hastarget = 1;
	   }
	   %upvec = "250";
	   %fmultiplier = $Zombie::FForwardSpeed;

       %vector = ZgetFacingDirection(%Demon,%closestClient,%pos);
       
	   %vector = vectorscale(%vector, %Fmultiplier);
	   %x = Getword(%vector,0);
	   %y = Getword(%vector,1);
	   %z = Getword(%vector,2);
	   if(%z >= "1200" && %Demon.canjump == 1){
	      %Demon.setvelocity("0 0 0");
	      %upvec = (%upvec * 8);
	      %x = (%x * 0.5);
	      %y = (%y * 0.5);
	      %Demon.canjump = 0;
	      schedule(2500, %Demon, "Zsetjump", %Demon);
	   }
    
       if(%Demon.canFire) {
          MiniDemonFire(%Demon, %closestclient);
       }

	   %vector = %x@" "@%y@" "@%upvec;
	   %Demon.applyImpulse(%pos, %vector);
   }
   else if(%Demon.hastarget == 1) {
      %Demon.hastarget = 0;
      %Demon.DemonRmove = schedule(100, %Demon, "ZSetRandomMove", %Demon);
   }
   %Demon.moveloop = schedule(500, %Demon, "MiniDemonMoveToTarget", %Demon);
}

function MiniDemonFire(%demon, %closestclient){
   %pos = %demon.getMuzzlePoint(4);
   %tpos = %closestclient.getWorldBoxCenter();
   %tvel = %closestclient.getvelocity();
   %vec = vectorsub(%tpos,%pos);
   %dist = vectorlen(%vec);
   %velpredict = vectorscale(%tvel,(%dist / 125));
   %vector = vectoradd(%vec,%velpredict);
   %ndist = vectorlen(%vector);
   %upvec = "0 0 "@((%ndist / 125) * (%ndist / 125) * 2);
   %vector = vectornormalize(vectoradd(%vector,%upvec));
   
      %p = new GrenadeProjectile() {
	      dataBlock        = MiniDemonBlaster;
	      initialDirection = %vector;
	      initialPosition  = %pos;
	      sourceObject     = %demon;
	      sourceSlot       = 4;
      };

   %demon.canFire = 0;
}
