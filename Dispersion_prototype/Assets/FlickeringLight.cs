using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    // Update is called once per frame
    [SerializeField]
    float minFlickerSpeed = 0.1f;
    [SerializeField]
    float maxFlickerSpeed = 1.0f;

    [SerializeField]
    float minIntensity = 50;
    [SerializeField]
    float maxIntensity = 90;

    Light light;

    bool flag;

    private void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(DoFlicker());
    }

    private void FixedUpdate()
    {
        if (!flag)
            StartCoroutine(DoFlicker());
    }
    private IEnumerator DoFlicker()
    {
        flag = true;
        light.intensity = Random.Range(minIntensity, maxIntensity);
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        light.intensity = maxIntensity;
        flag = false;
    }
}
