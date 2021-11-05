using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class InputSceneManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void appearcanvas();
    [DllImport("__Internal")]
    private static extern void fadecanvas();
    [SerializeField] GameObject popup;
    [SerializeField] Text indexLabel;
    [SerializeField] Text motionNameLabel;
    [SerializeField] SVMmanager svmManager;
    public Button doneButton;
    public Button playButton;
    public Button startLearningButton;
    [System.NonSerialized] public int chosenMotionIndex = 0;
    [SerializeField] GameObject inputUISet;
    [SerializeField] GameObject compareUISet;
    private bool onceLearned { get; set; } = true;//本番ではfalseに設定することで一度目は必ず学習させる

    // Start is called before the first frame update
    void Start()
    {
        FadeManager.FadeIn();
        ShowPopup("まずはデフォルトのポーズに対応する手の形を決めましょう。");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReInput()
    {
        onceLearned = true;
        chosenMotionIndex = 0;
        ShowPopup("まずはデフォルトのポーズに対応する手の形を決めましょう。");
        startLearningButton.GetComponentInChildren<Text>().text = "学習開始(B)";
        doneButton.GetComponentInChildren<Text>().text = "次に進む(N)";
        indexLabel.text = "";
        motionNameLabel.text = "デフォルト";
    }

    void ShowPopup(string text)
    {
        popup.SetActive(true);
        popup.GetComponentInChildren<Text>().text = text;
        fadecanvas();
    }

    public void IncrementAnimationIndex()
    {
        chosenMotionIndex += 1;
        if (chosenMotionIndex == 1) ShowPopup("次に、8つのモーションに対応する手の動きを決めましょう。");
        if (chosenMotionIndex <= 8)
        {
            Debug.Log("Proceeded to next");
            startLearningButton.interactable = true;
            startLearningButton.GetComponentInChildren<Text>().text = "学習開始(B)";
            // doneButton.interactable = false;
            doneButton.GetComponentInChildren<Text>().text = "次に進む(N)";
            playButton.gameObject.SetActive(true);
            indexLabel.text = $"{chosenMotionIndex}/8";
            motionNameLabel.text = Constants.motionNames[chosenMotionIndex];
        }
        else if (SVMmanager._currentstep == Step.Input)
        {
            doneButton.interactable = false;
            svmManager.StartCalculate();
        }
        else if (SVMmanager._currentstep == Step.Output)
        {
            inputUISet.SetActive(false);
            compareUISet.SetActive(true);
            svmManager.StartReproducInstruction();
            ShowPopup("比較画面の説明を入れる");
        }
    }

    public void SetLearnedUI(bool isLearned)
    {
        startLearningButton.GetComponentInChildren<Text>().text = isLearned ? "再学習(B)" : "学習中……";
        SetAllButton(isLearned);
        if (!onceLearned && isLearned) startLearningButton.interactable = false;
    }

    public void SetCalculatingUI()
    {
        playButton.interactable = false;
        doneButton.interactable = false;
    }

    public void SetCalculatedUI()
    {
        indexLabel.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        doneButton.interactable = true;
        startLearningButton.GetComponentInChildren<Text>().text = "やり直す(B)";
        startLearningButton.interactable = true;
        motionNameLabel.text = "検出中モーション: なし";
        ShowPopup("この画面では，先ほど作成した操作方法を自由に試すことができます\n\n修正したい場合は「やり直す(B)」ボタンを，\n\nこれでよければ「次に進む」ボタンを押してください．");
    }

    public void ClosePopup()
    {
        popup.SetActive(false);
        if (SVMmanager._currentstep == Step.ReproductInstruction)
        {
            svmManager.StartReproduction();
        }
        else
        {
            appearcanvas();
        }
    }

    public void SegueToFinish()
    {
        FadeManager.FadeOut(4);
    }
    public void SetAllButton(bool isinteractable)
    {
        doneButton.interactable = isinteractable;
        playButton.interactable = isinteractable;
        startLearningButton.interactable = (onceLearned) ? isinteractable : false;
    }
}
