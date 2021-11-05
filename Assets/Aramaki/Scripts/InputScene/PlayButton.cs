using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    public SVMmanager svmManager = null;
    [SerializeField] InputSceneManager sceneManager;
    private Button thisbutton;
    [SerializeField] Button donebutton;
    private void Start()
    {
        thisbutton = GetComponent<Button>();
    }
    private void Update()
    {
        if (thisbutton.interactable && Input.GetKeyDown(KeyCode.V)) thisbutton.onClick.Invoke();
    }
    public void OnClick() {
        sceneManager.isactive_donebutton = donebutton.interactable;
        svmManager.userStudyAnimator.changeAnimationToIndex(sceneManager.chosenMotionIndex);
        sceneManager.SetAllButton(false);
    }
}
