using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickupableWeapon : MonoBehaviour, IInteractable
{
    [Header("Settings:")]
    [SerializeField] GameObject pickupableWeapon;
    [SerializeField] float maxDistance = 5;

    public float maxDist => maxDistance;
    public Vector3 pos => transform.position;
    string pickUpForTheFirstTimeTxt => "Hold F to pickup " + "Weapon";  // pickupableWeapon
    public string interactTxt { get => pickUpForTheFirstTimeTxt; }

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        if (plrInteractor.plrCon.weaponManager.state != WepManState.NONE)
            return;
        Transform cam = plrInteractor.plrCon.plrCamera;
        Vector3 pos = cam.position;
        pos += (-cam.right * 0.3f);

        transform.DOMove(pos, 0.1f);
        transform.DORotateQuaternion(Random.rotation, 0.1f);

        DOVirtual.DelayedCall(.1f, () => plrInteractor.plrCon.weaponManager.EquipWeapon(pickupableWeapon));
        DOVirtual.DelayedCall(.1f, () => Destroy(gameObject));
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {
        //TODO: display pickup? UI
    }
}
