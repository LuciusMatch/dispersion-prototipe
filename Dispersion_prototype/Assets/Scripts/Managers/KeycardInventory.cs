using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardInventory : MonoBehaviour
{
    public List<GameObject> indicators = new List<GameObject>();

    [HideInInspector]
    public List<int> keycards { get; private set; } = new List<int>();

    private void Start()
    {
        foreach (GameObject indicator in indicators)
        {
            indicator.SetActive(false);
        }
        UpdateIndicators();
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

    public void SetKeycards(List<int> keycards)
    {
        this.keycards = keycards;
        UpdateIndicators();
    }

    public void UpdateIndicators()
    {
        foreach (int keycard in keycards)
        {
            indicators[keycard].SetActive(true);
        }
    }
}
