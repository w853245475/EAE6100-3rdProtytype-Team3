using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public GameObject DraggingObject;
    public GameObject CurrentContainer;

    public static GameManage instance;
    public Image FadeImage;
    public GameObject TotalContainer;
    public int gameDays = 0;

    
    public Canvas canvas;
    public GameObject customer;

    const float customerWaitPos = 18f;
    const float customerArrivePos = 13f;
    private bool customerArrived = false;
    private bool customerReceived = false;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;

    public GameObject Bee;
    
    private int spawnBeeCounter = 0;
    private int spawnBeeTime = 2000;
    private void Awake()
    {
        instance = this;
    }

    public void PlaceObject()
    {
        if(DraggingObject!=null && CurrentContainer != null)
        {
            GameObject SpawnedPlant = Instantiate(DraggingObject.GetComponent<ObjectDragging>().card.objectGame, canvas.transform);
            SpawnedPlant.transform.SetParent(CurrentContainer.transform);
            SpawnedPlant.transform.position = CurrentContainer.transform.position;

            CurrentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
    }

    public void DayEndCalculation()
    {
        gameDays++;
        FadeIn();
  
        StartCoroutine(Sleep());
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(5);

        ObjectContainer[] allContainers = TotalContainer.GetComponentsInChildren<ObjectContainer>();
        foreach (ObjectContainer container in allContainers)
        {
            if (container.isFull)
            {
                container.GetComponentInChildren<PlantGrow>().grow();
            }
        }

        FadeOut();
    }

    IEnumerator Wake()
    {
        yield return new WaitForSeconds(3);
        FadeImage.enabled = false;

    }

    public void FadeIn()
    {
        FadeImage.enabled = true;
        FadeImage.CrossFadeAlpha(1, 2, false);
    }

    public void FadeOut()
    {
        FadeImage.CrossFadeAlpha(0, 2, false);
        StartCoroutine(Wake());
    }

    void SpawnBee()
    {
        GameObject currentBee = Instantiate(Bee, canvas.transform);
    }



    private void Update()
    {
        //Debug.Log(customer.GetComponent<Transform>().position.x);
        if(gameDays == 1)
        {
            if (customer.GetComponent<Transform>().position.x >= customerWaitPos)
            {
                customer.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
                customerArrived = true;
            }
            else if(customer.GetComponent<Transform>().position.x <= customerArrivePos)
            {
                customer.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                if (customerReceived == true)
                    customer.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
            }
        }

        if (spawnBeeCounter <= spawnBeeTime)
            spawnBeeCounter++;
        else
        {
            spawnBeeCounter = 0;
            SpawnBee();
        }
    }
    private void Start()
    {

    }
 
}
