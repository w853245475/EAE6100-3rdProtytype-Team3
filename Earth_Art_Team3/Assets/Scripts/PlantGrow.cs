using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    public Sprite seedSprite;
    public Sprite midSprite;
    public Sprite finishSprite;




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

    public void grow()
    {
        if(GameManage.instance.gameDays - DayPlaced  == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = midSprite;
        }

        if (GameManage.instance.gameDays - DayPlaced == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = finishSprite;
        }
    }
}
