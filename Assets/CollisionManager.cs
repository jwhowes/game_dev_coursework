using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour{
    void Start(){  // Manages the collisions between layers
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(8, 9);
    }
}
