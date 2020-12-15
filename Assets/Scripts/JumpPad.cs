using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour{
    public float force;
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * force, ForceMode.VelocityChange);
        }
    }
}
