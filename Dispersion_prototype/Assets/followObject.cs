using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObject : MonoBehaviour
{
    [SerializeField]
    Transform transformToFollow;

    void FixedUpdate()
    {
        this.transform.position = transformToFollow.position;
    }
}
