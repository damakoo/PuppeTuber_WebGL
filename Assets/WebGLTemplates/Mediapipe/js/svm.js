var svm = new SVM({
    kernel: 2, // The type of kernel I want to use
    type: 0,    // The type of SVM I want to run
    gamma: 1,                     // RBF kernel gamma parameter
    cost: 1                       // C_SVC cost parameter
});

// This is the xor problem
//
//  1  0
//  0  1
 const features = [[0, 0], [1, 1], [1, 0], [0, 1]];
 const labels = [0, 0, 1, 1];
 svm.train(features, labels);  // train the model
const predictedLabel = svm.predictOne([0.7, 0.8]);
 console.log(predictedLabel) // 0