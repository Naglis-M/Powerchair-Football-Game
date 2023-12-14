using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float pushForceMultiplier = 2.0f;
    public float smoothingFactor = 0.5f; // Adjust this value
    private Vector3 lastPosition;
    private Vector3 smoothedImpactDirection;

    void Start()
    {
        // Initialize lastPosition at the start
        lastPosition = transform.position;
        smoothedImpactDirection = Vector3.zero;
    }
    void Update()
    {
        float moveVertical = -Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Rotate the chair
        transform.Rotate(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0);

        // Move the chair
        transform.Translate(moveVertical * speed * Time.deltaTime, 0, 0);

        // Update lastPosition
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if colliding with an object tagged as "Ball"
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Calculate the impact direction based on the difference in positions
            Vector3 currentImpactDirection = (transform.position - lastPosition).normalized;

            // Smooth the impact direction over time
            smoothedImpactDirection = Vector3.Lerp(smoothedImpactDirection, currentImpactDirection, smoothingFactor);

            // Get the relative speed of the collision
            float relativeSpeed = collision.relativeVelocity.magnitude;

            // Apply force to the ball to simulate impact based on relative speed and smoothed impact direction
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(smoothedImpactDirection * relativeSpeed * pushForceMultiplier, ForceMode.Impulse);
            }
        }
    }
}

