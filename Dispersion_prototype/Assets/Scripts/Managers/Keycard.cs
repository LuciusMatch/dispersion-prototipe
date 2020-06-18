using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public int keycardId = 0;
    private List<Collider> others = new List<Collider>();
    private bool pickedUp = false;

    private void Update()
    {
        if (pickedUp)
        {
            PickingUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        others.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        others.Remove(other);
    }

    public void PickUp()
    {
        pickedUp = true;
        GameManager.audioPlayer.KeycardPickedUp();
    }

    private void PickingUp()
    {
        others.RemoveAll(item => item == null);

        // Disable children, disabling the whole game object would disable the audio too
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Collider other in others)
        {
            KeycardInventory inventory = other.GetComponent<KeycardInventory>();
            if (inventory != null)
            {
                inventory.AddKeycard(this);
            }
        }
    }

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        pickedUp = false;
    }
}
