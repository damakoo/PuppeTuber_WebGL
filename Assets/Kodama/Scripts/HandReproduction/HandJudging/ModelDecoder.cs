using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

public class ModelDecoder : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void getmodel();
    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);
    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
    [SerializeField] ModelLoader modelloader;
    private void Start()
    {
        IEnumerator decode = Decodemodel();
        StartCoroutine(decode);
    }
    IEnumerator Decodemodel()
    {
        //NCMBfunction.Read(modelname, out model);
        while (!modelloader.isLoaded)
        {
            yield return new WaitForSeconds(0.2f); 
        }
        SetLocalStorage("model", modelloader.model);
        yield return null;
        getmodel();
        Debug.Log(modelloader.model);
    }
}
