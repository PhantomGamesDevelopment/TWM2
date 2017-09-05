//DATABLOCKS
datablock StaticShapeData(NoCollideBio) : StaticShapeDamageProfile {
	className = "player";
	shapeFile = "bioderm_light.dts"; // dmiscf.dts, alternate
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 0;
    isInvincible = true;
};

datablock StaticShapeData(NoCollideHum) : StaticShapeDamageProfile {
	className = "player";
	shapeFile = "light_male.dts"; // dmiscf.dts, alternate
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 0;
    isInvincible = true;
};

//Stops console spammage
function NoCollideBio::shouldApplyImpulse(%targetObject) {
   return false;
}
function NoCollideHum::shouldApplyImpulse(%targetObject) {
   return false;
}

datablock PlayerData(InsigniaZombieArmor) : LightMaleBiodermArmor {
   maxDamage = 700.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;

   damageScale[$DamageType::M1700] = 2.0;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
	max[SmokeGrenade]			= 0;
	max[BeaconSmokeGrenade]		= 0;
	max[Blaster]			= 0;
	max[Plasma]			= 0;
	max[PlasmaAmmo]			= 0;
	max[Disc]			= 0;
	max[DiscAmmo]			= 0;
	max[SniperRifle]		= 0;
	max[GrenadeLauncher]		= 0;
	max[GrenadeLauncherAmmo]	= 0;
	max[Mortar]			= 0;
	max[MortarAmmo]			= 0;
	max[MissileLauncher]		= 0;
	max[MissileLauncherAmmo]	= 0;
	max[Chaingun]			= 0;
	max[ChaingunAmmo]		= 0;
	max[RepairGun]			= 0;
	max[CloakingPack]		= 0;
	max[SensorJammerPack]		= 0;
	max[EnergyPack]			= 0;
	max[RepairPack]			= 0;
	max[ShieldPack]			= 0;
	max[AmmoPack]			= 0;
	max[SatchelCharge]		= 0;
	max[MortarBarrelPack]		= 0;
	max[MissileBarrelPack]		= 0;
	max[AABarrelPack]		= 0;
	max[PlasmaBarrelPack]		= 0;
	max[ELFBarrelPack]		= 0;
	max[artillerybarrelpack]	= 0;
	max[InventoryDeployable]	= 0;
	max[MotionSensorDeployable]	= 0;
	max[PulseSensorDeployable]	= 0;
	max[TurretOutdoorDeployable]	= 0;
	max[TurretIndoorDeployable]	= 0;
	max[FlashGrenade]		= 0;
	max[ConcussionGrenade]		= 0;
	max[FlareGrenade]		= 0;
	max[TargetingLaser]		= 0;
	max[ELFGun]			= 0;
	max[ShockLance]			= 0;
	max[CameraGrenade]		= 0;
	max[Beacon]			= 0;
	max[flamerAmmoPack]		= 0;
	max[ParachutePack]		= 0;
	max[MedPack]			= 0;
	//Guns
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
	max[nukeme]				= 0;
	max[nukemeAmmo]			= 0;
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
	//Building parts
	max[spineDeployable]		= 0;
	max[mspineDeployable]		= 0;
	max[wWallDeployable]		= 0;
	max[floorDeployable]		= 0;
	max[WallDeployable]		= 0;
      max[DoorDeployable]           = 0;
	//Turrets
	max[TurretLaserDeployable]	= 0;
	max[TurretMissileRackDeployable]= 0;
	max[DiscTurretDeployable]	= 0;
	//Largepacks
	max[EnergizerDeployable]	= 0;
	max[TreeDeployable]		= 0;
	max[CrateDeployable]		= 0;
	max[DecorationDeployable]	= 0;
	max[LogoProjectorDeployable]	= 0;
	max[LightDeployable]		= 0;
	max[TripwireDeployable]		= 0;
	max[TelePadPack]		= 0;
	max[TurretBasePack]		= 0;
	max[LargeInventoryDeployable]	= 0;
	max[GeneratorDeployable]	= 0;
	max[SolarPanelDeployable]	= 0;
	max[SwitchDeployable]		= 0;
	max[MediumSensorDeployable]	= 0;
	max[LargeSensorDeployable]	= 0;
	max[SpySatelliteDeployable]	= 0;
	//Misc
	max[JumpadDeployable]		= 0;
	max[EscapePodDeployable]	= 0;
	max[ForceFieldDeployable]	= 0;
	max[GravityFieldDeployable]	= 0;
      max[VehiclePadPack]		= 0;
};

//CREATION
function SpawnInsignia(%position) {
   %Zombie = new player(){
      Datablock = "InsigniaZombieArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('msgBossAlertreturn', "\c4"@$TWM2::BossName["Insignia"]@": The battle begins, and now you shall all die...");

   %command = "Insigniamovetotarget";
   %zombie.ticks = 0;
   InitiateBoss(%zombie, "Insignia");
   schedule(5000, 0, InsigniaAttack_FUNC, "ZombieSummon", %zombie);
   InsigniaAttack(%zombie);

   %Zombie.team = 30;
   %zname = $TWM2::BossName["Insignia"]; // <- To Hosts, Enjoy, You can
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


//AI

function Insigniamovetotarget(%zombie){
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
      %zombie.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), TWM2Lib_MainControl("getRandomPosition", 25 TAB 1)));
      %zombie.setVelocity("0 0 0");
      MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I won't go away that easily...");
   }
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){
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
   %zombie.moveloop = schedule(500, %zombie, "Insigniamovetotarget", %zombie);
}

//
function InsigniaAttack_FUNC(%att, %args) {
   switch$(%att) {
      case "ZombieSummon":
         %z = getWord(%args, 0);
         if(!isobject(%z) || %z.getState() $= "dead") {
            return;
         }
         schedule(30000, 0, InsigniaAttack_FUNC, "ZombieSummon", %z);
         //--------------------
         %type = getRandomZombieType("1 2 3 5 9 12 13 15 17");   //omit 4 in place of 17: Demon -> Elite Demon
         messageall('RogMsg',"\c4"@$TWM2::BossName["Insignia"]@": Slay the humans!!!");
         for(%i = 0; %i < 6; %i++) {
            %pos = vectoradd(%z.getPosition(), TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
            %fpos = vectoradd("0 0 5",%pos);
            StartAZombie(%fpos, %type);
         }
         %z.setMoveState(true);
         %z.setActionThread($Zombie::RAAMThread, true);
         %z.schedule(3500, "setMoveState", false);
      
      case "Reinforce":
         %zombie = getWord(%args, 0);
         %type = getRandomZombieType("1 2 3 5 9 12 13 15 17");
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Insignia"]@": It's time for you to take on my reinforcements!");
         %typeCaller = %type SPC %type SPC %type SPC %type;
         %callPos1 = vectorAdd(%zombie.getPosition(), "2000 100 400");
         spawnHunterDropship(%callPos1, "AA", %typeCaller);
         %callPos2 = vectorAdd(%zombie.getPosition(), "2000 -100 400");
         spawnHunterDropship(%callPos2, "AA", %typeCaller);
   
      case "GravShot":
         %plyr = getWord(%args, 0);
         %vec = getWords(%args, 1);
         if(%plyr.isAlive()) {
            %plyr.applyImpulse(%plyr.getPosition(), VectorScale(%vec, 2000));
            %plyr.flight = 1;
            schedule(4000, 0, "eval", ""@%plyr@".flight = 0;");
            for( %i = 0; %i < 20; %i++ ) {
               schedule(%i * 80, 0, InsigniaAttack_FUNC, "Projection_Bio", %plyr);
            }
         }

      case "Projection_Bio":
         %plyr = getWord(%args, 0);
         if(isPlayer(%plyr)) {
            %trans2 = %plyr.getTransform();
            %player = new StaticShape(){
               Datablock = "NoCollideBio";
            };
            %player.setTransform(%trans2);
            %player.startfade(2000, 0, true);
            %player.schedule(5000, "Delete");
         }

      case "Projection_Hum":
         %plyr = getWord(%args, 0);
         if(isPlayer(%plyr)) {
            %trans2 = %plyr.getTransform();
            %player = new StaticShape(){
               Datablock = "NoCollideHum";
            };
            %player.setTransform(%trans2);
            %player.startfade(2000, 0, true);
            %player.schedule(5000, "Delete");
         }
         
      case "AcidShot_Single":
         %zombie = getWord(%args, 0);
         %target = getWord(%args, 1);
         if(isobject(%zombie) && isobject(%target)){
            %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(6));
            %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));
            %p = new TracerProjectile() {
               dataBlock        = LZombieAcidBall;
               initialDirection = %vec;
               initialPosition  = %zombie.getMuzzlePoint(6);
               sourceObject     = %zombie;
               sourceSlot       = 6;
            };
         }
         
      case "AcidStorm":
         %zombie = getWord(%args, 0);
         %target = getWord(%args, 1);
         if(!isobject(%zombie) || %zombie.getState() $= "dead") {
            return;
         }
         if(!isobject(%target) || %target.getState() $= "dead") {
            return;
         }
         
         %zombie.setMoveState(true);
         InsigniaAttack_FUNC("AcidShot_Single", %zombie SPC %target);
         schedule(500, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(1000, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(1500, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(2000, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(2500, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(3000, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(3500, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(4000, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         schedule(4500, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         %zombie.schedule(5000, "setMoveState", false);
      
      case "AcidMachineGun":
         %zombie = getWord(%args, 0);
         %target = getWord(%args, 1);
         if(!isobject(%zombie) || %zombie.getState() $= "dead") {
            return;
         }
         if(!isobject(%target) || %target.getState() $= "dead") {
            return;
         }
         for(%i = 0; %i < 40; %i++) {
            schedule(%i * 100, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         }
      
      case "LapseStrike":
         %zombie = getWord(%args, 0);
         %target = getWord(%args, 1);
         %ct     = getWord(%args, 2);
         if(!isObject(%zombie) || %zombie.getState() $= "dead") {
            return;
         }
         if(!isObject(%target) || %target.getState() $= "dead") {
            return;
         }
         if(%ct > 30) {
            return;
         }
         %ct++;
         schedule(200, 0, InsigniaAttack_FUNC, "LapseStrike", %zombie SPC %target SPC %ct);
         if(%ct < 5) {
            %zombie.setVelocity("0 0 10");
         }
         else if(%ct >= 5 && %ct <= 10) {
            %zombie.startFade(400, 0, true);
            %zombie.startFade(1000, 0, false);
            %zombie.schedule(700, "setPosition", vectorAdd(%target.getPosition(), "20 0 50"));
         }
         else if(%ct == 10 || %ct == 11) {
            schedule(1, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         }
         else if(%ct >= 12 && %ct <= 17) {
            %zombie.startFade(400, 0, true);
            %zombie.startFade(1000, 0, false);
            %zombie.schedule(700, "setPosition", vectorAdd(%target.getPosition(), "-20 0 50"));
         }
         else if(%ct == 18 || %ct == 19 || %ct == 20) {
            schedule(1, 0, InsigniaAttack_FUNC, "AcidShot_Single", %zombie SPC %target);
         }
         else if(%ct > 20 && %ct <= 25) {
            %zombie.setVelocity("0 0 10");
            %zombie.startFade(400, 0, true);
            %zombie.startFade(1000, 0, false);
            %zombie.schedule(700, "setPosition", vectorAdd(%target.getPosition(), "-20 20 50"));
         }
         else if(%ct > 26 && %ct < 30) {
            %zombie.setVelocity("0 0 10");
         }
      
      case "DropSummon":
         %target = getWord(%args, 0);
         %target.setMoveState(true);
         %target.schedule(1500, "SetMoveState", false);
         %target.setPosition(vectorAdd(%zombie.getPosition(), "0 0 20"));
         InsigniaAttack_FUNC("GravShot", %target@" 0 0 -1");
   }
}
//

//ATTACKS

function InsigniaAttack(%zombie) {
   if(!isObject(%zombie) || %zombie.getState() $= "dead") {
      return;
   }
   schedule(27000, 0, "InsigniaAttack", %zombie);
   %attack = getRandom(1, 7);
   switch(%attack) {
      case 1:
         %target = FindValidTarget(%zombie);
         %target = %target.player;
         if(!isObject(%target) || %target.getState() $= "Dead") {
            MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I suppose I can wait...");
            return;
         }
         MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": Lets shorten the distance... "@getTaggedString(%target.client.name)@".");
         %vec = vectorsub(%target.getworldboxcenter(), %zombie.getMuzzlePoint(0));
         %vec = vectoradd(%vec, vectorscale(%target.getvelocity(), vectorlen(%vec)/100));
         InsigniaAttack_FUNC("GravShot", %zombie SPC %vec);
         
      case 2:
         %target = FindValidTarget(%zombie);
         %target = %target.player;
         if(!isObject(%target) || %target.getState() $= "Dead") {
            MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I suppose I can wait...");
            return;
         }
         InsigniaAttack_FUNC("GravShot", %zombie SPC "0 0 200");
         MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": Death from above "@getTaggedString(%target.client.name)@".");
         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(0));
         %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));
         schedule(1500, 0, InsigniaAttack_FUNC, "GravShot", %zombie SPC %vec);
         
      case 3:
         %target = FindValidTarget(%zombie);
         %target = %target.player;
         if(!isObject(%target) || %target.getState() $= "Dead") {
            MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I suppose I can wait...");
            return;
         }
         InsigniaAttack_FUNC("AcidStorm", %zombie SPC %target);
         MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": Acid Storm, just for you... "@getTaggedString(%target.client.name)@".");
         
      case 4:
         %target = FindValidTarget(%zombie);
         %target = %target.player;
         if(!isObject(%target) || %target.getState() $= "Dead") {
            MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I suppose I can wait...");
            return;
         }
         InsigniaAttack_FUNC("LapseStrike", %zombie SPC %target SPC 0);
         MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": Hey, "@getTaggedString(%target.client.name)@". Watch this.");
         
      case 5:
         %target = FindValidTarget(%zombie);
         %target = %target.player;
         if(!isObject(%target) || %target.getState() $= "Dead") {
            MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I suppose I can wait...");
            return;
         }
         InsigniaAttack_FUNC("AcidMachineGun", %zombie SPC %target);
         MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": ENOUGH, "@getTaggedString(%target.client.name)@". DIE.");

      case 6:
         %target = FindValidTarget(%zombie);
         %target = %target.player;
         if(!isObject(%target) || %target.getState() $= "Dead") {
            MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": I suppose I can wait...");
            return;
         }
         MessageAll('msgBossAlertAttack', "\c4"@$TWM2::BossName["Insignia"]@": C'Mere, "@getTaggedString(%target.client.name)@".");
         InsigniaAttack_FUNC("DropSummon", %target);
         
      case 7:
         InsigniaAttack_FUNC("Reinforce", %zombie);
   }
}
