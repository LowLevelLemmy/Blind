using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float gravity = -9.81f;    // good grav?
    [SerializeField] float jumpHeight = -5f;
    [SerializeField] float speed = 12f;

    PlayerInput playerInput;
    CharacterController cc;
    Vector3 verticleVel;
    bool isGrounded;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        isGrounded = cc.isGrounded;
        if (isGrounded)
            verticleVel.y = -2;

        Vector3 move = transform.right * playerInput.move.x + transform.forward * playerInput.move.y;
        cc.Move(move * speed * Time.deltaTime);


        // Changes the height position of the player..
        if (playerInput.jump && isGrounded)
            verticleVel.y = jumpHeight;

        verticleVel.y += gravity * Time.deltaTime;
        cc.Move(verticleVel * Time.deltaTime);
    }
}
