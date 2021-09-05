using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_RoundCounter : MonoBehaviour
{
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
    }
}
