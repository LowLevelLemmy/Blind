using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBar : MonoBehaviour, IWeapon
{
    [SerializeField] float takeOutTime = 0.57f;

    public WeaponManager owner { get; set; }
    public GameObject gunGameObject => gameObject;

    Animator anim;

    public void GetDependencies()
    {
        anim = GetComponent<Animator>();
    }

    public void Fire()
    {
        print("SWING!");
    }

    public void TakeOutWeapon(TweenCallback call)
    {
        GetDependencies();
        anim.Play("TakeOut");
        DOVirtual.DelayedCall(takeOutTime, () => func(call));
    }

    void func(TweenCallback cally)  // SUPER HORRIBLE FIX   // I made this bc the delayed call would happpen AFTER the weapon is thrown and destroyed. This "Func" prevents that
    {
        // idk how "func" even RUNS if I destoryed the gameobject it's attached to. But who knows?
        if (this != null && owner != null)
            cally();
    }   // This func fix causes DoTween to have safe mode errors. Weird

    public void Throw()
    {
        print("THROW");
    }
}
