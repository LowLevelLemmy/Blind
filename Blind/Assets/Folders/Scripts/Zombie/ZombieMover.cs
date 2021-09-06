using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EasyButtons;

public class ZombieMover : MonoBehaviour
{
    [SerializeField] ZombieController zomCom;
    [SerializeField] CharacterController cc;
    [SerializeField] float avoidanceRadius = 2;
    [SerializeField] float avoidanceIntensity = 3;
    [SerializeField] public float maxAgentSpeed = 5;
    [SerializeField] public float speedMultiplier = .4f;

    NavMeshAgent agent => zomCom.agent;
    Vector3 avoidanceTamed;
    Vector3 rot;
    Vector3 lastPos;

    Director dir;


    void OnEnable()
    {
        lastPos = transform.position;
        agent.updatePosition = false;
        agent.updateRotation = false;
        dir = FindObjectOfType<Director>();
        CalculateSpeedMultiplier();
        agent.speed = maxAgentSpeed * speedMultiplier;
    }

    [Button]
    void CalculateSpeedMultiplier()
    {
        speedMultiplier = Mathf.Clamp01((.2f * dir.currentRound) + .15f);
        speedMultiplier *= Random.Range(.5f, 1f);
        //for (int i = 1; i < 10; ++i)
        //{
        //    print("On Round " + i + ":\t" + ((.2f * i) + .15f));
        //}
    }
    
    void Update()
    {
        if (zomCom.state != ZombieStates.CHASING)
            return;

        Vector3 agentDesiredPos = agent.nextPosition;
        Vector3 avoidanceVec = GetAvoidanceVector();

        avoidanceTamed = Vector3.Lerp(avoidanceTamed, avoidanceVec, avoidanceIntensity * Time.deltaTime);
        Vector3 abba = agentDesiredPos + avoidanceTamed;    // No dampening

        cc.Move(abba - lastPos);

        if (agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(agent.velocity), zomCom.rotSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        lastPos = transform.position;
    }

    [Button]
    Vector3 GetAvoidanceVector()
    {
        var zoms = GetNearbyZombies();
        Vector3 avoidanceVec = new Vector3();
        foreach(var zom in zoms)
        {
            Vector3 vecAway = (transform.position - zom.position);
            vecAway.y = 0;
            avoidanceVec += vecAway;
        }
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
