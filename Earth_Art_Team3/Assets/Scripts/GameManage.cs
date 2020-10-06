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

    private void Awake()
    {
        instance = this;
    }

    public void PlaceObject()
    {
        if(DraggingObject!=null && CurrentContainer != null)
        {
            GameObject SpawnedPlant = Instantiate(DraggingObject.GetComponent<ObjectDragging>().card.objectGame);
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
        Debug.Log(allContainers);
        foreach (ObjectContainer container in allContainers)
        {
            if (container.isFull)
            {
                container.GetComponentInChildren<PlantGrow>().grow();
            }
        }

        FadeOut();
    }

    public void FadeIn()
    {
        FadeImage.CrossFadeAlpha(1, 2, false);
    }

    public void FadeOut()
    {
        FadeImage.CrossFadeAlpha(0, 2, false);
    }
}
