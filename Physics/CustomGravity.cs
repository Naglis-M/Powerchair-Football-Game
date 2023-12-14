using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public float gravityStrength = 9.81f; // Strength of gravity, you can adjust this value

    void Update()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        // Find all objects within the gravity range
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10.0f);

        foreach (Collider collider in colliders)
        {
            // Check if the object has a script for gravity
            GravityAffectedObject gravityObject = collider.GetComponent<GravityAffectedObject>();

            if (gravityObject != null)
            {
                // Calculate gravity direction
                Vector3 gravityDirection = (transform.position - collider.transform.position).normalized;

                // Apply gravitational force
                gravityObject.ApplyGravity(gravityDirection, gravityStrength);
            }
        }
    }

    // Visualize the gravity range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10.0f);
    }
}

