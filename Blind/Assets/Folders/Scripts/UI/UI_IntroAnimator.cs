using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EasyButtons;

public class UI_IntroAnimator : MonoBehaviour
{
    [SerializeField] Transform roundsUI;
    [SerializeField] Transform destination;
    [SerializeField] float dur1;
    [SerializeField] float dur2;
    [SerializeField] float dur3;

    Vector3 intialPos;
    Vector3 intialScale;

    void Start()
    {
        intialPos = roundsUI.position;
        intialScale = roundsUI.localScale;
        AnimateIntro1();
    }

    [Button]
    void AnimateIntro1()
    {
        DOTween.defaultTimeScaleIndependent = true;
        roundsUI.DOMove(destination.position, dur1);
        roundsUI.DOScale(destination.localScale, dur1);
        DOVirtual.DelayedCall(dur1, AnimateIntro2);
        DOTween.defaultTimeScaleIndependent = false;
    }

    void AnimateIntro2()
    {
        DOTween.defaultTimeScaleIndependent = true;
        DOVirtual.DelayedCall(dur2, AnimateIntro3);
        DOTween.defaultTimeScaleIndependent = false;
    }

    void AnimateIntro3()
    {
        DOTween.defaultTimeScaleIndependent = true;
        roundsUI.DOMove(intialPos, dur3);
        roundsUI.DOScale(intialScale, dur3);
        DOTween.defaultTimeScaleIndependent = false;
    }
}
