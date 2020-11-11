﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBomb : MonoBehaviour{
    public GameObject poisonSpot;
    public float blastRadius;
    public float lifeTime = 3f;

    void OnCollisionEnter(Collision collision){
        // Play a different explosion effect (a more green one probably
        PoisonSpot spot = Instantiate(poisonSpot, transform.position, transform.rotation).GetComponent<PoisonSpot>();
        spot.radius = blastRadius;
        Destroy(gameObject);
    }
    void Update(){
        if(lifeTime <= 0){
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }
}