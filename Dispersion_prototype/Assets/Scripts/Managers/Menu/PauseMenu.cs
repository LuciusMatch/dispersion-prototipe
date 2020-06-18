using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject optionsMenuUI;

    public GameObject pauseFirstButton;
    public GameObject optionsButton;
    public GameObject optionsFirstButton;

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameMenu.isPaused = false;
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Destroy(CheckPointManager.instance.gameObject);
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        gameObject.SetActive(false);
        optionsMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void ReturnFromOptions()
    {
        gameObject.SetActive(true);
        optionsMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }
}
