using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentAnimater : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    float vel;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        vel = agent.velocity.magnitude / agent.speed;   // 0 to 1 range

        anim.SetFloat("Speed", vel);
    }
}
