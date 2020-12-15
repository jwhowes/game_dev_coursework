using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour{
    public void Continue(){
        GameManager.instance.GetComponent<DialogueManager>().NextSentence();
    }

    public void Skip(){
        GameManager.instance.GetComponent<DialogueManager>().EndDialogue();
    }
}
