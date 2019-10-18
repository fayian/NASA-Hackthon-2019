
using UnityEngine;

public class PlasticBagsMovement : MonoBehaviour
{

    private Vector3 velocity;
    private Vector3 rotation;
    private float switchTime = 2.5f;
    private float curTime = 0.0f;
    private float speedRange = Global.KmPerHrToUnitPerSec(1.0f);
    private float rotatateSpeedRange = 15.0f;
    private const float existRadius = 50.0f;
    private const float eatRadius = 2.0f;
    private const float eatAngle = 30.0f;
    private const float plasticIncrease = 1.0f;
    void Start()
    {
        SetVelocity();
        SetRotation();
    }

    void SetVelocity()
    {
        velocity = new Vector3(Random.Range(-speedRange, speedRange), Random.Range(-speedRange, speedRange), Random.Range(-speedRange, speedRange));
    }

    void SetRotation()
    {
        rotation= new Vector3(Random.Range(-rotatateSpeedRange, rotatateSpeedRange), Random.Range(-rotatateSpeedRange, rotatateSpeedRange), Random.Range(-rotatateSpeedRange, rotatateSpeedRange));
    }
    void FixedUpdate()
    {
        if(curTime<switchTime)
        {curTime += 1 * Time.fixedDeltaTime; }
        else
        {
            curTime = 0.0f;
            SetVelocity();
            SetRotation();
        }
        transform.position += velocity * Time.fixedDeltaTime;
        transform.eulerAngles += rotation * Time.fixedDeltaTime;

        Vector3 playerPosition = Global.player.transform.position;
        Vector3 direction = transform.position - playerPosition;
        float distance = Vector3.Distance(playerPosition, transform.position);
        Disappear(distance, direction);
    }
    void Disappear(float distance, Vector3 direction)
    {
        if (distance > existRadius)
        {
            Destroy(gameObject);
        }

        if (distance < eatRadius && Vector3.Angle(direction, Global.player.transform.forward) < eatAngle)
        {
            Destroy(gameObject);
            Global.player.GetComponent<PlayerStatus>().EatPlastic(plasticIncrease);
        }
    }
}
