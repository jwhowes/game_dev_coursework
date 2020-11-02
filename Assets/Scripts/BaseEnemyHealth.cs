using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyHealth : CharacterHealth{
    protected override void Die(){
        Destroy(gameObject);
    }
}
