using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public RawImage depthWarning;
    public Texture[] depthWarningTextures = new Texture[4];

    private PlayerStatus player;

    private const float constantSpeed = 4.0f / 60 * 1000 / Global.METER_PER_UNIT;  //4(km/h) in-game scale
    private const float rushingSpeed = 10.0f / 60 * 1000 / Global.METER_PER_UNIT;  //10(km/h) in-game scale
    private float speed = constantSpeed;

    private float verticalAngle = 0.0f;
    private const float maxVerticalAngle = 20.0f;
    private const float verticalRotateSpeed = 45.0f;
    
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
        HandleUpAndDown();
        HandleRotation();
        HandleMovement();
        HandleMaxDepth(15.0f);       
    }

    private void HandleMovement()
    {
        float Angle=transform.eulerAngles.y*Mathf.Deg2Rad;
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

        if (transform.position.y > 0)
            transform.position -= new Vector3(0.0f, transform.position.y, 0.0f);

        if (Input.GetMouseButton(0) && player.Stamina > 0)
        {
            speed = rushingSpeed;
            player.isRushing = true;
        }
        else { 
            speed = constantSpeed;
            player.isRushing = false;
        }
    }

    private void HandleRotation()
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
    private void HandleUpAndDown()
    {
        if(Input.GetKey(KeyCode.W))
            verticalAngle = Mathf.Max(-maxVerticalAngle, verticalAngle - verticalRotateSpeed * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.S))
            verticalAngle = Mathf.Min(maxVerticalAngle, verticalAngle + verticalRotateSpeed * Time.fixedDeltaTime);
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
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

    private void HandleMaxDepth(float depth/*in unity unit*/) {

        if (transform.position.y < -depth * 14 / 15) 
            depthWarning.texture = depthWarningTextures[3];
        else if (transform.position.y < -depth * 5 / 6) 
            depthWarning.texture = depthWarningTextures[2];
        else if (transform.position.y < -depth * 2 / 3) 
            depthWarning.texture = depthWarningTextures[1];
        else 
            depthWarning.texture = depthWarningTextures[0];

        if (transform.position.y < -depth) Global.GameOver();
    }
}
