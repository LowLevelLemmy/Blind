using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockGun : MonoBehaviour, IWeapon
{
    //Settings:
    [SerializeField] float timeBetweenShots;
    [SerializeField] float takeOutTime = 0.5666666666666667f;
    [SerializeField] LayerMask layerMask;

    // properties
    public WeaponManager owner { get; set; }

    // Dependecies
    Transform playerCam => owner.plrCon.plrCamera;
    Animator anim;

    float lastFireTime = -999;

    public void SetDependencies(WeaponManager owner)
    {
        anim = GetComponent<Animator>();
    }

    public void Throw()
    {
        print("Throw!");
    }

    public void TakeOutWeapon(TweenCallback call)
    {
        SetDependencies(owner);
        anim.Play("TakeOut");

        DOVirtual.DelayedCall(takeOutTime, call);
    }

    public void Fire()
    {
        if (Time.time - lastFireTime < timeBetweenShots)
            return;

        lastFireTime = Time.time;
        anim.Play("Fire");
        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            print("Hit: " + hit.collider.name);
            //GameObject decal = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //decal.transform.position = hit.point;
            //decal.transform.localScale = Vector3.one * 0.3f;
            //decal.GetComponent<Renderer>().material.color = Color.red;
        }
    }

}
