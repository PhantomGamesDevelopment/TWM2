//BossSystem.cs
//Robert C. Fritzen (Phantom139)
//TWM 2

function InitiateBoss(%Boss, %name) {
	if($TWM::PlayingHorde || $TWM::PlayingHelljump) {
		error("SERVER: Cannot initiate boss, in horde/helljump");
		return;
	}

	if(!isObject($TWM2::BossManager)) {
		$TWM2::BossManager = new scriptObject() {
			class = "BossManager";
		};
	}
	if(!%Boss.isMultiPhaseBoss && !%Boss.isFirstPhase) {
		$TWM2::BossManager.bossKills = 0;
	}
	$TWM2::BossManager.bossObject = %Boss;
	$TWM2::BossManager.activeBoss = %name;

	$TWM2::BossGoing = 1;
	switch$(%name) {
		case "Yvex":
			%print = "<color:FF0000>BOSS BATTLE \n LORD YVEX";
		case "CnlWindshear":
			%print = "<color:FF0000>BOSS BATTLE \n COLONEL WINDSHEAR";
		case "GhostOfLightning":
			%print = "<color:FF0000>BOSS BATTLE \n GHOST OF LIGHTNING";
		case "Vengenor":
			%print = "<color:FF0000>BOSS BATTLE \n GENERAL VENGENOR";
		case "LordRog":
			%print = "<color:FF0000>BOSS BATTLE \n LORD ROG";
		case "Insignia":
			%print = "<color:FF0000>BOSS BATTLE \n MAJOR INSIGNIA";
		case "Vardison1":
			%print = "<color:FF0000>BOSS BATTLE \n LORD VARDISON";
		case "Vardison2":
			%print = "<color:FF0000>BOSS ALERT \n LORD VARDISON HAS ENTERED HIS SECOND FORM";
		case "Vardison3":
			%print = "<color:FF0000>BOSS ALERT \n LORD VARDISON HAS ENTERED HIS FINAL FORM";
		case "Trevor":
			%print = "<color:FF0000>BOSS BATTLE \n LORDRANIUS TREVOR";
		case "Stormrider":
			%print = "<color:FF0000>CLASSIC BOSS BATTLE \n COMMANDER STORMRIDER";
		case "GhostOfFire":
			%print = "<color:FF0000>CLASSIC BOSS BATTLE \n GHOST OF FIRE";
		case "ShadeLord":
			%print = "<color:FF0000>BOSS BATTLE \n THE SHADE LORD";
	}
	//INITIATE TO CLIENTS
	%count = ClientGroup.getCount();
	for(%i = 0; %i < %count; %i++) {
		%cl = ClientGroup.getObject(%i);
		BottomPrint(%cl, ""@%print@"", 5, 3);
		%cl.damageToBoss = 0;
		resetBossProficiency(%cl);
	}
	%boss.isBoss = 1; // the isBoss Flag helps us out with things
	BossCheckUp(%boss, %name);

	if($BossMaxHealth[%name] $= "") {
		$BossMaxHealth[%name] = %boss.getMaxDamage();
	}
}

function resetBossProficiency(%client) {
	if(isObject(%client.bossProficiency)) {
		%client.bossProficiency.delete();
	}
	%client.bossProficiency = new ScriptObject() {
		class = "BossProficiency";
		name = "ClientBossProficiency_" + %client;
		client = %client;
	};
}

function BossCheckUp(%boss, %name) {
	%percentage = (mFloor(%boss.getDamageLeft()*100) / mFloor(%boss.getMaxDamage()*100)) * 100;
	MessageAll('MsgSPCurrentObjective1', "", "Boss Battle: "@$TWM2::BossName[%name]@" [Boss Kill Count: "@$TWM2::BossManager.bossKills@"]");
	MessageAll('MsgSPCurrentObjective2', "", "Boss HP: "@mFloor(%boss.getDamageLeft()*100)@"/"@mFloor(%boss.getMaxDamage()*100)@" ("@%percentage@"%)");

	if(%name !$= "CnlWindshear" && %name !$= "Trevor" && %name !$= "Stormrider") {
		if(!isObject(%boss) || %boss.getState() $= "dead") {
			if(%name $= "Vardison1") {
				%count = ClientGroup.getCount();
				for(%i = 0; %i < %count; %i++) {
					%cl = ClientGroup.getObject(%i);
					recordAction(%cl, "BOSS", "Vardison1");
				}
				SpawnVardison2(%boss.getPosition());
				return;
			}
			if(%name $= "Vardison2") {
				%count = ClientGroup.getCount();
				for(%i = 0; %i < %count; %i++) {
					%cl = ClientGroup.getObject(%i);
					recordAction(%cl, "BOSS", "Vardison2");
				}
				SpawnVardison3(%boss.getPosition());
				return;
			}
			//the boss has been defeated, horrah!!!
			%count = ClientGroup.getCount();
			for(%i = 0; %i < %count; %i++) {
				%cl = ClientGroup.getObject(%i);
				if(%cl.damageToBoss > 0) {
					%cl.GiveBossAward(%name);
				}
			}
			$TWM2::BossGoing = 0;
			MessageAll('MsgSPCurrentObjective1', "", "Welcome to TWM2!");
			MessageAll('MsgSPCurrentObjective2', "", "Phantom139, DoL, Signal360");
			return;
		}
		schedule(1000, 0, "BossCheckUp", %boss, %name);
	}
	else {
		if(!isObject(%boss)) {
			%count = ClientGroup.getCount();
			for(%i = 0; %i < %count; %i++) {
				%cl = ClientGroup.getObject(%i);
				if(%cl.damageToBoss) {
					%cl.GiveBossAward(%name);
				}
			}
			MessageAll('MsgSPCurrentObjective1', "", "Welcome to TWM2!");
			MessageAll('MsgSPCurrentObjective2', "", "Phantom139, DoL, Signal360");
			$TWM2::BossGoing = 0;
			return;
		}
		schedule(1000, 0, "BossCheckUp", %boss, %name);
	}
}

function GameConnection::GiveBossAward(%client, %bossName) {
	%scriptController = %client.TWM2Core;
	%file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
	//you earn less EXP every time you defeat a specific boss, so tread lightly on those defeat counts :)

	%damageCount = %client.damageToBoss;
	%maxHP = $BossMaxHealth[%bossName];

	%percentage = (%damageCount / %maxHP) * 100;   
	if(%percentage > 5) {
		recordAction(%client, "BOSS", %bossName);

		if(!isSet(%scriptController.bossDefeatCount[%bossName])) {
			%scriptController.bossDefeatCount[%bossName] = 0;
		}
		if(%bossName $= "Yvex") {
			AwardClient(%client, "1");
		}
		else if(%bossName $= "CnlWindshear") {
			AwardClient(%client, "8");
		}
		else if(%bossName $= "GhostOfLightning") {
			AwardClient(%client, "9");
		}
		else if(%bossName $= "Vengenor") {
			AwardClient(%client, "10");
		}
		else if(%bossName $= "LordRog") {
			AwardClient(%client, "11");
		}
		else if(%bossName $= "Insignia") {
			AwardClient(%client, "12");
		}
		else if(%bossName $= "GhostOfFire") {
			AwardClient(%client, "27");
		}
		else if(%bossName $= "Stormrider") {
			AwardClient(%client, "28");
		}
		else if(%bossName $= "ShadeLord") {
			AwardClient(%client, "30");
		}
		//VARDISON
		else if(%bossName $= "Vardison3") {
			AwardClient(%client, 13);
			if($TWM2::VardisonDifficulty == 1) {
				CompleteNWChallenge(%client, "VardEasy");
			}
			else if($TWM2::VardisonDifficulty == 2) {
				CompleteNWChallenge(%client, "VardNorm");
			}
			else if($TWM2::VardisonDifficulty == 3) {
				CompleteNWChallenge(%client, "VardHard");
			}	
			else if($TWM2::VardisonDifficulty == 4) {
				CompleteNWChallenge(%client, "VardWtf");
			}		 
		}
		else if(%bossName $= "Trevor") {
			AwardClient(%client, 15);
		}
		//rank writing
		%scriptController.bossDefeatCount[%bossName]++;
		%scriptController.save(%file);

		%award = mFloor($TWM2::BossXPAward[%bossName] / %scriptController.bossDefeatCount[%bossName]);
		GainExperience(%client, %award, ""@%bossName@" defeated, congratulations! ");
		CheckBossChallenge(%client, %bossName);
		CheckBossProficiency(%client, %bossName);
	}
	else {
		MessageClient(%client, 'msgFailed', "\c5Command: The boss was defeated, however your input to the team effort was minimal... you must provide support to your allies in need.");
		MessageClient(%client, 'msgFailed', "\c2Data: You inflicted "@%percentage@"% damage to the boss, in order to be eligable for rewards, you must inflict at least 5%.");
	}
}

function FindValidTarget(%boss, %counter) {   //This is usefull
	if(%counter $= "") {
		%counter = 10; //10 attempts
	}
	for(%i = 0; %i < %counter; %i++) {
		%test = ClientGroup.getObject(getRandom(0, ClientGroup.getCount()));
		if(isObject(%test)) {
			%tPL = %test.getControlObject();
			if(isObject(%tPL)) {
				if(isPlayer(%tPL)) {
					if(%tPL.getState() !$= "dead") {
						//Got one!
						return %test;
					}
				}
				else {
					return %test;
				}
			}
		}
	}
	return -1; //Found nothing.
}

function CheckBossChallenge(%client, %boss) {
	%scriptController = %client.TWM2Core;
	%dc = %scriptController.bossDefeatCount[%boss];
	switch$(%boss) {
		case "Yvex":
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "Yvex1");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "Yvex2");
			}
			if(%dc >= 10) {
				CompleteNWChallenge(%client, "Yvex3");
			}
		case "CnlWindshear":
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "CWS1");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "CWS2");
			}
			if(%dc >= 10) {
				CompleteNWChallenge(%client, "CWS3");
			}
		case "GhostOfLightning":
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "GOL1");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "GOL2");
			}
			if(%dc >= 10) {
				CompleteNWChallenge(%client, "GOL3");
			}
		case "GhostOfFire":
			if(%dc >= 1) {
				CompleteNWChallenge(%client, "GOF1");
			}
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "GOF2");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "GOF3");
			}		 
		case "Vegenor":
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "Veg1");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "Veg2");
			}
			if(%dc >= 10) {
				CompleteNWChallenge(%client, "Veg3");
			}
		case "LordRog":
			if(%dc >= 2) {
				CompleteNWChallenge(%client, "LRog1");
			}
			if(%dc >= 4) {
				CompleteNWChallenge(%client, "LRog2");
			}
			if(%dc >= 7) {
				CompleteNWChallenge(%client, "LRog3");
			}
		case "Insignia":
			if(%dc >= 2) {
				CompleteNWChallenge(%client, "Ins1");
			}
			if(%dc >= 4) {
				CompleteNWChallenge(%client, "Ins2");
			}
			if(%dc >= 7) {
				CompleteNWChallenge(%client, "Ins3");
			}
		case "Vardison3":
			if(%dc >= 1) {
				CompleteNWChallenge(%client, "Vard1");
			}
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "Vard2");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "Vard3");
			}
		case "Stormrider":
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "Stormrider1");
			}
			if(%dc >= 5) {
				CompleteNWChallenge(%client, "Stormrider2");
			}
			if(%dc >= 10) {
				CompleteNWChallenge(%client, "Stormrider3");
			}		 
		case "Trevor":
			if(%dc >= 2) {
				CompleteNWChallenge(%client, "Trev1");
			}
			if(%dc >= 4) {
				CompleteNWChallenge(%client, "Trev2");
			}
			if(%dc >= 7) {
				CompleteNWChallenge(%client, "Trev3");
			}
		case "ShadeLord":
			if(%dc >= 1) {
				CompleteNWChallenge(%client, "ShadeLord1");
			}
			if(%dc >= 2) {
				CompleteNWChallenge(%client, "ShadeLord2");
			}
			if(%dc >= 3) {
				CompleteNWChallenge(%client, "ShadeLord3");
			}		 
	}
}

function CheckBossProficiency(%client, %bossName) {
	if(!%client || %client $= "" || !isObject(%client)) {
		return;
	}
	if(%bossName $= "") {
		return;
	}
	//Proficiency: Added TWM2 3.9.2
	// Awards clients for completing feats during the boss fight
	MessageClient(%client, 'msgTag', "\c5==== PROFICIENCY AWARDS ====");
	%count = 0;
	%totalEXP = 0;
	
	%damageCount = %client.damageToBoss;
	%maxHP = $BossMaxHealth[%bossName];
	%dPerc = (%damageCount / %maxHP) * 100;   		
	
	for(%i = 0; $Boss::Proficiency[%bossName, %i] !$= ""; %i++) {
		//Proficiency Code Inputs:
		// [bossName]: Name of the boss, internally
		// [client]: client ID
		// [bProf]: client's boss proficiency object
		// [dPerc]: client's damage percentage
		%prof = $Boss::Proficiency[%bossName, %i];
		%pName = getField(%prof, 0);
		%pExp = getField(%prof, 1);
		%pDesc = getField(%prof, 2);
		%code = $Boss::ProficiencyCode[%bossName, %i];
		%eCode = strReplace(%code, "[bossName]", %bossName);
		%eCode = strReplace(%eCode, "[client]", %client);
		%eCode = strReplace(%eCode, "[bProf]", %client.bossProficiency);
		%eCode = strReplace(%eCode, "[dPerc]", %dPerc);
		//Generate the code and eval it
		%evalCode = "return "@%eCode@";";
		%award = eval(%evalCode);
		if(%award == true) {
			%totalEXP += %pExp;
			GainExperience(%client, %pExp, %pName);
			%count++;
			MessageClient(%client, 'msgFailed', "\c5* "@%pName@": "@%pDesc);
		}
	}
	if(!%count) {
		MessageClient(%client, 'msgFailed', "\c5* No Awards Were Earned, Try Again Next Time!");
	}
	else {
		MessageClient(%client, 'msgFailed', "\c5* Summary: You earned "@%count@" proficiency awards, totaling "@%totalEXP@"EXP points, well done!");
	}
}

function BossManager::addKill(%this, %tObj) {
	%this.bossKills++;
	if(isObject(%tObj.client.bossProficiency)) {
		%tObj.client.bossProficiency.bossDeaths++;
	}
}

//Load The Boss Files
exec("scripts/TWM2/Bosses/LordYvex.cs");
exec("scripts/TWM2/Bosses/ColonelWindshear.cs");
exec("scripts/TWM2/Bosses/GhostOfLightning.cs");
exec("scripts/TWM2/Bosses/GeneralVegenor.cs");
exec("scripts/TWM2/Bosses/LordRog.cs");
exec("scripts/TWM2/Bosses/MajorInsignia.cs");
exec("scripts/TWM2/Bosses/LordraniusTrevor.cs");
exec("scripts/TWM2/Bosses/Stormrider.cs");
exec("scripts/TWM2/Bosses/GhostOfFire.cs");
exec("scripts/TWM2/Bosses/ShadeLord.cs");
