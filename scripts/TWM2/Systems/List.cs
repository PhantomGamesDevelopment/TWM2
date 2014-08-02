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
   if(%this.find(%elementTxt) == -1) {
      %this.addElement(%newValue);
   }
   else {
      %indx = getField(%this.find(%elementTxt), 1);
      %this.set(%indx, %newValue);
   }
}

function ListInstance::set(%this, %index, %new) {
   %this.element[%index] = %new;
}

function ListInstance::addElement(%this, %element) {
   %this.element[%this.numberOfElements] = %element;
   %this.numberOfElements++;
}

function ListInstance::removeElement(%this, %index) {
   if(%index > %this.count() || %index < 0) {
      error("ListInstance::removeElements("@%index@"): Specified index is out of list bounds.");
      return;
   }
   %this.element[%this.numberOfElements] = "";
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
   for(%i = %this.count(); %i >= 0; %i--) {
      if(%this.element[%i] $= "") {
         //Strip item, move others forward
         for(%x = %i; %x < %this.count(); %x++) {
            %this.element[%x] = %this.element[%x+1];
         }
         %this.numberOfElements--;
      }
   }
}
