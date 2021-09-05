using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IHurtable
{
    [SerializeField] int pointsPerKill = 50;
    ZombieController zomCon;

    void Start()
    {
        zomCon = GetComponent<ZombieController>();
    }

    public void OnHurt(GameObject inflicter = null)
    {
        PointsMan.instance.AddPoints(pointsPerKill);
        zomCon.Die();
    }
}
