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
    public float offsetX = 7.0f;
    public float offsetY = 10.0f;
    public float offsetZ = -2.0f;


    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && instance < instanceLimit) 
        { 
            InvokeRepeating("SpawnObjects", 0.5f, 1.0f);
            instance = instance + 1;
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
            Vector3 randPos = new Vector3(transform.position.x + offsetX + randXPos, transform.position.y + offsetY,
                transform.position.z + offsetZ+ randZPos);
            Instantiate(objectPrefabs[objectPrefabsIndex], randPos, transform.rotation);
            spawnCount = spawnCount + 1;
        }
        

    }
}
