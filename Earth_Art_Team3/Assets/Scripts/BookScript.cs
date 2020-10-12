using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image Book;
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = 1.5f * this.transform.localScale;



    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioSource.PlayOneShot(AudioClip, 0.5f);
        this.transform.localScale = this.transform.localScale/1.5f;
        if (Book.gameObject.active == false)
        {
            Book.gameObject.SetActive(true);
        }
        else
        {
            Book.gameObject.SetActive(false);
        }
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
