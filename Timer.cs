using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text penaltyText;
    static float timer;
    public static bool TimeTrialIsFinished = false;
    public GameObject levelSummaryUI;
    public GameObject LeaderboardEntryUI;
    public float penaltyTime = 5.0f; // The time penalty for hitting a cone
    public TMP_Text finalTimeText; // Text element for final time
    public TMP_Text penaltiesCountText; // Text element for penalties count
    private int penaltyCount = 0; // Counter for penalties
    public LevelCheckpoints levelCheckpoints;
    public string levelKey; // Set this in the Unity Inspector for each level
    public TMP_InputField playerNameInput;

    private float completedTime;

    // Start is called before the first frame update
    public void Start()
    {
        ResetTimer();
        TimeTrialIsFinished = false; // Make sure the time trial is set to running
        UpdateTimerDisplay(); // Update the display to show the reset timer
    }

    public void ResetTimer()
    {
        timer = 0.0f; // Reset timer to 0 at the start of the game
    }
    // Update is called once per frame
    public void Update()
    {
        if (!TimeTrialIsFinished) {
            timer += Time.deltaTime; // This will only increment when TimeTrialIsFinished is false
            UpdateTimerDisplay(); // Keep updating the display
        }
    }

    public void AddTimePenalty(float penalty)
    {
        timer += penalty;
        UpdateTimerDisplay();
        StartCoroutine(ShowPenaltyNotification(penalty));
        penaltyCount++; // Increment the penalty count
    }

    public IEnumerator ShowPenaltyNotification(float penalty)
    {
        if (penaltyText != null)
        {
            penaltyText.text = $"+{penalty}s";
            penaltyText.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(2);
            
            penaltyText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("PenaltyText is not assigned in the inspector");
        }
    }


    // You may want to create a new method to update the timer display if you don't have one already.
    public void UpdateTimerDisplay()
    {
        // Calculate the seconds and milliseconds from the timer
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 1000);

        // Update the timer text to show seconds and milliseconds
        timerText.text = string.Format("{0:0}:{1:000}", seconds, milliseconds);
    }


    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "FinishLine" && !TimeTrialIsFinished)
        {
            Debug.Log("Level Completed, preparing submission.");
            TimeTrialIsFinished = true;

            // Capture the timer before resetting
            completedTime = timer;
            GetCurrentTime();
            
            timerText.enabled = false; // Optionally hide the timer
            penaltyText.gameObject.SetActive(false);

            timer = 0.0f;
            Time.timeScale = 0f; // Stop all gameplay, like a pause
            AudioListener.pause = true; // Stop all audio

            // Show the leaderboard entry UI
            LeaderboardEntryUI.SetActive(true);

        }
    }


    public void SubmitScore(string levelKey, float completedTime, string playerName)
    {
        FindObjectOfType<BestTimesTable>().AddTimeEntry(completedTime, playerName, levelKey);
    }

    
    public void HandleSubmission()
    {
        // Assume playerNameInput is a TMP_InputField where the player enters their name
        string playerName = playerNameInput.text;
        SubmitScore(levelKey, completedTime, playerName);

        // After submission, update and show the level summary UI
        finalTimeText.text = "Final Time: " + completedTime.ToString("F3") + "s";
        penaltiesCountText.text = "Penalties: " + penaltyCount * (int)penaltyTime + "s";
        levelSummaryUI.SetActive(true); // Show the level summary UI
        LeaderboardEntryUI.SetActive(false); // Hide the leaderboard entry UI
    }

    public float GetCurrentTime()
    {
        return timer;
    }

}