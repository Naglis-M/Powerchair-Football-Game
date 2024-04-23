using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ChairController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f; // Adjusted for more granular control
    public float pushForceMultiplier = 2.0f;

    private Rigidbody chairRigidbody;

    void Start()
    {
        chairRigidbody = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate() // Use FixedUpdate for physics calculations
    {
        float moveVertical = -Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Apply rotation
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, moveHorizontal * rotationSpeed * Time.fixedDeltaTime, 0));
        chairRigidbody.MoveRotation(chairRigidbody.rotation * deltaRotation);

        // Apply movement
        Vector3 moveDirection = transform.right * moveVertical * speed;
        chairRigidbody.velocity = moveDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 force = transform.forward * chairRigidbody.velocity.magnitude * pushForceMultiplier;
                ballRigidbody.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
