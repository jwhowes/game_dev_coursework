using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerShoot : EnemyShootBomb{
    public float fireRate;

    private float countdown = 0;
    void Update(){
        if(CanSeeTarget() && countdown <= 0){
            countdown = fireRate;
            Shoot();
        }
        countdown -= Time.deltaTime;
    }
}
