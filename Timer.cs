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
    static float timer;
    public static bool TimeTrialIsFinished = false;
    public GameObject finishedTmpUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer - mins * 60);

        string time = string.Format("{0:0}:{1:00}", mins, secs);

        timerText.text = time;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "FinishLine")
        {
            finishedTmpUI.SetActive(true);
            TimeTrialIsFinished = true;
            finishedText.text = "Finished in: " +timerText.text;
            timer = 0.0f;
        }
    }
}
