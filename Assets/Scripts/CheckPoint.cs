using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Debug.Log("Reset spawnpoint");
            other.GetComponent<PlayerHealth>().spawnPoint = transform.position;
        }
    }
}
