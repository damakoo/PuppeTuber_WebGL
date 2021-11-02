using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.Collections;

public class HandReader:MonoBehaviour
{
    public static List<List<Vector3>> HandsList;

    private void Start()
    {
        Read();
    }
    private void Decode(string content)
    {
        HandsList = new List<List<Vector3>>();
        string[] line = content.Trim().Split('\n');
        for (int j = 0; j < line.Length; j++)
        {
            string[] list = line[j].Trim().Split(' ');
            List<Vector3> nowHand = new List<Vector3>();
            for (int i = 0; i < list.Length / 3; i++)
            {
                nowHand.Add(new Vector3(float.Parse(list[3 * i]), -float.Parse(list[3 * i + 1]), float.Parse(list[3 * i + 2])));
            }
            HandsList.Add(nowHand);
        }
    }
    
    public void Read()
    {
        NCMBFile file = new NCMBFile("HandRecord.txt");
        file.FetchAsync((byte[] fileData, NCMBException error) =>
        {
            if (error != null)
            {
                // ���s
                Read();
            }
            else
            {
                // ����
                Decode(System.Text.Encoding.UTF8.GetString(fileData));
                UnityEngine.Debug.Log("HandRead Success");
            }
        });
    }
}
