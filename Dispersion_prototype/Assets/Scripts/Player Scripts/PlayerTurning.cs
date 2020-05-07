using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurning : MonoBehaviour
{
    [SerializeField]
    public float turningspeed = 5;

    float camRayLength = 100f;
    int floorMask;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        floorMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Turning();
    }
    void Turning()
    {
        if (playerController.usingGun) //if a player has gun, then character turns to the coursor, if not, then to the movement direction 
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {

                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10f);
                //transform.rotation = newRotation;
            }
        }

        else
        {
            if (playerController.movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(playerController.movement), Time.deltaTime * turningspeed);
            }
        }
    }
}
