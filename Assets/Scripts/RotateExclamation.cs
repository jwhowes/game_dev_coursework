using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateExclamation : MonoBehaviour{
    public float rotateSpeed;
    public float verticalDistance;
    private Vector3 initPos;
    void Start(){
        initPos = transform.position;
    }
    void Update(){
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        transform.position = initPos + (Vector3.up * verticalDistance * Mathf.Sin(Time.time)); //new Vector3(0, verticalDistance * Mathf.Sin(Time.time), 0);
    }
}
