using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool PlayerIn = false;

    void Update()
    {
        if (!PlayerIn)
            return;

        if (Input.GetButton("Use") == true)
            Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GunArea")
            return;

        if (PlayerIn)
            return;

        PlayerIn = (other.tag == "Player" || other.tag == "Clone");

        GameObject.Find("Help_text").GetComponent<Text>().text = "press 'E' to use";

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GunArea")
            return;

        if (!PlayerIn)
            return;

        PlayerIn = !(other.tag == "Player" || other.tag == "Clone");

        GameObject.Find("Help_text").GetComponent<Text>().text = "";
    }

    public virtual void Interact()
    {
        Debug.Log("INTERACT");
    }
}
