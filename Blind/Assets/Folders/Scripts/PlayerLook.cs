using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public GameObject playerCam;
    public float camRotSpeed = 1f;

    PlayerInput playerInput;

    float camVerticalAngle;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        HandleLook();
    }

    void HandleLook()
    {
        Vector2 lookdelt = playerInput.lookDelta;

        // Horizontal Char Rotation:
        float rotDegrees = lookdelt.x * camRotSpeed;
        transform.Rotate(new Vector3(0f, rotDegrees, 0f), Space.Self);

        // Vertical Char Rotation:
        camVerticalAngle -= lookdelt.y * camRotSpeed;

        // limit the camera's vertical angle to min/max
        camVerticalAngle = Mathf.Clamp(camVerticalAngle, -89f, 89f);

        // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        playerCam.transform.localEulerAngles = new Vector3(camVerticalAngle, 0, 0);
    }
}
