using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBagsGenerator : MonoBehaviour
{
    public GameObject[] plasticBags;
    private Vector3 generateArea;
    private float waitTime;
    private float maxWaitTime = 0.5f;
    private float minWaitTime = 1.5f;
    private float startWaitTime = 0.5f;
    private bool stop=false;

    private int randPlasticBags;
    void Start()
    {
        generateArea = new Vector3(10, 10, 10);
        StartCoroutine(Generator());
    }

    
    void Update()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    IEnumerator Generator()
    {
        yield return new WaitForSeconds(startWaitTime);

        while(!stop)
        {
            randPlasticBags = Random.Range(0, 2);

            Vector3 generatePosition = new Vector3(Random.Range(-generateArea.x, generateArea.x), Random.Range(-generateArea.y, generateArea.y), Random.Range(-generateArea.z, generateArea.z));

            Instantiate(plasticBags[randPlasticBags], generatePosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            yield return new WaitForSeconds(waitTime);
        }
             

    }
}
