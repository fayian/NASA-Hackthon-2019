using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus { RUNNING, PAUSE };
public enum DeathReason { STARVE, PRESSURE, PLASTIC };
public static class Global {
    public static readonly float METER_PER_UNIT = 100.0f;

    public static float KmPerHrToUnitPerSec(float KPH) {
        return KPH * 1000 / METER_PER_UNIT / 60/*1 in-game hour = 60 second(1 sec = 1 in-game min)*/;
    }
    public static float KmToUnit(float km) { return km * 1000 / METER_PER_UNIT;  }
    public static GameStatus gameStatus = GameStatus.PAUSE;
    public static GameObject player;
    public static bool isGameOver = false;

    public static void GameOver(DeathReason deathReason) {
        gameStatus = GameStatus.PAUSE;
        GameObject.Find("Gameover").GetComponent<Gameover>().EndGame(deathReason);
    }
}
