//DBS
datablock PlayerData(LightningGhostArmor) : MediumMaleHumanArmor       /// ooooo scary
{
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 550.0;
   minImpactSpeed = 35;
   shapeFile = "medium_male.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
	max[SmokeGrenade]			= 0;
	max[BeaconSmokeGrenade]		= 0;
	max[Blaster]			= 0;
	max[Plasma]				= 0;
	max[PlasmaAmmo]			= 0;
	max[Disc]				= 0;
	max[DiscAmmo]			= 0;
	max[SniperRifle]			= 0;
	max[GrenadeLauncher]		= 0;
	max[GrenadeLauncherAmmo]	= 0;
	max[Mortar]				= 0;
	max[MortarAmmo]			= 0;
	max[MissileLauncher]		= 0;
	max[MissileLauncherAmmo]	= 0;
	max[Chaingun]			= 0;
	max[ChaingunAmmo]			= 0;
	max[RepairGun]			= 0;
	max[CloakingPack]			= 0;
	max[SensorJammerPack]		= 0;
	max[EnergyPack]			= 0;
	max[RepairPack]			= 0;
	max[ShieldPack]			= 0;
	max[AmmoPack]			= 0;
	max[SatchelCharge]		= 0;
	max[MortarBarrelPack]		= 0;
	max[MissileBarrelPack]		= 0;
	max[AABarrelPack]			= 0;
	max[PlasmaBarrelPack]		= 0;
	max[ELFBarrelPack]		= 0;
	max[artillerybarrelpack]	= 0;
	max[MedPack]			= 0;
	max[InventoryDeployable]	= 0;
	max[MotionSensorDeployable]	= 0;
	max[PulseSensorDeployable]	= 0;
	max[TurretOutdoorDeployable]	= 0;
	max[TurretIndoorDeployable]	= 0;
	max[FlashGrenade]			= 0;
	max[ConcussionGrenade]		= 0;
	max[FlareGrenade]			= 0;
	max[TargetingLaser]		= 0;
	max[ELFGun]				= 0;
	max[ShockLance]			= 0;
	max[CameraGrenade]		= 0;
	max[Beacon]				= 0;
	max[flamerAmmoPack]		= 0;
	max[ParachutePack]		= 0;
	max[ConstructionTool]		= 0;
	max[MergeTool]			= 0;
	max[NerfGun]			= 0;
	max[NerfBallLauncher]		= 0;
	max[NerfBallLauncherAmmo]	= 0;
	max[SuperChaingun]		= 0;
	max[SuperChaingunAmmo]		= 0;
	max[RPChaingun]			= 0;
	max[RPChaingunAmmo]		= 0;
	max[MGClip]				= 0;
	max[LSMG]				= 0;
	max[LSMGAmmo]			= 0;
	max[LSMGClip]			= 0;
	max[snipergun]			= 0;
	max[snipergunAmmo]		= 0;
	max[Bazooka]			= 0;
	max[BazookaAmmo]			= 0;
	max[BunkerBuster]				= 0;
	max[MG42]				= 0;
	max[MG42Ammo]			= 0;
	max[SPistol]			= 0;
	max[Pistol]				= 0;
	max[PistolAmmo]			= 0;
	max[Pistolclip]			= 0;
	max[flamer]				= 0;
	max[flamerAmmo]			= 0;
	max[AALauncher]			= 0;
	max[AALauncherAmmo]		= 0;
	max[melee]				= 0;
	max[SOmelee]			= 0;
	max[KriegRifle]			= 0;
	max[KriegAmmo]			= 0;
	max[Rifleclip]			= 0;
	max[Shotgun]			= 0;
	max[ShotgunAmmo]			= 0;
	max[ShotgunClip]			= 0;
	max[RShotgun]			= 0;
	max[RShotgunAmmo]			= 0;
	max[RShotgunClip]			= 0;
	max[LMissileLauncher]		= 0;
	max[LMissileLauncherAmmo]	= 0;
	max[HRPChaingun]			= 0;
	max[RPGAmmo]			= 0;
	max[RPGItem]			= 0;
	max[spineDeployable]		= 0;
	max[mspineDeployable]		= 0;
	max[wWallDeployable]		= 0;
	max[floorDeployable]		= 0;
	max[WallDeployable]		= 0;
      max[DoorDeployable]           = 0;
	max[TurretLaserDeployable]	= 0;
	max[TurretMissileRackDeployable]= 0;
	max[DiscTurretDeployable]	= 0;
	max[EnergizerDeployable]	= 0;
	max[TreeDeployable]		= 0;
	max[CrateDeployable]		= 0;
	max[DecorationDeployable]	= 0;
	max[LogoProjectorDeployable]	= 0;
	max[LightDeployable]		= 0;
	max[TripwireDeployable]		= 0;
	max[TelePadPack]			= 0;
	max[TurretBasePack]		= 0;
	max[LargeInventoryDeployable]	= 0;
	max[GeneratorDeployable]	= 0;
	max[SolarPanelDeployable]	= 0;
	max[SwitchDeployable]		= 0;
	max[MediumSensorDeployable]	= 0;
	max[LargeSensorDeployable]	= 0;
	max[SpySatelliteDeployable]	= 0;
	max[JumpadDeployable]		= 0;
	max[EscapePodDeployable]	= 0;
	max[ForceFieldDeployable]	= 0;
	max[GravityFieldDeployable]	= 0;
      max[VehiclePadPack]		= 0;
};

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
