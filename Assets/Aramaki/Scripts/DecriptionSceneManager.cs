using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecriptionSceneManager : MonoBehaviour
{
  public Text text;
  string[] introArray = {
    "本作品では，あなたがアバターを手で直感的に操作できるようになることを目指します．\n完成した操作方法をもとに，以前この作品を体験した人とあなたの「直感性のキョリ」を測定します．", 
    "最初に，あなただけの操作方法を作りましょう．\n\nこれからアバターが出てきて，全部で8つのモーションを行います．\n自分がそのアバターを手で操作しているつもりになって，アバターの動きと一緒に手を動かしてください．"
  };
  string[] compareArray = {
    "あなただけの操作方法が完成しました．\n\nこの操作方法と，以前この作品を体験した人が作成した操作方法を比較することで，二者間の「直感性のキョリ」を測定してみましょう．",
    "比較画面に関する説明が入る"
  };
  int state = 0;

    // Start is called before the first frame update
  void Start()
  {
    FadeManager.FadeIn();
  }

  // Update is called once per frame
  void Update()
  {
    string[] textArray = introArray;
    if (Input.GetMouseButtonDown(0)) {
      if (state < textArray.Length - 1){
        state += 1;
      } else {
        FadeManager.FadeOut(2);
      }
    }
    text.text = textArray[state];
  }
}
