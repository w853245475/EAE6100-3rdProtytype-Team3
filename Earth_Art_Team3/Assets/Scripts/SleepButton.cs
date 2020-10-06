using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SleepButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image FadeImage;
    // Start is called before the first frame update
    void Start()
    {
        FadeImage.canvasRenderer.SetAlpha(0.0f);
        FadeImage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FadeImage.enabled = true;
        FadeImage.canvasRenderer.SetAlpha(0.0f);

        GameManage.instance.DayEndCalculation();
    }
}
