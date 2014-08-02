// WeaponModes.cs
// Phantom139
// TWM2 3.9
// Declares weapon mode print tags for weapons (Where necessary)

//This value defines how often a player may switch modes. (In Miliseconds)
$WeaponModes::SwitchDelay = 75;

function displayWeaponInfo(%this, %obj, %pMode, %sMode, %replaceTags) {
   %imageName = %this.getName();
   %printMsg = "";
   
   if($WeaponModesTag[%imageName] !$= "") {
      %printMsg = $WeaponModesTag[%imageName];
      %printMsg = strReplace(%printMsg, "[XMSG]", $WeaponModes[%imageName, %pMode, %sMode]);
   }
   else {
      %printMsg = $WeaponModes[%imageName, %pMode, %sMode];
   }
   //Replace tags is a special field used to replace instances with other instances.
   // Essentially, when using this param, you want the first word to be [TAG] and then everything up to the \t to be what you want.
   for(%i = 0; %i < getFieldCount(%replaceTags); %i++) {
      %replaceA = getWord(getField(%replaceTags, %i), 0);
      %replaceB = getWords(getField(%replaceTags, %i), 1, getWordCount(getField(%replaceTags, %i)));
      %printMsg = strReplace(%printMsg, %replaceA, %replaceB);
   }
   
   CommandToClient(%obj.client, 'BottomPrint', %printMsg, 3, 3);
}

//This is where you can define the print messages for your weapons. There are two options for coding these tags.
// 1. Use $WeaponModesTag[Image]: to declare a standard text for all prints. The only difference is in the [X] tag. If you don't define this, the system uses #2 by default.
// 2. Use $WeaponModes[Image, Pri, Sec]: To declare individual modes, if $WeaponModesTag is defined, this replaces [X], otherwise this is the text used.

//Construction Tool
$WeaponModesTag[ConstructionToolImage] = "<font:Sui Generis:14>>>>Construction Tool<<<\n<font:Arial:14>[XMSG]\nLuCiD, Mostlikely, JackTL.";
$WeaponModes[ConstructionToolImage, 0, 0] = "Mode: Deconstruction | Sub Mode: Normal Deconstruction";
$WeaponModes[ConstructionToolImage, 0, 1] = "Mode: Deconstruction | Sub Mode: Cascading Deconstruction";
$WeaponModes[ConstructionToolImage, 1, 0] = "Mode: Rotate | Sub Mode: Push [REPA] Degrees";
$WeaponModes[ConstructionToolImage, 1, 1] = "Mode: Rotate | Sub Mode: Pull [REPA] Degrees";
$WeaponModes[ConstructionToolImage, 2, 0] = "Mode: Advanced Rotation | Sub Mode: Select Center Of Rotation";
$WeaponModes[ConstructionToolImage, 2, 1] = "Mode: Advanced Rotation | Sub Mode: Add Object To Rotation List";
$WeaponModes[ConstructionToolImage, 2, 2] = "Mode: Advanced Rotation | Sub Mode: Select Rotation Speed";
$WeaponModes[ConstructionToolImage, 2, 3] = "Mode: Advanced Rotation | Sub Mode: Apply Rotation";
$WeaponModes[ConstructionToolImage, 2, 4] = "Mode: Advanced Rotation | Sub Mode: Flash Selected Items";
$WeaponModes[ConstructionToolImage, 2, 5] = "Mode: Advanced Rotation | Sub Mode: Clear Rotation List";
$WeaponModes[ConstructionToolImage, 3, 0] = "Mode: Power Management | Sub Mode: Toggle Power Generation Object";
$WeaponModes[ConstructionToolImage, 3, 1] = "Mode: Power Management | Sub Mode: Increase Current Power Frequency";
$WeaponModes[ConstructionToolImage, 3, 2] = "Mode: Power Management | Sub Mode: Decrease Current Power Frequency";
$WeaponModes[ConstructionToolImage, 3, 3] = "Mode: Power Management | Sub Mode: Read Object Power Information";

//Manipulator Gun
$WeaponModesTag[EditGunImage] = "<font:Sui Generis:14>>>>Manipulator<<< - Phantom139\n<font:Arial:14>[XMSG]";
$WeaponModes[EditGunImage, 0, 0] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Blue Pad - [LSB] - MSB";
$WeaponModes[EditGunImage, 0, 1] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: LSB - [MSB] - Walkway";
$WeaponModes[EditGunImage, 0, 2] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: MSB - [Walkway] - Medium Floor";
$WeaponModes[EditGunImage, 0, 3] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Walkway - [Medium Floor] - Dark Pad";
$WeaponModes[EditGunImage, 0, 4] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Medium Floor - [Dark Pad] - V-Pad";
$WeaponModes[EditGunImage, 0, 5] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Dark Pad - [V-Pad] - C.1 Backpack";
$WeaponModes[EditGunImage, 0, 6] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: V-Pad - [C.1 Backpack] - C.2 Small Containment";
$WeaponModes[EditGunImage, 0, 7] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.1 Backpack - [C.2 Small Containment] - C.3 Large Containment";
$WeaponModes[EditGunImage, 0, 8] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.2 Small Containment - [C.3 Large Containment] - C.4 Compressor";
$WeaponModes[EditGunImage, 0, 9] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.3 Large Containment - [C.4 Compressor] - C.5 Tubes";
$WeaponModes[EditGunImage, 0, 10] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.4 Compressor - [C.5 Tubes] - C.6 Quantium Battery";
$WeaponModes[EditGunImage, 0, 11] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.5 Tubes - [C.6 Quantium Battery] - C.7 Proton Acc.";
$WeaponModes[EditGunImage, 0, 12] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.6 Quantium Battery - [C.7 Proton Acc.] - C.8 Cargo Crate";
$WeaponModes[EditGunImage, 0, 13] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.7 Proton Acc. - [C.8 Cargo Crate] - C.9 Mag Cooler";
$WeaponModes[EditGunImage, 0, 14] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.8 Cargo Crate - [C.9 Mag Cooler] - C.10 Recycle Unit";
$WeaponModes[EditGunImage, 0, 15] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.9 Mag Cooler - [C.10 Recycle Unit] - C.11 Fuel Canister";
$WeaponModes[EditGunImage, 0, 16] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.10 Recycle Unit - [C.11 Fuel Canister] - C.12 Wooden Box";
$WeaponModes[EditGunImage, 0, 17] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.11 Fuel Canister - [C.12 Wooden Box] - C.13 Plasma Router";
$WeaponModes[EditGunImage, 0, 18] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.12 Wooden Box - [C.13 Plasma Router] - Statue Base";
$WeaponModes[EditGunImage, 0, 19] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.13 Plasma Router - [Statue Base] - Blue Pad";
$WeaponModes[EditGunImage, 0, 20] = " Mine: <Pad Swap> - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Statue Base - [Blue Pad] - LSB";
$WeaponModes[EditGunImage, 1, 0] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Yellow - [Solid White] - Solid Red";
$WeaponModes[EditGunImage, 1, 1] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid White - [Solid Red] - Solid Green";
$WeaponModes[EditGunImage, 1, 2] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Red - [Solid Green] - Solid Blue";
$WeaponModes[EditGunImage, 1, 3] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Green - [Solid Blue] - Solid Cyan";
$WeaponModes[EditGunImage, 1, 4] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Blue - [Solid Cyan] - Solid Magenta";
$WeaponModes[EditGunImage, 1, 5] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Cyan - [Solid Magenta] - Solid Yellow";
$WeaponModes[EditGunImage, 1, 6] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Magenta - [Solid Yellow] - Team Pass White";
$WeaponModes[EditGunImage, 1, 7] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Yellow - [Team Pass White] - Team Pass Red";
$WeaponModes[EditGunImage, 1, 8] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass White - [Team Pass Red] - Team Pass Green";
$WeaponModes[EditGunImage, 1, 9] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Red - [Team Pass Green] - Team Pass Blue";
$WeaponModes[EditGunImage, 1, 10] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Green - [Team Pass Blue] - Team Pass Cyan";
$WeaponModes[EditGunImage, 1, 11] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Blue - [Team Pass Cyan] - Team Pass Magenta";
$WeaponModes[EditGunImage, 1, 12] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Cyan - [Team Pass Magenta] - Team Pass Yellow";
$WeaponModes[EditGunImage, 1, 13] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Magenta - [Team Pass Yellow] - All Pass White";
$WeaponModes[EditGunImage, 1, 14] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Yellow - [All Pass White] - All Pass Red";
$WeaponModes[EditGunImage, 1, 15] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass White - [All Pass Red] - All Pass Green";
$WeaponModes[EditGunImage, 1, 16] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Red - [All Pass Green] - All Pass Blue";
$WeaponModes[EditGunImage, 1, 17] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Green - [All Pass Blue] - All Pass Cyan";
$WeaponModes[EditGunImage, 1, 18] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Blue - [All Pass Cyan] - All Pass Magenta";
$WeaponModes[EditGunImage, 1, 19] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Cyan - [All Pass Magenta] - All Pass Yellow";
$WeaponModes[EditGunImage, 1, 20] = " Mine: Pad Swap - <FF Swap> - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Magenta - [All Pass Yellow] - Solid White";
$WeaponModes[EditGunImage, 2, 0] = " Mine: Pad Swap - FF Swap - <Barrel Swap> - Cloak/Fade - Delete Objects \n  Grenade: Mortar - [Anti Air] - Missile";
$WeaponModes[EditGunImage, 2, 1] = " Mine: Pad Swap - FF Swap - <Barrel Swap> - Cloak/Fade - Delete Objects \n  Grenade: Anti Air - [Missile] - Plasma";
$WeaponModes[EditGunImage, 2, 2] = " Mine: Pad Swap - FF Swap - <Barrel Swap> - Cloak/Fade - Delete Objects \n  Grenade: Missile - [Plasma] - ELF";
$WeaponModes[EditGunImage, 2, 3] = " Mine: Pad Swap - FF Swap - <Barrel Swap> - Cloak/Fade - Delete Objects \n  Grenade: Plasma - [ELF] - Mortar";
$WeaponModes[EditGunImage, 2, 4] = " Mine: Pad Swap - FF Swap - <Barrel Swap> - Cloak/Fade - Delete Objects \n  Grenade: ELF - [Mortar] - Anti Air";
$WeaponModes[EditGunImage, 3, 0] = " Mine: Pad Swap - FF Swap - Barrel Swap - <Cloak/Fade> - Delete Objects \n  Grenade: [Cloak] - UnCloak - Fade - UnFade";
$WeaponModes[EditGunImage, 3, 1] = " Mine: Pad Swap - FF Swap - Barrel Swap - <Cloak/Fade> - Delete Objects \n  Grenade: Cloak - [UnCloak] - Fade - UnFade";
$WeaponModes[EditGunImage, 3, 2] = " Mine: Pad Swap - FF Swap - Barrel Swap - <Cloak/Fade> - Delete Objects \n  Grenade: Cloak - UnCloak - [Fade] - UnFade";
$WeaponModes[EditGunImage, 3, 3] = " Mine: Pad Swap - FF Swap - Barrel Swap - <Cloak/Fade> - Delete Objects \n  Grenade: Cloak - UnCloak - Fade - [UnFade]";
$WeaponModes[EditGunImage, 4, 0] = " Mine: Pad Swap - FF Swap - Barrel Swap - Cloak/Fade - <Delete Objects> \n  Grenade: [Single] - Cascade";
$WeaponModes[EditGunImage, 4, 1] = " Mine: Pad Swap - FF Swap - Barrel Swap - Cloak/Fade - <Delete Objects> \n  Grenade: Single - [Cascade]";

//Merge Tool
$WeaponModesTag[MergeToolImage] = "<font:Sui Generis:14>>>>M/I/S Tool<<<\n<font:Arial:14>[XMSG]\nCoded by Electricutioner.";
$WeaponModes[MergeToolImage, 0, 0] = "Mode: Merge. Fire the tool at two pieces. If possible, they will merge. Tolerance: " @ $ElecMod::MergeTool::Tolerance @ " meters.";
$WeaponModes[MergeToolImage, 0, 1] = "Mode: Merge. Fire the tool at two pieces. If possible, they will merge. Tolerance: " @ $ElecMod::MergeTool::HighTolerance @ " meters.";
$WeaponModes[MergeToolImage, 1, 0] = "Mode: Isometric. Fire the tool at a piece to isometrically rotate them. Rotate: Default.";
$WeaponModes[MergeToolImage, 1, 1] = "Mode: Isometric. Fire the tool at a piece to isometrically rotate them. Rotate: Z-Axis.";
$WeaponModes[MergeToolImage, 2, 0] = "Mode: Split. Fire at a piece to split it in half. Axis: Automatic.";
$WeaponModes[MergeToolImage, 2, 1] = "Mode: Split. Fire at a piece to split it on crosshair. Axis: Automatic.";
$WeaponModes[MergeToolImage, 2, 2] = "Mode: Split. Fire at a piece to split it in half. Axis: X.";
$WeaponModes[MergeToolImage, 2, 3] = "Mode: Split. Fire at a piece to split it in half. Axis: Y.";
$WeaponModes[MergeToolImage, 2, 4] = "Mode: Split. Fire at a piece to split it in half. Axis: Z.";
$WeaponModes[MergeToolImage, 2, 5] = "Mode: Split. Fire at a piece to split it on crosshair. Axis: X.";
$WeaponModes[MergeToolImage, 2, 6] = "Mode: Split. Fire at a piece to split it on crosshair. Axis: Y.";
$WeaponModes[MergeToolImage, 2, 7] = "Mode: Split. Fire at a piece to split it on crosshair. Axis: Z.";
$WeaponModes[MergeToolImage, 3, 0] = "Mode: Nudge. Fire at a piece to move it. Nudge [REPA]M +X Axis.";
$WeaponModes[MergeToolImage, 3, 1] = "Mode: Nudge. Fire at a piece to move it. Nudge [REPA]M +Y Axis.";
$WeaponModes[MergeToolImage, 3, 2] = "Mode: Nudge. Fire at a piece to move it. Nudge [REPA]M +Z Axis.";
$WeaponModes[MergeToolImage, 3, 3] = "Mode: Nudge. Fire at a piece to move it. Nudge [REPA]M -X Axis.";
$WeaponModes[MergeToolImage, 3, 4] = "Mode: Nudge. Fire at a piece to move it. Nudge [REPA]M -Y Axis.";
$WeaponModes[MergeToolImage, 3, 5] = "Mode: Nudge. Fire at a piece to move it. Nudge [REPA]M -Z Axis.";
$WeaponModes[MergeToolImage, 4, 0] = "Mode: Full Scale. Fire at a piece to scale it. Grow .01M.";
$WeaponModes[MergeToolImage, 4, 1] = "Mode: Full Scale. Fire at a piece to scale it. Shrink .01M.";

//Super Chain Gun
$WeaponModesTag[SuperChaingunImage] = "<font:Sui Generis:14>>>>Super Chaingun<<<\n<font:Arial:14>[XMSG]\nConstruct, Mostlikely, JackTL.";
$WeaponModes[SuperChaingunImage, 0, 0] = "Rapid Fire O.P. Insta-Kill Bullets";
$WeaponModes[SuperChaingunImage, 1, 0] = "Ion Shots: Ion Progression Disabled";
$WeaponModes[SuperChaingunImage, 1, 1] = "Ion Shots: Ion Progression Enabled";
$WeaponModes[SuperChaingunImage, 2, 0] = "Repair Pulse";
$WeaponModes[SuperChaingunImage, 3, 0] = "Cloak Pulse";
$WeaponModes[SuperChaingunImage, 4, 0] = "Deconstruction Pulse";
$WeaponModes[SuperChaingunImage, 5, 0] = "EMP Pulse";
$WeaponModes[SuperChaingunImage, 6, 0] = "Morphing Pulse";
