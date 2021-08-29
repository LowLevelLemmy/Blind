using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IHurtable
{
    ZombieController zomCon;

    void Start()
    {
        zomCon = GetComponent<ZombieController>();
    }

    public void OnHurt()
    {
        // ragdoll
        // despawn after 5 secs
        zomCon.EnableRagdoll();
        Destroy(gameObject, 5);
    }
}
