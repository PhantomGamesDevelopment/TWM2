//Weather
//Allows for changing conditions
//By Phantom139

//Get Weather via Zip Code
$GettingWx = false;
function GetWeather(%zip, %functionality) {
   if($GettingWx) {
      return;
   }
   if(strlen(%zip) != 5) {
      if(%functionality == 1) {
         schedule(500, 0, MessageAll, 'msgTime', "\c4"@$ChatBot::Name@": That's an invalid zip code/char code.");
      }
      else {
         error("Invalid zip code/char code sent to the server for weather updates, canceling.");
      }
      return;
   }
   //
   if(strlen(%zip) == 5) {
      for (%i = 0; %i < strlen(%zip); %i++) {
         %char = strcmp(getSubStr(%zip, %i, 1), "");
         if (%char > 57 || %char < 48) {
            if(%functionality == 1) {
               schedule(500, 0, MessageAll, 'msgTime', "\c4"@$ChatBot::Name@": That's an invalid zip code.");
            }
            else {
               error("Invalid zip code sent to the server for weather updates, canceling.");
            }
            return;
         }
      }
   }
   //
   $GettingWx = true;
   if (!isObject(WeatherGetter)) {
      %Downloader = new TCPObject(WeatherGetter);
   }
   else {
      %Downloader = WeatherGetter;
   }
   %Downloader.func = %functionality;
   %Downloader.zip = %zip;
   %Downloader.connect("forecast.weather.gov:80");
}

//http://forecast.weather.gov/zipcity.php
//zipcity -> POST var
function WeatherGetter::onConnected(%this) {
   %this.schedule(5000, "disconnect");

   %sep = getRandomSeparator(16);
   %loc = "/zipcity.php";
   %header1 = "POST" SPC %loc SPC "HTTP/1.1\r\n";
   %host = "Host: forecast.weather.gov\r\n";
   %header2 = "Connection: close\r\nUser-Agent: Tribes 2\r\n";
   %contType = "Content-Type: multipart/form-data; boundary="@%sep@"\r\n";
   %zipReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"inputstring\"\r\n\r\n"@%this.zip@"";
   %payload = %zipReq@"--"@%sep@"--";
   %qlen = strLen(%payload);
   %contentLeng = "Content-Length: "@%qlen@"\r\n\r\n";
   %query = %header1@%host@%header2@%contType@%contentLeng@%payload;
   
   %this.send(%query);
}

//Test Dump
//Latest Observation for Rockford, IL (61110)
//Temp: 39F (3C)
function WeatherGetter::onLine(%this, %line) {
   //get Location of the Zip
   if(strstr(%line, "Point Forecast:</b>") != -1) {
      //strip line to the necessary point.
      %linePos = strPos(%line, "Point Forecast:</b>");
      %newLine = getsubStr(%line, %linePos+strLen("Point Forecast:</b>"), strLen(%line));
      %endPos = strPos(%newLine, "<br>");
      %this.WeatherLoc = getSubStr(%newLine, 0, %endPos);
   }
   //get the weather
   if(strstr(%line, "<td class=\"big\" width=\"120\" align=\"center\">") != -1) {
      //obtain the sky
      %linePos = strPos(%line, "<td class=\"big\" width=\"120\" align=\"center\">");
      %newLine = getsubStr(%line, %linePos+strLen("<td class=\"big\" width=\"120\" align=\"center\">"), strLen(%line));
      %endPos1 = strPos(%newLine, "<br><br>");
      %this.OutSky = getSubStr(%newLine, 0, %endPos1);
      //-------------------------------
      //obtain the temperature
      %newLine = getSubStr(%newLine, %endPos1+strLen("<br><br>"), strLen(%newLine));
      %endPos2 = strPos(%newLine, "&deg;F<br>");
      %temp = trim(getSubStr(%newLine, 0, %endPos2));
      %this.Temp = %temp@" F ("@mCeil((%temp-32)*(5/9))@" C)";
      //-------------------------------
   }
   //MessageAll('wxDump', ""@%line@"");
}

function WeatherGetter::onConnectFailed(%this) {
   $GettingWx = false;
   if(%this.func == 1) {
      schedule(500, 0, MessageAll, 'msgTime', "\c4"@$ChatBot::Name@": Unable to connect to NOAA National Weather Service Website.");
   }
   else {
      //keep the current conditions
   }
}

function WeatherGetter::onDisconnect(%this) {
   // Uh oh, someone was naughty! Kill it!
   $GettingWx = false;
   if(!isSet(trim(%this.WeatherLoc))) {
      schedule(500, 0, MessageAll, 'msgTime', "\c4"@$ChatBot::Name@": That's an invalid location.");
      return;
   }
   //
   if(%this.func == 1) {
      schedule(500, 0, MessageAll, 'msgTime', "\c4"@$ChatBot::Name@": Current conditions for: "@%this.WeatherLoc@", "@%this.OutSky@": "@%this.Temp@".");
   }
   else {
      ApplyWeatherCond(%this.Sky, %this.Weather);
   }
   %this.schedule(1000, delete);
}

//

function ApplyWeatherCond(%skyStr, %weaStr) {
   return; //Working on This!

   echo("** WEATHER: Applying new weather: "@%skyStr@", "@%weaStr@"");
   echo("** Updated at: "@FormatTimeString("hh:mm")@"");
   //
   if(!isSet($Weather::HourlyUpdate) || $Weather::HourlyUpdate < 1) {
      $Weather::HourlyUpdate = 1;
   }
   //
   if($Weather::UseConstantConditionMonitor) {
      messageAll('MsgAdminForce', "\c3SERVER: Updating current weather conditions.");
      //Call this once to clear any 'issues'
      skySunny();
      //
      // is there a set 'Weather' going on?
      if(%weaStr !$= "N/A") {
         //OK, we must have some precip going on, lets head to our search tools
         if(strstr(%weaStr, "rain") != -1) {
            echo("*** WEATHER: Applying Rain Sky");
            messageAll('msgWeather', "\c3SERVER: Weather conditions report \c5Rain\c3.");
            skyRain();
         }
         else if(strstr(%weaStr, "thunderstorm") != -1) {
            echo("*** WEATHER: Applying Storm Sky");
            messageAll('msgWeather', "\c3SERVER: Weather conditions report \c5Thunderstorms\c3.");
            skyThunderStorm();
         }
         else if(strstr(%weaStr, "snow") != -1) {
            echo("*** WEATHER: Applying Snow Sky");
            messageAll('msgWeather', "\c3SERVER: Weather conditions report \c5Snow\c3.");
            skySnowy();
         }
      }
      else {
         //no precip, lets search the skies string for sunny, or cloudy
         if(strstr(%skyStr, "sunny") != -1 || strstr(%skyStr, "clear") != -1) {
            echo("*** WEATHER: Applying Sunny Sky");
            messageAll('msgWeather', "\c3SERVER: Weather conditions report \c5Sunny\c3.");
            skySunny();
         }
         else {
            echo("*** WEATHER: Applying Cloudy Sky");
            messageAll('msgWeather', "\c3SERVER: Weather conditions report \c5Cloudy\c3.");
            skyCloudy();
         }
      }
   }
   //
   $Weather::NextUpdate = schedule((1000 * 60 * 60 * $Weather::HourlyUpdate), 0, "GetWeather", $Weather::DefaultZipLocation, 2);
}




















//DATABLOCKS/FUNCTIONS:
datablock PrecipitationData(RainFix) {
   type = 0;
   soundProfile = "";
   materialList = "raindrops.dml";
   sizeX = 0.2;
   sizeY = 0.45;

   movingBoxPer = 0.35;
   divHeightVal = 1.5;
   sizeBigBox = 1;
   topBoxSpeed = 20;
   frontBoxSpeed = 30;
   topBoxDrawPer = 0.5;
   bottomDrawHeight = 40;
   skipIfPer = -0.3;
   bottomSpeedPer = 1.0;
   frontSpeedPer = 1.5;
   frontRadiusPer = 0.5;
};

//Some of these functions aren't used, but I'm sure someone out there can find a use for it.

function skyDusk() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "1000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "800";
		fogColor = "0.5 0.2 0.00 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "SOM_TR2_Armageddon.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skyCloudy() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "450";
		fogColor = "0.3 0.3 0.3 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_badlands_cloudy.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}
function skyThunderStorm() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "40";
		fogColor = "0.2 0.2 0.2 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_badlands_cloudy.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
 	new Lightning(Lightning) {
		position = "0 0 0";
		rotation = "1 1 0 0";
		scale = "9999 9999 160";
		dataBlock = "DefaultStorm";
		strikesPerMinute = 30;
		strikeWidth = 1 + getrandom() * 3;
		chanceToHitTarget = 0.0;
		strikeRadius = 1000;
		boltStartRadius = 1000;
		color = "1 1 1";
		fadeColor = "1 1 1";
		useFog = "0";
	};
    new Precipitation(Precipitation) {
		position = "-225.463 143.423 202.782";
		rotation = "1 0 0 0";
		scale = "5 5 5";
		dataBlock = "RainFix";
		percentage = "1";
		color1 = "0.8 0.8 0.8 1";
		color2 = "0.6 0.6 0.6 1";
		color3 = "0.4 0.4 0.4 1";
		offsetSpeed = "0.25";
		minVelocity = "1";
		maxVelocity = "2";
		maxNumDrops = "2000";
		maxRadius = "100";
	};
}
function skyRain() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "300";
		fogColor = "0.3 0.3 0.3 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_desert_brown.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
    new Precipitation(Precipitation) {
		position = "-225.463 143.423 202.782";
		rotation = "1 0 0 0";
		scale = "5 5 5";
		dataBlock = "RainFix";
		percentage = "1";
		color1 = "0.8 0.8 0.8 1";
		color2 = "0.6 0.6 0.6 1";
		color3 = "0.4 0.4 0.4 1";
		offsetSpeed = "0.25";
		minVelocity = "1";
		maxVelocity = "2";
		maxNumDrops = "2000";
		maxRadius = "100";
	};
}
function skyVeryDark() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "0";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "300";
		fogColor = "0.0 0.0 0.0 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_desert_starrynight.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}
function skyDaylight() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "700";
		fogColor = "0.5 0.5 0.5 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lush_blue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skyNight() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "200";
		fogColor = "0.0 0.0 0.00 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lava_starrynight.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skySunny() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "600";
		fogColor = "0.650000 0.650000 0.500000 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "SOM_TR2_WinterBlue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skyMorning() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "450";
		fogColor = "0.6 0.6 0.7 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lush_blue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skySnowy() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
    new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
    	cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "300";
		fogColor = "0.5 0.5 0.5 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lush_blue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
    };
    new Precipitation(Precipitation) {
		position = "0 0 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "Snow";
		percentage = "1";
		color1 = "1 1 1 1.000000";
		color2 = "-1 -1 -1 1";
    	color3 = "-1 -1 -1 1.000000";
		offsetSpeed = "0.25";
		minVelocity = "1";
		maxVelocity = "2";
		maxNumDrops = "2000";
		maxRadius = "100";
   };
}

function skyFog() {
    $SevereStormInProgress = 0;
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "250";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "15";
		fogColor = "0.3 0.3 0.3 1";
		fogVolume1 = "5 5 5";
		fogVolume2 = "5 5 5";
		fogVolume3 = "5 5 5";
		materialList = "sky_badlands_cloudy.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}































//Killer Weather :D
function HailStones(%strength) {
   if(!$SevereStormInProgress) {
      return;
   }
   for(%i = 0; %i < %strength; %i++) {
      %pos = TWM2Lib_MainControl("RMPG");
      %spawn = vectorAdd(%pos, "0 0 300");
      %hail = new LinearProjectile() {
         datablock = HailProjectile;
         initialPosition = vectorAdd(%spawn, "0 0 "@%i + 0.07@"");
         initialDirection = "0 0 -5";
      };
      MissionCleanup.add(%hail);
   }
   schedule(4000, 0, "HailStones", GetRandom(100, 500));
}

function skySevereTStorm() {
    messageAll('msgAlert', "\c3NWS: A Severe Thunderstorm Warning has been issued for the "@$CurrentMission@" area! ~wfx/misc/red_alert_short.wav");
    $SevereStormInProgress = 1;
    %tornadicChance = getRandom(1, 10);
    if(%tornadicChance % 2 == 0) {
       %timeToSpawn = getRandom(5000, 15000);
       schedule(%timeToSpawn, 0, "RandomTornado");
    }
    //
    //
    if(isObject(Sky)) {
	   Sky.delete();
    }
    if(isObject(Precipitation)) {
 	   Precipitation.delete();
    }
    if(isObject(Lightning)) {
 	   Lightning.delete();
    }
	new Sky(Sky) {
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.002";
		cloudSpeed2 = "0.003";
		cloudSpeed3 = "0.004";
		visibleDistance = "400";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "40";
		fogColor = "0.2 0.2 0.2 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_badlands_cloudy.dml";
		windVelocity = getRandom()*10 SPC getRandom()*10 SPC getRandom()*10;
		windEffectPrecipitation = "1";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
    %tHei = getTerrainHeight("0 0 0");
 	new Lightning(Lightning) {
		position = "0 0 "@ %tHei;
		rotation = "1 1 0 0";
		scale = "1 1 160";
		datablock = "DefaultStorm";
		strikesPerMinute = 90;
		strikeWidth = 1 + getrandom() * 3;
		chanceToHitTarget = 50.0;
		strikeRadius = 1000;
		boltStartRadius = 500;
		color = "1 1 1";
		fadeColor = "1 1 1";
		useFog = "0";
	};
    new Precipitation(Precipitation) {
		position = "-225.463 143.423 202.782";
		rotation = "1 0 0 0";
		scale = "5 5 5";
		datablock = "RainFix";
		percentage = "1";
		color1 = "0.8 0.8 0.8 1";
		color2 = "0.6 0.6 0.6 1";
		color3 = "0.4 0.4 0.4 1";
		offsetSpeed = "0.25";
		minVelocity = "5";
		maxVelocity = "8";
		maxNumDrops = "5000";
		maxRadius = "100";
	};
    HailStones(GetRandom(100, 500));
}

datablock LinearProjectileData(HailProjectile)
{
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.35;
   damageRadius        = 10.5;
   radiusDamageType    = $DamageType::Hail;
   kickBackStrength    = 1750;

   sound 				= discProjectileSound;
   explosion           = "DiscExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = 300;
   wetVelocity       = 250;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};

datablock ParticleData(TornadoSmoke)
{
   dragCoeffiecient     = 0.2;
   gravityCoefficient   = -0.4;   // rises slowly
   inheritedVelFactor   = 0.4;
   windCoefficient   = 0;
   lifetimeMS           =  8000;
   lifetimeVarianceMS   =  300;
   useInvAlpha          =  true;
   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;

   animateTexture = true;
   framesPerSec = 2;

   animTexName[00]       = "special/smoke/bigSmoke";
   animTexName[01]       = "special/smoke/smoke_001";
   animTexName[02]       = "special/smoke/smoke_002";
   animTexName[03]       = "special/smoke/smoke_003";
   animTexName[04]       = "special/smoke/smoke_004";
   animTexName[05]       = "special/smoke/smoke_005";
   animTexName[06]       = "special/smoke/smoke_006";
   animTexName[07]       = "special/smoke/smoke_007";
   animTexName[08]       = "special/smoke/smoke_008";
   animTexName[09]       = "special/smoke/smoke_009";
   animTexName[10]       = "special/smoke/smoke_010";
   animTexName[11]       = "special/smoke/smoke_011";
   animTexName[12]       = "special/smoke/smoke_012";

   colors[0]     = "0.3 0.3 0.3 1.0";
   colors[1]     = "0.3 0.3 0.3 0.5";
   colors[2]     = "0.3 0.3 0.3 0.0";

   sizes[0]      = 10.0;
   sizes[1]      = 13.0;
   sizes[2]      = 17.0;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(TornadoSmokeEmitter) {
   ejectionPeriodMS = 15;
   periodVarianceMS = 0.5;

   ejectionVelocity = 10.25;
   velocityVariance = 0.55;
   thetaMin         = 0.0;
   thetaMax         = 20.0;
   phiReferenceVel = 500;
   phiVariance = 360;
   overrideAdvances = 0;
   orientParticles= 0;
   orientOnVelocity = 1;

   particles = "TornadoSmoke";
};

function RunTornadoEffect_AT(%pos) {
   //
   %z = getTerrainHeight(%pos);
   %pos = getWords(%pos, 0, 1) SPC %z;
   //
   %c = createEmitter(%pos, TornadoSmokeEmitter, "1 0 0");
   %c.schedule(500, "Delete");
   //PHASE 1
   // - SUCKING
   %TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType;
   InitContainerRadiusSearch(vectorAdd(%pos, "0 0 35"), 100, %TargetSearchMask);

   while ((%potentialTarget = ContainerSearchNext()) != 0){
      if(strStr(%potentialTarget.getName(), "Terrain") == -1) {
         %dist = containerSearchCurrRadDamageDist();
         %tgtPos = %potentialTarget.getWorldBoxCenter();
         %distance2 = VectorDist(%tgtPos,vectorAdd(%pos, "0 0 35"));
         %distance = mfloor(%distance2);
         %vec = VectorNormalize(VectorSub(vectorAdd(%pos, "0 0 35"),%tgtpos));
         %CN = %potentialTarget.getClassName();
         //echo("CLASS: "@%CN@"");
         if(%CN $= "HoverVehicle") {
            %nForce = -5000;
         }
         else {
            %nForce = -500;                              //come here =-D
         }
         %forceVector = vectorScale(%vec, -1*%nForce);

         if (%potentialTarget.getPosition() != vectorAdd(%pos, "0 0 35")) {
            %potentialTarget.applyImpulse(%potentialTarget.getPosition(), %forceVector);
         }
      }
   }
   //Phase 2
   // - DAMAGE
   %TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType
      | $TypeMasks::StaticObjectType | $TypeMasks::TurretObjectType | $TypeMasks::SensorObjectType;
   InitContainerRadiusSearch(%pos, 50, %TargetSearchMask);

   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      // Pieces?
      if(strStr(%potentialTarget.getName(), "Terrain") == -1) {
         if(%potentialTarget.getOwner() != 0) {
            %potentialTarget.damage(0, %potentialTarget.getPosition(), 100.01, $DamageType::Tornado);
         }
         else {
         // Non-Pieces
            if(vectorDist(vectorAdd(%pos, "0 0 35"), %potentialTarget.getPosition()) < 10) {
               %potentialTarget.damage(0, %potentialTarget.getPosition(), 100.01, $DamageType::Tornado);
            }
            if(vectorDist(%pos, %potentialTarget.getPosition()) < 10) {
               %potentialTarget.damage(0, %potentialTarget.getPosition(), 100.01, $DamageType::Tornado);
            }
         }
      }
   }
}

function CreateTornado(%startPos, %endPos) {
   MessageAll('msgWarning', "\c3NWS: A Tornado Warning has been issued for the "@$CurrentMission@" Area, a Tornado has been spotted.");
   %liveTime = getRandom(30000, 90000);
   %moveTicks = mFloor(%liveTime / 100);
   %vectoral_Subtraction = vectorSub(%endPos, %startPos);
   //each step, we move another one of these
   %vectoral_XYZ = (getWord(%vectoral_Subtraction, 0)/%moveTicks) SPC (getWord(%vectoral_Subtraction, 1)/%moveTicks) SPC getWord(%vectoral_Subtraction, 2);
   //begin the twister :D
   TornadicLoop(%moveTicks, %startPos, %vectoral_XYZ, 0);
}

function TornadicLoop(%maximum_move, %starting_position, %vector_move, %counter) {
   %counter++;
   if(%counter > %maximum_move) {
      return;
   }
   else {
      //scale the tornadic move positioning
      %new_position_x = getWord(%starting_position, 0) + E_Sigma(0, %counter, getWord(%vector_move, 0));
      %new_position_y = getWord(%starting_position, 1) + E_Sigma(0, %counter, getWord(%vector_move, 1));
      %final = %new_position_x SPC %new_position_y SPC getWord(%vector_move, 2);
      RunTornadoEffect_AT(%final);
      schedule(100, 0, "TornadicLoop", %maximum_move, %starting_position, %vector_move, %counter);
   }
}

//cause random is epic :D
function RandomTornado() {
   %spawn = TWM2Lib_MainControl("RMPG");
   %end = TWM2Lib_MainControl("RMPG");
   // begin
   CreateTornado(%spawn, %end);
}
