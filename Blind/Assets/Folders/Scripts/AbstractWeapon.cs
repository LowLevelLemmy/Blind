using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class AbstractWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject throwableWeapon;

    [Header("Settings")]
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float takeOutTime = 0.57f;
    [SerializeField] protected float throwForce = 10f;
    [SerializeField] protected LayerMask layerMask;

    public WeaponManager owner;

    protected Animator anim;
    protected float lastFireTime = -999;

    protected Transform playerCam => owner.plrCon.plrCamera;
    protected bool canFire => Time.time - lastFireTime >= timeBetweenShots;
    protected virtual void GetDependencies()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Throw() // handles throwing physics
    {
        Vector3 spawnPos = playerCam.position + (playerCam.forward * 0.3f) + (playerCam.up * 0.1f);
        Rigidbody rb = Instantiate(throwableWeapon, spawnPos, Random.rotation).GetComponent<Rigidbody>();

        Vector3 forceVec = playerCam.forward * throwForce;
        rb.AddForce(forceVec, ForceMode.Impulse);
        rb.AddTorque(ExtensionMethods.RandomVec3(Vector3.one * -800, Vector3.one * 800));   // applied rotation
    }
    public virtual void TakeOutWeapon(TweenCallback call)
    {
        GetDependencies();
        anim.Play("TakeOut");
        DOVirtual.DelayedCall(takeOutTime, () => Func(call));
    }

    protected virtual void Func(TweenCallback cally)  // SUPER HORRIBLE FIX   // I made this bc the delayed call would happpen AFTER the weapon is thrown and destroyed. This "Func" prevents that
    {
        // idk how "func" even RUNS if I destoryed the gameobject it's attached to. But who knows?
        if (this != null && owner != null)
            cally();
    }   // This func fix causes DoTween to have safe mode errors. Weird

    public virtual void Fire()
    {
        if (!canFire)
            return;

        lastFireTime = Time.time;
        anim.Play("Fire");
    }
}
