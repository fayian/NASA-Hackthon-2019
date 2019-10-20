﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {
    public Image gameoverImage;
    public GameObject deathMessage;
    public Text deathMessageText;

    private const float fadeInTime = 2.0f; //(second)
    private DeathReason deathReason;
    private Dictionary<DeathReason, string> deathReasonContent = new Dictionary<DeathReason, string>();

    void Awake() {
        deathReasonContent.Add(DeathReason.PLASTIC,
            "You ate too much plastic bags! Sea turtles have 22% death rate when eating the first plastic piece. The death rate will rise to 50% after eating 14 pieces\n\n" +
            "If you are wondering when did you eat any, so do sea turtles are.  Sea turtles can confuse plastic bags with jellyfish easily.\n\n" +
            "With the passage of time, plastic piles up in the body of sea turtles due to plastic they ate and also microplastics all around the ocean, threatening their lives.");

        deathReasonContent.Add(DeathReason.STARVE,
            "You starved to death. I have nothing to say.");
        deathReasonContent.Add(DeathReason.PRESSURE,
            "Although leatherbacks are great divers, individual have been recorded diving to depths as great as 1,280 m, it doesn't mean that they can dive to the bottom of the ocean.");
    }

    private IEnumerator func() {
        yield return new WaitForSeconds(0.5f);
        while (gameoverImage.color.a < 1.0f) {
            gameoverImage.color += new Color(0.0f, 0.0f, 0.0f, (1.0f / fadeInTime) * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        deathMessage.SetActive(true);

        string tmp;
        deathReasonContent.TryGetValue(deathReason, out tmp);
        deathMessageText.text = tmp;
    }
    public void EndGame(DeathReason deathReason) {
        this.deathReason = deathReason;
         StartCoroutine(func());
    }

    public void Retry() {
        SceneManager.LoadScene("MainGame");
    }
    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void SeeMore() {
        Application.OpenURL("https://htk7fq0q9o.mobirisesite.com/");
    }
}
