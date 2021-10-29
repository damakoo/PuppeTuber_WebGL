using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJointAngle : MonoBehaviour
{
    public List<JointAngle> JointAngleList;
    [SerializeField] HandTrackingValue handTrackingValue;
    public Vector3 handdirection { get; set; }
    void Start()
    {
        JointAngleList = JointAngle.AutomaticCreate(JointAngleTable.JointAngleConstantTable, handTrackingValue);
    }

    void Update()
    {
        handdirection = (handTrackingValue.landmarks[17] - handTrackingValue.landmarks[1]).normalized;
    }
}
