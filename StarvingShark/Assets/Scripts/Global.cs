using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus { RUNNING, PAUSE };
public static class Global {
    public const float METER_PER_UNIT = 100.0f;

    public static GameStatus gameStatus = GameStatus.RUNNING;
    public static void GameOver() {
        gameStatus = GameStatus.PAUSE;
        //TODO
    }
}
