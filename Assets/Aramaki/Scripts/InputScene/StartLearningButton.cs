using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLearningButton : MonoBehaviour
{
  [SerializeField] SVMmanager svmManager;

  public void OnClick()
  {
    svmManager.StartLearning();
  }
}
