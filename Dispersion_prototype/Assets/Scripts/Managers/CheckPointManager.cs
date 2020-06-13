using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    public static int lastCheckPoint;
    public const string PREFS_LAST_CHECKPOINT_KEY = "LastCheckpoint";

    public static CheckPointManager instance;
    private CheckPoint[] checkPoints;

    public int lastCheckPointOverride = -1;

    public bool hadGun;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        if (lastCheckPointOverride >= 0)
        {
            lastCheckPoint = lastCheckPointOverride;
        }
    }

    public CheckPoint GetLastCheckPoint()
    {
        return GetCheckPointById(lastCheckPoint);
    }

    public CheckPoint GetCheckPointById(int id)
    {
        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (checkPoint.checkPointNumber == id)
            {
                return checkPoint;
            }
        }

        return null;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        checkPoints = GameManager.Instance.checkPointParent.GetComponentsInChildren<CheckPoint>();
    }
}
