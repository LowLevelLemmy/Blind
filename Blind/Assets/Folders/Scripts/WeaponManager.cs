using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    //[SerializeField] GameObject blockGun;
    public WepManState state;

    [Header("References")]
    public PlayerController plrCon;
    public AbstractWeapon currentWeapon;

    public UnityEvent<AbstractWeapon> OnWeaponEquiped;
    public UnityEvent<AbstractWeapon> OnWeaponUnEquiped;

    void Start()
    {
        plrCon = GetComponent<PlayerController>();
        state = WepManState.NONE;
    }

    void Update()
    {
        if (plrCon.CmpState(PlayerStates.DEAD))
            return;

        if (plrCon.input_Fire && state == WepManState.READY)
            currentWeapon.Fire();

        if (plrCon.input_AltFire && state != WepManState.NONE)
            ThrowCurWeapon();
    }

    public void ThrowCurWeapon()
    {
        if (currentWeapon == null) return;
        currentWeapon.Throw();
        UnEquipWeapon();
    }

    public void EquipWeapon(PickupableWeapon pickupableWep)  // no animation
    {
        if (state != WepManState.NONE)  // we're already holding a weapon
            return;

        state = WepManState.SWAPPING;
        currentWeapon = Instantiate(pickupableWep.pickupableWeapon, weaponParent).GetComponent<AbstractWeapon>();

        if (pickupableWep.ammo != -1)
            currentWeapon.ammo = pickupableWep.ammo;


        currentWeapon.owner = this;
        currentWeapon.TakeOutWeapon(OnWeaponReady); // animation
        OnWeaponEquiped?.Invoke(currentWeapon);
    }

    public void UnEquipWeapon() // no animation
    {
        if (currentWeapon == null) return;
        state = WepManState.NONE;
        OnWeaponUnEquiped?.Invoke(currentWeapon);
        Destroy(currentWeapon.owner = null);
        Destroy(currentWeapon.gameObject);
    }

    void OnWeaponReady()
    {
        if (state == WepManState.SWAPPING)
            state = WepManState.READY;
    }
}
