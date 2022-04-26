using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public float xPosition = 3.0f;
    public float zPosition = 3.0f;
    private int spawnCount = 0;
    public int spawnLimit = 20;
    private int instance = 0;
    private int instanceLimit = 1;
    public float x = 0;
    public float y = 0;
    public float z = 0;


    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        
        if (instance < instanceLimit)
        {
            InvokeRepeating("SpawnObjects", 0.5f, 1.0f);
            instance = instance + 1;
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
        if (spawnCount < spawnLimit)
        {
            float randXPos = Random.Range(-xPosition, xPosition);
            float randZPos = Random.Range(-zPosition, zPosition);
            int objectPrefabsIndex = Random.Range(0, objectPrefabs.Length);
            Vector3 randPos = new Vector3(7 + randXPos, 10, -2 + randZPos);
            Instantiate(objectPrefabs[objectPrefabsIndex], randPos, transform.rotation);
            spawnCount = spawnCount + 1;
        }
        

    }
}
