using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour{
    // A generic health class with a TakeDamage function and abstract Die function (will add more functionality later)
    public float baseHealth = 100f;

    protected float health;

    void Start(){
        health = baseHealth;
    }

    protected abstract void Die();

    public virtual void TakeDamage(float amount){
        health -= amount;
        if(health <= 0){
            Die();
        }
    }
}
