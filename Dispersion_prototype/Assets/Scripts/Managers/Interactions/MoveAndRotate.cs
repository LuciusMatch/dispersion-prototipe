using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndRotate : MonoBehaviour
{
    [SerializeField]
    Transform transform_1;
    [SerializeField]
    Transform transform_2;
    [SerializeField]
    float speed = 4;
    [SerializeField]
    bool translateObject;
    [SerializeField]
    bool rotateObject;

    private void Start()
    {

    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (translateObject)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, transform_2.position, step);
            if (transform.position == transform_2.position)
            {
                translateObject = false;
                Transform temp = transform_2;
                transform_2 = transform_1;
                transform_1 = temp;
            }
        }

        if (rotateObject)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform_2.rotation, step * 4);
            if (transform.rotation == transform_2.rotation)
            {
                rotateObject = false;
                //Transform temp = transform_2;
                //transform_2 = transform_1;
                //transform_1 = temp;
            }
        }
    }

    public void TranslateObject()
    {
        translateObject = true;
    }

    public void RotateObject()
    {
        rotateObject = true;
    }
}
