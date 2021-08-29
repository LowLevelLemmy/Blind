using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius;

    [Button]
    void Attack()
    {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackRadius);
        foreach (var col in cols)
        {
            print("Zombie hit: " + col.name);
            if (col.CompareTag("Player"))
            {
                var hurtable = col.GetComponent<IHurtable>();
                hurtable.OnHurt();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
