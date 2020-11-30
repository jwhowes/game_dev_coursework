using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterShoot : EnemyShootBomb{
    public float mainFireRate;
    public float subFireRate;
    public int subProjectiles;

    public float spawnMaxTimer;
    public float spawnMinTimer;
    public GameObject smallFloater;

    private float spawnTimer;
    private float mainCountdown = 0;
    private float subCountdown = 0;
    private bool firingSub = false;
    private int subFired = 0;
    void Update(){
        if(CanSeeTarget()){
            if(mainCountdown <= 0){
                firingSub = true;
                subFired = 0;
                mainCountdown = mainFireRate;
            }
            if(spawnTimer <= 0){
                Instantiate(smallFloater, transform.position + (new Vector3(Random.value, Random.value, Random.value)).normalized, Quaternion.identity);
                spawnTimer = Random.Range(spawnMinTimer, spawnMaxTimer);
            }
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
        spawnTimer -= Time.deltaTime;
    }
}