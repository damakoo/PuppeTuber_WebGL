using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Proceed()
    {
        // SceneManager.LoadScene("Description");
        FadeManager.FadeOut(3);
    }
}
