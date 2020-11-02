using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject bomb;
    public float shootForce = 2000f;
    private float countdown;

    void Start(){
        Physics.IgnoreLayerCollision(8, 9);
    }

    void Update(){
        if(Input.GetButton("Fire1") && countdown <= 0){
            countdown = fireRate;
            GameObject cloneBomb = Instantiate(bomb, transform.position + transform.forward, transform.rotation);
            cloneBomb.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        }
        countdown -= Time.deltaTime;
    }
}
