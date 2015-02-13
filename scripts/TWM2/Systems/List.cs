// List.cs
// Phantom139
// TWM2 3.9
// Declares a simple container system for list properties.

function initList() {
   %list = new ScriptObject() {
      class = "ListInstance";
      numberOfElements = 0;
   };
   return %list;
}

function ListInstance::advancedAdd(%this, %elementTxt, %newValue) {
<<<<<<< HEAD
   if(%this.find(%elementTxt) == -1) {
      %this.addElement(%newValue);
   }
   else {
=======
   echo("AdvancedAdd("@%this@", "@%elementTxt@", "@%newValue@")");
   if(%this.find(%elementTxt) == -1) {
      echo("AdvancedAdd: Add New");
      %this.addElement(%newValue);
   }
   else {
      echo("AdvancedAdd: Replace Old");
>>>>>>> origin/development
      %indx = getField(%this.find(%elementTxt), 1);
      %this.set(%indx, %newValue);
   }
}

function ListInstance::set(%this, %index, %new) {
   %this.element[%index] = %new;
}

function ListInstance::addElement(%this, %element) {
<<<<<<< HEAD
=======
   echo("Add "@%element@" => "@%this.numberOfElements);
>>>>>>> origin/development
   %this.element[%this.numberOfElements] = %element;
   %this.numberOfElements++;
}

function ListInstance::removeElement(%this, %index) {
   if(%index > %this.count() || %index < 0) {
      error("ListInstance::removeElements("@%index@"): Specified index is out of list bounds.");
      return;
   }
<<<<<<< HEAD
   %this.element[%this.numberOfElements] = "";
=======
   echo(%this@".removeElement("@%index@"): Strip "@%this.element[%index]);
   %this.element[%index] = "";
>>>>>>> origin/development
   %this.compactList();
}

function ListInstance::element(%this, %index) {
   return %this.element[%index];
}

function ListInstance::count(%this) {
   return %this.numberOfElements;
}

function ListInstance::find(%this, %key) {
   for(%i = %this.count(); %i >= 0; %i--) {
      if(strstr(%this.element[%i], %key) == 0) {
         return %this.element[%i] TAB %i;
      }
   }
   return -1;
}

function ListInstance::compactList(%this) {
<<<<<<< HEAD
   for(%i = %this.count(); %i >= 0; %i--) {
      if(%this.element[%i] $= "") {
         //Strip item, move others forward
         for(%x = %i; %x < %this.count(); %x++) {
            %this.element[%x] = %this.element[%x+1];
         }
=======
   echo("Compact "@%this@", "@%this.count());
   for(%i = %this.count(); %i >= 0; %i--) {
      echo("Test "@%i@": "@%this.element[%i]);
      if(%this.element[%i] $= "") {
         echo("Remove Element "@%i);
         //Strip item, move others forward
         for(%x = %i; %x < %this.count(); %x++) {
            echo(%x@" Is Now: "@%this.element[%x+1]);
            %this.element[%x] = %this.element[%x+1];
         }
         //Remove the last item....
         %this.element[%this.numberOfElements] = "";
         echo("Subduct "@%this.numberOfElements);
>>>>>>> origin/development
         %this.numberOfElements--;
      }
   }
}
