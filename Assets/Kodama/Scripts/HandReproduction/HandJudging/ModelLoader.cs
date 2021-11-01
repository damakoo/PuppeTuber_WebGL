using UnityEngine;
using NCMB;
using System.Collections;


public class ModelLoader : MonoBehaviour
{
    [SerializeField] string modelname = "model.txt";
    public string model;
    public bool isLoaded = false;

    private void Start()
    {
        //IEnumerator loadmodel = Loadmodel();
        //StartCoroutine(loadmodel);
        Reading(ref model,ref isLoaded);
        Debug.Log("load model : " + model);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("load model : " + model);
            Debug.Log("isLoaded : " + isLoaded.ToString());
        }
    }
    IEnumerator Loadmodel()
    {
        yield return new WaitForSeconds(3);
        //NCMBfunction.Read(modelname, out model);
        while (model == null)
        {
            isLoaded = false;
            Read();
            while (!isLoaded)
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
        Debug.Log(model);
    }
    public void Read()
    {
        NCMBFile file = new NCMBFile("model.txt");
        file.FetchAsync((byte[] fileData, NCMBException error) =>
        {
            if (error != null)
            {
                // é∏îs
                //Read();
                isLoaded = false;
            }
            else
            { 
                // ê¨å˜
                //model = System.Text.Encoding.UTF8.GetString(fileData);
                UnityEngine.Debug.Log("Read Success");
                isLoaded = true;
            }
        });
        UnityEngine.Debug.Log(model.ToString());
    }
    private void Reading(ref string model,ref bool isLoaded)
    {
        string _model = "";
        var _isLoaded = false;
        NCMBFile file = new NCMBFile("HandRecord.txt");
        while (file.FileData == null)
        {
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // é∏îs
                    Reading(ref _model, ref _isLoaded);
                }
                else
                {
                    // ê¨å˜
                    Debug.Log(System.Text.Encoding.UTF8.GetString(fileData));
                }
            });
        }
        Debug.Log("_model : " + _model.ToString());
        Debug.Log("data : " + System.Text.Encoding.UTF8.GetString(file.FileData));
        model = _model;
        isLoaded = _isLoaded;
    }
}

