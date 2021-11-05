using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopupManager : MonoBehaviour
{
  [SerializeField] InputSceneManager sceneManager;
  public GameObject popup;
    private Button thisbutton;
    private void Start()
    {
        thisbutton = GetComponent<Button>();
    }
    private void Update()
    {
        if (thisbutton.interactable && Input.GetKeyDown(KeyCode.B)) thisbutton.onClick.Invoke();
    }


    public void OnClick() {
        Debug.Log("pressed");
        sceneManager.ClosePopup();
    }
}
