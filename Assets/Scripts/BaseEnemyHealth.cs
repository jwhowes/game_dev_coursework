using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyHealth : CharacterHealth{
    public GameObject drop;
    public int maxDrops;
    protected override void Die(){
        int num = (int)Mathf.Floor(Random.Range(0, maxDrops));
        for (int i = 0; i <= num; i++){
            GameObject dropInfo = Instantiate(drop, transform.position, Quaternion.identity);
            dropInfo.GetComponent<Rigidbody>().AddForce(Random.Range(5f, 10f) * new Vector3(Random.value, 1.2f * Random.value, Random.value), ForceMode.VelocityChange);
        }
        Destroy(gameObject);
    }
}
