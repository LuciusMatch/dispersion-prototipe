using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Responsible for traversing the menu up
public class GoBack : MonoBehaviour
{
    public UnityEvent e;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            e.Invoke();
        }
    }
}
