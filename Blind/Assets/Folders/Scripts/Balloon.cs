using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour, IHurtable
{
    public void OnHurt()
    {
        print("Balloon: OUCH!");
    }
}
