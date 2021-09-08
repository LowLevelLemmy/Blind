using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using EasyButtons;

public class UI_RoundCounter : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] TextMeshProUGUI rndTxt;
    Director director;

    void OnEnable()
    {
        director = GameObject.FindObjectOfType<Director>();
        director.OnNewRound.AddListener(OnNewRound);
    }

    void OnNewRound()
    {
        rndTxt.text = director.currentRound.ToString();
        Animate();
    }

    [Button]
    void Animate()
    {
        DOTween.defaultTimeScaleIndependent = true; // I do this... is there's a better way to do this? I don't know
        parent.transform.DOPunchScale(transform.localScale * 25, .3f, 10, .5f);
        DOTween.defaultTimeScaleIndependent = false;
    }
}
