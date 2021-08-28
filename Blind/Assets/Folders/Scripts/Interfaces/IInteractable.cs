using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public string interactTxt { get; }
    public float maxDist { get; }
    public Vector3 pos { get; }
    public void OnLookedAt(PlayerInteractor plrInteractor);
    public void OnInteractedWith(PlayerInteractor plrInteractor);
}
