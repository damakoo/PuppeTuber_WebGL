using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WriteData : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveData(string fileName, string Data);
    // Start is called before the first frame update
    void Start()
    {
        SaveData("result.json",System.DateTime.Now.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
