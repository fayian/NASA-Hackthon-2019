using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 rotation;

    private float switchTime = 5.0f;
    private float curTime = 0.0f;

    private readonly float constantSpeed = Global.KmPerHrToUnitPerSec(1.0f);
    private readonly float rushingSpeed = Global.KmPerHrToUnitPerSec(3.0f);
    private float speed;

    private float rotatateSpeedRange = 45.0f;

    private float triggerRadius = 5.0f;
    private float existRadius = 20.0f;

    void Start()
    {
        speed = constantSpeed;
        SetVelocity();
        SetRotation();
        transform.eulerAngles += new Vector3(-90, 0, 0);
    }

    void SetVelocity()
    {
        velocity = transform.forward * speed;
    }

    void SetRotation()
    {
        rotation = new Vector3(Random.Range(-rotatateSpeedRange, rotatateSpeedRange), Random.Range(-rotatateSpeedRange, rotatateSpeedRange), Random.Range(-rotatateSpeedRange, rotatateSpeedRange));
    }
    void FixedUpdate()
    {
        if (curTime < switchTime)
        { curTime += 1 * Time.fixedDeltaTime; }
        else
        {
            curTime = 0.0f;
            SetRotation();
        }
        
        if(curTime<1)
        { transform.eulerAngles += rotation * Time.fixedDeltaTime; }

        SetVelocity();
        transform.position += velocity * Time.fixedDeltaTime;

        Vector3 playerPosition = Global.player.transform.position;
        float distance = Mathf.Sqrt(playerPosition.x * playerPosition.x + playerPosition.y * playerPosition.y + playerPosition.z * playerPosition.z);
        if (distance < triggerRadius) 
        {
            speed = rushingSpeed;
            Vector3 direction = transform.position - playerPosition;
            transform.eulerAngles = Quaternion.FromToRotation(Vector3.forward, direction).eulerAngles;
        }
        else { speed = constantSpeed; }

        
    }
}
