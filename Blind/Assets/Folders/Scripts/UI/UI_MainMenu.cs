using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using EasyButtons;
using UnityEngine.Rendering.Universal;
using System.Linq;


public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] ForwardRendererData rendererData;
    [SerializeField] string featureName;

    Blit blit;
    Material blitMat;

    void Start()
    {
        blit = TryGetFeature() as Blit;
        blitMat = blit.blitPass.blitMaterial;
        AnimateIntro();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LemmyClicked()
    {
        Application.OpenURL("https://lll.rip/");
    }

    [Button]
    void AnimateIntro()
    {
        //blitMat.SetFloat("Res", 10);
        //blitMat.DOFloat(100, "Res", 5f);

        blitMat.SetFloat("Remaping", 0);
        blitMat.DOFloat(.7f, "Remaping", 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        transform.DOShakePosition(5, .2f, 0).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

    }

    private ScriptableRendererFeature TryGetFeature()
    {
        var feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();
        return feature;
    }
}
