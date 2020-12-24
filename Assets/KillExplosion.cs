using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillExplosion : MonoBehaviour{
    public float lifeTime;
    void Update(){
        if(lifeTime <= 0f){
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }
}
