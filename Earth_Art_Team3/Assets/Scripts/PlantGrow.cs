using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantGrow : MonoBehaviour, IPointerDownHandler,  IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public Sprite seedSprite;
    public Sprite midSprite;
    public Sprite finishSprite;

    private bool IsMature = false;
    private bool EndDrag = false;

    int DayPlaced;
    // Start is called before the first frame update
    void Start()
    {
        DayPlaced = GameManage.instance.gameDays;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getIsMature()
    {
        return IsMature;
    }

    public void grow()
    {
        if(GameManage.instance.gameDays - DayPlaced  == 1)
        {
            this.GetComponent<Image>().sprite = midSprite;
        }

        if (GameManage.instance.gameDays - DayPlaced == 2)
        {
            this.GetComponent<Image>().sprite = finishSprite;
            IsMature = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(IsMature)
        {

            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(this.transform.position.x,
                                                    this.transform.position.y,
                                                    0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(IsMature)
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(this.transform.position.x,
                                            this.transform.position.y,
                                            0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Customer")
        {
            if (IsMature)
            {
                
                if (EndDrag)
                {
                    Debug.Log("Die");
                    this.GetComponentInParent<ObjectContainer>().isFull = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
