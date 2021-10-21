using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPosition : MonoBehaviour
{
    public Transform thistransform { get; set; }
    private HandTrackingManager handTrackingmanager;
    public int HandNum { get; set; }
    private Vector3[] landmarks => handTrackingmanager.landmarks;

    private void Update()
    {
        Caliburation();
    }
    public FingerPosition Initialize(int _handnum, HandTrackingManager _handTrackingmanager)
    {
        HandNum = _handnum;
        thistransform = this.gameObject.transform;
        handTrackingmanager = _handTrackingmanager;
        return this;
    }
    private void Caliburation()
    {
        thistransform.localPosition = landmarks[HandNum];
    }
}
