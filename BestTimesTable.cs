using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random=UnityEngine.Random;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BestTimesTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> timeEntryTransformList;

    public string levelKey; // Set this dynamically

    private void Awake() {
        entryContainer = transform.Find("TimesEntryContainer");
        entryTemplate = entryContainer.Find("TimesEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);
        timeEntryTransformList = new List<Transform>(); // Ensure this list is initialized

        LoadLeaderboard();
    }

    public void LoadLeaderboard() {
        string jsonString = PlayerPrefs.GetString(levelKey, "{}"); // Load specific level's leaderboard
        BestTimes bestTimes = JsonUtility.FromJson<BestTimes>(jsonString);

        bestTimes.timeEntryList = bestTimes.timeEntryList.OrderBy(entry => entry.time).ToList();

        // Clear previous entries
        foreach (Transform child in timeEntryTransformList) {
            Destroy(child.gameObject);
        }
        timeEntryTransformList.Clear();

        // Populate leaderboard UI
        for (int i = 0; i < bestTimes.timeEntryList.Count && i < 10; i++) {
            CreateTimeEntryTransform(bestTimes.timeEntryList[i], entryContainer, timeEntryTransformList);
        }
        
    }
    
    private void CreateTimeEntryTransform(TimeEntry timeEntry, Transform container, List<Transform> transformList) {
        
        float templateHeight = 80f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRecTransform = entryTransform.GetComponent<RectTransform>();
        entryRecTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;
                
        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;                
        }

        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;

        float time = timeEntry.time; 
        entryTransform.Find("timeText").GetComponent<TMP_Text>().text = timeEntry.time.ToString("F3"); 

        string name = timeEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;

        // Set background visible odds and evens, easier to see
        entryTransform.Find("bg").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1) {
            // Highlight First Place
            entryTransform.Find("nameText").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("posText").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("timeText").GetComponent<TMP_Text>().color = Color.green;
        }
        
        // Set Trophy
        string colorHex;
        Color colorValue;

        switch (rank) {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                colorHex = "#FFD200"; // Gold color
                if (ColorUtility.TryParseHtmlString(colorHex, out colorValue)) {
                    entryTransform.Find("trophy").GetComponent<Image>().color = colorValue;
                }
                break;
            case 2:
                colorHex = "#D4D4D4"; // Silver color
                if (ColorUtility.TryParseHtmlString(colorHex, out colorValue)) {
                    entryTransform.Find("trophy").GetComponent<Image>().color = colorValue;
                }
                break;
            case 3:
                colorHex = "#9F7236"; // Bronze color
                if (ColorUtility.TryParseHtmlString(colorHex, out colorValue)) {
                    entryTransform.Find("trophy").GetComponent<Image>().color = colorValue;
                }
                break;
        }
                transformList.Add(entryTransform);
    }


    //Delete times button manually
    public void ClearAllTimes() {
        // Delete the leaderboard key from PlayerPrefs if it exists
        if (PlayerPrefs.HasKey(levelKey)) {
            PlayerPrefs.DeleteKey(levelKey);
            PlayerPrefs.Save();  // Make sure to save the changes to PlayerPrefs
        }

        // Update the UI to show that the leaderboard is now empty
        LoadLeaderboard();
    }


    public void AddTimeEntry(float time, string name, string key) {
        // Fetch, update, and save the leaderboard for the given level key
        TimeEntry newEntry = new TimeEntry { time = time, name = name };
        string jsonString = PlayerPrefs.GetString(key, "{}");
        BestTimes bestTimes = JsonUtility.FromJson<BestTimes>(jsonString);
        bestTimes.timeEntryList.Add(newEntry);
        string json = JsonUtility.ToJson(bestTimes);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class BestTimes {
        public List<TimeEntry> timeEntryList = new List<TimeEntry>();
    }

    [System.Serializable]
    public class TimeEntry {
        public float time;
        public string name;
    }
}