using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private float minSpawnTime;

    [SerializeField]
    private float maxSpawnTime;

    [SerializeField]
    private float bossInterval;

    public GameOverManager gameOverManager;

    private float spawnTime;
    private float diffTime;

    private float bossSpawnTime;
    private bool bossSpawn;

    void Awake()
    {
        SetSpawnTime();
        if (bossInterval == 0)
            bossSpawn = false;
        else
            bossSpawn = true;

        bossSpawnTime = bossInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        diffTime += Time.deltaTime;
        bossSpawnTime -= Time.deltaTime;
        
        if (diffTime > 10f)
        {
            IncreaseDifficulty();
            diffTime = 0;
        }

        if (spawnTime <= 0 && !gameOverManager.gameOver)
        {
            Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
            SetSpawnTime();
        }

        if (bossSpawnTime <= 0 && bossSpawn)
        {
            Instantiate(bossPrefab, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
            bossSpawnTime = bossInterval * 0.9f;
            bossInterval *= 0.9f;
        }
    }

    private void SetSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void IncreaseDifficulty()
    {
        minSpawnTime *= 0.9f;
        maxSpawnTime *= 0.9f;
    }
}
