using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneField game;
    private bool gameInProgress;

    public void Start()
    {
        gameInProgress = PlayerPrefs.HasKey(CheckPointManager.PREFS_LAST_CHECKPOINT_KEY);
        gameObject.transform.Find("Continue Button").GetComponent<Button>().interactable = gameInProgress;
    }

    public void StartGame()
    {
        if (gameInProgress)
        {
            Debug.Log("Are you sure you want to start a new game? All your progress wil be lost.");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        CheckPointManager.lastCheckPoint = PlayerPrefs.GetInt(CheckPointManager.PREFS_LAST_CHECKPOINT_KEY);
        SceneManager.LoadScene(game);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        PlayerPrefs.DeleteAll();
#else
        Application.Quit();
#endif
    }
}
