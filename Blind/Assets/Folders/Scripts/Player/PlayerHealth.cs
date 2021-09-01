using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EasyButtons;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour, IHurtable
{
    [SerializeField] bool injured;
    [SerializeField] float vigColorSpeed = 5f;

    [SerializeField] Volume vol;
    Bloom bloom;
    Vignette vig;
    [SerializeField] float normalVigIntensity = 0.46f;
    [SerializeField] float deadVigIntensity = 1f;

    [SerializeField] Color normalVigCol;
    [SerializeField] Color injuredVigColor = Color.red;

    void Start()
    {
        vol.profile.TryGet(out bloom);
        vol.profile.TryGet(out vig);
        normalVigCol = vig.color.value;
        StartCoroutine(Animate());
    }

    void Update()
    {

    }

    IEnumerator Animate()   // horible code
    {
        while (true)
        {
            while(injured)
            {
                vig.color.value = Color.Lerp(vig.color.value, injuredVigColor, vigColorSpeed * Time.deltaTime);
                vig.intensity.value = Mathf.Lerp(vig.intensity.value, deadVigIntensity, vigColorSpeed * Time.deltaTime);
                yield return 0;
            }
        
            while(!injured)
            {
                vig.color.value = Color.Lerp(vig.color.value, normalVigCol, vigColorSpeed * Time.deltaTime);
                vig.intensity.value = Mathf.Lerp(vig.intensity.value, normalVigIntensity, vigColorSpeed * Time.deltaTime);
                yield return 0;
            }
        }
    }

    public void OnHurt()
    {
        if (injured)
        {
            Die();
        }
        else
        {
            injured = true;
            DOVirtual.DelayedCall(5, Recover);
        }
    }

    void Recover()
    {
        if (injured)
            injured = false;
    }

    void Die()
    {
        print("I DEAD");
    }

}
