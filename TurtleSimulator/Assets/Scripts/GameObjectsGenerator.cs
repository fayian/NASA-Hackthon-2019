using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsGenerator : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Transform player;
    private Vector3 generateArea;
    private float waitTime;
    private float maxWaitTime = 0.5f;
    private float minWaitTime = 1.5f;
    private float startWaitTime = 0.5f;

    private int randGameObject;
    void Start()
    {
        generateArea = new Vector3(15, 15, 15);
        StartCoroutine(GameObjectGenerator());
    }

    
    void Update() {
        if (Global.gameStatus == GameStatus.RUNNING)
            waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    IEnumerator GameObjectGenerator()
    {
        yield return new WaitForSeconds(startWaitTime);

        while(true)
        {
            if(Global.gameStatus == GameStatus.RUNNING)
            {
                randGameObject = Random.Range(0, 5);

                Vector3 generatePosition = new Vector3(Random.Range(-generateArea.x, generateArea.x), Random.Range(-generateArea.y, generateArea.y), Random.Range(-generateArea.z, generateArea.z)) + player.position;

                Instantiate(gameObjects[randGameObject], generatePosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            }

            yield return new WaitForSeconds(waitTime);
        }
             

    }
}
