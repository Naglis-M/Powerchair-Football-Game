using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class LevelCheckpoints : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;
    public event EventHandler OnAllCheckpointsCompleted; // Event when all checkpoints are completed

    private List<CheckpointSingle> checkpointSingleList;
    private int nextCheckpointSingleIndex;

    public GameObject finishLine; // Assign this in the Inspector

    private void Awake() {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform) {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetLevelCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndex = 0;
        finishLine.SetActive(false); // Disable finish line initially
    }

    public bool IsNextCheckpoint(CheckpointSingle checkpoint) {
        return checkpointSingleList.IndexOf(checkpoint) == nextCheckpointSingleIndex;
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle) {
        if (IsNextCheckpoint(checkpointSingle)) {
            // Correct checkpoint
            checkpointSingle.isActivated = true; // Mark the checkpoint as activated
            checkpointSingle.Hide();
            nextCheckpointSingleIndex++;
            
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
            
            if (AreAllCheckpointsCompleted()) {
                finishLine.SetActive(true); // Activate the finish line
                OnAllCheckpointsCompleted?.Invoke(this, EventArgs.Empty);
            }
        } else {
            // Wrong checkpoint
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayerMissedCheckpoint(CheckpointSingle checkpointSingle) {
        //Logic for when a player misses a checkpoint here
        CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
        correctCheckpointSingle.Show();
        OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
    }

    private bool AreAllCheckpointsCompleted() {
        foreach (var checkpoint in checkpointSingleList) {
            if (!checkpoint.isActivated) {
                return false;
            }
        }
        return nextCheckpointSingleIndex >= checkpointSingleList.Count;
    }
}