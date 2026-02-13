using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header("Boss Settings")]
    public GameObject Boss;
    public int bossWaveInterval = 10;

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

    [Header("UI References")]
    public BossHealthBar bossHealthUI;
    public TextMeshProUGUI waveText;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Start next wave if no wave is in progress and no enemies are alive
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

        // Spawn normal enemies
        for (int i = 0; i < enemiesThisWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        // Spawn boss if wave is a boss wave
        if (currentWave % bossWaveInterval == 0)
        {
            SpawnBoss();
        }

        if (waveText != null)
        {
            waveText.text = "Wave " + currentWave;
        }

        waveInProgress = false;
    }

    void SpawnEnemy()
    {
        SpawnZone zone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 spawnPos = zone.GetRandomPoint();

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemiesAlive++;

        // Scale health based on wave
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            float baseHealth = health.maxHealth; // prefab base health
            float scaledHealth = baseHealth + (currentWave - 1) * healthIncreasePerWave;
            health.maxHealth = scaledHealth;
            health.ResetHealth(); // set current health to new max
        }
    }

    void SpawnBoss()
    {
        SpawnZone zone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 spawnPos = zone.GetRandomPoint();

        GameObject boss = Instantiate(Boss, spawnPos, Quaternion.identity);
        enemiesAlive++;

        // Scale boss health harder than normal enemies
        EnemyHealth health = boss.GetComponent<EnemyHealth>();
        if (health != null)
        {
            float baseHealth = health.maxHealth;
            health.maxHealth = baseHealth + (currentWave * 10f);
            health.ResetHealth();
        }

        // Show boss health bar
        if (bossHealthUI != null && health != null)
        {
            bossHealthUI.SetBoss(health);
        }
    }

    // Called by enemies when they die
    public static void OnEnemyKilled()
    {
        if (instance != null)
        {
            instance.enemiesAlive--;
        }
    }
}