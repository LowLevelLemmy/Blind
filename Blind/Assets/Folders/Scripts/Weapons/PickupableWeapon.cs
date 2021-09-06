using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickupableWeapon : MonoBehaviour, IInteractable
{
    [Header("Settings:")]
    [SerializeField] float maxDistance = 5;
    public GameObject pickupableWeapon;
    public int ammo = -1;   // if ammo is -1 then use default ammo

    public float maxDist => maxDistance;
    public GameObject self => gameObject;
    string pickUpForTheFirstTimeTxt => "Hold F to pickup " + "Weapon";  // pickupableWeapon
    public string interactTxt { get => pickUpForTheFirstTimeTxt; }
    public bool altUse => false;

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        if (plrInteractor.plrCon.weaponManager.state != WepManState.NONE)
            return;

        Transform cam = plrInteractor.plrCon.plrCamera;
        Vector3 pos = cam.position;
        pos += (-cam.right * 0.3f);

        transform.DOMove(pos, 0.1f);
        transform.DORotateQuaternion(Random.rotation, 0.1f);

        DOVirtual.DelayedCall(.1f, () => plrInteractor.plrCon.weaponManager.EquipWeapon(this));
        DOVirtual.DelayedCall(.1f, () => Destroy(gameObject));
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {
        //TODO: display pickup? UI
    }
}
