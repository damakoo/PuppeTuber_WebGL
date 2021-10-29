using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButton : MonoBehaviour
{
  [SerializeField] InputSceneManager sceneManager;

  public void OnClick() {
    sceneManager.IncrementAnimationIndex();
  }
}
