using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTurning : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    CloneController cloneController;

    //public float turningspeed = 5;

    //CloneController cloneController;
    //// Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.Find("PlayerTurning").gameObject;
        //player = GameManager.Instance.player.transform.Find("PlayerTurning").gameObject; ;
        cloneController = transform.parent.GetComponent<CloneController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (using Gun)
        CopyTurning();

        //Turning();
    }

    void CopyTurning()
    {

        Quaternion newRotation = player.transform.localRotation;

        if (cloneController.reversemovement == true)
        {
            newRotation.w *= -1;
            newRotation.x *= -1;
        }

        if (cloneController.alive)
        {
            transform.localRotation = newRotation;
        }
    }

    //void Turning()
    //{
    //    if (cloneController.movement != Vector3.zero)
    //    {
    //        transform.rotation = Quaternion.Slerp(
    //            transform.rotation,
    //            Quaternion.LookRotation(cloneController.movement), Time.deltaTime * turningspeed); // IT IS NOT LOCAL!!!
    //    }
    //}

}
