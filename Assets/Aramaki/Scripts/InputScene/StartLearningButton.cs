using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartLearningButton : MonoBehaviour
{
  [SerializeField] SVMmanager svmManager;
    [SerializeField] GameObject popup;
    private Button thisbutton;
    private void Start()
    {
        thisbutton = GetComponent<Button>();
    }
    private void Update()
    {
        if (!popup.activeSelf && thisbutton.interactable && Input.GetKeyDown(KeyCode.B)) thisbutton.onClick.Invoke();
    }
    public void OnClick()
  {
    svmManager.LearningorInputagain();
  }
}
