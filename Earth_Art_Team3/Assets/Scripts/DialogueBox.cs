using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Dialogue");
        bool hasText = DialogueManager.instance.DisplayNextSentence();
        if(!hasText)
        {
            CustomerScript.instance.giveSeeds();
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
