using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIK_Rotation : MonoBehaviour
{
    //    [SerializeField] Transform UnityHandTransform;
    //    [SerializeField] Transform thistransform;
    //    [SerializeField] Transform originTransform;
    //    Vector3 EulerRotation;
    //    Vector3 Unitychan_initRot;

    //    private void Start()
    //    {
    //        Unitychan_initRot = UnityHandTransform.transform.rotation.eulerAngles;
    //    }
    //    // Update is called once per frame
    //    void Update()
    //    {
    //        EulerRotation = thistransform.transform.position - originTransform.transform.position + Unitychan_initRot;
    //        thistransform.rotation = Quaternion.Euler(EulerRotation);
    //    }
    static Regression_Parent ByeBye_Right = new Regression_Parent(
    
        new Regression(0.004656f,0.029632f,0.058459f,-1.09841f),
        new Regression(0.007239f,0.023479f,0.034087f,-0.93002f),
        new Regression(-0.03486f,-0.0317f,0.07812f,1.117839f),
        new Regression(-0.06092f,0.015601f,0.033245f,-0.25202f)
    );
    [SerializeField] Transform unitychanreference;
    [SerializeField] Transform thistransform;
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 pos = thistransform.position - unitychanreference.position;
        thistransform.transform.rotation = ByeBye_Right.Regression_Analysis(pos);
        Debug.Log(pos.x + " : " + pos.y + " : " + pos.z);
    }

}
