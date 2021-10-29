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
    Caliburation();
  }
  public HandVRPosition Initialize(int _handnum,HandVRManager _handVRManager)
  {
    HandNum = _handnum;
    thistransform = this.gameObject.transform;
    handVRManager = _handVRManager;
    return this;
  }
  private void Caliburation()
 {
    thistransform.localPosition = HandReader.HandsList[handVRManager.Frame][HandNum];
  }
}
