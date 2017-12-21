datablock SeekerProjectileData(VegenorFireMissile) : YvexZombieMakerMissile {
   indirectDamage      = 0.5;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Fire;
};

datablock GrenadeProjectileData(VegenorFireMeteor) : JTLMeteorStormFireball {
	projectileShapeName  = "plasmabolt.dts";
	scale                = "40.0 40.0 40.0";
	emitterDelay         = -1;
	directDamage         = 0;
	directDamageType     = $DamageType::Fire;
	hasDamageRadius      = true; // true;
	indirectDamage       = 0.5; // 0.5;
	damageRadius         = 150.0;
	radiusDamageType     = $DamageType::Fire;
};

datablock PlayerData(VegenorZombieArmor) : HeavyMaleBiodermArmor {
   maxDamage = 600.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;

   damageScale[$DamageType::M1700] = 2.0;
   damageScale[$DamageType::Fire] = 0.1;
   damageScale[$DamageType::Burn] = 0.1;
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

function SpawnVegenor(%position) {
   %Zombie = new player(){
      Datablock = "VegenorZombieArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Time to engage the enemy soldiers!!!");

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
      %zombie.setPosition(vectorAdd(vectoradd(%closestclient.player.getPosition(), "0 0 20"), TWM2Lib_MainControl("getRandomPosition", 25 TAB 1)));
      %zombie.setVelocity("0 0 0");
      MessageAll('msgAntiFall', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Falling, How about no...");
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
         %type = TWM2Lib_Zombie_Core("getRandomZombieType", "1 2 3 4 5 9 12 13 14");
         %msg = getrandom(1,2);
         switch(%msg) {
            case 1:
               messageall('MsgSummon',"\c4"@$TWM2::BossNameInternal["Vegenor"]@": Attack the enemy");
            case 2:
               messageall('MsgSummon',"\c4"@$TWM2::BossNameInternal["Vegenor"]@": Hunt them all down");
         }
         for(%i = 0; %i < 6; %i++) {
            %pos = vectoradd(%z.getPosition(), TWM2Lib_MainControl("getRandomPosition", 10 TAB 1));
            %fpos = vectoradd("0 0 5",%pos);
            TWM2Lib_Zombie_Core("SpawnZombie", "zSpawnCommand", %type, %fpos);
         }
         %z.setMoveState(true);
         %z.setActionThread($Zombie::RogThread, true);
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
         createMissileSeekingProjectile("VegenorFireMissile", %Tobj, %z, %z.getMuzzlePoint(4), %vec, 4, 100);
      case "MeteorDrop":
         %t = getWord(%args, 0);
         %fpos = vectoradd(%t.getposition(), TWM2Lib_MainControl("getRandomPosition", 50 TAB 0));
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
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Flame on "@getTaggedString(%target.name)@"!");
            VegenorAttack_FUNC("SetFire", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": heh, "@getTaggedString(%target.name)@" is already dead!");
         }
      case 2:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Lets insert some fire into your life, "@getTaggedString(%target.name)@"!");
            VegenorAttack_FUNC("FlameMissileSingle", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Hiding from me "@getTaggedString(%target.name)@"?");
         }
      case 3:
         MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Fire Missiles For ALL!!");
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %cl = ClientGroup.getObject(%i);
            if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
               VegenorAttack_FUNC("FlameMissileSingle", %cl.player);
            }
         }
      case 4:
         %target = FindValidTarget(%z);
         if(isObject(%target.player) && !%target.ignoredbyZombs) {
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Hey "@getTaggedString(%target.name)@", LOOK UP!!!");
            VegenorAttack_FUNC("MeteorDrop", %target.player);
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": Hiding does not beat me "@getTaggedString(%target.name)@"!!!");
         }
      default:
         MessageAll('MessageAll', "\c4"@$TWM2::BossNameInternal["Vegenor"]@": I shall wait!!!");
   }
}
