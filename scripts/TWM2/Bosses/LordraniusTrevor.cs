datablock HoverVehicleData(TrevorTank) : CentaurVehicle {
   spawnOffset = "0 0 4";
   canControl = true;
   floatingGravMag = 4.5;

   catagory = "Vehicles";
   shapeFile = "vehicle_grav_tank.dts";
   multipassenger = false;
   computeCRC = true;
   renderWhenDestroyed = false;
   
   mountPose[0] = sitting;
   numMountPoints = 0;  // <-- Ignore this
   isProtectedMountPoint[0] = true;

   maxDamage = 100.15;
   destroyedLevel = 100.15;

   isShielded = true;
   rechargeRate = 1.0;
   energyPerDamagePoint = 135;
   maxEnergy = 400;
   minJetEnergy = 15;
   jetEnergyDrain = 2.0;

   targetNameTag = 'Centaur';
   targetTypeTag = 'MK III';
   
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   ShieldDamageScale[$DamageType::Bullet] = 0.01;  //I deny you shrike n0bs
};

function StartTrevor(%pos){
	%team = 6;
	%rotation = "1 0 0 0";
    %skill = 10;

   %tank = new HoverVehicle() {
      dataBlock    = TrevorTank;
      position     = %pos;
      rotation     = %rotation;
      team         = %team;
   };
   MissionCleanUp.add(%tank);

   setTargetSensorGroup(%tank.getTarget(), %team);
   %tank.isdrone = 1;
   %tank.skill = 0.2 + (%skill / 12.5);

   %tank.CanUseSpec = 1;
   %tank.SpecTicks = 0;
   InitiateBoss(%tank, "Trevor");

   TrevorDetermineAttack(%tank);
   TrevorMove(%tank);
   MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": It's time to test the harbinger clan's ultimate siege weapon. ON YOU!");

   return %tank;
}

function TrevorTank::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   %turret = TurretData::create(CentaurTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.team = 6;
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);

   %turret.mountImage(Cent50CalBarrel, 0);
   %obj.barrel = "Chain";

   %obj.turretObject = %turret;
   %turret.source = %obj;

   %turret.setCapacitorRechargeRate(999);
   %turret.setAutoFire(true);

   %obj.schedule(6000, "playThread", $ActivateThread, "activate");

   // set the turret's target info
   setTargetSensorGroup(%obj.getTarget(), 6);
   setTargetAlwaysVisMask(%obj.getTarget(), 0xffffffff);
   setTargetSensorGroup(%turret.getTarget(), 6);
   setTargetAlwaysVisMask(%turret.getTarget(), 0xffffffff);
}
function TrevorTank::deleteAllMounted(%data, %obj) {
   CentaurVehicle::deleteAllMounted(%data, %obj);
}

function TrevorLocateTarget(%tank) {
   %wbpos = %tank.getworldboxcenter();
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
   return %closestClient SPC %closestDistance;
}

function TrevorRotateAndVec(%tank, %tPl){
   if(!isObject(%tPl)) {
      return;
   }
   //
   %cPos = %tank.getPosition();
   %vector = vectorNormalize(vectorSub(%cpos, %tPl.getPosition()));
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %v3 = getword(%vector, 2);
   %nv1 = (%v2*-1);
   %nv2 = (%v1);
   %set = %nv1@" "@%nv2@" "@%v3;
   %tank.setRotation(fullrot("0 0 0", %Set));
   //
   return %set;
}

function TrevorMove(%tank) {
   if(!isObject(%tank)) {
      return;
   }
   if(%tank.performingSpec) {  //Specials make the tank do things that this must be off to work
      schedule(250, 0, "TrevorMove", %tank);
      return;
   }
   %target = TrevorLocateTarget(%tank);
   if(!isObject(%target.player)) {
      schedule(100, 0, "TrevorMove", %tank);
      return;
   }
   %vec = TrevorRotateAndVec(%tank, %target.player);  //turns the tank
   %dist = VectorDist(%target.player.getPosition(), %tank.getPosition());   //The all important
   //don't ask how we would get this far
   //but we simply move
   if(%dist > 1500) {
      %vector = vectorscale(%tank.getForwardVector(), 6000);
      %tank.applyImpulse(%tank.getPosition(), %vector);
      //throw in another speed boost
      %tank.setImageTrigger(3, true); //the jets
   }
   else if(%dist <= 1500 && %dist > 200) {
      %vector = vectorscale(%tank.getForwardVector(), 3000);
      %tank.applyImpulse(%tank.getPosition(), %vector);
      //decent move, we like this range
   }
   //This range is potentially deadly, so take it slower
   else {
      if(%dist < 10 && !%tank.teleporting) {
         //interesting... trying to cut it close?
         %tank.teleporting = 1;
         MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Trying to do that huhn? I think not!");
         PhaseShift(%tank);
      }
      %vector = vectorscale(%tank.getForwardVector(), 1150);
      %tank.applyImpulse(%tank.getPosition(), %vector);
   }
   schedule(100, 0, "TrevorMove", %tank);
}

function TrevorDetermineAttack(%tank) {
   if(!isObject(%tank)) {
      return;
   }
   if(%tank.performingSpec) {  //Specials make the tank do things that this must be off to work
      schedule(250, 0, "TrevorDetermineAttack", %tank);
      return;
   }
   %target = TrevorLocateTarget(%tank);
   if(!isObject(%target)) {
      schedule(250, 0, "TrevorDetermineAttack", %tank);
      return;
   }
   %dist = VectorDist(%target.player.getPosition(), %tank.getPosition());   //The all important
   if(%dist < 175) { //in CG Range
      //Collider?
      if(%tank.barrel $= "Collider") {
         %tank.barrel = "Chain";
         //unmount first, we no likey UEs
         %tank.turretObject.schedule(500, "unmountImage", 0);
         %tank.turretObject.schedule(500, "unmountImage", 1);
         //
         %tank.turretObject.schedule(3500, "mountImage", Cent50CalBarrel, 0);
      }
   }
   //striking range for collider
   else {
      if(%tank.barrel $= "Chain") {
         %tank.barrel = "Collider";
         //unmount first, we no likey UEs
         %tank.turretObject.schedule(500, "unmountImage", 0);
         %tank.turretObject.schedule(500, "unmountImage", 1);
         //
         %tank.turretObject.schedule(3500, "mountImage", CentaurColliderBarrel, 0);
      }
   }
   if(%tank.CanUseSpec) {
      %tank.CanUseSpec = 0;
      schedule(20000, 0, "eval", "%tank.CanUseSpec = 1;");
      if(%dist < 150) { //in CG Range
         //Now, Lets try an attack
         %attackNum = GetRandom(1, 3);
         //1. Ramming Run: Tank boosts to ram the target
         //2. CG Slide: Tank slides around the target and CGs it
         //3. Phase Shift: Tank Shifts to collider range
         switch(%attackNum) {
            case 1:
               MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Dodge this "@getTaggedString(%target.name)@"!!!");
               RammingSpeed(%tank, %target);
            case 2:
               MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Engage sideswipe boosters");
               SlideTarget(%tank, %target);
            case 3:
               MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Engage Phase Shift!");
               PhaseShift(%tank);
         }
      }
      //striking range for collider (and some of my other goodies)
      else {
         %attackNum = getRandom(1,3);
         switch(%attackNum) {
            case 1:
               MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Engage missile storm on "@getTaggedString(%target.name)@"!!!");
               MissileStorm(%tank, %target);
            case 2:
               MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Let a firey storm of missiles rain upon you, "@getTaggedString(%target.name)@"!!!");
               MissileRain(%tank, %target);
            case 3:
               MessageAll('MsgBossBegin', "\c4"@$TWM2::BossName["Trevor"]@": Engage missile storm on "@getTaggedString(%target.name)@"!!!");
               MissileStorm(%tank, %target);
         }
      }
   }
   schedule(250, 0, "TrevorDetermineAttack", %tank);
}

//Attackz0rs
function RammingSpeed(%tank, %target) {
   //get ready to blast into them
   if(!isObject(%tank)) {
      return;
   }
   if(!isObject(%target.player)) {
      %tank.performingSpec = 0;
      %tank.SpecTicks = 0;
      %tank.barrel = "Chain";
      %tank.turretObject.schedule(3500, "mountImage", Cent50CalBarrel, 0);
      echo("done");
      return;
   }
   %tank.SpecTicks++;
   if(%tank.SpecTicks == 1) {
      echo("prep");
      %tank.performingSpec = 1;
      %tank.barrel = "";
      %tank.turretObject.schedule(100, "unMountImage", 0);
   }
   else {
      %vec = TrevorRotateAndVec(%tank, %target.player);  //turns the tank
      %vector = vectorscale(%tank.getForwardVector(), 15000);
      %tank.applyImpulse(%tank.getPosition(), %vector);
      //%tank.setImageTrigger(3, true);
      //echo("boost");
   }
   if(%tank.SpecTicks > 150) {
      %tank.performingSpec = 0;
      %tank.SpecTicks = 0;
      %tank.barrel = "Chain";
      %tank.turretObject.schedule(15000, "mountImage", Cent50CalBarrel, 0);
      echo("done");
      return;
   }
   schedule(100, 0, "RammingSpeed", %tank, %target);
}

function SlideTarget(%tank, %target) {
   if(!isObject(%tank)) {
      return;
   }
   if(!isObject(%target.player)) {
      %tank.performingSpec = 0;
      return;
   }
   %tank.SpecTicks++;
   if(%tank.SpecTicks == 1) {
      %tank.performingSpec = 1;
   }
   else {
      %vec = TrevorRotateAndVec(%tank, %target.player);  //turns the tank
      %x = Getword(%vec,0);
      %y = Getword(%vec,1);
      %nv1 = %y;
      %nv2 = (%x * -1);
      %vector = %nv1@" "@%nv2@" 0";
      %vector = vectorscale(%vector, 8000);
      %tank.applyImpulse(%tank.getPosition(), %vector);
   }
   if(%tank.SpecTicks > 100) {
      %tank.performingSpec = 0;
      %tank.SpecTicks = 0;
      return;
   }
   schedule(100, 0, "SlideTarget", %tank, %target);
}

function PhaseShift(%tank) {
   %tank.setCloaked(true);
   %tank.setFrozenState(true);
   %tank.schedule(2500, "setCloaked", false);
   %tank.schedule(2500, "setFrozenState", false);
   %CPos = %tank.getPosition();
   %rand = TWM2Lib_MainControl("getRandomPosition", 250 TAB 1);
   %fin = vectorAdd(%CPos, %rand);
   %xy = getwords(%fin, 0, 1);
   %z = getTerrainHeight(%fin) + 9;
   %goto = %xy SPC %z;
   %tank.schedule(1000, "SetPosition", %goto);
   %tank.SpecTicks = 0;
   %tank.performingSpec = 0;
   schedule(2500, 0, eval, ""@%tank@".teleporting = 0;");
}

// hack
function CentaurTurret::selectTarget(%this, %turret) {
   if(%turret.source.isboss) {
	%turretTarg = %turret.getTarget();
	if(%turretTarg == -1)
		return;

	// if the turret isn't on a team, don't fire at anyone
	if(getTargetSensorGroup(%turretTarg) == 0) {
		%turret.clearTarget();
		return;
	}

	// stop firing if turret is disabled or if it needs power and isn't powered
	if((!%turret.isPowered()) && (!%turret.needsNoPower)) {
		%turret.clearTarget();
		return;
	}

	%TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType;

	InitContainerRadiusSearch(%turret.getMuzzlePoint(0), %turret.getMountedImage(0).attackRadius,
				%TargetSearchMask);

	// TODO - clean up this mess + GameBaseData::hasLOS()

	while ((%potentialTarget = ContainerSearchNext()) != 0) {
		if (%potentialtarget && %potentialTarget != %turret.source) {
			%potTargTarg = %potentialTarget.getTarget();
				if (%potentialTarget.homingCount > 0 && !%secondTarg) {
					if (!%firstTarg)
						%firstTarg = %potentialTarget;
					else
						%secondTarg = %potentialTarget;
				}
				else {
					%turret.setTargetObject(%potentialTarget);
					%turret.aquireTime = getSimTime();
					return;
				}
		}
	}
	if (%secondTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
	if (%firstTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
   }
   else {
      TurretData::selectTarget(%this, %turret);
   }
}

function MissileStorm(%tank, %target) {
   schedule(700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(1400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(2100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(2800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(3500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(4200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(4900,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 5);
   schedule(5600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 3);
   schedule(6300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 3);
   schedule(7000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 3);
   // Quick Shots
   schedule(8000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(8700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   %tank.SpecTicks = 0;
   %tank.performingSpec = 0;
}

//ultimate evil
function MissileRain(%tank, %target) {
   schedule(100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(900,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   // Quick Shots
   schedule(1100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(1900,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2100,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2200,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2300,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2400,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2500,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2600,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2700,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2800,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(2900,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   schedule(3000,0,"WindshearAttack_FUNC", "SidewinderLaunch", %tank SPC %target.player SPC 1);
   %tank.SpecTicks = 0;
   %tank.performingSpec = 0;
}
