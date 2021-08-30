using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentAnimater : MonoBehaviour
{
    ZombieController zomCon;
    NavMeshAgent agent;
    Animator anim;

    float vel;

    void Start()
    {
        zomCon = GetComponent <ZombieController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zomCon.state == ZombieStates.DEAD)
            return;

        vel = agent.velocity.magnitude / agent.speed;   // 0 to 1 range
        anim.SetFloat("Speed", vel);
    }
}
