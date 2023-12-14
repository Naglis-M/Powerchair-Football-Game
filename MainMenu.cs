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
    public void TimeTrial()
    {
        SceneManager.LoadScene("TT Level 1");
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
