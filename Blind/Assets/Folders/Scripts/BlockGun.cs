using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGun : MonoBehaviour, IWeapon
{
    [SerializeField] float timeBetweenShots;
    [SerializeField] LayerMask layerMask;

    // properties
    public WeaponManager owner { get; set; }

    // Dependecies
    Transform playerCam;

    float lastFireTime = -1;

    void Start()
    {

    }

    public void SetDependencies(WeaponManager owner)
    {
        playerCam = owner.playerCam;
    }

    void Update()
    {

    }
    public void Throw()
    {
        print("Throw!");
    }

    public void TakeOutWeapon()
    {
        SetDependencies(owner);
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
