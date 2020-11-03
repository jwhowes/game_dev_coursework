using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterShoot : EnemyShootBomb{
    public float mainFireRate;
    public float subFireRate;
    public int subProjectiles;

    private float mainCountdown = 0;
    private float subCountdown = 0;
    private bool firingSub = false;
    private int subFired = 0;
    void Update(){
        if(CanSeeTarget() && mainCountdown <= 0){
            firingSub = true;
            subFired = 0;
            mainCountdown = mainFireRate;
        }
        if(firingSub && subFired < subProjectiles && subCountdown <= 0){
            subCountdown = subFireRate;
            subFired += 1;
            Shoot();
        }
        if(firingSub && subFired >= subProjectiles){
            firingSub = false;
        }
        mainCountdown -= Time.deltaTime;
        subCountdown -= Time.deltaTime;
    }
}
