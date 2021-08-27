using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float gravity = -0.5f;    // good grav?

    PlayerInput playerInput;
    CharacterController cc;
    float speed = 12f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector3 move = transform.right * playerInput.move.x + transform.forward * playerInput.move.y;
        move.y += gravity;
        cc.Move(move * speed * Time.deltaTime);
    }
}
