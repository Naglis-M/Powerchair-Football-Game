using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int totalScore = 0;
    public int totalBalls;
    public GameObject summaryUI;
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;

    public void StartLevel(int ballCount)
    {
        totalBalls = ballCount; // Set this from the inspector for each level
        totalScore = 0;
        UpdatePointsDisplay();
        
    }

    public void AddPoints(int points)
    {
        totalScore += points;
        totalBalls--; // Decrement the count of balls as they score
        CheckCompletion();
        UpdatePointsDisplay();
    }
    
    public void UpdatePointsDisplay()
    {
        scoreText.text = "Score: " + totalScore.ToString();
    }

    public void BallOut()
    {
        totalBalls--; // Decrement the count when a ball is destroyed or goes out
        CheckCompletion();
    }

    public void CheckCompletion()
    {
        if (totalBalls <= 0) // Check if all balls are processed
        {
            Time.timeScale = 0f;
            scoreText.enabled = false;
            AudioListener.pause = true;
            summaryUI.SetActive(true);
            finalScoreText.text = "Total Score: " + totalScore.ToString();
        }
    }

}