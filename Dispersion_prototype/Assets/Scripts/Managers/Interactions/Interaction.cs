using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType { Use, UseOnce }
public class Interaction : MonoBehaviour
{
    public EventType eventType;
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
            action.Invoke();
            alreadyUsed = true;
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
