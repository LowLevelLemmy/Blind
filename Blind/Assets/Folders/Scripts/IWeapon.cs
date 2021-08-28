using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IWeapon
{
    public void Fire();
    public void Throw();
    public void TakeOutWeapon(TweenCallback call);  // call is what's called when weapon is taken OUT!
    public WeaponManager owner { get; set; }
}
