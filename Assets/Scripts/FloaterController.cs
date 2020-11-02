using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterController : MonoBehaviour{
    public Transform target;  // The target to chase
    public Rigidbody rb;
    public float speed;

    public float chaseDist;  // The distance at which the floater chases the target
    public float recomputePathTimer;  // The period of time in between path recomputations

    public float onPointTolerance;  // The tolerance to determine if the floater has reached a point on the path

    public FlyingNavMesh navmesh;

    private List<Vector3> path;
    private float timer;
    void Start(){
        target = PlayerManager.instance.player.transform;
        path =  new List<Vector3>();
        timer = Random.Range(0, recomputePathTimer);  // Timer begins at a random value to avoid all floaters recomputing at once
    }
    void Update(){  // Keep experimenting with the timer (may need to mess with onPointTolerance, chaseDist, etc. but I feel like it could work).
        if(path.Count > 0 && Vector3.Distance(transform.position, path[0]) <= onPointTolerance){
            path.RemoveAt(0);
        }
        if(path.Count > 0){
            // Potentially remove target from path and say if path is empty go towards target (so it doesn't go to player's old pos)
                // This would probably only make a difference if the timer is used (as is, if path is empty then we generate a new one immediately)
            rb.AddForce((path[0] - transform.position).normalized * speed);
        }else if(Vector3.Distance(transform.position, target.position) <= chaseDist){
            path = navmesh.GetPath(transform.position, target.position);
        }
        /*if(timer <= 0 && Vector3.Distance(transform.position, target.position) <= chaseDist){
            timer = recomputePathTimer;
            path = navmesh.GetPath(transform.position, target.position);
        }
        if(timer > 0){  // If the player is out of range the timer doesn't reset so we don't want it to go too negative
            timer -= Time.deltaTime;
        }*/
    }
}
