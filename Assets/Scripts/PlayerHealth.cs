using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth{
    // An implementation of the abstract CharacterHealth class for the player
    private Vector3 spawnPoint = new Vector3(0f, 5f, 0f);
    override protected void Die(){
        health = baseHealth;
        transform.position = transform.TransformPoint(spawnPoint);
    }
    public override void TakeDamage(float amount){
        GameManager.instance.PlayerDamageAnim();
        base.TakeDamage(amount);
    }
}
