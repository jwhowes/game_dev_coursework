using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    private static GameManager _instance;
    public static GameManager instance{
        get{
            return _instance;
        }
    }
    private void Awake(){
        _instance = this;
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(13, 8);
        Physics.IgnoreLayerCollision(13, 12);
        Physics.IgnoreLayerCollision(13, 13);
        playerDead = false;
    }

    public Animator playerDamage;
    public void PlayerDamageAnim(){
        playerDamage.Play("PlayerDamage");
    }

    public GameObject playerDeadUI;
    bool playerDead;
    public void PlayerDeath(){
        Time.timeScale = 0;
        playerDead = true;
        playerDeadUI.SetActive(true);
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Return) && playerDead) {
            Time.timeScale = 1;
            playerDead = false;
            playerDeadUI.SetActive(false);
            PlayerManager.instance.player.GetComponent<PlayerHealth>().Respawn();
            // Should probably also destroy drops
        }
    }
}