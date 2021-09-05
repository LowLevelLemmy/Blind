using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsMan : MonoBehaviour
{
    public static PointsMan instance;
    [SerializeField] TextMeshProUGUI pointsTxt;
    public int playerPoints = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoints(int amount)
    {
        playerPoints += amount;
        pointsTxt.text = playerPoints.ToString();
    }
}
