//Mod Customization
//Place your scripts in here:

//THIS FILE IS EXECUTED LAST

//it will not interfere with the patches, and thus prevents errors

//If you want to manipulate damage modifiers or datablock stuff, this is how you do it:
//Datablockname.Modifier = new;
//EX: S3Bullet.directDamage = 1.1; //.4 higher

//Add custom chat commands here!
//I've included a little sample command (in comments)
//function parseCustomCommands(%sender, %command, %args) {
//   switch$(strLwr(%command)) {   //CAUTION! Note the strlwr here! that means your command must show in lowercase in each case "": statement
      //case "samplecommand":
      //   %arg1 = getWord(%args, 0);
      //   %arg2 = getWord(%args, 1)
      //   //Do stuff here!
      //   return 1; //return 1; - Command executed correctly\
//      default:
//   }
//}
//make sure you add all of your commands to the list by doing this:
//addCMD("Custom", "sampleCommand", "Usage: /sampleCommand [arg1] [arg2]: Sample command ftw! people see this when they type /cmdHelp sampleCommand.");