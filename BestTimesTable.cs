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
    private List<TimeEntry> timeEntryList;
    private List<Transform> timeEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("TimesEntryContainer");
        entryTemplate = entryContainer.Find("TimesEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        timeEntryList = new List<TimeEntry>() {
            new TimeEntry { time = 07.66f, name = "AAA" },
            new TimeEntry { time = 09.54f, name = "MAX" },
            new TimeEntry { time = 06.09f, name = "DAN" },
            new TimeEntry { time = 11.34f, name = "BOO" },
            new TimeEntry { time = 04.67f, name = "LAP" },
            new TimeEntry { time = 08.21f, name = "NAG" },
            new TimeEntry { time = 08.24f, name = "MAT" },
            new TimeEntry { time = 07.89f, name = "LEE" },
            new TimeEntry { time = 14.23f, name = "COL" },
            new TimeEntry { time = 10.01f, name = "YOI" },
        };

        timeEntryTransformList = new List<Transform>();

        foreach (TimeEntry timeEntry in timeEntryList) {
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

        transformList.Add(entryTransform);
    }

    //Represents a single time entry

    private class TimeEntry {
        public float time;
        public string name;
    }
}