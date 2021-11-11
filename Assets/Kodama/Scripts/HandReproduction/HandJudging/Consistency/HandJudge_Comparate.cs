using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class HandJudge_Comparate : MonoBehaviour
{
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] UserStudyAnimator _UserStudyAnimator2;
    [SerializeField] Animator animator;
    [SerializeField] Animator animator_before;
    [SerializeField] AddJointAngle_HR _addJointAngle;
    [SerializeField] Text motionNameLabel1;
    [SerializeField] Text motionNameLabel2;

    private handState _handstate_before = handState.defaultstate;
    private handState _handstate_before2 = handState.defaultstate;
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
            if (_handstate_before != (handState)Enum.ToObject(typeof(handState), judge))
            {
                _UserStudyAnimator._handState = (handState)Enum.ToObject(typeof(handState), judge);
                motionNameLabel1.text = "あなたのモーション:" + Constants.motionNames[(handState)Enum.ToObject(typeof(handState), judge)];
                animator.SetInteger("handstate",judge);
                _handstate_before = _UserStudyAnimator._handState;
            }
            if (_handstate_before2 != (handState)Enum.ToObject(typeof(handState), judge2))
            {
                _UserStudyAnimator2._handState = (handState)Enum.ToObject(typeof(handState), judge2);
                motionNameLabel2.text = "ある人のモーション:" + Constants.motionNames[(handState)Enum.ToObject(typeof(handState), judge2)];
                animator_before.SetInteger("handstate", judge2);
                _handstate_before2 = _UserStudyAnimator2._handState;
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
    private void Predict()
    {
        predict();
        predict_before();
    }
}