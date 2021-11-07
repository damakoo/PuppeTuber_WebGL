using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanpose : MonoBehaviour
{
    [SerializeField] Transform unipos;
    Vector3 startpos;
    private string de = "0.09113821,0.0006584826,0.03616342,-0.08654898,0.02990985,0.007042673,0,0,0,-0.06508806,-0.07017461,0.07598134,-0.06269223,-0.07074851,0.07371698,0,0,0,0,0,0,0.7086587,0.06631874,0.003070111,0.9438615,0.04376881,-0.1629541,-0.2171759,-0.000450767,0.6143113,0.1615361,0.09978121,0.9514889,-0.08151353,-0.06757495,-0.3346032,-0.0003897306,-0.1582906,-1.742072E-05,-0.5726489,0.3962244,0.05248716,0.7448073,0.1037144,0.187848,-0.135357,3.415097E-07,-1.616478E-05,-0.5681223,0.3953516,0.196762,0.7979785,-0.003899559,0.02215746,0.09996059,-1.500001,-0.4999988,-2.134434E-08,0.6000001,0.7,-0.6999996,0.3999999,0.8,0.6000001,-1.299997,0.3999999,0.8000001,0.4999995,2.041199E-07,0.3999999,0.8,0.4999996,-0.3000008,0.6,0.8000001,-1.199998,-0.2999998,-2.134434E-08,0.6000001,0.6000001,-0.6999996,0.3000002,0.8000001,0.4999995,-1.499999,0.25,0.8000001,0.4999995,2.006117E-07,0.3999998,0.8000001,0.4999995,-0.2500003,0.5,0.8000001";
    [SerializeField] GameObject unitychan;
    private HumanPoseHandler humanposehandler;
    private UnityEngine.HumanPose humanpose;
    public UnityEngine.HumanPose humanpose0;
    [SerializeField] int _Joint = 42;
    [SerializeField] float _clamp = 1.0f;
    [SerializeField] int frame = 0;
    List<float> nowHumanPose = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        startpos = unipos.transform.position;
        humanpose = new UnityEngine.HumanPose();
        humanpose0 = new UnityEngine.HumanPose();
        humanposehandler = new HumanPoseHandler(unitychan.GetComponent<Animator>().avatar, unitychan.transform);
        humanposehandler.GetHumanPose(ref humanpose0);
        //Decode();
    }

    // Update is called once per frame
    void Update()
    {
        humanpose = new HumanPose();
        humanposehandler.GetHumanPose(ref humanpose);
        humanpose = humanpose0;
        for (int i = 0; i < humanpose.muscles.Length; i++)
        {
            humanpose.muscles[i] = Mathf.Clamp(AnimationLoader.HumanPoseAnimList[6][frame][i], -1.0f, 1.0f);
        }
        humanposehandler.SetHumanPose(ref humanpose);
        unipos.transform.position = startpos;
    }
    private void Decode()
    {
        string[] line = de.Trim().Split('\n');
        for (int j = 0; j < line.Length; j++)
        {
            string[] list = line[j].Trim().Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                nowHumanPose.Add(float.Parse(list[i]));
            }
        }
    }
}
