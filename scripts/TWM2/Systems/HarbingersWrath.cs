//Necessar Edits
function SetMissileTargeted(%target, %proj) {
   %target.missileLaunch = %proj;
   %target.MissileChase[%proj] = 1;
   MissileCheckLoop(%proj, %target);
}

function MissileCheckLoop(%projectile, %target) {
   if(!isObject(%projectile)) {
      %target.MissileChase[%projectile] = 0;
      %target.missileLaunch = 0;
      return;
   }
   if(%target.isPlayer()) {
      if(!isObject(%target) || %target.getState() $= "dead") {
         return;
      }
   }
   else {
      if(!isObject(%target)) {
         return;
      }
   }
   %target.MissileChase[%projectile] = 1;
   %target.missileLaunch = %projectile;
   schedule(100, 0, "MissileCheckLoop", %projectile, %target);
}

//$HarbinsWrath::PointVec -> Points in our Jaws Circling On Targets :p, fun and evil
$HarbinsWrath::PointVec[1] = "0 -400 400";
$HarbinsWrath::PointVec[2] = "-750 0 400";
$HarbinsWrath::PointVec[3] = "0 400 400";
$HarbinsWrath::PointVec[4] = "750 0 400";

function StartAC130(%client, %unmanned, %unlim) {
   if(%unlim $= "") {
      %unlim = 0;
   }
   //
   if($Killstreak::GunshipSpawnLocation[$CurrentMission] $= "") {
      %spawn = "0 -1000 400";
   }
   else {
      %spawn = $Killstreak::GunshipSpawnLocation[$CurrentMission];
   }
   %obj = new FlyingVehicle() {
      dataBlock    = AC130;
      position     = %spawn;
      rotation     = "0 0 0 1";
      team         = %client.team;
   };
   MissionCleanUp.add(%obj);
   %obj.TurretObject.barrel = "Chain";
   %obj.TurretObject.schedule(2000, SetFrozenState, false);
   %obj.TurretObject.schedule(2000, SetMoveState, false);

   %obj.isHarbinsWrathShip = 1;
   %obj.isUltrAlly = 1; // ah what the heck, you should get 1000 XP for blowing one of these
                        // bastages up.

   setTargetSensorGroup(%obj.getTarget(), %client.team);

   %obj.GoPoint = 1;
   %obj.CanFlare = 1;
   GunshipForwardImpulse(%obj);
   %obj.ScanLoop = GetNextGunshipPoint(%obj);

   //Unlimited Gunships do not have player gunners, these are for missions and stuff :p
   if(!%unlim) {
      schedule($TWM2::GunshipControlTime*1000, 0, "EndGunship", %obj, %client);
      if(!%unmanned) {
         %obj.isKillstreakVehicle = 1;
         //%client.schedule(1000, "setControlObject", %obj.turretObject);
     	 //commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%obj.turretObject,%client.player));
         %client.gunshipControlLoop = schedule(1000, 0, "GunshipControlLoop", %client, %obj);
         messageClient(%client, 'msgControls', "\c3GUNSHIP: Press the [Mine] key to toggle weapons");
         %client.player.lastTransformStuff = %client.player.getTransform();
         //%client.player.setPosition(VectorAdd(%x SPC %y SPC 0,$Prison::JailPos));
         %client.player.getDataBlock().onCollision(%client.player, %obj, 1);
         
         %client.inKillstreak = 1;
      }
      else {
         SwitchACGunLoop(%obj); //randomly switch weapons
      }
   }
   else {
      SwitchACGunLoop(%obj); //randomly switch weapons
   }
}

function StartHarbingersWrath(%client, %unmanned, %unlim) {
   if(%unlim $= "") {
      %unlim = 0;
   }
   //
   if($Killstreak::GunshipSpawnLocation[$CurrentMission] $= "") {
      %spawn = "0 -1000 400";
   }
   else {
      %spawn = $Killstreak::GunshipSpawnLocation[$CurrentMission];
   }
   %obj = new FlyingVehicle() {
      dataBlock    = HarbingerGunship;
      position     = %spawn;
      rotation     = "0 0 0 1";
      team         = %client.team;
   };
   MissionCleanUp.add(%obj);
   %obj.TurretObject.barrel = "Chain";
   %obj.TurretObject.schedule(2000, SetFrozenState, false);
   %obj.TurretObject.schedule(2000, SetMoveState, false);
   
   %obj.isHarbinsWrathShip = 1;
   %obj.isUltrAlly = 1; // ah what the heck, you should get 1000 XP for blowing one of these
                        // bastages up.

   setTargetSensorGroup(%obj.getTarget(), %client.team);

   %obj.GoPoint = 1;
   %obj.CanFlare = 1;
   GunshipForwardImpulse(%obj);
   %obj.ScanLoop = GetNextGunshipPoint(%obj);
   
   //Unlimited Gunships do not have player gunners, these are for missions and stuff :p
   if(!%unlim) {
      schedule($TWM2::GunshipControlTime*1000, 0, "EndGunship", %obj, %client);
      if(!%unmanned) {
         %obj.isKillstreakVehicle = 1;
         //%client.schedule(1000, "setControlObject", %obj.turretObject);
     	 //commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%obj.turretObject,%client.player));
         %client.gunshipControlLoop = schedule(1000, 0, "GunshipControlLoop", %client, %obj);
         messageClient(%client, 'msgControls', "\c3GUNSHIP: Press the [Mine] key to toggle weapons");
         %client.player.lastTransformStuff = %client.player.getTransform();
         //%client.player.setPosition(VectorAdd(%x SPC %y SPC 0,$Prison::JailPos));
         %client.player.getDataBlock().onCollision(%client.player, %obj, 1);
         
         %client.inKillstreak = 1;
      }
      else {
         SwitchHarbieGunLoop(%obj); //randomly switch weapons
      }
   }
   else {
      SwitchHarbieGunLoop(%obj); //randomly switch weapons
   }
}

function ResetFlare(%obj) {
   %obj.CanFlare = 1;
}

function GunshipControlLoop(%client, %gunship) {
   if(!isObject(%gunship)) {
      if(isObject(%client.player)) {
         ReMoveClientSW(%client);
      }
      return;
   }
   //Remember, we're controlling the turret
   if(%client.getControlObject() != %gunship.turretObject) {
      if(isObject(%client.player)) {
         ReMoveClientSW(%client);
      }
      EndGunship(%gunship, %client);
      return;
   }
   %client.gunshipControlLoop = schedule(100, 0, "GunshipControlLoop", %client, %gunship);
}


function EndGunship(%obj, %client) {
   if(!isObject(%obj)) {
	  return;
   }
   //3.7 Dismount from gunship
   %client.player.unmount();
   %client.inKillstreak = 0;
   
   Cancel(%obj.ScanLoop);
   ReMoveClientSW(%client);
   Cancel(%client.gunshipControlLoop);
   GunshipMoveToPoint(%obj, "0 90000 1000");
   %obj.schedule(25000, "setCloaked", true);
   %obj.schedule(30000, "Delete");
   //
   %obj.CanFlare = 0;
   schedule(700,0,"FireFlares",%obj);
   schedule(1400,0,"FireFlares",%obj);
   schedule(2100,0,"FireFlares",%obj);
   schedule(2800,0,"FireFlares",%obj);
   schedule(3500,0,"FireFlares",%obj);
   //
   schedule(9000,0,"FireFlares",%obj);
   schedule(9700,0,"FireFlares",%obj);
   schedule(10400,0,"FireFlares",%obj);
   schedule(11000,0,"FireFlares",%obj);
   schedule(11700,0,"FireFlares",%obj);
}

function GunshipForwardImpulse(%obj){
   if(!isObject(%obj)) {
	  return;
   }
   if(vectorLen(%obj.getVelocity()) < 165 && %obj.isTurning) {
	  %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), $Drone::FrdImpulse * 0.95));
   }
   else if(vectorLen(%obj.getVelocity()) < 230) {
	  %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), $Drone::FrdImpulse * 0.7));
   }
   //missile incoming check
   if(%obj.missileLaunch != 0) {
      //oh sheet! incomith!
      if(%obj.CanFlare) {
         //Fire the flares!! hurry!!!
         %obj.CanFlare = 0;
         schedule(700,0,"FireFlares",%obj);
         schedule(1400,0,"FireFlares",%obj);
         schedule(2100,0,"FireFlares",%obj);
         schedule(2800,0,"FireFlares",%obj);
         schedule(3500,0,"FireFlares",%obj);
         schedule(15000, 0, "ResetFlare", %obj);
      }
   }
   schedule(100, 0, "GunshipForwardImpulse", %obj);
}

function GetNextGunshipPoint(%obj) {
   //echo("getting point");
   if(!isObject(%obj)) {
      return;
   }
   %team = %obj.team;
   //echo("team: "@%team@"");
   //Radius Scan for enemy players to "Circle"
   InitContainerRadiusSearch(%obj.getposition(), 9999, $TypeMasks::PlayerObjectType);
   while ((%target = containerSearchNext()) != 0) {
      if(%target.team != %team) {
         //echo("got one! "@%target@"");
         //we have the closest target, lets get our current position aspect, and move the gunship
         %obj.GoPoint++;
         %tpos = $HarbinsWrath::PointVec[%obj.goPoint];
         %posToGo1 = VectorAdd(%tpos, %target.getPosition());
         //Modified 2.7, lets stop gunships from climbing due to grapple noobs
         %groundPos = getTerrainHeight(%posToGo1);
         %zPosToGo = getWord(%groundPos, 2) + getWord(%tpos, 2);
         %posToGo = getWords(%posToGo1, 0, 1) SPC %zPosToGo;
         //End 2.7 modification
         if($HarbinsWrath::PointVec[%obj.goPoint] $= "") {
            %obj.GoPoint = 0;
         }
         //echo("Go to "@%posToGo@"");
         %obj.ScanLoop = GunshipMoveToPoint(%obj, %posToGo);
         return;
      }
   }
   %obj.ScanLoop = schedule(500, 0, "GetNextGunshipPoint", %obj);
}

function GunshipMoveToPoint(%obj, %Tpos){
   if(!isObject(%obj)) {
      return;
   }

   %pos = %obj.getPosition();
   %objfrd = %obj.getForwardVector();
   %objup = %obj.getUpVector();

   %dist = vectorDist(%pos,%Tpos);
   %aimvec = vectorNormalize(vectorSub(%Tpos,%pos));

   %vec = vectorSub(%aimvec, %objfrd);
   %vec = vectorCross(%vec, %objfrd);
   %vec = vectorNormalize(vectorCross(%objfrd, %vec));
   if(vectorDist(%objfrd,vectorNormalize(%aimvec)) < 0.1) {
	  %obj.applyImpulse(vectorAdd(%obj.getPosition(),vectorScale(%objfrd,($Drone::TurnImpulse / 2))),%vec);
   }
   else if(vectorDist(%objup, %vec) > 0.1){
	  %vec = vectorSub(%vec, %objup);
	  %vec = vectorCross(%vec, %objup);
	  %vec = vectorNormalize(vectorCross(%objup, %vec));
	  %pos = vectorAdd(%obj.getPosition(),vectorScale(%objup,$Drone::TurnImpulse * 2));
	  %obj.applyImpulse(%pos,%vec);
      %obj.isTurning = 1;
   }
   else {
	  %obj.applyImpulse(vectorAdd(%obj.getPosition(),%objfrd),vectorScale(%vec,$Drone::TurnImpulse));
   }

   if(%dist < 22) {
      //echo("getting next pos");
      %obj.isTurning = 0;
	  %obj.ScanLoop = schedule(100, 0, "GetNextGunshipPoint", %obj);
   }
   else {
      //echo("moving");
      %obj.ScanLoop = schedule(100, 0, "GunshipMoveToPoint", %obj, %tpos);
   }
}
