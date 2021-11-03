using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using NCMB;

public class HandReader : MonoBehaviour
{
    public static List<List<Vector3>> HandsList;
    public static bool isLoaded = false;
    public static bool isvalidvalue = true;
    public static bool isfinished = false;

    private void Start()
    {
        isLoaded = false;
        Read();
    }
    public static void Decode(string content)
    {
        HandsList = new List<List<Vector3>>();
        string[] line = content.Trim().Split('\n');
        for (int j = 0; j < line.Length; j++)
        {
            string[] list = line[j].Trim().Split(' ');
            List<Vector3> nowHand = new List<Vector3>();
            for (int i = 0; i < list.Length / 3; i++)
            {
                var hasX = float.TryParse(list[3 * i], out var X);
                var hasY = float.TryParse(list[3 * i + 1], out var Y);
                var hasZ = float.TryParse(list[3 * i + 2], out var Z);
                if(hasX && hasY && hasZ)
                {
                    nowHand.Add(new Vector3(float.Parse(list[3 * i]), float.Parse(list[3 * i + 1]), float.Parse(list[3 * i + 2])));
                }
            }
            if(nowHand.Count < 10)
            {
                isvalidvalue = false;
            }
            HandsList.Add(nowHand);
        }
    }

    public static void Read(int i = 0)
    {
        if (!isLoaded)
        {
            NCMBFile file = new NCMBFile("HandRecord.txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // ���s
                    if (i < 5)
                    {
                        Debug.Log("Read : " + i);
                        Read(i + 1);
                    }
                    else
                    {
                        isLoaded = true;
                        isvalidvalue = false ;
                    }

                }
                else
                {
                    // ����
                    Decode(System.Text.Encoding.UTF8.GetString(fileData));
                    UnityEngine.Debug.Log("HandRead Success");
                    isLoaded = true;
                }
            });
        }
    }
    public static void Read_backup()
    {
        if (!isLoaded)
        {
            NCMBFile file = new NCMBFile("HandRecord_backup.txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // ���s
                    Read_backup();

                }
                else
                {
                    // ����
                    Decode(System.Text.Encoding.UTF8.GetString(fileData));
                    UnityEngine.Debug.Log("HandRead_backup Success");
                    isLoaded = true;
                }
            });
        }
    }
}
