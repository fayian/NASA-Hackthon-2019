using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody rb;
    public PlayerStatus player;

    private float constantSpeed = 10.0f;
    private float rushingSpeed = 20.0f;
    private float speed = 10.0f;

    private float verticalSpeed = 8.0f;
    private float verticalAngle = 0.0f;
    private float maxVerticalAngle = 20.0f;
    private float verticalRotateSpeed = 45.0f;
    private float verticalDecreaseSpeed = 90.0f;
    
    private float staminaDecreaseRate=10.0f;

    private float rotateSpeed=0.0f;
    private float maxRotateSpeed = 90.0f;
    private float minRotateSpeed = -90.0f;
    private float rotateAcceleration = 30.0f;
    private float rotateDecreaseRate = 1.2f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerStatus>();
    }
    void FixedUpdate()
    {
        handleMovement();
        handleRotation();
        handleUpAndDown();
        print(transform.eulerAngles);
    }

    private void handleMovement()
    {
        float Angle=transform.eulerAngles.y*Mathf.Deg2Rad;
        transform.position+=new Vector3(speed* Time.deltaTime * Mathf.Sin(Angle), 0, speed * Time.deltaTime*Mathf.Cos(Angle));

        
        if(Input.GetMouseButton(0)&&player.stamina>=0)
        {
            speed = rushingSpeed;
            player.stamina = Mathf.Max(0, player.stamina - staminaDecreaseRate * Time.deltaTime);
        }
        else
        {
            speed = constantSpeed;
        }
    }

    private void handleRotation()
    {
        if(Input.GetKey("d"))
        {
            rotateSpeed = Mathf.Min(maxRotateSpeed, rotateSpeed + rotateAcceleration * Time.deltaTime);                
        }
         
        if (Input.GetKey("a"))
        {
            rotateSpeed = Mathf.Max(minRotateSpeed, rotateSpeed - rotateAcceleration * Time.deltaTime);
        }

        if(!Input.GetKey("a")&&!Input.GetKey("d"))
        { 
            rotateSpeed = rotateSpeed/rotateDecreaseRate ; 
        }

        transform.Rotate(0,  rotateSpeed*Time.deltaTime,0);

    }
    private void handleUpAndDown()
    {
        if(Input.GetKey("w"))
        {
            transform.position += new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            verticalAngle = Mathf.Max(-maxVerticalAngle, verticalAngle - verticalRotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0, -verticalSpeed * Time.deltaTime, 0);
            verticalAngle = Mathf.Min(maxVerticalAngle, verticalAngle + verticalRotateSpeed * Time.deltaTime);
        }

        if (!Input.GetKey("w") && !Input.GetKey("s"))
        {
            if (verticalAngle >= 0)
                verticalAngle = Mathf.Max(0, verticalAngle - verticalRotateSpeed * Time.deltaTime);
            else
                verticalAngle = Mathf.Min(0, verticalAngle + verticalRotateSpeed * Time.deltaTime);
        }

        if (verticalAngle >= 0)
            transform.eulerAngles = new Vector3(verticalAngle, transform.eulerAngles.y, 0);
        else
            transform.eulerAngles = new Vector3(verticalAngle+360, transform.eulerAngles.y, 0);
    }
}
