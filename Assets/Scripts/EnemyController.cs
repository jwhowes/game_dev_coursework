using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    void Start(){
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update(){
        agent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));  // A crude way to make them chase the player even when the player's in the air
        if(Vector3.Distance(target.position, transform.position) <= agent.stoppingDistance){
            FaceTarget();
        }
    }
    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
