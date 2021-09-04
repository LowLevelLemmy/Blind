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

    public void OnHurt(GameObject inflicter = null)
    {
        zomCon.Die();
    }
}
