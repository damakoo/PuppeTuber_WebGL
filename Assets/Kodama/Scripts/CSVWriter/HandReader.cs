using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.Collections;

public class HandReader:MonoBehaviour
{
    public static List<List<Vector3>> HandsList;
    private string content;
    private bool isLoaded = false;
    private bool isdecoded = false;
    private void Start()
    {
        IEnumerator handloading = HandLoading();
        //StartCoroutine(handloading);
    }

    private void Decode(string content, out List<List<Vector3>> handslist)
    {
        handslist = new List<List<Vector3>>();
        string[] line = content.Trim().Split('\n');
        for (int j = 0; j < line.Length; j++)
        {
            string[] list = line[j].Trim().Split(' ');
            List<Vector3> nowHand = new List<Vector3>();
            for (int i = 0; i < list.Length / 3; i++)
            {
                nowHand.Add(new Vector3(float.Parse(list[3 * i]), float.Parse(list[3 * i + 1]), float.Parse(list[3 * i + 2])));
            }
            handslist.Add(nowHand);
        }
        isdecoded = true;
    }
    IEnumerator HandLoading()
    {
        //NCMBfunction.Read("HandRecord.txt", out var content);
        Read();
        while (!isLoaded)
        {
            yield return new WaitForSeconds(0.2f);
        }
        Decode(content,out HandsList);
        while (!isdecoded)
        {
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log(HandsList[0].Count);
    }
    public void Read()
    {
        NCMBFile file = new NCMBFile("HandRecord.txt");
        file.FetchAsync((byte[] fileData, NCMBException error) =>
        {
            if (error != null)
            {
                // é∏îs
                Read();
            }
            else
            {
                // ê¨å˜
                content = System.Text.Encoding.UTF8.GetString(fileData);
                UnityEngine.Debug.Log("Read Success");
                isLoaded = true;
            }
        });
    }
}
