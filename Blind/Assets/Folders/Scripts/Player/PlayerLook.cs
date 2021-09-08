using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public PlayerController plrCon;
    public float camRotSpeed = 1f;
    float camVerticalAngle;

    void Start()
    {
        camRotSpeed = PlayerPrefs.GetFloat("mSens", 1.2f);
        if (camRotSpeed == 0)   // I'm being throrough cuz I don't have another PC to test on.
            camRotSpeed = 1.2f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        plrCon = GetComponent<PlayerController>();
    }

    void Update()
    {
        HandleLook();
    }

    void HandleLook()
    {
        Vector2 lookdelt = plrCon.input_LookDelta;

        // Horizontal Char Rotation:
        float rotDegrees = lookdelt.x * camRotSpeed;
        transform.Rotate(new Vector3(0f, rotDegrees, 0f), Space.Self);

        // Vertical Char Rotation:
        camVerticalAngle -= lookdelt.y * camRotSpeed;

        // limit the camera's vertical angle to min/max
        camVerticalAngle = Mathf.Clamp(camVerticalAngle, -89f, 89f);

        // apply the vertical angle as a local rotation to the HEAD transform along its right axis (makes it pivot up and down)
        plrCon.head.localEulerAngles = new Vector3(camVerticalAngle, 0, 0);
    }
}
