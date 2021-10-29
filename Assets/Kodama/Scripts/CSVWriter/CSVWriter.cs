using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSVWriter : MonoBehaviour
{
    [SerializeField]
    private string _filepath;
    [SerializeField]
    private string _filename;
    [SerializeField]
    private string _modelname;
    
    public string trainpath { get; set; }
    public string modelpath { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        trainpath = Application.dataPath + "/" + _filepath + "/" + _filename + ".txt";
        modelpath = Application.dataPath + "/"+ _filepath + "/" + _modelname + ".txt";
    }

    public void WriteCSV(string line)
    {
        StreamWriter streamWriter;
        FileInfo fileInfo;
        fileInfo = new FileInfo(trainpath);
        streamWriter = fileInfo.AppendText();
        streamWriter.WriteLine(line);
        streamWriter.Flush();
        streamWriter.Close();
    }
  public void ClearCSV()
  {
    FileStream filestream = new FileStream(trainpath,FileMode.Open);
    filestream.SetLength(0);
    filestream.Close();
  }
}
