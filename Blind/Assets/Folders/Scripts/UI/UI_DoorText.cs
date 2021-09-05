using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DoorText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI doorTxt;

    void OnEnable()
    {
        PlayerInteractor plrInt = GameObject.FindObjectOfType<PlayerInteractor>();
        plrInt.OnInteractTxtShouldComeUpNow += OnLookedAtTxt;
        plrInt.OnLostSightOfInteractable += OnLostSight;
    }

    void OnLookedAtTxt(IInteractable interactable)
    {
        doorTxt.text = interactable.interactTxt;
    }

    void OnLostSight()
    {
        doorTxt.text = "";
    }
}
