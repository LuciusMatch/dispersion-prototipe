using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadAfterCkeckPoint : MonoBehaviour
{
    CheckPointManager checkPointManager;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();

        checkPointManager = GameObject.Find("CheckPoint Manager").GetComponent<CheckPointManager>();
        string lastcheckpointname = string.Format("CheckPoint " + checkPointManager.lastCheckPoint);
        CheckPoint lastcheckpoint = GameObject.Find(lastcheckpointname).GetComponent<CheckPoint>();
        transform.position = lastcheckpoint.CameraPoint.position;
        transform.rotation = lastcheckpoint.CameraPoint.rotation;
        cam.orthographic = lastcheckpoint.isortographic;
        if (lastcheckpoint.isortographic)
            cam.orthographicSize = lastcheckpoint.camerasize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
