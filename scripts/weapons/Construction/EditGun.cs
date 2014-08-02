//Editing Tool(s) By Phantom139
//This Tool Contains Many Useful Modes For Players To Quickly Modify Objects

//Weapon Modes:

// - Texture Swapping
// - FF Swappings
// - Turret Barrel Swapping
// - Object Cloak / Fade
// - Object Deletion

//Texture Swapping Vars
$EditTool::PadModeCount = 20;
$EditTool::PadModes[0] = "DeployedSpine";
$EditTool::PadModes[1] = "DeployedMSpine";
$EditTool::PadModes[2] = "DeployedwWall";
$EditTool::PadModes[3] = "DeployedWall";
$EditTool::PadModes[4] = "DeployedSpine2";
$EditTool::PadModes[5] = "DeployedSpine3";
$EditTool::PadModes[6] = "DeployedCrate0";
$EditTool::PadModes[7] = "DeployedCrate1";
$EditTool::PadModes[8] = "DeployedCrate2";
$EditTool::PadModes[9] = "DeployedCrate3";
$EditTool::PadModes[10] = "DeployedCrate4";
$EditTool::PadModes[11] = "DeployedCrate5";
$EditTool::PadModes[12] = "DeployedCrate6";
$EditTool::PadModes[13] = "DeployedCrate7";
$EditTool::PadModes[14] = "DeployedCrate8";
$EditTool::PadModes[15] = "DeployedCrate9";
$EditTool::PadModes[16] = "DeployedCrate10";
$EditTool::PadModes[17] = "DeployedCrate11";
$EditTool::PadModes[18] = "DeployedCrate12";
$EditTool::PadModes[19] = "DeployedDecoration6";
$EditTool::PadModes[20] = "DeployedDecoration16";

datablock ItemData(EditTool) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_disc.dts";
   image = EditGunImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Editing Tool";
};

datablock ShapeBaseImageData(EditGunImage) {
   className = WeaponImage;
   shapeFile = "weapon_disc.dts";
   item = EditTool;
   offset = "0 0 0";
   emap = true;
   usesEnergy = true;
   minEnergy = 0.01;

   //projectile = EditorBolt;
   //projectileType = LinearFlareProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.2;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = GrenadeLauncherFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateSound[4] = SoulTakerReloadSound;
   stateEjectShell[4]       = true;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = ChaingunDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

function EditGunImage::onMount(%this, %obj, %slot) {
   Parent::onMount(%this, %obj, %slot);
   DispEditorToolInfo(%obj);
   if(!isSet(%obj.EditPMode)) {
      %obj.EditPMode = 0;
   }
   if(!isSet(%obj.EditSMode)) {
      %obj.EditSMode = 0;
   }
   %obj.UsingEditTool = true;
}

function EditGunImage::onunmount(%this,%obj,%slot)
{
Parent::onUnmount(%this, %obj, %slot);
%obj.UsingEditTool = false;
}

function EditGunImage::onFire(%data, %obj, %slot) {
   //RAYCAST
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   %targetpos   = vectoradd(%mp,vectorscale(%vector, 2500));
   %targ         = containerraycast(%mp, %targetpos, $typemasks::staticshapeobjecttype, %obj);
   %targetObject  = getword(%targ, 0);
   if(%targetObject == 0) {
      BottomPrint(%obj.client, "No Object Found", 2, 2);
      return;
   }
   if (!Deployables.isMember(%targetObject)) {
      messageclient(%obj.client, 'MsgClient', "\c2Manipulator: Error, Map Object Selected.");
      return;
   }
   //APPLY EDITS
   switch$(%obj.EditPMode) {
      case 0:
         EToolswaping(%targetObject, %obj, 0, %obj.EditSMode);
      case 1:
         EToolswaping(%targetObject, %obj, 1, %obj.EditSMode);
      case 2:
         EToolTurrets(%targetObject, %obj, %obj.EditSMode);
      case 3:
         EToolCloakandFade(%targetObject, %obj, %obj.EditSMode);
      case 4:
         EToolDeleting(%targetObject, %obj, %obj.EditSMode);
   }
}

function EditGunImage::onMount(%this, %obj, %slot) {
   Parent::onMount(%this, %obj, %slot);
   DispEditorToolInfo(%obj);
   if(!isSet(%obj.EditPMode)) {
      %obj.EditPMode = 0;
   }
   if(!isSet(%obj.EditSMode)) {
      %obj.EditSMode = 0;
   }
   %obj.UsingEditTool = true;

}

function EditGunImage::onunmount(%this, %obj, %slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.UsingEditTool = false;
}

function DispEditorToolInfo(%obj) {
   switch(%obj.EditPMode) {
      case 0:
         %primary = "Pad Swapping";
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Blue Pad - [LSB] - MSB<spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: LSB - [MSB] - Walkway<spop>", 5, 3);
            case 2:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: MSB - [Walkway] - Medium Floor <spop>", 5, 3);
            case 3:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Walkway - [Medium Floor] - Dark Pad<spop>", 5, 3);
            case 4:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Medium Floor - [Dark Pad] - V-Pad<spop>", 5, 3);
            case 5:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Dark Pad - [V-Pad] - C.1 Backpack<spop>", 5, 3);
             case 6:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: V-Pad - [C.1 Backpack] - C.2 Small Containment<spop>", 5, 3);
             case 7:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.1 Backpack - [C.2 Small Containment] - C.3 Large Containment<spop>", 5, 3);
             case 8:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.2 Small Containment - [C.3 Large Containment] - C.4 Compressor<spop>", 5, 3);
             case 9:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.3 Large Containment - [C.4 Compressor] - C.5 Tubes<spop>", 5, 3);
             case 10:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.4 Compressor - [C.5 Tubes] - C.6 Quantium Bat.<spop>", 5, 3);
             case 11:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.5 Tubes - [C.6 Quantium Bat.] - C.7 Proton Acc.<spop>", 5, 3);
             case 12:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.6 Quantium Bat. - [C.7 Proton Acc.] - C.8 Cargo Crate<spop>", 5, 3);
             case 13:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.7 Proton Acc. - [C.8 Cargo Crate] - C.9 Mag Cooler<spop>", 5, 3);
             case 14:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.8 Cargo Crate - [C.9 Mag Cooler] - C.10 Recycle Unit<spop>", 5, 3);
             case 15:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.9 Mag Cooler - [C.10 Recycle Unit] - C.11 Fuel Canister<spop>", 5, 3);
             case 16:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.10 Recycle Unit - [C.11 Fuel Canister] - C.12 Wooden Box<spop>", 5, 3);
             case 17:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.11 Fuel Canister - [C.12 Wooden Box] - C.13 Plasma Router<spop>", 5, 3);
             case 18:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.12 Wooden Box - [C.13 Plasma Router] - Statue Base<spop>", 5, 3);
             case 19:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: C.13 Plasma Router - [Statue Base] - Blue Pad<spop>", 5, 3);
             case 20:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Statue Base - [Blue Pad] - LSB<spop>", 5, 3);
         }
      case 1:
         %primary = "Force-Field Swapping";
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Yellow - [Solid White] - Solid Red<spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid White - [Solid Red] - Solid Green<spop>", 5, 3);
            case 2:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Red - [Solid Green] - Solid Blue<spop>", 5, 3);
            case 3:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Green - [Solid Blue] - Solid Cyan<spop>", 5, 3);
            case 4:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Blue - [Solid Cyan] - Solid Magenta<spop>", 5, 3);
            case 5:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Cyan - [Solid Magenta] - Solid Yellow<spop>", 5, 3);
            case 6:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Magenta - [Solid Yellow] - Team Pass White<spop>", 5, 3);
            case 7:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Solid Yellow - [Team Pass White] - Team Pass Red<spop>", 5, 3);
            case 8:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass White - [Team Pass Red] - Team Pass Green<spop>", 5, 3);
            case 9:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Red - [Team Pass Green] - Team Pass Blue<spop>", 5, 3);
            case 10:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Green - [Team Pass Blue] - Team Pass Cyan<spop>", 5, 3);
            case 11:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Blue - [Team Pass Cyan] - Team Pass Magenta<spop>", 5, 3);
            case 12:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Cyan - [Team Pass Magenta] - Team Pass Yellow<spop>", 5, 3);
            case 13:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Magenta - [Team Pass Yellow] - All Pass White<spop>", 5, 3);
            case 14:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: Team Pass Yellow - [All Pass White] - All Pass Red<spop>", 5, 3);
            case 15:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass White - [All Pass Red] - All Pass Green<spop>", 5, 3);
            case 16:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Red - [All Pass Green] - All Pass Blue<spop>", 5, 3);
            case 17:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Green - [All Pass Blue] - All Pass Cyan<spop>", 5, 3);
            case 18:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Blue - [All Pass Cyan] - All Pass Magenta<spop>", 5, 3);
            case 19:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Cyan - [All Pass Magenta] - All Pass Yellow<spop>", 5, 3);
            case 20:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n  Grenade: All Pass Magenta - [All Pass Yellow] - Solid White<spop>", 5, 3);
         }
      case 2:
         %primary = "Turret Barrel Swapping";
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n  Grenade: Mortar - [Anti Air] - Missile<spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n  Grenade: Anti Air - [Missile] - Plasma<spop>", 5, 3);
            case 2:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n  Grenade: Missile - [Plasma] - ELF<spop>", 5, 3);
            case 3:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n  Grenade: Plasma - [ELF] - Mortar<spop>", 5, 3);
            case 4:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n  Grenade: ELF - [Mortar] - Anti Air<spop>", 5, 3);
         }
      case 3:
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n  Grenade: [Cloak] - UnCloak - Fade - UnFade <spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n  Grenade: Cloak - [UnCloak] - Fade - UnFade <spop>", 5, 3);
            case 2:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n  Grenade: Cloak - UnCloak - [Fade] - UnFade <spop>", 5, 3);
            case 3:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n  Grenade: Cloak - UnCloak - Fade - [UnFade] <spop>", 5, 3);
         }
      case 4:
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - Cloak/Fade - [Delete Objects] \n  Grenade: [Single] - Cascade <spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush>[{Manipulator}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - Cloak/Fade - [Delete Objects] \n  Grenade: Single - [Cascade] <spop>", 5, 3);
         }
   }
}

function ChangeEditGunMode(%this, %data, %PriSec) {  //This Is Easier To use
   if(%PriSec == 1) {    //Primary
      if (!(getSimTime() > (%this.mineModeTime + 100)))
         return;
      %this.mineModeTime = getSimTime();
      %this.EditPMode++;
      %this.EditSMode = 0;    //Reset Secondary Mode TO Prevent Errors
      if (%this.EditPMode > 4) {
         %this.EditPMode = 0;
      }
      DispEditorToolInfo(%this);
      return;
   }
   else {            //Secondary
      if (!(getSimTime() > (%this.grenadeModeTime + 100)))
         return;
      %this.grenadeModeTime = getSimTime();
      %this.EditSMode++;
      //Check Primaries
      if(%this.EditPMode == 0 && %this.EditSMode > 20) {
         %this.EditSMode = 0;
      }
      else if(%this.EditPMode == 1 && %this.EditSMode > 20) {
         %this.EditSMode = 0;
      }
      else if(%this.EditPMode == 2 && %this.EditSMode > 4) {
         %this.EditSMode = 0;
      }
      else if(%this.EditPMode == 3 && %this.EditSMode > 3) {
         %this.EditSMode = 0;
      }
      else if(%this.EditPMode == 4 && %this.EditSMode > 1) {
         %this.EditSMode = 0;
      }
      DispEditorToolInfo(%this);
      return;
   }
}

//Editor Tool Functioning
//
//
//
function EToolDeleting(%tobj,%plyr,%Mode) {
   %cl=%plyr.client;
   if ( %tobj.ownerGUID != %cl.guid){
      if (!%cl.isadmin && !%cl.issuperadmin){
         if (%tobj.ownerGUID !$=""){
            messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, You Do Not Own This Piece.");
            return;
         }
      }
   }
   if (%tobj.squaresize !$="") {
      messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, Unknown Object Selected.");
      return;
   }
   if (!Deployables.isMember(%tobj)) {
      messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, Map Object Selected.");
      return;
   }
   switch(%Mode) {
      case 0:
         messageclient(%cl, 'MsgClient', "\c2TextureTool: Deleting Object.");
         %tobj.getDataBlock().disassemble(%plyr, %tobj);               //this found in constructionTool.cs
      case 1:
         messageclient(%cl, 'MsgClient', "\c2TextureTool: Cascade Deleting Object (All Conective Objects).");
         cascade(%tobj,true);
   }
}
//
function EToolCloakandFade(%tobj,%plyr,%Mode) {
   %cl=%plyr.client;
   if (%tobj.ownerGUID != %cl.guid){
      if (!%cl.isadmin && !%cl.issuperadmin){
         if (%tobj.ownerGUID !$=""){
            messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, You Do Not Own This Piece.");
            return;
         }
      }
   }
   if (%tobj.squaresize !$="") {
      messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, Unknown Object Selected.");
      return;
   }
   if (!Deployables.isMember(%tobj)) {
      messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, Map Object Selected.");
      return;
   }
   switch(%Mode) {
      case 0:
         messageclient(%cl, 'MsgClient', "\c2TextureTool: Object Cloaked");
         %tobj.setCloaked(true);
         %tobj.cloaked = 1;
      case 1:
         messageclient(%cl, 'MsgClient', "\c2TextureTool: Object Un-Cloaked");
         %tobj.setCloaked(false);
         %tobj.cloaked = 0;
      case 2:
         messageclient(%cl, 'MsgClient', "\c2TextureTool: Object Faded");
         %tobj.startfade(1,0,1);
      case 3:
         messageclient(%cl, 'MsgClient', "\c2TextureTool: Object Un-Faded");
         %tobj.startfade(1,0,0);
   }
}
//
function EToolTurrets(%tobj,%plyr,%Mode) {
   %cl=%plyr.client;
   if ( %tobj.ownerGUID != %cl.guid){
      if (!%cl.isadmin && !%cl.issuperadmin){
         if (%tobj.ownerGUID !$=""){
            messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, You Do Not Own This Piece.");
            return;
         }
      }
   }
   if (%tobj.squaresize !$="") {
      messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, Unknown Object Selected.");
      return;
   }
   %classname= %tobj.getDataBlock().getName();
   if(%classname $= "TurretBaseLarge" || %classname $= "TurretDeployedBase") {
      switch$(%mode) {         //Thanks for help on this Krash..
         case 0:
            %tobj.mountImage("AABarrelLarge", 0);
            messageclient(%cl, 'MsgClient', '\c5TextureTool: Swapping Barrel with AA Barrel.');
         case 1:
            %tobj.mountImage("MissileBarrelLarge", 0);
            messageclient(%cl, 'MsgClient', '\c5TextureTool: Swapping Barrel with Missile Barrel.');
         case 2:
            %tobj.mountImage("PlasmaBarrelLarge", 0);
            messageclient(%cl, 'MsgClient', '\c5TextureTool: Swapping Barrel with Plasma Barrel.');
         case 3:
            %tobj.mountImage("ELFBarrelLarge", 0);
            messageclient(%cl, 'MsgClient', '\c5TextureTool: Swapping Barrel with ELF Barrel.');
         case 4:
            %tobj.mountImage("MortarBarrelLarge", 0);
            messageclient(%cl, 'MsgClient', '\c5TextureTool: Swapping Barrel with Mortar Barrel.');
      }
   }
   else {
      messageclient(%cl, 'MsgClient', "\c2TextureTool: Error, Object not a base turret.");
      return;
   }
}

function EToolswaping(%tobj,%plyr,%PMode,%SMode){
   //Could be cleaned up a bit later, but it works.
   %sender = %plyr.client;
   if (%tobj.ownerGUID != %sender.guid){
      if (!%sender.isadmin && !%sender.issuperadmin){
         if (%tobj.ownerGUID !$= ""){
            messageclient(%sender, 'MsgClient', '\c2You do not own this.');
            return;
         }
      }
   }
   if (%tobj.squaresize !$="")
      return;
   %classname= %tobj.getDataBlock().classname;
   %objscale = %tobj.scale;
   %grounded = %tobj.grounded;
   %pwrfreq  = %tobj.powerFreq;
   %Transform = %tobj.getTransform();

   if (%pmode == 1) {
      %db = "DeployedForceField"@%SMode;
  	  %deplObj = new ("ForceFieldBare")() {
		dataBlock = %db;
		scale     = %objscale;
	  };
      %deplObj.setTransform(%Transform);
      if (%tobj.noSlow == true){
         %deplObj.noSlow = true;
         %deplObj.pzone.delete();
	     %deplObj.pzone = "";
      }
      if (%tobj.pzone !$= "")
         %tobj.pzone.delete();
      %tobj.delete();

      // misc info
      addDSurface(%item.surface,%deplObj);

	  // [[Settings]]:

      %deplObj.grounded = %grounded;
      %deplObj.needsFit = 1;

	  // [[Normal Stuff]]:

	  // set team, owner, and handle
	  %deplObj.team = %plyr.client.team;
	  %deplObj.setOwner(%plyr);

	  // set power frequency
	  %deplObj.powerFreq = %pwrfreq;

	  // set the sensor group if it needs one
	  if (%deplObj.getTarget() != -1)
	  	  setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	  // place the deployable in the MissionCleanup/Deployables group (AI reasons)
	  addToDeployGroup(%deplObj);

	  //let the AI know as well...
	  AIDeployObject(%plyr.client, %deplObj);

	  // increment the team count for this deployed object
	  $TeamDeployedCount[%plyr.team, %item.item]++;

	  // Power object
	  checkPowerObject(%deplObj);

	  return %deplObj;
   }
   else if (%pmode == 0 && (%classname $= "decoration" || %classname $= "crate"
      || %classname $= "floor" || %classname $= "spine" || %classname $= "mspine"
      || %classname $= "wall" || %classname $= "wwall" || %classname $= "Wspine"
      || %classname $= "Sspine" || %classname $= "floor" || %classname $= "door")) {
      %tobj.setCloaked(true);
      %tobj.schedule(290, "setCloaked", false);
      if (%tobj.isdoor == 1 || %tobj.getdatablock().getname() $="DeployedTdoor"){
         if (%tobj.canmove == false) //if it cant move
            return;
         if (%tobj.state !$="closed" && %tobj.state !$="")
            return;
         }
         %db = getword($EditTool::PadModes[%SMode],0);
         if (%tobj.getdatablock().getname() $="DeployedFloor")
            %datablock="DeployedwWall";
         else if (%tobj.getdatablock().getname() $="DeployedMSpinering")
            %datablock="DeployedMSpine";
         else if (%tobj.getdatablock().getname() $="DeployedTdoor") {
            %datablock="DeployedMSpine";
         }

      else
         %datablock = %tobj.getdatablock().getname();
     %team = %tobj.team;
     %owner     = %tobj.owner;
     if (%tobj.ownerGUID>0)
        %ownerGUID = %tobj.ownerGUID;
     else
         %ownerGUID = "";

    if (%tobj.isdoor == 1 || %tobj.getdatablock().getname() $="DeployedTdoor"){
       %issliding     = %tobj.issliding;
       %state         = %tobj.state;
       %canmove       = %tobj.canmove;
       %closedscale   = %tobj.closedscale;
       %openedscale   = %tobj.openedscale;
       %oldscale      = %tobj.oldscale;
       %moving        = %tobj.moving;
       %toggletype    = %tobj.toggletype;
       %powercontrol  = %tobj.powercontrol;
       %Collision     = %tobj.Collision;
       %lv            = %tobj.lv;
       }

     %scale = %tobj.GetRealSize();

     %deplObj = new ("StaticShape")() {
		dataBlock = %db;
	 };
     %deplObj.SetRealSize(%scale);
     %deplObj.setTransform(%Transform);
//////////////////////////Apply settings//////////////////////////////

	// misc info
	addDSurface(%item.surface,%deplObj);

	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;

	// set team, owner, and handle
	%deplObj.team = %team;
	%deplObj.Ownerguid=%ownerGUID;
    %deplObj.Owner=%owner;

	// set power frequency
	%deplObj.powerFreq = %pwrfreq;
     %deplObj.OriginalPos = %tobj.OriginalPos;
	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	%deplObj.deploy();

	// Power object
	checkPowerObject(%deplObj);

    if (%tobj.isdoor == 1 || %tobj.getdatablock().getname() $="DeployedTdoor"){
       %deplObj.closedscale  = %deplObj.getScale();
       %deplObj.openedscale  = getwords(%deplObj.getScale(),0,1) SPC 0.1;
       %deplObj.isdoor       = 1;
       %deplObj.state        = %state  ;
       %deplObj.canmove      = %canmove  ;
       %deplObj.moving       = %moving ;
       %deplObj.toggletype   = %toggletype ;
       %deplObj.powercontrol = %powercontrol;
       %deplObj.Collision    = %Collision ;
       %deplObj.lv           = %lv ;
       }
       %tobj.delete();
	   return %deplObj;
     }
}
