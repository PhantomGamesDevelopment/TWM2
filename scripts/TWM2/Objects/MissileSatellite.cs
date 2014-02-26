//
datablock StaticShapeData(MissileShape) : StaticShapeDamageProfile {
   shapeFile      = "weapon_missile_projectile.dts";
   mass           = 1.0;
   repairRate     = 0;
   dynamicType    = $TypeMasks::StaticShapeObjectType;
   heatSignature  = 0;
};

function CruiseMissileVehicle::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);
   setTargetSensorGroup(%obj.getTarget(), %obj.team);

   %body = new StaticShape() {
       scale = "20 20 20";
       datablock = MissileShape;
   };
   MissionCleanup.add(%body);
   %obj.mountObject(%body, 1);
   %body.vehicleMounted = %obj;

   %obj.startFade(0,100,1);
}

function CruiseMissileVehicle::deleteAllMounted(%data, %obj) {

   %body = %obj.getMountNodeObject(1);
   if(!%body) {
      return;
   }
   %body.delete();
}
//
function CreateMissileSat(%client, %unlim, %rem) {
   if(%unlim $= "" || !%unlim) {
      %unlim = 0;
      %rem = 0;
   }
   
   if($Killstreak::GunshipSpawnLocation[$CurrentMission] $= "") {
      %spawn = "0 -1000 400";
   }
   else {
      %spawn = $Killstreak::GunshipSpawnLocation[$CurrentMission];
   }
   %sat = new FlyingVehicle() {
      dataBlock    = UAVVehicle;
      position     = %spawn;
      rotation     = "0 0 0 1";
      team         = %client.team;
   };
   MissionCleanUp.add(%sat);
   setTargetSensorGroup(%sat.getTarget(), %client.team);

   %sat.GoPoint = 1;
   GunshipForwardImpulse(%sat);
   %sat.ScanLoop = schedule(500, 0, "GetNextGunshipPoint", %sat);
   %client.player.lastTransformStuff = %client.player.getTransform();
   
   %sat.team = %client.Team;
   %sat.setOwner(%client.player);
   
   %sat.canLaucnhStrike = 1;
   %sat.isUnlimitedSat = %unlim;
   
   MessageClient(%client, 'msgSatcom', "\c3UAMS: Satellite Moving to Position, Standby....");
   
   if(!%unlim) {
      %client.player.setPosition(VectorAdd(%x SPC %y SPC 0,$Prison::JailPos));
   
      %client.setControlObject(%sat.turretObject);
      %client.schedule(499, setControlObject, %sat.turretObject);
      MissileSatControlLoop(%client, %sat);
   }
   else {
      %client.setControlObject(%sat.turretObject);
      commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%sat.turretObject,%client.player));
   }
   
   if(%rem) {
      %sat.turretObject.setAutoFire(true);
   }
}

function FireSatHornet(%sat, %slot, %source) {
   %muzzlePos = %sat.getMuzzlePoint(%slot);
   %muzzleVec = %sat.getMuzzleVector(%slot);
   //Fiah
   spawnprojectileSourceMod(HornetStrikeMissile, SeekerProjectile, %muzzlePos, %muzzleVec, %source);
   ServerPlay3d(EscapePodLaunchSound, %sat.getPosition());
   ServerPlay3d(EscapePodLaunchSound2, %sat.getPosition());
}

function MissileSatelliteBarrel::onFire(%data, %obj, %slot) {
   //echo(%obj);
   if(%obj.mountobj.canLaucnhStrike) {
      %client = %obj.getControllingClient();
      %source = %client.player; //muhaha
   
      %obj.mountobj.canLaucnhStrike = 0;

      FireSatHornet(%obj, %slot, %source);
      schedule(1500, 0, "FireSatHornet", %obj, %slot, %source);
      schedule(3000, 0, "FireSatHornet", %obj, %slot, %source);

      if(!%obj.mountobj.isUnlimitedSat) {
         schedule(4000, 0, "MakeCruiseMissile", %client, %obj);
         schedule(4000, 0, "ServerPlay3d", EscapePodLaunchSound2, %obj.getPosition());
      }
      else {
         schedule(30000, 0, "ResetSat", %obj.mountobj);
         //
      }
   }
   else {
      if(!%obj.mountobj.isUnlimitedSat) {
         return;
      }
      %client = %obj.getControllingClient();
      bottomPrint(%client, "Missiles are still reloading... standby.", 2, 2);
      if(isObject(%client.player) && %client.player.getState() !$= "dead") {
         %client.setControlObject(%client.player);
      }
   }
}

function ResetSat(%sat) {
   if(isObject(%sat)) {
      %sat.canLaucnhStrike = 1;
   }
}

function MakeCruiseMissile(%client, %sat) {
   if(%client.getControlObject() != %sat) {
      return;
   }
   %Missile = new FlyingVehicle() {
       dataBlock = "CruiseMissileVehicle";
       scale = "1 1 1";
       team = %client.team;
       mountable = "0"; //drive only
   };
   
   setTargetSensorGroup(%Missile.getTarget(), %Missile.team);
   %Missile.setTransform(vectorAdd(%sat.getPosition(), "0 0 -5") SPC rotFromTransform(%sat.getTransform()));
   
   %Missile.controller = %client;
   %sat.GuidedMissile = %Missile;
   MissionCleanup.add(%Missile);
   %client.setControlObject(%Missile);
   
   MissileSatGuidedLoop(%client, %Missile);
}

function ReMoveClientSW(%client) {
   if(!isObject(%client.player) || %client.player.getState() $= "dead") {
      return;
   }
   else {
      %sp = Game.pickPlayerSpawn(%client, false);
      //2 sec Invincibility please?
      %client.player.setInvinc(1);
      %client.player.schedule(2000, "setInvinc", 0);
      %client.player.setTransform(%client.player.lastTransformStuff);  //%sp for new spawn
      %client.setControlObject(%client.player);
   }
}

//just a good function to delete the satelite if the client reliquishes control
function MissileSatControlLoop(%client, %sat) {
   if(!isObject(%sat)) {
      if(isObject(%client.player)) {
         ReMoveClientSW(%client);
      }
      return;
   }
   if((%client.getControlObject() != %sat.turretObject) && !%sat.isUnlimitedSat) {
      //lets check if they are in the missile now...
      if(%client.getControlObject() == %sat.turretObject.GuidedMissile) {
         //MissileSatGuidedLoop(%client, %sat.turretObject.GuidedMissile);
      }
      //No, they reliquished all control before the guided fired...
      else {
         if(isObject(%client.player)) {
            ReMoveClientSW(%client);
         }
      }
      %sat.schedule(1000, "Delete");
      return;
   }
   //%client.setControlObject(%sat.turretObject);
   schedule(100, 0, "MissileSatControlLoop", %client, %sat);
}

function MissileSatGuidedLoop(%client, %missile) {
   if(%client.getControlObject() != %missile) {
      if(isObject(%client.player)) {
         ReMoveClientSW(%client);
      }
      return;
   }
   schedule(100, 0, "MissileSatGuidedLoop", %client, %missile);
}
