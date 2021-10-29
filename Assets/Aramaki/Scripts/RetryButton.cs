using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
  [SerializeField] ResultSceneManager sceneManager;
  public void OnClick()
  {
    sceneManager.Restart();
  }
}
