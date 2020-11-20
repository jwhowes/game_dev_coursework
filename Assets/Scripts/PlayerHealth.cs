﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth{
    // An implementation of the abstract CharacterHealth class for the player
    public Slider healthBar;
    
    private Vector3 spawnPoint = new Vector3(0f, 5f, 0f);
    public override void Start(){
        base.Start();
        healthBar.maxValue = baseHealth;
        healthBar.value = baseHealth;
    }
    override protected void Die(){
        health = baseHealth;
        transform.position = spawnPoint;
    }
    public bool Heal(float amount){
        if(health < baseHealth){
            health = (health + amount <= baseHealth) ? health + amount : baseHealth;
            healthBar.value = health;
            return true;
        }
        return false;
    }
    public override void TakeDamage(float amount){
        GameManager.instance.PlayerDamageAnim();
        base.TakeDamage(amount);
        healthBar.value = health;
    }
}
