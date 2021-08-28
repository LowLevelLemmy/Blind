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
            print("Hit: " + hit.collider.name);
            GameObject decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            decal.transform.position = hit.point;
            decal.transform.localScale = Vector3.one * 0.3f;
            decal.GetComponent<Renderer>().material.color = Color.red;
        }
        base.Fire();
    }
}
