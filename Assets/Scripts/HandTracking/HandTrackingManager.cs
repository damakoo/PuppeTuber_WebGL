using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingManager : MonoBehaviour
{
    [SerializeField] HandTrackingValue handTrackingValue;
    public Vector3[] landmarks => handTrackingValue.landmarks;
    public List<FingerPosition> fingerpositionlist { get; set; } = new List<FingerPosition>();
    private void Start()
    {
        HandReproduction();
    }
    private void HandReproduction()
    {
        int linenum = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "Finger")
            {
                FingerPosition _FingerPosition = transform.GetChild(i).gameObject.AddComponent<FingerPosition>();
                _FingerPosition.Initialize(i, this);
                fingerpositionlist.Add(_FingerPosition);
            }
            else if (transform.GetChild(i).gameObject.tag == "FingerLine")
            {
                FingerLine _Fingerline = transform.GetChild(i).gameObject.AddComponent<FingerLine>();
                _Fingerline.Initialize(HandTables.LineRendererTable[i % 5], this);
                linenum += 1;
            }
        }
    }
}
