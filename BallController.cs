using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    public float forceMagnitude = 1.0f; // Adjust this value as needed
    public ScoreManager scoreManager;
    public bool canCollide = true; // A flag to check if the ball can collide with a boundary

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Check for boundary collision
        if (collision.gameObject.CompareTag("Boundary") && canCollide)
        {
            scoreManager.BallOut();
            Debug.Log("Hit Boundary");
            Destroy(gameObject); // Destroy the ball if it hits a boundary
            StartCoroutine(CoolDown()); // Start cooldown period
        }
        else
        {
            // Existing collision logic for other objects
            Vector3 impactDirection = collision.contacts[0].normal;
            if (Vector3.Dot(impactDirection, Vector3.up) < 0)
            {
                ballRigidbody.AddForce(impactDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    public IEnumerator CoolDown()
    {
        canCollide = false; // Disable collision
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        canCollide = true; // Re-enable collision
    }
}





