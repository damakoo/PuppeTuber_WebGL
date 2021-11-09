using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceCordinator : MonoBehaviour
{
    [SerializeField] GameObject UnitychanReference;
    [SerializeField] GameObject UnitychanHips;
    [SerializeField] GameObject AnimationArea;
    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            AnimationArea.transform.position = new Vector3(UnitychanReference.transform.position.x, AnimationArea.transform.position.y, AnimationArea.transform.position.z);
        }
        else
        {
            AnimationArea.transform.position = new Vector3(UnitychanHips.transform.position.x, AnimationArea.transform.position.y, AnimationArea.transform.position.z);

        }
    }
}
