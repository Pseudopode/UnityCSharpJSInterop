mergeInto(LibraryManager.library, {

  jsHello: function () {
    window.alert("A little hi from Unity side!");
  },
  
  jsUnitySendInfoToBrowser: function(str)
  {
	jsUSendInfo(Pointer_stringify(str));
  },
  
  jsReturnInfoToBrowser: function(str)
  {
	jsUReturnInfo(Pointer_stringify(str));
  },
  
  
  jsStringReturnValueFunction: function () {
    var returnStr = infoSetOnJSSide;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },
   
});