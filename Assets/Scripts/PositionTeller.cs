using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTeller : MonoBehaviour
{
    [SerializeField] GameObject animationarea;
    [SerializeField] GameObject ballpos;
    [SerializeField] GameObject referencepos;
    [SerializeField] GameObject Hippos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(animationarea.ToString() + " : " + animationarea.transform.position.x + " : " + animationarea.transform.position.y + " : " + animationarea.transform.position.z);
            Debug.Log(ballpos.ToString() + " : " + ballpos.transform.position.x + " : " + ballpos.transform.position.y + " : " + ballpos.transform.position.z);
            Debug.Log(referencepos.ToString() + " : " + referencepos.transform.position.x + " : " + referencepos.transform.position.y + " : " + referencepos.transform.position.z);
            Debug.Log(Hippos.ToString() + " : " + Hippos.transform.position.x + " : " + Hippos.transform.position.y + " : " + Hippos.transform.position.z);
        }

    }
}
