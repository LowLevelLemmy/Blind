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
    [SerializeField] Transform weaponParent;
    [SerializeField] GameObject blockGun;
    [SerializeField] WepManState state;
    public IWeapon currentWeapon;

    public PlayerController plrCon;
    PlayerInput playerInput => plrCon.playerInput;

    void Start()
    {
        plrCon = GetComponent<PlayerController>();
        state = WepManState.NONE;
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
    public void EquipWeapon(GameObject weapon)
    {
        currentWeapon = Instantiate(weapon, weaponParent).GetComponent<IWeapon>();
        currentWeapon.owner = this;

        state = WepManState.SWAPPING;
        currentWeapon.TakeOutWeapon(OnWeaponReady);
    }

    void OnWeaponReady()
    {
        state = WepManState.READY;
    }
}
