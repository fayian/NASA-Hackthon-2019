using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private float ConstantForce = 500f;
    public Rigidbody rb;
    

    void FixedUpdate()
    {
        rb.AddForce(0, 0, ConstantForce * Time.deltaTime);



        if(Input.GetMouseButtonDown(0) )
        {

        }
    }
}
