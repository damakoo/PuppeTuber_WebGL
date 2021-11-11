using System.Collections.Generic;
using UnityEngine;

public class AnimRotTables
{
    public static Dictionary<handState, Quaternion> HandRotRightLocal = new Dictionary<handState, Quaternion>()
    {
 {handState.byebye,new Quaternion(-0.12f,0.04f,-0.06f,0.98f).normalized },
{handState.crap,new Quaternion(-0.03f,-0.17f,-0.03f,0.98f).normalized },
{handState.Rokuromawashi,new Quaternion(0.34f,-0.28f,-0.12f,0.88f).normalized },
{handState.peace,new Quaternion(-0.27f,-0.013f,-0.074f,0.958f).normalized},
        //{handState.covermouse,new Quaternion(0.18f,-0.33f,-0.088f,0.92f).normalized },
        {handState.covermouse,new Quaternion(-0.00166f,-0.085f,0.0005f,0.996f) },
{handState.gutspose,new Quaternion(0.156f,-0.237f,-0.183f,0.94f).normalized },
    };

    public static Dictionary<handState, Quaternion> HandRotLeftLocal = new Dictionary<handState, Quaternion>()
    {
{handState.byebye,new Quaternion(-0.285f,0.062f,-0.035f,0.956f).normalized },
{handState.crap,new Quaternion(-0.16f,-0.055f,-0.144f,0.973f).normalized },
{handState.Rokuromawashi,new Quaternion(-0.422f,0.118f,-0.094f,0.88f).normalized },
{handState.peace,new Quaternion(-0.157f,0.0035f,0.059f,0.985f).normalized},
        //{handState.covermouse,new Quaternion(-0.35f,-0.074f,-0.09f,0.928f).normalized },
        { handState.covermouse,new Quaternion(-0.10f,-0.022f,-0.0013f,0.994f).normalized },
{handState.gutspose,new Quaternion(-0.17f,-0.02f,-0.019f,0.985f).normalized },
    };
}
