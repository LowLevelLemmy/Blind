using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMan : MonoBehaviour
{
    [SerializeField] float minTime = 0.1f;
    [SerializeField] float maxTime = 1.9f;
    [SerializeField] float timeDampen = 1f;
    PlayerMovement plrMov;
    float targetTimeScale;
    float timescal;

    void Start()
    {
        plrMov = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        targetTimeScale = plrMov.moveFactor;
        targetTimeScale = targetTimeScale.Remap(0, 1, minTime, maxTime);
        timescal = Mathf.Lerp(Time.timeScale, targetTimeScale, timeDampen * Time.deltaTime);
        Time.timeScale = timescal;
    }
}
