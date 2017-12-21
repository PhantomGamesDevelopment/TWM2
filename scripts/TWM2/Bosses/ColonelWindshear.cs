$Boss::Proficiency["CnlWindshear", 0] = "Team Bronze\t1000\tDefeat Colonel Windshear with your team dying no more than 15 times";
$Boss::ProficiencyCode["CnlWindshear", 0] = "$TWM2::BossManager.bossKills < 15";
$Boss::Proficiency["CnlWindshear", 1] = "Team Silver\t5000\tDefeat Colonel Windshear with your team dying no more than 10 times";
$Boss::ProficiencyCode["CnlWindshear", 1] = "$TWM2::BossManager.bossKills < 10";
$Boss::Proficiency["CnlWindshear", 2] = "Team Gold\t10000\tDefeat Colonel Windshear with your team dying no more than 5 times";
$Boss::ProficiencyCode["CnlWindshear", 2] = "$TWM2::BossManager.bossKills < 5";

//This sets up the drone and the functions needed to start the drone.
function StartWindshear(%pos){
   %team = 6;
   %rotation = "1 0 0 0";
   %skill = 10;
    
   %drone = new FlyingVehicle() {
      dataBlock    = WindshearPlatform;
      position     = %pos;
      rotation     = %rotation;
      team         = %team;
   };
   MissionCleanUp.add(%drone);

   setTargetSensorGroup(%drone.getTarget(), %team);

   %drone.isdrone = 1;
   %drone.dodgeGround = 0;

   %drone.isace = 1;

   %drone.skill = 0.2 + (%skill / 12.5);

   schedule(100, 0, "WSForwardImpulse", %drone); //special impulse
   schedule(101, 0, "DronefindTarget", %drone);
   schedule(102, 0, "WSScanGround", %drone);

   InitiateBoss(%drone, "CnlWindshear");
   
   %drone.turretObject.team = %team;
   setTargetSensorGroup(%drone.turretObject.getTarget(), %team);
   %drone.turretObject2.team = %team;
   setTargetSensorGroup(%drone.turretObject2.getTarget(), %team);
   %drone.turretObject3.team = %team;
   setTargetSensorGroup(%drone.turretObject3.getTarget(), %team);
   %drone.turretObject4.team = %team;
   setTargetSensorGroup(%drone.turretObject4.getTarget(), %team);
   %drone.turretObject5.team = %team;
   setTargetSensorGroup(%drone.turretObject5.getTarget(), %team);    
   
   //Helpers
   %pos2 = vectoradd(%pos, "50 0 0");
   %pos3 = vectoradd(%pos, "-50 0 0");
   %d2 = StartAIGunship(%pos2, "0 0 0 1", %team, "ace", 0);
   %d3 = StartAIGunship(%pos3, "0 0 0 1", %team, "ace", 0);
   %d2.isUltrally = 1;
   %d3.isUltrally = 1;
   //
   
   WindshearAttacks(%drone);

   return %drone;
}

function WSForwardImpulse(%obj){
   if(!isObject(%obj)) {
	  return;
   }
   if(vectorLen(%obj.getVelocity()) < 500) {
      %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(),$Drone::FrdImpulse));
   }
   schedule(100, 0, "WSForwardImpulse", %obj);
}

function WSScanGround(%obj){
   if(!isObject(%obj))
	return;
   %vec = %obj.getForwardVector();
   %vector = vectorAdd(%obj.getPosition(),"0 0 -350");
   %searchResult = containerRayCast(%obj.getWorldBoxCenter(), %vector, $TypeMasks::TerrainObjectType, %obj);
   if(%searchResult){
	%z = getWord(%vec,2);
      %height = vectorDist(%obj.getPosition(),posFromRaycast(%searchresult));
	if(%z < 0){
	   if(%height <= (200 + ((%z * -1) * 300))){
		%obj.dodgeground = 1;
		schedule(100, 0, "WSDodgeGround", %obj);
		return;
	   }
	}
   }
   schedule(100, 0, "WSScanGround", %obj);
}

function WSDodgeGround(%obj){
   if(!isObject(%obj))
	return;
   %vec = %obj.getForwardVector();
   %z = getWord(%vec,2);
   if(%z > 0){
	%obj.dodgeground = 0;
	schedule(100, 0, "DronefindTarget", %obj);
	schedule(101, 0, "WSScanGround", %obj);
	return;
   }
   %pos = vectorAdd(%obj.getPosition(),%vec);
   %obj.applyImpulse(%pos,vectorScale("0 0 1",$Drone::TurnImpulse * %obj.skill));
   schedule(100, 0, "WSDodgeGround", %obj);
}

function DroneFindNearestPilot(%radius, %drone) {
   %pos = %drone.getposition();
   %wbpos = %drone.getworldboxcenter();
   %count = ClientGroup.getCount();
   %closestClient = -1;
   %closestDistance = %radius;
   for(%i = 0; %i < %count; %i++)
   {
	%cl = ClientGroup.getObject(%i);
	if(isObject(%cl.player)){
	   %testPos = %cl.player.getWorldBoxCenter();
	   %distance = vectorDist(%wbpos, %testPos);
	   if (%distance > 0 && %distance < %closestDistance)
	   {
	   	%closestClient = %cl;
	   	%closestDistance = %distance;
	   }
	}
   }
   return %closestClient;
}

function WindshearAttacks(%drone) {
   if(!isObject(%drone)) {
      return;
   }
   schedule(30000, 0, "WindshearAttacks", %drone);
   %rand = getRandom(1, 4);
   switch(%rand) {
      case 1:
          schedule(700, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(1400, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(2100, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(2800, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(3500, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(4200, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(4900, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(5600, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(6300, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(7000, 0, "WindshearAttack_FUNC", "Flare", %drone);
          // Quick Shots
          schedule(8000, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(8100, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(8200, 0, "WindshearAttack_FUNC", "Flare", %drone);
          schedule(8300, 0, "WindshearAttack_FUNC", "Flare", %drone);
          MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Windshear"]@": Did you actually think those missiles could hit me?");
      case 2:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
            WindshearAttack_FUNC("SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(700, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(1400, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(2100, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(2800, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(3500, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(4200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(4900, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(5600, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(6300, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(7000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            // Quick Shots
            schedule(8000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(8100, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(8200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(8300, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Windshear"]@": I've got missiles with "@getTaggedString(%target.name)@"'s name on them");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Windshear"]@": No missiles for now....");
         }
      case 3:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
            schedule(700, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(1400, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(2100, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(2800, 0, "WindshearAttack_FUNC", "Flare", %drone);
            WindshearAttack_FUNC("SidewinderLaunch", %drone SPC %target.player SPC 3);
            // Quick Shots
            schedule(1000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(1100, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(1200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(1300, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(1400, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Windshear"]@": I'ma kill me a "@getTaggedString(%target.name)@"!");
         }
         else {
            schedule(700, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(1400, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(2100, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(2800, 0, "WindshearAttack_FUNC", "Flare", %drone);
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Windshear"]@": FLARES!");
         }
      default:
            schedule(700, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(1400, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(2100, 0, "WindshearAttack_FUNC", "Flare", %drone);
            schedule(2800, 0, "WindshearAttack_FUNC", "Flare", %drone);
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Windshear"]@": FLARES! NOW!!!");
   }
}

//Attack Function
function WindshearAttack_FUNC(%att, %args) {
   switch$(%att) {
      case "Flare":
         %d = getWord(%args, 0);
         if(!isObject(%d)) {
            return;
         }
         %SPos = vectorAdd(%d.getPosition(),"0 0 0");
         %p1 = new FlareProjectile() {
             dataBlock        = FlareGrenadeProj;
             initialDirection = "5 0 -3";
             initialPosition  = %SPos;
   	         sourceObject     = %d;
   	         sourceSlot       = 6;
         };
         %p2 = new FlareProjectile() {
   	         dataBlock        = FlareGrenadeProj;
             initialDirection = "-5 0 -3";
             initialPosition  = %SPos;
   	         sourceObject     = %d;
   	         sourceSlot       = 6;
         };
         FlareSet.add(%p1);
         FlareSet.add(%p2);
         MissionCleanup.add(%p1);
         MissionCleanup.add(%p2);
      case "HeatLoop":
         %t = getWord(%args, 0);
         %v = getWord(%args, 1);
         
         if(!isObject(%t) || %t.getState() $= "dead") {
            return;
         }
         if(%v > 200) {
            return;
         }
         %v++;
         %t.setHeat(100);
         schedule(100, 0, "WindshearAttack_FUNC", "HeatLoop", %t SPC %v);
      case "MissileDrop":
         %t = getWord(%args, 0);
         %m = getWord(%args, 1);
      
         %t.setHeat(100);
         %m.setObjectTarget(%t);
         //HeatLoop(%t,0);
         WindshearAttack_FUNC("HeatLoop", %t SPC 0);
      case "SidewinderLaunch":
         %d = getWord(%args, 0);
         %t = getWord(%args, 1);
         %TTL = getWord(%args, 2);
         
         if(!isObject(%d) || !isObject(%t) || %t.getState() $= "dead") {
            return;
         }
         %SPos = vectorAdd(%d.getPosition(),"0 0 3");
         %p1 = new SeekerProjectile() {
             dataBlock        = BossMissiles;
             initialDirection = "0 0 10";
             initialPosition  = %SPos;
   	         sourceObject     = %d;
   	         sourceSlot       = 6;
         };
         MissionCleanup.add(%p1);
         schedule(%TTL * 1000, 0, "WindshearAttack_FUNC", "MissileDrop", %t SPC %p1);
   }
}

