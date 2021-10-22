using System;
using System.Collections.Generic;
using UnityEngine;

public class WritingCSV : MonoBehaviour
{
    [SerializeField] CSVWriter csvWriter;
    // Start is called before the first frame update
    void Start()
    {
        csvWriter.WriteCSV(System.DateTime.Now.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        csvWriter.ClearCSV();
        csvWriter.WriteCSV(System.DateTime.Now.ToString());
    }
}
