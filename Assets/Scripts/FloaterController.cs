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

    public LayerMask layerMask;

    [System.NonSerialized] public bool retrieving;

    [System.NonSerialized] public List<Vector3> path;
    private float timer;
    void Start(){
        retrieving = false;
        target = PlayerManager.instance.player.transform;
        path =  new List<Vector3>();
        timer = Random.Range(0, recomputePathTimer);  // Timer begins at a random value to avoid all floaters recomputing at once
    }
    void OnTriggerStay(Collider other){
        rb.AddForce((transform.position - other.ClosestPoint(transform.position)).normalized * speed, ForceMode.Acceleration);
    }
    bool CanSeeTarget(){
        RaycastHit hitInfo = new RaycastHit();
        return Physics.Raycast(new Ray(transform.position, (target.position - transform.position).normalized), out hitInfo, 500f, layerMask) && hitInfo.collider.tag == "Player";
    }
    void FixedUpdate(){  // Keep experimenting with the timer (may need to mess with onPointTolerance, chaseDist, etc. but I feel like it could work).
        if (CanSeeTarget() || navmesh == null){
            path = new List<Vector3>();
            rb.AddForce((target.position - transform.position).normalized * speed);
        }else{
            if (path.Count > 0 && Vector3.Distance(transform.position, path[0]) <= onPointTolerance){
                path.RemoveAt(0);
            }
            if (path.Count > 0){
                // Potentially remove target from path and say if path is empty go towards target (so it doesn't go to player's old pos)
                // This would probably only make a difference if the timer is used (as is, if path is empty then we generate a new one immediately)
                rb.AddForce((path[0] - transform.position).normalized * speed);
            }
            else if (!retrieving && Vector3.Distance(transform.position, target.position) <= chaseDist){
                navmesh.GetPath(this, transform.position, target.position);
            }
        }
    }
}
