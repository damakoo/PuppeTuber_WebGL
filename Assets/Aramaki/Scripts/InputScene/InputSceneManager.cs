using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class InputSceneManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void appearcanvas();
    [DllImport("__Internal")]
    private static extern void fadecanvas();
    [DllImport("__Internal")]
    private static extern string GetLocalStorage(string key);
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
    public bool isactive_donebutton { get; set; } = false;
    private bool onceLearned = false;//false:本番用．各アニメーション一度は必ず学習
    private bool HandDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        SetAllButton(false);
        FadeManager.FadeIn();
        ShowPopup("まずはデフォルトのポーズに対応する手の形を決めましょう。\n\n「閉じる(B)」を押してカメラに手を映してください．\nうまく手が認識されると「学習開始(B)」を押せるようになります．");
    }

    public void ReInput(string popuptext = "まずはデフォルトのポーズに対応する手の形を決めましょう。", bool _onceLearned = true)
    {
        indexLabel.gameObject.SetActive(true);
        onceLearned = _onceLearned;
        chosenMotionIndex = 0;
        ShowPopup(popuptext);
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
            doneButton.GetComponentInChildren<Text>().text = "次に進む(N)";
            playButton.gameObject.SetActive(true);
            indexLabel.text = $"{chosenMotionIndex}/8";
            motionNameLabel.text = Constants.motionNames[(handState)Enum.ToObject(typeof(handState), chosenMotionIndex)];
            SetAllButton(true);
            doneButton.interactable = onceLearned;
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
            ShowPopup("これからあなたと前に使ってくれた方との心のキョリを推定します．\n\n左側にあなたに合わせたモデルが，\n右側に前ユーザに合わせたモデルがいます．\n\n前ユーザの手の動きに対して二人のモデルがどれくらい同じ動きをするか\n見てみましょう");
        }
    }

    public void SetLearnedUI(bool isLearned)
    {
        startLearningButton.GetComponentInChildren<Text>().text = isLearned ? "再学習(B)" : "学習中……";
        SetAllButton(isLearned);
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
            if (!HandDetected)
            {
                IEnumerator _checkCamera = CheckCamera();
                StartCoroutine(_checkCamera);
                HandDetected = true;
            }
        }
    }
    private IEnumerator CheckCamera()
    {
        var jsonstr = GetLocalStorage("handpos");
        while (jsonstr == null)
        {
            jsonstr = GetLocalStorage("handpos");
            yield return null;
        }
        SetAllButton(true);
        doneButton.interactable = onceLearned;
    }

    public void SegueToFinish()
    {
        FadeManager.FadeOut(3);
    }
    public void SetAllButton(bool isinteractable)
    {
        startLearningButton.interactable = isinteractable;
        playButton.interactable = isinteractable;
        doneButton.interactable = isinteractable;
    }
}
