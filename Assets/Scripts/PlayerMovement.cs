using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public Rigidbody rb;
    public float speed = 200f;
    public float jumpForce = 1000f;  // Make jump higher (it's difficult to rocket jump side-to-side)

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    private bool isGrounded;

    public void Launch() {
        if(!isGrounded){
            rb.AddForce(transform.up * 200f);
        }
    }

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded){
            // As I'm using physics to control the character, I want a lot of drag when the character is touching the ground
            rb.drag = 7f;
        }else{
            // However, when the character's in the air I want to reduce drag so they float smoothly
            rb.drag = 0f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(x != 0){
            x = x / Mathf.Abs(x);
        }
        if(z != 0){
            z = z / Mathf.Abs(z);
        }
        if(isGrounded){
            rb.AddForce((transform.right * x + transform.forward * z) * speed * Time.deltaTime, ForceMode.VelocityChange);
        }else{
            rb.AddForce((transform.right * x + transform.forward * z) * speed * 7 * Time.deltaTime);
        }
        rb.angularVelocity = Vector3.zero;
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(0f, jumpForce, 0f);
        }
    }
}
