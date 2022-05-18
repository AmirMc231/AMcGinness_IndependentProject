using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    private float spawnRange = 20.0f;
    private int enemyCount;
    public int waveLimit = 3;
    private int waveCount = 0;

    private int spawnNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 spawnpos2 = GenerateSpawnPosition();

        SpawnWave(1);

    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<TurretBehavior>().Length;
        if (enemyCount == 0 && waveCount <= waveLimit)
        {
            SpawnWave(spawnNumber);
            //++spawnNumber;
            spawnNumber = spawnNumber + 2;
        }
    }

    void SpawnWave(int enemyNum)
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        for (int i = 0; i < enemyNum; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            waveCount = waveCount + 1;
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnpos = new Vector3(xPos, enemyPrefab.transform.position.y, zPos);
        
        return spawnpos;
    }
}
