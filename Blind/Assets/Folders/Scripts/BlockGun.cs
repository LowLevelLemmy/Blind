using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockGun : AbstractWeapon
{
    public override void Fire()
    {
        if (!canFire)
            return;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent<IHurtable>(out var hurtable))
                hurtable.OnHurt();
        }
        base.Fire();
    }
}
