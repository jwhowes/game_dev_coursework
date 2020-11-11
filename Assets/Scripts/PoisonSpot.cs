using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpot : MonoBehaviour{
    public float radius;
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

    void Update(){
        if(Vector3.Distance(transform.position, target.position) <= radius){
            targetHealth.TakeDamage(Time.deltaTime);
        }
        if(lifeTime <= 0){
            // Destroy(poisonEffectInstance);
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }
}
