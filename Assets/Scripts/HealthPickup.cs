using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour{
    public float lifeTime;
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if (other.GetComponent<PlayerHealth>().Heal(10)){
                Destroy(gameObject);
            }
        }
    }

    void Update(){
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0){
            Destroy(gameObject);
        }
    }
}
