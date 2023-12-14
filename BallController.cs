using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    public float forceMagnitude = 1.0f; // Adjust this value as needed

    void Start()
    {
        // Get the Rigidbody component
        ballRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get the direction of the collision (collision normal)
        Vector3 impactDirection = collision.contacts[0].normal;

        // Check if the collision is with a surface facing away from the floor
        if (Vector3.Dot(impactDirection, Vector3.up) < 0)
        {
            // Apply force to the ball to simulate impact in the direction of collision normal
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(impactDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}





