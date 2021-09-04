using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EasyButtons;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

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

    PlayerController plrCon;

    public UnityEvent OnPlayerDied;
    
    void Start()
    {
        plrCon = GetComponent<PlayerController>();
        vol.profile.TryGet(out bloom);
        vol.profile.TryGet(out vig);
        normalVigCol = vig.color.value;
        StartCoroutine(Animate());
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

    public void OnHurt(GameObject inflicter = null)
    {
        print(inflicter.name);
        if (injured)
        {
            Die(inflicter);
        }
        else
        {
            injured = true;
            DOVirtual.DelayedCall(5, Recover);
        }
    }

    void Recover()
    {
        if (plrCon.CmpState(PlayerStates.DEAD))
            return;

        if (injured)
            injured = false;
    }

    [Button]
    void Die(GameObject inflicter = null)
    {
        print("I DEAD");

        plrCon.SwitchState(PlayerStates.DEAD);
        plrCon.weaponManager.ThrowCurWeapon();
        plrCon.cc.enabled = false;

        Transform head = plrCon.head;
        head.parent = null;
        Rigidbody headRb = head.GetComponent<Rigidbody>();
        Collider headCol = head.GetComponent<Collider>();

        headCol.isTrigger = false;
        headRb.isKinematic = false;

        if (inflicter)
        {
            Vector3 dir = (inflicter.transform.position - transform.position).normalized;
            dir.y = 0f;
            headRb.AddForce(dir * 100);
        }

        OnPlayerDied?.Invoke();

        Destroy(gameObject);

        // Add impulse from zombie hit direction
        // Pass in the zombie that hurt the player in the Hurt method

        // TODO: WHEN YOU COME BACK... MAKE IT ALL WORK.
            // Zombies are glitching when they come outta windows
            // Attacking dosen't seem to have a timer delay.
    }
}
