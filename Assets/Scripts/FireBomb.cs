using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject bomb;
    public float shootForce = 2000f;
    public AudioSource shootSound;
    public Animation recoilAnimation;
    [System.NonSerialized] public bool canShoot = true;

    private float countdown;

    void Update(){
        if(canShoot && Input.GetButton("Fire1") && countdown <= 0){
            recoilAnimation.Play();
            shootSound.Play();
            countdown = fireRate;
            GameObject cloneBomb = Instantiate(bomb, transform.position + transform.forward, transform.rotation);
            cloneBomb.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        }
        countdown -= Time.deltaTime;
    }
}
