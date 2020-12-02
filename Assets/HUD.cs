using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour{
    void Start(){
        GameManager.instance.playerDamage = gameObject.transform.GetChild(1).GetComponent<Animator>();
        GameManager.instance.playerDeadUI = gameObject.transform.GetChild(3).gameObject;
    }
}
