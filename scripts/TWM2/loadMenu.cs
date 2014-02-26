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
      %tip = "Watch the sniper trails, the R700 Leaves a trail, so you can easily find the shooter.";
      case 2:
      %tip = "Check your ammo! People with limited ammo on their primary weapons are more likely to be killed";
      case 3:
      %tip = "Use /help to view commands. /Checkstats to view your Stats, and the [F2] Menu to check other info";
      case 4:
      %tip = "To level up, Kill enemies and zombies. Killing teammates holds a harsh penalty! Use /checkstats to see your info";
      case 5:
      %tip = "The easiest way to deal with a zombie lord is to deliver a head shot with a sniper rifle.";
      case 6:
      %tip = "To check what weapons you can use. Open up your [F2] Menu and select weapons information";
      case 7:
      %tip = "Beware Demon Lord Zombies! They are quick, decisive, and are armed with many abilities";
      case 8:
      %tip = "Prioritize your tasks! Focus on the greatest threats to you first";
      case 9:
      %tip = "Watch your flanks in sabotage, if you have the bomb, you are visible to everyone, including the enemy";
      case 10:
      %tip = "What weapons will work best for you? check out the Weapons information in your [F2] Menu";
      case 11:
      %tip = "Enemies coming in a group? use the C4 Mines to blow them all up";
      case 12:
      %tip = "Challenges are good! Complete them to unlock vehicles, weapon attachments, and more!";
      case 13:
      %tip = "Ultra Drones, Killers of the SKY. To kill them, stay out of the sky, hit them with SAM's";
      case 14:
      %tip = "Bothered by enemy vehicles? Grab the Javalin, and blast those suckers";
      case 15:
      %tip = "Infected? use your health kit, it comes preloaded with an infection cure";
      case 16:
      %tip = "XP Points are earned differently by killing players and zombies, stronger enemies give more XP.";
      case 17:
      %tip = "When do I get a certian weapon? check the [F2] Menu to find that out";
      case 18:
      %tip = "Killstealing is bad, and should never be done, protection is enabled in horde/helljump to block those theives!";
      case 19:
      %tip = "Perks and Killstreaks can be set in the [F2] Menu by clicking Settings and then the respective category";
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

    %Credits = "\n<Font:Arial:16>Version "@$TWM2::Version@"" @
               "\n<Font:Arial:14>TWM 2 Developer: Phantom139"@
               "\n<Font:Arial:14>TWM 2 Co-Devs: Dark Dragon DX, DarknessOfLight, Signal360";

    // this callback adds content to the bulk of the gui
    messageClient(%client, 'MsgDebriefAddLine', "", %Credits);
    
    %Site = "\n<Font:Arial:16>Site: Http://www.phantomdev.net\n";

    // this callback adds content to the bulk of the gui
    messageClient(%client, 'MsgDebriefAddLine', "", %Site);
    
    %Thanks = "\n<Font:Arial:14>Thanks: Thyth, -Linker-, Cons Mod Devs"@
               "\n";
    messageClient(%client, 'MsgDebriefAddLine', "", %Thanks);
    
    %MoreCredits = "\n<Font:Arial:16>CCM Developer: Dondelium_X" @
               "\n<Font:Arial:14>CCM Co-Devs: FalconBlade, Ur_A_Dum"@
               "\n";

    messageClient(%client, 'MsgDebriefAddLine', "", %MoreCredits);

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
    
    %MOTDMsg = "\n<Font:Arial:14>Message Of The Day:" @
               "\n<Font:Arial:12>"@$Server::MOTD@"."@
               "\n\n\n";
    messageClient(%client, 'MsgDebriefAddLine', "", %MOTDMsg);
    
    %PGDMsg = "\n<Font:Arial:14>Join the Phantom Games Development community for up to the minute news on TWM2 and our other projects! " @
    "\n http://www.public.phantomdev.net";
    messageClient(%client, 'MsgDebriefAddLine', "", %PGDMsg);

   }
};

activatepackage(loadmodinfo);
