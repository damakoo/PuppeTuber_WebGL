using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class HandTrackingValue : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void HandTracking();
    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
    [DllImport("__Internal")]
    private static extern void SetLocalStorage(string key, string value);
    public Vector3[] landmarks { get; set; } = new Vector3[21];
    //public Vector3[] landmarks = new Vector3[21];

    void Update()
    {
        HandTracking();
        var jsonstr = GetLocalStorage("handpos");
        landmarks = JsonHelper.FromJson<Vector3>(jsonstr);
    }
}
