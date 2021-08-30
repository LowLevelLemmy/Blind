using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EasyButtons;

public class ZombieMover : MonoBehaviour
{
    [SerializeField] ZombieController zomCom;
    [SerializeField] float avoidanceRadius = 2;
    [SerializeField] float avoidanceIntensity = 3;

    NavMeshAgent agent => zomCom.agent;

    Vector3 avoidanceTamed;
    Vector3 rot;

    void OnEnable()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (zomCom.state != ZombieStates.CHASING)
            return;

        Vector3 agentDesiredPos = agent.nextPosition;
        Vector3 avoidanceVec = GetAvoidanceVector();

        avoidanceTamed = Vector3.Lerp(avoidanceTamed, avoidanceVec, avoidanceIntensity * Time.deltaTime);
        transform.position = agentDesiredPos + avoidanceTamed;

        if (agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(agent.velocity), zomCom.rotSpeed * Time.deltaTime);
    }

    [Button]
    Vector3 GetAvoidanceVector()
    {
        var zoms = GetNearbyZombies();
        Vector3 avoidanceVec = new Vector3();
        foreach(var zom in zoms)
        {
            Vector3 vecAway = (transform.position - zom.position);
            avoidanceVec += vecAway;
        }
        print("Avoidance vec: " + avoidanceVec);
        return avoidanceVec.normalized;
    }

    [Button]
    void DEBUG_NearByZoms()
    {
        var abba = GetNearbyZombies();
        print("PRINTING ZOMBIES: ");
        foreach (var i in abba)
            print(i.name);
    }

    List<Transform> GetNearbyZombies()
    {
        List<Transform> nearByZoms = new List<Transform>();
        Collider[] cols = Physics.OverlapSphere(transform.position, avoidanceRadius);

        foreach (var c in cols)
        {
            if (c.gameObject == gameObject)
                continue;
            if (c.CompareTag("Zombie"))
                nearByZoms.Add(c.gameObject.transform);
        }

        return nearByZoms;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}
