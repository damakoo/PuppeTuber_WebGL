using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject rightHandTarget = null;
  public GameObject leftHandTarget = null;
  public GameObject headTarget = null;
  public GameObject pelvisTarget = null;
  public GameObject bodyCenter = null;
  // 目標値に到達するまでのおおよその時間[s]
  private float _smoothTimeRight = 0.1f; private float _smoothTimeLeft = 0.1f;
  // 最高速度
  private float _maxSpeedRight = float.PositiveInfinity; private float _maxSpeedLeft = float.PositiveInfinity;
  // 現在速度(SmoothDampの計算のために必要)
  private Vector3 _currentVelocityRight = new Vector3(0, 0, 0); private Vector3 _currentVelocityLeft = new Vector3(0, 0, 0);
  private int mode = 0;
  private Vector3 firstPosition = new Vector3(6, -2, 0);
  void Start()
  {
    pelvisTarget.transform.position = new Vector3(
      bodyCenter.transform.position.x, 
      bodyCenter.transform.position.y + 30, 
      bodyCenter.transform.position.z
      );
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.Alpha0)) mode = 0;
    if (Input.GetKey(KeyCode.Alpha1)) mode = 1;
    if (Input.GetKey(KeyCode.Alpha2)) mode = 2;
    if (Input.GetKey(KeyCode.Alpha3)) mode = 3;
    if (Input.GetKey(KeyCode.Alpha4)) mode = 4;
    if (Input.GetKey(KeyCode.Alpha5)) mode = 5;
    if (Input.GetKey(KeyCode.Alpha6)) mode = 6;
    if (Input.GetKey(KeyCode.Alpha7)) mode = 7;
    if (Input.GetKey(KeyCode.Alpha8)) mode = 8;
    if (Input.GetKey(KeyCode.Alpha9)) mode = 9;

    Vector3 clampedPosition;
    switch (mode)
    {
      case 0:
        rightHandTarget.transform.position = rightHandPosition(firstPosition);
        leftHandTarget.transform.position = leftHandPosition(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, 90);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -90, 90);
        break;
      case 1:
        clampedPosition = new Vector3(13.5f, 49.5f, -2);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, 0, -90);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      case 2:
        clampedPosition = new Vector3(3, 37, -7);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, 0);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -90, 0);
        break;
      case 3:
        clampedPosition = new Vector3(6.5f, 37f, -7);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, -30);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -90, -30);
        break;
      case 5:
        clampedPosition = new Vector3(7.5f, 45.5f, -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, 0, 180);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      case 7:
        clampedPosition = new Vector3(4, 43, -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(180, 0, 150);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, -30);
        break;
      case 8:
        clampedPosition = new Vector3(10.5f, 46.5f, -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, -90);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      case 9:
        clampedPosition = new Vector3(9, 45.5f, -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(-90, 0, 180);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      default:
        break;
    }
  }

  Vector3 rightHandPosition(Vector3 position)
  {
    return new Vector3(bodyCenter.transform.position.x - position.x,
                      bodyCenter.transform.position.y + position.y,
                      bodyCenter.transform.position.z + position.z);
  }
  Vector3 leftHandPosition(Vector3 position)
  {
    return new Vector3(bodyCenter.transform.position.x + position.x,
                      bodyCenter.transform.position.y + position.y,
                      bodyCenter.transform.position.z + position.z);
  }

  Vector3 SmoothDampRight(Vector3 position)
  {
    return Vector3.SmoothDamp(
            rightHandTarget.transform.position,
            rightHandPosition(position),
            ref _currentVelocityRight,
            _smoothTimeRight,
            _maxSpeedRight);
  }

  Vector3 SmoothDampLeft(Vector3 position)
  {
    return Vector3.SmoothDamp(
            leftHandTarget.transform.position,
            leftHandPosition(position),
            ref _currentVelocityLeft,
            _smoothTimeLeft,
            _maxSpeedLeft);
  }

  /*public void UpdatePosition(Vector3 position)
  {
    Vector3 clampedPosition;
    switch (mode)
    {
      case 0:
        rightHandTarget.transform.position = SmoothDampRight(firstPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        break;
      case 1:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 6, 21), clamp(position.y, 0, 1, 58, 41), -2);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, 0, -120 + clampedPosition.x * 5);
        break;
      case 2:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 0, 6), clamp(position.y, 0, 1, 38, 36), -7);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        break;
      case 3:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 3, 10), clamp(position.y, 0, 1, 42, 32), -7);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        break;
      case 5:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 0, 15), clamp(position.y, 0, 1, 50, 41), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, 0, 160 + clampedPosition.x * 7);
        break;
      case 7:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 0, 8), clamp(position.y, 0, 1, 45, 41), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        break;
      case 8:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 6, 15), clamp(position.y, 0, 1, 52, 41), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        break;
      case 9:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 5, 13), clamp(position.y, 0, 1, 47, 44), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        break;
      default:
        break;
    }
  }*/

  float clamp(float val, float from1, float from2, float to1, float to2)
  {
    return (val - from1) * (to2 - to1) / (from2 - from1) + to1;
  }
}
