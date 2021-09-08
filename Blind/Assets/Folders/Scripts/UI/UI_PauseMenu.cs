using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuParent;
    [SerializeField] Vector2 minMaxMouseSens;
    [SerializeField] Slider sensSlider;

    public bool paused = false;
    float previousTimescale;
    PlayerLook plrLook;

    void Start()
    {
        plrLook = FindObjectOfType<PlayerLook>();
    }

    void OnEnable()
    {
        PauseOrUnPause(false);
    }

    void OnPause()
    {
        paused = !paused;
        PauseOrUnPause(paused);
    }

    void PauseOrUnPause(bool paused)
    {
        pauseMenuParent.SetActive(paused);
        if (paused) // pause
            Pause();
        else
            UnPause();
    }

    void Pause()
    {
        sensSlider.value = plrLook.camRotSpeed.Remap(minMaxMouseSens.x, minMaxMouseSens.y, 0, 1);
        previousTimescale = Time.timeScale;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UnPause()
    {
        Time.timeScale = previousTimescale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnSensSliderMoved(float x)
    {
        float sens = x.Remap(0, 1, minMaxMouseSens.x, minMaxMouseSens.y);
        plrLook.camRotSpeed = sens;
        PlayerPrefs.SetFloat("mSens", sens);
    }
}
