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

    CharacterController cc;
    PlayerController plrCon;
    Vector3 verticleVel;
    bool isGrounded;

    void Start()
    {
        plrCon = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = cc.isGrounded;
        if (isGrounded)
            verticleVel.y = -2;

        Vector3 move = transform.right * plrCon.input_Move.x + transform.forward * plrCon.input_Move.y;
        cc.Move(move * speed * Time.deltaTime);


        // Changes the height position of the player..
        if (plrCon.input_Jump && isGrounded)
            verticleVel.y = jumpHeight;

        verticleVel.y += gravity * Time.deltaTime;
        cc.Move(verticleVel * Time.deltaTime);
    }
}
