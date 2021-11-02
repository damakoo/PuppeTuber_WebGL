using UnityEngine;
using NCMB;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class ModelLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void addmodel_before_feature();
    [DllImport("__Internal")]
    private static extern void addmodel_before_label();
    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);
    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
    [SerializeField] string modelname = "model.txt";
    [SerializeField] int Interval = 100;
    private bool labelLoaded = false;
    private bool featureLoaded = false;

    private void Start()
    {
        labelLoaded = false;
        featureLoaded = false;
        IEnumerator modelload = ModelLoad();
        StartCoroutine(modelload);
    }

    IEnumerator ModelLoad()
    {
        labelLoaded = false;
        featureLoaded = false;
        LabelLoad();
        Featureload();
        while (!labelLoaded && !featureLoaded)
        {
            yield return new WaitForSeconds(0.2f);
        }
        getmodel();
    }

    private void LabelLoad()
    {
        NCMBFile file = new NCMBFile("traindata_label.txt");
        file.FetchAsync((byte[] fileData, NCMBException error) =>
        {
            if (error != null)
            {
                // é∏îs
                LabelLoad();
            }
            else
            {
                // ê¨å˜
                var label_json = System.Text.Encoding.UTF8.GetString(fileData);
                foreach (var child in label_json.SubstringAtCount(Interval))
                {
                    SetLocalStorage("traindata_label", child);
                    addmodel_before_label();
                }
                Debug.Log("label loaded");
                labelLoaded = true;
            }
        });
    }
  
    private void Featureload()
    {
        NCMBFile file = new NCMBFile("traindata_feature.txt");
        file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // é∏îs
                    Featureload();
                }
                else
                {
                    // ê¨å˜
                    var feature_json = System.Text.Encoding.UTF8.GetString(fileData);
                    foreach (var child in feature_json.SubstringAtCount(Interval))
                    {
                        SetLocalStorage("traindata_feature", child);
                        addmodel_before_feature();
                    }
                    Debug.Log("feature loaded");
                    featureLoaded = true;
                }
            });
    }
}

