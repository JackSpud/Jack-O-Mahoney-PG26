using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;

    [Header("Spawn Zones")]
    public SpawnZone[] spawnZones;

    [Header("Wave Settings")]
    public int startingEnemies = 3;
    public int enemiesPerWaveIncrease = 2;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 3f;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaveLoop());
    }

    IEnumerator SpawnWaveLoop()
    {
        while (true)
        {
            currentWave++;
            int enemiesThisWave = startingEnemies + enemiesPerWaveIncrease * (currentWave - 1);

            Debug.Log($"Wave {currentWave} - Spawning {enemiesThisWave} enemies");

            for (int i = 0; i < enemiesThisWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        SpawnZone zone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 spawnPos = zone.GetRandomPoint();

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
