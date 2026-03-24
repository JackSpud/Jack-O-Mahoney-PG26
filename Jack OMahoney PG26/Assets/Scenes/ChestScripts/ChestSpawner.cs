using System.Collections;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;
    public Transform[] spawnPoints; // Assign empty GameObjects around the map as potential spawn locations
    public float minSpawnInterval = 30f; // minimum time between chest spawns
    public float maxSpawnInterval = 90f; // maximum time between chest spawns

    private void Start()
    {
        StartCoroutine(SpawnChestRoutine());
    }

    IEnumerator SpawnChestRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnChest();
        }
    }

    void SpawnChest()
    {
        if (spawnPoints.Length == 0 || chestPrefab == null) return;

        // Choose a random spawn point
        Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate chest
        Instantiate(chestPrefab, chosenPoint.position, chosenPoint.rotation);
    }
}