using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] LayerMask layerMsk;
    [SerializeField] float interactCoolDown = 0.2f;

    public UnityAction<IInteractable> OnInteractTxtShouldComeUpNow;
    public UnityAction<IInteractable> OnLostSightOfInteractable;    // passed in the interactable we lost sight of
    public UnityAction<IInteractable> OnInteractableLookedAt;
    public PlayerController plrCon;

    float lastTimeInteracted = -999;

    Transform playerCam => plrCon.plrCamera;

    bool lookingAtInteractable;
    bool wasLookingAtInteractable;

    IInteractable curInteractableLookingAt = null;

    void Awake()
    {
        plrCon = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (plrCon.CmpState(PlayerStates.DEAD))
            return;

        if (Time.time - lastTimeInteracted < interactCoolDown)
            return;

        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMsk, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (!InRange(interactable)) // if not in range
                    return;

                if (curInteractableLookingAt != interactable)
                    OnLostSightOfInteractable?.Invoke(curInteractableLookingAt);
                bool input = interactable.altUse ? plrCon.input_AltUse : plrCon.input_Use;
                if (input)   // INTERACTED WITH!
                    interactable.OnInteractedWith(this);

                interactable.OnLookedAt(this);
                OnInteractableLookedAt?.Invoke(interactable);

                if (interactable.altUse)
                    OnInteractTxtShouldComeUpNow?.Invoke(interactable);

                curInteractableLookingAt = interactable;
            }
            else
            {
                if (curInteractableLookingAt != null)
                {
                    OnLostSightOfInteractable?.Invoke(curInteractableLookingAt);
                    curInteractableLookingAt = null;
                }
            }
        }
        else
        {
            if (curInteractableLookingAt != null)
            {
                OnLostSightOfInteractable?.Invoke(curInteractableLookingAt);
                curInteractableLookingAt = null;
            }
        }
    }

    void LateUpdate()
    {
        wasLookingAtInteractable = lookingAtInteractable;
    }

    bool InRange(IInteractable interactable)
    {
        float dist = Vector3.Distance(transform.position, interactable.self.transform.position);
        return dist <= interactable.maxDist;    // distance check
    }
}
