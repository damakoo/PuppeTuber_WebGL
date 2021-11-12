using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public static Dictionary<int, Shogo> title = new Dictionary<int, Shogo>()
    {
        {0,new Shogo("ザ・たっち","10cm") },
        {1,new Shogo("みかみなおみ","a") },
        {2,new Shogo("あらまきみなみ" ,"b")},
        {3,new Shogo("友達以上恋人未満" ,"c")},
        {4,new Shogo("こだまだいき","d") },
        {5,new Shogo("はただゆうじ","e") },
        {6,new Shogo("スカイツリー","634m") },
        {7,new Shogo("東京-名古屋","352.1km") },
        {8,new Shogo("月とスッポン","384400km" )},
        {9,new Shogo("アンドロメダ","2537000光年") },
        {10,new Shogo("こだまとあらまき" ,"パラレルワールド")}
    };
}
