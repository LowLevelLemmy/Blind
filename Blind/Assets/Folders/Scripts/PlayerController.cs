using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   // holds popular player vars and all scripts
{
    public Transform plrCamera;

    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerLook playerLook;
    public WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();

        plrCamera = Camera.main.transform;
    }
}
