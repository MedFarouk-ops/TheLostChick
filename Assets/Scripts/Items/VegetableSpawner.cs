using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableSpawner : MonoBehaviour
{
    public GameObject[] vegetablePrefabs; // Array of vegetable prefabs to spawn
    public Transform[] spawnPoints; // Array of initial spawn points

    private void Start()
    {
        SpawnVegetables();
    }

    private void SpawnVegetables()
    {
        // Spawn vegetables at the initial positions
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject randomVegetablePrefab = vegetablePrefabs[Random.Range(0, vegetablePrefabs.Length)];
            Instantiate(randomVegetablePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
