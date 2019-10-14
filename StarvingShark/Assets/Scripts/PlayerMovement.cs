using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody rb;
    public PlayerStatus player;

    private const float constantSpeed = 10.0f;
    private const float rushingSpeed = 20.0f;
    private float speed = constantSpeed;

    private float verticalAngle = 0.0f;
    private const float verticalSpeed = 8.0f;
    private const float maxVerticalAngle = 20.0f;
    private const float verticalRotateSpeed = 45.0f;
    
    private const float minimumRushStamina = 5.0f;

    private float rotateSpeed = 0.0f;
    private const float maxRotateSpeed = 90.0f;
    private const float minRotateSpeed = -90.0f;
    private const float rotateAcceleration = 30.0f;
    private const float rotateDecreaseRate = 1.2f;
    
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
    }

    private void handleMovement()
    {
        float Angle=transform.eulerAngles.y*Mathf.Deg2Rad;
        transform.position+=new Vector3(speed* Time.deltaTime * Mathf.Sin(Angle), 0, speed * Time.deltaTime*Mathf.Cos(Angle));
         
        
        if(Input.GetMouseButton(0) && player.Stamina > 0)
        {
            speed = rushingSpeed;
            player.isRushing = true;
        }
        else { 
            speed = constantSpeed;
            player.isRushing = false;
        }
    }

    private void handleRotation()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rotateSpeed = Mathf.Min(maxRotateSpeed, rotateSpeed + rotateAcceleration * Time.fixedDeltaTime);                
        }
         
        if (Input.GetKey(KeyCode.A))
        {
            rotateSpeed = Mathf.Max(minRotateSpeed, rotateSpeed - rotateAcceleration * Time.fixedDeltaTime);
        }

        if(!Input.GetKey(KeyCode.A)&&!Input.GetKey(KeyCode.D))
        { 
            rotateSpeed = rotateSpeed/rotateDecreaseRate ; 
        }

        transform.Rotate(0,  rotateSpeed*Time.fixedDeltaTime, 0);

    }
    private void handleUpAndDown()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, verticalSpeed * Time.fixedDeltaTime, 0);
            verticalAngle = Mathf.Max(-maxVerticalAngle, verticalAngle - verticalRotateSpeed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -verticalSpeed * Time.fixedDeltaTime, 0);
            verticalAngle = Mathf.Min(maxVerticalAngle, verticalAngle + verticalRotateSpeed * Time.fixedDeltaTime);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (verticalAngle >= 0)
                verticalAngle = Mathf.Max(0, verticalAngle - verticalRotateSpeed * Time.fixedDeltaTime);
            else
                verticalAngle = Mathf.Min(0, verticalAngle + verticalRotateSpeed * Time.fixedDeltaTime);
        }

        if (verticalAngle >= 0)
            transform.eulerAngles = new Vector3(verticalAngle, transform.eulerAngles.y, 0);
        else
            transform.eulerAngles = new Vector3(verticalAngle+360, transform.eulerAngles.y, 0);
    }
}
