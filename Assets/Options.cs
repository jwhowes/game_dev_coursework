using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour{
    public Button easy;
    public Button medium;
    public Button hard;

    public void Highlight(){
        switch (GameManager.instance.difficulty){
            case 0:
                easy.Select();
                break;
            case 1:
                medium.Select();
                break;
            case 2:
                hard.Select();
                break;
        }
    }
    public void ChangeDifficulty(int newDiff){
        GameManager.instance.difficulty = newDiff;
        Highlight();
    }
}
