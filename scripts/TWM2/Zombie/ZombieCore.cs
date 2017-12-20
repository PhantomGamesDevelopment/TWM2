//ZombieCore.cs
//TWM2 
// Dondelium_X, Phantom139

// Definitions that are common across all zombie types.
//  For type specific functioning, please access the individual files in the ZombieTypes/ directory.

// NOTE: Massive cleaning done as of TWM2 3.9.2
//  * GOALS:
//   - Reduce Function Count
//   - Smooth Zombie Movement
//   - Simplify Zombie Logic
//   - Add more customizability to zombie movement, abilities, etc via globals

//$Zombie::detectDist: The maximum distance in which a zombie may target a player
$Zombie::detectDist = 9999;
//$Zombie::FallDieHeight: The minimum altitude a zombie may be before being script killed
$Zombie::FallDieHeight = -500;

//$Zombie::BaseDamage: The default damage taken when touching a zombie.
$Zombie::BaseDamage = 0.2;
//$Zombie::TypeDamage[#]: Overrides the BaseDamage with this value
$Zombie::TypeDamage[4] = 0.4;
$Zombie::TypeDamage[5] = 0.4;
$Zombie::TypeDamage[6] = 0.5;
$Zombie::TypeDamage[9] = 0.3;
$Zombie::TypeDamage[10] = 0.3;
$Zombie::TypeDamage[12] = 0.5;
$Zombie::TypeDamage[15] = 0.6;
 
//$Zombie::InfectOnCollide[#]: Special flag to determine if infection is applied upon contact, by default, this is true

//$Zombie::TypeInfectedMultiplier[#]: A damage multiplier to apply to infected targets, ie: Total Damage = $Zombie::TypeDamage[#] * $Zombie::TypeInfectedMultiplier[#] 
// when the player taking damage is already infected.
$Zombie::TypeInfectedMultiplier[1] = 1.5;

//$Zombie::BaseSpeed: The default speed setting on zombies, any zombie that does not have a TypeSpeed var. set will default to the BaseSpeed
// Be mindful of the $Zombie::SpeedUpdateTime[#] value when tuning, these two numbers augment closely so you need to be careful on tweaking
$Zombie::BaseSpeed = 150;
//$Zombie::TypeSpeed[#]: The speed of a specific zombie type instance, overrides BaseSpeed for that specific type
$Zombie::TypeSpeed[2] = 300;
$Zombie::TypeSpeed[3] = 4000;
$Zombie::TypeSpeed[4] = 240;
$Zombie::TypeSpeed[5] = 1500;
$Zombie::TypeSpeed[6] = 1200;

//$Zombie::BaseJumpCooldown: The time zombies must elapse before jumping / lunging
$Zombie::BaseJumpCooldown = 1500;

//$Zombie::SpeedMultiplier[#]: An additional multiplier to be applied to the zombie's base speed when calculating the impulse, used to fine tune speed values
$Zombie::SpeedMultiplier[2] = 0.6;
$Zombie::SpeedMultiplier[3] = 0.8;
$Zombie::SpeedMultiplier[4] = 0.75;

//$Zombie::BaseSpeedUpdateTime: How long (in ms) between each zombie update. The lower the number, the smoother the movement, but the more processing needed
$Zombie::BaseSpeedUpdateTime = 100;
//$Zombie::SpeedUpdateTime[#]: An override to the base update type, use for specific types that need slower or faster processing between AI steps
$Zombie::SpeedUpdateTime[3] = 500;
$Zombie::SpeedUpdateTime[5] = 500;
$Zombie::SpeedUpdateTime[6] = 500;

//$Zombie::LungeDistance: How far (m) a zombie must be to lunge at a target
$Zombie::LungeDistance = 10;

// SPECIFIC ZOMBIE TYPE GLOBALS

//$Zombie::LordGrabDistance: How far (m) a zombie lord must be to grab a target
$Zombie::LordGrabDistance = 5;
//$Zombie::ZombieLordShieldHealth: How much health the zombie lord energy barrier has
$Zombie::ZombieLordShieldHealth = 10.0;
//$Zombie::ZombieLordShieldEnergy: How much energy the shield starts with. Note: Multiply this value by $Zombie::SpeedUpdateTime[3] to determine how long in MS the shield will be up.
$Zombie::ZombieLordShieldEnergy = 30;
//$Zombie::ZombieLordPhotonCooldown: How long (in ms) that the zombie lord's photon cannon must cool down for
$Zombie::ZombieLordPhotonCooldown = 15000;
//$Zombie::ZombieLordPhotonMinRange: The minimum range required for photon cannon strikes (Infantry only)
$Zombie::ZombieLordPhotonMinRange = 35;

//$Zombie::DemonZombieFireBombCooldown: How long (in ms) that a demon zombie must wait in between firebomb attacks
$Zombie::DemonZombieFireBombCooldown = 4500;
//$Zombie::DemonZombieFireBombMinRange: The minimum required distance that a demon firebomb can be thrown from
$Zombie::DemonZombieFireBombMinRange = 20;
//$Zombie::DemonZombieFireBombMaxRange: The maximum range of the demon firebomb attack
$Zombie::DemonZombieFireBombMaxRange = 250; 

//$Zombie::RapierUpwardScaling: How fast a rapier zombie will ascend when holding a player
$Zombie::RapierUpwardScaling = 750;

//$Zombie::DemonLord_FireLunge_Thrust: The velocity scalar of the thrust associated with the fire lunge attack
$Zombie::DemonLord_FireLunge_Thrust = 4000;
//$Zombie::DemonLord_FireLunge_MinimumRange: The minimum range following the burst of speed to trigger the fire explosion
$Zombie::DemonLord_FireLunge_MinimumRange = 10;
//$Zombie::DemonLord_FirestormTrigger: How long in miliseconds between the firestorm charge up and the attack itself
$Zombie::DemonLord_FirestormTrigger = 1000;
//$Zombie::DemonLord_MinionSpawnChance: The chance scalar associated with the minion summon attack (The larger this number, the less likely)
$Zombie::DemonLord_MinionSpawnChance = 120;

//$Zombie::Shifter_Teleport_MaximumRange: The maximum range at which a shifter is allowed to teleport from
$Zombie::Shifter_Teleport_MaximumRange = 400;
//$Zombie::Shifter_Teleport_PrepTime: The amount of time (in ms) that it takes for the shifter to lock down before teleporting
$Zombie::Shifter_Teleport_PrepTime = 1500;
//$Zombie::Shifter_Teleport_Cooldown: The cooldown between each teleport
$Zombie::Shifter_Teleport_Cooldown = 12500;

//$Zombie::Summoner_Cooldown: The cooldown of the summoner zombie's summon ability
$Zombie::Summoner_Cooldown = 25000;

//MISC Globals, Do not edit.
$Zombie::killpoints = 5;
$Zombie::RogThread = "cel1";

//**************************************************************************\\
//**************************************************************************\\
//**************************************************************************\\
//**************************************************************************\\
//**************************************************************************\\
//************************ ZOMBIE CORE FUNCTIONING *************************\\
//**************************************************************************\\
//**************************************************************************\\
//**************************************************************************\\
//**************************************************************************\\
//**************************************************************************\\

function TWM2Lib_Zombie_Core(%functionName, %arg1, %arg2, %arg3, %arg4) {
	switch$(strlwr(%functionName)) {
		
		//definezspeed(%type): Called upon creation, determines the zombie's base speed
		case "definezspeed":
			%type = %arg1;
			if(!isSet(%type)) {
				%type = 1;
			}
			%speed = $Zombie::BaseSpeed;
			if(isSet($Zombie::TypeSpeed[%type])) {
				%speed = $Zombie::TypeSpeed[%type];
			}
			%multiplier = 1;
			if(isSet($Zombie::SpeedMultiplier[%type])) {
				%multiplier = $Zombie::SpeedMultiplier[%type];
			}
			%final = %speed * %multiplier;
			return %final;		
		
		//playzaudio(%zombie, %chanceToPlay, %chanceHowl): Plays the moaning sounds associated with zombies
		case "playzaudio":
			if(!isObject(%arg1) || %arg1.getState() $= "dead") {
				return;
			}
			if(!isSet(%arg2)) {
				%arg2 = 50;
			}
			if(!isSet(%arg3)) {
				%arg3 = 35;
			}
			%chance = (getrandom() * %arg2);
			if(%chance >= (%arg2 - 1)) {
				%chance = (getRandom() * %arg3);
				if(%chance <= (%arg3 - 1)) {
					serverPlay3d("ZombieMoan", %arg1.getWorldBoxCenter());
				}
				else {
					serverPlay3d("ZombieHOWL", %arg1.getWorldBoxCenter());
				}
			}			
		
		//zsetrandommove(%zombie): Activated when the zombie needs to begin random movement
		case "zsetrandommove":
			if(!isObject(%arg1) || arg1.getState() $= "dead") {
				return;
			}
			%rx = getRandom(-10, 10);
			%ry = getRandom(-10, 10);
			%vec = %rx @ SPC @ %ry @ SPC @ 0;
			%arg1.direction = vectorNormalize(%vec);
			%arg1.Mnum = getRandom(1, 20);
			%arg1.zombieRmove = schedule($Zombie::SpeedUpdateTime, %arg1, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %arg1);
			
		//zrandommoveloop(%zombie): Moves the zombies around in a random direction
		case "zrandommoveloop":
			if(!isObject(%arg1) || %arg1.getState() $= "dead") {
				return;
			}
			if(%arg1.hasTarget) {
				%arg1.direction = "";
				return;
			}
			if(%arg1.Mnum >= 1) {
				%x = getWord(%arg1.direction, 1);
				%y = getWord(%arg1.direction, 0) * -1;
				%vec = %x @ SPC @ %y @ SPC @ 0;
				%arg1.setRotation(fullrot("0 0 0", %vec));
				%speed = %arg1.speed;
				%vector = vectorScale(%vec, %speed);
				%arg1.applyImpulse(%arg1.direction, %vector);
				%arg1.Mnum -= 1;
				%arg1.zombieRmove = schedule($Zombie::SpeedUpdateTime, %arg1, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %arg1);
			}
			else {
				%arg1.zombieRmove = schedule($Zombie::SpeedUpdateTime, %arg1, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %arg1);
			}
			
		//cureInfection(%player): Cures the zombie infection
		case "cureinfection":
			if(%arg1.infected) {
				%arg1.infected = 0;
				if(isEventPending(%arg1.infectedDamage)) {
					cancel(%arg1.infectedDamage);
					%arg1.infectedDamage = "";
					%arg1.beats = 0;
					%arg1.canZkill = 0;
					cancel(%arg1.zombieAttackImpulse);
					%arg1.setcancelimpulse = 1;
					schedule(5000,0, "eval", ""@%arg1@".setcancelimpulse=0;"); //goodie
				}
			}		
			
		//infectloop(%player, %infectSource): Performs the infection loop.
		case "infectloop":
			//Check for flags that disable this.
			if($TWM::PlayingHellJump || !$TWM::AllowZombieInfection) {
				return;
			}
			if(!isObject(%arg1) || %arg1.getState() $= "dead" || %arg1.isGoingToDie) {
				return;
			}
			if(%arg1.isBoss || !%arg1.infected) {
				return;
			}
			//Additional checks...
			if(%arg1.client !$= "") {
				if(%arg1.client.isActivePerk("No-Infect Armor")) {
					%arg1.playShieldEffect("1 1 1");
					%arg1.infected = 0;
					return;
				}
			}
			//
			if(%arg2 $= "impact") {
				if(%arg1.usingPlasmasaber) {
					%arg1.playShieldEffect("1 1 1");
					%arg1.infected = 0;
					return;				
				}
			}
			//Once we reach this point, we're good to go...
			if(%arg1.beats $= "") {
				TWM2Lib_Zombie_PlayerFunctions("zombieAttackImpulse", %arg1, 0);
			}
			
			if(arg1.beats < 15) {
				%arg1.setWhiteOut(%arg1.beats * 0.05);
			}
			else {
				%arg1.setDamageFlash(1);
			}
			
			if(%arg1.beats == 15) {
				%arg1.canZKill = 1;
			}
			
			if(%arg1.beats >= 15) {
				serverPlay3D("ZombieMoan", %arg1.getWorldBoxCenter());
			}
			else if(%arg1.beats >= 10) {
				playDeathCry(%arg1);
			}
			else {
				playPain(%arg1);
			}
			
			if(%arg1.beats == 20) {
				if($Host::canZombie $= "") {
					$Host::canZombie = 0;
				}
				if($Host::canZombie) {
					TWM2Lib_Zombie_PlayerFunctions("makePersonZombie", %arg1.client, %arg1.getTransform());
				}
				else {
					%arg1.damage(0, %arg1.getPosition(), 100.0, $DamageType::Zombie);
				}
				return;
			}
			%arg1.beats++;
			%arg1.infectedDamage = schedule(2000, %arg1, "TWM2Lib_Zombie_Core", "infectLoop", %arg1);
			
		//setZFlag(%zombie, %flag, %value): Sets a flag value for a zombie
		case "setzflag":
			if(!isObject(%arg1)) {
				return;
			}
			if(!isSet(%arg3)) {
				%arg3 = 0;
			}
			switch$(strlwr(%arg2)):
				case "canjump":
					%arg1.canJump = %arg3;
				case "recentshift":
					%arg1.recentShift = %arg3;
				case "cansummon":
					%arg1.cansummon = %arg3;
				case "canshield":
					%arg1.canShield = %arg3;
				case "firingweapon":
					%arg1.firingWeapon = %arg3;
				case "canfireweapon":
					%arg1.canFireWeapon = %arg3;
					
		//lookForTarget(%zombie, [%pilot], [%groundVeh]): Identify the closest target, and the distance to that target
		case "lookfortarget":
			if(!isObject(%arg1) || %arg1.getState() $= "dead") {
				return;
			}
			%worldPos = %arg1.getWorldBoxCenter();
			%zPos = getWord(%worldPos, 2);
			if(%zPos < $Zombie::FallDieHeight) {
				%arg1.scriptKill(0);
				return;
			}
			//Are we looking for pilots?
			if(isSet(%arg2) && %arg2 == true) {
				%needPilot = true;
				if(isSet(%arg3)) {
					if(%arg3 == true) {
						%needTank = true;
					}
					else {
						%needJet = true;
					}
				}
			}
			%clientCount = ClientGroup.getCount();
			%closestClient = -1;
			%closestDistance = 999999;
			for(%i = 0; %i < ClientGroup.getCount(); %i++) {
				%cl = ClientGroup.getObject(%i);
				if(isObject(%cl.player) && %cl.player.getState() !$= "dead" && TWM2Lib_Zombie_Core("ZombieTargetingAllowed", %arg1, %cl.player)) {
					if(%needPilot) {
						//Are we a pilot?
						if(!%cl.player.isPilot()) {
							//Nope...
							continue;
						}
						%vObj = %cl.vehicleMounted;
						if(isObject(%vObj)) {
							//Check the type...
							if(%needTank) {
								if(%vObj.classname !$= "HoverVehicle" && %vObj.classname !$= "WheeledVehicle") {
									continue;
								}
							}
							else if(%needJet) {
								if(%vObj.classname !$= "FlyingVehicle") {
									continue;
								}
							}
						}
						else {
							//Not mounted... move along
							continue;
						}
					}
					%vDist = vectorDist(%worldPos, %cl.player.getWorldBoxCenter());
					if(%vDist > 0 && %vDist < %closestDistance) {
						%closestClient = %cl;
						%closestDistance = %vDist;
					}
				}
			}
			return %closestClient SPC %closestDistance;
			
		//zombieTargetingAllowed(%zombie, %player): Verifies that the target player can be targeted by zombies
		case "zombietargetingallowed":
			//Object tests
			if(!isObject(%arg1) || %arg1.getState() $= "dead") {
				return false;
			}
			if(!isObject(%arg2) || %arg2.getState() $= "dead") {
				return false;
			}
			//Flag Tests
			if(%arg2.isFTD || %arg2.stealthed || %arg2.isBoss || %arg2.iszombie || %arg2.isGoingToDie || %arg2.inZombieSafeZone || %arg2.ignoredbyZombs) {
				return false;
			}
			//NOTE: If you want to add additional targeting constraints, do so here.
			return true;
			
		//zombieGetFacingDirection(%zombie, %lookPos): Rotate a zombie object to the correct "look" direction
		// NOTE: I revised this function to be a lot more effective, and to allow having zombies able to do lookAt when moving to non-player positions
		case "zombiegetfacingdirection":
			if(!isObject(%arg1) || %arg1.getState() $= "dead") {
				return;
			}
			//
			if(!isSet(%arg2)) {
				%tPos = TWM2Lib_MainControl("RMPG");
			}
			else {
				%tPos = %arg2;
			}
			//
			%vec = vectorNormalize(vectorSub(%tPos, %arg1.getWorldBoxCenter()));
			%vx = getWord(%vector, 0)
			%vy = getWord(%vector, 1)
			%nvx = %vy
			%nvy = (%vx * -1)
			%lookVector = %nvx @ SPC @ %nvy @ SPC @ 0;
			%arg1.setRotation(fullRot("0 0 0", %lookVector));
			
			return %vec;
			
		//getrandomzombietype(%list): Fetch a random type of zombie from a given list
		case "getrandomzombietype":
			%select = getWord(%arg1, getRandom(0, getWordCount(%arg1)));
			if(%select $= "") {
				return 1;
			}
			return %select;
			
		//canspawnzombies(%source): Flag testing function to validate that a zombie is allowed to spawn
		case "canspawnzombies":
			if(strlwr(%arg1) $= "infectedplayer") {
				if($Game::ZombieCount > $TWM2::MaxZombies || !$TWM2::CanSpawnZ || $TWM::PlayingInfection || $TWM::PlayingHorde || $TWM::PlayingHellJump) {
					return false;
				}
				return true;
			}
			else if(strlwr(%arg1) $= "zpack") {
				if($Game::ZombieCount > $TWM2::MaxZombies || !$TWM2::CanSpawnZ || $Host::LivingWorldMode) {
					return false;
				}		
				return true;
			}
			else if(strlwr(%arg1) $= "zspawncommand") {
				if($Game::ZombieCount > $TWM2::MaxZombies || !$TWM2::CanSpawnZ || $Host::LivingWorldMode) {
					return false;
				}		
				return true;
			}	
			else if(strlwr(%arg1) $= "forced") {
				return true;
			}	
			echo("TWM2Lib_Zombie_Core(canSpawnZombies): blocking zombie spawning, no source argument provided...");
			return false;
			
		//spawnZombie(%source, %type [%obj], %position)
		case "spawnzombie":
			if(!TWM2Lib_Zombie_Core("canSpawnZombies", %arg1)) {
				return false;
			}
			//Fetch spawning parameters
			switch$(strlwr(%arg1)) {
				//infectedPlayer: Uses %obj as second argument, where %obj is the dead player
				case "infectedplayer":
					if(!isObject(%arg2)) {
						return false;
					}
					%spawnPos = %arg2.getPosition();
					%spawnType = 1;
				
				//zPack: Uses %obj as the second argument, where %obj is the pack object
				case "zpack":
					if(!isObject(%arg2)) {
						return false;
					}
					%spawnPos = vectorAdd(%arg2.getPosition(), "0 0 4");
					%spawnType = %arg2.ZType;
				
				//zSpawnCommand / forced: Uses %type and %position arguments
				case "zspawncommand" or "forced":
					%spawnPos = %arg3;
					%spawnType = %arg2;
			}
			//Address parameters
			if(%type $= "" || %type == 7 || %type == 8 || %type <= 0) {
				%type = 1;
			}			
			if(!isSet(%spawnPos)) {
				echo("TWM2Lib_Zombie_Core(spawnZombie): Spawning position not specified, breaking.");
				return false;
			}
			//Spawn the zombie.
			switch(%spawnType) {
				//Normal Zombie
				case 1:
					%zombie = new player() {
						Datablock = "ZombieArmor";
					};
					if(Game.CheckModifier("YouCantSeeMe") == 1) {
						%zombie.setCloaked(true);
					}		
					
				//Ravager Zombie
				case 2:
					%zombie = new player() {
						Datablock = "RavagerZombieArmor";
					};
				
				//Zombie Lord
				case 3:
					%zombie = new player() {
						Datablock = "LordZombieArmor";
					};
					%zombie.client = $zombie::Lclient;
					%zombie.mountImage(ZHead, 3);
					%zombie.mountImage(ZBack, 4);
					%zombie.mountImage(ZDummyslotImg, 5);
					%zombie.mountImage(ZDummyslotImg2, 6);
					%zombie.mountImage(zLordPhotonCannonImg, 7);
					%zombie.canFireWeapon = 1;
					%zombie.canShield = 1;				
				
				//Demon Zombie
				case 4:
					%zombie = new player() {
						Datablock = "DemonZombieArmor";
					};
					%zombie.mountImage(ZdummyslotImg, 4);	
					%zombie.canFireWeapon = 1;
				
				//Air-Rapier Zombie
				case 5:
					%zombie = new player() {
						Datablock = "RapierZombieArmor";
					};
					%zombie.mountImage(ZWingImage, 3);
					%zombie.mountImage(ZWingImage2, 4);
					%zombie.setActionThread("scoutRoot",true);				
				
				//Demon Mother (Lord) Zombie
				case 6:
					%zombie = new player() {
						Datablock = "DemonMotherZombieArmor";
					};	
					%zombie.mountImage(ZdummyslotImg, 4);
					%zombie.setInventory(AcidCannon, 1, true);
					%zombie.use(AcidCannon);	
					%zombie.justshot = 0;
					%zombie.justmelee = 0;
					%zombie.noHS = 1;					
				
				//Shifter Zombie
				case 9:
					%zombie = new player() {
						Datablock = "ShifterZombieArmor";
					};				
				
				//Zombie Summoner
				case 10:
					%zombie = new player() {
						Datablock = "SummonerZombieArmor";
					};
					%zombie.canSummon = 1;
				
				//Sniper Zombie
				case 11:
					%zombie = new player(){
						Datablock = "SniperZombieArmor";
					};
					%zombie.mountImage(ZSniperImage1, 4);
					%zombie.mountImage(ZSniperImage2, 5);
				
				//Ultra-Demon Zombie
				case 12:
					%zombie = new player(){
						Datablock = "DemonUltraZombieArmor";
					};
					%zombie.NoHS = 1;
				
				//Volatile Ravager Zombie
				case 13:
					%zombie = new player(){
						Datablock = "VolatileRavagerZombieArmor";
					};
					%zombie.mountImage(ZExplosivePack, 4);				
				
				//Sling-Shot Zombie
				case 14:
					%zombie = new player(){
						Datablock = "SSZombieArmor";
					};
					%zombie.mountImage(SSZombImage2, 4);
					%zombie.mountImage(SSZombImage3, 5);				
				
				//Wraith Zombie
				case 15:
					%zombie = new player(){
						Datablock = "WraithZombieArmor";
					};
					%zombie.setInventory(AcidCannon, 1, true);
					%zombie.use(AcidCannon);				
				
				//(Classic) General Rog
				case 16:
					%zombie = new player() {
						Datablock = "ROGZombieArmor";
					};
					%zombie.NoHS = 1;
					%zombie.isBoss = 1;

					%zombie.mountImage(ZdummyslotImg, 4);
					%zombie.mountImage(ZMG42BaseImage, 5);
					%zombie.mountImage(ROGSAWImage1, 6);
					%zombie.mountImage(ROGSAWImage2, 7);
					%zombie.mountImage(ROGSAWImage3, 8);

					%zombie.shotsfired = 0;
					RapierShieldApply(%zombie);				
				
				//Elite Demon (Vardison Minion)
				case 17:
					%zombie = new player(){
						Datablock = "EliteDemonZombieArmor";
					};
					%zombie.mountImage(ZdummyslotImg, 4);				
			}
			//Verify that we spawned a zombie object, force spawn a normal zombie if we did not.
			if(!isObject(%zombie)) {
				echo("Zero zombie error, spawning normal");
				%spawnType = 1;
				%zombie = new player(){
					Datablock = "ZombieArmor";
				};
				if(Game.CheckModifier("YouCantSeeMe") == 1) {
					%zombie.setCloaked(true);
				}
			}			
			//Post spawn arguments
			%zombie.team = 30;
			%zname = $TWM2::ZombieName[%spawnType]; // <- To Hosts, Enjoy, You can
											   //Change the Zombie Names now!!!
			%zombie.target = createTarget(%zombie, %zname, "", "Derm3", '', %zombie.team, PlayerSensor);
			setTargetSensorData(%zombie.target, PlayerSensor);
			setTargetSensorGroup(%zombie.target, 30);
			setTargetName(%zombie.target, addtaggedstring(%zname));
			setTargetSkin(%zombie.target, 'Horde');
			//
			%zombie.type = %spawnType;
			%zombie.setTransform(%spawnPos);
			%zombie.canjump = 1;
			%zombie.hastarget = 1;
			%zombie.isZombie = 1;
			ZombieGroup.add(%zombie);

			$Game::ZombieCount++;
			
			if(strlwr(%arg1) $= "zpack") {
				if(%arg2.isInTheMission) {
					%zombie.isInTheMission = 1;
				}
				%zombie.hasCP = 1;
				if(%arg2.spawnTypeset == 1) {
					%arg2.numZ = 3;
				}
				else {
					%zombie.CP = %arg2;	
				}
			}
			
			if(strlwr(%arg1) $= "infectedplayer") {
				%zombie.zapObject();
				revivestand(%zombie, 0);			
			}
			//Define Speed Parameters
			%zombie.speed = TWM2Lib_Zombie_Core("defineZSpeed", %spawnType);		
			if(!isSet($Zombie::SpeedUpdateTime[%spawnType])) {
				%zombie.updateTimeFrequency = $Zombie::BaseSpeedUpdateTime;
			}
			%zombie.updateTimeFrequency = $Zombie::SpeedUpdateTime[%spawnType];			
			//Define Damage Parameters
			if(!isSet($Zombie::InfectOnCollide[%spawnType])) {
				%zombie.damage_infectOnTouch = true;
			}
			else {
				%zombie.damage_infectOnTouch = $Zombie::InfectOnCollide[%spawnType];
			}
			
			if(!isSet($Zombie::TypeDamage[%spawnType])) {
				%zombie.damage_amountOnTouch = $Zombie::BaseDamage;
			}
			else {
				%zombie.damage_amountOnTouch = $Zombie::TypeDamage[%spawnType];
			}
			
			if(!isSet($Zombie::TypeInfectedMultiplier[%spawnType])) {
				%zombie.damage_alreadyInfectedMultiplier = 1;
			}
			else {
				%zombie.damage_alreadyInfectedMultiplier = $Zombie::TypeInfectedMultiplier[%spawnType];
			}
			//Begin the AI
			%zombie.getDatablock().AI(%zombie);
			return %zombie;
	}
}