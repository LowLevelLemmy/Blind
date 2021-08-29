using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] LayerMask layerMsk;
    [SerializeField] float interactCoolDown = 0.2f;

    public UnityAction<IInteractable> OnInteractTxtShouldComeUpNow;
    public UnityAction OnLostSightOfInteractable;
    public UnityAction OnInteractableLookedAt;
    public PlayerController plrCon;

    float lastTimeInteracted = -999;

    Transform playerCam => plrCon.plrCamera;

    bool lookingAtInteractable;
    bool wasLookingAtInteractable;

    void Awake()
    {
        plrCon = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Time.time - lastTimeInteracted < interactCoolDown)
            return;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMsk, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (!InRange(interactable))
                    return;

                lookingAtInteractable = true;

                if (plrCon.input_Use)
                    interactable.OnInteractedWith(this);

                interactable.OnLookedAt(this);
                OnInteractableLookedAt?.Invoke();
                OnInteractTxtShouldComeUpNow?.Invoke(interactable);
            }
            else
                lookingAtInteractable = false;
        }
        else
            lookingAtInteractable = false;

        if (wasLookingAtInteractable && !lookingAtInteractable)
            OnLostSightOfInteractable?.Invoke();
    }

    void LateUpdate()
    {
        wasLookingAtInteractable = lookingAtInteractable;
    }

    bool InRange(IInteractable interactable)
    {
        float dist = Vector3.Distance(transform.position, interactable.pos);
        return dist <= interactable.maxDist;    // distance check
    }
}
