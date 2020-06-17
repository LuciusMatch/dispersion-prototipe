using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graviplate : MonoBehaviour
{
    public GameObject wall;
    public EmissionHandler emission;
    private bool isEnabled = false;

    public void Enable()
    {
        wall.tag = "Simple Gravitation";
        isEnabled = true;
    }

    public void Disable()
    {
        wall.tag = "Untagged";
        isEnabled = false;
    }

    public void Toggle()
    {
        if (isEnabled)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        emission.TurnOff();
        Disable();
    }
}
