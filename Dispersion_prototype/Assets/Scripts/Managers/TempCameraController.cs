using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraController : MonoBehaviour
{

    [SerializeField]
    Transform CameraPoint;
    [SerializeField]
    int camerasize;
    [SerializeField]
    bool isortographic;
    Camera cam;

    public float speed = 5.0F;
    private float startTime;
    private float journeyLength;
    Vector3 starttransform;
    Quaternion startrotation;

    private bool movecam;

    PlayerController playerController;

    void Start()
    {
        movecam = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (movecam)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            cam.transform.position = Vector3.Lerp(starttransform, CameraPoint.position, fracJourney*4);
            cam.transform.rotation = Quaternion.Lerp(startrotation, CameraPoint.rotation, fracJourney*4);

            if (isortographic)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camerasize, fracJourney/5);

            if ((cam.transform.position == CameraPoint.position) && (cam.transform.rotation == CameraPoint.rotation))
            {
                playerController.GetMovmentDir();
                movecam = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cam = Camera.main;

            startTime = Time.time;
            startrotation = cam.transform.rotation;
            starttransform = cam.transform.position;
            journeyLength = Vector3.Distance(starttransform, CameraPoint.position);

            if (isortographic)
                cam.orthographic = true;
            else
                cam.orthographic = false;

            movecam = true;
        }
    }

}
