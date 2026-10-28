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
        // Find the score system so spawn difficulty can scale by level.
        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();
    }

    void Update()
    {
        if (scoreCounter == null)
        {
            return;
        }

        // Every 1000 points increases the game level.
        int level =
            (scoreCounter.score / 1000) + 1;

        // Higher levels reduce the delay between asteroid spawns.
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

        // Spawn another object once the delay has passed.
        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroid();

            nextSpawnTime =
                Time.time + spawnDelay;
        }
    }

    void SpawnAsteroid()
    {
        // Choose a random horizontal spawn position.
        float randomX =
            Random.Range(minX, maxX);

        Vector3 spawnPosition =
            new Vector3(
                randomX,
                spawnY,
                0f
            );

        // Decide whether to spawn a normal or health asteroid.
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