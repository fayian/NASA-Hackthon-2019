using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    //飢餓值
    [SerializeField]
    private float hunger = 100.0f;
    private const float maxHunger = 100.0f;    
    private const float hungryRate = 0.5f;
    //體力值
    [SerializeField]
    private float stamina = 100.0f;
    private const float maxStamina = 100.0f;
    private const float staminaRecoverRate = 8.0f;
    private const float staminaDecreaseRate = 10.0f; //stamina decrease rate when rushing
    //塑料值
    [SerializeField]
    private float plastic = 0.0f;
    private const float maxPlastic = 100.0f;
    private const float passivePlasticIncreaseRate = 0.0f; //隨時間增加的塑料值

    public bool isRushing = false;

    //Getters
    public float Hunger { get => hunger; }
    public float MaxHunger { get => maxHunger;  }
    public float Stamina { get => stamina; }
    public float MaxStamina { get => maxStamina; }
    public float Plastic { get => plastic; }
    public float MaxPlastic { get => maxPlastic; }
    

    //Functions
    public void EatFood(float amount) {
        hunger = Mathf.Min(hunger + amount, maxHunger);
    }
    public void EatPlastic(float amount) {
        plastic += amount;
        if (plastic > maxPlastic) {
            Global.GameOver();
        }
    }
    public void Rush(float deltaTime) {
        
        isRushing = true;
    }

    private void StatusUpdate(float deltaTime) {
        if (Global.gameStatus == GameStatus.RUNNING) {
            hunger -= hungryRate * deltaTime;
            if(hunger <= 0) {
                Global.GameOver();
            }

            EatPlastic(passivePlasticIncreaseRate * deltaTime);

            if(isRushing)
                stamina = Mathf.Max(0, stamina - staminaDecreaseRate * deltaTime);
            else if (stamina != maxStamina)
                stamina = Mathf.Min(stamina + staminaRecoverRate * deltaTime, maxStamina);
        }
    }

    void Update() {
        StatusUpdate(Time.deltaTime);
    }

}
