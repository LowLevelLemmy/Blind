using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using EasyButtons;
using System.Collections.Generic;

public class Director : MonoBehaviour
{
    [Header("SETTINGS:")]
    [SerializeField] float delayAfterKillingLastZom = 2f;
    [SerializeField] float startingDelay = 8f;

    public UnityEvent OnNewRound;
    public List<int> unlockedRoomIndexs = new List<int>();  // used by doors to add room indexes to spawn zombies from
    public int currentRound = 0;


    int maxZombiesAlive => Mathf.CeilToInt(24 * Mathf.Clamp01(.2f * currentRound));


    ZombieSpawner zomSpawner;
    int zomsLeftToSpawn;
    int zombiesAlive => zomSpawner.spawnedZombies.Count;

    private void OnEnable()
    {
        zomSpawner = GetComponent<ZombieSpawner>();
        zomSpawner.OnZombieRemoved.AddListener(OnZombieRemoved);
        DOVirtual.DelayedCall(startingDelay, StartNewRound);
    }

    void OnZombieRemoved(GameObject zom)
    {
        if (zomSpawner.spawnedZombies.Count == 0 && zomsLeftToSpawn == 0)   // no zombies left
        {
            // TODO: play omonious music here
            DOTween.defaultTimeScaleIndependent = true;
            DOVirtual.DelayedCall(delayAfterKillingLastZom, StartNewRound);
            DOTween.defaultTimeScaleIndependent = false;
        }
    }

    void SpawnZoms()
    {
        StartCoroutine(SpawnZomCour());
    }

    IEnumerator SpawnZomCour()
    {
        while (zomsLeftToSpawn > 0)
        {
            while (zombiesAlive >= maxZombiesAlive)    // if we are at MAX CAPACITY
            {
                yield return new WaitForSeconds(3f);
                continue;
            }

            float delayBetweenZoms = (Random.value + .1f) * 5;
            SpawnZom();
            yield return new WaitForSeconds(delayBetweenZoms);
        }
    }

    void SpawnZom()
    {
        int roomIndex = Random.Range(0, unlockedRoomIndexs.Count);
        zomSpawner.SpawnZombie(unlockedRoomIndexs[roomIndex]);
        --zomsLeftToSpawn;
    }

    [Button]
    void StartNewRound()
    {
        ++currentRound;
        OnNewRound?.Invoke();
        zomsLeftToSpawn = Mathf.CeilToInt(0.15f * currentRound * 24 + 2);
        print("NEW ROUND: " + currentRound + "\tAmmount Of Zoms: " + zomsLeftToSpawn + "\tMax Zombies Allowed: " + maxZombiesAlive);
        SpawnZoms();
        //TODO: Add sound after last zombie killed.
    }
}
