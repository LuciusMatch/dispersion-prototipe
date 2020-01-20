using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    int chekpointnumber;

    [SerializeField]
    bool checkpointactivated;

    CheckPointManager checkPointManager;

    private void Start()
    {
        checkPointManager = GameObject.Find("CheckPoint Manager").GetComponent<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            checkpointactivated = true;
            checkPointManager.lastactivated = chekpointnumber;
        }
    }
}
