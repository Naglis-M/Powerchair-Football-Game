using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class CheckpointSingle : MonoBehaviour
{
    public bool isActivated = false; // Indicates whether the checkpoint has been passed
    private LevelCheckpoints levelCheckpoints;
    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        Hide();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Chair") {
            if (!isActivated && levelCheckpoints.IsNextCheckpoint(this)) {
                // This is the next expected checkpoint
                isActivated = true;
                levelCheckpoints.PlayerThroughCheckpoint(this);
                Hide();
            } else if (!isActivated) {
                // Player missed a previous checkpoint
                levelCheckpoints.PlayerMissedCheckpoint(this);
                // Optionally show some indication that the player has missed a checkpoint
            }
        }
    }

    public void SetLevelCheckpoints(LevelCheckpoints levelCheckpoints) {
        this.levelCheckpoints = levelCheckpoints;
    }

    public void Show() {
        meshRenderer.enabled = true;
    }

    public void Hide() {
        meshRenderer.enabled = false;
    }
}