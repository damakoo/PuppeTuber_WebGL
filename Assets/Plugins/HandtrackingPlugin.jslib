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
  },

  train: function(){
    train();
  },

  setmodel: function(){
     setmodel();
  },
  
  getmodel: function(){
    getmodel();
  },
    predict: function(){
    predict();
  },
  predict_before: function(){
    predict_before();
  },
  addmodel: function(){
    addmodel();
  },
  addmodel_before_feature: function(){
    addmodel_before_feature();
  },
  addmodel_before_label:function(){
    addmodel_before_label();
  },
  ResetJson: function(){
    ResetJson();
  },
    addmodel_feature: function(){
    addmodel_feature();
  },
  addmodel_label:function(){
    addmodel_label();
  },
  appearcanvas:function(){
    appearcanvas();
  },
  fadecanvas:function(){
    fadecanvas();
  }
});