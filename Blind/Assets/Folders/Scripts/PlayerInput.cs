using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 move;
    public Vector2 lookDelta;

    void OnMove(InputValue context)
    {
        move = context.Get<Vector2>();
    }

    void OnLookDelta(InputValue context)
    {
        lookDelta = context.Get<Vector2>();
    }
}
