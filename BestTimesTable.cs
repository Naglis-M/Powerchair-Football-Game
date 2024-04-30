using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class BestTimesTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> timeEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("TimesEntryContainer");
        entryTemplate = entryContainer.Find("TimesEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        
        string jsonString = PlayerPrefs.GetString("bestTimesTable", "{}"); // Provide a default empty JSON if nothing is stored yet
        BestTimes bestTimes = JsonUtility.FromJson<BestTimes>(jsonString);

        // Manually adding data
        //AddTimeEntry(02.99f, "WIN");

        //sort entry list by time
        for (int i = 0; i < bestTimes.timeEntryList.Count; i++) {
            for (int j = i + 1; j < bestTimes.timeEntryList.Count; j++) {
                if (bestTimes.timeEntryList[j].time < bestTimes.timeEntryList[i].time) {
                    //swap
                    TimeEntry tmp = bestTimes.timeEntryList[i];
                    bestTimes.timeEntryList[i] = bestTimes.timeEntryList[j];
                    bestTimes.timeEntryList[j] = tmp;
                }
                
            }
            
        }
        timeEntryTransformList = new List<Transform>();

        foreach (TimeEntry timeEntry in bestTimes.timeEntryList) {
            CreateTimeEntryTransform(timeEntry, entryContainer, timeEntryTransformList);
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
        entryTransform.Find("timeText").GetComponent<TMP_Text>().text = time.ToString(); 

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

    private void AddTimeEntry(float time, string name) {
        // Create TimeEntry
        TimeEntry timeEntry = new TimeEntry { time = time, name = name };

        // Load saved bestTimes
        string jsonString = PlayerPrefs.GetString("bestTimesTable", "{}"); // Provide a default empty JSON if nothing is stored yet
        BestTimes bestTimes = JsonUtility.FromJson<BestTimes>(jsonString);

        // Add new entry to bestTimes
        bestTimes.timeEntryList.Add(timeEntry);

        // Save updated bestTimes
        string json = JsonUtility.ToJson(bestTimes);
        PlayerPrefs.SetString("bestTimesTable", json);
        PlayerPrefs.Save();
    }

    private class BestTimes {
        public List<TimeEntry> timeEntryList;
    }

    //Represents a single time entry
    [System.Serializable]
    private class TimeEntry {
        public float time;
        public string name;
    }
}