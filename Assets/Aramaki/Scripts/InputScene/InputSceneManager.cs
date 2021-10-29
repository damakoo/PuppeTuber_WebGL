using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSceneManager : MonoBehaviour
{
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

  void ShowPopup(string text) {
    popup.SetActive(true);
    popup.GetComponentInChildren<Text>().text = text;
  }

  public void IncrementAnimationIndex() {
    chosenMotionIndex += 1;
    if (chosenMotionIndex == 1) ShowPopup("次に、8つのモーションに対応する手の動きを決めましょう。");
    if (chosenMotionIndex <= 8) {
      Debug.Log("Proceeded to next");
      startLearningButton.interactable = true;
      startLearningButton.GetComponentInChildren<Text>().text = "学習開始";
      // doneButton.interactable = false;
      doneButton.GetComponentInChildren<Text>().text = "次に進む";
      playButton.gameObject.SetActive(true);
      indexLabel.text = $"{chosenMotionIndex}/8";
      motionNameLabel.text = Constants.motionNames[chosenMotionIndex];
    } else if (svmManager._currentstep == Step.Input) {
      doneButton.interactable = false;
      svmManager.StartCalculate();
    } else if (svmManager._currentstep == Step.Output) {
      inputUISet.SetActive(false);
      compareUISet.SetActive(true);
      svmManager.StartReproducInstruction();
      ShowPopup("比較画面の説明を入れる");
    }
  }

  public void SetLearnedUI(bool isLearned) {
    startLearningButton.GetComponentInChildren<Text>().text = isLearned ? "学習済み" : "学習中……";
    if (isLearned) doneButton.interactable = true;
    else startLearningButton.interactable = false;
  }

  public void SetCalculatingUI() {
    playButton.interactable = false;
    doneButton.interactable = false;
  }

  public void SetCalculatedUI() {
    indexLabel.gameObject.SetActive(false);
    playButton.gameObject.SetActive(false);
    doneButton.interactable = true;
    startLearningButton.GetComponentInChildren<Text>().text = "やり直す";
    startLearningButton.interactable = false;
    motionNameLabel.text = "検出中モーション: なし";
    ShowPopup("この画面では，先ほど作成した操作方法を自由に試すことができます\n\n修正したい場合は「やり直す」ボタンを，これでよければ「次に進む」ボタンを押してください．");
  }

  public void ClosePopup() {
    popup.SetActive(false);
    if (svmManager._currentstep == Step.ReproductInstruction) {
      svmManager.StartReproduction();
    }
  }

  public void SegueToFinish() {
    FadeManager.FadeOut(4);
  }
}
