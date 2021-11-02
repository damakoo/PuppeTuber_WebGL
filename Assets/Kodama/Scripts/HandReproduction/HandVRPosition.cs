using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVRPosition : MonoBehaviour
{
  public Transform thistransform { get; set; }
  public int HandNum { get; set; }
  HandVRManager handVRManager;

  private void Update()
  {
    Calibration();
  }
  public HandVRPosition Initialize(int _handnum,HandVRManager _handVRManager)
  {
    HandNum = _handnum;
    thistransform = this.gameObject.transform;
    handVRManager = _handVRManager;
    return this;
  }
  private void Calibration()
 {
    thistransform.localPosition = HandReader.HandsList[handVRManager.Frame][HandNum];
    thistransform.localPosition = new Vector3(thistransform.localPosition.x, thistransform.localPosition.y, thistransform.localPosition.z);
  }
}
