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
    }
    public Animator playerDamage;

    public void PlayerDamageAnim(){
        playerDamage.Play("PlayerDamage");
    }
}