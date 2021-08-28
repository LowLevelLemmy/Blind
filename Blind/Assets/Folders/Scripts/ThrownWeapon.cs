using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] LayerMask ignoredLayers;
    bool deadily = true;

    void OnEnable()
    {
        deadily = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))    // Ignore Player
            return;

        //print("collided with:" + col.gameObject.name);

        if (col.gameObject.TryGetComponent<IHurtable>(out var hurtable))
        {
            if (deadily)
                hurtable.OnHurt();
        }

        deadily = false;    // this way, it's deadily until it collides with something.
    }

}
