using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAvatar : MonoBehaviour
{
    [SerializeField] GameObject UnityChan_Reference;
    [SerializeField] GameObject UnityChanhip_Reference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");
            //UnityChan_Reference.transform.position += new Vector3(0.1f, 0, 0);
            UnityChanhip_Reference.transform.localPosition += new Vector3(0.1f, 0, 0);

            Debug.Log("b");
        }
    }
}
