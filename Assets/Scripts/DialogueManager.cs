using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour{
    private Queue<string> sentences;
    void Start(){
        sentences = new Queue<string>();
    }
    public void StartDialogue(string[] inpSentences){
        foreach(string s in inpSentences){
            sentences.Enqueue(s);
        }
        GameManager.instance.HUD.SetActive(false);
        GameManager.instance.DialogueCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        PlayerManager.instance.player.GetComponentInChildren<FireBomb>().canShoot = false;
        NextSentence();
    }
    public void NextSentence(){
        if(sentences.Count > 0){
            GameManager.instance.DialogueCanvas.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = sentences.Dequeue();
        }else{
            EndDialogue();
        }
    }
    public void EndDialogue(){
        sentences.Clear();
        GameManager.instance.HUD.SetActive(true);
        GameManager.instance.DialogueCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        PlayerManager.instance.player.GetComponentInChildren<FireBomb>().canShoot = true;
    }
}