using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using EasyButtons;

public class Director : MonoBehaviour
{
    [Header("SETTINGS:")]
    [SerializeField] int zomsLeftToSpawn;
    public int currentRound = 0;

    public UnityEvent OnNewRound;

    int maxZombiesAlive => Mathf.CeilToInt(24 * Mathf.Clamp01(.2f * currentRound));

    ZombieSpawner zomSpawner;

    int zombiesAlive => zomSpawner.spawnedZombies.Count;

    private void OnEnable()
    {
        zomSpawner = GetComponent<ZombieSpawner>();
        zomSpawner.OnZombieRemoved.AddListener(OnZombieRemoved);
        DOVirtual.DelayedCall(1, StartNewRound);
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
        zomSpawner.SpawnZombie();
        --zomsLeftToSpawn;
    }

    [Button]
    void StartNewRound()
    {
        ++currentRound;
        OnNewRound?.Invoke();
        zomsLeftToSpawn = Mathf.CeilToInt(0.15f * currentRound * 24 + 2);
        print("NEW ROUND: " + currentRound + "\tAmmount Of Zoms: " + zomsLeftToSpawn + "\tMax Zombies Allowed: " + maxZombiesAlive);
        DOVirtual.DelayedCall(5, SpawnZoms);
    }
}
