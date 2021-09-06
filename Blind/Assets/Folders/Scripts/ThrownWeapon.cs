using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] LayerMask ignoredLayers;
    [SerializeField] GameObject shatterParticles;
    [SerializeField] bool destroyOnImpact;
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

        if (deadily && hurtable != null)
        {
            hurtable?.OnHurt();
            if (destroyOnImpact)
            {
                Instantiate(shatterParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        deadily = false;    // this way, it's deadily until it collides with something.
    }

}
