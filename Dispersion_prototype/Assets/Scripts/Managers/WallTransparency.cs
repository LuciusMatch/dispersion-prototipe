using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency  : MonoBehaviour
{

    public bool transparentOn;

    [SerializeField]
    public Material baseMaterial;
    [SerializeField]
    public Material fadeMaterial;

    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (transparentOn)
            renderer.material = fadeMaterial;
        else
            renderer.material = baseMaterial;
    }

}
