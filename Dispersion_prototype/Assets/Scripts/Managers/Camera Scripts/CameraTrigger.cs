using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    Camera cam;
    CameraController cameraController;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    int cameraSize;
    [SerializeField]
    bool isOrtographic;
    [SerializeField]
    float cameraSpeed;
    [SerializeField]
    bool perspectiveFollow;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameManager.Instance.mainCamera.GetComponent<Camera>();
        cameraController = GameManager.Instance.mainCamera.GetComponent<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (perspectiveFollow)
            {
                cameraController.SetCameraFollowPlayer(false);
            }
            else
                cameraController.SetNewCameraTransform(cameraTransform, cameraSize, cameraSpeed, isOrtographic);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        cam.orthographic = true;

        if (other.tag == "Player" && !perspectiveFollow)
        {
            cameraController.SetCameraFollowPlayer(true);
        }
    }
}
