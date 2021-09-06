using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentAnimater : MonoBehaviour
{
    ZombieController zomCon;
    NavMeshAgent agent;
    Animator anim;
    ZombieMover mover;
    float vel;
    float maxSpeed;

    void Start()
    {
        zomCon = GetComponent <ZombieController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        mover = GetComponent<ZombieMover>();
        //anim.SetFloat("Speed", mover.speedMultiplier);
        maxSpeed = mover.maxAgentSpeed;
    }

    void Update()
    {
        if (zomCon.state == ZombieStates.DEAD)
            return;

        vel = agent.velocity.magnitude / maxSpeed;   // 0 to 1 range
        anim.SetFloat("Speed", vel);
    }
}
