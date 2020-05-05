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
    private Collider other;

    void Awake()
    {
        if (action == null)
            action = new UnityEvent();
    }

    private void Update()
    {
        if (other != null
            && (eventType == EventType.Use || (eventType == EventType.UseOnce && !alreadyUsed))
            && (other.tag == "Player" || other.tag == "Clone")
            && Input.GetButtonDown("Use"))
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

    // MAY CAUSE PROBLEMS IF MULTIPLE OBJECTS CAN ENTER THE TRIGGER AT THE SAME TIME – AND ONLY ONE LEAVES

    void OnTriggerEnter(Collider other)
    {
        this.other = other;
    }

    void OnTriggerExit(Collider other)
    {
        this.other = null;
    }
}
