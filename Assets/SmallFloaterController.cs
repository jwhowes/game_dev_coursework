using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFloaterController : MonoBehaviour{
    public Rigidbody rb;
    public float speed;
    public float lifeSpan;
    public float blastRadius;
    public float baseDamage;
    public GameObject target;
    public GameObject explosionEffect;

    private float age;
    void Start(){
        target = PlayerManager.instance.player;
        age = lifeSpan;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Explode();
        }
    }

    void OnTriggerStay(Collider other){
        rb.AddForce((transform.position - other.ClosestPoint(transform.position)).normalized * speed, ForceMode.Acceleration);
    }

    void Explode(){
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        if(Vector3.Distance(transform.position, target.transform.position) <= blastRadius){
            target.GetComponent<CharacterHealth>().TakeDamage(baseDamage / Vector3.Distance(target.transform.position, transform.position));
        }
        Destroy(gameObject);
    }

    void Update(){
        if(age <= 0){
            Explode();
        }
        rb.AddForce((target.transform.position - transform.position).normalized * speed);
        age -= Time.deltaTime;
    }
}
