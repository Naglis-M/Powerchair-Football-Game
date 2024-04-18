using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ConeCollision : MonoBehaviour
{
    public float timePenalty = 5f; // 5 seconds penalty
    private Timer timerScript;
    private bool hasBeenHit = false; // Flag to check if the cone has already been hit

    void Start()
    {
        // Find the Timer script in the scene (assuming there is only one)
        timerScript = FindObjectOfType<Timer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player hit the cone and the cone hasn't been hit before
        if (collision.collider.CompareTag("Chair") && !hasBeenHit)
        {
            hasBeenHit = true; // Set the flag to true since the cone is now hit
            timerScript.AddTimePenalty(timePenalty);
            // Optionally, disable the cone or change its appearance to indicate it's been hit.
        }
    }
}
