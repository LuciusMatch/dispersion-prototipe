using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraTrigger : MonoBehaviour
{
    Camera cam;
    CameraController cameraController;
    Transform cameraTransform;

    [SerializeField] int cameraSize;
    [SerializeField] bool isOrtographic;
    [SerializeField] float cameraSpeed;
    [SerializeField] bool perspectiveFollow;

    void Start()
    {
        cam = GameManager.Instance.mainCamera.GetComponent<Camera>();
        cameraController = GameManager.Instance.mainCamera.GetComponent<CameraController>();

        Assert.IsTrue(gameObject.GetComponentsInChildren<Transform>().Length > 1); // there is a child
        cameraTransform = GetComponentsInChildren<Transform>()[1];
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
            {
                cameraController.SetNewCameraTransform(cameraTransform, cameraSize, cameraSpeed, isOrtographic);
            }
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
