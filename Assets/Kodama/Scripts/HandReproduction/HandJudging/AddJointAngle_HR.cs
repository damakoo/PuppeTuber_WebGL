using System.Collections.Generic;
using UnityEngine;

public class AddJointAngle_HR : MonoBehaviour
{
  public List<JointAngle_HR> JointAngleList;
  public Vector3 handdirection { get; set; }
  [SerializeField] HandVRManager handVRManager;
  public List<float> node { get; set; }
  void OnEnable()
  {
    JointAngleList = JointAngle_HR.AutomaticCreate(JointAngleTable.JointAngleConstantTable, handVRManager);
  }

  void Update()
  {
      handdirection = (HandReader.HandsList[handVRManager.Frame][17] - HandReader.HandsList[handVRManager.Frame][1]).normalized;
  }
  private List<float> Node()
  {
    List<float> nodes = new List<float>();
    for (int i = 0; i < JointAngleList.Count; i++)
    {
      nodes.Add(JointAngleList[i].angle());
    }
    nodes.Add(handdirection.x);
    nodes.Add(handdirection.y);
    nodes.Add(handdirection.z);
    return nodes;
  }
  public void UpdateNode()
  {
    node = Node();
  }
}
