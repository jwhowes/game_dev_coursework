using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerHealth : CharacterHealth{
    protected override void Die(){
        Destroy(gameObject);
    }
}
