using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject DraggingObject;
    public GameObject CurrentContainer;

    public static GameManage instance;

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

    public void 
}
