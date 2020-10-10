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



    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;

    [Header("Bee")]
    public GameObject Bee;
    public GameObject BeeContainer;

    [Header("Plants")]
    public GameObject WhiteRose;
    public GameObject RedRose;
    public GameObject obj;
    
    private int spawnBeeCounter = 0;
    private int spawnBeeTime = 1500;

    public int MaxiumIncreaseFlower = 3;
    public int TodayIncreaseFlower = 0;

    private bool canSpawnBee = true;
    private bool canSpawnFlower = true;

    [Header("Spawn Plant")]
    public List<GameObject> FlowersToSpawn;
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
        
        canSpawnBee = false;
        FadeIn();
  
        StartCoroutine(Sleep());
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(5);
        gameDays++;

        foreach (GameObject obj in FlowersToSpawn)
        {
            SpawnFlower(obj);
        }
        GrowFlower();
        FlowersToSpawn.Clear();
        TodayIncreaseFlower = 0;
        

        FadeOut();

    }

    IEnumerator Wake()
    {
        yield return new WaitForSeconds(3);
        FadeImage.enabled = false;
        canSpawnBee = true;
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
        GameObject currentBee = Instantiate(Bee, BeeContainer.transform);
    }


    private void Update()
    {
        //Debug.Log(customer.GetComponent<Transform>().position.x);
        if(gameDays == 1)
        {
            customer.GetComponent<CustomerScript>().SetSail();

            
        }
        else
        {
            /*
            if (customer.GetComponent<Transform>().position.x <= customerArrivePos)
            {
                if (customerReceived == true)
                {
                    customer.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
                }

                else
                    customer.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            }
            */
        }

        if (canSpawnBee)
        {
            if (spawnBeeCounter <= spawnBeeTime)
                spawnBeeCounter++;
            else
            {
                spawnBeeCounter = 0;
                SpawnBee();
            }
        }
    }

    private void Start()
    {

    }

    void SpawnFlower(GameObject flower)
    {
        Debug.Log(TodayIncreaseFlower);
        ObjectContainer[] allContainers = TotalContainer.GetComponentsInChildren<ObjectContainer>();
        //int spawnedFlowerCount = 0;
        foreach (ObjectContainer container in allContainers)
        {
            //if (spawnedFlowerCount >= TodayIncreaseFlower)
            //    break;
            if (!container.isFull)
            {
                GameObject SpawnedPlant = Instantiate(flower, canvas.transform);
                SpawnedPlant.GetComponent<PlantGrow>().DayPlaced = gameDays - 1;
                SpawnedPlant.transform.SetParent(container.transform);
                SpawnedPlant.transform.position = container.transform.position;

                container.GetComponent<ObjectContainer>().isFull = true;
                //spawnedFlowerCount++;
                return;
            }
        }
    }

    void GrowFlower()
    {
        ObjectContainer[] allContainers = TotalContainer.GetComponentsInChildren<ObjectContainer>();
        foreach (ObjectContainer container in allContainers)
        {
            if (container.isFull)
            {
                container.GetComponentInChildren<PlantGrow>().grow();
            }
        }
    }
 
}
