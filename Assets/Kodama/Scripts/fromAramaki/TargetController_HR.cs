using UnityEngine;

public class TargetController_HR : MonoBehaviour
{
  [SerializeField] GameObject rightHandTarget;
  [SerializeField] GameObject leftHandTarget;
  [SerializeField] GameObject headTarget;
  [SerializeField] GameObject pelvisTarget;
  [SerializeField] UserStudyAnimator userStudyAnimator;
  [SerializeField] GameObject bodyCenter;
  [SerializeField] float _smoothTimeRight = 0.1f;
  [SerializeField] float _smoothTimeLeft = 0.1f;
  [SerializeField] HandVRManager handVRManager;
  // 最高速度
  [SerializeField] float _maxSpeedRight = float.PositiveInfinity;
  [SerializeField] float _maxSpeedLeft = float.PositiveInfinity;
  // 現在速度(SmoothDampの計算のために必要)
  private Vector3 _currentVelocityRight = new Vector3(0, 0, 0);
  private Vector3 _currentVelocityLeft = new Vector3(0, 0, 0);
  private Vector3 firstPosition = new Vector3(0, -2, 0);
  void Start()
  {
    pelvisTarget.transform.position = new Vector3(
      bodyCenter.transform.position.x,
      bodyCenter.transform.position.y + 30,
      bodyCenter.transform.position.z
    );
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

  public void UpdateUnitychanPos()
  {
    UpdatePosition(HandReader.HandsList[handVRManager.Frame][9], (int)userStudyAnimator._handState);
  }

  public void UpdatePosition(Vector3 position, int mode)
  {
    pelvisTarget.transform.position = new Vector3(
      bodyCenter.transform.position.x,
      bodyCenter.transform.position.y + 30,
      bodyCenter.transform.position.z
    );
    Vector3 clampedPosition;
    switch (mode)
    {
      case 0:
        rightHandTarget.transform.position = pelvisTarget.transform.position;//SmoothDampRight(firstPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, 90);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -90, 90);
        break;
      case 1:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 6, 21), clamp(position.y, 0, 1, 58, 41), -2);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, 0, -120 + clampedPosition.x * 5);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -90, 90);
        break;
      case 2:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 0, 6), clamp(position.y, 0, 1, 38, 36), -7);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -120, -20);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -60, -20);
        break;
      case 3:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 3, 10), clamp(position.y, 0, 1, 42, 32), -7);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, -30);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, -90, -30);
        break;
      case 5:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 0, 15), clamp(position.y, 0, 1, 50, 41), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, 0, 160 + clampedPosition.x * 7);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      case 7:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 0, 8), clamp(position.y, 0, 1, 45, 41), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(clampedPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(180, 0, 150);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, -30);
        break;
      case 8:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 6, 15), clamp(position.y, 0, 1, 52, 41), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(0, -90, -90);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      case 9:
        clampedPosition = new Vector3(clamp(position.x, 0, 1, 5, 13), clamp(position.y, 0, 1, 47, 44), -5);
        rightHandTarget.transform.position = SmoothDampRight(clampedPosition);
        leftHandTarget.transform.position = SmoothDampLeft(firstPosition);
        rightHandTarget.transform.rotation = Quaternion.Euler(-90, 0, 180);
        leftHandTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
      default:
        break;
    }
  }

  float clamp(float val, float from1, float from2, float to1, float to2)
  {
    return (val - from1) * (to2 - to1) / (from2 - from1) + to1;
  }
}
