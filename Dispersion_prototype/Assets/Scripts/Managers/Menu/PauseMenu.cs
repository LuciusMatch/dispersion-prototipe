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
        AudioListener.pause = false;
        GameMenu.isPaused = false;
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Destroy(DontDestroy.obj);
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
