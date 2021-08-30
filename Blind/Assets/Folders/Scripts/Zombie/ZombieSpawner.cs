using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] GameObject zombiePrefab;

    public List<GameObject> spawnedZombies = new List<GameObject>();

    [Button]
    void SpawnZombie(int i)
    {
        var spawnedZom = Instantiate(zombiePrefab, spawnLocations[i].position, spawnLocations[i].rotation);
        spawnedZombies.Add(spawnedZom);
    }
}
