using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadAfterCkeckPoint : MonoBehaviour
{
    CheckPointManager checkPointManager;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        checkPointManager = CheckPointManager.instance;
        CheckPoint lastCheckPoint = checkPointManager.GetLastCheckPoint();

        if (lastCheckPoint == null)
        {
            Debug.LogError("Initial checkpoint not found!");
        }

        transform.position = lastCheckPoint.CameraPoint.position;
        transform.rotation = lastCheckPoint.CameraPoint.rotation;
        cam.orthographic = lastCheckPoint.isortographic;

        if (lastCheckPoint.isortographic)
            cam.orthographicSize = lastCheckPoint.camerasize;
    }
}
