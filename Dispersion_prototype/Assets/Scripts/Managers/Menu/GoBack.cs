using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Responsible for traversing the menu up
public class GoBack : MonoBehaviour
{
    public UnityEvent e;
    private PlayerControls input;

    private void Awake()
    {
        input = new PlayerControls();
    }

    void Update()
    {
        if (input.UI.Cancel.triggered)
        {
            e.Invoke();
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
