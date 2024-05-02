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
    public Vector3 startPosition; // Default start position
    public Quaternion startRotation; // Default start rotation

    private Rigidbody chairRigidbody;

    void Start()
    {
        chairRigidbody = GetComponent<Rigidbody>();
        // Initialize with default start position and rotation if not set
        if (startPosition == Vector3.zero)
            startPosition = transform.position;
        if (startRotation == Quaternion.identity)
            startRotation = transform.rotation;
    }

    void Update() 
    {
        // Check if the "R" key or a specific controller button is pressed
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ResetPosition"))
        {
            ResetPosition();
        }
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

    // Method to reset the chair's position
    public void ResetPosition()
    {
        chairRigidbody.velocity = Vector3.zero; // Stop all movement
        chairRigidbody.angularVelocity = Vector3.zero; // Stop all rotation
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}