using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int checkpointnumber;

    CheckPointManager checkPointManager;
    public Transform CameraPoint;
    public int camerasize;
    public bool isortographic;

    private void Start()
    {
        gameObject.name = string.Format("CheckPoint " + checkpointnumber);
        checkPointManager = GameObject.Find("CheckPoint Manager").GetComponent<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            checkPointManager.lastCheckPoint = checkpointnumber;
        }
    }
}
