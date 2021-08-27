using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Fire();
    public void Throw();
    public void TakeOutWeapon();
    public WeaponManager owner { get; set; }
}
