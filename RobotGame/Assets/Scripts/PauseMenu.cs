using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // остановка времени в игре
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // остановка времени в игре
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("Untitled");
        Time.timeScale = 1f;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0); //LevelRestart
    }       
}
