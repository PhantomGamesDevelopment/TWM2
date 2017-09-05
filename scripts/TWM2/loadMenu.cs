//-------------------------------------------
// Mod information load screen - ZOD. z0ddm0d
// and Modified later by the T2CC
//-------------------------------------------

package loadmodinfo
{

   function GetTipMessage() {
       %r = getRandom(1,19);
       switch(%r) {
          case 1:
            %tip = "High powered sniper rifles, such as the R700 leave a trail. This can help you to identify hostile snipers";
          case 2:
            %tip = "Check your ammo! People with limited ammo on their weapons are more likely to be killed";
          case 3:
            %tip = "Use /help to view commands. /Checkstats to view your Stats, and the [F2] Menu to check other info";
          case 4:
            %tip = "Want to unlock new gear? Level up by defeating enemy combatants, or securing combat medals through tough tasks";
          case 5:
            %tip = "Got zombie problems? Aim high! One shot to the head with a strong enough weapon will dispatch of most enemy combatants";
          case 6:
            %tip = "Looking to earn a lot of XP points? Invite some of your friends and take on the bosses of TWM2 for large EXP sums";
          case 7:
            %tip = "Be sure to frequently access the [F2] menu, additional player setting options such as perks and killstreaks can be found inside";
          case 8:
            %tip = "Prioritize your tasks! Focus on the greatest threats to you first";
          case 9:
            %tip = "Watch your flanks in sabotage, if you have the bomb, you are visible to everyone, including the enemy";
          case 10:
            %tip = "What weapons will work best for you? check out the weapons information tab in your [F2] Menu";
          case 11:
            %tip = "Large groups taking you on? Try using advanced equipment such as Satchel Charges, Mines, and C4 to dwindle enemy numbers";
          case 12:
            %tip = "Try to tackle individual challenges at a time, challenges sometimes have unique awards such as weapons, armors, and even vehicles!";
          case 13:
            %tip = "Ultra Drones are expert airhunters and should not be triffled with, if one is giving you problems, try using SAM weapons";
          case 14:
            %tip = "Got vehicle problems? Try using a weapon like the Stinger or Javelin to pound it with heavy explosive damage";
          case 15:
            %tip = "Zombie got you infected? use your health kit or the medic pack, both of which have a built in infection cure";
          case 16:
            %tip = "XP Points are earned differently by killing players and zombies, stronger enemies give more XP.";
          case 17:
            %tip = "Want to know when you're going to unlock that next piece of sweet gear? Check out the weapon information page in the [F2] menu";
          case 18:
            %tip = "Killstealing is bad, and should never be done, protection is enabled in horde/helljump to block those theives!";
          case 19:
            %tip = "Air rapiers sometimes spell doom once grabbed, but if you aim directly up with a strong enough weapon, you can escape their lethal grasp!";
          }
          return %tip;
   }

   function sendLoadInfoToClient( %client ) {
	Parent::sendLoadInfoToClient(%client);
	schedule(1000, 0, "sendLoadscreen", %client);
   }

   function sendLoadscreen(%client){
	messageClient( %client, 'MsgGameOver', "");
    messageClient( %client, 'MsgClearDebrief', "" );

    if($ServerType $= "") {
       %STO = "Checking PGD...";
    }
    else {
       %STO = $ServerType;
    }

    messageClient(%client, 'MsgDebriefResult', "", "<Font:Arial Bold:18><Just:CENTER>"@$Host::GameName);
    messageClient(%client, 'MsgDebriefResult', "", "<Font:Arial Bold:14><Just:CENTER>Total Warfare Mod 2 : Advanced Warfare");
    messageClient(%client, 'MsgDebriefResult', "", "<Font:Arial Bold:14><Just:CENTER>Server Type: "@%STO@"");

    %Credits = "\n<Font:Arial:16>Version v"@$TWM2::ModVersionString@"" @
               "\n<Font:Arial:14>TWM 2 Creator (Lead Developer): Phantom139"@
               "\n<Font:Arial:14>TWM 2 Co-Devs: Dark Dragon DX, DarknessOfLight, and Signal360"@
               "\n<Font:Arial:14>CCM Developers: Dondelium_X, FalconBlade, and Ur_A_Dum";

    // this callback adds content to the bulk of the gui
    messageClient(%client, 'MsgDebriefAddLine', "", %Credits);
    
    %Site = "\n<Font:Arial:16>Site: Http://www.phantomdev.net\n";

    // this callback adds content to the bulk of the gui
    messageClient(%client, 'MsgDebriefAddLine', "", %Site);
    
    %Thanks = "\n<Font:Arial:14>Additional Thanks: Thyth, -Linker-, Construction Mod Developers"@
               "\n";
    messageClient(%client, 'MsgDebriefAddLine', "", %Thanks);

    if($Rank::Top[1] $= "") {
       %TopRanks = "\n<Font:Arial:14>Top 5 Ranks:" @
               "\nRanks Not Yet Discovered, run FindTopRanks(); in console";

       messageClient(%client, 'MsgDebriefAddLine', "", %TopRanks);
    }
    else {
       %TopRanks = "\n<Font:Arial:14>Top 5 Ranks" @
               "\n<Font:Arial:12>First: "@$Rank::Top[1]@", "@$Rank::TopRank[1]@" ,"@$Rank::TopXP[1]@"XP"@
               "\nSecond: "@$Rank::Top[2]@", "@$Rank::TopRank[2]@" ,"@$Rank::TopXP[2]@"XP"@
               "\nThird: "@$Rank::Top[3]@", "@$Rank::TopRank[3]@" ,"@$Rank::TopXP[3]@"XP";

       messageClient(%client, 'MsgDebriefAddLine', "", %TopRanks);
       //It cuts off sometimes, so this prevents that from happening
       %TopRanks2 = "<Font:Arial:12>Fourth: "@$Rank::Top[4]@", "@$Rank::TopRank[4]@" ,"@$Rank::TopXP[4]@"XP"@
                 "\nFifth: "@$Rank::Top[5]@", "@$Rank::TopRank[5]@" ,"@$Rank::TopXP[5]@"XP"@
                 "\n";

       messageClient(%client, 'MsgDebriefAddLine', "", %TopRanks2);
    }
    %tip = GetTipMessage();
    %tipMsg = "\n<Font:Arial:14>Heres a tip for you:" @
               "\n<Font:Arial:12>"@%tip@"."@
               "\n";
    messageClient(%client, 'MsgDebriefAddLine', "", %tipMsg);
    
    %MOTDMsg = "\n<Font:Arial:14>Server Message Of The Day:" @
               "\n<Font:Arial:12>"@$Server::MOTD@"."@
               "\n\n\n";
    messageClient(%client, 'MsgDebriefAddLine', "", %MOTDMsg);
    
    %PGDMsg = "\n<Font:Arial:14>Join the Phantom Games Development community for up to the minute news on TWM2 and our other projects! " @
    "\n http://www.forums.phantomdev.net";
    messageClient(%client, 'MsgDebriefAddLine', "", %PGDMsg);

    %gettingStarted = "\n<Font:Arial:14>First time playing TWM2? Use the /help command for a list of chat commands and access the " @
    "\n Command menu with your [F2] key to get started!";
    messageClient(%client, 'MsgDebriefAddLine', "", %gettingStarted);

   }
};

activatepackage(loadmodinfo);
