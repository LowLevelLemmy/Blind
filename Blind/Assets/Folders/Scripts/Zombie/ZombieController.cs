using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Events;
using EasyButtons;

[System.Serializable]
public enum ZombieStates
{
    SPAWNING,
    CHASING,
    ATTACKING,
    DEAD
}

public class ZombieController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float maxAttackDistance = 1;
    [SerializeField] ZombieStates startingState;
    public float rotSpeed = 6;

    public NavMeshAgent agent;
    public Transform target;
    Animator anim;
    ZombieAttack zomAttack;

    public ZombieStates state { get; private set; }

    public UnityEvent<GameObject> OnDeath;

    [Button]
    public void SetState(ZombieStates newState)
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
        agent.enabled = false;
        DisableRagdoll();
        if (startingState == ZombieStates.CHASING)
            StartChasing();
        else
            state = ZombieStates.SPAWNING;
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

            case ZombieStates.DEAD:
                break;
        }
    }

    void ChasingThink()
    {
        if (target == null)
            return;
        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) <= maxAttackDistance)
        {
            if (zomAttack.CanAttack)
            {
                Attack();
            }
        }
    }

    void AttackingThink()
    {
        //rotate torward player
        if (target == null)
            return;
        agent.SetDestination(target.position);
        Vector3 abba = target.position - transform.position;
        abba.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(abba);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
    }

    void Attack()
    {
        SetState(ZombieStates.ATTACKING);
        //agent.destination = transform.position;
        zomAttack.AnimateAttack();
        DOVirtual.DelayedCall(zomAttack.attackCooldown, StartChasing);
    }

    public void StartChasing()
    {
        if (agent == null)
            return;
        agent.enabled = true;
        SetState(ZombieStates.CHASING);
    }

    public void Die()
    {
        SetState(ZombieStates.DEAD);
        EnableRagdoll();
        OnDeath?.Invoke(gameObject);
        AnimateDespawn();
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
                c.gameObject.layer = LayerMask.NameToLayer("IgnoreRagdolling");
                c.isTrigger = false;
                rb.isKinematic = false;
            }
        }
    }

    void AnimateDespawn()
    {
        GetComponent<ZombieShaderAnimatior>().AnimateFadeOut();
    }

    public void DisableRagdoll()
    {
        anim.enabled = true;

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
                c.isTrigger = false;
                rb.isKinematic = true;
            }
        }
    }
}
