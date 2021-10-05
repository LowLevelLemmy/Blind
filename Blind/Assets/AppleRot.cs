using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRot : MonoBehaviour
{
    public float speed;
    void Start()
    {

    }

    void Update()
    {
        Vector3 eAngles = transform.rotation.eulerAngles;
        eAngles.y += Time.deltaTime * speed;
        transform.eulerAngles = eAngles;
    }
}
