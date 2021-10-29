using UnityEngine;
using UnityEngine.UI;
using RootMotion.FinalIK;

public class SVMmanager : MonoBehaviour
{
    public UserStudyAnimator userStudyAnimator;
    [SerializeField] UserStudyAnimator userStudyAnimator_before;
    [SerializeField] HandJudge handJudge;
    [SerializeField] WriteJointAngle writeJointAngle;
    [SerializeField] Text _stepText;
    [SerializeField] VRIK _vrIK;
    [SerializeField] TargetController_aramaki _targetController_Aramaki;
    [SerializeField] HandsRecorder _handsrecorder;
    [SerializeField] GameObject reproducthand;
    [SerializeField] HandVRManager handVRManager;
    [SerializeField] GameObject unitychan_before;
    [SerializeField] Comparator comparator;
    [System.NonSerialized] public Step _currentstep;
    [System.NonSerialized] bool isLearning = false;
    [SerializeField] InputSceneManager sceneManager;
    private void Start()
    {
        unitychan_before.SetActive(false);
        reproducthand.SetActive(false);
        writeJointAngle.enabled = false;
        _targetController_Aramaki.enabled = false;
        _handsrecorder.enabled = false;
        _vrIK.enabled = false;
        userStudyAnimator.EnableUI();
        _currentstep = Step.InputInstruction;
        _stepText.text = "Wait for Input";
    }

    private void Update()
    {
        if (_currentstep == Step.InputInstruction)
        {
            // userStudyAnimator.changeAnimation();
            // if (Input.GetKeyDown(_keyCode))
            // {
            StartInput();
            // }
        }
        else if (_currentstep == Step.Input)
        {
            // userStudyAnimator.changeAnimation();
            if (isLearning)
            {
                bool motionHasEnded = (userStudyAnimator._handState == handState.defaultstate);
                if (sceneManager.chosenMotionIndex > 0 && motionHasEnded) PauseLearning();
                writeJointAngle.AddTraingdata((int)userStudyAnimator._handState);
                _handsrecorder.Recording((int)userStudyAnimator._handState);
            }
        }
        else if (_currentstep == Step.Calculate)
        {
            sceneManager.SetCalculatingUI();
            writeJointAngle.Calculatemodel();
            writeJointAngle.Savemodel();
            _handsrecorder.SendRecordingData();
            sceneManager.SetCalculatedUI();
            StartWaitingforOutput();
        }
        else if (_currentstep == Step.OutputInstruction)
        {
            // if (Input.GetKeyDown(_keyCode))
            // {
            StartOutput();
            // }
        }
        else if (_currentstep == Step.Output)
        {
            handJudge.JudgingHand();
            _targetController_Aramaki.UpdateUnitychanPos();
            // if (Input.GetKeyDown(_keyCode))
            // {
            //   StartReproducInstruction();
            // }
        }
        else if (_currentstep == Step.ReproductInstruction)
        {
            // if (Input.GetKeyDown(_keyCode))
            // {
            //   StartReproduction();
            // }
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

    public void StartLearning()
    {
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
        _stepText.text = "Input Now";
        Debug.Log("Input Start");
        SwitchStep(Step.Input);
    }
    public void StartCalculate()
    {
        _stepText.text = "Calculating ...";
        Debug.Log("Calculate Start");
        SwitchStep(Step.Calculate);
    }
    private void StartWaitingforOutput()
    {
        _targetController_Aramaki.enabled = true;
        _vrIK.enabled = true;
        _stepText.text = "Wait for Output";
        Debug.Log("Learning finished");
        SwitchStep(Step.OutputInstruction);
    }
    private void StartOutput()
    {
        _handsrecorder.enabled = true;
        _stepText.text = "Output Now";
        Debug.Log("Output Start");
        SwitchStep(Step.Output);
    }
    public void StartReproducInstruction()
    {
        userStudyAnimator_before.EnableUI();
        unitychan_before.SetActive(true);
        _stepText.text = "Wait for Reproduction";
        SwitchStep(Step.ReproductInstruction);
    }
    public void StartReproduction()
    {
        _stepText.text = "Reproduction";
        reproducthand.SetActive(true);
        SwitchStep(Step.Reproduction);
    }
    private void Finish()
    {
        comparator.ShowResult();
        _stepText.text = "Finish";
        Debug.Log("Finished");
        SwitchStep(Step.Finished);
    }
}
