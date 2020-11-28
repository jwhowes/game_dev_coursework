using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFloaterController : MonoBehaviour{
    public Rigidbody rb;
    public float speed;
    void OnTriggerStay(Collider other){
        rb.AddForce((transform.position - other.ClosestPoint(transform.position)).normalized * speed, ForceMode.Acceleration);
    }
}
