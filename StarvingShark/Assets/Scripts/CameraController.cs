using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float rotateSpeed = 1.0f;

    private Vector2 mousePos;
    private Vector2 fixedMousePos;

    void Update() {
        mousePos = (Vector2)Input.mousePosition;
        if (mousePos.x < 0) mousePos.x = 0; if (mousePos.x > Screen.width) mousePos.x = Screen.width;
        if (mousePos.y < 0) mousePos.y = 0; if (mousePos.y > Screen.height) mousePos.y = Screen.height;

        fixedMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        print(fixedMousePos);

        transform.eulerAngles = new Vector3(-fixedMousePos.y, fixedMousePos.x, 0.0f) * rotateSpeed;
    }
}
