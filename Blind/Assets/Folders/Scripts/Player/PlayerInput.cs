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
    public bool altUse;
    public bool jump;

    IControlable con;   // short for controller :)
    UI_PauseMenu pauseMenu;

    void OnEnable()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();
        con = GetComponent<IControlable>();
    }

    void Update()
    {
        if (pauseMenu.paused)
            return;

        con.SetInputs(move, lookDelta, fire, altFire, use, jump, altUse);
    }

    void LateUpdate()
    {
        fire = false;
        altFire = false;
        use = false;
        jump = false;
        altUse = false;
        lookDelta = Vector2.zero;
        con.SetInputs(move, lookDelta, fire, altFire, use, jump, altUse);
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

    void OnAltUse(InputValue context)
    {
        altUse = context.isPressed;
    }
}
