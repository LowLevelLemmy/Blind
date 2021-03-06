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


        wepMan.OnWeaponEquiped.AddListener(OnWeaponEquiped);
        wepMan.OnWeaponUnEquiped.AddListener(OnWeaponUnEquiped);
        plrCon.plrHealth.OnPlayerDied.AddListener(OnPlrDeath);

        grabIcon.SetActive(false);
        ShowDot();
    }

    public void HideAll()
    {
        print("hiding all");
        dot.SetActive(false);
        grabIcon.SetActive(false);
        crossHair.SetActive(false);
    }

    void OnPlrDeath()
    {
        HideAll();
    }

    void OnDisable()
    {
        plrInteractor.OnInteractableLookedAt -= OnInteractableLookedAt;
        plrInteractor.OnLostSightOfInteractable -= OnLostSightOfInteractable;
    }


    void OnInteractableLookedAt(IInteractable interactable)
    {
        if (plrCon.CmpState(PlayerStates.DEAD))
            return;
        if (plrCon.weaponManager.state == WepManState.NONE)
        {
            if (!interactable.altUse)
                grabIcon.SetActive(true);   // display hand img
        }
    }

    void OnLostSightOfInteractable(IInteractable interactable)
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
        if (plrCon.CmpState(PlayerStates.DEAD))
            return;
        dot.SetActive(false);
        crossHair.SetActive(true);
    }

    void ShowDot()
    {
        if (plrCon.CmpState(PlayerStates.DEAD))
            return;
        dot.SetActive(true);
        crossHair.SetActive(false);
    }
}
