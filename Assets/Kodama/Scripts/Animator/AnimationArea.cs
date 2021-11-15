using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationArea : MonoBehaviour
{
    public List<AnimationInfo> animationareaList { get; set; } = new List<AnimationInfo>();
    [SerializeField] Transform ByeBye;
    [SerializeField] Transform Crap;
    [SerializeField] Transform rokuromawashi;
    [SerializeField] Transform peace;
    [SerializeField] Transform covermouse;
    [SerializeField] Transform gutspose;
    public Transform Lefthandpos_default;

    void Start()
    {
        AddAnimation(handState.defaultstate);
        AddAnimation(handState.byebye, ByeBye, true,Regression_Parameter.ByeBye_Right,Regression_Parameter.LeftBase);
        AddAnimation(handState.crap, Crap, false,Regression_Parameter.Crap_Right,Regression_Parameter.Crap_Left,0.25f);
        AddAnimation(handState.Rokuromawashi, rokuromawashi, false,Regression_Parameter.Rokuro_Right,Regression_Parameter.Rokuro_Left,0.4f);
        AddAnimation(handState.nod);
        AddAnimation(handState.peace, peace, true,Regression_Parameter.Peace_Right,Regression_Parameter.LeftBase);
        AddAnimation(handState.bow);
        AddAnimation(handState.covermouse, covermouse, false,Regression_Parameter.CoverMouse_Right,Regression_Parameter.CoverMouse_Left,0.45f);
        AddAnimation(handState.gutspose, gutspose, true,Regression_Parameter.GutsPose_Right,Regression_Parameter.LeftBase);
    }

    void AddAnimation(handState handState, Transform transform,bool _onlyRightHand,Regression_Parent _RightHandRot, Regression_Parent _LeftHandRot, float _handdistance = 0)
    {
        AnimationInfo _animation = new AnimationInfo(handState,transform,_onlyRightHand,_RightHandRot,_LeftHandRot,_handdistance);
        animationareaList.Add(_animation);
    }
    void AddAnimation(handState handState)
    {
        AnimationInfo _animation = new AnimationInfo(handState);
        animationareaList.Add(_animation);
    }
}
