using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text finishedText;
    public TMP_Text penaltyText;
    static float timer;
    public static bool TimeTrialIsFinished = false;
    public GameObject finishedTmpUI;
    public float penaltyTime = 5.0f; // The time penalty for hitting a cone

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeTrialIsFinished)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void AddTimePenalty(float penalty)
    {
        timer += penalty;
        UpdateTimerDisplay();
        StartCoroutine(ShowPenaltyNotification(penalty));
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
        if(collider.gameObject.tag == "FinishLine" && !TimeTrialIsFinished)
        {
            finishedTmpUI.SetActive(true);
            TimeTrialIsFinished = true;
            finishedText.text = "Finished in: " + timerText.text;
            // You may want to stop the timer update when finished
        }
    }
}
