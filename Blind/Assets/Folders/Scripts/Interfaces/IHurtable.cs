using UnityEngine;

public interface IHurtable
{
    void OnHurt(Transform partHit = null, GameObject inflicter = null);
}
