using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGameScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioSource.PlayOneShot(AudioClip, 0.5f);
        Debug.Log("mamaipi");
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

        //Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
