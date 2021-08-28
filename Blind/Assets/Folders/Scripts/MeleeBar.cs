using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBar : AbstractWeapon
{
    [SerializeField] float swingRange = 3;
    public override void Fire()
    {
        if (!canFire)
            return;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, swingRange, layerMask, QueryTriggerInteraction.Ignore))
        {
            print("Swing hit: " + hit.collider.name);
            if (hit.collider.TryGetComponent<IHurtable>(out var hurtable))
                hurtable.OnHurt();
        }
        base.Fire();
    }
}
