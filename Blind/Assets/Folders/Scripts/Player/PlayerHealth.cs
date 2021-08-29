using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHurtable
{
    int hearts = 3;
    public void OnHurt()
    {
        --hearts;
        print("PLAYER HURT! Hearts Left: " + hearts);
    }
}
