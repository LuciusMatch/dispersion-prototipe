using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionHandler : MonoBehaviour
{
    public bool isEnabled = true;
    public bool hasTrigger = true;
    public int emissiveMaterialIndex = 0;

    private Collider trigger;
    private ParticleSystem particles;
    private Material emissiveMaterial;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
        particles = GetComponentInChildren<ParticleSystem>();
        emissiveMaterial = GetComponentInChildren<MeshRenderer>().materials[emissiveMaterialIndex];

        if (!isEnabled) TurnOff();
        else TurnOn();
    }

    public void TurnOn()
    {
        if (hasTrigger) trigger.enabled = true;
        particles.Play();
        emissiveMaterial.EnableKeyword("_EMISSION");
        isEnabled = true;
    }

    public void TurnOff()
    {
        if (hasTrigger) trigger.enabled = false;
        particles.Stop();
        emissiveMaterial.DisableKeyword("_EMISSION");
        isEnabled = false;
    }

    public void Toggle()
    {
        if (isEnabled)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
}
