using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum ZombieStates
{
    CHASING,
    ATTACKING,
    DEAD
}

public class ZombieController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float maxAttackDistance = 1;
    [SerializeField] float attackRotationSpeed = 6;

    public NavMeshAgent agent;
    public Transform target;
    Animator anim;
    ZombieAttack zomAttack;

    public ZombieStates state { get; private set; }

    void SetState(ZombieStates newState)
    {
        if (state != ZombieStates.DEAD)
            state = newState;
    }

    void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        zomAttack = GetComponent<ZombieAttack>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        DisableRagdoll();
    }

    void Update()
    {
        Think();
    }

    void Think()
    {
        switch (state)
        {
            case ZombieStates.CHASING:
                ChasingThink();
                break;

            case ZombieStates.ATTACKING:
                AttackingThink();
                break;
        }
    }

    void AttackingThink()
    {
        //rotate torward player
        Vector3 abba = target.position - transform.position;
        abba.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(abba);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, attackRotationSpeed * Time.deltaTime);
    }

    void ChasingThink()
    {
        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) <= maxAttackDistance)
        {
            if (zomAttack.CanAttack)
                Attack();
        }
    }

    void Attack()
    {
        state = ZombieStates.ATTACKING;
        agent.isStopped = true;
        zomAttack.AnimateAttack();
        DOVirtual.DelayedCall(zomAttack.attackCooldown, StartChasing);
    }

    void StartChasing()
    {
        SetState(ZombieStates.CHASING);
        agent.isStopped = false;
    }

    public void Die()
    {
        SetState(ZombieStates.DEAD);
        EnableRagdoll();
    }

    void EnableRagdoll()
    {
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
