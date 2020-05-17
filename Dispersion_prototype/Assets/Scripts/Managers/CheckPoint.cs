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
        checkPointManager = CheckPointManager.instance;
        checkPointNumber = int.Parse(gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            checkPointManager.lastCheckPoint = checkPointNumber;
            checkPointManager.hadGun = other.GetComponent<PlayerController>().hasGun;
        }
    }
}
