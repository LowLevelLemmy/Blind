using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Crosshair : MonoBehaviour   // handles center of the canvas pretty much
{
    [SerializeField] GameObject dot;
    [SerializeField] GameObject grabIcon;
    [SerializeField] GameObject crossHair;

    PlayerController plrCon;
    PlayerInteractor plrInteractor;
    [SerializeField] WeaponManager wepMan;

    void OnEnable()
    {
        plrCon = GameObject.FindObjectOfType<PlayerController>();
        plrInteractor = plrCon.GetComponent<PlayerInteractor>();

        plrInteractor.OnInteractableLookedAt += OnInteractableLookedAt;
        plrInteractor.OnLostSightOfInteractable += OnLostSightOfInteractable;

        grabIcon.SetActive(false);

        wepMan.OnWeaponEquiped.AddListener(OnWeaponEquiped);
        wepMan.OnWeaponUnEquiped.AddListener(OnWeaponUnEquiped);

        ShowDot();
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

    void OnWeaponEquiped(AbstractWeapon newWep)
    {
        if (newWep.shouldDisplayCrosshair)
            ShowCrosshair();
        else
            ShowDot();
    }

    void OnWeaponUnEquiped(AbstractWeapon newWep)
    {
        ShowDot();
    }

    void ShowCrosshair()
    {
        dot.SetActive(false);
        crossHair.SetActive(true);
    }

    void ShowDot()
    {
        dot.SetActive(true);
        crossHair.SetActive(false);
    }
}
