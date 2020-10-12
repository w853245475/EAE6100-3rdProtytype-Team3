using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public static CustomerScript instance;
    
    public int TodayFlowerCount = 0;


    public const float customerWaitPos = 20f;
    public const float customerArrivePos = 13.9f;

    public bool customerArrived = false;
    public bool customerReceived = false;

    public bool canStartDialogue = false;

    public GameObject DialogueBox;

    public GameObject RedSeed;
    public GameObject MeatSeed;
    public GameObject StrangeSeed;
    public Canvas canvas;

    public int ArriveTimes;

    GameObject currentSeed;

    public GameObject PlantContainer;

    public bool IsSetSail = false;

    private string[] Day1Sentences = {"I want a flower like this.",
                                      "Can you try to plant it for me?",
                                      "I will pick it up in a few days.",
                                      "I found a hybrid manual, but many places seem to be contaminated with ink and cannot be seen clearly",
                                      "but I think you can still get some useful information on it."};

    private string[] Arrive2Sentences = {"I got some special seeds last week, but I don’t know what genotype they are.",
                                      "Can you help me plant them? See you."
                                      };

    private string[] Arrive3Sentences = {"These orange flowers are so beautiful.",
                                      "I want three of this flower! ",
                                      "Can you help me think about which two plants can be crossed to get this kind of flower?"
                                      };

    private string[] Arrive4Sentences = {"I want a blue flower with a special pattern (RRYYWW). ",
                                         "This is the photo. Can you help me figure out how to plant it?"
                                      };

    private string[] Arrive5Sentences = { "Ha! I guess you don’t know.",
                                        "Actually, I am Mendel, I invite you to come here just want to let you learn something about Mendel’s law!",
                                        "Congratulations you have completely mastered it!",
                                        "These are all the flowers you plant for me. I saved them for you. Now I'll give it back to you as a reward.",
                                        "I Hope you can keep learning genetics in the future! Bye!"};

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
        ArriveTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Transform>().position.x >= customerWaitPos)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
        else if (this.GetComponent<Transform>().position.x <= customerArrivePos)
        {
             this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }

        if(ArriveTimes == 0 && GameManage.instance.gameDays == 0 && !IsSetSail)
        {
            //SetSail();
            AudioSource.PlayOneShot(AudioClip, 0.5f);
            IsSetSail = true;
            this.GetComponent<DialogueTrigger>().dialogue.sentences = Day1Sentences;
        }
        if(ArriveTimes == 1 && !IsSetSail)
        {
            Debug.Log("arr" + ArriveTimes);
            ObjectContainer[] allContainers = PlantContainer.GetComponentsInChildren<ObjectContainer>();
            foreach (ObjectContainer container in allContainers)
            {
                if (container.isFull)
                {
                    PlantGrow currentPlant = container.GetComponentInChildren<PlantGrow>();
                    if (currentPlant.FlowerName == "OrangeLight" && currentPlant.IsMature)
                    {
                        Debug.Log("arr");
                        //SetSail();
                        AudioSource.PlayOneShot(AudioClip, 0.5f);
                        IsSetSail = true;
                        this.GetComponent<DialogueTrigger>().dialogue.sentences = Arrive2Sentences;
                        canStartDialogue = true;
                        break;
                    }
                }
            }
        }

        if (ArriveTimes == 2 && !IsSetSail)
        {
            ObjectContainer[] allContainers = PlantContainer.GetComponentsInChildren<ObjectContainer>();
            int ThridArriveCounter = 0;
            foreach (ObjectContainer container in allContainers)
            {
                if (container.isFull)
                {
                    PlantGrow currentPlant = container.GetComponentInChildren<PlantGrow>();
                    if (currentPlant.FlowerName == "BlueLight" && currentPlant.IsMature)
                    {
                        GameManage.instance.canvas.transform.Find("BlueLightSeed").gameObject.SetActive(false);
                        IsSetSail = true;
                        AudioSource.PlayOneShot(AudioClip, 0.5f);
                        this.GetComponent<DialogueTrigger>().dialogue.sentences = Arrive3Sentences;
                        canStartDialogue = true;
                        break;
                    }
                }
            }
        }

        if (ArriveTimes == 3 && !IsSetSail)
        {
            ObjectContainer[] allContainers = PlantContainer.GetComponentsInChildren<ObjectContainer>();
            int ThridArriveCounter = 0;
            foreach (ObjectContainer container in allContainers)
            {
                if (container.isFull)
                {
                    PlantGrow currentPlant = container.GetComponentInChildren<PlantGrow>();
                    if (currentPlant.FlowerName == "BlueLight" && currentPlant.IsMature)
                    {
                        ThridArriveCounter++;             
                    }
                    if(ThridArriveCounter >= 3)
                    {
                        GameManage.instance.canvas.transform.Find("BlueLightSeed").gameObject.SetActive(true);
                        IsSetSail = true;
                        AudioSource.PlayOneShot(AudioClip, 0.5f);
                        this.GetComponent<DialogueTrigger>().dialogue.sentences = Arrive4Sentences;
                        canStartDialogue = true;
                        break;
                    }
                }
            }
        }

        //if (ArriveTimes == 3 && !IsSetSail)
        //{
        //    ObjectContainer[] allContainers = PlantContainer.GetComponentsInChildren<ObjectContainer>();
        //    foreach (ObjectContainer container in allContainers)
        //    {
        //        if (container.isFull)
        //        {
        //            PlantGrow currentPlant = container.GetComponentInChildren<PlantGrow>();
        //            if (currentPlant.FlowerName == "BlueLight" && currentPlant.IsMature)
        //            {
        //                Debug.Log("arr123");
        //                AudioSource.PlayOneShot(AudioClip, 0.5f);
        //                IsSetSail = true;
        //                this.GetComponent<DialogueTrigger>().dialogue.sentences = Arrive4Sentences;
        //                canStartDialogue = true;
        //                break;
        //            }
        //        }
        //    }
        //}

        if (ArriveTimes == 4 && !IsSetSail)
        {
            ObjectContainer[] allContainers = PlantContainer.GetComponentsInChildren<ObjectContainer>();
            foreach (ObjectContainer container in allContainers)
            {
                if (container.isFull)
                {
                    PlantGrow currentPlant = container.GetComponentInChildren<PlantGrow>();
                    if (currentPlant.FlowerName == "BlueDark" && currentPlant.IsMature)
                    {
                        Debug.Log("arr12333");
                        AudioSource.PlayOneShot(AudioClip, 0.5f);
                        IsSetSail = true;
                        this.GetComponent<DialogueTrigger>().dialogue.sentences = Arrive5Sentences;
                        canStartDialogue = true;
                        break;
                    }
                }
            }
        }


        if (IsSetSail)
        {  SetSail(); }
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
                //IsSetSail = false;
            }

            else
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }


    public void giveSeeds()
    {
        if(ArriveTimes == 1)
        {
            currentSeed = Instantiate(StrangeSeed, canvas.transform);
            currentSeed.gameObject.SetActive(true);
            currentSeed.GetComponent<ObjectCaller>().IsGivenByCustomer = true;
            currentSeed.GetComponent<ObjectCaller>().SeedTag = 0;
            
        }
        Debug.Log("asd");
        currentSeed.transform.position = this.transform.position;
        currentSeed.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -20.0f);
        if(currentSeed.GetComponent<ObjectCaller>().canvas == null)
        {
            currentSeed.GetComponent<ObjectCaller>().canvas = canvas;
        }

        StartCoroutine(SeedMoveTime());
    }

    IEnumerator SeedMoveTime()
    {
        yield return new WaitForSeconds(0.3f);
        currentSeed.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    public void ClearGround()
    {
        ObjectContainer[] allContainers = PlantContainer.GetComponentsInChildren<ObjectContainer>();
        foreach (ObjectContainer container in allContainers)
        {
            if (container.isFull)
            {
                container.isFull = false;
                Destroy(container.transform.GetChild(0).gameObject);
            }
        }
    }
}
