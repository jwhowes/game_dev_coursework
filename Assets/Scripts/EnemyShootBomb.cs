using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBomb : MonoBehaviour{
    public Transform target;
    public LayerMask hitLayerMask;
    public GameObject bomb;

    public float shootForce = 2000f;
    public float blastRadius;
    public float damage;

    protected virtual void Start(){
        target = PlayerManager.instance.player.transform;
    }

    protected void Shoot(){
        Vector3 aimTarget = target.GetChild(2).position;
        Vector3 aimNoise = new Vector3(
            Random.Range(-1, 1),
            Random.Range(-1, 1),
            Random.Range(-1, 1)
            );
        Vector3 shootDirection = (Quaternion.Euler(aimNoise) * (aimTarget - transform.position)).normalized;
        GameObject cloneBomb = Instantiate(bomb, transform.position + (target.position - transform.position).normalized, Quaternion.LookRotation(shootDirection));
        EnemyBomb bombInfo = cloneBomb.GetComponent<EnemyBomb>();
        bombInfo.blastRadius = blastRadius;
        bombInfo.damage = damage;
        cloneBomb.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce);
    }
    protected bool CanSeeTarget() {
        RaycastHit hitInfo = new RaycastHit();
        return Physics.Raycast(new Ray(transform.position, (target.position - transform.position).normalized), out hitInfo, 300f, hitLayerMask) && hitInfo.collider.tag == "Player";
    }
}
