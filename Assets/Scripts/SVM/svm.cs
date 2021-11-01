using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class svm : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void train();
    [DllImport("__Internal")]
    private static extern void setmodel();
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void predict();

    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key,string json);

    List<List<float>> features = new List<List<float>>{ new List<float> { 0, 0 }, new List<float> { 0.1f, 0.21f }, new List<float> { 1.3f, 1.4f }, new List<float> { 1.2f, 1.6f }, new List<float> { 2.4f, 2.2f } };
    int[] labels = new int[] { 0, 0, 1, 1 ,2};
    List<List<float>> input = new List<List<float>> { new List<float> { 0.7f, 0.8f }, new List<float> { 0.8f, 1.3f }, new List<float> { 2.1f, 0 }};
    string features_json;
    string labels_json;
    string input_json;
    [SerializeField] HandReader handreader;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //features = 
            features_json = "[";
            input_json = "[";
            for (int i = 0; i < features.Count; i++)
            {
                features_json += JsonHelper.ToJson(features[i]) + ",";
            }
            for (int i = 0; i < input.Count; i++)
            {
                input_json += JsonHelper.ToJson(input[i]) + ",";
            }
            features_json = features_json.Remove(features_json.Length - 1);
            input_json = input_json.Remove(input_json.Length - 1);
            features_json += "]";
            input_json += "]";
            labels_json = JsonHelper.ToJson(labels); ;
            SetLocalStorage("traindata_feature", features_json);
            SetLocalStorage("traindata_label", labels_json);
            SetLocalStorage("Input", input_json);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            train();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            setmodel();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            getmodel();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            predict();
        }
    }
}
