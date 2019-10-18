using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 rotation;

    private float switchTime = 5.0f;
    private float curTime = 0.0f;

    private float speed = 1.0f;
    private float constantSpeed = Global.KmPerHrToUnitPerSec(1.5f);
    private float rushingSpeed = Global.KmPerHrToUnitPerSec(3.0f);

    private float rotatateSpeedRange = 45.0f;

    private const float triggerRadius = 6.0f;
    private const float existRadius = 50.0f;
    private const float eatRadius = 2.0f;
    private const float eatAngle = 30.0f;

    private const float hungerRecover = 10.0f;

    void Start()
    {
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

        if (curTime < 1)
        { transform.eulerAngles += rotation * Time.fixedDeltaTime; }

        SetVelocity();
        transform.position += velocity * Time.fixedDeltaTime;

        Vector3 playerPosition = Global.player.transform.position;
        Vector3 direction = transform.position - playerPosition;
        float distance = Vector3.Distance(playerPosition, transform.position);

        PlayerApproach(distance, playerPosition);

        Disappear(distance, direction);
    }
    void PlayerApproach(float distance, Vector3 direction)
    {

        if (distance < triggerRadius)
        {
            speed = rushingSpeed;
            Vector3 rotateDirection = Quaternion.FromToRotation(Vector3.forward, direction).eulerAngles - transform.eulerAngles;
            transform.eulerAngles += rotateDirection * Time.fixedDeltaTime;
        }
        else { speed = constantSpeed; }

    }

    void Disappear(float distance,Vector3 direction)
    {
        if (distance > existRadius)
        {
            Destroy(gameObject);
        }
    }
}

