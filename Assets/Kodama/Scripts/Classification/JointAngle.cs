using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAngle
{
    private  HandTrackingValue handTrackingValue;
    private int fingeri;

    private int fingerj;

    private int fingerk;
    private JointAngle(ref HandTrackingValue _handTrackingValue, int _fingeri, int _fingerj, int _fingerk)
    {
        this.handTrackingValue = _handTrackingValue;
        this.fingeri = _fingeri;
        this.fingerj = _fingerj;
        this.fingerk = _fingerk;
    }

    private Vector3 fingeripos =>  handTrackingValue.landmarks[fingeri];
    private Vector3 fingerjpos => handTrackingValue.landmarks[fingerj];
    private Vector3 fingerkpos => handTrackingValue.landmarks[fingerk];

    public float angle()
    {
        Vector3 n1 = fingeripos - fingerjpos;
        Vector3 n2 = fingerkpos - fingerjpos;
        return (float)System.Math.Acos(Vector3.Dot(n1, n2) / n1.magnitude / n2.magnitude);
    }
    public static List<JointAngle> AutomaticCreate(List<int[]> HandList, HandTrackingValue _handTrackingValue)
    {
        var jointanglelist = new List<JointAngle>(); 
        for (int i = 0;i<HandList.Count;i++)
        {
            jointanglelist.Add(new JointAngle(ref _handTrackingValue, HandList[i][0], HandList[i][1], HandList[i][2]));
        }
        return jointanglelist;
    }
}
