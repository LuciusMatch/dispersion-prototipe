﻿using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType { Use, UseOnce, PickUp, PressurePlateOnOff, PressurePlateRepeated }
public class Interaction : MonoBehaviour
{
    public EventType eventType;
    [ConditionalField(nameof(eventType), false, EventType.Use, EventType.UseOnce)] public bool keycardRequired = false;
    [ConditionalField(nameof(keycardRequired))] public int keycardID;
    public UnityEvent action;
    [ConditionalField(nameof(eventType), false, EventType.PressurePlateOnOff)] public UnityEvent actionOff;

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
            if (other.tag == "Player" || other.tag == "Clone")
            {
                if (Input.GetButtonDown("Use"))
                {
                    if (eventType == EventType.Use || (eventType == EventType.UseOnce && !alreadyUsed))
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
                    else if (eventType == EventType.PickUp)
                    {
                        action.Invoke();
                        Destroy(gameObject);
                    }
                }
                else if (eventType == EventType.PressurePlateRepeated)
                {
                    action.Invoke();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        others.Add(other);

        if (eventType == EventType.PressurePlateOnOff && (other.tag == "Player" || other.tag == "Clone"))
        {
            action.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        others.Remove(other);

        if (eventType == EventType.PressurePlateOnOff && (other.tag == "Player" || other.tag == "Clone"))
        {
            actionOff.Invoke();
        }
    }
}
