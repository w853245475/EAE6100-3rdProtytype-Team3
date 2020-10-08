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




    }

    public void OnPointerUp(PointerEventData eventData)
    {
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
