using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Crosshair : MonoBehaviour
{
    [SerializeField] GameObject dot;
    [SerializeField] GameObject grabIcon;
    PlayerController plrCon;
    PlayerInteractor plrInteractor;

    void OnEnable()
    {
        plrCon = GameObject.FindObjectOfType<PlayerController>();
        plrInteractor = plrCon.GetComponent<PlayerInteractor>();

        plrInteractor.OnInteractableLookedAt += OnInteractableLookedAt;
        plrInteractor.OnLostSightOfInteractable += OnLostSightOfInteractable;

        grabIcon.SetActive(false);
    }

    void OnDisable()
    {
        plrInteractor.OnInteractableLookedAt -= OnInteractableLookedAt;
        plrInteractor.OnLostSightOfInteractable -= OnLostSightOfInteractable;
    }


    void OnInteractableLookedAt()
    {
        if (plrCon.weaponManager.state == WepManState.NONE)
            grabIcon.SetActive(true);   // display hand img
    }
    void OnLostSightOfInteractable()
    {
        // hide hand img
        grabIcon.SetActive(false);
    }
}
