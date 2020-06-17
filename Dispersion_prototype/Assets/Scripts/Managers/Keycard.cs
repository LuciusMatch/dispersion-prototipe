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
        Debug.Log(others);
        others.Remove(other);
        Debug.Log(others);
    }

    public void PickUp()
    {
        pickedUp = true;
    }

    private void PickingUp()
    {
        others.RemoveAll(item => item == null);
        gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        pickedUp = false;
    }
}
