using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject asteroidPrefab;
    public GameObject healthAsteroidPrefab;

    [Header("Spawn Area")]
    public float minX = -7f;
    public float maxX = 7f;
    public float spawnY = 5f;

    [Header("Health Asteroid")]
    [Range(0f, 1f)]
    public float healthAsteroidChance = 0.10f;

    private ScoreCounter scoreCounter;
    private float nextSpawnTime = 0f;

    void Start()
    {
        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();
    }

    void Update()
    {
        if (scoreCounter == null)
        {
            return;
        }

        int level =
            (scoreCounter.score / 1000) + 1;

        float spawnDelay = 2f;

        if (level == 2)
        {
            spawnDelay = 1.7f;
        }
        else if (level == 3)
        {
            spawnDelay = 1.4f;
        }
        else if (level >= 4)
        {
            spawnDelay = 1.1f;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroid();

            nextSpawnTime =
                Time.time + spawnDelay;
        }
    }

    void SpawnAsteroid()
    {
        float randomX =
            Random.Range(minX, maxX);

        Vector3 spawnPosition =
            new Vector3(
                randomX,
                spawnY,
                0f
            );

        float randomChance =
            Random.Range(0f, 1f);

        if (randomChance < healthAsteroidChance &&
            healthAsteroidPrefab != null)
        {
            Instantiate(
                healthAsteroidPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
        else
        {
            Instantiate(
                asteroidPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }
}