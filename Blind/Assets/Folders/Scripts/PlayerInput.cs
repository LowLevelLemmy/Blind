using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 move;
    public Vector2 lookDelta;

    public bool fire;
    public bool altFire;
    public bool use;
    public bool jump;

    PlayerController plrCon;

    private void OnEnable()
    {
        plrCon = GetComponent<PlayerController>();
    }

    void Update()
    {
        plrCon.SetInputs(move, lookDelta, fire, altFire, use, jump);
    }

    void LateUpdate()
    {
        fire = false;
        altFire = false;
        use = false;
        jump = false;
    }

    void OnMove(InputValue context)
    {
        move = context.Get<Vector2>();
    }

    void OnLookDelta(InputValue context)
    {
        lookDelta = context.Get<Vector2>();
    }

    void OnFire(InputValue context)
    {
        fire = context.isPressed;
    }

    void OnAltFire(InputValue context)
    {
        altFire = context.isPressed;
    }

    void OnUse(InputValue context)
    {
        use = context.isPressed;
    }

    void OnJump(InputValue context)
    {
        jump = context.isPressed;
    }
}
