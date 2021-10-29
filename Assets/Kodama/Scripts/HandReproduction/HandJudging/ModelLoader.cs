using UnityEngine;
using System.Runtime.InteropServices;
using NCMB;


public class ModelLoader : MonoBehaviour
{
    [SerializeField] string modelname = "model.txt";
    private string model;

    [DllImport("__Internal")]
    private static extern void setmodel();
    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string json);

    private void Start()
    {
        model = NCMBfunction.Read(modelname);
        SetLocalStorage("model", model);
        setmodel();
    }
}

