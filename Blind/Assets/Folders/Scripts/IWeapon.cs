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
    public GameObject gunGameObject { get; }
}

// This interface is retarded, I kept having to add things just bc it wasn't a monobehavior. Like "gunGameObject" and "owner"

// Never again, just use an Abstract class in the future.