using UnityEngine;
using UnityEngine.UI;
using System;

public class UserStudyAnimator : MonoBehaviour
{
  [SerializeField] Text StateInstructor;
  public Animator useranimation;
    [SerializeField] InputSceneManager sceneManager;
 
  public handState _handState { get; set; } = handState.defaultstate;
  public void changeAnimation()
  {
    if (Input.anyKeyDown)
    {
      foreach (var key in Input.inputString)
      {
        if (int.TryParse(key.ToString(), out var _number))
        {
          _handState = (handState)Enum.ToObject(typeof(handState), _number);
        }
        else if (key == (char)KeyCode.Q)
        {
          _handState = (handState)Enum.ToObject(typeof(handState), 10);
        }
        else if (key == (char)KeyCode.W)
        {
          _handState = (handState)Enum.ToObject(typeof(handState), 11);
        }
        else if (key == (char)KeyCode.E)
        {
          _handState = (handState)Enum.ToObject(typeof(handState), 12);
        }
      }
    }
    else if (useranimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && useranimation.GetCurrentAnimatorStateInfo(0).IsTag(((int)_handState).ToString()))
    {
      _handState = handState.defaultstate;
    }
  }
  private void Update()
  {
    StateInstructor.text = _handState.ToString();
    if (useranimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && useranimation.GetCurrentAnimatorStateInfo(0).IsTag(((int)_handState).ToString()))
    {
     
            if (SVMmanager._currentstep == Step.Input && SVMmanager.isLearning == false && _handState != handState.defaultstate)
            {
                sceneManager.SetAllButton(true);
            }
            _handState = handState.defaultstate;
        }
  }
  private void LateUpdate()
  {
    if (useranimation.isActiveAndEnabled)
    {
      useranimation.SetInteger("handstate", (int)_handState);
    }
  }
  public void EnableUI()
  {
    StateInstructor.enabled = true;
  }
  public void changeAnimationToIndex(int index) {
    _handState = (handState)Enum.ToObject(typeof(handState), index);
  }
}
