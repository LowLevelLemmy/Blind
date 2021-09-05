using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EasyButtons;
using DG.Tweening;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<Transform> spawnRooms = new List<Transform>();
    [SerializeField] GameObject zombiePrefab;
    public List<GameObject> spawnedZombies = new List<GameObject>();

    public UnityEvent<GameObject> OnZombieSpawned;
    public UnityEvent<GameObject> OnZombieRemoved;

    [SerializeField] float jumpPower;
    [SerializeField] float dur;


    [Button]
    public void SpawnZombie(int roomIndex = -1, int windowIndex = -1)
    {
        if (roomIndex == -1 || windowIndex == -1)
        {
            roomIndex = Random.Range(0, spawnRooms.Count);
            windowIndex = Random.Range(0, spawnRooms[roomIndex].childCount);
        }

        Transform spawnTrans = spawnRooms[roomIndex].GetChild(windowIndex);

        GameObject spawnedZom = Instantiate(zombiePrefab, spawnTrans.position, spawnTrans.rotation);
        spawnedZombies.Add(spawnedZom);
        var zomCon = spawnedZom.GetComponent<ZombieController>();
        zomCon.OnDeath.AddListener(OnZombieKilled);


        spawnedZom.GetComponent<Animator>().SetTrigger("RunJump");

        AnimateJump(spawnedZom.transform, spawnTrans.GetChild(0).position, zomCon);
        OnZombieSpawned?.Invoke(spawnedZom);
    }

    void OnZombieKilled(GameObject zomOb)
    {
        spawnedZombies.Remove(zomOb);   // remove zombie from list
        OnZombieRemoved?.Invoke(zomOb);
        zomOb.GetComponent<ZombieController>().OnDeath.RemoveListener(OnZombieKilled);
    }

    void AnimateJump(Transform tran, Vector3 landingPos, ZombieController zomCon)
    {
        tran.DOJump(landingPos, jumpPower, 0, dur).OnComplete(zomCon.StartChasing);
    }
}
