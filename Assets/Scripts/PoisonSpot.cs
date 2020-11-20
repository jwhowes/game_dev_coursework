using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpot : MonoBehaviour{
    public float lifeTime = 3f;
    public GameObject poisonEffect;
    public Transform target;

    private GameObject poisonEffectInstance;
    private CharacterHealth targetHealth;
    void Start(){
        //poisonEffectInstance = Instantiate(poisonEffect, transform.position, transform.rotation);
        target = PlayerManager.instance.player.transform;
        targetHealth = target.GetComponent<CharacterHealth>();
    }
    
    void OnTriggerStay(Collider other){
        if(other.tag == "Player"){
            targetHealth.TakeDamage(Time.deltaTime * 5f);
        }
    }

    void Update(){
        if(lifeTime <= 0){
            // Destroy(poisonEffectInstance);
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }
}
