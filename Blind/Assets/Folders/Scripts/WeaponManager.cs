using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    [Header("Settings")]
    [SerializeField] float interactCoolDown;
    [SerializeField] Transform weaponParent;
    [SerializeField] GameObject blockGun;
    public WepManState state;

    [Header("References")]
    public PlayerController plrCon;
    public IWeapon currentWeapon;

    PlayerInput playerInput => plrCon.playerInput;

    void Start()
    {
        plrCon = GetComponent<PlayerController>();
        state = WepManState.NONE;
    }

    void Update()
    {
        if (playerInput.fire && state == WepManState.READY)
        {
            currentWeapon.Fire();
        }

        if (playerInput.altFire && state != WepManState.NONE)
            ThrowCurWeapon();
    }

    void ThrowCurWeapon()
    {
        if (currentWeapon == null) return;
        currentWeapon.Throw();
        UnEquipWeapon();
    }

    [Button]
    public void EquipWeapon(GameObject weapon)  // no animation
    {
        state = WepManState.SWAPPING;
        currentWeapon = Instantiate(weapon, weaponParent).GetComponent<IWeapon>();
        currentWeapon.owner = this;
        currentWeapon.TakeOutWeapon(OnWeaponReady);
    }

    [Button]
    public void UnEquipWeapon() // no animation
    {
        if (currentWeapon == null) return;
        state = WepManState.NONE;
        Destroy(currentWeapon.owner = null);
        Destroy(currentWeapon.gunGameObject);
    }

    void OnWeaponReady()
    {
        // How come this is called after I destroy the object when throwing??!?
        if (state == WepManState.SWAPPING)  // this is not a good fix, bc WepState can be "Swapping" with a different weapon.
            state = WepManState.READY;
    }
}
