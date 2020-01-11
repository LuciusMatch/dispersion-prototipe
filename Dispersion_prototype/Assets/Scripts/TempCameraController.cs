using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraController : MonoBehaviour
{

    [SerializeField]
    Transform CameraPoint;
    [SerializeField]
    int camerasize;
    Camera cam;

    public float speed = 5.0F;
    private float startTime;
    private float journeyLength;
    Vector3 starttransform;

    private bool movecam;

    
    void Start()
    {
        movecam = false;
    }

    private void Update()
    {
        if (movecam)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            cam.transform.position = Vector3.Lerp(starttransform, CameraPoint.position, fracJourney*4);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, camerasize, fracJourney/5);
            if (cam.transform.position == CameraPoint.position) movecam = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cam = Camera.main;

            startTime = Time.time;
            starttransform = cam.transform.position;
            journeyLength = Vector3.Distance(starttransform, CameraPoint.position);

            movecam = true;
        }
    }

}
