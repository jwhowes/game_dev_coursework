using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;  // I should probably make my own explosion effect
    public float blastRadius = 10f;
    public float force = 700f;
    public float lifeTime = 3f;

    private float countdown;
    void Start(){
        countdown = lifeTime;
    }
    void OnCollisionEnter(Collision collision){
        Explode();
    }
    void Explode(){
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider nearbyObject in colliders){
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Transform trans = nearbyObject.GetComponent<Transform>();
            if(rb != null){
                rb.AddExplosionForce(force, transform.position, blastRadius*10);
                rb.AddForce(trans.up * 10f);
            }
            if(nearbyObject.tag == "Player"){
                nearbyObject.GetComponent<PlayerMovement>().Launch();
            }
            if(nearbyObject.tag == "Enemy"){
                // Need to rework damage system (direct hits should do more regardless of enemy model size)
                nearbyObject.GetComponent<CharacterHealth>().TakeDamage(30/Vector3.Distance(nearbyObject.transform.position, transform.position));
            }
            
        }
        Destroy(gameObject);
    }
    void Update(){
        if(countdown <= 0){
            Destroy(gameObject);
        }
        countdown -= Time.deltaTime;
    }
}
