﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour{
    public EnemySpawnPoint[] enemySpawnPoints;

    public bool dead = false;

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
            if (dead){
                Debug.Log("I'm dead!");
            }
        }
    }
}