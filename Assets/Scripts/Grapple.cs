using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour{
    public float range = 20f;
    public LineRenderer line;
    public Rigidbody rb;
    public GameObject player;
    public LayerMask layerMask;

    private Transform dynamicTarget;

    private bool hitStatic;
    private bool hitDynamic;
    private SpringJoint joint;

    private bool hasGrapple;
    private PlayerMovement pm;
    void Start(){
        pm = player.GetComponent<PlayerMovement>();
        hitStatic = false;
        hitDynamic = false;
    }
    void Update(){
        hasGrapple = hasGrapple || pm.isGrounded;
        if(Input.GetButtonDown("Fire2") && hasGrapple){
            Shoot();
        }
        if(Input.GetButton("Fire2") && (hitStatic || hitDynamic)){
            line.SetPosition(0, transform.position - new Vector3(0f, 1f, 0f));
            if (hitDynamic){
                line.SetPosition(1, dynamicTarget.position);
            }
        }
        if(Input.GetButtonUp("Fire2")){
            line.enabled = false;
            Destroy(joint);
        }
    }
    void Shoot(){
        RaycastHit hitInfo;  // Stores the result of the Raycast
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)){
            if(hitInfo.collider.tag == "FloatingGrapplePoint"){
                hitDynamic = true;
                hitStatic = false;
                dynamicTarget = hitInfo.collider.transform;
            }else{
                hitStatic = true;
                hitDynamic = false;
                hasGrapple = false;
                line.SetPosition(1, hitInfo.point);
            }
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hitInfo.point;
            joint.maxDistance = Vector3.Distance(player.transform.position, hitInfo.point) * 0.8f;

            joint.spring = 5f;  // One idea for nerfing the hook would be to just make the range really short
            joint.damper = 0f;  // Could also only make a few surfaces grapple-able (by adding a layer mask to the raycast) 
            joint.massScale = 1f;

            line.enabled = true;
        } else{
            hitStatic = false;
            hitDynamic = false;
        }
    }
}
