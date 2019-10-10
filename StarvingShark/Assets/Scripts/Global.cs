using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus { RUNNING, PAUSE };
public static class Global {
    public static GameStatus gameStatus = GameStatus.RUNNING;
    public static void GameOver() {
        gameStatus = GameStatus.RUNNING;
        //TODO
    }
}
