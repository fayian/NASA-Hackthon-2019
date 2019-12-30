using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public GameObject pauseWindow;

    public void Resume() {
        pauseWindow.SetActive(false);
        Global.gameStatus = GameStatus.RUNNING;
    }

    public void Restart() {
        SceneManager.LoadScene("MainGame");
    }
    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame() {
        Application.Quit();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            pauseWindow.SetActive(true);
            Global.gameStatus = GameStatus.PAUSE;
        }
    }
}
