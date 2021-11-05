using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DoneButton : MonoBehaviour
{
  [SerializeField] InputSceneManager sceneManager;
    private Button thisbutton;
    private void Start()
    {
        thisbutton = GetComponent<Button>();
    }
    private void Update()
    {
        if (thisbutton.interactable && Input.GetKeyDown(KeyCode.N)) thisbutton.onClick.Invoke();
    }


    public void OnClick() {
    sceneManager.IncrementAnimationIndex();
  }
}
