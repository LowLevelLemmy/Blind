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
            GameObject decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            decal.transform.position = hit.point;
            decal.transform.localScale = Vector3.one * 0.3f;
            decal.GetComponent<Renderer>().material.color = Color.blue;
        }
        base.Fire();
    }
}
