using UnityEngine;
using NCMB;
using System.Collections;
using System.Runtime.InteropServices;


public class ModelLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);
    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
    [SerializeField] string modelname = "model.txt";

    private void Start()
    {
        IEnumerator modelload = ModelLoad();
        StartCoroutine(modelload);
    }
    IEnumerator ModelLoad()
    {
        LabelLoad();
        Featureload();
        while(GetLocalStorage("traindata_label") == null || GetLocalStorage("traindata_feature") == null)
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
                SetLocalStorage("traindata_label", System.Text.Encoding.UTF8.GetString(fileData));
                Debug.Log("label loaded");
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
                    SetLocalStorage("traindata_feature", System.Text.Encoding.UTF8.GetString(fileData));
                    Debug.Log("feature loaded");
                }
            });
    }
}

