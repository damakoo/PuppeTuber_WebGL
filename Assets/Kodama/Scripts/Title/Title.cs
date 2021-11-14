using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public static Dictionary<int, Shogo> title = new Dictionary<int, Shogo>()
    {
        {0,new Shogo("ザ・たっち","0.3cm") },//ザ・たっちの身長差
        {1,new Shogo("身体近傍空間","20cm") },
        {2,new Shogo("友達以上恋人未満" ,"1m")},
        {3,new Shogo("オンラインで仲良い人とのキョリ" ,"3m")},
        {4,new Shogo("初対面のキョリ","5m") },
        {5,new Shogo("売れない地下アイドルとのキョリ","10m") },
        {6,new Shogo("スカイツリー","634m") },
        {7,new Shogo("東京-名古屋間キョリ","352.1km") },
        {8,new Shogo("月とスッポン","384400km" )},//地球と月の距離
        {9,new Shogo("アンドロメダ","2537000光年") },//アンドロメダ銀河との距離
        {10,new Shogo("パラレルワールド" ,"∞")}
    };
}
