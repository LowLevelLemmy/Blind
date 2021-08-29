using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public bool ragdolled;  // TODO: Replace with enum of states. Called "Zombie States"
    public NavMeshAgent agent;
    Animator anim;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        ragdolled = true;
        anim.enabled = false;
        agent.enabled = false;

        Collider[] cols = gameObject.GetComponentsInChildren<Collider>();
        foreach (var c in cols)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (c.gameObject == gameObject)
            {
                c.enabled = false;
                Destroy(rb);
            }
            else
            {
                c.isTrigger = false;
                rb.isKinematic = false;
            }
        }
    }

    public void DisableRagdoll()
    {
        ragdolled = false;
        anim.enabled = true;
        agent.enabled = true;

        Collider[] cols = gameObject.GetComponentsInChildren<Collider>();
        foreach (var c in cols)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (c.gameObject == gameObject)
            {
                c.enabled = true;
            }
            else
            {
                c.isTrigger = true;
                rb.isKinematic = true;
            }
        }
    }
}
