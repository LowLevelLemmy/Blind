using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IHurtable
{

    public void OnHurt()
    {
        // ragdoll
        // despawn after 5 secs
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void EnableRagdoll()
    {

    }
}
