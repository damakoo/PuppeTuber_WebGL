using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVRManager : MonoBehaviour
{

    [SerializeField] AddJointAngle_HR addJointAngle;
    [SerializeField] HandJudge_Comparate handJudge_Comparate;
    [SerializeField] TargetController_HR targetController_now;
    [SerializeField] TargetController_HR targetController_before;

    public List<HandVRPosition> handpositionlist { get; set; } = new List<HandVRPosition>();
    public List<HandVRRenderer> handVRRendererlist { get; set; } = new List<HandVRRenderer>();
    public int Frame { get; set; } = 0;
    private void OnEnable()
    {
        HandReproduction();
    }
    private void HandReproduction()
    {
        int linenum = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "Finger")
            {
                HandVRPosition handVRPosition = transform.GetChild(i).gameObject.AddComponent<HandVRPosition>();
                handVRPosition.Initialize(i, this);
                handpositionlist.Add(handVRPosition);
            }
            else if (transform.GetChild(i).gameObject.tag == "FingerLine")
            {
                HandVRRenderer handVRRenderer = transform.GetChild(i).gameObject.AddComponent<HandVRRenderer>();
                handVRRenderer.Initialize(HandVRTables.LineRendererTable[i % 5], this);
                handVRRendererlist.Add(handVRRenderer);
                linenum += 1;
            }
            else if (transform.GetChild(i).gameObject.tag == "HandMesh")
            {
                HandMesh handmesh = transform.GetChild(i).gameObject.AddComponent<HandMesh>();
                handmesh.Initialize(this);
            }
        }
    }
    public void AnimationUpdate()
    {
        addJointAngle.UpdateNode();
        handJudge_Comparate.JudgingHand();
        targetController_before.UpdateUnitychanPos();
        targetController_now.UpdateUnitychanPos();
    }
}
