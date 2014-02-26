function Gamebase::addTofxGroup(%obj,%set)
{
if (!isObject(%obj))
   return "";
   if (!isObject(fxGroup))
      {
      %main = new SimGroup("fxGroup");
      MissionCleanup.add(%main);
      %em = new Simgroup("fx_emitters");
      %au = new Simgroup("fx_audio");
      %pa = new Simgroup("fx_packs");
      %main.add(%em);
      %main.add(%au);
      %main.add(%pa);
      }
   if (IsObject(%obj)) {
      if (%set == 1) {
         %g = nameToID("fxGroup/fx_audio");
         %g.add(%obj);
      }
      else if (%set == 2) {
         %g = nameToID("fxGroup/fx_packs");
         %g.add(%obj);
      }
      else {
         %g = nameToID("fxGroup/fx_emitters");
         %g.add(%obj);
      }
   }
}



/////////EMITTER PACK//////////

//Example of using expert setting to select pack setting mode.
//Note uses custom (plugin) packsettings mode

//Format        <PackImageName>  = "%modes %lines %name"
//%modes gives the highest mode like the old settings
//%lines gives the amount of words in the settings which are the name of the mode
//       -1 gives the entire content of the mode
//        2 would give the first 3 words of the mode
//%name  gives the name of the pack as in %name "set to" %modename

$expertSettings[EmitterDepImage] = "3 -1 Emitter Pack:[Options]";

$expertSetting[EmitterDepImage,0] = "Select Emitter";
$expertSetting[EmitterDepImage,1] = "Select Emitter selection mode";
$expertSetting[EmitterDepImage,2] = "Select Power logic";
$expertSetting[EmitterDepImage,3] = "Select Cloak logic";

//These are my personal browse of the first few datablocks.
$packSettings[EmitterDepImage] = "12 1 Emitter Pack:[Emitter]";
$packSetting[EmitterDepImage,0] = "Small Steam ReverseEmitter";
$packSetting[EmitterDepImage,1] = "Small Bubbles GrenadeBubbleEmitter";
$packSetting[EmitterDepImage,2] = "Large Foam VehicleFoamEmitter";
$packSetting[EmitterDepImage,3] = "Small Fire PlasmaExplosionEmitter";
$packSetting[EmitterDepImage,4] = "Plasma Stream PlasmaRifleEmitter";
$packSetting[EmitterDepImage,5] = "Large Bubbles DiscExplosionBubbleEmitter";
$packSetting[EmitterDepImage,6] = "Dark Smoke GDebrisSmokeEmitter";
$packSetting[EmitterDepImage,7] = "Small Smoke GrenadeSmokeEmitter";
$packSetting[EmitterDepImage,8] = "Small Spark ELFSparksEmitter";
$packSetting[EmitterDepImage,9] = "Green Smoke MortarSmokeEmitter";
$packSetting[EmitterDepImage,10] = "Fire Stream MissileFireEmitter";
$packSetting[EmitterDepImage,11] = "Water Stream WaterStreamEmitter";
$packSetting[EmitterDepImage,12] = "Burning Object burnEmitter";

//Note how the extra %id defines the nesting of the modes.
//While previous settings could also include %id=0 I chose not to use that
//since nesting isn't directly compatible with the unified code.
//Using non nesting format for the previous allows me to use the unified code 
//for that setting.

$packSettings[EmitterDepImage,1] = "1 -1 Emitter Pack: [Selection mode]";
$packSetting[EmitterDepImage,0,1] = "Small selection";
$packSetting[EmitterDepImage,1,1] = "Sellect from datablocks";

$packSettings[EmitterDepImage,2] = "3 -1 Emitter Pack: [Power Logic]";
$packSetting[EmitterDepImage,0,2] = "Always on";
$packSetting[EmitterDepImage,1,2] = "On when powered";
$packSetting[EmitterDepImage,2,2] = "Off when powered";
$packSetting[EmitterDepImage,3,2] = "5 sec on when power change";

$packSettings[EmitterDepImage,3] = "1 -1 Emitter Pack: [Cloak Logic]";
$packSetting[EmitterDepImage,0,3] = "Always Vissible";
$packSetting[EmitterDepImage,1,3] = "Cloaked when emitting";
//Removed
$packSetting[EmitterDepImage,2,3] = "Cloaked when not emtting";
$packSetting[EmitterDepImage,3,3] = "Always Cloaked";


datablock ParticleData( WaterStreamParticle )
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 25000;  
   lifetimeVarianceMS   = 100;

   textureName          = "special/bubbles";

   useInvAlpha =     false;

   spinRandomMin = -360.0;
   spinRandomMax = 360.0;

   colors[0]     = "0.4 0.4 0.8 1.0";
   colors[1]     = "0.3 0.3 0.8 0.1";
   colors[2]     = "0.0 0.0 0.8 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 0.5;
   sizes[2]      = 0.7;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( WaterStreamEmitter )
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;

   ejectionVelocity = 30.0;  // A little oomph at the back end
   velocityVariance = 1.0;

   thetaMin         = 0.0;
   thetaMax         = 1.0;


   orientParticles  = False;
   orientOnVelocity = False;

   particles = "WaterStreamParticle";
};


datablock StaticShapeData(EmitterDep) : DeployedCrate 
{
	shapeFile = "stackable4l.dts";
        needsPower = true;        
};

datablock ShapeBaseImageData(EmitterDepImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable4l.dts";
	item = EmitterDepPack;
	mountPoint = 1;
	offset = "0 -0.18 -0.5";
	deployed = EmitterDepImage;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ItemData(EmitterDepPack) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable4l.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "EmitterDepImage";
	pickUpName = "an Emitter pack";
	heatSignature = 0;
	emap = true;
 };

function EmitterDepPack::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function EmitterDep::gainPower(%data, %obj) 
{
if (%obj.pLogic==0)
    EmitterDepSet(%obj,1);
if (%obj.pLogic==1)
    EmitterDepSet(%obj,1);
if (%obj.pLogic==2)
    EmitterDepSet(%obj,0);
if (%obj.pLogic==3)
    {
    EmitterDepSet(%obj,1);
    schedule(5000,%obj,"EmitterDepSet",%obj,0);
    }
}

function EmitterDep::losePower(%data, %obj) 
{
if (%obj.pLogic==0)
    EmitterDepSet(%obj,1);
if (%obj.pLogic==1)
    EmitterDepSet(%obj,0);
if (%obj.pLogic==2)
    EmitterDepSet(%obj,1);
if (%obj.pLogic==3)
    {
    EmitterDepSet(%obj,1);
    schedule(5000,%obj,"EmitterDepSet",%obj,0);
    }

}



function EmitterDepImage::onDeploy(%item, %plyr, %slot) {	

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;
	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
        
	%deplObj = new StaticShape() 
        {
	dataBlock = "EmitterDep";
        scale = "0.5 0.5 0.1";      
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);
        if (%plyr.packset == -1)       
           %deplObj.Emitterblock = datablockGroup.getObject(%plyr.packset[0]);
        else if (%plyr.packset[0] $= "")        
           %deplObj.Emitterblock = GetWord($packSetting[EmitterDepImage,0],2);
        else
           %deplObj.EmitterBlock = %plyr.packset[0];
        
        %deplObj.pLogic = %plyr.packset[2];
        %deplObj.cLogic = %plyr.packset[3];

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	addDSurface(%item.surface,%deplObj);
	%deplObj.powerFreq = %plyr.powerFreq;
        checkPowerObject(%deplObj);
        if (!%plyr.client.isAdmin) {
           %plyr.unmountImage(%slot);
   	       %plyr.decInventory(%item.item, 1);
       }


	return %deplObj;
}

function EmitterDep::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
        EmitterDepSet(%obj,0);
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, EmitterDepPack]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
}

function EmitterDepImage::onMount(%data, %obj, %node) {
        displayPowerFreq(%obj);
	%obj.hasEmpack = true; // not needed anymore
	%obj.packSet = 0;
	%obj.packSet[0] = 0;
	%obj.packSet[1] = 0;
	%obj.packSet[2] = 0;
	%obj.packSet[3] = 0;	
}

function EmitterDepImage::onUnmount(%data, %obj, %node) {
	%obj.hasEmpack = ""; // not needed anymore
	%obj.packSet = 0;
	%obj.packSet[0] = 0;
	%obj.packSet[1] = 0;
	%obj.packSet[2] = 0;
	%obj.packSet[3] = 0;
}

function EmitterDepSet(%obj,%set)
{
if (ISobject(%obj))
   {
   if (!%set && IsObject(%obj.emitter))
      {
      %obj.emitter.delete();
      if (%obj.cLogic == 2 || %obj.cLogic == 3)
         %obj.startfade(0,0,1);
      else if (%obj.cLogic == 1)             
         %obj.startfade(0,0,0);
      }
   if (%set && !ISobject(%obj.emitter))
      {
      %obj.emitter = CreateEmitter("0 0 0",%obj.emitterblock); 
      fx_emitters.add(%obj.emitter);
      %obj.emitter.setTransform(%obj.getTransform());
      if (%obj.cLogic == 1 || %obj.cLogic == 3)
          %obj.startfade(0,0,1);
      else if (%obj.cLogic == 2)
          %obj.startfade(0,0,0);
      }   
   }
}

//Example of nested setting modes with plugin capability.
//All but the first part of packssets are fully global defined.
//Expert mode and the very first packset refer back to the unified code.

function EmitterDepImage::ChangeMode(%data,%plyr,%val,%level)
{
if (%level == 0)
   {
   //Selecting emitter
   if (!%plyr.expertSet)
      {
      if (!%plyr.packset[1])
         {
         Parent::ChangeMode(%data,%plyr,%val,%level);
         %plyr.packset[0] = GetWord($packSetting[EmitterDepImage,%plyr.packset],2);
         }
      else
         {
         %plyr.packset = -1;
         %plyr.packset[0] = nextEmitter(%plyr.packset[0]+%val,%val);
         %name = datablockGroup.getObject(%plyr.packset[0]).GetName();       
         bottomPrint(%plyr.client,"Emitter pack set to emit:"SPC %name,2,1);
         }
      }
    //Selecting selection mode/PowerLogic/CloakLogic    
    else if (%plyr.expertSet > 0)
      {  
      %set = %plyr.expertSet;
      %image = %data.getName();
      %settings = $packSettings[%image,%set];

      %plyr.packSet[%set] = %plyr.packSet[%set] + %val;
      if (%plyr.packSet[%set] > getWord(%settings,0))
	  %plyr.packSet[%set] = 0;
      if (%plyr.packSet[%set] < 0)
 	  %plyr.packSet[%set] = getWord(%settings,0);

      %packname = GetWords(%settings,2,getWordCount(%settings));
      %curset = $PackSetting[%image,%plyr.packSet[%set],%set];    
      if (getWord(%settings,1) == -1)
	   %line = GetWords(%curset,0,getWordCount(%curset));
      else
	   %line = GetWords(%curset,0,getWord(%settings,1));
      bottomPrint(%plyr.client,%packname SPC "set to"SPC %line,2,1);        
      }
   }
else
   {
   Parent::ChangeMode(%data,%plyr,%val,%level);
   }
}

//Custion function to browse through all datablocks for emitter types.
//Allthought the cyclign takes forever and there can't be 1 proper name for each emitter
//It allows the player to fully utilize t2's content.

function nextEmitter(%start,%dir)
{
if (%dir == 1)
   {
   for(%i = %start; %i < DatablockGroup.getCount() ; %i++)
      {
      if (datablockGroup.getObject(%i).particles !$= "")
         {
         return %i;
         }
      }
   return nextEmitter(0,1);
   }
else 
   {
   for(%i = %start; %i > 0; %i--)
      {
      if (datablockGroup.getObject(%i).particles !$= "")
         {
         return %i;
         }
      }
   return nextEmitter(DatablockGroup.getCount(),-1);
   }
}

/////////Audio PACK//////////
//This is a total copy/past from the emitter deployable


$expertSettings[AudioDepImage] = "3 -1 Emitter Pack:[Options]";
$expertSetting[AudioDepImage,0] = "Select Emitter";
$expertSetting[AudioDepImage,1] = "Select Emitter selection mode";
$expertSetting[AudioDepImage,2] = "Select Power logic";
$expertSetting[AudioDepImage,3] = "Select Loop logic";

//Some I made myself
$packSettings[AudioDepImage] = "17 1 Emitter Pack:[Emitter]";
$packSetting[AudioDepImage,0] = "Jeti howl1 fx/environment/yeti_howl1.wav";
$packSetting[AudioDepImage,1] = "Jeti howl2 fx/environment/yeti_howl2.wav";
$packSetting[AudioDepImage,2] = "Jeti growl fx/environment/growl1.wav";
$packSetting[AudioDepImage,3] = "Frog sounds fx/environment/frog2.wav";
$packSetting[AudioDepImage,4] = "fly swarm fx/environment/fly_swarm.wav";
$packSetting[AudioDepImage,5] = "Bird sound1 fx/environment/bird_echo4.wav";
$packSetting[AudioDepImage,6] = "Bird sound2 fx/environment/bird_echo5.wav";
$packSetting[AudioDepImage,7] = "Ocean waves fx/environment/Oceanwaves.wav";
$packSetting[AudioDepImage,8] = "River stream fx/environment/River2.wav";
$packSetting[AudioDepImage,9] = "Cha ching fx/misc/bounty_bonus.wav";
$packSetting[AudioDepImage,10] = "Mario party fx/Bonuses/mario-6notes.wav";
$packSetting[AudioDepImage,11] = "Slick music fx/Bonuses/med-level4-slick.wav";
$packSetting[AudioDepImage,12] = "test music t2Intro.wav";
$packSetting[AudioDepImage,13] = "Tessios Heist-swing fx/bonuses/horz_straipass2_heist.wav";
$packSetting[AudioDepImage,14] = "Swami swashi fx/bonuses/low-level2-spitting.wav";
$packSetting[AudioDepImage,15] = "Smoking something fx/bonuses/med-level1-modest.wav";
$packSetting[AudioDepImage,16] = "Swami swashi2 fx/bonuses/med-level3-shining.wav";
$packSetting[AudioDepImage,17] = "Red Alert fx/misc/red_alert_short.wav";

$packSettings[AudioDepImage,1] = "1 -1 Emitter Pack: [Selection mode]";
$packSetting[AudioDepImage,0,1] = "Small selection";
$packSetting[AudioDepImage,1,1] = "Sellect from datablocks";

$packSettings[AudioDepImage,2] = "3 -1 Emitter Pack: [Power Logic]";
$packSetting[AudioDepImage,0,2] = "Always on";
$packSetting[AudioDepImage,1,2] = "On when powered";
$packSetting[AudioDepImage,2,2] = "Off when powered";
$packSetting[AudioDepImage,3,2] = "5 sec on when power change";

//Cloak logic was left out as it won't help the global image.
//And it will only worsen spam.

$packSettings[AudioDepImage,3] = "1 -1 Emitter Pack: [Loop Logic]";
$packSetting[AudioDepImage,0,3] = "Use standart";
$packSetting[AudioDepImage,1,3] = "Always loop";



datablock StaticShapeData(AudioDep) : DeployedCrate 
{
	shapeFile = "stackable2l.dts";
        needsPower = true;        
};

datablock ShapeBaseImageData(AudioDepImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable3s.dts";
	item = AudioDepPack;
	mountPoint = 1;
	offset = "0.5 -0.5 0";
        rotation = "0 1 0 90";
	deployed = AudioDepImage;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ItemData(AudioDepPack) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable3s.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "AudioDepImage";
	pickUpName = "an Sound pack";
	heatSignature = 0;
	emap = true;
 };


function AudioDepPack::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function AudioDep::gainPower(%data, %obj) 
{
if (%obj.pLogic==0)
    AudioDepSet(%obj,1);
if (%obj.pLogic==1)
    AudioDepSet(%obj,1);
if (%obj.pLogic==2)
    AudioDepSet(%obj,0);
if (%obj.pLogic==3)
    {
    AudioDepSet(%obj,1);
    schedule(5000,%obj,"AudioDepSet",%obj,0);
    }
}

function AudioDep::losePower(%data, %obj) 
{
if (%obj.pLogic==0)
    AudioDepSet(%obj,1);
if (%obj.pLogic==1)
    AudioDepSet(%obj,0);
if (%obj.pLogic==2)
    AudioDepSet(%obj,1);
if (%obj.pLogic==3)
    {
    AudioDepSet(%obj,1);
    schedule(5000,%obj,"AudioDepSet",%obj,0);
    }

}



function AudioDepImage::onDeploy(%item, %plyr, %slot) {	

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;
	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
        
	%deplObj = new StaticShape() 
        {
	dataBlock = "AudioDep";
        scale = "0.4 0.4 0.1";      
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);
        if (%plyr.packset == -1)       
           %deplObj.Audioblock = datablockGroup.getObject(%plyr.packset[0]);
        else if (%plyr.packset[0] !$= "")        
           %deplObj.AudioBlock = %plyr.packset[0];
        else
	   %deplObj.Audioblock = GetWord($packSetting[AudioDepImage,0],2);
           

        %deplObj.pLogic = %plyr.packset[2];
        %deplObj.lLogic = %plyr.packset[3];

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	addDSurface(%item.surface,%deplObj);
	%deplObj.powerFreq = %plyr.powerFreq;
        checkPowerObject(%deplObj);

        if (!%plyr.client.isAdmin)
           {
           %plyr.unmountImage(%slot);
   	   %plyr.decInventory(%item.item, 1);
           }
	return %deplObj;
}

function AudioDep::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
        AudioDepSet(%obj,0);
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, AudioDepPack]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
}

function AudioDepImage::onMount(%data, %obj, %node) {
        displayPowerFreq(%obj);
	%obj.hasAupack = true; // not needed
	%obj.packSet = 0;
	%obj.packSet[0] = "";
	%obj.packSet[1] = 0;
	%obj.packSet[2] = 0;
	%obj.packSet[3] = 0;	
}

function AudioDepImage::onUnmount(%data, %obj, %node) {
	%obj.hasAupack = "";
	%obj.packSet = 0;
	%obj.packSet[0] = "";
	%obj.packSet[1] = 0;
	%obj.packSet[2] = 0;
	%obj.packSet[3] = 0;
}

//We will keep calling the audio emitter %obj.emitter
//This will make sure in deconstruct the auto emitter is removed
function AudioDepSet(%obj,%set)
{
if (ISobject(%obj))
   {
   if (!%set && IsObject(%obj.emitter))
      {
      %obj.emitter.delete();
      }
   if (%set && !ISobject(%obj.emitter))
      {
      //The only working check to keep faulty emitters from staying around
      //Doesn't remove the console spam thougt.. :P
      %obj.emitter = CreateAudioEmitter(%obj.getTransform(),%obj.Audioblock,%obj.lLogic);
      if (isObject(%obj.emitter))
          fx_audio.add(%obj.emitter);
//      if ($audiopackloaded[%obj.emitter.filename]==1 && !Isfile(%obj.emitter.filename))
//           {
//           disassemble(%obj.getdatablock(),%obj.owner,%obj);
//           }
//      else
//           {
	   //%obj.emitter.setTransform(%obj.getTransform());
           $audiopackloaded[%obj.emitter.filename]=1;
//           }
      }
   }
}



// Just copy/past the emitter changemode since it needs almost totaly the same modes
function AudioDepImage::ChangeMode(%data,%plyr,%val,%level)
{
if (%level == 0)
   {   
   //Selecting emitter
   if (!%plyr.expertSet)
      {
      if (!%plyr.packset[1])
         {
         Parent::ChangeMode(%data,%plyr,%val,%level);
         %plyr.packset[0] = GetWord($packSetting[AudioDepImage,%plyr.packset],2);
         }
      else
         {
         %plyr.packset = -1;
         %plyr.packset[0] = nextsEmitter(%plyr.packset[0]+%val,%val);
         
         %name = datablockGroup.getObject(%plyr.packset[0]).GetName();       
         
         messageClient(%plyr.client, 'msgSound', "~w"@datablockGroup.getObject(%plyr.packset[0]).filename);
         bottomPrint(%plyr.client,"sound pack set to play:"SPC %name,2,1);
         }
      }
    //Selecting selection mode/PowerLogic/CloakLogic    
    else if (%plyr.expertSet > 0)
      {  
      %set = %plyr.expertSet;
      %image = %data.getName();
      %settings = $packSettings[%image,%set];

      %plyr.packSet[%set] = %plyr.packSet[%set] + %val;
      if (%plyr.packSet[%set] > getWord(%settings,0))
	  %plyr.packSet[%set] = 0;
      if (%plyr.packSet[%set] < 0)
 	  %plyr.packSet[%set] = getWord(%settings,0);

      %packname = GetWords(%settings,2,getWordCount(%settings));
      %curset = $PackSetting[%image,%plyr.packSet[%set],%set];    
      if (getWord(%settings,1) == -1)
	   %line = GetWords(%curset,0,getWordCount(%curset));
      else
	   %line = GetWords(%curset,0,getWord(%settings,1));
      bottomPrint(%plyr.client,%packname SPC "set to"SPC %line,2,1);

          messageClient(%plyr.client, 'msgSound', "~w"@getWords(%curset, 2));
      }
   }
else
   {
   Parent::ChangeMode(%data,%plyr,%val,%level);
   }
}


//Like promissed another browsing function.


function nextsEmitter(%start,%dir)
{
if (%dir == 1)
   {
   for(%i = %start; %i < DatablockGroup.getCount() ; %i++)
      {
      if (datablockGroup.getObject(%i).description.volume !$= "")// && isFile(DatablockGroup.getObject(%i).filename))
         {         
         return %i;
         }
      }
   return nextsEmitter(0,1);
   }
else 
   {
   for(%i = %start; %i > 0; %i--)
      {
      if (datablockGroup.getObject(%i).description.volume !$= "" ) //&& isFile(DatablockGroup.getObject(%i).filename))
         {
         return %i;
         }
      }
   return nextsEmitter(DatablockGroup.getCount(),-1);
   }
}



//Variant for createEmitter
//Can be modified to using custom audio files not found in datablocks
//Can also be futher extended to use other custom variables to people's desire
//Currently included looping mode and lifetime.
//Profile and description could be used.. however it will lessen the ability to 
//modify the emitter.

//it seems some clients deploying some sounds result in an faulty emitter
//I'l make sure those won't life to see the day.. but it's wierd still

//Added the ability to use file name as %data, so you don't need datablocks.. :D

function CreateAudioEmitter(%trans,%data,%forcelooping,%lifetime)
{

%dat = (IsObject(%data)); 
 
%desc = %data.description;

if (%forcelooping)
   %looping=1;
else
   %looping = %desc.isLooping;

%filename = %data.filename;

%audio = new AudioEmitter() 
{
position = GetWords(%trans,0,2);
rotation = GetWords(%trans,3,6);
scale = "1 1 1";
fileName = %dat ? %data.fileName : %data;
useProfileDescription = "0"; 

outsideAmbient = "1";
volume = %dat ? %desc.volume : "1";
isLooping = %dat ? %looping : "1";
is3D = %dat ? %desc.is3D : "1";
minDistance = %dat ? %desc.minDistance : "20";
maxDistance = %dat ? %desc.maxDistance : "100";
coneInsideAngle = %dat ? %desc.coneInsideAngle : "360";
coneOutsideAngle = %dat ? %desc.coneOutsideAngle : "360"; 
coneOutsideVolume = %dat ? %desc.coneOutsideVolume : "1";
coneVector = %dat ? %desc.coneVector : "0 0 1";
loopCount = %dat ? %desc.loopCount : "-1";
minLoopGap = %dat ? %desc.minLoopGap : "0";
maxLoopGap = %dat ? %desc.maxLoopGap : "0";
type = "EffectAudioType";
};

if (%lifetime !$="")
    %audio.schedule(%lifetime.delete());
return %audio;
}


/////////DISPENSER PACK//////////

//Another nessted pack setting

//Format        <PackImageName>  = "%modes %lines %name"
//%modes gives the highest mode like the old settings
//%lines gives the amount of words in the settings which are the name of the mode
//       -1 gives the entire content of the mode
//        2 would give the first 3 words of the mode
//%name  gives the name of the pack as in %name "set to" %modename

$expertSettings[DispenserDepImage] = "8 -1 Dispenser Pack:[Options]";
$expertSetting[DispenserDepImage,0] = "[Player Packs]";
$expertSetting[DispenserDepImage,1] = "[Basic Building Parts]";
$expertSetting[DispenserDepImage,2] = "[Base Assets]";
$expertSetting[DispenserDepImage,3] = "[T2 Deployables]";
$expertSetting[DispenserDepImage,4] = "[Decoration packs]";
$expertSetting[DispenserDepImage,5] = "[Misc Deployables]";
$expertSetting[DispenserDepImage,6] = "[Barrels]";
$expertSetting[DispenserDepImage,7] = "[Weapons]";
$expertSetting[DispenserDepImage,8] = "[Ammo]";


//Hacky hacky way to get modes working.. hahaha
$packSettings[DispenserDepImage] = "8 1 Dispenser Pack:[T2 Packs]";

//Snagged from player.cs
$packSettings[DispenserDepImage,0] = "4 1 Dispenser Pack:[T2 Packs]";
$packSetting[DispenserDepImage,0,0] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,0] = "[Random pack] random";
$packSetting[DispenserDepImage,2,0] = "Repair Pack RepairPack";
$packSetting[DispenserDepImage,3,0] = "Ammunition Pack AmmoPack";
$packSetting[DispenserDepImage,4,0] = "Satchel Charge SatchelCharge";

$packSettings[DispenserDepImage,1] = "8 1 Dispenser Pack:[Basic Building Parts]";
$packSetting[DispenserDepImage,0,1] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,1] = "[Random pack] random";
$packSetting[DispenserDepImage,2,1] = "Light beam spinedeployable";
$packSetting[DispenserDepImage,3,1] = "Light walkway wWallDeployable";
$packSetting[DispenserDepImage,4,1] = "Light BlastWall WallDeployable";
$packSetting[DispenserDepImage,5,1] = "Medium beam mspineDeployable";
$packSetting[DispenserDepImage,6,1] = "Medium Floor floorDeployable";
$packSetting[DispenserDepImage,7,1] = "Force Field ForceFieldDeployable";
$packSetting[DispenserDepImage,8,1] = "Gravity Field GravityFieldDeployable";

$packSettings[DispenserDepImage,2] = "9 1 Dispenser Pack:[Base Assets]";
$packSetting[DispenserDepImage,0,2] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,2] = "[Random pack] random";
$packSetting[DispenserDepImage,2,2] = "Large Inventory-station LargeInventoryDeployable";
$packSetting[DispenserDepImage,3,2] = "Medium-Sensor Pack MediumSensorDeployable";
$packSetting[DispenserDepImage,4,2] = "Large-Sensor Pack LargeSensorDeployable";
$packSetting[DispenserDepImage,5,2] = "Logo-Projector Pack LogoProjectorDeployable";
$packSetting[DispenserDepImage,6,2] = "Deployable Turret-Base TurretBasePack";
$packSetting[DispenserDepImage,7,2] = "Solar-Panel Pack SolarPanelDeployable";
$packSetting[DispenserDepImage,8,2] = "Generator Pack GeneratorDeployable";
$packSetting[DispenserDepImage,9,2] = "Switch Pack SwitchDeployable";

$packSettings[DispenserDepImage,3] = "6 1 Dispenser Pack:[T2 Deployables]";
$packSetting[DispenserDepImage,0,3] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,3] = "[Random pack] random";
$packSetting[DispenserDepImage,2,3] = "Motion-Sensor Pack MotionSensorDeployable";
$packSetting[DispenserDepImage,3,3] = "Pulse-Sensor Pack PulseSensorDeployable";
$packSetting[DispenserDepImage,4,3] = "Landspike Turret TurretOutdoorDeployable";
$packSetting[DispenserDepImage,5,3] = "SpiderClamp Turret TurretIndoorDeployable";
$packSetting[DispenserDepImage,6,3] = "Inventory Station InventoryDeployable";

$packSettings[DispenserDepImage,4] = "7 1 Dispenser Pack:[Decoration packs]";
$packSetting[DispenserDepImage,0,4] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,4] = "[Random pack] random";
$packSetting[DispenserDepImage,2,4] = "Decoration Pack DecorationDeployable";
$packSetting[DispenserDepImage,3,4] = "Crate Pack PulseSensorDeployable";
$packSetting[DispenserDepImage,4,4] = "Light Pack LightDeployable";
$packSetting[DispenserDepImage,5,4] = "Emitter Pack EmitterDepPack";
$packSetting[DispenserDepImage,6,4] = "Audio Pack AudioDepPack";
$packSetting[DispenserDepImage,7,4] = "Tree Pack TreeDeployable";

$packSettings[DispenserDepImage,5] = "7 1 Dispenser Pack:[Misc Deployables]";
$packSetting[DispenserDepImage,0,5] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,5] = "[Random pack] random";
$packSetting[DispenserDepImage,2,5] = "Jump Pad JumpadDeployable";
$packSetting[DispenserDepImage,3,5] = "Teleport Pad TelePadPack";
$packSetting[DispenserDepImage,4,5] = "Tripwire Pack TripwireDeployable";
$packSetting[DispenserDepImage,5,5] = "Escape Pod EscapePodDeployable";
$packSetting[DispenserDepImage,6,5] = "Pack Dispenser DispenserDepPack";
$packSetting[DispenserDepImage,7,5] = "Energize pack EnergizerDeployable";

$packSettings[DispenserDepImage,6] = "7 1 Dispenser Pack:[Decoration packs]";
$packSetting[DispenserDepImage,0,6] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,6] = "[Random pack] random";
$packSetting[DispenserDepImage,2,6] = "Mortar Barrel MortarBarrelPack";
$packSetting[DispenserDepImage,3,6] = "Plasma Barrel PlasmaBarrelPack";
$packSetting[DispenserDepImage,4,6] = "AA Barrel AABarrelPack";
$packSetting[DispenserDepImage,5,6] = "Missile Barrel MissileBarrelPack";

$packSettings[DispenserDepImage,7] = "8 1 Dispenser Pack:[Weapons]";
$packSetting[DispenserDepImage,0,7] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,7] = "[Random pack] random";
$packSetting[DispenserDepImage,2,7] = "S3 Rifle S3Rifle";
$packSetting[DispenserDepImage,3,7] = "Colt Pistol pistol";
$packSetting[DispenserDepImage,4,7] = "G41 Rifle G41Rifle";
$packSetting[DispenserDepImage,5,7] = "M1700 Shotgun M1700";
$packSetting[DispenserDepImage,6,7] = "Stinger Launcher Stinger";
$packSetting[DispenserDepImage,7,7] = "Javelin Launcher Javelin";
$packSetting[DispenserDepImage,8,7] = "Wp400 Shotgun Wp400";

$packSettings[DispenserDepImage,8] = "8 1 Dispenser Pack:[Ammo]";
$packSetting[DispenserDepImage,0,8] = "[Cycle packs] cycle";
$packSetting[DispenserDepImage,1,8] = "[Random pack] random";
$packSetting[DispenserDepImage,2,8] = "S3 Ammo S3RifleAmmo";
$packSetting[DispenserDepImage,3,8] = "G41 Ammo G41RifleAmmo";
$packSetting[DispenserDepImage,4,8] = "M1700 Ammo M1700Ammo";
$packSetting[DispenserDepImage,5,8] = "C4 Explosive C4";
$packSetting[DispenserDepImage,6,8] = "Stinger Ammo StingerAmmo";
$packSetting[DispenserDepImage,7,8] = "Javelin Ammo JavelinAmmo";
$packSetting[DispenserDepImage,8,8] = "Wp400 Ammo Wp400Ammo";
///(Arg my poor fingers.. :()


datablock ParticleData( DispenserEffectParticle )
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 250;  
   lifetimeVarianceMS   = 100;

   textureName          = "flarebase";

   useInvAlpha =     false;

   spinRandomMin = -360.0;
   spinRandomMax = 360.0;

   colors[0]     = "0.4 0.4 0.4 1.0";
   colors[1]     = "0.3 0.3 0.3 0.1";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 0.5;
   sizes[2]      = 0.7;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( DispenserEffectEmitter )
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;

   ejectionVelocity = 5.0;  // A little oomph at the back end
   velocityVariance = 2.0;

   thetaMin         = 20.0;
   thetaMax         = 30.0;
   phiReferenceVel = "0";
   phiVariance = "360";

   orientParticles  = False;
   orientOnVelocity = False;

   particles = "DispenserEffectParticle";
};


datablock StaticShapeData(DispenserDep) : DeployedCrate 
{
	shapeFile = "stackable4m.dts";
        needsPower = true;        
};

datablock ShapeBaseImageData(DispenserDepImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable4m.dts";
	item = DispenserDepPack;
	mountPoint = 1;
	offset = "0 -0.18 -0.5";
	deployed = DispenserDep;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ItemData(DispenserDepPack) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable4m.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "DispenserDepImage";
	pickUpName = "an pack Dispenser";
	heatSignature = 0;
	emap = true;
 };

function DispenserDepPack::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function DispenserDep::gainPower(%data, %obj) 
{
respawnpack(%obj);
}

function DispenserDep::losePower(%data, %obj) 
{
if (isObject(%obj.emitter))
	%obj.emitter.delete();
}



function DispenserDepImage::onDeploy(%item, %plyr, %slot) {	

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;
	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
        
	%deplObj = new StaticShape() 
        {
	dataBlock = "DispenserDep";
        scale = "1.5 1.5 0.5";      
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);

                
        %set1 = %plyr.packSet  ? %plyr.packSet : 0;
        %set2 = %plyr.expertSet ? %plyr.expertSet : 0;        
        %name =  GetWord($packSetting[DispenserDepImage, %set1,%set2],2);

        %deplObj.packblock = %name;
        %deplObj.set1 = %set1;
        %deplObj.set2 = %set2;
        
	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	addDSurface(%item.surface,%deplObj);
	%deplObj.powerFreq = %plyr.powerFreq;
        checkPowerObject(%deplObj);
        respawnpack(%deplObj);

        if (!%plyr.client.isAdmin)
           {
           %plyr.unmountImage(%slot);
   	   %plyr.decInventory(%item.item, 1);
           }

	return %deplObj;
}

function DispenserDep::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;      
        if (isObject(%obj.emitter))
            %obj.emitter.delete();
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, DispenserDepPack]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
}

function DispenserDepImage::onMount(%data, %obj, %node) {
        displayPowerFreq(%obj);
	%obj.hasEmpack = true; // not needed anymore
	%obj.packSet = 0;
	%obj.packSet[0] = 0;
	%obj.packSet[1] = 0;
	%obj.packSet[2] = 0;
	%obj.packSet[3] = 0;	
}

function DispenserDepImage::onUnmount(%data, %obj, %node) {
	%obj.hasEmpack = ""; // not needed anymore
	%obj.packSet = 0;
	%obj.packSet[0] = 0;
	%obj.packSet[1] = 0;
	%obj.packSet[2] = 0;
	%obj.packSet[3] = 0;
}



//Respawns dispenser's item.

function respawnpack(%disp,%override)
{
if ((!IsObject(%disp.pack)) && isObject(%disp))
   {
   %name = %disp.packblock;
   %set1 = %disp.set1; 
   %set2 = %disp.set2; 

   if (%name $= "cycle")
      {
      %disp.set1++; 
      %max =  GetWord($packSettings[DispenserDepImage,%set2 ],0);
      if (%disp.set1 > %max || %disp.set1 < 2)
          %disp.set1 = 2;
      %name =  GetWord($packSetting[DispenserDepImage, %disp.set1,%set2],2);
      }
   else if (%name $= "random")
      {
      %max =  GetWord($packSettings[DispenserDepImage,%set2 ],0);
      %set1 = mFloor(getRandom()*(%max-2) +2);
      %name =  GetWord($packSetting[DispenserDepImage, %set1,%set2],2);
      }   
   %pack = new Item() 
         {
         dataBlock = %name;
         static = false;
         rotate = true;                  
	 };
   %pack.startFade(0,0,1);
   %pos = VectorAdd(RealVec(%disp,"0 0 1"),%disp.getEdge("0 0 1"));
   %pack.setTransform(%pos SPC "0 0 1" SPC getRandom()*$Pi*2);
   %em = createLifeEmitter(%pos,DispenserEffectEmitter,1000);
   %pack.startFade(1000,0,0);
   %em.setRotation(%disp.getRotation);
   %pack.dispenser = %disp;   
   %disp.emitter = %pack;  //Lil hook to make it get removed when disas
   %disp.pack = %pack;
   %pack.addTofxGroup(2);
   }
}

//Not much has changed.. :D

function DispenserDepImage::ChangeMode(%data,%plyr,%val,%level) {
if (%level == 0) {
   if (%plyr.expertset $= "")
       %plyr.expertset = 0; 
   //Selecting Dispenser   
      %set = %plyr.expertSet $= "" ? 0 : %plyr.expertSet;
      %image = %data.getName();
      %settings = $packSettings[%image,%set];

      %plyr.packSet = %plyr.packSet + %val;
      if (%plyr.packSet > getWord(%settings,0))
	  %plyr.packSet = 0;
      if (%plyr.packSet < 0)
 	  %plyr.packSet = getWord(%settings,0);

      %packname = GetWords(%settings,2,getWordCount(%settings));
      %curset = $PackSetting[%image,%plyr.packSet,%set];    
      if (getWord(%settings,1) == -1)
	   %line = GetWords(%curset,0,getWordCount(%curset));
      else
	   %line = GetWords(%curset,0,getWord(%settings,1));
      bottomPrint(%plyr.client,%packname SPC "set to"SPC %line,2,1);        
      
   }
   else {
   Parent::ChangeMode(%data,%plyr,%val,%level);
   }
}

