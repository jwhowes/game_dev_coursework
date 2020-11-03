using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBomb : MonoBehaviour{
    public float fireRate;
    public Transform target;
    public LayerMask hitLayerMask;
    public GameObject bomb;
    public float shootForce = 2000f;

    public float blastRadius;
    public float damage;

    private float countdown;
    void Start(){
        target = PlayerManager.instance.player.transform;  // Aim at player's feet (the GroundCheck object)
        Physics.IgnoreLayerCollision(11, 12);
        countdown = fireRate;
    }

    void Update(){
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hitInfo, hitLayerMask) && hitInfo.collider.gameObject.tag == "Player" && countdown <= 0){
            // Floater can see player and has shoot available
            Vector3 aimTarget = target.GetChild(2).position;  // Aim for player's feet
            countdown = fireRate;
            GameObject cloneBomb = Instantiate(bomb, transform.position + (target.position - transform.position).normalized, Quaternion.LookRotation(aimTarget - transform.position));
            EnemyBomb bombInfo = cloneBomb.GetComponent<EnemyBomb>();
            bombInfo.blastRadius = blastRadius;
            bombInfo.damage = damage;
            cloneBomb.GetComponent<Rigidbody>().AddForce((aimTarget - transform.position).normalized * shootForce);
        }
        countdown -= Time.deltaTime;
    }
}
