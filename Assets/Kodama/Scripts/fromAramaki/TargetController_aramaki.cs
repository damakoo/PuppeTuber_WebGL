using UnityEngine;
using System.Collections.Generic;
using RootMotion.FinalIK;

public class TargetController_aramaki : MonoBehaviour
{
    [SerializeField] GameObject rightHandTarget;
    [SerializeField] GameObject leftHandTarget;
    [SerializeField] GameObject headTarget;
    [SerializeField] GameObject pelvisTarget;
    [SerializeField] UserStudyAnimator userStudyAnimator;
    Animator useranimation => userStudyAnimator.useranimation;
    [SerializeField] GameObject unitychan_hip;
    [SerializeField] GameObject unitychan;
    private Vector3 initpos;
    private Quaternion initRot;
    private Avatar destinationAvatar;
    private HumanPoseHandler humanposehandler;
    private HumanPose humanpose;
    private HumanPose inithumanpose;
    [SerializeField] float _smoothTimeRight = 0.1f;
    [SerializeField] float _smoothTimeLeft = 0.1f;
    [SerializeField] float _smoothTimemuscle = 0.1f;
    // 最高速度
    [SerializeField] float _maxSpeedRight = float.PositiveInfinity;
    [SerializeField] float _maxSpeedLeft = float.PositiveInfinity;
    [SerializeField] VRIK VRik;
    private bool isFirstFrame = true;
    // 現在速度(SmoothDampの計算のために必要)
    private Vector3 _currentVelocityRight = Vector3.zero;
    private Vector3 _currentVelocityLeft = Vector3.zero;
    private List<float> MuscleVelocity = new List<float>();
    private Vector3 firstPosition = new Vector3(6, -2, 0);
    [SerializeField] HandTrackingValue handTrackingValue;
    [SerializeField] AnimationArea animationArea;
    private List<AnimationInfo> animationareaList => animationArea.animationareaList;
    void Start()
    {
        initpos = unitychan_hip.transform.position;
        initRot = unitychan_hip.transform.localRotation;
        destinationAvatar = unitychan.GetComponent<Animator>().avatar;
        humanpose = new HumanPose();
        inithumanpose = new HumanPose();
        humanposehandler = new HumanPoseHandler(unitychan.GetComponent<Animator>().avatar, unitychan.transform);
        humanposehandler.GetHumanPose(ref inithumanpose);
        for (int i = 0; i < inithumanpose.muscles.Length; i++) MuscleVelocity.Add(0);
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
                UnityHandpos(position, animationareaList[mode],false),
                ref _currentVelocityLeft,
                _smoothTimeLeft,
                _maxSpeedLeft);
    }

    public void UpdateUnitychanPos()
    {
        UpdatePosition(handTrackingValue.landmarks[9], (int)userStudyAnimator._handState);
    }

    public void UpdatePosition(Vector3 position, int mode)
    {
        bool useAnimation = animationareaList[mode].useAnimation;
        if (useAnimation)
        {
            if (isFirstFrame)
            {
                useranimation.enabled = !useAnimation;
                VRik.enabled = !useAnimation;
                isFirstFrame = false;
            }
            else
            {
                UpdateAnimation(position, mode);
            }
        }
        else
        {
            isFirstFrame = true;
            useranimation.enabled = !useAnimation;
            VRik.enabled = !useAnimation;
            rightHandTarget.transform.position = SmoothDampRight(position, mode);
            leftHandTarget.transform.position = SmoothDampLeft(position, mode);
        }
    }

    float clamp(float val, float from1, float from2, float to1, float to2)
    {
        return (val - from1) * (to2 - to1) / (from2 - from1) + to1;
    }
    Vector3 UnityHandpos(Vector3 val, AnimationInfo animation, bool RightHand = true)
    {
        if (!RightHand && animation.onlyRightHand) return animationArea.Lefthandpos_default.position;
        Vector3 result;
        if (animation.onlyRightHand)
        {
                result.x = clamp(val.x, animation.handminRange.x, animation.handmaxRange.x, animation.minRange.x, animation.maxRange.x);          
        }
        else
        {
            if (RightHand)
            {
                result.x = clamp(val.x, animation.handminRange.x, animation.handmaxRange.x, animation.rangeTransform.position.x, animation.minRange.x);
            }
            else
            {
                result.x = clamp(val.x, animation.handminRange.x, animation.handmaxRange.x, animation.rangeTransform.position.x, animation.maxRange.x);
            }
        }
        result.y = clamp(val.y, animation.handmaxRange.y, animation.handminRange.y, animation.maxRange.y, animation.minRange.y);
        result.z = clamp(val.z, animation.handmaxRange.z, animation.handminRange.z, animation.maxRange.z, animation.minRange.z);
        return result;
    }
    void UpdateAnimation(Vector3 position, int mode)
    {
        humanpose = new HumanPose();
        float val = (position.y - animationareaList[mode].handminRange.y) * animationareaList[mode].Animlength / (animationareaList[mode].handmaxRange.y - animationareaList[mode].handminRange.y);
        int valint = Mathf.Clamp(Mathf.FloorToInt(val),0, animationareaList[mode].Animlength);
        humanposehandler.GetHumanPose(ref humanpose);
        for (int i = 0; i < humanpose.muscles.Length; i++)
        {
            float muscleVelocity = MuscleVelocity[i];
            humanpose.muscles[i] = Mathf.Clamp(Mathf.SmoothDamp(humanpose.muscles[i], animationareaList[mode].HumanPoseAnimList[valint][i], ref muscleVelocity, _smoothTimemuscle),-1,1);
            MuscleVelocity[i] = muscleVelocity;
        }
        humanposehandler.SetHumanPose(ref humanpose);
        unitychan_hip.transform.position = initpos;
        unitychan_hip.transform.localRotation = initRot;
    }
     private HumanPoseHandler CreateHumanPoseHandler(Animator animator)
    {
        if (animator == null) { return null; }
        var position = animator.transform.position;
        var rotation = animator.transform.rotation;
        var scale = animator.transform.localScale;

        animator.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        animator.transform.localScale = Vector3.one;
        var humanPoseHandler = new HumanPoseHandler(animator.avatar, animator.transform);

        var hipBone = animator.GetBoneTransform(HumanBodyBones.Hips);
        hipBone.rotation = rotation;
        hipBone.position = position + hipBone.up * hipBone.position.y * scale.y;
        hipBone.localScale = scale;
        return humanPoseHandler;
    }
}
