using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    ALIVE,
    DEAD
}

public class PlayerController : MonoBehaviour, IControlable   // holds popular player vars and all scripts
{
    //  Like a literal "Controller" set inputs here.

    PlayerStates state;

    public Transform head;
    public Transform plrCamera;

    // Scripts:
    public PlayerMovement playerMovement;
    public PlayerLook playerLook;
    public WeaponManager weaponManager;
    public PlayerInteractor playerInteractioner;
    public PlayerHealth plrHealth;

    // Other
    public CharacterController cc;

    [Header("Inputs:")]
    public Vector2 input_Move;
    public Vector2 input_LookDelta;

    public bool input_Fire;
    public bool input_AltFire;
    public bool input_Use;
    public bool input_Jump;

    void Awake()
    {
        head = transform.GetChild(0);
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        playerInteractioner = GetComponent<PlayerInteractor>();
        plrHealth = GetComponent<PlayerHealth>();
        cc = GetComponent<CharacterController>();

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

    public void SwitchState(PlayerStates newState)
    {
        state = newState;
    }

    public bool CmpState(PlayerStates cmpState)
    {
        if (state == cmpState)
            return true;
        else
            return false;
    }
}
