using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        CharacterHealth ch = other.GetComponent<CharacterHealth>();
        if(ch != null){
            ch.Die();
        }
    }
}
