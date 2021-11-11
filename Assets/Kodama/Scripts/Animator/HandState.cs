using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum handState
{
    defaultstate = 0,
    byebye = 1,
    crap = 2,
    Rokuromawashi = 3,
    nod = 4,
    peace = 5,
    bow = 6,
    covermouse = 7,
    gutspose = 8,
    cry = 9,
    hai = 10,
    holdhead = 11,
    demo = 12,
}
public class Constants
{
    public static Dictionary<handState,string> motionNames = new Dictionary<handState, string>{
        {handState.defaultstate, "デフォルト" },
        {handState.byebye, "バイバイ" },
        {handState.crap, "拍手" },
        {handState.Rokuromawashi, "ろくろ回し" },
        {handState.nod, "うなずく" },
        {handState.peace, "ピース" },
        {handState.bow, "お辞儀" },
        {handState.covermouse, "ハッとする" },
        {handState.gutspose, "ガッツポーズ" },
        {handState.cry, "泣く" },
        {handState.hai, "挙手" },
        {handState.holdhead, "頭を抱える" },
        {handState.demo, "デモ" }
    };
}
