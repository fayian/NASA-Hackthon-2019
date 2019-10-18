
using UnityEngine;

public class PlasticBagsMovement : MonoBehaviour
{

    private Vector3 velocity;
    private Vector3 rotation;
    private float switchTime = 2.5f;
    private float curTime = 0.0f;
    private float speedRange=1.0f;
    private float rotatateSpeedRange = 15.0f;
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
    }
    
}
