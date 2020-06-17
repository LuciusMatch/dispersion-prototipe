using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardInventory : MonoBehaviour
{
    public List<GameObject> indicators = new List<GameObject>();

    [HideInInspector]
    public List<Keycard> keycards { get; private set; } = new List<Keycard>();

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
        return keycards.Find(card => card.keycardId == id) != null;
    }

    public void AddKeycard(Keycard card)
    {
        keycards.Add(card);
        indicators[card.keycardId].SetActive(true);
        Debug.Log("Keycard " + card.keycardId + " collected");
    }

    public void SetKeycards(List<Keycard> keycards)
    {
        this.keycards = keycards;
        UpdateIndicators();
    }

    public void UpdateIndicators()
    {
        foreach (Keycard card in keycards)
        {
            indicators[card.keycardId].SetActive(true);
        }
    }
}
