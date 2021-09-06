using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carbine : AbstractWeapon
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject particles;

    public override void Fire()
    {
        if (!canFire)
            return;

        if (ammo <= 0)
            return;

        --ammo;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.root.TryGetComponent<IHurtable>(out var hurtable))
                hurtable.OnHurt();
        }
        base.Fire();

        Instantiate(particles, muzzle.position, muzzle.rotation);
    }
}
