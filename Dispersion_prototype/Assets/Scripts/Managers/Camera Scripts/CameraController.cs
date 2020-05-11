using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;

    Transform cameraPoint;
    int cameraSize;
    float cameraSpeed;
    bool cameraOrtographic;

    bool cameraIsLocked;

    private bool movecam;

    void Start()
    {
        cam = GetComponent<Camera>();
        movecam = false;
    }

    private void Update()
    {

        if (movecam && cameraOrtographic && cameraIsLocked)
        {
            MoveCameraOrtographic();
        }

        if (movecam && !cameraOrtographic && cameraIsLocked)
        {
            MoveCameraPerspective();
        }

        if (movecam && cameraOrtographic && !cameraIsLocked)
        {
            FollowPlayerOrtographic();
        }

        if (movecam && !cameraOrtographic && !cameraIsLocked)
        {
            //FollowPlayerPerspective();
        }
    }


    public void SetNewCameraTransform(Transform newposition, int newcamerasize, float speed, bool isOrtographic)
    {
        cameraPoint = newposition;
        cameraSize = newcamerasize;
        cameraSpeed = speed;
        cameraOrtographic = isOrtographic;

        if (cameraOrtographic)
            cam.orthographic = true;
        else
            cam.orthographic = false;

        cameraIsLocked = true;
        movecam = true;
    }

    public void SetCameraFollowPlayer(bool isOrtographic)
    {
        cameraOrtographic = isOrtographic;
        cameraIsLocked = false;
        movecam = true;
    }

    private void MoveCameraOrtographic()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPoint.position, cameraSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraPoint.rotation, cameraSpeed);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraSize, cameraSpeed);

        if (transform == cameraPoint && cam.orthographicSize == cameraSize) movecam = false;
    }

    private void MoveCameraPerspective()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPoint.position, cameraSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraPoint.rotation, cameraSpeed);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cameraSize, cameraSpeed);

        if (transform == cameraPoint && cam.fieldOfView == cameraSize) movecam = false;
    }

    private void FollowPlayerOrtographic() //TEMPORARY SOLUTION
    {
        Transform playerCam = GameManager.Instance.player.transform.Find("PlayerCameraPoint").transform;

        transform.position = Vector3.Lerp(transform.position, playerCam.position, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerCam.rotation, 0.05f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6.5f, 0.05f);


        //Vector3 newPosition = GameManager.Instance.player.transform.position + cameraPoint.position;

        //transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed);

        //transform.rotation = Quaternion.Lerp(transform.rotation, cameraPoint.rotation, cameraSpeed);
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraSize, cameraSpeed);
    }

    private void FollowPlayerPerspective() //TEMPORARY SOLUTION
    {
        //Transform playerCam = GameManager.Instance.player.transform.Find("PlayerCemeraPerspectivePoint").transform;

        //transform.position = Vector3.Lerp(transform.position, playerCam.position, 0.05f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, playerCam.rotation, 0.05f);
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6.5f, 0.05f);

        //if (transform.rotation == playerCam.rotation)
        //{
        //    Debug.Log("FFFFFFFFFF");
        //    GameManager.Instance.player.GetComponent<PlayerController>().GetMovmentDir();
        //}
    }
}
