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

//$Zombie::detectDist: The maximum distance in which a zombie may target a player
$Zombie::detectDist = 9999;
//$Zombie::FallDieHeight: The minimum altitude a zombie may be before being script killed
$Zombie::FallDieHeight = -500;

//$Zombie::BaseSpeed: The default speed setting on zombies, any zombie that does not have a TypeSpeed var. set will default to the BaseSpeed
$Zombie::BaseSpeed = 150;
//$Zombie::TypeSpeed[#]: The speed of a specific zombie type instance, overrides BaseSpeed for that specific type
$Zombie::TypeSpeed[2] = 300;
$Zombie::TypeSpeed[3] = 800;
$Zombie::TypeSpeed[4] = 240;
$Zombie::TypeSpeed[5] = 300;

//$Zombie::BaseJumpCooldown: The time zombies must elapse before jumping / lunging
$Zombie::BaseJumpCooldown = 1500;

//$Zombie::SpeedMultiplier[#]: An additional multiplier to be applied to the zombie's base speed when calculating the impulse
$Zombie::SpeedMultiplier[2] = 0.6;
$Zombie::SpeedMultiplier[3] = 0.8;
$Zombie::SpeedMultiplier[4] = 0.75;

//$Zombie::SpeedUpdateTime: How long (in ms) between each zombie update. The lower the number, the smoother the movement, but the more processing needed
$Zombie::SpeedUpdateTime = 100;

//$Zombie::LungeDistance: How far (m) a zombie must be to lunge at a target
$Zombie::LungeDistance = 10;
//$Zombie::LordGrabDistance: How far (m) a zombie lord must be to grab a target
$Zombie::LordGrabDistance = 5;
//$Zombie::RapierUpwardScaling: How fast a rapier zombie will ascend when holding a player
$Zombie::RapierUpwardScaling = 750;

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
		
		//zsetrandommove(%zombie): Activated when the zombie needs to begin random movement
		case "zsetrandommove":
			if(!isObject(%arg1)) {
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
				//Check for speed enhancement
				%multiplier = $Zombie::SpeedMultiplier[%arg1.type] $= "" ? 1 : $Zombie::SpeedMultiplier[%arg1.type];
				%speed = %arg1.speed * %multiplier;
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
			
		//infectloop(%player): Performs the infection loop.
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
					
		//lookForTarget(%zombie): Identify the closest target, and the distance to that target
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
			%clientCount = ClientGroup.getCount();
			%closestClient = -1;
			%closestDistance = 999999;
			for(%i = 0; %i < ClientGroup.getCount(); %i++) {
				%cl = ClientGroup.getObject(%i);
				if(isObject(%cl.player) && %cl.player.getState() !$= "dead" && TWM2Lib_Zombie_Core("ZombieTargetingAllowed", %arg1, %cl.player)) {
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
			
		//zombieGetFacingDirection(%zombie, %player, %position): Fetch the direction the zombie needs to look at for a specific player target
		case "zombiegetfacingdirection":
			if(!isObject(%arg1) || %arg1.getState() $= "dead") {
				return;
			}
			//
			if(isObject(%arg2) && %arg2.getState !$= "dead") {
				%tPos = %arg2.getPosition();
			}
			else {
				%tPos = TWM2Lib_MainControl("RMPG");
			}
			//
			%vec = vectorNormalize(vectorSub(%tPos, %arg3));
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
					%zombie.setInventory(AcidCannon, 1, true);
					%zombie.use(AcidCannon);
					%zombie.firstFired = 0;
					%zombie.canmove = 1;				
				
				//Demon Zombie
				case 4:
					%zombie = new player() {
						Datablock = "DemonZombieArmor";
					};
					%zombie.mountImage(ZdummyslotImg, 4);				
				
				//Air-Rapier Zombie
				case 5:
					%zombie = new player() {
						Datablock = "RapierZombieArmor";
					};
					%zombie.mountImage(ZWingImage, 3);
					%zombie.mountImage(ZWingImage2, 4);
					%zombie.setActionThread("scoutRoot",true);				
				
				//Demon Mother Zombie
				case 6:
					return DemonMotherCreate(%spawnPos);
				
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
			%zname = $TWM2::ZombieName[%type]; // <- To Hosts, Enjoy, You can
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

			if($Zombie::TypeSpeed[%spawnType] !$= "") {
				%zombie.speed = $Zombie::TypeSpeed[%spawnType];
			}
			else {
				%zombie.speed = $Zombie::BaseSpeed;
			}

			%zombie.getDatablock().AI(%zombie);
			return %zombie;
	}
}

//************************************************************
//*****************Zomb Attack Stuff**************************
//************************************************************

function ChargeEmitter(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.chargecount >= 2){
   	%charge2 = new ParticleEmissionDummy()
   	{
   	   position = %zombie.getMuzzlePoint(6);
   	   dataBlock = "defaultEmissionDummy";
   	   emitter = "burnEmitter";
      };
	MissionCleanup.add(%charge2);
	%charge2.schedule(100, "delete");
   }
   if(%zombie.chargecount <= 7){
   	%charge = new ParticleEmissionDummy()
   	{
   	   position = %zombie.getMuzzlePoint(5);
   	   dataBlock = "defaultEmissionDummy";
   	   emitter = "burnEmitter";
      };
	MissionCleanup.add(%charge);
	%charge.schedule(100, "delete");
   }
   if(%zombie.chargecount <= 9){
      %zombie.Fire = schedule(100, %zombie, "ChargeEmitter", %zombie);
	%zombie.chargecount++;
   }
   else
	%zombie.chargecount = 0;
}


//-----------------------------------------------------------
//DEATH
datablock AudioProfile(ZombieDeathSound1)
{
   filename    = "voice/Derm3/avo.deathcry_01.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ZombieDeathSound2)
{
   filename    = "voice/Derm2/avo.deathcry_01.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ZombieDeathSound3)
{
   filename    = "voice/Derm1/avo.deathcry_01.wav";
   description = AudioClose3d;
   preload = true;
};