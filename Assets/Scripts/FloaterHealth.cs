using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterHealth : CharacterHealth{
    protected override void Die(){
        Destroy(gameObject);
    }
}
