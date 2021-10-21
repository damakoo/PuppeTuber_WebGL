mergeInto(LibraryManager.library, {
    HandTracking: function(){
    showresult();
    },
 GetLocalStorage: function(key) {
    var item_string = localStorage.getItem(Pointer_stringify(key));
 
    if (!item_string) {
      return "";
    }
 
    var bufferSize = lengthBytesUTF8(item_string) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(item_string, buffer, bufferSize);
    return buffer;
  },
 
  SetLocalStorage: function(key, user_id) {
    localStorage.setItem(Pointer_stringify(key), Pointer_stringify(user_id));
  }
});