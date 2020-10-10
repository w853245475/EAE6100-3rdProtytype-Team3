using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image Book;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = 1.5f * this.transform.localScale;



    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = this.transform.localScale/1.5f;
        if (Book.GetComponent<Image>().enabled == false)
        {
            Book.GetComponent<Image>().enabled = true;
        }
        else
        {
            Book.GetComponent<Image>().enabled = false;
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
