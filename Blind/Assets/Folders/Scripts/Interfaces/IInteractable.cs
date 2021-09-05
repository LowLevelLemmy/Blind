using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public bool altUse { get; }
    public string interactTxt { get; }
    public float maxDist { get; }
    public GameObject self { get; }
    public void OnLookedAt(PlayerInteractor plrInteractor);
    public void OnInteractedWith(PlayerInteractor plrInteractor);
}
