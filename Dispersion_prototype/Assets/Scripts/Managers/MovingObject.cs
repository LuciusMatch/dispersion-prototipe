using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    Transform starttransform;
    [SerializeField]
    Transform endtransform;
    [SerializeField]
    bool iscontinous;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;



    // Start is called before the first frame update
    void Start()
    {
        starttransform = transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(starttransform.position, endtransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (iscontinous)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(starttransform.position, endtransform.position, fracJourney);

            if (transform.position == endtransform.position) //not working for some reason
            {
                Debug.Log("I am here");
                Vector3 temp = endtransform.position;
                endtransform.position = starttransform.position;
                starttransform.position = temp;
                journeyLength = Vector3.Distance(starttransform.position, endtransform.position);
            }

        }
        else
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(starttransform.position, endtransform.position, fracJourney);
        }
    }
}
