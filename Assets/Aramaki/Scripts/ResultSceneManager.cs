using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
  [SerializeField] Text resultLabel;
  public static double result = 0;
  // Start is called before the first frame update
  void Start()
  {
    FadeManager.FadeIn();
    int intResult = (int)(result * 100);
    resultLabel.text = $"{intResult}/100";
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void Restart() {
    FadeManager.FadeOut(3);
  }
}
