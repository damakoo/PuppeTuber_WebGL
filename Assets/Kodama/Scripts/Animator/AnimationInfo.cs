using UnityEngine;
using System.Collections.Generic;
public class AnimationInfo
{
    public handState handState { get; }
    public bool useAnimation { get; }
    public Transform rangeTransform { get; }
    public Vector3 minRange
    {
        get
        {
            return rangeTransform.position -  rangeTransform.lossyScale / 2f;
        }
    }
    public Vector3 maxRange
    {
        get
        {
            return rangeTransform.position + rangeTransform.lossyScale / 2f;
        }
    }
    public int Animlength
    {
        get
        {
            return HumanPoseAnimList.Count;
        }
    }
    public List<List<float>> HumanPoseAnimList
    {
        get
        {
            return AnimationLoader.HumanPoseAnimList[(int)handState];
        }
    }
    public Quaternion RightLocalRot
    {
        get
        {
            return AnimRotTables.HandRotRightLocal[handState];
        }
    }
    public Quaternion LeftLocalRot
    {
        get
        {
            return AnimRotTables.HandRotLeftLocal[handState];
        }
    }
    
    public AnimationInfo (handState _handState,Transform _rangeTransform,bool _onlyRightHand,float _handdistance = 0)
    {
        this.handState = _handState;
        this.useAnimation = false;
        this.rangeTransform = _rangeTransform;
        this.onlyRightHand = _onlyRightHand;
        this.HandDistance = _handdistance;
    }
    public AnimationInfo(handState _handState)
    {
        this.handState = _handState;
        this.useAnimation = true;
    }
    public float HandDistance { get; }
    public Vector3 handAVE { get; set; } = new Vector3(0.5f,0.5f,3);
    public Vector3 handSD { get; set; } = new Vector3(0.25f,0.25f,1.5f);
    public bool onlyRightHand { get; set; }
    public Vector3 handminRange
    {
        get
        {
            return handAVE - 2 * handSD;
        }
    }
    public Vector3 handmaxRange
    {
        get
        {
            return handAVE + 2 * handSD;
        }
    }
}
