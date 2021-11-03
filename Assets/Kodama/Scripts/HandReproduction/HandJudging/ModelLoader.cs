using UnityEngine;
using NCMB;
using System.Collections;
using System.Runtime.InteropServices;

public class ModelLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void ResetJson();
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
    public static bool isvalidlabel = true;
    public static bool isvalidfeature = true;
    public static bool isfinished = false;

    private void Start()
    {
        labelLoaded = false;
        featureLoaded = false;
        IEnumerator modelload = ModelLoad();
        StartCoroutine(modelload);
    }

    IEnumerator ModelLoad()
    {
        ResetJson();
        labelLoaded = false;
        featureLoaded = false;
        LabelLoad();
        Featureload();
        while (!labelLoaded || !featureLoaded)
        {
            yield return new WaitForSeconds(0.2f);
        }
        getmodel();
        isfinished = true;
        while (!isfinished || !HandReader.isLoaded)
        {
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log("json content : " + GetLocalStorage("isValidmodel"));
        Debug.Log("model : " + (GetLocalStorage("isValidmodel") == "valid"));
        Debug.Log("hands : " + HandReader.isvalidvalue);
        Debug.Log("feature : " + isvalidfeature);
        Debug.Log("label : " + isvalidlabel);
        if (!HandReader.isvalidvalue || !isvalidfeature || !isvalidlabel || GetLocalStorage("isValidmodel") == "invalid")
        {
            ResetJson();
            HandReader.isLoaded = false;
            HandReader.Read_backup();
            labelLoaded = false;
            featureLoaded = false;
            LabelLoad_backup();
            Featureload_backup();
            while (!labelLoaded || !featureLoaded)
            {
                yield return new WaitForSeconds(0.2f);
            }
            getmodel();
        }
    }

    private void LabelLoad(int i = 0)
    {
        if (!labelLoaded)
        {
            NCMBFile file = new NCMBFile("traindata_label.txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // é∏îs
                    if (i < 5)
                    {
                        Debug.Log("label : " + i);
                        LabelLoad(i + 1);
                    }
                    else
                    {
                        isvalidlabel = false;
                        labelLoaded = true;
                    }
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
    }
    private void LabelLoad_backup()
    {
        if (!labelLoaded)
        {
            NCMBFile file = new NCMBFile("traindata_label_backup.txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // é∏îs
                    LabelLoad_backup();
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
                    Debug.Log("label_backup loaded");
                    labelLoaded = true;
                }
            });
        }
    }

    private void Featureload(int i = 0)
    {
        if (!featureLoaded)
        {
            NCMBFile file = new NCMBFile("traindata_feature.txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
                {
                    if (error != null)
                    {
                        // é∏îs
                        if(i < 5)
                        {
                            Debug.Log("feature : " + i);
                            Featureload(i+1);
                        }
                        else
                        {
                            isvalidfeature = false;
                            featureLoaded = true;
                        }
                        
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
    private void Featureload_backup()
    {
        if (!featureLoaded)
        {
            NCMBFile file = new NCMBFile("traindata_feature_backup.txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // é∏îs
                    Featureload_backup();
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
                    Debug.Log("feature_backup loaded");
                    featureLoaded = true;
                }
            });
        }
    }
}

