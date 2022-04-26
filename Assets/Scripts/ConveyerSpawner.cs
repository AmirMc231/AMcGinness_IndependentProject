using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerSpawner : MonoBehaviour
{
    public GameObject obsPrefabs;
    private Vector3 spawnPos = new Vector3(-13, 3.5f, -4.0f);
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObs", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObs()
    {
        Instantiate(obsPrefabs, transform.position, transform.rotation);
    }
}
