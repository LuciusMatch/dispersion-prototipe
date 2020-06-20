using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public SceneField game;
    public int numberOfLevels;
    public Button continueButton;

    public Button levelSelectorButton;
    public Button levelSelectorBack;
    public Button levelSelectorReset;
    public GameObject levelSelectorUI;
    public Transform levelSelectorContent;
    public GameObject levelPrefab;

    private bool gameInProgress;

    public void Awake()
    {
        gameInProgress = (PlayerPrefs.GetInt(CheckPointManager.PREFS_LAST_CHECKPOINT_KEY, 0) > 0);
        continueButton.interactable = gameInProgress;
        levelSelectorButton.interactable = PlayerPrefs.HasKey(CheckPointManager.PREFS_MAX_CHECKPOINT_KEY);

        Button prevButton = null;
        for (int i = 0; i < numberOfLevels; i++)
        {
            GameObject go = Instantiate(levelPrefab, levelSelectorContent);
            go.GetComponent<MenuButton>().text = "Level " + i;
            Button btn = go.GetComponent<Button>();
            btn.interactable = (i <= PlayerPrefs.GetInt(CheckPointManager.PREFS_MAX_CHECKPOINT_KEY, 0));

            int checkPointId = i; // this is needed because else all the buttons will be set to the last checkpoint
            btn.onClick.AddListener(() => StartFromCheckpoint(checkPointId));

            if (i == PlayerPrefs.GetInt(CheckPointManager.PREFS_MAX_CHECKPOINT_KEY, 0))
            {
                Navigation navig = new Navigation();
                navig.mode = Navigation.Mode.Explicit;
                navig.selectOnDown = levelSelectorBack;
                navig.selectOnUp = prevButton;
                btn.navigation = navig;

                navig = levelSelectorBack.navigation;
                navig.selectOnUp = btn;
                levelSelectorBack.navigation = navig;

                navig = levelSelectorReset.navigation;
                navig.selectOnUp = btn;
                levelSelectorReset.navigation = navig;
            }

            prevButton = btn;
        }
    }

    public void StartGame()
    {
        if (gameInProgress)
        {
            Debug.Log("Are you sure you want to start a new game? All your progress wil be lost.");
            CheckPointManager.lastCheckPoint = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        CheckPointManager.lastCheckPoint = PlayerPrefs.GetInt(CheckPointManager.PREFS_LAST_CHECKPOINT_KEY);
        SceneManager.LoadScene(game);
    }

    public void StartFromCheckpoint(int checkpoint)
    {
        CheckPointManager.lastCheckPoint = checkpoint;
        SceneManager.LoadScene(game);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Levels()
    {
        gameObject.SetActive(false);
        levelSelectorUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(levelSelectorContent.GetComponentInChildren<Button>().gameObject);
    }

    public void ReturnFromLevels()
    {
        gameObject.SetActive(true);
        levelSelectorUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(levelSelectorButton.gameObject);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(CheckPointManager.PREFS_LAST_CHECKPOINT_KEY);
        PlayerPrefs.DeleteKey(CheckPointManager.PREFS_MAX_CHECKPOINT_KEY);
        continueButton.interactable = false;
        levelSelectorButton.interactable = false;
    }
}
