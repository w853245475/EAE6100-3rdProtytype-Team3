using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepButton : MonoBehaviour
{
    public Image FadeImage;
    // Start is called before the first frame update
    void Start()
    {
        FadeImage.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeIn()
    {
        FadeImage.CrossFadeAlpha(1, 2, false);
    }

    void FadeOut()
    {
        FadeImage.CrossFadeAlpha(0, 2, false);
    }
    
}
