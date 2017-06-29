//Challenge Menus
//TWM2 3.9.1
//Moved all of the Non-Weapon Challenge menus to a singular file
// under a single function to streamline it...

// Yes, I know I could "automate" this, but my laziness is still rather persistent to re-do the entire old system...

function GenerateChallengeSubMenu(%client, %subMenu, %tag, %index) {
	switch(%subMenu) {
		case 1:
			//Killstreaks
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Killstreak Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("UAV1")) {
				if(%client.CheckNWChallengeCompletion("UAV2")) {
					if(%client.CheckNWChallengeCompletion("UAV3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>UAV Expert III: Call in 150 UAV Recon Satellites");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert III: Call in 150 UAV Recon Satellites");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert II: Call in 75 UAV Recon Satellites");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Expert I: Call in 30 UAV Recon Satellites");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Airstrike1")) {
				if(%client.CheckNWChallengeCompletion("Airstrike2")) {
					if(%client.CheckNWChallengeCompletion("Airstrike3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Airstrike Expert III: Call in 125 Airstrikes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert III: Call in 125 Airstrikes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert II: Call in 65 Airstrikes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrike Expert I: Call in 25 Airstrikes");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("UAMS1")) {
				if(%client.CheckNWChallengeCompletion("UAMS2")) {
					if(%client.CheckNWChallengeCompletion("UAMS3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>UAMS Expert III: Call in 125 UAMS Strikes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert III: Call in 125 UAMS Strikes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert II: Call in 65 UAMS Strikes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "UAMS Expert I: Call in 25 UAMS Strikes");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Helicopter1")) {
				if(%client.CheckNWChallengeCompletion("Helicopter2")) {
					if(%client.CheckNWChallengeCompletion("Helicopter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Helicopter Expert III: Call in 125 Combat Helicopters");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert III: Call in 125 Combat Helicopters");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert II: Call in 65 Combat Helicopters");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopter Expert I: Call in 25 Combat Helicopters");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Harrier1")) {
				if(%client.CheckNWChallengeCompletion("Harrier2")) {
					if(%client.CheckNWChallengeCompletion("Harrier3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Harrier Expert III: Call in 110 Plasma Harrier Airstrikes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert III: Call in 110 Plasma Harrier Airstrikes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert II: Call in 55 Plasma Harrier Airstrikes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Expert I: Call in 20 Plasma Harrier Airstrikes");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("SatNuke1")) {
				if(%client.CheckNWChallengeCompletion("SatNuke2")) {
					if(%client.CheckNWChallengeCompletion("SatNuke3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>OLS Expert III: Call in 125 Orbital Laser Strikes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert III: Call in 125 Orbital Laser Strikes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert II: Call in 65 Orbital Laser Strikes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "OLS Expert I: Call in 25 Orbital Laser Strikes");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("NapalmHarrier1")) {
				if(%client.CheckNWChallengeCompletion("NapalmHarrier2")) {
					if(%client.CheckNWChallengeCompletion("NapalmHarrier3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Napalm Airstrike Expert III: Call in 110 Napalm Airstrikes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert III: Call in 110 Napalm Airstrikes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert II: Call in 55 Napalm Airstrikes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Napalm Airstrike Expert I: Call in 20 Napalm Airstrikes");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("GunHeli1")) {
				if(%client.CheckNWChallengeCompletion("GunHeli2")) {
					if(%client.CheckNWChallengeCompletion("GunHeli3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Gunship Helicopter Expert III: Call in 110 Gunship Helicopters");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert III: Call in 110 Gunship Helicopters");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert II: Call in 55 Gunship Helicopters");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopter Expert I: Call in 20 Gunship Helicopters");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("SBomber1")) {
				if(%client.CheckNWChallengeCompletion("SBomber2")) {
					if(%client.CheckNWChallengeCompletion("SBomber3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Stealth Bomber Expert III: Call in 100 Stealth Bombers");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert III: Call in 100 Stealth Bombers");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert II: Call in 50 Stealth Bombers");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bomber Expert I: Call in 20 Stealth Bombers");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Gunship1")) {
				if(%client.CheckNWChallengeCompletion("Gunship2")) {
					if(%client.CheckNWChallengeCompletion("Gunship3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Harbinger Gunship Expert III: Call in 75 Harbinger Gunships");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert III: Call in 75 Harbinger Gunships");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert II: Call in 35 Harbinger Gunships");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Gunship Expert I: Call in 15 Harbinger Gunships");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Apache1")) {
				if(%client.CheckNWChallengeCompletion("Apache2")) {
					if(%client.CheckNWChallengeCompletion("Apache3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Apache Gunner Expert III: Call in 75 Apache Gunners");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Gunner Expert III: Call in 75 Apache Gunners");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Gunner Expert II: Call in 35 Apache Gunners");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Apache Gunner Expert I: Call in 15 Apache Gunners");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Gunship3")) {
				if(%client.CheckNWChallengeCompletion("ACGunship1")) {
					if(%client.CheckNWChallengeCompletion("ACGunship2")) {
						if(%client.CheckNWChallengeCompletion("ACGunship3")) {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>AC-130 Expert III: Call in 75 AC-130 Gunners");
							%index++;
						}
						else {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert III: Call in 75 AC-130 Gunners");
							%index++;
						}
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert II: Call in 35 AC-130 Gunners");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "AC-130 Expert I: Call in 15 AC-130 Gunners");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked: Requires Harbinger Gunship Expert III.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Centaur1")) {
				if(%client.CheckNWChallengeCompletion("Centaur2")) {
					if(%client.CheckNWChallengeCompletion("Centaur3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Centaur Artillery Expert III: Call in 50 Artillery Strikes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert III: Call in 50 Artillery Strikes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert II: Call in 25 Artillery Strikes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Centaur Artillery Expert I: Call in 10 Artillery Strikes");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("EMP1")) {
				if(%client.CheckNWChallengeCompletion("EMP2")) {
					if(%client.CheckNWChallengeCompletion("EMP3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>EMP Expert III: Call in 25 Mass EMP's");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert III: Call in 25 Mass EMP's");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert II: Call in 10 Mass EMP's");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "EMP Expert I: Call in 5 Mass EMP's");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Nuke1")) {
				if(%client.CheckNWChallengeCompletion("Nuke2")) {
					if(%client.CheckNWChallengeCompletion("Nuke3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Nuke Expert III: Call in 25 Nukes");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert III: Call in 25 Nukes");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert II: Call in 10 Nukes");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Nuke Expert I: Call in 5 Nukes");
				%index++;
			}
			//
			if(%client.TWM2Core.Officer >= 1) {
				if(%client.CheckNWChallengeCompletion("Fission1")) {
					if(%client.CheckNWChallengeCompletion("Fission2")) {
						if(%client.CheckNWChallengeCompletion("Fission3")) {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Fission Bomb Expert III: Call in 5 Fission Bombs");
							%index++;
						}
						else {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert III: Call in 5 Fission Bombs");
							%index++;
						}
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert II: Call in 2 Fission Bombs");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Fission Bomb Expert I: Call in 1 Fission Bomb");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked: Requires Instructive Officer Rank (Off. Rank 1)");
				%index++;
			}
			//
			if(%client.TWM2Core.Officer >= 15) {
				if(%client.CheckNWChallengeCompletion("LOAS1")) {
					if(%client.CheckNWChallengeCompletion("LOAS2")) {
						if(%client.CheckNWChallengeCompletion("LOAS3")) {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>LOAS Expert III: Call in 15 Low Orbit Orbital Strikes (LOAS)");
							%index++;
						}
						else {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "LOAS Expert III: Call in 15 Low Orbit Orbital Strikes (LOAS)");
							%index++;
						}
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "LOAS Expert II: Call in 10 Low Orbit Orbital Strikes (LOAS)");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "LOAS Expert I: Call in 5 Low Orbit Orbital Strikes (LOAS)");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked: Requires Harbinger Officer Rank (Off. Rank 15)");
				%index++;
			}			
			return %index;			
			
		case 2:
			//Boss Hunting
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Boss Hunting Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("Yvex1")) {
				if(%client.CheckNWChallengeCompletion("Yvex2")) {
					if(%client.CheckNWChallengeCompletion("Yvex3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Shadowy Desecration: Defeat Lord Yvex 10 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Shadowy Desecration: Defeat Lord Yvex 10 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Darkness Rising: Defeat Lord Yvex 5 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Nightmarish Enterprise: Defeat Lord Yvex 3 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("CWS1")) {
				if(%client.CheckNWChallengeCompletion("CWS2")) {
					if(%client.CheckNWChallengeCompletion("CWS3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Harbinger's Bane: Defeat Colonel Windshear 10 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger's Bane: Defeat Colonel Windshear 10 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Aerieal Nightmare: Defeat Colonel Windshear 5 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Fortress In The Sky: Defeat Colonel Windshear 3 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("GOL1")) {
				if(%client.CheckNWChallengeCompletion("GOL2")) {
					if(%client.CheckNWChallengeCompletion("GOL3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Severe Thunderstorm: Defeat The Ghost Of Lightning 10 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Severe Thunderstorm: Defeat The Ghost Of Lightning 10 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "The Shocking Truth: Defeat The Ghost Of Lightning 5 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Envious Lightning: Defeat The Ghost Of Lightning 3 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("GOF1")) {
				if(%client.CheckNWChallengeCompletion("GOF2")) {
					if(%client.CheckNWChallengeCompletion("GOF3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Mt. Death Depleter: Defeat The Ghost Of Fire 5 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Mt. Death Depleter: Defeat The Ghost Of Fire 5 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Inceneration Ender: Defeat The Ghost Of Fire 3 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Purifier: Defeat The Ghost Of Fire");
				%index++;
			}			
			//
			if(%client.CheckNWChallengeCompletion("Veg1")) {
				if(%client.CheckNWChallengeCompletion("Veg2")) {
					if(%client.CheckNWChallengeCompletion("Veg3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Firestorm Ender: Defeat General Vegenor 10 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Firestorm Ender: Defeat General Vegenor 10 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Burning Frenzy: Defeat General Vegenor 5 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Flaming Revolt: Defeat General Vegenor 3 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("LRog1")) {
				if(%client.CheckNWChallengeCompletion("LRog2")) {
					if(%client.CheckNWChallengeCompletion("LRog3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Payback's A Bitch: Defeat Lord Rog 7 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Payback's A Bitch: Defeat Lord Rog 7 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Return to Returner: Defeat Lord Rog 4 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Revenge Halter: Defeat Lord Rog 2 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Ins1")) {
				if(%client.CheckNWChallengeCompletion("Ins2")) {
					if(%client.CheckNWChallengeCompletion("Ins3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Gravitational Influx: Defeat Major Insignia 7 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Gravitational Influx: Defeat Major Insignia 7 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "No Gravity, No Problem: Defeat Major Insignia 4 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "El Shipitor: Defeat Major Insignia 2 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Stormrider1")) {
				if(%client.CheckNWChallengeCompletion("Stormrider2")) {
					if(%client.CheckNWChallengeCompletion("Stormrider3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Harbinger Fighter Demolisher: Defeat Commander Stormrider 10 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Fighter Demolisher: Defeat Commander Stormrider 10 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Shootdown Master: Defeat Commander Stormrider 5 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Clear Skies: Defeat Commander Stormrider 3 Times");
				%index++;
			}			
			//
			if(%client.CheckNWChallengeCompletion("Treb1")) {
				if(%client.CheckNWChallengeCompletion("Treb2")) {
					if(%client.CheckNWChallengeCompletion("Treb3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Tank Halter: Defeat Lordranius Trevor 7 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Tank Halter: Defeat Lordranius Trevor 7 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Denied: Defeat Lordranius Trevor 4 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Precious Cargo: Defeat Lordranius Trevor 2 Times");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Vard1")) {
				if(%client.CheckNWChallengeCompletion("Vard2")) {
					if(%client.CheckNWChallengeCompletion("Vard3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Outevil The Wicked: Defeat Lord Vardison 5 Times");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Outevil The Wicked: Defeat Lord Vardison 5 Times");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Glare The Dark: Defeat Lord Vardison 3 Times");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Shining Star: Defeat Lord Vardison");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("VardEasy")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>The Standard Experience: Defeat Lord Vardison on Easy Difficulty");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "The Standard Experience: Defeat Lord Vardison on Easy Difficulty");
				%index++;
			}
			if(%client.CheckNWChallengeCompletion("VardNorm")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Demon Hunter: Defeat Lord Vardison on Normal Difficulty");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Demon Hunter: Defeat Lord Vardison on Normal Difficulty");
				%index++;
			}
			if(%client.CheckNWChallengeCompletion("VardHard")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Master Demon Slayer: Defeat Lord Vardison on Hard Difficulty");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Master Demon Slayer: Defeat Lord Vardison on Hard Difficulty");
				%index++;
			}
			if(%client.CheckNWChallengeCompletion("VardWtf")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>God of the Shadow Realm: You're a fucking badass... Just bask in that...");
				%index++;
				messageClient( %client, 'SetLineHud', "", %tag, %index, "");
				%index++;			
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "God of the Shadow Realm: Against all odds, emerge victorious against WTF difficulty Lord Vardison");
				%index++;
				messageClient( %client, 'SetLineHud', "", %tag, %index, "");
				%index++;			
			}
			//
			if(%client.CheckNWChallengeCompletion("ShadeLord1")) {
				if(%client.CheckNWChallengeCompletion("ShadeLord2")) {
					if(%client.CheckNWChallengeCompletion("ShadeLord3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Dawnlight Encarnate: Defeat The Shade Lord for the Third Time");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Dawnlight Encarnate: Defeat The Shade Lord for the Third Time");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Shadow Embracer: Defeat The Shade Lord Twice");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Night Stalker: Defeat The Shade Lord");
				%index++;
			}			
			//
			return %index;			
			
		case 3:
			//Wargames
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Wargames (PvP) Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("Slayer1")) {
				if(%client.CheckNWChallengeCompletion("Slayer2")) {
					if(%client.CheckNWChallengeCompletion("Slayer3")) {
						if(%client.CheckNWChallengeCompletion("Slayer4")) {
							if(%client.CheckNWChallengeCompletion("Slayer5")) {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Slayer V: Kill 1,000 Enemy Players");
								%index++;								
							}
							else {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "Slayer V: Kill 1,000 Enemy Players");
								%index++;								
							}
						}
						else {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "Slayer IV: Kill 750 Enemy Players");
							%index++;						
						}
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Slayer III: Kill 500 Enemy Players");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Slayer II: Kill 250 Enemy Players");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Slayer I: Kill 100 Enemy Players");
				%index++;
			}	
			//
			if(%client.CheckNWChallengeCompletion("Defectionator1")) {
				if(%client.CheckNWChallengeCompletion("Defectionator2")) {
					if(%client.CheckNWChallengeCompletion("Defectionator3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Defectionator III: Kill 500 \"Zombified\" Players");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Defectionator III: Kill 500 \"Zombified\" Players");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Defectionator II: Kill 250 \"Zombified\" Players");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Defectionator I: Kill 100 \"Zombified\" Players");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Infectionator1")) {
				if(%client.CheckNWChallengeCompletion("Infectionator2")) {
					if(%client.CheckNWChallengeCompletion("Infectionator3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Infectionator III: Convert 250 Players to the Zombie Horde");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Infectionator III: Convert 250 Players to the Zombie Horde");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Infectionator II: Convert 100 Players to the Zombie Horde");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Infectionator I: Convert 50 Players to the Zombie Horde...");
				%index++;
			}
			//		
			if(%client.CheckNWChallengeCompletion("HSHoncho1")) {
				if(%client.CheckNWChallengeCompletion("HSHoncho2")) {
					if(%client.CheckNWChallengeCompletion("HSHoncho3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Headshot Honcho III: Eliminate 300 Enemy Players with Headshots");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Headshot Honcho III: Eliminate 300 Enemy Players with Headshots");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Headshot Honcho II: Eliminate 200 Enemy Players with Headshots");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Headshot Honcho I: Eliminate 100 Enemy Players with Headshots");
				%index++;
			}
			//		
			if(%client.CheckNWChallengeCompletion("VehMans1")) {
				if(%client.CheckNWChallengeCompletion("VehMans2")) {
					if(%client.CheckNWChallengeCompletion("VehMans3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Vehicular Manslaughter III: Eliminate 250 Enemy Players with a vehicle");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Vehicular Manslaughter III: Eliminate 250 Enemy Players with a vehicle");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Vehicular Manslaughter II: Eliminate 100 Enemy Players with a vehicle");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Vehicular Manslaughter I: Eliminate 50 Enemy Players with a vehicle");
				%index++;
			}		
			//		
			if(%client.CheckNWChallengeCompletion("Assassin")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Assassinator: Backstab an enemy player using the Blade of Vengeance");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Assassinator: Backstab an enemy player using the Blade of Vengeance");
				%index++;
			}					
			//	
			if(%client.CheckNWChallengeCompletion("CompletelyUnexpected")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>That Was... Unexpected: Eliminate someone playing as General Rog by backstabbing them with the Blade of Vengence");
				%index++;
				messageClient( %client, 'SetLineHud', "", %tag, %index, "");
				%index++;				
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "That Was... Unexpected: Eliminate someone playing as General Rog by backstabbing them with the Blade of Vengence");
				%index++;
				messageClient( %client, 'SetLineHud', "", %tag, %index, "");
				%index++;				
			}			
			//	
			if(%client.CheckNWChallengeCompletion("Uncomprehendable")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Uncomprehendable: You committed the ultimate vehicle kill humiliation, well done!");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Uncomprehendable: Get killed in a fighter, and have the driverless vehicle run down your killer");
				%index++;
			}			
			//			
			return %index;
			
		case 4:
			//Zombie Slaying
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Zombie Slayer Challenges:");
			%index++;
			if(%client.CheckNWChallengeCompletion("NormHunter1")) {
				if(%client.CheckNWChallengeCompletion("NormHunter2")) {
					if(%client.CheckNWChallengeCompletion("NormHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Frontline Buster III: Slay 10,000 Zombies (Normal Type)");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Frontline Buster III: Slay 10,000 Zombies (Normal Type)");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Frontline Buster II: Slay 5,000 Zombies (Normal Type)");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Frontline Buster I: Slay 2,500 Zombies (Normal Type)");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("RavHunter1")) {
				if(%client.CheckNWChallengeCompletion("RavHunter2")) {
					if(%client.CheckNWChallengeCompletion("RavHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Speed Kills III: Slay 5,000 Ravager Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Speed Kills III: Slay 5,000 Ravager Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Speed Kills II: Slay 2,500 Ravager Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Speed Kills I: Slay 1,000 Ravager Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("LordHunter1")) {
				if(%client.CheckNWChallengeCompletion("LordHunter2")) {
					if(%client.CheckNWChallengeCompletion("LordHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>The Bigger They Are III: Slay 3,000 Zombie Lords");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "The Bigger They Are III: Slay 3,000 Zombie Lords");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "The Bigger They Are II: Slay 2,000 Zombie Lords");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "The Bigger They Are I: Slay 1,000 Zombie Lords");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("DemonHunter1")) {
				if(%client.CheckNWChallengeCompletion("DemonHunter2")) {
					if(%client.CheckNWChallengeCompletion("DemonHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Fire Retardant III: Slay 5,000 Demon Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Fire Retardant III: Slay 5,000 Demon Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Fire Retardant II: Slay 2,500 Demon Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Fire Retardant I: Slay 1,000 Demon Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("AirRapHunter1")) {
				if(%client.CheckNWChallengeCompletion("AirRapHunter2")) {
					if(%client.CheckNWChallengeCompletion("AirRapHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Bat Slayer III: Slay 6,000 Air Rapier Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Bat Slayer III: Slay 6,000 Air Rapier Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Bat Slayer II: Slay 3,500 Air Rapier Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Bat Slayer I: Slay 1,500 Air Rapier Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("DLordHunter1")) {
				if(%client.CheckNWChallengeCompletion("DLordHunter2")) {
					if(%client.CheckNWChallengeCompletion("DLordHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Hellspawn Erradicator III: Slay 1,500 Demon Lord Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Hellspawn Erradicator III: Slay 1,500 Demon Lord Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Hellspawn Erradicator II: Slay 1,000 Demon Lord Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Hellspawn Erradicator I: Slay 500 Demon Lord Zombies");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("ShifterHunter1")) {
				if(%client.CheckNWChallengeCompletion("ShifterHunter2")) {
					if(%client.CheckNWChallengeCompletion("ShifterHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Anti-Warp III: Slay 6,000 Shifter Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Anti-Warp III: Slay 6,000 Shifter Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Anti-Warp II: Slay 3,000 Shifter Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Anti-Warp I: Slay 1,500 Shifter Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("SummonerHunter1")) {
				if(%client.CheckNWChallengeCompletion("SummonerHunter2")) {
					if(%client.CheckNWChallengeCompletion("SummonerHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Horde Halter III: Slay 5,000 Zombie Summoners");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Horde Halter III: Slay 5,000 Zombie Summoners");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Horde Halter II: Slay 2,500 Zombie Summoners");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Horde Halter I: Slay 1,000 Zombie Summoners");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("SniperHunter1")) {
				if(%client.CheckNWChallengeCompletion("SniperHunter2")) {
					if(%client.CheckNWChallengeCompletion("SniperHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Scope Breaker III: Slay 5,000 Sniper Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Scope Breaker III: Slay 5,000 Sniper Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Scope Breaker II: Slay 2,500 Sniper Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Scope Breaker I: Slay 1,000 Sniper Zombies");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("UDemHunter1")) {
				if(%client.CheckNWChallengeCompletion("UDemHunter2")) {
					if(%client.CheckNWChallengeCompletion("UDemHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Runner Down III: Slay 5,000 Ultra Demon Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Runner Down III: Slay 5,000 Ultra Demon Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Runner Down II: Slay 2,500 Ultra Demon Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Runner Down I: Slay 1,000 Ultra Demon Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("VRavHunter1")) {
				if(%client.CheckNWChallengeCompletion("VRavHunter2")) {
					if(%client.CheckNWChallengeCompletion("VRavHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>C4 Coming Through III: Slay 5,000 Volatile Ravager Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "C4 Coming Through III: Slay 5,000 Volatile Ravager Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "C4 Coming Through II: Slay 2,500 Volatile Ravager Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "C4 Coming Through I: Slay 1,000 Volatile Ravager Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("SSHunter1")) {
				if(%client.CheckNWChallengeCompletion("SSHunter2")) {
					if(%client.CheckNWChallengeCompletion("SSHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>De-Flakerizer III: Slay 5,000 Slingshot Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "De-Flakerizer III: Slay 5,000 Slingshot Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "De-Flakerizer II: Slay 2,500 Slingshot Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "De-Flakerizer I: Slay 1,000 Slingshot Zombies");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("WraithHunter1")) {
				if(%client.CheckNWChallengeCompletion("WraithHunter2")) {
					if(%client.CheckNWChallengeCompletion("WraithHunter3")) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Anti Spec-Ops III: Slay 1,000 Wraith Zombies");
						%index++;
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Anti Spec-Ops III: Slay 1,000 Wraith Zombies");
						%index++;
					}
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Anti Spec-Ops II: Slay 750 Wraith Zombies");
					%index++;
				}
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Anti Spec-Ops I: Slay 500 Wraith Zombies");
				%index++;
			}
			//			
			return %index;
			
		case 5:
			//Special Events
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Special Event Challenges:");
			%index++;
			if(%client.CheckNWChallengeCompletion("NewYearsEve")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>New Years Eve Fireworks: Get a Javelin Kill on New Year's Eve.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "New Years Eve Fireworks: Get a Javelin Kill on New Year's Eve.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("NewYears")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>New Years Fireworks: Call in a Nuclear Strike on New Year's Day.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "New Years Fireworks: Call in a Nuclear Strike on New Year's Day.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("GunshipMall")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Gunship to The Mall: Call in a Gunship Killstreak on Christmas Mall 2009.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship to The Mall: Call in a Gunship Killstreak on Christmas Mall 2009.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("IndepRPG")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Independance RPG: Score an RPG Kill on the Fourth of July.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Independance RPG: Score an RPG Kill on the Fourth of July.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("SoulsticeBombard")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Soulstice Bombard: Call in artillery on one of the soulstices (6/21 or 12/21).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Soulstice Bombard: Call in artillery on one of the soulstices (6/21 or 12/21).");
				%index++;
			}		
			return %index;
		
		case 6:
			//PGD Daily Challenges
			//Handled by DChalg.cs
			%index = GenerateDWMChallengeMenu(%client, %tag, %index);
			return %index;
			
		case 7:
			//Sabotage
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Sabotage Game Mode Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("BombDisarmed")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Bomb Disarmed: Disarm an enemy bomb.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Disarmed: Disarm an enemy bomb.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("BombPlanted")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Bomb Planted: Arm the bomb at the objective.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Planted: Arm the bomb at the objective.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("BombDetonated")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Bomb Detonated: Win a Round Of Sabotage.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Bomb Detonated: Win a Round Of Sabotage.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("3For5Sabo")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Three For Five: Win 3 Rounds Of Sabotage in a match.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Three For Five: Win 3 Rounds Of Sabotage in a match.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("BaseDestroyer")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Base Destroyer: Go Undefeated in a full game of Sabotage.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Base Destroyer: Go Undefeated in a full game of Sabotage.");
				%index++;
			}
			//
			return %index;			
	
		case 8:
			//Domination
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Domination Game Mode Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("ZoneCapture")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Zone Conquerer: Capture an Area.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Zone Conquerer: Capture an Area.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("ABC")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Alpha Bravo Charlie: Secure All Three Areas at one Time.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Alpha Bravo Charlie: Secure All Three Areas at one Time.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("MatchSet")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Match Set: Win a Round Of Domination.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Match Set: Win a Round Of Domination.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("3For5")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Three For Five: Win 3 Rounds Of Domination.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Three For Five: Win 3 Rounds Of Domination.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Undefeatable")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Undefeatable: Go Undefeated in a full game of Domination.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Undefeatable: Go Undefeated in a full game of Domination.");
				%index++;
			}
			//
			return %index;			
			
		case 9:
			//Horde
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Horde 3 Game Mode Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("15For15")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>15 For 15: Complete Wave 15.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "15 For 15: Complete Wave 15.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Milestone25")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Milestone 25: Complete Wave 25.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Milestone 25: Complete Wave 25.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("ArmyOf50Stopped")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Army Of 50 Stopped: Complete Horde 3 (All 50 Waves).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Army Of 50 Stopped: Complete Horde 3 (All 50 Waves).");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Angel")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Angel: Revive a fallen teammate in Horde.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Angel: Revive a fallen teammate in Horde.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("ZBomber")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Z-Bomber: Call in a Z-Bomb While Playing Horde.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Z-Bomber: Call in a Z-Bomb While Playing Horde.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("FirstBlood")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>First Blood: Kill the first zombie that spawns in a Horde 3 game.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "First Blood: Kill the first zombie that spawns in a Horde 3 game.");
				%index++;
			}	
			//
			if(%client.CheckNWChallengeCompletion("SpeedSlayer")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Speed Slayer: Be the featured first killer 10 times in a single game.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Speed Slayer: Be the featured first killer 10 times in a single game.");
				%index++;
			}			
			//
			if(%client.CheckNWChallengeCompletion("HighScorer")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>High Scorer: Be the featured high scorer 10 times in a single game.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "High Scorer: Be the featured high scorer 10 times in a single game.");
				%index++;
			}			
			//			
			return %index;			
			
		case 10:
			//Helljump
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Helljump Game Mode Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("GroupBuster")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Group Buster: Complete A Group.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Group Buster: Complete A Group.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("WaveDefeater")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Wave Defeater: Complete A Wave.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Wave Defeater: Complete A Wave.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("OneK")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>1K Soldier: Earn 1,000 Points (Solo Score).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "1K Soldier: Earn 1,000 Points (Solo Score).");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("FiveK")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>5K Soldier: Earn 5,000 Points (Solo Score).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "5K Soldier: Earn 5,000 Points (Solo Score).");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("TenK")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>10K Soldier: Earn 10,000 Points (Solo Score).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "10K Soldier: Earn 10,000 Points (Solo Score).");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("PointsSurge")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Points Surge: Earn 7,500 Points (Team Score).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Points Surge: Earn 7,500 Points (Team Score).");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("PointsJackpot")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Points Jackpot: Earn 25,000 Points (Team Score).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Points Jackpot: Earn 25,000 Points (Team Score).");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("DownBoy")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Down Boy... Down: Kill the wraith zombie on Strike 5.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Down Boy... Down: Kill the wraith zombie on Strike 5.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("ClassExtravaganza")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Class Extravaganza: Use a hellclass.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Class Extravaganza: Use a hellclass.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("LifeGiver")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Giver of Life: Use a Full Team Respawn beacon.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Giver of Life: Use a Full Team Respawn beacon.");
				%index++;
			}
			//
			return %index;			
			
		case 11:
			//From The Top (Missions)
			messageClient( %client, 'SetLineHud', "", %tag, %index, "From The Top (Mission) Challenges:");
			%index++;			
			if(%client.CheckNWChallengeCompletion("SimonSays")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Simon Says: Order a mission.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Simon Says: Order a mission.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("FromTheTop")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>From The Top: Accept a mission.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "From The Top: Accept a mission.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("NaturalLeader")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Natural Leader: Order a mission and have another player join your team.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Natural Leader: Order a mission and have another player join your team.");
				%index++;
			}
			//			
			if(%client.CheckNWChallengeCompletion("GoldStar")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Gold Star: Complete a mission within the time limit.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Gold Star: Complete a mission within the time limit.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Faster")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Faster!: Complete a mission, but miss the time limit.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Faster!: Complete a mission, but miss the time limit.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("EpicFailure")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Epic Failure: Fail a mission ;).");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Epic Failure: Fail a mission ;).");
				%index++;
			}	
			//
			if(%client.CheckNWChallengeCompletion("ExpertGunner")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Expert AC-130 Gunner: Complete 'Rain Down'.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Expert AC-130 Gunner: Complete 'Rain Down'.");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("Survivalist")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Survivalist: Complete 'Surrounded'.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Survivalist: Complete 'Surrounded'.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Invisibreh")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Shhh.. I'm Invisibreh: Complete 'Enemy AC-130 Above' by outlasting the enemy AC-130.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Shhh.. I'm Invisibreh: Complete 'Enemy AC-130 Above' by outlasting the enemy AC-130.");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("WeakGunship")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>I have you now!: Complete 'Enemy AC-130 Above' by destroying the enemy AC-130.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "I have you now!: Complete 'Enemy AC-130 Above' by destroying the enemy AC-130.");
				%index++;
			}
			//	
			if(%client.CheckNWChallengeCompletion("InvasionBuster")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Invade THIS!: Complete 'Invasion'.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Invade THIS!: Complete 'Invasion'.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("SurvivalistExtreme")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Extreme Survivalist: Complete 'Surrounded 2.0'.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Extreme Survivalist: Complete 'Surrounded 2.0'.");
				%index++;
			}
			//			
			return %index;
			
		case 12:
			//Officer Promotion
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Officer Challenges:");
			%index++;					
			if(%client.CheckNWChallengeCompletion("Prestge1")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Instructive Private - Reach Officer Level 1.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Instructive Private - Reach Officer Level 1.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Prestge2")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Excelling Private - Reach Officer Level 2.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Excelling Private - Reach Officer Level 2.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Prestge3")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Champion Private - Reach Officer Level 3.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Champion Private - Reach Officer Level 3.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Prestge4")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Prestigious Private - Reach Officer Level 4.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Prestigious Private - Reach Officer Level 4.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Prestge5")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Supreme Private - Reach Officer Level 5.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Supreme Private - Reach Officer Level 5.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("Prestge9")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Phantom's Vengeance - Reach Oficer Level 9.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Phantom's Vengeance - Reach Oficer Level 9.");
				%index++;
			}
			//
			if(%client.CheckNWChallengeCompletion("GameEnder")) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Game Ender - Call in a Fission Bomb.");
				%index++;
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "Game Ender - Call in a Fission Bomb.");
				%index++;
			}
			//
			return %index;			
			
		default:
			//Invalid
			messageClient( %client, 'SetLineHud', "", %tag, %index, "Invalid menu option passed to GenerateChallengeSubMenu.");
			%index++;			
			return %index;
	}
}