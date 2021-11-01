using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using NCMB;
using System.Collections;

public class WriteJointAngle : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void train();
    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);
    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
    [DllImport("__Internal")]
    private static extern void setmodel();
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] AddJointAngle _addJointAngle;
    List<JointAngle> JointAngleList => _addJointAngle.JointAngleList;
    private List<List<List<float>>> TrainingList = new List<List<List<float>>>();
    private List<List<float>> TrainingSet = new List<List<float>>();
    private List<int> LabelList = new List<int>();
    [SerializeField] SVMmanager SVMmanager;
    
    private void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(handState)).Length; i++) TrainingList.Add(new List<List<float>>());
    }
    public void AddTraingdata(int animation)
    {
        TrainingList[animation].Add(makenodes());
        Debug.Log("animation" + animation.ToString());
    }
    private List<float> makenodes()
    {
        List<float> nodes = new List<float>();
        for (int i = 0; i < JointAngleList.Count; i++)
        {
            nodes.Add(JointAngleList[i].angle());
        }
        nodes.Add(_addJointAngle.handdirection.x);
        nodes.Add(_addJointAngle.handdirection.y);
        nodes.Add(_addJointAngle.handdirection.z);
        return nodes;
    }
    public void Calculatemodel()
    {
        TrainingSet = new List<List<float>>();
        LabelList = new List<int>();
        for (int i = 0; i < TrainingList.Count; i++)
        {
            for (int j = 0; j < TrainingList[i].Count; j++)
            {
                if(TrainingList[i][j].Count == 18)
                {
                    TrainingSet.Add(TrainingList[i][j]);
                    LabelList.Add(i);
                }
            }
        }
        IEnumerator calculate = _calculate();
        StartCoroutine(calculate);       
    }
    IEnumerator _calculate()
    {
        var LabelList_json = JsonHelper.ToJson(LabelList);
        SetLocalStorage("traindata_label", LabelList_json);
        NCMBfunction.OverWrite("traindata_label.txt", LabelList_json);
        var trainingset_json = TrainingSetToJson();
        SetLocalStorage("traindata_feature", trainingset_json);
        NCMBfunction.OverWrite("traindata_feature.txt", trainingset_json);
        yield return null;
        train();
        yield return null;
        SVMmanager.SetCalculatedUI();
        SVMmanager.StartWaitingforOutput();
    }
    public void Savemodel()
    {
        setmodel();
        NCMBfunction.OverWrite("model.txt",GetLocalStorage("model"));
    }
    private string TrainingSetToJson()
    {
        string trainingset_json = "[";
        for (int i = 0; i < TrainingSet.Count; i++)
        {
            trainingset_json += JsonHelper.ToJson(TrainingSet[i]) + ",";
        }
        trainingset_json = trainingset_json.Remove(trainingset_json.Length - 1);
        trainingset_json += "]";
        return trainingset_json;
    }
}
