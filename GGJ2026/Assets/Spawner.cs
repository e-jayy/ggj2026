using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabForSpawnPointA;
    public GameObject prefabForSpawnPointB;

    [Header("Spawn Points")]
    public Transform spawnPointA;
    public Transform spawnPointB;

    [Header("Spawn Timing")]
    public float baseSpawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float spawnRateIncreasePerScore = 0.01f;

    [Header("References")]
    public ScoreManager scoreManager;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        float currentInterval = Mathf.Max(
            minSpawnInterval,
            baseSpawnInterval - (scoreManager.score * spawnRateIncreasePerScore)
        );

        if (timer >= currentInterval)
        {
            Spawn();
            timer = 0f;
        }

        //Debug.Log("Current Spawn Interval: " + currentInterval);
    }

    void Spawn()
    {
        bool useSpawnPointA = Random.value < 0.5f;

        if (useSpawnPointA)
        {
            Instantiate(
                prefabForSpawnPointA,
                spawnPointA.position,
                Quaternion.identity
            );
        }
        else
        {
            Instantiate(
                prefabForSpawnPointB,
                spawnPointB.position,
                Quaternion.identity
            );
        }
    }
}
