using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth{
    public float easyHealth;
    public float mediumHealth;
    public float hardHealth;
    void Start(){
        switch (GameManager.instance.difficulty){
            case 0:
                baseHealth = easyHealth;
                break;
            case 1:
                baseHealth = mediumHealth;
                break;
            case 2:
                baseHealth = hardHealth;
                break;
        }
        base.Start();
    }

    public GameObject drop;
    public int maxDrops;
    public override void Die() {
        int num = Random.Range(0, maxDrops);
        for (int i = 0; i <= num; i++) {
            GameObject dropInfo = Instantiate(drop, transform.position, Quaternion.identity);
            dropInfo.GetComponent<Rigidbody>().AddForce(Random.Range(5f, 10f) * new Vector3(Random.value, 1.2f * Random.value, Random.value), ForceMode.VelocityChange);
        }
        gameObject.SetActive(false);
    }
    public void Spawn(){
        health = baseHealth;
    }
}
