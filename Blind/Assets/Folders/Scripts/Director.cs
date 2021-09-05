using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Director : MonoBehaviour
{
    [Header("SETTINGS:")]
    [SerializeField] int currentRound = 0;
    [SerializeField] int zomsOnRound1 = 5;
    [SerializeField] int zomsLeftToSpawn;
    [SerializeField] int maxZombiesAlive= 3;
    [SerializeField] float delayFactor = 3;

    ZombieSpawner zomSpawner;

    int zombiesAlive => zomSpawner.spawnedZombies.Count;

    private void OnEnable()
    {
        zomSpawner = GetComponent<ZombieSpawner>();
        zomSpawner.OnZombieRemoved.AddListener(OnZombieRemoved);
        StartNewRound();
    }

    void OnZombieRemoved(GameObject zom)
    {
        if (zomSpawner.spawnedZombies.Count == 0 && zomsLeftToSpawn == 0)   // no zombies left
            StartNewRound();
    }

    void SpawnZoms()
    {
        StartCoroutine(SpawnZomCour());
    }

    IEnumerator SpawnZomCour()
    {
        while (zomsLeftToSpawn > 0)
        {
            if (zombiesAlive >= maxZombiesAlive)    // if we aren't at MAX CAPACITY
            {
                yield return new WaitForSeconds(3f);
                continue;
            }

            float delayBetweenZoms = (Random.value + .1f) * delayFactor;
            SpawnZom();
            yield return new WaitForSeconds(delayBetweenZoms);
        }
    }

    void SpawnZom()
    {
        //zomSpawner.SpawnZombie();
        --zomsLeftToSpawn;
    }


    void StartNewRound()
    {
        ++currentRound;
        print("NEW ROUND: " + currentRound);
        zomsLeftToSpawn += zomsOnRound1 * currentRound;
        delayFactor *= .99f;
        SpawnZoms();
    }
}
