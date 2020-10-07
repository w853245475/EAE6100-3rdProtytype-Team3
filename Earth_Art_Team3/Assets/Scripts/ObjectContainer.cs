using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull = false;
    public GameManage gameManager;
    public Image backgroundImage;

    private void Start()
    {
        gameManager = GameManage.instance;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameManager.DraggingObject != null && !isFull)
        {
            gameManager.CurrentContainer = this.gameObject;
            backgroundImage.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.CurrentContainer = null;
        backgroundImage.enabled = false;
    }
}
