using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    // Update is called once per frame
    float minFlickerSpeed = 0.1f;
    float maxFlickerSpeed = 1.0f;
    Light light;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    void Update()
    {
        //light.enabled = true;
        //yield WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed);
        //light.enabled = false;
        //yield WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed);
    }
}
