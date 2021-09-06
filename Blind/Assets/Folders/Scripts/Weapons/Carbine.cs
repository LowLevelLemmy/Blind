using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carbine : AbstractWeapon
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject particles;
    [SerializeField] int penetrationCount = 2;

    public override void Fire()
    {
        if (!canFire)
            return;

        if (ammo <= 0)
            return;

        --ammo;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;

        var cols = Physics.RaycastAll(playerCam.position, bulletDirection, 1000, layerMask, QueryTriggerInteraction.Ignore);

        int penCount = penetrationCount;
        List<IHurtable> enemiesHit = new List<IHurtable>();
        foreach(var col in cols)
        {
            if (col.transform.root.TryGetComponent<IHurtable>(out var hurtable))
            {
                if (!enemiesHit.Contains(hurtable))
                    enemiesHit.Add(hurtable);
            }
        }

        if (enemiesHit.Count != 0)
        {
            if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
            {
                Instantiate(bloodParticles, hit.point, Quaternion.identity);
            }
        }

        foreach(var hurt in enemiesHit)
        {
            if (penCount <= 0)
                break;

            hurt.OnHurt();
            --penCount;
        }


        base.Fire();

        Instantiate(particles, muzzle.position, muzzle.rotation);
    }
}
