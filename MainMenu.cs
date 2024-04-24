using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void Minigames()
    {
        SceneManager.LoadScene("Minigames Menu");
    }
    public void TimeTrialSelect() 
    {
        SceneManager.LoadScene("TT Menu");
    }

    public void LoadLevel1() 
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("TT Level 1");
    }

    public void LoadLevel2() 
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("TT Level 2");
    }

    public void LoadLevel3() 
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("TT Level 3");
    }

    public void LoadLevel4() 
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("TT Level 4");
    }

    public void LoadLevel5() 
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("TT Level 5");
    }
    public void RestartLevel() 
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        // Reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Options Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
