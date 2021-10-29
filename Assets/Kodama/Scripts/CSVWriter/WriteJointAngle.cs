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
    [SerializeField] UserStudyAnimator _UserStudyAnimator;
    [SerializeField] AddJointAngle _addJointAngle;
    List<JointAngle> JointAngleList => _addJointAngle.JointAngleList;
    private List<List<List<float>>> TrainingList = new List<List<List<float>>>(Enum.GetNames(typeof(handState)).Length);
    private List<List<float>> TrainingSet = new List<List<float>>();
    private List<int> LabelList = new List<int>();

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
                TrainingSet.Add(TrainingList[i][j]);
                LabelList.Add(i);
            }
        }
        SetLocalStorage("traindata_feature",TrainingSetToJson());
        SetLocalStorage("traindata_label", JsonHelper.ToJson(LabelList));
        train();
    }
    public void Savemodel()
    {
        setmodel();
        NCMBfunction.OverWrite("model",GetLocalStorage("model"));
        
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
