using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBar : AbstractWeapon
{
    [SerializeField] float swingRange = 3;
    [SerializeField] GameObject particles;
    public override void Fire()
    {
        if (ammo <= 0)
            return;

        if (!canFire)
            return;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, swingRange, layerMask, QueryTriggerInteraction.Ignore))
        {
            var hurtable = hit.transform.root.GetComponent<IHurtable>();
            hurtable?.OnHurt();
            --ammo;
        }
        base.Fire();

        if (ammo <= 0)
        {
            DestroyWeapon();
            Instantiate(particles, hit.point, Quaternion.identity);
        }
    }

    void DestroyWeapon()
    {
        var wepMan = GameObject.FindObjectOfType<WeaponManager>();
        wepMan.UnEquipWeapon();
    }
}
