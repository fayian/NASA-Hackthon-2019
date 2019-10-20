using UnityEngine.UI;
using UnityEngine;

public class CoordinateTransformation : MonoBehaviour
{
    public GameObject directionIndicator;
    private readonly Vector3 northPole = new Vector3(Global.KmToUnit(3816.3f), 0.0f, Global.KmToUnit(8880.0f));

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

    private void IndicatorRotation() {
        float deltaCita = Global.player.transform.eulerAngles.y - Quaternion.LookRotation(northPole - Global.player.transform.position).eulerAngles.y;
        directionIndicator.transform.eulerAngles = new Vector3(0.0f, 0.0f, deltaCita);
    }

    void Awake() {
        Global.player.GetComponent<PlayerMovement>().coords = this;
        coordText = GetComponent<Text>();
    }
    void Update()
    {
        IndicatorRotation();
        Debug.DrawLine(Global.player.transform.position, northPole, new Color(255, 0, 0));
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
