using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public ZombiePool zombiePool;
    public Transform playerTransform;

    [Header("Spawn Settings")]
    public float spawnRadius = 15f;
    public float startSpawnInterval = 3f;
    public float minSpawnInterval = 0.5f;
    public float spawnAcceleration = 0.95f;
    public LayerMask obstacleLayer;

    [Header("Difficulty Settings")]
    public float difficultyIncreaseInterval = 30f;
    private float nextDifficultyTime;
    private int difficultyLevel = 1;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + startSpawnInterval;
        nextDifficultyTime = Time.time + difficultyIncreaseInterval;
    }

    private void Update()
    {
        if (PlayerHeath.IsGameOver) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnZombieAroundPlayer();

            startSpawnInterval *= spawnAcceleration;
            startSpawnInterval = Mathf.Max(startSpawnInterval, minSpawnInterval);

            nextSpawnTime = Time.time + startSpawnInterval;
        }

        if (Time.time >= nextDifficultyTime)
        {
            IncreaseDifficulty();
            nextDifficultyTime = Time.time + difficultyIncreaseInterval;
        }
    }

    void SpawnZombieAroundPlayer()
    {
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();

        if (spawnPosition == Vector3.zero) return;

        var zombie = zombiePool.Pool.Get();
        zombie.transform.position = spawnPosition;
        zombie.SetUp(difficultyLevel);
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        int maxAttempts = 10;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            attempts++;

            float angle = Random.Range(0f, 360f);
            float radian = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * spawnRadius;
            Vector3 spawnPos = playerTransform.position + offset;
            spawnPos.y = playerTransform.position.y;

            float minDistanceFromPlayer = 5f;
            if (Vector3.Distance(spawnPos, playerTransform.position) < minDistanceFromPlayer)
                continue;

            Collider[] hits = Physics.OverlapSphere(spawnPos, 0.5f, obstacleLayer);
            if (hits.Length == 0)
                return spawnPos;
        }
        return Vector3.zero;
    }



    void IncreaseDifficulty()
    {
        difficultyLevel++;

        spawnAcceleration -= 0.02f;
        spawnAcceleration = Mathf.Max(spawnAcceleration, 0.8f);

        Debug.Log($"Difficulty increased! Level: {difficultyLevel}");
    }
}
