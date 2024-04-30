using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Assign this in the inspector
    private int score = 0;

    void Start()
    {
        UpdatePointseDisplay();
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdatePointseDisplay();
    }

    private void UpdatePointseDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
