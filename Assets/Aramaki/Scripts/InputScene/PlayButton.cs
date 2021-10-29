using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public SVMmanager svmManager = null;
    [SerializeField] InputSceneManager sceneManager;

    public void OnClick() {
        svmManager.userStudyAnimator.changeAnimationToIndex(sceneManager.chosenMotionIndex);
    }
}
