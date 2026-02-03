using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;

    [Header("Spawn Zones")]
    public SpawnZone[] spawnZones;

    [Header("Wave Settings")]
    public int startingEnemies = 3;
    public int enemiesPerWaveIncrease = 2;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 5f;
    public float healthIncreasePerWave = 2f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!waveInProgress && enemiesAlive == 0)
        {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave()
    {
        waveInProgress = true;
        currentWave++;

        int enemiesThisWave = startingEnemies + enemiesPerWaveIncrease * (currentWave - 1);

        Debug.Log($"Wave {currentWave} - Spawning {enemiesThisWave} enemies");

        for (int i = 0; i < enemiesThisWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        waveInProgress = false;
    }

    void SpawnEnemy()
    {
        SpawnZone zone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 spawnPos = zone.GetRandomPoint();

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemiesAlive++;

        // Scale health
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            float baseHealth = health.maxHealth; // prefab base health
            float scaledHealth = baseHealth + (currentWave - 1) * healthIncreasePerWave;
            health.maxHealth = scaledHealth;
            health.ResetHealth(); // reset current health to new max
        }
    }

    public static void OnEnemyKilled()
    {
        if (instance != null)
        {
            instance.enemiesAlive--;
        }
    }
}
