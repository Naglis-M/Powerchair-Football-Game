using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ChairController : MonoBehaviour
{
    public float acceleration = 5.0f;
    public float rotationTorque = 100.0f; // Torque for rotating the chair
    public float pushForceMultiplier = 2.0f;
    public Vector3 startPosition;
    public Quaternion startRotation;

    private Rigidbody chairRigidbody;

    void Start()
    {
        chairRigidbody = GetComponent<Rigidbody>();
        ResetToStartPosition();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ResetPosition"))
        {
            ResetToStartPosition();
        }
    }

    void FixedUpdate()
    {
        float moveVertical = -Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Apply immediate impulse force for movement
        Vector3 forceDirection = transform.right * moveVertical;
        chairRigidbody.AddForce(forceDirection * acceleration, ForceMode.Impulse);

        // Continue applying torque for rotation
        chairRigidbody.AddTorque(0, moveHorizontal * rotationTorque * Time.fixedDeltaTime, 0, ForceMode.VelocityChange);
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

    public void ResetToStartPosition()
    {
        chairRigidbody.velocity = Vector3.zero;
        chairRigidbody.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}