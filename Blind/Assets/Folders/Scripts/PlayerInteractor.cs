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

    Transform playerCam => plrCon.plrCamera;

    void Awake()
    {
        plrCon = GetComponent<PlayerController>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 bulletDirection = playerCam.forward;
        if (Physics.Raycast(playerCam.position, bulletDirection, out hit, 1000, layerMsk, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                float dist = Vector3.Distance(transform.position, interactable.pos);
                if (dist > interactable.maxDist) return;    // distance check

                if (plrCon.playerInput.use)
                    interactable.OnInteractedWith(this);

                interactable.OnLookedAt(this);
                OnInteractTxtShouldComeUpNow?.Invoke(interactable);
            }
        }
    }
}
