using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Death : MonoBehaviour
{
    [SerializeField] Transform uiParent;

    bool restartable = false;

    void Start()
    {
        uiParent.gameObject.SetActive(false);
        FindObjectOfType<PlayerHealth>().OnPlayerDied.AddListener(OnDeath);
    }

    void OnRestart()
    {
        if (restartable)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void OnDeath()
    {
        uiParent.gameObject.SetActive(true);
        Image img = uiParent.GetComponent<Image>();

        var newCol = img.color; // make color's Alpha 0
        newCol.a = .2f;
        img.color = newCol;

        img.DOFade(.7f, 4f);
        restartable = true;
    }
}
