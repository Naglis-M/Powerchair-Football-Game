using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

  public void Resume()
{
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    AudioListener.pause = false; // Unpause all audio
    GameIsPaused = false;
}

void Pause()
{
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    AudioListener.pause = true; // Pause all audio
    GameIsPaused = true;
}

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Loading...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
