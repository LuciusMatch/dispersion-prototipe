using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardInventory : MonoBehaviour
{
    private List<int> keycards = new List<int>();

    public bool HasKeycard(int id)
    {
        return keycards.Contains(id);
    }

    public void AddKeycard(int id)
    {
        keycards.Add(id);
        Debug.Log("Keycard " + id + " collected");
    }
}
