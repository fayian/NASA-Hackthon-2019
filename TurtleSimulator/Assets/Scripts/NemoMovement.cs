using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemoMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 rotation;

    private float speed = Global.KmPerHrToUnitPerSec(10.0f);
    private const float existRadius = 50.0f;
    private float switchTime = 5.0f;
    private float curTime = 0.0f;
    private float rotatateSpeedRange = 90.0f;
    void Start()
    {
        SetVelocity();
        SetRotation();
    }

    
    void Update()
    {
        if(Global.gameStatus == GameStatus.RUNNING) {
            if (curTime < switchTime) { curTime += 1 * Time.fixedDeltaTime; } else {
                curTime = 0.0f;
                SetRotation();
            }

            if (curTime < 1) { transform.eulerAngles += rotation * Time.fixedDeltaTime; }

            SetVelocity();
            transform.position += velocity * Time.fixedDeltaTime;

            Vector3 playerPosition = Global.player.transform.position;
            float distance = Vector3.Distance(playerPosition, transform.position);
            if (distance > existRadius)
                Destroy(gameObject);
        }       
    }

    void SetVelocity()
    {
        velocity = transform.forward * speed;
    }

    void SetRotation()
    {
        rotation = new Vector3(0, Random.Range(-rotatateSpeedRange, rotatateSpeedRange), 0);
    }
}
