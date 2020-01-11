﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTurning : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.Find("PlayerTurning").gameObject; //FIX FIX FIX
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CopyTurning();
    }

    void CopyTurning()
    {

        Quaternion newRotation = player.transform.localRotation;

        if (transform.parent.GetComponent<CloneController>().reversemovement == true)
        {
            newRotation.w *= -1;
            newRotation.x *= -1;
        }

        transform.localRotation = newRotation;
    }

}
