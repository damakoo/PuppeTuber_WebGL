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
    //Regression_Parent ByeBye_Right = new Regression_Parent(

    //    new Regression(0.004656f,0.029632f,0.058459f,-1.09841f),
    //    new Regression(0.007239f,0.023479f,0.034087f,-0.93002f),
    //    new Regression(-0.03486f,-0.0317f,0.07812f,1.117839f),
    //    new Regression(-0.06092f,0.015601f,0.033245f,-0.25202f)
    //);
    public static Regression_Parent ByeBye_Right = new Regression_Parent(
new Regression(-36.9815f, 28.36071f, 59.09159f, -0.97723f),
new Regression(-60.2387f, -15.1416f, 18.91624f, 0.437316f),
new Regression(-7.30889f, 26.16098f, -62.8277f, -0.96816f),
new Regression(-7.48087f, 19.54086f, -33.2951f, -0.86865f)
);
    [SerializeField] Transform unitychanreference;
    [SerializeField] Transform thistransform;
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 pos_right = new Vector3(thistransform.localPosition.x / thistransform.lossyScale.x, thistransform.localPosition.y / thistransform.lossyScale.y, thistransform.localPosition.z / thistransform.lossyScale.z);
        thistransform.transform.localRotation = ByeBye_Right.Regression_Analysis(pos_right);
        Debug.Log(pos_right.x + " : " + pos_right.y + " : " + pos_right.z + " : " + thistransform.transform.localRotation.x + " : " + thistransform.transform.localRotation.y + " : " + thistransform.transform.localRotation.z + " : " + thistransform.transform.localRotation.w);
    }

}
