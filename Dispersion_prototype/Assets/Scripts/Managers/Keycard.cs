using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public int keycardId = 0;
    private List<Collider> others = new List<Collider>();

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
        foreach (Collider other in others)
        {
            KeycardInventory inventory = other.GetComponent<KeycardInventory>();
            if (inventory != null)
            {
                inventory.AddKeycard(keycardId);
            }
        }
    }
}
