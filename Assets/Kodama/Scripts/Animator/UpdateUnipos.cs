using UnityEngine;

public class UpdateUnipos : MonoBehaviour
{
    [SerializeField] GameObject UnityChan;
    [SerializeField] GameObject UnityChanbefore;
    public void AppearUnitychan_Reproduction()
    {
        UnityChan.SetActive(true);
        UnityChanbefore.SetActive(true);
    }
}
