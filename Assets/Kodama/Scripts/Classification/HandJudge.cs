using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

class HandJudge : MonoBehaviour
{
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] HandTrackingValue handTrackingValue;
    private string judge_json;
    private int judge;
    [DllImport("__Internal")]
    private static extern void train();
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void predict();

    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);

    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);

    List<List<float>> input = new List<List<float>>();
    [SerializeField] private float interval = 1.0f;
    private float _time = 0;
    [SerializeField]AddJointAngle _addJointAngle;
    [SerializeField] Text motionNameLabel;
    List<JointAngle> JointAngleList => _addJointAngle.JointAngleList;
    private handState _handstate_before = handState.defaultstate;


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
            if(_handstate_before != (handState)Enum.ToObject(typeof(handState), judge))
            {
                _UserStudyAnimator._handState = (handState)Enum.ToObject(typeof(handState), judge);
                motionNameLabel.text = "åüèoíÜÉÇÅ[ÉVÉáÉì:" + Constants.motionNames[(handState)Enum.ToObject(typeof(handState), judge)];
                _handstate_before = _UserStudyAnimator._handState;
            }
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