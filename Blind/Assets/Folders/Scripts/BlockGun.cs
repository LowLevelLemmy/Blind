using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockGun : AbstractWeapon
{
    [SerializeField] GameObject bulletPrefab;
    public override void Fire()
    {
        if (!canFire)
            return;

        Vector3 bulletDirection = playerCam.forward;
        var spawnedBullet = Instantiate(bulletPrefab, playerCam.position + (playerCam.forward * 0.6f), playerCam.rotation);
        base.Fire();
    }
}
