using System.Collections.Generic;
using UnityEngine;
using System;
using NCMB;

public class HandsRecorder : MonoBehaviour
{
    [SerializeField] HandTrackingValue handTrackingValue;
    private List<string> PositionList = new List<string>();
    private void Start()
    {
        for(int i = 0;i< Enum.GetNames(typeof(handState)).Length; i++) PositionList.Add("");
    }
    public void Recording(int animation)
    {
        for (int i = 0; i < handTrackingValue.landmarks.Length; i++)
      {
        PositionList[animation] += handTrackingValue.landmarks[i].x.ToString() + " " + handTrackingValue.landmarks[i].y.ToString() + " " + handTrackingValue.landmarks[i].z.ToString() + " ";
      }
        PositionList[animation] += "\n";
        System.Console.WriteLine(animation);
    }
    public void SendRecordingData()
    {
        var textToRecord = "";
        for(int i = 0; i < PositionList.Count; i++)
        {
            textToRecord += PositionList[i];
        }
        NCMBfunction.OverWrite("HandRecord.txt",textToRecord);
    }
}
