using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 100.0f;

    void Update()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);
    }
}
