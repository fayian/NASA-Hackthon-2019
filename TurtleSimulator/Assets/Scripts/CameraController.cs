using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraController : MonoBehaviour {
    [Tooltip("The total angle the camera can rotate horizontally in degrees")]
    public float sightWidth = 60.0f;
    [Tooltip("The total angle the camera can rotate vertically in degrees")]
    public float sightHeight = 30.0f;

    private Vector3 rotationOffset;
    private Vector2 mousePos;
    private Vector3 rotation;

    void Awake() {
        rotationOffset = transform.localRotation.eulerAngles;
        if (transform.parent == null) Debug.LogError("The Camera is not bound to a GameObject.");
    }

    void Update() {
        mousePos = Input.mousePosition;
        if (mousePos.x < 0) mousePos.x = 0; if (mousePos.x > Screen.width) mousePos.x = Screen.width;
        if (mousePos.y < 0) mousePos.y = 0; if (mousePos.y > Screen.height) mousePos.y = Screen.height;

        rotation.x = -(mousePos.y - Screen.height / 2) * (sightHeight / Screen.height);
        rotation.y = (mousePos.x - Screen.width / 2) * (sightWidth / Screen.width);
        transform.eulerAngles = Quaternion.LookRotation(transform.position - transform.parent.position).eulerAngles + rotation;
    }
}
    