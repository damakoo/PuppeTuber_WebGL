using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
  [SerializeField] InputSceneManager sceneManager;
  public GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        sceneManager.ClosePopup();
    }
}
