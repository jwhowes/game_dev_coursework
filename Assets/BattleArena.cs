using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour{
    public EnemySpawnPoint[] enemySpawnPoints;

    public void Activate(){
        foreach(EnemySpawnPoint esp in enemySpawnPoints){
            esp.Spawn();
        }
    }

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player"){
            PlayerHealth ph = collider.GetComponent<PlayerHealth>();
            if(ph.arena != this){
                collider.GetComponent<PlayerHealth>().arena = this;
                Activate();
            }
        }
    }
}
