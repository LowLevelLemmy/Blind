using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : AbstractWeapon
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject particles;
    [SerializeField] float range = 3;
    [SerializeField] float radius = 2;

    public override void Fire()
    {
        if (!canFire)
            return;

        if (ammo <= 0)
            return;

        --ammo;

        Vector3 bulletDirection = playerCam.forward;
        var cols = Physics.SphereCastAll(playerCam.position, radius, bulletDirection, range, layerMask, QueryTriggerInteraction.Ignore);
        foreach(var col in cols)
        {
            if (col.transform.root.TryGetComponent<IHurtable>(out var hurtable))
                hurtable.OnHurt();
        }

        base.Fire();

        GameObject papa = Instantiate(particles, muzzle.position, muzzle.rotation);
        papa.transform.localScale = particles.transform.localScale * 2;
    }
}
