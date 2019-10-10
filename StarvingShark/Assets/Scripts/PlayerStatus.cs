using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    //飢餓值
    public float maxHunger = 100.0f;
    public float hunger = 100.0f;
    private float hungryRate = 0.1f;
    //體力值
    public float maxStamina = 100.0f;
    public float stamina = 100.0f;
    private float staminaRecover = 2.0f;
    //塑料值
    public float maxPlastic = 100.0f;
    public float plastic = 0.0f;
    private float passivePlasticIncrease = 0.0f; //隨時間增加的塑料值

    //Getters
    public float MaxHunger { get => maxHunger;  }
    public float Hunger { get => hunger; }
    public float MaxStamina { get => maxStamina; }
    public float Stamina { get => stamina; }
    public float MaxPlastic { get => maxPlastic; }
    public float Plastic { get => plastic; }

    //Functions
    public void EatFood(float amount) {
        hunger = Mathf.Max(hunger + amount, maxHunger);
    }
    public void EatPlastic(float amount) {
        plastic += amount;
        if (plastic > maxPlastic) {
            Global.GameOver();
        }
    }

    private void StatusUpdate() {
        if (Global.gameStatus == GameStatus.RUNNING) {
            hunger -= hungryRate;
            EatPlastic(passivePlasticIncrease);
            if (stamina != maxStamina)
                stamina = Mathf.Max(stamina + staminaRecover, maxStamina);
        }
    }

    void Start() {
        InvokeRepeating("StatusUpdate", 0.0f, 0.25f); //每0.25秒一次
    }

}
