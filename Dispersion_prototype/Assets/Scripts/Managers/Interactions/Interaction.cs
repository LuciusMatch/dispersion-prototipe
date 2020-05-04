using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType { Use }
public class Interaction : MonoBehaviour
{
    public EventType eventType;
    public UnityEvent action;

    void Awake()
    {
        if (action == null)
            action = new UnityEvent();
    }

    void OnTriggerStay(Collider other)
    {
        if (eventType == EventType.Use && (other.tag == "Player" || other.tag == "Clone") && Input.GetButton("Use"))
        {
            action.Invoke();
        }
    }
}
