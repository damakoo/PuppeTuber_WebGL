
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


function train() {
    var feature_json = localStorage.getItem("traindata_feature");
    var labels_json = localStorage.getItem("traindata_label");
    var feature = JSON.parse(feature_json);
    var labels = JSON.parse(labels_json);
    svm.train(feature, labels);
}

function setmodel() {
    localStorage.setItem("model", JSON.stringify(svm));
}
function getmodel() {
    feature_before = JSON.parse(localStorage.getItem("traindata_feature"));
    label_before = JSON.parse(localStorage.getItem("traindata_label"))
    svm_before.train(feature_before,label_before);
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
