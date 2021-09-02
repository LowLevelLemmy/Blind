using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using DG.Tweening;

public class ZombieShaderAnimatior : MonoBehaviour
{
    [SerializeField] Renderer render;
    [SerializeField] float fadeDur = 3;

    [Button]
    public void AnimateFadeOut()
    {
        foreach (var mat in render.materials)
        {
            mat.color = Color.black;
            //mat.DOColor(Color.black, fadeDur).SetEase(Ease.Linear);
            //mat.SetColor("_EmissionColor", Color.black);
            StartCoroutine(FadeOutEmission(mat));
        }
    }

    IEnumerator FadeOutEmission(Material mat)
    {
        float t = 0.0f;
        Color startEmission = mat.GetColor("_EmissionColor");
        while (t < fadeDur)
        {
            t += Time.deltaTime;
            Color newColor = Color.Lerp(startEmission, Color.black, t / fadeDur);
            mat.SetColor("_EmissionColor", newColor);
            yield return null;
        }
        Destroy(gameObject);
    }
}
