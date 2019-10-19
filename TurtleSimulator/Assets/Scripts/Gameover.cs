using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour {
    public Image gameoverImage;
    public const float fadeInTime = 2.0f; //(second)
    private bool activated = false;
    private IEnumerator EndGame() {
        yield return new WaitForSeconds(0.5f);
        while (gameoverImage.color.a < 1.0f) {
            gameoverImage.color += new Color(0.0f, 0.0f, 0.0f, (1.0f / fadeInTime) * Time.deltaTime);
            yield return null;
        }

    }
    void Update() {
        if(Global.isGameOver && !activated) {
            StartCoroutine(EndGame());
            activated = true;
        }       
    }
}
