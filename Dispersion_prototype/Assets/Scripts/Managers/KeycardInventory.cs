using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardInventory : MonoBehaviour
{
    public List<GameObject> indicators = new List<GameObject>();

    [SerializeField]
    private List<int> keycards = new List<int>();

    private void Start()
    {
        foreach (GameObject indicator in indicators)
        {
            indicator.SetActive(false);
        }
    }

    public bool HasKeycard(int id)
    {
        return keycards.Contains(id);
    }

    public void AddKeycard(int id)
    {
        keycards.Add(id);
        indicators[id].SetActive(true);
        Debug.Log("Keycard " + id + " collected");
    }
}
