using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollow : MonoBehaviour
{
    ZombieController zomCon;
    [SerializeField] Transform player;
    NavMeshAgent agent;

    void Start()
    {

        zomCon = GetComponent<ZombieController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!zomCon.ragdolled)
            agent.SetDestination(player.position);
    }
}
