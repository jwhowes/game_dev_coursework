using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    private static GameManager _instance;
    public static GameManager instance{
        get{
            return _instance;
        }
    }
    private void Awake(){
        _instance = this;
        Physics.IgnoreLayerCollision(13, 16);
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(14, 16);
        Physics.IgnoreLayerCollision(14, 12);
        Physics.IgnoreLayerCollision(14, 11);
        playerDead = false;
        if(arena != null){
            arena.Activate();
        }
        difficulty = 1;
        DontDestroyOnLoad(this.gameObject);
    }

    public int difficulty;

    public Animator playerDamage;

    public void PlayerDamageAnim(){
        playerDamage.Play("PlayerDamage");
    }

    public void WinLevel(){
        Time.timeScale = 0;
        levelWon = true;
        DialogueCanvas.SetActive(false);
        winCanvas.SetActive(true);
    }

    public BattleArena arena;

    public GameObject playerDeadUI;
    public GameObject HUD;
    public GameObject DialogueCanvas;
    public GameObject winCanvas;

    bool levelWon;
    bool playerDead;

    public void PlayerDeath(){
        Time.timeScale = 0;
        playerDead = true;
        playerDeadUI.SetActive(true);
    }

    public bool AnyAlive(){
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            if (playerDead){
                Time.timeScale = 1;
                playerDead = false;
                playerDeadUI.SetActive(false);
                PlayerManager.instance.player.GetComponent<PlayerHealth>().Respawn();
                if (arena != null){
                    arena.Activate();
                }
            }
            if (levelWon){
                Time.timeScale = 1;
                levelWon = false;
                winCanvas.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            // Should probably also destroy drops
        }
        if(arena != null && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Debug.Log("They're all dead!");
            arena.Kill();
            arena = null;
        }
    }
}