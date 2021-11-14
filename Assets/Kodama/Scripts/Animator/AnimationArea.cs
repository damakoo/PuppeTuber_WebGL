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
        AddAnimation(handState.byebye, ByeBye, true);
        AddAnimation(handState.crap, Crap, false,0.1f);
        AddAnimation(handState.Rokuromawashi, rokuromawashi, false,0.3f);
        AddAnimation(handState.nod);
        AddAnimation(handState.peace, peace, true);
        AddAnimation(handState.bow);
        AddAnimation(handState.covermouse, covermouse, false,0.3f);
        AddAnimation(handState.gutspose, gutspose, true);
    }

    void AddAnimation(handState handState, Transform transform,bool _onlyRightHand,float _handdistance = 0)
    {
        AnimationInfo _animation = new AnimationInfo(handState,transform,_onlyRightHand,_handdistance);
        animationareaList.Add(_animation);
    }
    void AddAnimation(handState handState)
    {
        AnimationInfo _animation = new AnimationInfo(handState);
        animationareaList.Add(_animation);
    }
}