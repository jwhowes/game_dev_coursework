using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCell : MonoBehaviour{
    enum State{
        Scanning,
        LockingOn,
        Firing,
        CoolingDown
    }
    public float lockonTime;  // Time taken to lock on
    public float coolDown;  // Time after firing before selecting next target
    public LayerMask targetLayerMask;
    public float range;
    public float damage;
    public LineRenderer line;
    public float pushForce;
    
    private State state;
    private GameObject target = null;

    private LayerMask hitLayerMask;
    private float lockonCountdown;
    private float fireDelayCountdown;
    private float coolDownCountdown;
    void Start(){
        state = State.Scanning;
        lockonCountdown = lockonTime;
        coolDownCountdown = coolDown;
        hitLayerMask = LayerMask.GetMask("Terrain") | targetLayerMask;
    }
    
    void FixedUpdate(){
        switch(state){
            case State.Scanning: {
                Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayerMask);
                RaycastHit hitInfo;
                foreach(Collider collider in colliders){
                    if(Physics.Raycast(transform.position, (collider.gameObject.transform.position - transform.position).normalized, out hitInfo, hitLayerMask) && hitInfo.collider.Equals(collider)){
                        target = collider.gameObject;
                        line.enabled = true;
                        state = State.LockingOn;
                        break;
                    }
                }
                break;
            }
            case State.LockingOn: {
                // Show some locking on effect
                if(target != null){
                    RaycastHit hitInfo;
                    if(Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hitInfo, hitLayerMask) && hitInfo.collider.gameObject.Equals(target)){
                        line.SetPosition(1, target.transform.position);
                        lockonCountdown -= Time.deltaTime;
                        if(lockonCountdown <= 0){
                            state = State.Firing;
                            lockonCountdown = lockonTime;
                        }
                    }else{
                        line.enabled = false;
                        target = null;
                        lockonCountdown = lockonTime;
                        state = State.Scanning;
                    }
                }else{
                    line.enabled = false;
                    lockonCountdown = lockonTime;
                    state = State.Scanning;
                }
                break;
            }
            case State.Firing: {
                // Set up laser line
                line.startWidth = 0.2f;
                line.endWidth = 0.2f;
                target.GetComponent<CharacterHealth>().TakeDamage(damage);
                target.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * pushForce + Vector3.up * pushForce/2, ForceMode.Acceleration);
                state = State.CoolingDown;
                break;
            }
            case State.CoolingDown: {
                coolDownCountdown -= Time.deltaTime;
                if(coolDownCountdown <= 0){
                    coolDownCountdown = coolDown;
                    if(target == null){
                        line.enabled = false;
                        state = State.Scanning;
                    }else {
                        // Revert line back to lock on line
                        line.startWidth = 0.1f;
                        line.endWidth = 0.1f;
                        state = State.LockingOn;
                    }
                }
                break;
            }
        }
    }
}
