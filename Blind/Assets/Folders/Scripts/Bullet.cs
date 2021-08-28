using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<IHurtable>(out var hurtable))
        {
            hurtable.OnHurt();
        }

        // Instantiate some particles too
        Destroy(gameObject);
    }
}
