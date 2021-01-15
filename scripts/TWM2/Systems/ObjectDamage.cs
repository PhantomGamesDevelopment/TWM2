//objectDamage.cs
//TWM2 3.9.1
//By: Robert C. Fritzen (Phantom139)

//A collection of TWM2's damage functions, object handler events, and damage over time style functioning

//=========================================================
//CalculateProjectileDamage
// %projectile: The source projectile
// %target: The target object recieving damage
// %amount: The numeric damage amount
// %dType: The damage type flag
// %damLoc: The damage location
// %type: The type of damage (Projectile or Explosion)
// Calculates a damage modifier based on TWM2 settings (Perks, Armors, Equipment) to apply to an object struck by damage
function CalculateProjectileDamage(%projectile, %target, %amount, %dType, %damLoc, %type) {
	//terrain block
	if(%target.getType() & ($TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType)) {
		return;
	}

	%data         = %projectile.getDatablock();
	%sourceObject = %projectile.sourceObject;
	%sourceClient = %sourceObject.client;
	%targetClient = %target.client $= "" ? 0 : %target.client;
	%TDB          = %target.getDatablock();
	if(isObject(%sourceObject)) {
		%SDB       = %sourceObject.getDatablock();
	}
	else {
		%SDB       = "";
	}
	%total        = 1;

	switch$(%type) {
		//Projectiles...
		case "projectile":
			%target.headShot = 0; //Reset first
			if(%sourceClient.ActivePerk["AP Bullets"]) {
				%total *= 1.5;
			}
			if(%targetClient != 0) {
				if(%targetClient.IsActivePerk("Kevlar Armor")) {
					%total *= 0.5;
				}
			}
			if(%target.isZombie) {
				if(Game.CheckModifier("Demonic") == 1) {
					%total = 0.5;
				}
			}
			//------------------------------------------------------
			//source object fixing
			if(strStr(%SDB.getClassName(), "Turret") != -1) {
				if(%SDB.getName() $= "HarbingerGunshipTurret") {
					%projectile.sourceObject = %projectile.sourceObject.mountobj;
					%sourceObject = %projectile.sourceObject;
					%SDB = %sourceObject.getDataBlock();
				}
				else if(%SDB.getName() $= "AC130GunshipTurret") {
					%projectile.sourceObject = %projectile.sourceObject.mountobj;
					%sourceObject = %projectile.sourceObject;
					%SDB = %sourceObject.getDataBlock();
				}
				else if(%SDB.getName() $= "CentaurTurret") {
					%projectile.sourceObject = %projectile.sourceObject.source;
					%sourceObject = %projectile.sourceObject;
					%SDB = %sourceObject.getDataBlock();
				}
			}

			//--------------------------------------------------------
			//Headshot checking
			if(%damLoc $= "head" && %TDB.getClassName() $= "PlayerData") {
				if(%data.HeadMultiplier !$= "") {
					%modifier *= %data.HeadMultiplier;
				}
				if(%data.HeadShotKill && $TWM2::HeadshotKill) {
					%target.headShot = 1;
				}
				if(%sourceClient !$= "") {
					if(%sourceClient.UpgradeOn("HSBullets", %projectile.WeaponImageSource) && $TWM2::HeadshotKill) {
						%target.headShot = 1;
					}
				}
				if(%target.headShot) {
					if(%targetClient != 0 && %targetClient.ActivePerk["Head Guard"]) {
						%target.headShot = 0;
					}
					else {
						if((!%target.isBoss && !%target.noHS) && !(%target.getShieldHealth() > 0)) {
							if(%target.isZombie) {
								if(%TDB $= "FZombieArmor") {
									AwardClient(%sourceClient, "16");
								}
								//
								if(Game.CheckModifier("WheresMyHead") == 1) {
									%target.headShot = 0;
								}
								else {
									%total *= 1000;
								}
							}
							else {
								if(%target.isPilot() || %target.vehicleMounted) {
									%target.headShot = 0;
								}
								else {
									%total *= 1000;
									if(%targetClient != 0) {
										BottomPrint(%targetClient, "You Lost Your Head!!!", 3, 1);
										//Recording...
										if(%sourceClient !$= "") {
											%sourceClient.TWM2Core.PvPHeadshotKills++;
											if(%sourceClient.TWM2Core.PvPHeadshotKills >= 100) {
												CompleteNWChallenge(%sourceClient, "HSHoncho1");
												if(%sourceClient.TWM2Core.PvPHeadshotKills >= 200) {
													CompleteNWChallenge(%sourceClient, "HSHoncho2");
													if(%sourceClient.TWM2Core.PvPHeadshotKills >= 300) {
														CompleteNWChallenge(%sourceClient, "HSHoncho3");
													}
												}
											}									 
										}
									}
								}
							}
						}
					}
				}
			}
			else if(%damLoc $= "legs") {
				if(%data.LegsMultiplier !$= "") {
					%total *= %data.LegsMultiplier;
				}
			}
			
		case "explosion":
			%total = 1;
			if(%dType == $DamageType::RapierShield) {
				if(%target == %sourceObject || %target.isZombie || %target.isBoss) {
					%total = 0;
				}
			}
	}

	//Boss Scaling: TWM2 3.9.2
	// - This system scales fights to make it easier for smaller player counts in harder fights
	if(%target.isBoss) {
		%tName = $TWM2::BossManager.activeBoss;
		if($Boss::DamageScaling[%tName]) {
			%bossScale = $Boss::DamageScaling[%tName];
			
			%playerCountReduction = ClientGroup.getCount() - 1;
			%reduction = $Boss::ScaleReduction[%tName];
			
			%net = %reduction * %playerCountReduction;
			if(%net >= 1) {
				%net = 1;
			}
			
			%scale = %bossScale - (%bossScale * %net);
			%total *= %scale;
		}
	}
	
	%deal = %total * %amount;
	if(%target.isBoss) {
		if(%dType == $DamageType::SuperChaingun) {
			%deal = 0;
		}
		%sourceClient.damageToBoss += %deal;
	}

	return %total;
}

//=========================================================
//postObjectDestroyed
// %source: The source of what killed the object (Another object, projectile, etc)
// %targetObject: What died.
// %dType: The internal damage type flag
// %dLoc: The location of damage
function postObjectDestroyed(%source, %targetObject, %dType, %dLoc) {
	if(%targetObject.getType() & ($TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType)) {
		//Skip terrain and interiors
		return;
	}
	if((%source.isZombie || %source.isBoss) && !%source.isPlayerZombie) {
		//Stop here.
		return;
	}
	if(!isObject(%source) || %source $= "") {
		%SDB = "";
		%sourceObject = 0;
		%sourceClient = 0;
	}
	else {
		%sourceDatablock = %source.getDatablock();
		if(strStr(%sourceDatablock.getName(), "Projectile") != -1) {
			%sourceObject = %source.sourceObject;
		}
		else {
			%sourceObject = %source;
		}
		%sourceClient = %sourceObject.client;
		if(isObject(%sourceObject)) {
			%SDB       = %sourceObject.getDatablock();
		}
		else {
			%SDB       = "";
		}			
	}
	%targetClient = %targetObject.client $= "" ? 0 : %targetObject.client;
	%TDB          = %targetObject.getDatablock();
	//Proceed into object specific tests now...
	//Killed by Vehicle.
	if(strStr(%SDB.getClassName(), "Vehicle") != -1) {
		if((%targetObject.getType() & ($TypeMasks::PlayerObjectType)) && %targetObject.getState() $= "dead") {
			%pl = %sourceObject.getMountNodeObject(0); //the pilot
			%cl = %pl.client;
			if(%cl !$= "") {
				if(%targetObject.client !$= "" && !%targetObject.isZombie && %targetObject.team != %pl.team) {
					%cl.TWM2Core.PvPVehicleKills++;
					if(%cl.TWM2Core.PvPVehicleKills >= 50) {
						CompleteNWChallenge(%cl, "VehMans1");
						if(%cl.TWM2Core.PvPVehicleKills >= 100) {
							CompleteNWChallenge(%cl, "VehMans2");
							if(%cl.TWM2Core.PvPVehicleKills >= 250) {
								CompleteNWChallenge(%cl, "VehMans3");
							}
						}
					}
				}
				UpdateVehicleKillFile(%cl, %SDB.getName());
				//
				if(%TDB $= "DemonMotherZombieArmor" && %SDB $= "CentaurVehicle") {
					%cl.CDLKills++;
					if(%cl.CDLKills >= 5) {
						AwardClient(%cl, "19");
					}
				}
			}
		}
	}
	//Is there a boss going?
	if(!%targetObject.isZombie && !%targetObject.isBossMinion) {
		if($TWM2::BossGoing) {
			//Chalk up the kill count :P
			$TWM2::BossManager.addKill(%targetObject);
		}
	}
	if(%targetObject.isVardisonMinion) {
		$TWM2::VardisonManager.minionCount--;
	}	
	//Game modes
	if($TWM2::PlayingSabo) {
		if(Game.Bomb.Carrier == %targetObject) {
			if(%damageType == $DamageType::FellOff) {
				MessageAll('msgWhoops', "\c5SABOTAGE: Bomb Reset.");
				Game.BombDropped(Game.Bomb, %targetObject);
				Game.bomb.setPosition($SabotageGame::BombLocation[$CurrentMission]);
			}
			else {
				Game.BombDropped(Game.Bomb, %targetObject);
			}
		}
	}
	//Gore Mod On?
	if($TWM2::UseGoreMod) {
		CreateBlood(%targetObject);
	}
	//Rog Rapier Shield
	if(%damageType == $DamageType::RapierShield) {
		if(%sourceObject.client !$= "") {
			UpdateWeaponKillFile(%sourceObject.client, "rapierShieldImage");
		}
	}	
	//Zombie Checks
	if(%targetObject.isZombie) {
		//Horde 3 Checks
		if($TWM::PlayingHorde == 1) {
			if($HordeGame::Zombiecount > 0) { 
				$HordeGame::Zombiecount--;
				messageAll('MsgSPCurrentObjective1' ,"", "Wave "@$HordeGame::CurrentWave@" | Zombies Left: "@$HordeGame::Zombiecount@"");
			}
			if($HordeGame::Zombiecount <= 0) {
				HordeNextWave($HordeGame::Game, $HordeGame::NextWave);
			}
		}
		//Helljump Checks
		if($TWM::PlayingHelljump == 1) {
			if($HellJump::Zombiecount > 0) { 
				$HellJump::Zombiecount--;
				messageAll('MsgSPCurrentObjective1' ,"", "[W"@$HellJump::CurrentWave@"|G"@$HellJump::CurrentGroup@"|S"@$HellJump::CurrentStrike@"] | Zombies Left: "@$HellJump::Zombiecount@"");
			}
			if($HellJump::Zombiecount <= 0) {
				$HellJump::Game.GoNextStrike();
			}
		}
		//TWM Infection / PvPz Checks
		if(%targetObject.isPlayerZombie) {
			%sourceClient.TWM2Core.PvPZombieKills++;
			if(%sourceClient.TWM2Core.PvPZombieKills >= 100) {
				CompleteNWChallenge(%sourceClient, "Defectionator1");
				if(%sourceClient.TWM2Core.PvPZombieKills >= 250) {
					CompleteNWChallenge(%sourceClient, "Defectionator2");
					if(%sourceClient.TWM2Core.PvPZombieKills >= 500) {
						CompleteNWChallenge(%sourceClient, "Defectionator3");
					}
				}
			}
		}		 
		//Global Methods
		Game.ZkillUpdateScore(%sourceClient, %sourceObject, %targetObject);
		%sourceObject.zombiekillsinarow++;
		DoZKillstreakChecks(%sourceClient);
	}
	//PvP Checks
	else {
		if(%targetObject.team != %sourceClient.team && !%targetObject.isBoss) {
			if(isObject(%sourceClient) && %sourceClient.IsActivePerk("Double Down")) {
				GainExperience(%sourceClient, $TWM2::KillXPGain * 2, "[D-D]Enemy Killed ");
			}
			else {
				GainExperience(%sourceClient, $TWM2::KillXPGain, "Enemy Killed ");
			}
			//Zombie Kills Player
			if(!%targetObject.isZombie && %sourceObject.isZombie) {
				%sourceClient.TWM2Core.PvPHumanKills++;
				if(%sourceClient.TWM2Core.PvPHumanKills >= 50) {
					CompleteNWChallenge(%sourceClient, "Infectionator1");
					if(%sourceClient.TWM2Core.PvPHumanKills >= 100) {
						CompleteNWChallenge(%sourceClient, "Infectionator2");
						if(%sourceClient.TWM2Core.PvPHumanKills >= 250) {
							CompleteNWChallenge(%sourceClient, "Infectionator3");
						}
					}
				}			   
			}
			else {
				//Player Kills Player
				%sourceClient.TWM2Core.PvPKills++;
				if(%sourceClient.TWM2Core.PvPKills >= 100) {
					CompleteNWChallenge(%sourceClient, "Slayer1");
					if(%sourceClient.TWM2Core.PvPKills >= 250) {
						CompleteNWChallenge(%sourceClient, "Slayer2");
						if(%sourceClient.TWM2Core.PvPKills >= 500) {
							CompleteNWChallenge(%sourceClient, "Slayer3");
							if(%sourceClient.TWM2Core.PvPKills >= 750) {
								CompleteNWChallenge(%sourceClient, "Slayer4");
								if(%sourceClient.TWM2Core.PvPKills >= 1000) {
									CompleteNWChallenge(%sourceClient, "Slayer5");
								}
							}
						}
					}
				}				
			}
			//Team Gain Perk
			if(isObject(%sourceClient) && %sourceClient.IsActivePerk("Team Gain")) {
				%TargetSearchMask = $TypeMasks::PlayerObjectType;
				InitContainerRadiusSearch(%sourceObject.getPosition(), 20, %TargetSearchMask); //small distance
				while ((%potentialTarget = ContainerSearchNext()) != 0){
					if (%potentialTarget.getPosition() != %pos) {
						if(%potentialTarget.client.team == %sourceClient.team && %potentialTarget != %sourceObject) {
							GainExperience(%potentialTarget.client, $TWM2::KillXPGain, "Team Gain From "@%sourceClient.namebase@" ");
						}
					}
				}
			}
			//Challenges, Successive Kills, Killstreaks
			//doChallengeCheck(%sourceClient, %targetClient);
			%sourceObject.killsinarow++;
			%sourceObject.killsinarow2++;
			//TWM2 3.2 -> Successive Kills
			%sourceObject.kills[%damageType]++;
			PerformSuccessiveKills(%sourceObject, %damageType);
			//
			if(%sourceObject.killsinarow2 == 10 && !(%sourceObject.isBoss || %sourceObject.isZombie)) {
				MessageAll('MsgWOW', "\c2TWM2: "@%sourceClient.namebase@" is on a killsteak of 10");
				awardClient(%sourceClient, "14");
			}
			if(%sourceObject.killsinarow2 == 20 && !(%sourceObject.isBoss || %sourceObject.isZombie)) {
				MessageAll('MsgWOW', "\c2TWM2: "@%sourceClient.namebase@" is on a killsteak of 20");
			}
			if(%sourceObject.killsinarow2 == 25 && !(%sourceObject.isBoss || %sourceObject.isZombie)) {
				MessageAll('MsgWOW', "\c2TWM2: "@%sourceClient.namebase@" is on a killsteak of 25");
			}
			DoKillstreakChecks(%sourceClient);
		}
	}
	//Record Challenge Kill
	//doChallengeKillRecording(%sourceObject, %targetObject);
	//martydom
	if(%targetClient !$= "" && %targetClient != 0 && %targetClient.IsActivePerk("Martydom")) {
		serverPlay3d(SatchelChargeActivateSound, %targetObject.getPosition());
		schedule(2200, 0, "MartydomExplode", %targetObject.getPosition(), %targetClient);
	}		
}


datablock ParticleData(burnParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "special/cloudflash";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "1 0.18 0.03 0.4";
   colors[1]     = "1 0.18 0.03 0.3";
   colors[2]     = "1 0.18 0.03 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(burnEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "burnParticle";
};

function burnloop(%obj){
   if(!isobject(%obj)) {
	  return;
   }
   if(%obj.onfire == 0) {
     return;
   }
   if(%obj.firecount >= %obj.maxfirecount){
	  %obj.firecount = "";
	  %obj.maxfirecount = 0;
	  %obj.onfire = 0;
      return;
   }
   else {
	  %obj.damage(0, %obj.getposition(), 0.01, $DamageType::Burn);
      %obj.lastDamagedImage = "flamerImage";
   	  %fire = new ParticleEmissionDummy(){
   	     position = vectoradd(%obj.getPosition(),"0 0 0.5");
   	     dataBlock = "defaultEmissionDummy";
   	     emitter = "BurnEmitter";
      };
	  MissionCleanup.add(%fire);
	  %fire.schedule(100, "delete");
	  %obj.firecount++;
	  schedule(100, %obj, "burnloop", %obj);
   }
}

function ApplyEMP(%client) {
   if(%client.isEMPd) {
      //applying second emp = laggy + bad
      return;
   }
   %client.isEMPd = 1;
   EMPEKill(%client.player);
   //echo("EMP: applied EMP to "@%client@" - "@%client.player@"");
}

function KillEMP(%client) {
   %client.isEMPd = 0;
   %client.player.stopZap();
   messageClient(%client, 'msgDieEMP', "\c5Armor: Electronic Stability has returned.");
   //echo("EMP: kill EMP: "@%client@"");
}

function EMPEKill(%obj) {
   if(%obj.client.isEMPd) {
      if(!isObject(%obj) || %obj.getState() $= "Dead") {
         //echo("EMP: "@%obj@" dead, sending Re-EMP to "@%obj.client@"");
         ReEMPLoop(%obj.client);
         return;
      }
      %obj.setEnergyLevel(0.0);
      %obj.zapObject();
      schedule(100, 0, "EMPEKill", %obj);
   }
   else {
      %obj.stopZap();
      return;
   }
}

function ReEMPLoop(%client) {
   if(!%client.isEMPd) {
      //echo("EMP: RE-EMP: "@%client@" no longer EMP");
      return;
   }
   if(!isObject(%client.player) || %client.player.getState() $= "Dead") {
      //echo("EMP: RE: dead, trying to re-loop on "@%client@"");
      schedule(500, 0, "ReEMPLoop", %client);
      return;
   }
   EMPLoop(%client.player);
   EMPEKill(%client.player);
   //echo("EMP: Re-EMP: "@%client@" - "@%client.player@"");
}
