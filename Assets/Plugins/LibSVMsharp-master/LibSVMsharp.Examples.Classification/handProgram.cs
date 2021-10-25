using LibSVMsharp.Helpers;
using LibSVMsharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LibSVMsharp.Examples.Classification
{
    class handProgram:MonoBehaviour
    { 
        [SerializeField] private string _filename;
        [SerializeField] private string _testname;
        [SerializeField] private string _modelname;
        private SVMProblem train;
        //[SerializeField] CSVWriter CSVWriter;
        private void Start()
        {
            train = new SVMProblem();
            for (int a = 0; a < 1000; a++)
            {
                List<SVMNode> nodes = new List<SVMNode>();
                for (int i = 0; i < 10; i++)
                {
                    SVMNode node = new SVMNode();
                    node.Index = i + 1;
                    node.Value = (double)UnityEngine.Random.RandomRange(1, 20);
                    nodes.Add(node);
                }
                train.Add(nodes.ToArray(), UnityEngine.Random.Range(0, 5));
            }
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Main();
            }
        }

        private void Main()
        {
            // Set print function for accessing output logs
            // Please do not call this function more than a couple of times since it may cause memory leak !
            //SVM.SetPrintStringFunction(new SVMPrintFunction(PrintFunction));

            //string trainpath = Application.dataPath +"/" +_filename;
            //string testpath = Application.dataPath + "/" + _testname;
            //Debug.Log(trainpath);

            // Load the datasets: In this example I use the same datasets for training and testing which is not suggested
            //SVMProblem trainingSet = SVMProblemHelper.Load(trainpath);
            //SVMProblem testSet = SVMProblemHelper.Load(testpath);
            //Debug.Log(trainingSet != null);

            // Normalize the datasets if you want: L2 Norm => x / ||x||
            //trainingSet = trainingSet.Normalize(SVMNormType.L2);
            //testSet = testSet.Normalize(SVMNormType.L2);

            // Select the parameter set
            SVMParameter parameter = new SVMParameter();
            parameter.Type = SVMType.C_SVC;
            parameter.Kernel = SVMKernelType.RBF;
            parameter.C = 1;
            parameter.Gamma = 1;

            // Do cross validation to check this parameter set is correct for the dataset or not
            //double[] crossValidationResults; // output labels
            //int nFold = 5;
            //trainingSet.CrossValidation(parameter, nFold, out crossValidationResults);

            // Evaluate the cross validation result
            // If it is not good enough, select the parameter set again
            //double crossValidationAccuracy = trainingSet.EvaluateClassificationProblem(crossValidationResults);

            // Train the model, If your parameter set gives good result on cross validation
            SVMModel model = train.Train(parameter);
            Debug.Log(model.ClassCount.ToString());
            // Save the model
            //SVM.SaveModel(model, Application.dataPath + "/"+ _modelname);

            // Predict the instances in the test set
            double[] testResults = train.Predict(model);
            //foreach(var key in testResults)
            //{
            //    CSVWriter.WriteCSV(key.ToString());
            //}
            
            // Evaluate the test results
            //int[,] confusionMatrix;
            //double testAccuracy = testSet.EvaluateClassificationProblem(testResults, model.Labels, out confusionMatrix);

            // Print the resutls
            //Debug.Log("Cross validation accuracy: " + crossValidationAccuracy);
            //Debug.Log("Test accuracy: " + testAccuracy);
            /*Debug.Log("Confusion matrix:");

            // Print formatted confusion matrix
            Console.Write(String.Format("{0,6}", ""));
            for (int i = 0; i < model.Labels.Length; i++)
                Console.Write(String.Format("{0,5}", "(" + model.Labels[i] + ")"));
            Console.WriteLine();
            for (int i = 0; i < confusionMatrix.GetLength(0); i++)
            {
                Console.Write(String.Format("{0,5}", "(" + model.Labels[i] + ")"));
                for (int j = 0; j < confusionMatrix.GetLength(1); j++)
                    Console.Write(String.Format("{0,5}", confusionMatrix[i,j]));
                Console.WriteLine();
            }

            Console.WriteLine("\n\nPress any key to quit...");
            Console.ReadLine();*/
        }
    }
}
