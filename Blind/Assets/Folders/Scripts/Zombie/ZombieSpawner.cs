using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EasyButtons;
using DG.Tweening;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] GameObject zombiePrefab;
    public List<GameObject> spawnedZombies = new List<GameObject>();

    public UnityEvent<GameObject> OnZombieSpawned;
    public UnityEvent<GameObject> OnZombieRemoved;

    [Button]
    public void SpawnZombie(int i = -999)
    {
        if (i == -999)  // do random value if I is null
            i = Random.Range(0, spawnLocations.Count);

        var spawnedZom = Instantiate(zombiePrefab, spawnLocations[i].position, spawnLocations[i].rotation);
        spawnedZombies.Add(spawnedZom);
        spawnedZom.GetComponent<ZombieController>().OnDeath.AddListener(OnZombieKilled);


        OnZombieSpawned?.Invoke(spawnedZom);
    }

    void OnZombieKilled(GameObject zomOb)
    {
        spawnedZombies.Remove(zomOb);   // remove zombie from list
        OnZombieRemoved?.Invoke(zomOb);
        zomOb.GetComponent<ZombieController>().OnDeath.RemoveListener(OnZombieKilled);
    }
}
