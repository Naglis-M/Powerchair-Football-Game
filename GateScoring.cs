using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GateScoring : MonoBehaviour
{
    public int points = 1; // Points to add when the ball passes through the gate
    public ScoreManager scoreManager; // Reference to the score manager in your game

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Check if the object that entered the trigger is tagged as "Ball"
        {
            scoreManager.AddPoints(points); // Add points to the score
            Destroy(other.gameObject); // Optional: Destroy the ball if you don't need it anymore
        }
    }
}