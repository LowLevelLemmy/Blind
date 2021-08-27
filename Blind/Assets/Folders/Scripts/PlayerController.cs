using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   // holds popular player vars and all scripts
{
    public Transform head;
    public Transform plrCamera;

    // Scripts:
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerLook playerLook;
    public WeaponManager weaponManager;
    public PlayerInteractor playerInteractioner;

    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0);
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        playerInteractioner = GetComponent<PlayerInteractor>();

        plrCamera = Camera.main.transform;
    }
}
