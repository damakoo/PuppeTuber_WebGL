using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FingerLine : MonoBehaviour
{
    private int[] linenumlist;

    private HandTrackingManager handTrackingmanager;
    private List<FingerPosition> fingerpositionlist => handTrackingmanager.fingerpositionlist;
    private LineRenderer lineRenderer;

    public FingerLine Initialize(int[] _linenumlist, HandTrackingManager _handTrackingmanager)
    {
        this.linenumlist = _linenumlist;
        this.handTrackingmanager = _handTrackingmanager;
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        return this;
    }
    private void Update()
    {
        Calibration();
    }

    private void Calibration()
    {
        Vector3[] positions = new Vector3[5];
        for (int i = 0; i < linenumlist.Length; i++)
        {
            positions[i] = fingerpositionlist[linenumlist[i]].thistransform.position;
        }
        lineRenderer.SetPositions(positions);
    }
}
