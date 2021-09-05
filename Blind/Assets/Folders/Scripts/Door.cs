using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int pointsToUnlock;
    [SerializeField] int roomIndexToUnlock;

    public string interactTxt => "Press F to Open Door [Cost: " + pointsToUnlock + "]";
    public float maxDist => 3;
    public GameObject self => gameObject;
    public bool altUse => true;

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        PointsMan pMan = GameObject.FindObjectOfType<PointsMan>();
        if (pMan.playerPoints < pointsToUnlock)
            return;

        pMan.AddPoints(pointsToUnlock * -1);
        Director dir = GameObject.FindObjectOfType<Director>();

        if (!dir.unlockedRoomIndexs.Contains(roomIndexToUnlock))    //unlock room so zombies can spawn in it
            dir.unlockedRoomIndexs.Add(roomIndexToUnlock);

        transform.DOLocalMoveY(5, 2).OnComplete(() => Destroy(gameObject));
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {

    }
}
