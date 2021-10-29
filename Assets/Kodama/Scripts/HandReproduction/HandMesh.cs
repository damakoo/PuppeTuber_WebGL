using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HandMesh : MonoBehaviour
{
  int[] handnum;
  int[] meshtriangles;
  MeshRenderer meshRenderer_;
  MeshFilter filter;
  HandVRManager handVRManager;
  public HandMesh Initialize(HandVRManager _handVRManager)
  {
    handnum = new int[]
    {
      0,1,2,5,9,13,17
    };
    meshRenderer_ = GetComponent<MeshRenderer>();
    meshtriangles = new int[] {
                0 ,6, 5,
                0, 5, 4,
                0, 4, 3,
                0, 3, 2,
                0, 2, 1,
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 5,
                0, 5, 6
            };
    filter = GetComponent<MeshFilter>();
    handVRManager = _handVRManager;
    return this;
  }

    // Update is called once per frame
    void Update()
  {
    Mesh mesh = new Mesh();
    mesh.vertices = new Vector3[] {
     HandReader.HandsList[handVRManager.Frame][handnum[0]],
     HandReader.HandsList[handVRManager.Frame][handnum[1]],
     HandReader.HandsList[handVRManager.Frame][handnum[2]],
     HandReader.HandsList[handVRManager.Frame][handnum[3]],
     HandReader.HandsList[handVRManager.Frame][handnum[4]],
     HandReader.HandsList[handVRManager.Frame][handnum[5]],
     HandReader.HandsList[handVRManager.Frame][handnum[6]]
            };
    mesh.triangles = meshtriangles;
    mesh.RecalculateNormals();
    filter.mesh = mesh;
  }
}
