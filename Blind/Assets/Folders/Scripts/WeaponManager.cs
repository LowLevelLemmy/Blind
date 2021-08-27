using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

[System.Serializable]
public enum WepManState
{
    NONE,
    SWAPPING,
    READY
}

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponParent;
    [SerializeField] GameObject blockGun;
    [SerializeField] WepManState state;

    public Transform playerCam;
    PlayerInput playerInput;
    public IWeapon currentWeapon;

    void Start()
    {
        state = WepManState.NONE;
        playerCam = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (state != WepManState.READY) return;

        if (playerInput.fire)
            currentWeapon.Fire();

        if (playerInput.altFire)
            currentWeapon.Throw();
    }

    [Button]
    void EquipWeapon()
    {
        currentWeapon = Instantiate(blockGun, weaponParent).GetComponent<IWeapon>();
        currentWeapon.owner = this;
        currentWeapon.TakeOutWeapon();

        state = WepManState.SWAPPING;
        // animate equipping animation
        state = WepManState.READY;
    }
}
