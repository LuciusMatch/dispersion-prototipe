using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    private CheckPoint[] checkPoints;

    public int lastCheckPoint;

    public bool hadGun;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            checkPoints = GetComponentsInChildren<CheckPoint>();
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
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
}
