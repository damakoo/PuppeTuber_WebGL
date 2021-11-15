using UnityEngine;
public class Regression_Parent
{
    public Regression rotX { get; }
    public Regression rotY { get; }
    public Regression rotZ { get; }
    public Regression rotW { get; }
    public Regression_Parent(Regression _X, Regression _Y, Regression _Z, Regression _W)
    {
        this.rotX = _X;
        this.rotY = _Y;
        this.rotZ = _Z;
        this.rotW = _W;
    }
    public Quaternion Regression_Analysis(Vector3 Handposition)
    {
        Quaternion rot;
        rot.x = Handposition.x * rotX.a_x + Handposition.y * rotX.a_y + Handposition.z * rotX.a_z + rotX.b;
        rot.y = Handposition.x * rotY.a_x + Handposition.y * rotY.a_y + Handposition.z * rotY.a_z + rotY.b;
        rot.z = Handposition.x * rotZ.a_x + Handposition.y * rotZ.a_y + Handposition.z * rotZ.a_z + rotZ.b;
        rot.w = Handposition.x * rotW.a_x + Handposition.y * rotW.a_y + Handposition.z * rotW.a_z + rotW.b;
        return rot;
    }
}
