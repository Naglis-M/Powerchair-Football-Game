using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAffectedObject : MonoBehaviour
{
    public float Mass = 1.0f; // Adjust this value based on the object's mass
    public float elasticity = 0.5f; // Adjust this value for bounciness

    private Vector3 velocity;

    void Update()
    {
        // Update position based on velocity
        transform.position += velocity * Time.deltaTime;
    }

    public void ApplyGravity(Vector3 gravityForce, float deltaTime)
    {
        // Apply gravitational force manually
        velocity += gravityForce / Mass * deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if colliding with another gravity-affected object
        GravityAffectedObject otherObject = collision.gameObject.GetComponent<GravityAffectedObject>();

        if (otherObject != null)
        {
            // Calculate collision response
            Vector3 normal = collision.contacts[0].normal;
            Vector3 relativeVelocity = velocity - otherObject.velocity;
            float impulse = Vector3.Dot(relativeVelocity, normal) * (1 + elasticity) / (1 / Mass + 1 / otherObject.Mass);

            // Apply collision response
            velocity -= impulse / Mass * normal;
            otherObject.velocity += impulse / otherObject.Mass * normal;
        }
    }
}






