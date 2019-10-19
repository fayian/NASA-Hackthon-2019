using UnityEngine.UI;
using UnityEngine;

public class CoordinateTransformation : MonoBehaviour
{
    private Text coordText;

    private const float MIN_LATITIUDE = 10; //N
    private const float MAX_LATITIUDE = 40; //N
    private const float MIN_LONGTITUDE = 150; //E
    private const float MAX_LONGTITUDE = 220; /*-140W*/

    private float longtitude;
    private float latitude;
    private float depth;

    private string longtitudeText;
    public void UVMapping(float u, float v) {
        longtitude = (MAX_LONGTITUDE - MIN_LONGTITUDE) * u + MIN_LONGTITUDE;
        latitude = (MAX_LATITIUDE - MIN_LATITIUDE) * v + MIN_LATITIUDE;
    }

    void Awake() {
        Global.player.GetComponent<PlayerMovement>().coords = this;
        coordText = GetComponent<Text>();
    }
    void Update()
    {
        depth = -Global.player.transform.position.y * Global.METER_PER_UNIT;

        if (longtitude < 180)
            longtitudeText = "Longtitude: " + longtitude.ToString("F2") + "°E\r\n";
        else if(longtitude > 180)
            longtitudeText = "Longtitude: " + (360 - longtitude).ToString("F2") + "°W\r\n";
        else
            longtitudeText = "Longtitude: 180.00°\r\n";


        coordText.text = longtitudeText + "Latitude:" + latitude.ToString("F2") + "N\r\n" + "Depth:" + depth.ToString("F2") + "m";
    }
}
