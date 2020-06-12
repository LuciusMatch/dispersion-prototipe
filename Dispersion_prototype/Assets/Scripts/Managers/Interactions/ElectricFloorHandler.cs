using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloorHandler : MonoBehaviour
{
    public bool isEnabled = true;

    private Collider trigger;
    private ParticleSystem particles;
    private Material emissiveMaterial;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
        particles = GetComponentInChildren<ParticleSystem>();
        emissiveMaterial = GetComponentInChildren<MeshRenderer>().material;

        if (!isEnabled) TurnOff();
        else TurnOn();
    }

    public void TurnOn()
    {
        trigger.enabled = true;
        particles.Play();
        emissiveMaterial.EnableKeyword("_EMISSION");
        isEnabled = true;
    }

    public void TurnOff()
    {
        trigger.enabled = false;
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
