using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDash : MonoBehaviour{
    bool triggered = false;
    void OnTriggerEnter(Collider other){
        if(!triggered && other.tag == "Player"){
            other.GetComponent<PlayerMovement>().numDashes += 1;
            triggered = true;

        }
    }
}
