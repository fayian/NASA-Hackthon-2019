using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody rb;
    private float ConstantForce = 200.0f;
    public PlayerStatus player;
    private float staminaDecreaseRate=10.0f;

    private float RotateSpeed=0.0f;
    private float maxRotateSpeed = 90.0f;
    private float minRotateSpeed = -90.0f;
    private float RotateAcceleration = 30.0f;
    private float RotateDecreaseRate = 1.2f;
    private Vector3 Forward;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerStatus>();
    }
    void FixedUpdate()
    {
        handleMovement();
        handleRotation();
    }

    private void handleMovement()
    {
        float Angle=transform.eulerAngles.y*Mathf.Deg2Rad;
        rb.AddForce(ConstantForce * Time.deltaTime * Mathf.Sin(Angle), 0, ConstantForce * Time.deltaTime*Mathf.Cos(Angle));

        
        if(Input.GetMouseButton(0)&&player.stamina>=10)
        {
            rb.AddForce(ConstantForce * Time.deltaTime * Mathf.Sin(Angle), 0, ConstantForce * Time.deltaTime * Mathf.Cos(Angle)); ;//2 time the force
            player.stamina = Mathf.Max(0, player.stamina - staminaDecreaseRate * Time.deltaTime);
        }
    }

    private void handleRotation()
    {
        if(Input.GetKey("d"))
        {
            RotateSpeed = Mathf.Min(maxRotateSpeed, RotateSpeed + RotateAcceleration * Time.deltaTime);                
        }
         
        if (Input.GetKey("a"))
        {
            RotateSpeed = Mathf.Max(minRotateSpeed, RotateSpeed - RotateAcceleration * Time.deltaTime);
        }

        if(!Input.GetKey("a")&&!Input.GetKey("d"))
        { 
            RotateSpeed = RotateSpeed/RotateDecreaseRate ; 
        }

        transform.Rotate(0,  RotateSpeed*Time.deltaTime,0);

    }
}
