using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWalkerHealth : CharacterHealth{
    protected override void Die(){
        Destroy(gameObject);
    }
}
