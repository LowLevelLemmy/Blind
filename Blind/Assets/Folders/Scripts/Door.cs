using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using EasyButtons;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int pointsToUnlock;
    [SerializeField] int roomIndexToUnlock;
    [SerializeField] float timeItTakesToOpen = 2;
    bool unlocked = false;

    public string interactTxt => "Press F to Open Door [Cost: " + pointsToUnlock + "]";
    public float maxDist => 3;
    public GameObject self => gameObject;
    public bool altUse => true;

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        if (unlocked)
            return;
        unlocked = true;

        PointsMan pMan = GameObject.FindObjectOfType<PointsMan>();
        if (pMan.playerPoints < pointsToUnlock)
            return;

        pMan.AddPoints(pointsToUnlock * -1);

        Director dir = GameObject.FindObjectOfType<Director>();
        if (!dir.unlockedRoomIndexs.Contains(roomIndexToUnlock))    //unlock room so zombies can spawn in it
            dir.unlockedRoomIndexs.Add(roomIndexToUnlock);

        UnlockDoor();
    }

    [Button]
    void UnlockDoor()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        transform.DOLocalMoveY(10, timeItTakesToOpen).OnComplete(() => Destroy(gameObject));
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {

    }
}
