using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockGun : MonoBehaviour, IWeapon
{
    [Header("References")]
    [SerializeField] GameObject throwableWeapon;

    [Header("Settings")]
    [SerializeField] float timeBetweenShots;
    [SerializeField] float takeOutTime = 0.57f;
    [SerializeField] float throwForce = 10f;
    [SerializeField] LayerMask layerMask;

    // properties
    public WeaponManager owner { get; set; }
    public GameObject gunGameObject => gameObject;

    Transform playerCam => owner.plrCon.plrCamera;


    Animator anim;

    float lastFireTime = -999;

    public void GetDependencies()
    {
        anim = GetComponent<Animator>();
    }

    public void Throw() // handles throwing physics
    {
        Vector3 spawnPos = playerCam.position + (playerCam.forward * 0.3f) + (playerCam.up * 0.1f);
        Rigidbody rb = Instantiate(throwableWeapon, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();

        Vector3 forceVec = playerCam.forward * throwForce;
        rb.AddForce(forceVec, ForceMode.Impulse);
        rb.AddTorque(ExtensionMethods.RandomVec3(Vector3.one * -800, Vector3.one * 800));   // applied rotation
    }

    public void TakeOutWeapon(TweenCallback call)
    {
        GetDependencies();
        anim.Play("TakeOut");
        DOVirtual.DelayedCall(takeOutTime, () => func(call));
    }

    void func(TweenCallback cally)  // SUPER HORRIBLE FIX   // I made this bc the delayed call would happpen AFTER the weapon is thrown and destroyed. This "Func" prevents that
    {
        // idk how "func" even RUNS if I destoryed the gameobject it's attached to. But who knows?
        if (this != null && owner != null)
            cally();
    }   // This func fix causes DoTween to have safe mode errors. Weird

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
