using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour{
    public Rigidbody rb;
    public float speed = 200f;
    public float jumpForce = 1000f;  // Make jump higher (it's difficult to rocket jump side-to-side)
    public float dashSpeed = 10000f;

    public int numDashes = 2;
    public float dashRecharge;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    [System.NonSerialized] public bool isGrounded;

    public Slider dashCharge;  // Currently doesn't work for more than one dash
    public TMPro.TextMeshProUGUI dashText;

    private int dashes;
    private float dashCountdown;
    public void Launch() {
        if(!isGrounded){
            rb.AddForce(transform.up * 200f);
        }
    }

    void Start(){
        dashes = numDashes;
        dashCountdown = dashRecharge;
        dashCharge.maxValue = dashRecharge;
        dashCharge.value = dashRecharge;
        rb = GetComponent<Rigidbody>();
        PlayerManager.instance.player = gameObject;
    }

    void Update(){
        dashText.text = dashes.ToString();
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
            rb.AddForce((transform.right * x + transform.forward * z) * speed * Time.deltaTime, ForceMode.Impulse);
        }else{
            rb.AddForce((transform.right * x + transform.forward * z) * speed * 7 * Time.deltaTime);
        }
        rb.angularVelocity = Vector3.zero;
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(0f, jumpForce, 0f);
        }
        if(dashCountdown <= 0 && dashes < numDashes){
            dashCharge.value = dashRecharge;
            dashes++;
            dashCountdown = dashRecharge;
        }
        if(dashes < numDashes){
            dashCharge.value = dashRecharge - dashCountdown;
            dashCountdown -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashes > 0){
            dashes--;
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            rb.AddForce((transform.right * x + transform.forward * z) * (isGrounded ? dashSpeed : dashSpeed / 3f), ForceMode.VelocityChange);  // Time.deltaTime not need as this happens once
        }
    }
}
