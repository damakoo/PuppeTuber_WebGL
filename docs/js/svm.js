var svm = new SVM({
    kernel: 2, // The type of kernel I want to use
    type: 0,    // The type of SVM I want to run
    gamma: 1,                     // RBF kernel gamma parameter
    cost: 1                       // C_SVC cost parameter
});
var svm_before = new SVM({
    kernel: 2, // The type of kernel I want to use
    type: 0,    // The type of SVM I want to run
    gamma: 1,                     // RBF kernel gamma parameter
    cost: 1                       // C_SVC cost parameter
});
var feature_json_before = "";
var label_json_before = "";
var feature_json = "";
var label_json = "";
function train() {
    // var feature_json = localStorage.getItem("traindata_feature");
    // var labels_json = localStorage.getItem("traindata_label");
    // var feature = JSON.parse(feature_json);
    // var labels = JSON.parse(labels_json);
    console.log(label_json);
    console.log(feature_json);
    features = JSON.parse(feature_json);
    labels = JSON.parse(label_json);
    svm.train(features, labels);
}
function ResetJson() {
    feature_json = "";
    label_json = "";
    feature_json_before = "";
    label_json_before = "";
}
function setmodel() {
    localStorage.setItem("model", JSON.stringify(svm));
}
function getmodel() {
    console.log(feature_json_before);
    console.log(label_json_before);
    feature_before = JSON.parse(feature_json_before);
    label_before = JSON.parse(label_json_before);
    svm_before.train(feature_before,label_before);
}
function addmodel_before_feature() {
    feature_json_before += localStorage.getItem("traindata_feature");
}
function addmodel_before_label(){
    label_json_before += localStorage.getItem("traindata_label");
}
function addmodel_feature() {
    feature_json += localStorage.getItem("traindata_feature");
}
function addmodel_label(){
    label_json += localStorage.getItem("traindata_label");
}
function addmodel(){
    var feature_json = localStorage.getItem("traindata_feature");
    var labels_json = localStorage.getItem("traindata_label");
    features = features.concat(JSON.parse(feature_json));
    labels = labels.concat(JSON.parse(labels_json));
    localStorage.setItem("Received",JSON.stringify(true));
}
function predict() {
    var animation = {0:0, 1: 0, 2: 0, 3: 0, 4: 0, 5: 0, 6: 0, 7: 0, 8: 0, 9: 0, 10: 0, 11: 0 }
    var input_json = localStorage.getItem("Input");
    var input = JSON.parse(input_json);
    input.forEach(element => {
        var result = svm.predictOne(element);
        animation[String(result)] += 1;
    });
    var maxcount = 0;
    var predictedLabel;
    for (var key in animation) {
        if (maxcount < animation[key]) {
            predictedLabel = key;
            maxcount = animation[key];
        }
    }
    localStorage.setItem("predictLabel", predictedLabel);
}
function predict_before() {
    var animation = {0:0, 1: 0, 2: 0, 3: 0, 4: 0, 5: 0, 6: 0, 7: 0, 8: 0, 9: 0, 10: 0, 11: 0 }
    var input_json = localStorage.getItem("Input");
    var input = JSON.parse(input_json);
    input.forEach(element => {
        var result = svm_before.predictOne(element);
        animation[String(result)] += 1;
    });
    var maxcount = 0;
    var predictedLabel;
    for (var key in animation) {
        if (maxcount < animation[key]) {
            predictedLabel = key;
            maxcount = animation[key];
        }
    }
    localStorage.setItem("predictLabel_before", predictedLabel);
}
