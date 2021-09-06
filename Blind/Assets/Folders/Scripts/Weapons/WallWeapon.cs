using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] string weaponName = "Block Glock";
    [SerializeField] int price = 500;
    [SerializeField] float throwForce = 1;
    [SerializeField] GameObject spawnableWeapon;
    [SerializeField] GameObject particleEffect;
    [SerializeField] GameObject visual;
    [SerializeField] PointsMan pMan;
    [SerializeField] Transform weaponSpawnLoc;
    

    // Iinterface stupid ass idea:
    public bool altUse => true;
    public string interactTxt => "Press F to buy " + weaponName + " [Cost: " + price + "]";
    public float maxDist => 2.3f;
    public GameObject self => gameObject;

    void OnEnable()
    {
        pMan = GameObject.FindObjectOfType<PointsMan>();
    }

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        if (pMan.playerPoints < price)
            return;

        pMan.AddPoints(price * -1);
        ThrowWeapon();

        var partSys = Instantiate(particleEffect, weaponSpawnLoc.position, Quaternion.identity).GetComponent<ParticleSystem>();
        var partSysMainSettings = partSys.main;
        var renderer = partSys.GetComponent<Renderer>();
        renderer.material.SetVector("_EmissionColor", Color.white * 5);
        visual.SetActive(true);
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {

    }

    void ThrowWeapon()
    {
        Rigidbody rb = Instantiate(spawnableWeapon, weaponSpawnLoc.position, Quaternion.AngleAxis(90, Vector3.up)).GetComponent<Rigidbody>();
        Vector3 forceVec = weaponSpawnLoc.forward * throwForce;
        rb.AddForce(forceVec, ForceMode.Impulse);
        //rb.AddTorque(ExtensionMethods.RandomVec3(Vector3.one * -800, Vector3.one * 800));   // applied rotation
    }
}
