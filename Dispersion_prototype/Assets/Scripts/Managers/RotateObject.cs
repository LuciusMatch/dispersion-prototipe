using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    float xRotSpeed = 10;
    [SerializeField]
    float yRotSpeed = 0;
    [SerializeField]
    float zRotSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Rotate(xRotSpeed * Time.deltaTime, yRotSpeed * Time.deltaTime, zRotSpeed * Time.deltaTime);
    }
}
