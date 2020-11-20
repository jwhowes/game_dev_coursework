using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if (other.GetComponent<PlayerHealth>().Heal(10)){
                Destroy(gameObject);
            }
        }
    }
}
