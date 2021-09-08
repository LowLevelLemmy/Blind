using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyButtons;
using DG.Tweening;

public class PointsMan : MonoBehaviour
{
    public static PointsMan instance;
    [SerializeField] TextMeshProUGUI pointsTxt;
    Transform pointsParent => pointsTxt.transform.parent;
    public int playerPoints = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        pointsTxt.text = playerPoints.ToString();
    }

    public void AddPoints(int amount)
    {
        playerPoints += amount;
        pointsTxt.text = playerPoints.ToString();
        Animate();
    }

    [Button]
    void Animate()
    {
        DOTween.defaultTimeScaleIndependent = true; // I do this... is there's a better way to do this? I don't know
        pointsParent.transform.DOPunchScale(transform.localScale * -.15f, .3f, 10, .5f);
        DOTween.defaultTimeScaleIndependent = false;
    }
}
