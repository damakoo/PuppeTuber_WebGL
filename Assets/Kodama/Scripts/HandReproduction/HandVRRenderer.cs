using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HandVRRenderer : MonoBehaviour
{
  private int[] linenumlist;
  private HandVRManager handVRManager;
  private List<HandVRPosition> handPositionlist => handVRManager.handpositionlist;
  private LineRenderer lineRenderer;

  public HandVRRenderer Initialize(int[] _linenumlist,HandVRManager _handVRManager)
  {
    this.linenumlist = _linenumlist;
    this.handVRManager = _handVRManager;
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
      positions[i] = handPositionlist[linenumlist[i]].thistransform.position;
    }
    lineRenderer.SetPositions(positions);
  }
}
