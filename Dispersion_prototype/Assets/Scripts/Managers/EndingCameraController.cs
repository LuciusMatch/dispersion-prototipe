using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCameraController : MonoBehaviour
{

    [SerializeField]
    Transform CameraPointEnding1;
    [SerializeField]
    Transform CameraPointEnding2;
    [SerializeField]
    int camerasize;
    [SerializeField]
    bool isortographic;
    Camera cam;

    public float speed1 = 10000.0F;
    public float speed2 = 25.0F;
    private float startTime;
    private float journeyLength;
    Vector3 starttransform;
    Quaternion startrotation;

    private bool movecam;
    private bool movetoanend;
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
            float distCovered = (Time.time - startTime) * speed1;
            float fracJourney = distCovered / journeyLength;

            cam.transform.position = Vector3.Lerp(starttransform, CameraPointEnding1.position, fracJourney * 4);
            cam.transform.rotation = Quaternion.Lerp(startrotation, CameraPointEnding1.rotation, fracJourney * 4);

            if (isortographic)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camerasize, fracJourney / 5);

            if ((cam.transform.position == CameraPointEnding1.position) && (cam.transform.rotation == CameraPointEnding1.rotation))
            {
                CountNewPath();
                movetoanend = true;
            }

            if (movetoanend)
            {
                distCovered = (Time.time - startTime) * speed2;
                fracJourney = distCovered / journeyLength;

                cam.transform.position = Vector3.Lerp(starttransform, CameraPointEnding2.position, fracJourney * 4);
                cam.transform.rotation = Quaternion.Lerp(startrotation, CameraPointEnding2.rotation, fracJourney * 4);

                if ((cam.transform.position == CameraPointEnding2.position) && (cam.transform.rotation == CameraPointEnding2.rotation))
                {
                    movecam = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
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
            journeyLength = Vector3.Distance(starttransform, CameraPointEnding1.position);

            if (isortographic)
                cam.orthographic = true;
            else
                cam.orthographic = false;

            movecam = true;
        }
    }

    private void CountNewPath()
    {
        cam = Camera.main;

        startTime = Time.time;
        startrotation = cam.transform.rotation;
        starttransform = cam.transform.position;
        journeyLength = Vector3.Distance(starttransform, CameraPointEnding2.position);

        if (isortographic)
            cam.orthographic = true;
        else
            cam.orthographic = false;

        movecam = true;
    }

}
