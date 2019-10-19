using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public RawImage depthWarning;
    public Texture[] depthWarningTextures = new Texture[4];

    public CoordinateTransformation coords;

    private PlayerStatus player;

    private readonly float constantSpeed = Global.KmPerHrToUnitPerSec(8.0f);  //8(km/h) in-game scale
    private readonly float rushingSpeed = Global.KmPerHrToUnitPerSec(20.0f);  //20(km/h) in-game scale
    private float speed;

    private float verticalAngle = 0.0f;
    private const float maxVerticalAngle = 20.0f;
    private const float verticalRotateSpeed = 45.0f;
    
    private float rotateSpeed = 0.0f;
    private const float maxRotateSpeed = 90.0f;
    private const float minRotateSpeed = -90.0f;
    private const float rotateAcceleration = 30.0f;
    private const float rotateDecreaseRate = 1.2f;
    
    private Vector2 leftBorder;
    private Vector2 rightBorder;
    private float topBorder;
    private float bottomBorder;
    //unit: kilometer
    private readonly Vector2 borderBottomLeft = new Vector2(0.0f, 0.0f);
    private readonly Vector2 borderBottomRight = new Vector2(7636.6f, 0.0f);
    private readonly Vector2 borderTopLeft = new Vector2(833.65f, 3330.0f);
    private readonly Vector2 borderTopRight = new Vector2(6802.95f, 3330.0f);

    private void HandleMovement()
    {
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

        float minX, maxX;
        minX = leftBorder.x * transform.position.z / (borderTopRight.y * 1000 / Global.METER_PER_UNIT);
        maxX = rightBorder.x * transform.position.z / (borderTopRight.y * 1000 / Global.METER_PER_UNIT) + (borderBottomRight.x * 1000 / Global.METER_PER_UNIT);

        coords.UVMapping((transform.position.x - minX) / (maxX - minX),
                                            transform.position.z / (borderTopRight.y * 1000 / Global.METER_PER_UNIT));
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

        if (transform.position.y < -depth) Global.GameOver(DeathReason.PRESSURE);
    }

    private void SetSpawnArea(Vector3 bottomLeft, Vector3 topRight) {
        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);
        float z = Random.Range(bottomLeft.z, topRight.z);
        transform.position = new Vector3(x, y, z);
    }
    private void SetUpBorder() {
        leftBorder = (borderTopLeft - borderBottomLeft) * 1000 / Global.METER_PER_UNIT;
        rightBorder  = (borderTopRight - borderBottomRight) * 1000 / Global.METER_PER_UNIT;
        topBorder = borderTopLeft.y * 1000 / Global.METER_PER_UNIT;
        bottomBorder = borderBottomLeft.y * 1000 / Global.METER_PER_UNIT;
    }
    private void CheckBorder() {
        if (transform.position.z > topBorder) transform.position = new Vector3(transform.position.x, transform.position.y, topBorder);
        else if (transform.position.z < bottomBorder) transform.position = new Vector3(transform.position.x, transform.position.y, bottomBorder );
        float xMin = leftBorder.x * transform.position.z / leftBorder.y;
        float xMax = rightBorder.x * transform.position.z / rightBorder.y + borderBottomRight.x * 1000 / Global.METER_PER_UNIT;
        if (transform.position.x > xMax) transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        else if (transform.position.x < xMin) transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
    }

    void Awake() {
        Random.InitState(Guid.NewGuid().GetHashCode());
        speed = constantSpeed;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerStatus>();
        SetUpBorder();
        SetSpawnArea(new Vector3(30000.0f, 3.0f, 10000.0f), new Vector3(50000.0f, 8.0f, 20000.0f));
    }
    void FixedUpdate() {
        HandleUpAndDown();
        HandleRotation();
        HandleMovement();
        HandleMaxDepth(15.0f);
        CheckBorder();
        Debug.DrawLine(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(8336.5f, 0.0f, 33300f), new Color(255, 0, 0));
        Debug.DrawLine(new Vector3(76366f, 0.0f, 0.0f), new Vector3(68029.5f, 0.0f, 33300f), new Color(255, 0, 0));
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Plastic") {
            player.EatPlastic(5.0f);
        }
        if(other.gameObject.tag == "Food") {
            player.EatFood(20.0f);
        }
        Destroy(other.gameObject);
    }
}
