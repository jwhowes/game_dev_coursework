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
    public float warmUpTime;
    public LayerMask targetLayerMask;
    public float range;
    public float damage;
    public LineRenderer line;
    public float pushForce;
    
    private State state;
    private GameObject target = null;

    private Transform head;
    private Transform lineBasePos;

    private LayerMask hitLayerMask;
    private float lockonCountdown;
    private float fireDelayCountdown;
    private float coolDownCountdown;
    private float warmUpCountDown;
    void Start(){
        head = transform.GetChild(0).GetChild(0);
        lineBasePos = head.GetChild(0);
        state = State.Scanning;
        lockonCountdown = lockonTime;
        coolDownCountdown = coolDown;
        warmUpCountDown = warmUpTime;
        hitLayerMask = LayerMask.GetMask("Terrain") | targetLayerMask;
        line.SetPosition(0, transform.position);
    }
    
    void FixedUpdate(){
        line.SetPosition(0, lineBasePos.position);
        RaycastHit hitInfo = new RaycastHit();
        switch (state){
            case State.Scanning: {
                Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayerMask);
                foreach(Collider collider in colliders){
                    if(Physics.Raycast(new Ray(transform.position, (collider.gameObject.transform.position - transform.position).normalized), out hitInfo, 300f, hitLayerMask) && hitInfo.collider.Equals(collider)){
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
                    if(Physics.Raycast(new Ray(transform.position, (target.transform.position - transform.position).normalized), out hitInfo, 300f, hitLayerMask) && hitInfo.collider.gameObject.Equals(target)){
                        head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation((target.transform.position - transform.position).normalized), Time.deltaTime * 5f);
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
                head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation((line.GetPosition(1) - transform.position).normalized), Time.deltaTime * 5f);
                if (warmUpCountDown <= 0){
                    line.startWidth = 0.2f;
                    line.endWidth = 0.2f;
                    warmUpCountDown = warmUpTime;
                    if(Physics.Raycast(transform.position, (line.GetPosition(1) - transform.position).normalized, out hitInfo) && hitInfo.collider.gameObject.Equals(target)){
                        target.GetComponent<CharacterHealth>().TakeDamage(damage);
                        target.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * pushForce + Vector3.up * pushForce / 2, ForceMode.Acceleration);
                    }
                    state = State.CoolingDown;
                }else{
                    warmUpCountDown -= Time.deltaTime;
                }
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
