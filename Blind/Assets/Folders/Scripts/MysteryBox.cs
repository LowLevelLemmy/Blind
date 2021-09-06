using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MysteryBox : MonoBehaviour, IInteractable
{
    [Header("Settings:")]
    [SerializeField] int price = 950;
    [SerializeField] float throwForce = 10;
    [SerializeField] List<GameObject> spawnableWeapons;
    [SerializeField] Transform weaponSpawnLoc;

    [Header("Other")]
    [SerializeField] PointsMan pMan;
    [SerializeField] GameObject particleEffect;

    // IInteractable:
    public bool altUse => true;
    public string interactTxt => "Press F to hit the BOX [Cost: " + price + "]";
    public float maxDist => 3;
    public GameObject self => gameObject;

    bool boxBusy = false;

    void OnEnable()
    {
        pMan = GameObject.FindObjectOfType<PointsMan>();
    }

    public void OnInteractedWith(PlayerInteractor plrInteractor)
    {
        if (boxBusy)
            return;
        if (pMan.playerPoints < price)
            return;

        boxBusy = true;
        pMan.AddPoints(price * -1);
        transform.DOPunchScale(transform.localScale * -.3f, .3f, 10, .5f).OnComplete(() => boxBusy = false);
        ThrowWeapon(Random.Range(0, spawnableWeapons.Count));

        var partSys = Instantiate(particleEffect, weaponSpawnLoc.position, Quaternion.identity).GetComponent<ParticleSystem>();
        var partSysMainSettings = partSys.main;
        var renderer = partSys.GetComponent<Renderer>();
        renderer.material.SetVector("_EmissionColor", Color.yellow * 5);
    }

    void ThrowWeapon(int i)
    {
        Rigidbody rb = Instantiate(spawnableWeapons[i], weaponSpawnLoc.position, Random.rotation).GetComponent<Rigidbody>();
        Vector3 forceVec = weaponSpawnLoc.forward * throwForce;
        rb.AddForce(forceVec, ForceMode.Impulse);
        rb.AddTorque(ExtensionMethods.RandomVec3(Vector3.one * -800, Vector3.one * 800));   // applied rotation
    }

    public void OnLookedAt(PlayerInteractor plrInteractor)
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
