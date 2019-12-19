using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurning : MonoBehaviour
{
    float camRayLength = 100f;
    int floorMask;
    // Start is called before the first frame update
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Turning();
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {

            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
        }
    }
}
