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
        private SVMProblem train;
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
            // Select the parameter set
            SVMParameter parameter = new SVMParameter();
            parameter.Type = SVMType.C_SVC;
            parameter.Kernel = SVMKernelType.RBF;
            parameter.C = 1;
            parameter.Gamma = 1;

            SVMModel model = train.Train(parameter);
            Debug.Log(model.ClassCount.ToString());

            double[] testResults = train.Predict(model);
        }
    }
}
