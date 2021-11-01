using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using UnityEngine;

class HandJudge_Comparate : MonoBehaviour
{
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] UserStudyAnimator _UserStudyAnimator2;
    [SerializeField] AddJointAngle_HR _addJointAngle;
    List<float> nodes => _addJointAngle.node;
    private int judge;
    private string judge_json;
    private int judge2;
    private string judge_json2;
    [SerializeField] private float interval = 0.5f;
    private float _time = 0;
    double[] testResults;
    private int numberofanimation;
    [DllImport("__Internal")]
    private static extern void train();
    [DllImport("__Internal")]
    private static extern void setmodel();
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void predict_before();
    [DllImport("__Internal")]
    private static extern void predict();

    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);

    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
    List<List<float>> input = new List<List<float>>();

    private void Start()
    {
        numberofanimation = Enum.GetNames(typeof(handState)).Length;
    }

    public void JudgingHand()
    {
        _time += Time.deltaTime;
        if (_time < interval)
        {
            input.Add(nodes);
        }
        else
        {
            SetInput();
            Predict();
            judge_json = GetLocalStorage("predictLabel");
            judge = int.Parse(judge_json);
            judge_json2 = GetLocalStorage("predictLabel_before");
            judge2 = int.Parse(judge_json2);
            _time = 0;
            input = new List<List<float>>();
            _UserStudyAnimator._handState = (handState)Enum.ToObject(typeof(handState), judge);
            _UserStudyAnimator2._handState = (handState)Enum.ToObject(typeof(handState), judge2);
        }
    }
    private void SetInput()
    {
        var input_json = "[";
        for (int i = 0; i < input.Count; i++)
        {
            input_json += JsonHelper.ToJson(input[i]) + ",";
        }
        input_json = input_json.Remove(input_json.Length - 1);
        input_json += "]";
        SetLocalStorage("Input", input_json);
    }
    private void Predict()
    {
        predict();
        predict_before();
    }
}