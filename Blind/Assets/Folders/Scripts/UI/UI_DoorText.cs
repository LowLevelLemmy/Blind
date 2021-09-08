using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DoorText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI doorTxt;
    [SerializeField] GameObject bg;

    void OnEnable()
    {
        PlayerInteractor plrInt = GameObject.FindObjectOfType<PlayerInteractor>();
        plrInt.OnInteractTxtShouldComeUpNow += OnLookedAtTxt;
        plrInt.OnLostSightOfInteractable += OnLostSight;

        doorTxt.text = "";
        bg.SetActive(false);
    }

    void OnLookedAtTxt(IInteractable interactable)
    {
        bg.SetActive(true);
        doorTxt.text = interactable.interactTxt;
    }

    void OnLostSight(IInteractable interactable)
    {
        bg.SetActive(false);
        doorTxt.text = "";
    }
}
