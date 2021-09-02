using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class HitKeyToChangeShader : MonoBehaviour
{

    // Some code stolen from: https://www.youtube.com/watch?v=6Yg2EedqDhc

    [SerializeField] ForwardRendererData rendererData;
    [SerializeField] string featureName;

    [SerializeField] Material mat1;
    [SerializeField] Material mat2;

    public void OnSwapShader()
    {
        ToggleFeatureMat();
    }

    void ToggleFeatureMat()
    {
        if (TryGetFeature(out var feature))
        {

            var blitFeature = feature as Blit;
            blitFeature.blitPass.blitMaterial = blitFeature.blitPass.blitMaterial == mat1 ? mat2 : mat1;    // don't even look at how ugly this line is
        }
    }

    private bool TryGetFeature(out ScriptableRendererFeature feature)
    {
        feature = rendererData.rendererFeatures.Where((f) => f.name == featureName).FirstOrDefault();
        return feature != null;
    }
}
