using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceingCamera : MonoBehaviour
{

    void Update()
    {

        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.right,
            Camera.main.transform.rotation * - Vector3.forward);
    }
}
