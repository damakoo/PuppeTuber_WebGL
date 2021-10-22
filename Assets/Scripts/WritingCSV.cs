using System;
using System.Collections.Generic;
using UnityEngine;

public class WritingCSV : MonoBehaviour
{
    [SerializeField] CSVWriter csvWriter;
    // Start is called before the first frame update
    void Start()
    {
        csvWriter.WriteCSV("");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach(var key in Input.inputString)
            {
                if(key != (char)KeyCode.Backspace)
                {
                    csvWriter.WriteCSV(key.ToString());
                }
                else
                {
                    csvWriter.ClearCSV();
                }
                if(key == (char)KeyCode.Space)
                {
                    PlayerPrefs.SetString("Time", System.DateTime.Now.ToString());
                    PlayerPrefs.Save();
                }
                if(key == (char)KeyCode.Return)
                {
                    Debug.Log(PlayerPrefs.GetString("Time", null));
                }
            }
        }
    }
}
