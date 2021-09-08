using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using DG.Tweening;

public class ZombieAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float attackRadius;
    [SerializeField] float attackFrame = 33;  // time after attack animation played, to call Attack function
    public float attackCooldown = 2;  // time after attack animation played, to call Attack function

    [SerializeField] Transform attackPoint;

    public bool CanAttack => Time.time - lastAttackTime >= attackCooldown;
    public float AttackDur => attackFrame / 30;

    ZombieController zomCom;
    Animator anim;
    public float lastAttackTime = -999;

    void OnEnable()
    {
        anim = GetComponent<Animator>();
        zomCom = GetComponent<ZombieController>();
    }

    public void AnimateAttack()
    {
        lastAttackTime = Time.time;
        anim.SetTrigger("Attack");
        DOVirtual.DelayedCall(AttackDur, Attack);
    }

    void Attack()
    {
        if (zomCom.state != ZombieStates.ATTACKING)
            return;

        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRadius);
        bool attacked = false;
        foreach (var col in cols)
        {
            if (col.CompareTag("Player"))
            {
                var hurtable = col.transform.root.GetComponent<IHurtable>();
                hurtable.OnHurt(null, gameObject);
                attacked = true;
            }

            if (attacked)
                return;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
