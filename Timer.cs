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
    public float penaltyTime = 5.0f; // The time penalty for hitting a cone
    public TMP_Text finalTimeText; // Text element for final time
    public TMP_Text penaltiesCountText; // Text element for penalties count
    private int penaltyCount = 0; // Counter for penalties
    public LevelCheckpoints levelCheckpoints;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f; // Reset timer to 0 at the start of the game
        TimeTrialIsFinished = false; // Make sure the time trial is set to running
        UpdateTimerDisplay(); // Update the display to show the reset timer
    }

    // Update is called once per frame
    void Update()
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

    private IEnumerator ShowPenaltyNotification(float penalty)
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
    void UpdateTimerDisplay()
    {
        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer % 60);
        string time = string.Format("{0:0}:{1:00}", mins, secs);
        timerText.text = time;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "FinishLine" && !TimeTrialIsFinished)
        {
            Debug.Log("Attempting to show level summary");
            TimeTrialIsFinished = true;
            
            timerText.enabled = false; // Optionally hide the timer
            penaltyText.gameObject.SetActive(false);

            int totalPenaltySeconds = penaltyCount * (int)penaltyTime;
            // Stop the timer from counting
            timer = 0.0f;
            Time.timeScale = 0f; // Optional: Stop all gameplay, like a pause
            AudioListener.pause = true; // Optional: Stop all audio

            // Display the summary information
            finalTimeText.text = "Final Time: " + timerText.text;
            penaltiesCountText.text = "Penalties: " + totalPenaltySeconds.ToString() + "s";

            levelSummaryUI.SetActive(true); // Show the level summary UI
            
            // You can add more code here to update the summary UI with specific details


        }
    }
}
