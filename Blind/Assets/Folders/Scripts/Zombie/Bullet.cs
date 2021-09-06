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

    void OnCollisionEnter(Collision col)
    {
        var hurtable = col.transform.root.GetComponent<IHurtable>();
        hurtable?.OnHurt();
        print(col.transform.root.name);

        // TODO: Instantiate some particles too
        Destroy(gameObject);
    }
}
