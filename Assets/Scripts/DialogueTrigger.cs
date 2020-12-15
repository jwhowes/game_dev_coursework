using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour{
    [TextArea(3, 10)]
    public string[] sentences;

    private DialogueManager manager;

    void Start(){
        manager = GameManager.instance.GetComponent<DialogueManager>();
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            manager.StartDialogue(sentences);
        }
    }
}
