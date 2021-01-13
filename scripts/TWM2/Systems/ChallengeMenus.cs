//Challenge Menus
//TWM2 3.9.1
//All of the challenge menu functions are now in this file to make locating them for adjustment
// easier to maintain. Also, as of 3.9.1, I have now provided a means to automate this menu's
// creation such that new challenges can be very easily added.

function GenerateChallengesMenu(%client, %tag, %index) {
	%scriptController = %client.TWM2Core;
	%xp = getCurrentEXP(%client);
	messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:16>Select a category to view challenges:");
	%index++;
	//
	for(%i = 2; $Challenge::Category[%i] !$= ""; %i++) {
		%categoryReq = getField($Challenge::Category[%i], 2);
		if(getWord(%categoryReq, 0) $= "Officer") {
			%offLevel = getWord(%categoryReq, 1);
			if(%scriptController.officer >= %offLevel) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><a:gamelink\tOtherTasksSub\t"@%i@">"@getField($Challenge::Category[%i], 0)@"</a>: "@getField($Challenge::Category[%i], 1));
				%index++;				
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@getField($Challenge::Category[%i], 0)@": Locked, Requires officer level "@%offLevel@" ("@strReplace($Prestige::Name[%offLevel], " ", "")@")");
				%index++;				
			}
		}
		else if(getWord(%categoryReq, 0) $= "MaxRank") {
			if(%scriptController.officer >= 15 && %scriptController.millionxp >= 3) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><a:gamelink\tOtherTasksSub\t"@%i@">"@getField($Challenge::Category[%i], 0)@"</a>: "@getField($Challenge::Category[%i], 1));
				%index++;				
			}
			else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><color:BB0000>=== CLASSIFIED: CONTINUE PLAYING TWM2 TO UNLOCK ===");
				%index++;				
			}			
		}
		else {	
			if(%categoryReq == -1) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><a:gamelink\tOtherTasksSub\t"@%i@">"@getField($Challenge::Category[%i], 0)@"</a>: "@getField($Challenge::Category[%i], 1));
				%index++;					
			}
			else {
				if(%xp >= $Rank::MinPoints[%categoryReq]) {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><a:gamelink\tOtherTasksSub\t"@%i@">"@getField($Challenge::Category[%i], 0)@"</a>: "@getField($Challenge::Category[%i], 1));
					%index++;					
				}
				else {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@getField($Challenge::Category[%i], 0)@": Locked, Requires Rank of "@$Ranks::NewRank[%categoryReq]@".");
					%index++;					
				}
			}			
		}
	}
	return %index;
}

function GenerateChallengeSubMenu(%client, %subMenu, %tag, %index) {
	if(%subMenu == 1) {
		return GenerateDWMChallengeMenu(%client, %tag, %index);
	}
	
	%scriptController = %client.TWM2Core;
	%xp = getCurrentEXP(%client);
	
	for(%i = 0; $Challenge::Challenge[%subMenu, %i] !$= ""; %i++) {
		%loopAndFinish = false;
		%challengeInternalName = $Challenge::Challenge[%subMenu, %i];
		%challengeDetails = $Challenge::Info[%challengeInternalName];
		%trailing = TWM2Lib_MainControl("getStrTrailingNumber", %challengeInternalName);
		if(%done[%challengeInternalName]) {
			continue;
		}
		if(%challengeDetails !$= "") {
			//Is this NOT a multi-tier challenge?
			if($Challenge::IsNotMultiTier[%challengeInternalName] || %trailing $= "" || %trailing == 0) {
				//Proceed to writing ;)
				if(%client.CheckNWChallengeCompletion(%challengeInternalName)) {
					messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><color:33FF00>"@getField(%challengeDetails, 0)@": "@getField(%challengeDetails, 4));
					%index++;
					continue;
				}
				else {
					//Does this challenge require another challenge be completed?
					if($Challenge::RequiresChallenge[%subMenu, %i] !$= "") {
						%requiredChallengeName = $Challenge::RequiresChallenge[%subMenu, %i];
						%requiredChallengeDetails = $Challenge::Info[%requiredChallengeName];
						if(!%client.CheckNWChallengeCompletion(%requiredChallengeName)) {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked: Requires Completion of "@getField(%requiredChallengeDetails, 0));
							%index++;
							continue;
						}
					}					
					//Does this challenge have an embedded requirement?
					if($Challenge::SetRequirement[%subMenu, %i] !$= "") {
						%cReq = $Challenge::SetRequirement[%subMenu, %i];
						if(getWord(%cReq, 0) $= "Officer") {
							%offLevel = getWord(%cReq, 1);
							if(%scriptController.officer < %offLevel) {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked, Requires officer level "@%offLevel@" ("@strReplace($Prestige::Name[%offLevel], " ", "")@")");
								%index++;	
								continue;
							}
						}
						else {
							%expReq = $Rank::MinPoints[%cReq];
							if(%xp < $expReq) {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked, Requires "@$Ranks::NewRank[%cReq]@" rank");
								%index++;
								continue;								
							}
						}
					}
					//Is this a "hidden" challenge?
					if($Challenge::SetHidden[%subMenu, %i]) {
						if($Challenge::HiddenMessage[%subMenu, %i] !$= "") {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@$Challenge::HiddenMessage[%subMenu, %i]);
							%index++;
							continue;
						}
						else {
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>This is a hidden challenge, unlock it to learn more.");
							%index++;
							continue;
						}
					}
					//All tests passed, write normally.
					messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@getField(%challengeDetails, 0)@": "@getField(%challengeDetails, 4));
					%index++;					
				}
			}
			else {
				//Scan for additional challenges using the same internal name..
				%chStr = getSubStr(%challengeInternalName, 0, strLen(%challengeInternalName) - strLen(%trailing));
				//Loop forward until the system draws a blank
				%fCount = 0;
				%j = %i;
				while(true) {
					%nextC = $Challenge::Challenge[%subMenu, %j];
					%nextT = TWM2Lib_MainControl("getStrTrailingNumber", %nextC);
					%nextChStr = getSubStr(%nextC, 0, strLen(%nextC) - strLen(%nextT));
					if(%nextChStr $= %chStr && %nextT == %fCount+1) {
						%fCount++;
						%j++;
					}
					else {
						break;
					}
				}
				//This isn't a multi-tier, flag it internally so we can skip this code later, and push a message to the console to let the host know.
				if(%fCount == 1) {
					error("NOTE: Challenge menu generator has flagged a non multi-tier challenge caught inside the multi-tier loop, consider removing "@%challengeInternalName@" by adding the $Challenge::IsNotMultiTier flag to this challenge.");
					if(%client.CheckNWChallengeCompletion(%challengeInternalName)) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><color:33FF00>"@getField(%challengeDetails, 0)@": "@getField(%challengeDetails, 4));
						%index++;
						continue;
					}
					else {
						//Does this challenge require another challenge be completed?
						if($Challenge::RequiresChallenge[%subMenu, %i] !$= "") {
							%requiredChallengeName = $Challenge::RequiresChallenge[%subMenu, %i];
							%requiredChallengeDetails = $Challenge::Info[%requiredChallengeName];
							if(!%client.CheckNWChallengeCompletion(%requiredChallengeName)) {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked: Requires Completion of "@getField(%requiredChallengeDetails, 0));
								%index++;
								continue;
							}
						}					
						//Does this challenge have an embedded requirement?
						if($Challenge::SetRequirement[%subMenu, %i] !$= "") {
							%cReq = $Challenge::SetRequirement[%subMenu, %i];
							if(getWord(%cReq, 0) $= "Officer") {
								%offLevel = getWord(%cReq, 1);
								if(%scriptController.officer < %offLevel) {
									messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked, Requires officer level "@%offLevel@" ("@strReplace($Prestige::Name[%offLevel], " ", "")@")");
									%index++;
									continue;
								}
							}
							else {
								%expReq = $Rank::MinPoints[%cReq];
								if(%xp < $expReq) {
									messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked, Requires "@$Ranks::NewRank[%cReq]@" rank");
									%index++;
									continue;
								}
							}
						}
						//Is this a "hidden" challenge?
						if($Challenge::SetHidden[%subMenu, %i]) {
							if($Challenge::HiddenMessage[%subMenu, %i] !$= "") {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@$Challenge::HiddenMessage[%subMenu, %i]);
								%index++;	
								continue;
							}
							else {
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>This is a hidden challenge, unlock it to learn more.");
								%index++;
								continue;
							}
						}					
						//All tests passed, write normally.
						messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@getField(%challengeDetails, 0)@": "@getField(%challengeDetails, 4));
						%index++;					
					}
				}
				else {
					//Alright, we've got a multi-tier challenge. Now, let's process...
					//Step 1: Identify the active challenge...
					%j = %i;
					%fCount = 1;
					while(true) {
						%currentC = $Challenge::Challenge[%subMenu, %j];
						%currentT = TWM2Lib_MainControl("getStrTrailingNumber", %currentC);
						%currentChStr = getSubStr(%currentC, 0, strLen(%currentC) - strLen(%currentT));
						%cDetails = $Challenge::Info[%currentC];
						if(%loopAndFinish) {
							if(%currentChStr $= %chStr && %currentT == %fCount) {
								%done[%currentC] = true;							
								%fCount++;
								%j++;
								continue;
							}
							else {
								break;
							}						
						}
						//Is this challenge complete? If so, check to see if the next challenge is still contained in our "sequence"
						if(%client.CheckNWChallengeCompletion(%currentC)) {
							%done[%currentC] = true;
							//Yes, it's complete, move to the next.
							%nextC = $Challenge::Challenge[%subMenu, %j+1];
							%nextT = TWM2Lib_MainControl("getStrTrailingNumber", %nextC);
							%nextChStr = getSubStr(%nextC, 0, strLen(%nextC) - strLen(%nextT));							
							if(%nextChStr $= %currentChStr && %nextT == %fCount+1) {
								%fCount++;
								%j++;
								continue;
							}
							else {
								%cDetails = $Challenge::Info[%currentC];
								//This is the last challenge in the sequence, and it's done... write.
								messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><color:33FF00>"@getField(%cDetails, 0)@": "@getField(%cDetails, 4));
								%index++;								
								break;
							}
						}
						else {
							//Not complete, this is the active challenge. Proceed to write, then flag the others in the chain as "done"
							//Does this challenge require another challenge be completed?
							if($Challenge::RequiresChallenge[%subMenu, %j] !$= "") {
								%requiredChallengeName = $Challenge::RequiresChallenge[%subMenu, %j];
								%requiredChallengeDetails = $Challenge::Info[%requiredChallengeName];
								if(!%client.CheckNWChallengeCompletion(%requiredChallengeName)) {
									messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked: Requires Completion of "@getField(%requiredChallengeDetails, 0));
									%index++;
									%loopAndFinish = true;
									continue;
								}
							}					
							//Does this challenge have an embedded requirement?
							if($Challenge::SetRequirement[%subMenu, %j] !$= "") {
								%cReq = $Challenge::SetRequirement[%subMenu, %j];
								if(getWord(%cReq, 0) $= "Officer") {
									%offLevel = getWord(%cReq, 1);
									if(%scriptController.officer < %offLevel) {
										messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked, Requires officer level "@%offLevel@" ("@strReplace($Prestige::Name[%offLevel], " ", "")@")");
										%index++;
										%loopAndFinish = true;
										continue;
									}
								}
								else {
									%expReq = $Rank::MinPoints[%cReq];
									if(%xp < $expReq) {
										messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>Challenge Locked, Requires "@$Ranks::NewRank[%cReq]@" rank");
										%index++;
										%loopAndFinish = true;
										continue;
									}
								}
							}
							//Is this a "hidden" challenge?
							if($Challenge::SetHidden[%subMenu, %j]) {
								if($Challenge::HiddenMessage[%subMenu, %j] !$= "") {
									messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@$Challenge::HiddenMessage[%subMenu, %i]);
									%index++;
									%loopAndFinish = true;
									continue;
								}
								else {
									messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>This is a hidden challenge, unlock it to learn more.");
									%index++;
									%loopAndFinish = true;
									continue;
								}
							}					
							//All tests passed, write normally.
							messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14>"@getField(%cDetails, 0)@": "@getField(%cDetails, 4));
							%index++;	
							//Flag the rest of the chain as done.
							%loopAndFinish = true;
						}
					}
				}
			}
		}
		else {
			error("GenerateChallengeSubMenu(): Something went wrong.. Loop found challenge "@%challengeInternalName@", but this is not defined in the NWChallengeIndex.");
		}
	}
	return %index;			

}