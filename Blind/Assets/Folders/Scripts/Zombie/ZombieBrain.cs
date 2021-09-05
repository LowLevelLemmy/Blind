using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBrain : MonoBehaviour
{
    Vector2 move;
    Vector2 lookDelta;
    bool fire;
    bool altFire;
    bool use;
    bool jump;

    IControlable con;   // short for controller :)
    void OnEnable()
    {
        con = GetComponent<IControlable>();
    }
    void Update()
    {
        MoveTorwardPlayer();
        con.SetInputs(move, lookDelta, fire, altFire, use, jump, false);
    }

    void MoveTorwardPlayer()
    {
        move.y = 1.0f;
    }
}
