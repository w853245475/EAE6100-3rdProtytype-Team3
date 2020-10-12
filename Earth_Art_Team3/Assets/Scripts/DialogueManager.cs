using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text DialogueText;

    private Queue<string> sentences;

    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Dialogue" + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return false;
        }
        string sentence = sentences.Dequeue();
        DialogueText.text = sentence;
        Debug.Log(sentence);
        return true;
    }

    void EndDialogue()
    {
        if(CustomerScript.instance.ArriveTimes == 0)
        {
            GameManage.instance.openBook();
            CustomerScript.instance.customerReceived = true;
            CustomerScript.instance.canStartDialogue = false;
        }

        if (CustomerScript.instance.ArriveTimes == 1)
        {
            CustomerScript.instance.giveSeeds();
            CustomerScript.instance.customerReceived = true;
            CustomerScript.instance.canStartDialogue = false;
            CustomerScript.instance.ClearGround();
            GameManage.instance.canvas.transform.Find("BlueLightSeed").gameObject.SetActive(true);
        }

        if (CustomerScript.instance.ArriveTimes == 2)
        {
            CustomerScript.instance.customerReceived = true;
            CustomerScript.instance.canStartDialogue = false;
            CustomerScript.instance.ClearGround();
            //GameManage.instance.canvas.transform.Find("BlueLightSeed").gameObject.SetActive(false);
        }

        if (CustomerScript.instance.ArriveTimes == 3)
        {
            CustomerScript.instance.customerReceived = true;
            CustomerScript.instance.canStartDialogue = false;
            CustomerScript.instance.ClearGround();
            //GameManage.instance.canvas.transform.Find("Ending").gameObject.SetActive(true);
            GameManage.instance.canvas.transform.Find("BlueLightSeed").gameObject.SetActive(true);
        }

        if (CustomerScript.instance.ArriveTimes == 4)
        {
            CustomerScript.instance.customerReceived = true;
            CustomerScript.instance.canStartDialogue = false;
            CustomerScript.instance.ClearGround();
            GameManage.instance.canvas.transform.Find("Ending").gameObject.SetActive(true);
            //GameManage.instance.canvas.transform.Find("BlueLightSeed").gameObject.SetActive(true);
        }
    }
}
