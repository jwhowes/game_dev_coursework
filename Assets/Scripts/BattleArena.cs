using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour{
    public EnemySpawnPoint[] enemySpawnPoints;

    public GameObject[] endObjects;

    bool dead = false;

    public void Activate() {
        foreach (EnemySpawnPoint esp in enemySpawnPoints) {
            esp.Spawn();
        }
    }
    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player"){
            if(GameManager.instance.arena != this && !dead){
                GameManager.instance.arena = this;
                Activate();
            }
        }
    }
    public void Kill(){
        dead = true;
        foreach(GameObject go in endObjects){
            go.SetActive(!go.activeSelf);
        }
    }
}
