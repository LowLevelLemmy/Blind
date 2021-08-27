using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] LayerMask layerMsk;

    public UnityAction<IInteractable> OnInteractTxtShouldComeUpNow;
    public UnityAction OnLostSightOfInteraction;

    public PlayerController plrCon;

    void Awake()
    {
        plrCon = GetComponent<PlayerController>();
    }

    void Update()
    {
        Collider[] hitCols = Physics.OverlapSphere(plrCon.plrCamera.position + plrCon.plrCamera.forward * 0.5f, 0.75f, layerMsk, QueryTriggerInteraction.Collide);
        if (hitCols.IsNullOrEmpty())
        {
            OnLostSightOfInteraction?.Invoke(); // lets UI know to hide interact UI
            return;
        }

        foreach(Collider col in hitCols)
        {
            if (col.TryGetComponent<IInteractable>(out var interactable))
            {
                if (plrCon.playerInput.use)    // replace with playerInput
                {
                    interactable.OnInteractedWith(this);
                }

                interactable.OnLookedAt(this);
                OnInteractTxtShouldComeUpNow?.Invoke(interactable);
            }
        }

    }
}
