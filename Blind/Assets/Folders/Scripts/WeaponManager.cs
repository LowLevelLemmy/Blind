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
    //[SerializeField] GameObject blockGun;
    public WepManState state;

    [Header("References")]
    public PlayerController plrCon;
    public AbstractWeapon currentWeapon;

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
        currentWeapon = Instantiate(weapon, weaponParent).GetComponent<AbstractWeapon>();
        currentWeapon.owner = this;
        currentWeapon.TakeOutWeapon(OnWeaponReady);
    }

    [Button]
    public void UnEquipWeapon() // no animation
    {
        if (currentWeapon == null) return;
        state = WepManState.NONE;
        Destroy(currentWeapon.owner = null);
        Destroy(currentWeapon.gameObject);
    }

    void OnWeaponReady()
    {
        if (state == WepManState.SWAPPING)
            state = WepManState.READY;
    }
}
