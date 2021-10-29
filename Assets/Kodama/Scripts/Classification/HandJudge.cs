using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

class HandJudge : MonoBehaviour
{
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] HandTrackingValue handTrackingValue;
    private string judge_json;
    private int judge;
    [DllImport("__Internal")]
    private static extern void train();
    [DllImport("__Internal")]
    private static extern void setmodel();
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void predict();

    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);

    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);

    List<List<float>> input = new List<List<float>>();
    [SerializeField] private float interval = 0.5f;
    private float _time = 0;
    double[] testResults;
    private int numberofanimation;
    AddJointAngle _addJointAngle;
    List<JointAngle> JointAngleList => _addJointAngle.JointAngleList;

    private void Start()
    {
        numberofanimation = Enum.GetNames(typeof(handState)).Length;
    }

    public void JudgingHand()
    {
        _time += Time.deltaTime;
        if (_time < interval)
        {
            List<float> nodes = new List<float>();
            for (int i = 0; i < JointAngleList.Count; i++)
            {
                nodes.Add(JointAngleList[i].angle());
            }
            nodes.Add(_addJointAngle.handdirection.x);
            nodes.Add(_addJointAngle.handdirection.y);
            nodes.Add(_addJointAngle.handdirection.z);
            input.Add(nodes);
        }
        else
        {
            SetInput();
            predict();
            judge_json = GetLocalStorage("predictLabel");
            judge = int.Parse(judge_json);
            _time = 0;
            input = new List<List<float>>();
            _UserStudyAnimator._handState = (handState)Enum.ToObject(typeof(handState), judge);
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
}