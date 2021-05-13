using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject player;

    private int enemyCount;
    private bool playerExist;
    private int waveNumber = 1;
    private float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        playerExist = (player.transform.position.y >= 0);
        if (enemyCount == 0 && playerExist)
        {
            SpawnEnemyWave(waveNumber);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        waveNumber++;
    }

    // GenerateSpawnPosition generate position to instantiate enemy
    private Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(xPos, 0, zPos);

        return randomPos;
    }
}
