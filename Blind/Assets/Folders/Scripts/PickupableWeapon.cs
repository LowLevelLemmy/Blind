using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject pickupableWeapon;
    string pickUpForTheFirstTimeTxt => "Hold F to pickup " + "Weapon";  // pickupableWeapon.;

    public string interactTxt { get => pickUpForTheFirstTimeTxt; }

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        plrInteractor.plrCon.weaponManager.EquipWeapon(pickupableWeapon);
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {
        print("LOOKED AT");
    }
}
