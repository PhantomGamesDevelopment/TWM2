//LivingWorldMode.cs
//Phantom139
//TWM2 3.9.2
// Primary script control for the living world mode function

// Living World Mode Details
//  * Adds in-game events and enemy AI spawns outside of the need of an administrator to enable functioning
//   - Enemy spawns are based upon player "activity", so a more active player will see more enemies near them
//   - Conversely, inactive players will only see a few enemies here and there, and will usually be able to avoid combat if they want to
//   - Enemy spawns will not occur near the map's main spawn point to prevent spawn camping
//  * Boss Fights Disabled
//  * Operations are still allowed

// Living World Content
//  * Threat Level: Indicates how much "action" is occuring, allows for more enemies and stronger enemies to spawn
//  * Counter-Ops: Enemy forces will attempt to move into the region to secure assets or attack players, these public events are open to allowed
//  * Boss Arena: When the Threat Level is maximized, hostile command elements will arrive, players can enter these zones to engage mini-boss enemies.

function livingWorldOff() {
	$Host::LivingWorldMode = 0;
	LivingWorldLib_Function("DisableLivingWorld");
}

function livingWorldOn() {
	$Host::LivingWorldMode = 1;
	LivingWorldLib_Function("EnableLivingWorld");
}

function LivingWorldLib_Function(%functionName, %arg1, %arg2, %arg3, %arg4, %arg5) {
	switch$(%functionName) {
		//EnableLivingWorld(): No Arguments
		// - Activate the script container instance responsible for storing information
		case "EnableLivingWorld":
			echo("LivingWorldLib_Function(): enable living world");
			if(isObject($TWM2::LivingWorldContainer)) {
				$TWM2::LivingWorldContainer.delete();
			}
			$TWM2::LivingWorldContainer = new ScriptObject() {
				class = "LivingWorldContainer";
				threatLevel = 0;
				instanceObjects = new SimGroup(LivingWorldInstanceObjects);
			};
		
		case "DisableLivingWorld":
			echo("LivingWorldLib_Function(): disable living world");
			//Loop through the container, find and fade-delete all instance objects
			%group = NameToID("LivingWorldInstanceObjects");
			for(%i = 0; %i < %group.getCount(); %i++) {
				%obj = %group.getObject(%i);
				if(%obj.isPlayer()) {
					%obj.scriptKill(0);
					%obj.startFade(200, 0, true);
					%obj.schedule(205, delete);
				}
				else {
					//Fade-Delete
					%obj.startFade(500, 0, true);
					%obj.schedule(505, delete);
				}
			}
			%group.delete();			
			if(isObject($TWM2::LivingWorldContainer)) {
				$TWM2::LivingWorldContainer.delete();
			}		
	}
}