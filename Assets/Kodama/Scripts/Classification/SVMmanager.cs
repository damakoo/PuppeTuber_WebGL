using UnityEngine;
using UnityEngine.UI;
using RootMotion.FinalIK;

public class SVMmanager : MonoBehaviour
{
    public UserStudyAnimator userStudyAnimator;
    [SerializeField] HandJudge handJudge;
    [SerializeField] WriteJointAngle writeJointAngle;
    [SerializeField] VRIK _vrIK;
    [SerializeField] TargetController_aramaki _targetController_Aramaki;
    [SerializeField] HandsRecorder _handsrecorder;
    [SerializeField] GameObject reproducthand;
    [SerializeField] HandVRManager handVRManager;
  [SerializeField] GameObject unitychan;
  [SerializeField] GameObject unitychan_before;
    [SerializeField] Comparator comparator;
    [System.NonSerialized] public static Step _currentstep;
    [System.NonSerialized] public static bool isLearning = false;
    [SerializeField] InputSceneManager sceneManager;
    [SerializeField] UpdateUnipos updateUnipos;
    public void SetCalculatedUI() => sceneManager.SetCalculatedUI();
    private void Start()
    {
        unitychan_before.SetActive(false);
        reproducthand.SetActive(false);
        writeJointAngle.enabled = false;
        _targetController_Aramaki.enabled = false;
        _vrIK.enabled = false;
        userStudyAnimator.EnableUI();
        _currentstep = Step.InputInstruction;
    }

    private void Update()
    {
        if (_currentstep == Step.InputInstruction)
        {
            StartInput();
        }
        else if (_currentstep == Step.Input)
        {
            if (isLearning)
            {
                bool motionHasEnded = (userStudyAnimator._handState == handState.defaultstate);
                if (sceneManager.chosenMotionIndex > 0 && motionHasEnded) PauseLearning();
                if (HandTrackingValue.islandmarkpresent)
                {
                    writeJointAngle.AddTraingdata((int)userStudyAnimator._handState);
                    _handsrecorder.Recording((int)userStudyAnimator._handState);
                }
            }
        }
        else if (_currentstep == Step.Calculate)
        {
            sceneManager.SetCalculatingUI();
            writeJointAngle.Calculatemodel();
    }
        else if (_currentstep == Step.OutputInstruction)
        {
            StartOutput();
        }
        else if (_currentstep == Step.Output)
        {
            handJudge.JudgingHand();
            _targetController_Aramaki.UpdateUnitychanPos();
        }
        else if (_currentstep == Step.ReproductInstruction)
        {

        }
        else if (_currentstep == Step.Reproduction)
        {
            if (handVRManager.Frame < HandReader.HandsList.Count - 1)
            {
                handVRManager.Frame += 1;
                handVRManager.AnimationUpdate();
                comparator.UpdateParameter();
            }
            else
            {
                reproducthand.SetActive(false);
                Finish();
                sceneManager.SegueToFinish();
            }
        }
    }

    public void LearningorInputagain()
    {
        if(_currentstep == Step.Output)
        {
            InputAgain();
        }
        else
        {
            StartLearning();
        }
    }
    public void StartLearning()
    {
        _handsrecorder.ClearPositionList(sceneManager.chosenMotionIndex);
        writeJointAngle.ClearTraingingdata(sceneManager.chosenMotionIndex);
        userStudyAnimator.changeAnimationToIndex(sceneManager.chosenMotionIndex);
        isLearning = true;
        sceneManager.SetLearnedUI(false);
        if (sceneManager.chosenMotionIndex == 0) Invoke("PauseLearning", 3);
        Debug.Log("Learning Started");
    }

    public void PauseLearning()
    {
        isLearning = false;
        sceneManager.SetLearnedUI(true);
        Debug.Log("Learning Ended");
    }

    private void SwitchStep(Step step)
    {
        _currentstep = step;
    }
    private void StartInput()
    {
        writeJointAngle.enabled = true;
        Debug.Log("Input Start");
        SwitchStep(Step.Input);
    }
    public void StartCalculate()
    {
        Debug.Log("Calculate Start");
        SwitchStep(Step.Calculate);
    }
    public void StartWaitingforOutput()
    {
        sceneManager.SetCalculatedUI();
        _targetController_Aramaki.enabled = true;
        _vrIK.enabled = true;
        Debug.Log("Learning finished");
        SwitchStep(Step.OutputInstruction);
    }
    private void StartOutput()
    {
        Debug.Log("Output Start");
        SwitchStep(Step.Output);
    }
    public void StartReproducInstruction()
    {
        _handsrecorder.SendRecordingData();
        writeJointAngle.sendData();
        unitychan_before.SetActive(true);
        updateUnipos.UpdateUnityChanpos();
        SwitchStep(Step.ReproductInstruction);
    }
    public void StartReproduction()
    {
        reproducthand.SetActive(true);
        SwitchStep(Step.Reproduction);
    }
    private void Finish()
    {
        comparator.ShowResult();
        Debug.Log("Finished");
        SwitchStep(Step.Finished);
    }
    public void InputAgain(string popuptext = "まずはデフォルトのポーズに対応する手の形を決めましょう。", bool _onceLearned = true)
    {
        _vrIK.enabled = false;
        sceneManager.ReInput(popuptext,_onceLearned);
        userStudyAnimator.changeAnimationToIndex(sceneManager.chosenMotionIndex);
        StartInput();
    }
}
