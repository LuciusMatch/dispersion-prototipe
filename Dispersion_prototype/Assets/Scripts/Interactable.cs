using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool PlayerIn = false;

    void Update()
    {
        if (!PlayerIn)
            return;

        if (Input.GetMouseButtonDown(0) == true)
            Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIn)
            return;

        PlayerIn = (other.tag == "Player" || other.tag == "Clone");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PlayerIn)
            return;

        PlayerIn = !(other.tag == "Player" || other.tag == "Clone");
    }

    public virtual void Interact()
    {
        Debug.Log("INTERACT");
    }
}
