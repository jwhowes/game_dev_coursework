using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour{
    public EnemySpawnPoint[] enemySpawnPoints;

    public void Activate() {
        foreach (EnemySpawnPoint esp in enemySpawnPoints) {
            esp.Spawn();
        }
    }
    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player"){
            if(GameManager.instance.arena != this){
                GameManager.instance.arena = this;
                Activate();
            }
        }
    }
}
