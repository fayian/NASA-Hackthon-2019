﻿using UnityEngine.UI;
using UnityEngine;

public class CoordinateTransformation : MonoBehaviour
{
    public Transform player;
    public Text coordinate;
    private float coordinateRate = 0.01f;
    
    private float longtitude;
    private float latitude;
    private float depth;

    void Update()
    {
        longtitude = player.position.x * coordinateRate+150.0f;
        latitude = player.position.z * coordinateRate+23.5f;
        depth = -player.position.y * Global.METER_PER_UNIT;
        coordinate.text = "Longtitude:" + longtitude.ToString("F2") + "E\r\n" + "Latitude:" + latitude.ToString("F2") + "N\r\n" + "Depth:" + depth.ToString("F2") + "m";
    }
}
