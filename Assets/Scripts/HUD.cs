using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour{
    void Start(){
        GameManager.instance.HUD = gameObject.transform.GetChild(0).gameObject;
        GameManager.instance.DialogueCanvas = gameObject.transform.GetChild(1).gameObject;
        GameManager.instance.playerDamage = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        GameManager.instance.playerDeadUI = gameObject.transform.GetChild(0).GetChild(3).gameObject;
    }
}
