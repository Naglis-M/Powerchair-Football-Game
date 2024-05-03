using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public BestTimesTable bestTimesTable;
    public TMPro.TMP_Dropdown levelDropdown;

    void Start()
    {
        levelDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void OnDropdownChanged(int index)
    {
        bestTimesTable.levelKey = "Level" + (index + 1) + "Times"; // Assuming dropdown values start from 0
        bestTimesTable.LoadLeaderboard();
    }
}
