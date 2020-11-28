using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour{
    public GameObject character;
    public void Spawn(){
        character.SetActive(true);
        character.transform.position = transform.position;
        character.GetComponent<EnemyHealth>().Spawn();
    }
}
