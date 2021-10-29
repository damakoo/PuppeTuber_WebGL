using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class HandReader:MonoBehaviour
{
    public static List<List<Vector3>> HandsList;
    private void Start()
    {
        HandsList = HandLoading();
    }

    public static List<List<Vector3>> HandLoading()
    {
        var content = NCMBfunction.Read("HandRecord.txt");
        List<List<Vector3>> _handlist = new List<List<Vector3>>();

        string[] line = content.Trim().Split('\n');
        for (int j = 0; j < line.Length; j++)
        {
            string[] list = line[j].Trim().Split(' ');
            List<Vector3> nowHand = new List<Vector3>();
            for (int i = 0; i < list.Length / 3; i++)
            {
                nowHand.Add(new Vector3(float.Parse(list[3 * i]), float.Parse(list[3 * i + 1]), float.Parse(list[3 * i + 2])));
            }
            _handlist.Add(nowHand);
        }
        return _handlist;
    }
}
