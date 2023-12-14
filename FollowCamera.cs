using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    public Transform target;
    public float xOffset = 0f;
    public float zOffset = 0f;

    public float rotationSpeed = 100.0f;

    // LateUpdate is called once per frame after Update method
    void LateUpdate()
    {
        float xPosition = target.position.x + xOffset;
        float zPosition = target.position.z + zOffset;

        transform.position = new Vector3(xPosition, transform.position.y, zPosition);
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);
    }
}
