using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour{
    public Transform target;
    public float damage;
    public float damageTimer;
    public float damageDistance;
    public AudioSource biteSound;

    private CharacterHealth targetHealth;
    private float countdown;
    void Start(){
        target = PlayerManager.instance.player.transform;
        targetHealth = target.GetComponent<CharacterHealth>();
        countdown = damageTimer;
    }

    void Update(){
        if(Vector3.Distance(target.position, transform.position) <= damageDistance && countdown <= 0){
            if (!biteSound.isPlaying){
                biteSound.Play();
            }
            countdown = damageTimer;
            targetHealth.TakeDamage(damage);
        }
        countdown -= Time.deltaTime;
    }
}
