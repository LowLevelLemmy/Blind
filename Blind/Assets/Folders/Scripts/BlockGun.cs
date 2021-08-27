using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockGun : MonoBehaviour, IWeapon
{
    [SerializeField] float timeBetweenShots;
    [SerializeField] LayerMask layerMask;

    // properties
    public WeaponManager owner { get; set; }

    // Dependecies
    Transform playerCam;
    Animator anim;

    float lastFireTime = -1;

    public void SetDependencies(WeaponManager owner)
    {
        anim = GetComponent<Animator>();
        playerCam = owner.plrCon.plrCamera;
    }

    public void Throw()
    {
        print("Throw!");
    }

    public void TakeOutWeapon(TweenCallback call)
    {
        SetDependencies(owner);
        anim.Play("TakeOut");
        float dur = anim.GetCurrentAnimatorStateInfo(0).length; // get duration of animation
        DOVirtual.DelayedCall(dur, call);
    }

    public void PutAwayWeapon(TweenCallback call)
    {
        SetDependencies(owner);
        anim.Play("PutAway");
        float dur = anim.GetCurrentAnimatorStateInfo(0).length; // get duration of animation
        DOVirtual.DelayedCall(dur, call);
    }

    public void Fire()
    {
        if (Time.time - lastFireTime < timeBetweenShots)
            return;

        lastFireTime = Time.time;

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
