using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using NCMB;

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
    [DllImport("__Internal")]
    private static extern void addmodel_label();
    [DllImport("__Internal")]
    private static extern void addmodel_feature();
    [DllImport("__Internal")]
    private static extern void ResetJson();
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] AddJointAngle _addJointAngle;
    [SerializeField] int Interval = 300;
    List<JointAngle> JointAngleList => _addJointAngle.JointAngleList;
    private List<List<List<float>>> TrainingList = new List<List<List<float>>>();
    private List<List<float>> TrainingSet = new List<List<float>>();
    private List<int> LabelList = new List<int>();
    [SerializeField] SVMmanager SVMmanager;

    
    private void Awake()
    {
        for (int i = 0; i < Enum.GetNames(typeof(handState)).Length; i++) TrainingList.Add(new List<List<float>>());
        ResetJson();
    }
    public void AddTraingdata(int animation)
    {
        TrainingList[animation].Add(makenodes());
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
        calculate();       
    }
    private void calculate()
    {
        ResetJson();
        SetLocalStorage("traindata_label", "");
        SetLocalStorage("traindata_feature", "");
        var ALLLabelList_json = JsonHelper.ToJson(LabelList);
        var ALLtrainingset_json = ALLTrainingSetToJson();
        NCMBfunction.OverWrite("traindata_label.txt", ALLLabelList_json);
        NCMBfunction.OverWrite("traindata_feature.txt", ALLtrainingset_json);
        setlabel(ALLLabelList_json);
        setfeature(ALLtrainingset_json);
        train();
        SetLocalStorage("traindata_label", "");
        SetLocalStorage("traindata_feature", "");
    }

    private string ALLTrainingSetToJson()
    {
        string trainingset_json = "[";
        for (int j = 0; j < TrainingSet.Count; j++)
        {
            trainingset_json += JsonHelper.ToJson(TrainingSet[j]) + ",";
        }
        trainingset_json = trainingset_json.Remove(trainingset_json.Length - 1);
        trainingset_json += "]";
        return trainingset_json;
    }
    private void setfeature(string feature_json)
    {
        foreach (var child in feature_json.SubstringAtCount(Interval))
        {
            SetLocalStorage("traindata_feature", child);
            addmodel_feature();
        }
        Debug.Log("feature saved");
    }
    private void setlabel(string label_json)
    {
        foreach (var child in label_json.SubstringAtCount(Interval))
        {
            SetLocalStorage("traindata_label", child);
            addmodel_label();
        }
        Debug.Log("label saved");
    }
}
