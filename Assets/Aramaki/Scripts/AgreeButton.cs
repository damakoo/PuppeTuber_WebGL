using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgreeButton : MonoBehaviour
{
    public void OnClick()
    {
        // SceneManager.LoadScene("Description");
        FadeManager.FadeOut(1);
    }
}
