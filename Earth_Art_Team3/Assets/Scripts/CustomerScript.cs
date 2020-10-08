using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static CustomerScript instance;
    
    public int TodayFlowerCount = 0;


    public const float customerWaitPos = 18f;
    public const float customerArrivePos = 13f;

    public bool customerArrived = false;
    public bool customerReceived = false;

    private void Awake()
    {
        instance = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Plants")
        {
            

        }

        if(TodayFlowerCount >= 2)
        {
            customerReceived = true;
        }
    }

    public void SetSail()
    {
        if (this.GetComponent<Transform>().position.x >= customerWaitPos)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
        }
        else if (this.GetComponent<Transform>().position.x <= customerArrivePos)
        {
            customerArrived = true;
            if (customerReceived == true)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
            }

            else
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }
}
