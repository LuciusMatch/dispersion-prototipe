using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool characterIn = false;
    public GameObject Interactor;

    void Update()
    {
        if (!characterIn)
            return;

        if (Input.GetButton("Use") == true)
            Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GunArea")
            return;

        if (characterIn)
            return;

        characterIn = (other.tag == "Player" || other.tag == "Clone");

        Interactor = other.gameObject;
        //Debug.Log(Interactor.name + " in interacting!");
        GameObject.Find("Help_text").GetComponent<Text>().text = "press 'E' to use";

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GunArea")
            return;

        if (!characterIn)
            return;

        characterIn = !(other.tag == "Player" || other.tag == "Clone");
        
        Interactor = null;
        
        GameObject.Find("Help_text").GetComponent<Text>().text = "";
    }

    public virtual void Interact()
    {
        Debug.Log("INTERACT");
    }
}
