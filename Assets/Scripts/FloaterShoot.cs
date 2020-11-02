using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterShoot : MonoBehaviour{
    public float fireRate;
    public Transform target;
    public LayerMask hitLayerMask;
    public GameObject bomb;
    public float shootForce = 2000f;

    private float countdown;
    void Start(){
        target = PlayerManager.instance.player.transform.GetChild(2);
        Physics.IgnoreLayerCollision(11, 12);
        countdown = fireRate;
    }

    void Update(){
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hitInfo, hitLayerMask) && hitInfo.collider.gameObject.tag == "Player" && countdown <= 0){
            // Floater can see player and has shoot available
            countdown = fireRate;
            GameObject cloneBomb = Instantiate(bomb, transform.position + (target.position - transform.position).normalized, Quaternion.LookRotation(target.position - transform.position));  // Make it face the player
            cloneBomb.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * shootForce);
        }
        countdown -= Time.deltaTime;
    }
}
