using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner enemySpawner;

    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private float enemy1SpawnChance;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private float enemy2SpawnChance;

    [SerializeField] private float baseSpawnRadius = 1f;
    [SerializeField] private float spawnRadiusMult = 15f;

    [SerializeField] private float timeToSpawnNext = 5f;
    private float lastSpawned;

    private bool gameStarted = false;

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

            float randomEnemy = Random.Range(0f, 1f);
            if (randomEnemy < enemy1SpawnChance)
            {
                Instantiate(enemyPrefab1, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
            }

            lastSpawned = Time.time;
        }
    }
}
