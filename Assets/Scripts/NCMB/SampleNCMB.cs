using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class SampleNCMB : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Delete();
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Read();
        }
    }
    private void Delete()
    {
        NCMBFile file = new NCMBFile("Hello.txt");
        file.DeleteAsync((NCMBException error) => {
            if (error != null)
            {
                // ���s
                //Delete();
            }
            else
            {
                // ����
                Debug.Log(" Delete Success");
            }
        });
    }
    private void Read()
    {
        NCMBFile file = new NCMBFile("Hello.txt");
        file.FetchAsync((byte[] fileData, NCMBException error) => {
            if (error != null)
            {
                // ���s
                Read();
            }
            else
            {
                // ����
                Debug.Log(System.Text.Encoding.UTF8.GetString(fileData));
            }
        });
    }
    private void Save()
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(System.DateTime.Now.ToString());
        NCMBFile file2 = new NCMBFile("Hello.txt", data);
        file2.SaveAsync((NCMBException error) => {
            if (error != null)
            {
                // ���s
                Save();
            }
            else
            {
                // ����
                Debug.Log("Input Success");
            }
        });
    }
}
