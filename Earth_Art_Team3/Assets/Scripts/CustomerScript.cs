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

    public bool canStartDialogue = false;

    public GameObject DialogueBox;

    public GameObject RedSeed;
    public GameObject MeatSeed;
    public Canvas canvas;

    GameObject currentSeed;
    private void Awake()
    {
        instance = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = 1.5f * this.transform.localScale;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = this.transform.localScale / 1.5f;
        if (canStartDialogue)
        {
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
            if (!DialogueBox.transform.parent.gameObject.activeInHierarchy)
            {
                DialogueBox.transform.parent.gameObject.SetActive(true);
            }

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
            if(customerReceived == false)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
            }
        }
        else if (this.GetComponent<Transform>().position.x <= customerArrivePos)
        {
            customerArrived = true;
            canStartDialogue = true;
            if (customerReceived == true)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
            }

            else
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }

    public void giveSeeds()
    {
        currentSeed = Instantiate(RedSeed, canvas.transform);
        currentSeed.transform.position = this.transform.position;
        currentSeed.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -20.0f);
        if(currentSeed.GetComponent<ObjectCaller>().canvas == null)
        {
            currentSeed.GetComponent<ObjectCaller>().canvas = canvas;
        }
        currentSeed.GetComponent<ObjectCaller>().IsGivenByCustomer = true;
        currentSeed.GetComponent<ObjectCaller>().SeedTag = 1;
        StartCoroutine(SeedMoveTime());
    }

    IEnumerator SeedMoveTime()
    {
        yield return new WaitForSeconds(0.3f);
        currentSeed.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }
}
