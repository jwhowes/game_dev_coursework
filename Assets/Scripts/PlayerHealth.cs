﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth{
    // An implementation of the abstract CharacterHealth class for the player
    public Slider healthBar;

    public Vector3 spawnPoint = new Vector3(0f, 5f, 0f);

    public BattleArena arena;

    public override void Start(){
        if(arena != null){
            arena.Activate();
        }
        base.Start();
        healthBar.maxValue = baseHealth;
        healthBar.value = baseHealth;
    }
    override public void Die(){
        if(arena != null){
            arena.Activate();
        }
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
