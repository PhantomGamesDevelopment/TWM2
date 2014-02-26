//CUSTOM ARMOR FUNCTIONS
function MicroburstMaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 4) {
      // Throw grenade
      if (%val == 1) {
         %player.grenTimer = 1;
      }
      else {
         if (%player.grenTimer == 0) {
            // Bad throw for some reason
         }
         else {
            %player.use(Grenade);
            %player.grenTimer = 0;
         }
      }
   }
   else if (%triggerNum == 5) {
      // Throw mine
      if (%val == 1) {
         %player.mineTimer = 1;
      }
      else {
         if (%player.mineTimer == 0) {
            // Bad throw for some reason
         }
         else {
            if(%player.inv[C4] > 0) {
               %player.use(C4);
               %player.mineTimer = 0;
            }
            else {
               %player.use(Mine);
               %player.mineTimer = 0;
            }
         }
      }
   }
   else if (%triggerNum == 3) {
      // val = 1 when jet key (LMB) first pressed down
      // val = 0 when jet key released
      // MES - do we need this at all any more?
      if(!%player.CantFireS3Shock) {
         %image = %player.getMountedImage($WeaponSlot).getName();

         if(%image $= "S3RifleImage") {
            %obj = %player;
            %slot = 0;
            //Shocklance Stuff
            %muzzlePos = %obj.getMuzzlePoint(%slot);
            %muzzleVec = %obj.getMuzzleVector(%slot);

            %endPos    = VectorAdd(%muzzlePos, VectorScale(%muzzleVec, S3Shocker.extension));

            %damageMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
               $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
               $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType;

            %everythingElseMask = $TypeMasks::TerrainObjectType |
                         $TypeMasks::InteriorObjectType |
                         $TypeMasks::ForceFieldObjectType |
                         $TypeMasks::StaticObjectType |
                         $TypeMasks::MoveableObjectType |
                         $TypeMasks::DamagableItemObjectType;

            // did I miss anything? players, vehicles, stations, gens, sensors, turrets
            %hit = ContainerRayCast(%muzzlePos, %endPos, %damageMasks | %everythingElseMask, %obj);

            %noDisplay = true;
            if (%hit !$= "0") {
               %obj.setEnergyLevel(%obj.getEnergyLevel() - 15);

               %hitobj = getWord(%hit, 0);
               %hitpos = getWord(%hit, 1) @ " " @ getWord(%hit, 2) @ " " @ getWord(%hit, 3);

               if ( %hitObj.getType() & %damageMasks ) {
                  %hitobj.applyImpulse(%hitpos, VectorScale(%muzzleVec, S3Shocker.impulse));
                  %obj.playAudio(0, ShockLanceHitSound);

                  // This is truly lame, but we need the sourceobject property present...
                  %p = new ShockLanceProjectile() {
                      dataBlock        = S3Shocker;
                      initialDirection = %obj.getMuzzleVector(%slot);
                      initialPosition  = %obj.getMuzzlePoint(%slot);
                      sourceObject     = %obj;
                      sourceSlot       = %slot;
                      targetId         = %hit;
                  };
                  MissionCleanup.add(%p);
                  %p.WeaponImageSource = "S3RifleImage";

                  %damageMultiplier = 1.0;

                  if(%hitObj.getDataBlock().getClassName() $= "PlayerData") {
                     // Now we see if we hit from behind...
                     %forwardVec = %hitobj.getForwardVector();
                     %objDir2D   = getWord(%forwardVec, 0) @ " " @ getWord(%forwardVec,1) @ " " @ "0.0";
                     %objPos     = %hitObj.getPosition();
                     %dif        = VectorSub(%objPos, %muzzlePos);
                     %dif        = getWord(%dif, 0) @ " " @ getWord(%dif, 1) @ " 0";
                     %dif        = VectorNormalize(%dif);
                     %dot        = VectorDot(%dif, %objDir2D);

                     // 120 Deg angle test...
                     // 1.05 == 60 degrees in radians
                     if (%dot >= mCos(1.05)) {
                        // Rear hit
                        %damageMultiplier = 3.0;
                     }
                  }

                  %totalDamage = S3Shocker.DirectDamage * %damageMultiplier;
                  %hitObj.getDataBlock().damageObject(%hitobj, %p.sourceObject, %hitpos, %totalDamage, $DamageType::ShockLance);

                  %noDisplay = false;
               }
            }
            if( %noDisplay ) {
               // Miss
               %obj.setEnergyLevel(%obj.getEnergyLevel() - 0);
               %obj.playAudio(0, ShockLanceMissSound);

               %p = new ShockLanceProjectile() {
                  dataBlock        = S3Shocker;
                  initialDirection = %obj.getMuzzleVector(%slot);
                  initialPosition  = %obj.getMuzzlePoint(%slot);
                  sourceObject     = %obj;
                  sourceSlot       = %slot;
               };
               MissionCleanup.add(%p);
               %p.WeaponImageSource = "S3RifleImage";
            }
            //END STUFF
            %player.CantFireS3Shock = 1;
            schedule(6000, 0, "ResetS3Shock", %player);
         }
      }
   }
}

function MicroburstFemaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val) {
   MicroburstMaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val);
}

function MicroburstMaleBiodermArmor::onTrigger(%data, %player, %triggerNum, %val) {
   MicroburstMaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val);
}

function ResetS3Shock(%player) {
   %player.CantFireS3Shock = 0;
}























function ShadowCommandoMaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 4) {
      // Throw grenade
      if (%val == 1) {
         %player.grenTimer = 1;
      }
      else {
         if (%player.grenTimer == 0) {
            // Bad throw for some reason
         }
         else {
            %player.use(Grenade);
            %player.grenTimer = 0;
         }
      }
   }
   else if (%triggerNum == 5) {
      // Throw mine
      if (%val == 1) {
         %player.mineTimer = 1;
      }
      else {
         if (%player.mineTimer == 0) {
            // Bad throw for some reason
         }
         else {
            if(%player.inv[C4] > 0) {
               %player.use(C4);
               %player.mineTimer = 0;
            }
            else {
               %player.use(Mine);
               %player.mineTimer = 0;
            }
         }
      }
   }
   else if (%triggerNum == 3) {
      // val = 1 when jet key (LMB) first pressed down
      // val = 0 when jet key released
      // MES - do we need this at all any more?
      if(%player.RecentAttack["Shade"] == 1) {
         return;
      }
      else {
         messageClient(%player.client, 'MsgShd', "\c5TWM2: Activating Shade");
         %player.RecentAttack["Shade"] = 1;
         schedule(90000, 0, "ResetPlayerDLAttack", %player, "Shade");
         %player.zapObject();
         %player.setCloaked(true);
         %player.setCloakStatus(true);
         %player.startFade(2000, 0, true);
         %player.schedule(15000, "zapObject");
         %player.schedule(15000, "setCloaked", false);
         %player.schedule(15000, "startFade", 5000, 0, false);
         %player.schedule(15000, "setCloakStatus", false);
      }
   }
}

function Player::setCloakStatus(%player, %status) {
   %player.stealthed = %status;
}

function ShadowCommandoFemaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val) {
   ShadowCommandoMaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val);
}

function ShadowCommandoMaleBiodermArmor::onTrigger(%data, %player, %triggerNum, %val) {
   ShadowCommandoMaleHumanArmor::onTrigger(%data, %player, %triggerNum, %val);
}


















//ZOMBAHS
function ZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) {
      if(vectorLen(%player.getVelocity()) < 30) { //we dont want hyper speed zombies :)
	     %player.applyImpulse(%player.getPosition(),vectorScale(%player.getMuzzleVector(0),$Zombie::forwardspeed));
      }
   }
}

function LordZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 0) { //shooting
      PlayerLordFire1(%player);
   }
   else if (%triggerNum == 5) { //lifting
      playerLiftTarget(%player,0);
   }
}

function DemonZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if(%player.isSniperZombie) {
      if (%triggerNum == 0) { //shooting
         if(%player.RecentAttack["Shot"] == 1) {
            return;
         }
         else {
            %player.RecentAttack["Shot"] = 1;
            schedule(3000, 0, "ResetPlayerDLAttack", %player, "Shot");
            %p = new TracerProjectile() { //TWM2 Sniper zombies use M1 Snipers :P
               dataBlock        = M1Bullet;
               initialDirection = %player.getMuzzleVector(4);
               initialPosition  = %player.getMuzzlePoint(4);
               sourceObject     = %player;
               sourceSlot       = 4;
            };
            ServerPlay3D(M1FireSound, %player.getPosition());
         }
      }
      if(%triggerNum == 3) {
         %pos = %player.getPosition();
         %vector = %player.getMuzzleVector(4);
         %x = getWord(%vector, 0);
         %y = getWord(%vector, 1);
         %finX = %x * -1;
         %finY = %y * -1;
         %finalVec = %finX SPC %finY SPC 0;
         %finalVec = VectorScale(%finalVec, $Zombie::DForwardSpeed * 3);
         //Z is unimportant
         %player.applyImpulse(%pos, %finalVec);
      }
   }
   else {
      if (%triggerNum == 0) { //shooting
         if(%player.recharging) {
            return;
         }
   	     %p = new GrenadeProjectile() {
   	      	 dataBlock        = DemonFireball;
             initialDirection = %player.getMuzzleVector(0);
             initialPosition  = vectoradd(%player.getMuzzlePoint(0), "0 0 1.5");
   	   	     sourceObject     = %player;
   	   	     sourceSlot       = 6;
   	     };
         MissionCleanup.add(%p);
         %player.recharging = 1;
         schedule(4000,0,"ResetZombieCharge",%player);
      }
   }
}

function RapierZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) { //flying
      if(%val == 1)
         %player.isJetting = true;
      else {
         %player.isJetting = false;
      }
   }
   if (%triggerNum == 0) { //speeding
      if(vectorLen(%player.getVelocity()) < 30) { //we dont want hyper speed zombies :)
	     %player.applyImpulse(%player.getPosition(),vectorScale(%player.getMuzzleVector(0),$Zombie::forwardspeed));
      }
   }
}

function ResetPlayerDLAttack(%player, %attack) {
   %player.RecentAttack[""@%attack@""] = 0;
}

function DemonMotherZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 0) { //shooting
      PlayerLordFire1(%player);
   }
   else if (%triggerNum == 3) { //flying
      if(%player.RecentAttack["Fly"] == 1) {
         return;
      }
      else {
         %attack = getRandom(1, 2);
         %targetInfo = ZombieLookforTarget(%player);
         %target = getWord(%targetInfo, 0);
         if(%target.player.IsAlive()) {
            %player.RecentAttack["Fly"] = 1;
            schedule(17000, 0, "ResetPlayerDLAttack", %player, "Fly");
            if(%attack == 1) {
               PlayerDemonLordFlyAttack(%player, %target.player);
            }
            else {
               PlayerDemonLordPlasmaAttack(%player, %target.player);
            }
         }
      }
   }
   else if (%triggerNum == 4) {
      if(%player.RecentAttack["Missile"] == 1) {
         return;
      }
      else {
         %targetInfo = ZombieLookforTarget(%player);
         %target = getWord(%targetInfo, 0);
         if(%target.player.IsAlive()) {
            %player.RecentAttack["Missile"] = 1;
            schedule(17000, 0, "ResetPlayerDLAttack", %player, "Missile");
            //t3h missile
            %target = %target.player;
            %vec = vectorNormalize(vectorSub(%target.getPosition(),%player.getPosition()));
   	        %p = new SeekerProjectile() {
	           dataBlock        = DMMissile;
	           initialDirection = %vec;
	           initialPosition  = %player.getMuzzlePoint(4);
	           sourceObject     = %player;
	           sourceSlot       = 4;
   	        };
   	        %beacon = new BeaconObject() {
               dataBlock = "SubBeacon";
               beaconType = "vehicle";
               position = %target.getWorldBoxCenter();
            };
   	        %beacon.team = 0;
   	        %beacon.setTarget(0);
   	        MissionCleanup.add(%beacon);
            %p.setObjectTarget(%beacon);
            DemonMotherMissileFollow(%target,%beacon,%p);
            //
         }
      }
   }
   else if (%triggerNum == 5) {
      if(%player.RecentAttack["Firestorm"] == 1) {
         return;
      }
      else {
         %targetInfo = ZombieLookforTarget(%player);
         %target = getWord(%targetInfo, 0);
         if(%target.player.IsAlive()) {
            %player.RecentAttack["Firestorm"] = 1;
            schedule(17000, 0, "ResetPlayerDLAttack", %player, "Firestorm");
            PlayerDemonLordFireRainAttack(%player, %target.player);
         }
      }
   }
}

function YvexZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 0) { //shooting
      if(%player.RecentAttack["Pulse"] == 1) {
         return;
      }
      else {
         %attack = getRandom(1, 2);
         %player.RecentAttack["Pulse"] = 1;
         schedule(14000, 0, "ResetPlayerDLAttack", %player, "Pulse");
         if(%attack == 1) {
            %p = new LinearFlareProjectile() {
               dataBlock        = KillerPulse;
               initialDirection = %player.getMuzzleVector(0);
               initialPosition  = %player.getMuzzlePoint(0);
               sourceObject     = %player;
               sourceSlot       = 0;
            };
         }
         else {
            %p = new LinearFlareProjectile() {
               dataBlock        = YvexSniperShot;
               initialDirection = %player.getMuzzleVector(0);
               initialPosition  = %player.getMuzzlePoint(0);
               sourceObject     = %player;
               sourceSlot       = 0;
            };
         }
      }
   }
   else if (%triggerNum == 3) { //flying
      if(%player.RecentAttack["RandNight"] == 1) {
         return;
      }
      else {
         %player.RecentAttack["RandNight"] = 1;
         schedule(20000, 0, "ResetPlayerDLAttack", %player, "RandNight");
         %client = FindValidTarget(%player);
         %counts = 0;
         messageClient(%player.client,'msgZombie',"\c3Attempting Random Nightmare.");
         if(isObject(%client.player) && %client.player.getState !$= "dead" && !%client.ignoredbyZombs) {
            %c = createEmitter(%client.player.position,InfNightmareGlobeEmitter,"1 0 0");      //Rotate it
            MissionCleanup.add(%c); // I think This should be used
            schedule(3000,0,"killit",%c);
            %luck = getrandom(1,5);
            switch(%luck) {
               case 1:
                  messageClient(%player.client,'msgZombie',"\c3Success!!!");
                  Yvexnightmareloop(%player,%client);
                  messageClient(%client,'msgClient',"~wfx/misc/diagnostic_on.wav");
               case 2:
                  messageClient(%player.client,'msgZombie',"\c3Failed.");
               case 3:
                  messageClient(%player.client,'msgZombie',"\c3Success!!!");
                  Yvexnightmareloop(%player,%client);
                  messageClient(%client,'msgClient',"~wfx/misc/diagnostic_on.wav");
               case 4:
                  messageClient(%player.client,'msgZombie',"\c3Failed.");
               case 5:
                  messageClient(%player.client,'msgZombie',"\c3Success!!!");
                  Yvexnightmareloop(%player,%client);
                  messageClient(%client,'msgClient',"~wfx/misc/diagnostic_on.wav");
            }
         }
         else {
            messageClient(%player.client,'msgZombie',"\c3Failed.");
         }
      }
   }
   else if (%triggerNum == 4) {
      if(%player.RecentAttack["Missile"] == 1) {
         return;
      }
      else {
         %targetInfo = ZombieLookforTarget(%player);
         %target = getWord(%targetInfo, 0);
         if(%target.player.IsAlive()) {
            %player.RecentAttack["Missile"] = 1;
            schedule(27500, 0, "ResetPlayerDLAttack", %player, "Missile");
            //t3h missile
            %target = %target.player;
            %vec = vectorNormalize(vectorSub(%target.getPosition(),%player.getPosition()));
   	        %p = new SeekerProjectile() {
	           dataBlock        = YvexZombieMakerMissile;
	           initialDirection = %vec;
	           initialPosition  = %player.getMuzzlePoint(4);
	           sourceObject     = %player;
	           sourceSlot       = 4;
   	        };
   	        %beacon = new BeaconObject() {
               dataBlock = "SubBeacon";
               beaconType = "vehicle";
               position = %target.getWorldBoxCenter();
            };
   	        %beacon.team = 0;
   	        %beacon.setTarget(0);
   	        MissionCleanup.add(%beacon);
            %p.setObjectTarget(%beacon);
            DemonMotherMissileFollow(%target,%beacon,%p);
            //
         }
      }
   }
   else if (%triggerNum == 5) {
      if(%player.RecentAttack["Summon"] == 1) {
         return;
      }
      else {
         %player.RecentAttack["Summon"] = 1;
         schedule(40000, 0, "ResetPlayerDLAttack", %player, "Summon");
         PlayerSummon(%player, 5);
      }
   }
}

function LordRogZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 0) { //shooting
      if(%player.RecentAttack["BOV"] == 1) {
         return;
      }
      else {
         %player.RecentAttack["BOV"] = 1;
         schedule(1000, 0, "ResetPlayerDLAttack", %player, "BOV");
         %p = new (LinearProjectile)() {
	        dataBlock = BOVhit;
	        initialDirection = %player.getMuzzleVector(0);
	        initialPosition = %player.getMuzzlePoint(0);
	        sourceObject = %player;
	        sourceSlot = 0;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%triggerNum == 3) { //flying
      if(%player.RecentAttack["Summon"] == 1) {
         return;
      }
      else {
         %player.RecentAttack["Summon"] = 1;
         schedule(35000, 0, "ResetPlayerDLAttack", %player, "Summon");
         PlayerSummon(%player, 6);
      }
   }
   else if (%triggerNum == 5) {
      if(%player.RecentAttack["RandAttack"] == 1) {
         return;
      }
      else {
         %player.RecentAttack["RandAttack"] = 1;
         schedule(27500, 0, "ResetPlayerDLAttack", %player, "RandAttack");
         PlayerLRAbilities(%player);
      }
   }
}

function ShifterZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) { //flying
      if(%player.RecentAttack["Teleport"] == 1) {
         return;
      }
      else {
         %targetInfo = ZombieLookforTarget(%player);
         %target = getWord(%targetInfo, 0);
         if(%target.player.IsAlive()) {
            %player.RecentAttack["Teleport"] = 1;
            schedule(3500, 0, "ResetPlayerDLAttack", %player, "Teleport");
            %player.setVelocity("0 0 10");
       	    %player.startFade(500, 0, true);
            %player.schedule(600, "SetPosition", VectorAdd(%target.player.getPosition(), "0 0 3"));
            %player.startFade(750, 0, false);
         }
      }
   }
}

function SummonerZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) { //flying
      if(%player.RecentAttack["Summon"] == 1) {
         return;
      }
      else {
         %player.RecentAttack["Summon"] = 1;
         schedule(25000, 0, "ResetPlayerDLAttack", %player, "Summon");
         %Ct = GetRandom(1,3);
         %type = GetRandom(1, 8);
         if(%type > 5) {
            %type += 3;
            if(%type == 10) { //summoners don;t summon more summoners
               %type = 12;
            }
         }
         %SumPos = vectorAdd(VectorAdd(GetRandomPosition(20, 1), "0 0 7"), %player.getPosition());
         %c = CreateEmitter(%SumPos, NightmareGlobeEmitter, "0 0 1");
         %c.schedule(((%Ct * 1000) + 500), "delete");
         for(%i = 1; %i <= %ct; %i++) {
            %time = %i * 1000;
            schedule(%time, 0, "StartAZombie", %SumPos, %type);
         }
      }
   }
}

function DemonUltraZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 0) { //shooting
      if(%player.recharging) {
         return;
      }
      %p = new GrenadeProjectile() {
   	      dataBlock        = DemonFireball;
          initialDirection = %player.getMuzzleVector(0);
          initialPosition  = vectoradd(%player.getMuzzlePoint(0), "0 0 1.5");
	      sourceObject     = %player;
   	      sourceSlot       = 6;
   	  };
      MissionCleanup.add(%p);
      %player.recharging = 1;
      schedule(1000,0,"ResetZombieCharge",%player);
   }
   else if (%triggerNum == 3) {
      if(vectorLen(%player.getVelocity()) < 30) { //we dont want hyper speed zombies :)
	     %player.applyImpulse(%player.getPosition(),vectorScale(%player.getMuzzleVector(0),$Zombie::forwardspeed));
         createBiodermProjection(%player);
      }
   }
}

function FZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) {
      if(%player.zombieType == 13) {
         ServerPlay3D("SatchelChargeExplosionSound", %player.getPosition());
         %c4 = new Item() {
            datablock = C4Deployed;
            position = %player.getPosition();
            scale = ".1 .1 .1";
         };
         MissionCleanup.add(%c4);
         schedule(770, 0, "C4GoBoom", %c4);
         
         %c4.theClient = %player.client;
         return;
      }
   }
}

function WraithZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) {
      if(vectorLen(%player.getVelocity()) < 30) { //we dont want hyper speed zombies :)
	     %player.applyImpulse(%player.getPosition(),vectorScale(%player.getMuzzleVector(0),$Zombie::forwardspeed));
      }
   }
}

function ROGZombieArmor::onTrigger(%data, %player, %triggerNum, %val) {
   if (%triggerNum == 3) {
      if(%player.state["canUseMG"]) {
         playerRogFire(%player);
      }
   }
   
   if(%triggerNum == 0) {
      %player.SALoop = RapierShieldAttack(%player, %val, 0);
   }
}
