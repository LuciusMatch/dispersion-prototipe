using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType { Use, UseOnce }
public class Interaction : MonoBehaviour
{
    public EventType eventType;
    [ConditionalField(nameof(eventType), false, EventType.Use, EventType.UseOnce)] public bool keycardRequired = false;
    [ConditionalField(nameof(keycardRequired))] public int keycardID;
    public UnityEvent action;

    private bool alreadyUsed = false;
    private List<Collider> others = new List<Collider>();

    void Awake()
    {
        if (action == null)
            action = new UnityEvent();
    }

    private void Update()
    {
        foreach (Collider other in others)
        {
            if (Input.GetButtonDown("Use")
                && (eventType == EventType.Use || (eventType == EventType.UseOnce && !alreadyUsed))
                && (other.tag == "Player" || other.tag == "Clone"))
            {
                if (!keycardRequired || GameManager.Instance.player.GetComponent<KeycardInventory>().HasKeycard(keycardID))
                {
                    action.Invoke();
                    alreadyUsed = true;
                }
                else
                {
                    Debug.Log("Keycard not collected yet");
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        others.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        others.Remove(other);
    }
}
