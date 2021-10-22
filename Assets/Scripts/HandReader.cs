using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class HandReader : MonoBehaviour
{
    [SerializeField]
    private string _filename = "HelloWorld";
    public string Handspath { get; set; }
    [SerializeField] Text text;

    // Start is called before the first frame update
    private void Awake()
    {
        Handspath = Application.persistentDataPath + "/" + _filename + ".txt";
    }

    public string HandLoading(string _handspath)
    {
        string content = "";
        using (StreamReader sr = new StreamReader(_handspath))
        {
            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                content += line;
            }
        }
        return content;
    }
    private void Update()
    {
        text.text = HandLoading(Handspath);
    }
}
