using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAngle_HR
{
  
  private int fingeri;

  private int fingerj;

  private int fingerk;
  HandVRManager handVRManager;
  private JointAngle_HR(HandVRManager _handVRManager,int _fingeri, int _fingerj, int _fingerk)
  {
    handVRManager = _handVRManager;
    this.fingeri = _fingeri;
    this.fingerj = _fingerj;
    this.fingerk = _fingerk;
  }
  private Vector3 fingeripos => HandReader.HandsList[handVRManager.Frame][fingeri];
  private Vector3 fingerjpos => HandReader.HandsList[handVRManager.Frame][fingerj];
  private Vector3 fingerkpos => HandReader.HandsList[handVRManager.Frame][fingerk];

  public float angle()
  {
    Vector3 n1 = fingeripos - fingerjpos;
    Vector3 n2 = fingerkpos - fingerjpos;
    return (float)System.Math.Acos(Vector3.Dot(n1, n2) / n1.magnitude / n2.magnitude);
  }
  public static List<JointAngle_HR> AutomaticCreate(List<int[]> HandList,HandVRManager _handVRManager)
  {
    var jointanglelist = new List<JointAngle_HR>();
    for (int i = 0; i < HandList.Count; i++)
    {
      jointanglelist.Add(new JointAngle_HR(_handVRManager, HandList[i][0], HandList[i][1], HandList[i][2]));
    }
    return jointanglelist;
  }
}
