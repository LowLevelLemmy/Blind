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
    bool dead = false;

    UI_PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();   // it's stupid... but it's working
        plrMov = FindObjectOfType<PlayerMovement>();
        FindObjectOfType<PlayerHealth>().OnPlayerDied.AddListener(OnDeath);
    }

    void OnDeath()
    {
        dead = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (pauseMenu.paused || dead)
            return;

        targetTimeScale = plrMov.moveFactor;
        targetTimeScale = targetTimeScale.Remap(0, 1, minTime, maxTime);
        timescal = Mathf.Lerp(Time.timeScale, targetTimeScale, timeDampen * Time.deltaTime);
        Time.timeScale = timescal;
    }
}
