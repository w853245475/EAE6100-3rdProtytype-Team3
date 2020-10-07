using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeeScript : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IPointerUpHandler
{
    public Sprite FirstFrame;
    public Sprite LastFrame;

    public Sprite FirstFrame2;
    public Sprite LastFrame2;

    private int animCounter = 0;
    private int animChange = 20;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;

    private bool UseBee = true;
    private bool isOnPlant = false;
    private bool isPlayAnim = true;
    private GameObject Flower = null;
    // Start is called before the first frame update
    void Start()
    {
        int decideSprite = Random.Range(0, 2);
        if (decideSprite == 1)
            UseBee = false;
        else
            UseBee = true;

        int position = Random.Range(0, 4);
        switch (position)
        {
            case 3:
                this.transform.position = SpawnPoint4.transform.position;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 5.0f, 0.0f);
                break;

            case 0:
                this.transform.position = SpawnPoint1.transform.position;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(5.0f, 0.0f, 0.0f);
                break;

            case 1:
                this.transform.position = SpawnPoint2.transform.position;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -5.0f, 0.0f);
                break;

            case 2:
                this.transform.position = SpawnPoint3.transform.position;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(-5.0f, 0.0f, 0.0f);
                break;
        }
        
        if (this.GetComponent<Rigidbody2D>().velocity.x >= 0)
        {
            if (Mathf.Abs(180 - this.GetComponent<Image>().transform.rotation.y) >= 1)
            {
                this.GetComponent<Image>().transform.Rotate(new Vector3(
                    this.GetComponent<Image>().transform.rotation.x,
                    180,
                    this.GetComponent<Image>().transform.rotation.z
                    ));
            }
        }
        else
        {
            if (Mathf.Abs(0 - this.GetComponent<Image>().transform.rotation.y) >= 1)
            {
                this.GetComponent<Image>().transform.Rotate(new Vector3(
                    this.GetComponent<Image>().transform.rotation.x,
                    0,
                    this.GetComponent<Image>().transform.rotation.z
                    ));
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (animCounter <= animChange)
        {
            animCounter++;
        }
        else
        {
            if (isPlayAnim)
            {


                if (UseBee)
                {
                    if (this.GetComponent<Image>().sprite == FirstFrame)
                        this.GetComponent<Image>().sprite = LastFrame;
                    else
                        this.GetComponent<Image>().sprite = FirstFrame;
                    animCounter = 0;
                }
                else
                {
                    if (this.GetComponent<Image>().sprite == FirstFrame2)
                        this.GetComponent<Image>().sprite = LastFrame2;
                    else
                        this.GetComponent<Image>().sprite = FirstFrame2;
                    animCounter = 0;
                }
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(this.transform.position.x,
                                                this.transform.position.y,
                                                0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

   

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Yes!!!!");

        this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(this.transform.position.x,
                                                this.transform.position.y,
                                                0);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isOnPlant)
        {
            isPlayAnim = false;
            this.transform.position = Flower.transform.position;
            StartCoroutine(StayOnFlower());
        }

    }

    IEnumerator StayOnFlower()
    {
        yield return new WaitForSeconds(3);
        isPlayAnim = true;

        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 3:
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 5.0f, 0.0f);
                break;

            case 0:
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(5.0f, 0.0f, 0.0f);
                break;

            case 1:
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -5.0f, 0.0f);
                break;

            case 2:
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(-5.0f, 0.0f, 0.0f);
                break;
        }

        if (GameManage.instance.TodayIncreaseFlower < GameManage.instance.MaxiumIncreaseFlower)
            GameManage.instance.TodayIncreaseFlower++;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Plants")
        {
            isOnPlant = true;
            Flower = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plants")
        {
            isOnPlant = false;
            Flower = null;
        }
    }
}
