using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform CameraPoint;
    public int camerasize;
    public bool isortographic;
    [HideInInspector] public int checkPointNumber;

    private CheckPointManager checkPointManager;

    private void Awake()
    {
        checkPointNumber = int.Parse(gameObject.name);
    }

    private void Start()
    {
        checkPointManager = CheckPointManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CheckPointManager.lastCheckPoint = checkPointNumber;
            PlayerPrefs.SetInt(CheckPointManager.PREFS_LAST_CHECKPOINT_KEY, checkPointNumber);
            checkPointManager.hadGun = other.GetComponent<PlayerController>().hasGun;
        }
    }
}
