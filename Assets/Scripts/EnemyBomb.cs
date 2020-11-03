using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour{
    public GameObject explosionEffect;
    public float blastRadius = 100f;
    public CharacterHealth playerHealth;
    public float damage;

    void OnCollisionEnter(Collision collision){
        Explode();
    }
    void Explode(){
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider nearbyObject in colliders){
            if(nearbyObject.tag == "Player"){
                playerHealth.TakeDamage(damage/Vector3.Distance(nearbyObject.transform.position, transform.position));  // This is a lot of damage!
            }
        }
        Destroy(gameObject);
    }
}
