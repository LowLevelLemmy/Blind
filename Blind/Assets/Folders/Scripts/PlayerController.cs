using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   // holds popular player vars and all scripts
{
    //
    //  Like a literal "Controller" set inputs here.
    //

    public Transform head;
    public Transform plrCamera;

    // Scripts:
    public PlayerMovement playerMovement;
    public PlayerLook playerLook;
    public WeaponManager weaponManager;
    public PlayerInteractor playerInteractioner;

    [Header("Inputs:")]
    public Vector2 input_Move;
    public Vector2 input_LookDelta;

    public bool input_Fire;
    public bool input_AltFire;
    public bool input_Use;
    public bool input_Jump;

    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0);
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        playerInteractioner = GetComponent<PlayerInteractor>();

        plrCamera = Camera.main.transform;
    }

    public void SetInputs(Vector2 move, Vector2 lookDelt, bool fire, bool altFire, bool use, bool jump)
    {
        input_Move = move;
        input_LookDelta = lookDelt;
        input_AltFire = altFire;
        input_Fire = fire;
        input_Use = use;
        input_Jump = jump;
    }
}
