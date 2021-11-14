using UnityEngine;
using System.Collections.Generic;
using RootMotion.FinalIK;

public class TargetController_HR : MonoBehaviour
{
  [SerializeField] GameObject rightHandTarget;
  [SerializeField] GameObject leftHandTarget;
  [SerializeField] UserStudyAnimator userStudyAnimator;
    Animator useranimation => userStudyAnimator.useranimation;
    [SerializeField] GameObject unitychan;
    [SerializeField] GameObject unitychan_hip;
    [SerializeField] GameObject unitychan_ref;
    [SerializeField] GameObject Righthand;
    [SerializeField] GameObject Lefthand;
    private Vector3 initpos;
    private Quaternion initRot;
    private Vector3 initposref;
    private Quaternion initRotref;
    private HumanPoseHandler humanposehandler;
    private HumanPose humanpose;
    [SerializeField] float _smoothTimeRight = 0.1f;
    [SerializeField] float _smoothTimeLeft = 0.1f;
    [SerializeField] float _smoothTimemuscle = 0.1f;
    // 最高速度
    [SerializeField] float _maxSpeedRight = float.PositiveInfinity;
    [SerializeField] float _maxSpeedLeft = float.PositiveInfinity;
    [SerializeField] VRIK VRik;
    // 現在速度(SmoothDampの計算のために必要)
    private Vector3 _currentVelocityRight = Vector3.zero;
    private Vector3 _currentVelocityLeft = Vector3.zero;
    private List<float> MuscleVelocity = new List<float>();
    [SerializeField] AnimationArea animationArea;
    [SerializeField] HandVRManager handVRManager;
    private List<AnimationInfo> animationareaList => animationArea.animationareaList;
    void Start()
  {
        initpos = unitychan_hip.transform.localPosition;
        initRot = unitychan_hip.transform.localRotation; 
        initposref = unitychan_ref.transform.localPosition;
        initRotref = unitychan_ref.transform.localRotation;
        humanpose = new HumanPose();
        humanposehandler = new HumanPoseHandler(unitychan.GetComponent<Animator>().avatar, unitychan.transform);
        humanposehandler.GetHumanPose(ref humanpose);
        for (int i = 0; i < humanpose.muscles.Length; i++) MuscleVelocity.Add(0);
    }
    Vector3 SmoothDampRight(Vector3 position, int mode)
    {
        return Vector3.SmoothDamp(
                rightHandTarget.transform.position,
                UnityHandpos(position, animationareaList[mode]),
                ref _currentVelocityRight,
                _smoothTimeRight,
                _maxSpeedRight);
    }

    Vector3 SmoothDampLeft(Vector3 position, int mode)
    {
        return Vector3.SmoothDamp(
                leftHandTarget.transform.position,
                UnityHandpos(position, animationareaList[mode], false),
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
        bool useAnimation = animationareaList[mode].useAnimation;
        useranimation.enabled = !useAnimation;
        VRik.enabled = !useAnimation;
        if (useAnimation)
        {
            UpdateAnimation(position, mode);
        }
        else
        {
            UpdateBasePos();
            UpdateVRIKPos(position, mode);
            UpdateVRIKRot(mode);
        }
    }
    float clamp(float val, float from1, float from2, float to1, float to2)
    {
        float result = (val - from1) * (to2 - to1) / (from2 - from1) + to1;
        return (to1 > to2) ? Mathf.Clamp(result, to2, to1) : Mathf.Clamp(result, to1, to2);
    }
    float clamp_symmetry(float val, float from1, float from2, float to1, float to2, float handdistance = 0)
    {
        float to1_1 = to1 + (to2 - to1) * handdistance;
        float result = Mathf.Abs(val - from1) * (to2 - to1_1) / (from2 - from1) + to1_1;
        return (to1 > to2) ? Mathf.Clamp(result, to2, to1_1) : Mathf.Clamp(result, to1_1, to2);
    }
    Vector3 UnityHandpos(Vector3 val, AnimationInfo animation, bool RightHand = true)
    {
        if (!RightHand && animation.onlyRightHand) return animationArea.Lefthandpos_default.position;
        Vector3 result;
        if (animation.onlyRightHand)
        {
            result.x = clamp(val.x, animation.handminRange.x, animation.handmaxRange.x, animation.maxRange.x, animation.minRange.x);
        }
        else
        {
            if (RightHand)
            {
                result.x = clamp_symmetry(val.x, animation.handAVE.x, animation.handmaxRange.x, animation.rangeTransform.position.x, animation.minRange.x,animation.HandDistance);
            }
            else
            {
                result.x = clamp_symmetry(val.x, animation.handAVE.x, animation.handmaxRange.x, animation.rangeTransform.position.x, animation.maxRange.x,animation.HandDistance);
            }
        }
        result.y = clamp(val.y, animation.handmaxRange.y, animation.handminRange.y, animation.minRange.y, animation.maxRange.y);
        result.z = clamp(val.z, animation.handmaxRange.z, animation.handminRange.z, animation.maxRange.z, animation.minRange.z);
        return result;
    }
    void UpdateVRIKPos(Vector3 position, int mode)
    {
        rightHandTarget.transform.position = SmoothDampRight(position, mode);
        leftHandTarget.transform.position = SmoothDampLeft(position, mode);
    }
    void UpdateVRIKRot(int mode)
    {
        Lefthand.transform.localRotation = animationareaList[mode].LeftLocalRot;
        Righthand.transform.localRotation = animationareaList[mode].RightLocalRot;
        leftHandTarget.transform.rotation = Lefthand.transform.rotation;
        rightHandTarget.transform.rotation = Righthand.transform.rotation;
    }
    void UpdateAnimation(Vector3 position, int mode)
    {
        humanpose = new HumanPose();
        float val = (position.y - animationareaList[mode].handminRange.y) * animationareaList[mode].Animlength / (animationareaList[mode].handmaxRange.y - animationareaList[mode].handminRange.y);
        int valint = Mathf.Clamp(Mathf.FloorToInt(val), 0, animationareaList[mode].Animlength);
        humanposehandler.GetHumanPose(ref humanpose);
        for (int i = 0; i < humanpose.muscles.Length; i++)
        {
            float muscleVelocity = MuscleVelocity[i];
            humanpose.muscles[i] = Mathf.Clamp(Mathf.SmoothDamp(humanpose.muscles[i], animationareaList[mode].HumanPoseAnimList[valint][i], ref muscleVelocity, _smoothTimemuscle), -1, 1);
            MuscleVelocity[i] = muscleVelocity;
        }
        humanposehandler.SetHumanPose(ref humanpose);
        UpdateBasePos();
    }
    private void UpdateBasePos()
    {
        unitychan_hip.transform.localPosition = initpos;
        unitychan_hip.transform.localRotation = initRot;
        unitychan_ref.transform.localPosition = initposref;
        unitychan_ref.transform.localRotation = initRotref;
    }
    public void UpdateInitpos()
    {
        initpos = unitychan_hip.transform.localPosition;
        initRot = unitychan_hip.transform.localRotation;
        initposref = unitychan_ref.transform.localPosition;
        initRotref = unitychan_ref.transform.localRotation;
    }
}
