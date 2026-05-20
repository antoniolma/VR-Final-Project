using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner enemySpawner;

    [SerializeField] private float weakEnemyChance = 1.01f;
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private float enemy1SpawnChance = 0.5f;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private float enemy2SpawnChance;

    [SerializeField] private GameObject strongEnemyPrefab;

    [SerializeField] private float baseSpawnRadius = 1f;
    [SerializeField] private float spawnRadiusMult = 15f;

    [SerializeField] private float timeToSpawnNext = 5f;
    private float lastSpawned;
    public List<GameObject> enemiesSpawned = new List<GameObject>();

    public bool gameStarted = false;

    private void Awake()
    {
        enemySpawner = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawned = Time.time;
    }

    public void StartGame()
    {
        lastSpawned = Time.time;
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawned > timeToSpawnNext && gameStarted)
        {
            Vector3 spawnPosition = PlayerInstance.playerInstance.transform.position;
            float randomX = Random.Range(-1f, 1f);
            float randomPosX = Random.Range(baseSpawnRadius, baseSpawnRadius + 1f);
            if (randomX < 0f)
            {
                randomPosX *= -1;
            }
            float randomZ = Random.Range(-1f, 1f);
            float randomPosZ = Random.Range(baseSpawnRadius, baseSpawnRadius + 1f);
            if (randomZ < 0f)
            {
                randomPosZ *= -1;
            }
            Vector3 spawnModifier = spawnRadiusMult * new Vector3(randomX, 0f, randomZ).normalized + new Vector3(randomPosX, 0f, randomPosZ);
            spawnPosition += spawnModifier;

            GameObject enemy;
            float randomEnemyStrength = Random.Range(0f, 1f);
            if (randomEnemyStrength < weakEnemyChance)
            {
                float randomEnemy = Random.Range(0f, 1f);
                if (randomEnemy < enemy1SpawnChance)
                {
                    enemy = Instantiate(enemyPrefab1, spawnPosition, Quaternion.identity);
                }
                else
                {
                    enemy = Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                enemy = Instantiate(strongEnemyPrefab, spawnPosition, Quaternion.identity);
            }
            enemiesSpawned.Add(enemy);

            lastSpawned = Time.time;
        }
    }

    public void NextHorda()
    {
        weakEnemyChance = Mathf.Max(0.1f, weakEnemyChance - 0.2f);
        timeToSpawnNext = Mathf.Max(0.1f, timeToSpawnNext - 1f);
    }
}
