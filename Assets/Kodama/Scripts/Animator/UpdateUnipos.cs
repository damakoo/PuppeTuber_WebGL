using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUnipos : MonoBehaviour
{
    [SerializeField] GameObject UnityChan_Reference;
    [SerializeField] GameObject UnityChanbefore_Reference;
    [SerializeField] GameObject UnityChan_Destination;
    [SerializeField] GameObject UnityChanbefore_Destination;
    [SerializeField] TargetController_HR targetController;
    [SerializeField] TargetController_HR targetController_before;

    public void UpdateUnityChanpos()
    {
        UnityChan_Reference.transform.position = UnityChan_Destination.transform.position;
        UnityChan_Reference.transform.rotation = UnityChan_Destination.transform.rotation;
        UnityChanbefore_Reference.transform.position = UnityChanbefore_Destination.transform.position;
        UnityChanbefore_Reference.transform.rotation = UnityChanbefore_Destination.transform.rotation;
        targetController.UpdateInitpos();
        targetController_before.UpdateInitpos();
    }
}
