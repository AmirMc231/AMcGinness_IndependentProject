using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public float xPosition = 3.0f;
    public float zPosition = 3.0f;
    private int spawnlimit = 0;

    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        spawnlimit = spawnlimit + 1;
        if (spawnlimit < 20)
        {
            InvokeRepeating("SpawnObjects", 0.5f, 0.0f);
        }
        else
        {

        }
        

    }

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects()
    {
        float randXPos = Random.Range(-xPosition, xPosition);
        float randZPos = Random.Range(-zPosition, zPosition);
        int objectPrefabsIndex = Random.Range(0, objectPrefabs.Length);
        Vector3 randPos = new Vector3(7 + randXPos, 10 , -2 + randZPos);
        Instantiate(objectPrefabs[objectPrefabsIndex], randPos, transform.rotation);
    }
}
