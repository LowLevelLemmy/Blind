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

        var hurtable = col.transform.root.GetComponent<IHurtable>();

        if (deadily)
            hurtable?.OnHurt();

        deadily = false;    // this way, it's deadily until it collides with something.
    }

}
