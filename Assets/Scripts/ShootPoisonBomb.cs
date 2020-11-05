using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoisonBomb : MonoBehaviour{
    public Transform target;
    public LayerMask hitLayerMask;
    public GameObject bomb;

    public float shootForce = 2000f;
    public float blastRadius = 8f;

    public float fireRate;

    private float countdown;
    void Start(){
        target = PlayerManager.instance.player.transform;
        countdown = fireRate;
    }

    void Update(){
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hitInfo, hitLayerMask) && hitInfo.collider.gameObject.tag == "Player" && countdown <= 0){
            countdown = fireRate;
            Vector3 aimTarget = target.GetChild(2).position;
            GameObject cloneBomb = Instantiate(bomb, transform.position + (target.position - transform.position).normalized, Quaternion.LookRotation(aimTarget));
            cloneBomb.GetComponent<PoisonBomb>().blastRadius = blastRadius;
            cloneBomb.GetComponent<Rigidbody>().AddForce((aimTarget - transform.position).normalized * shootForce);
        }
        countdown -= Time.deltaTime;
    }
}
