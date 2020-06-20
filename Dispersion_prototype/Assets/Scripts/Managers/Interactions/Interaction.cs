using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType { Use, UseOnce, Trigger, TriggerOnce, PressurePlateOnOff, PressurePlateRepeated, StoryNote }
public class Interaction : MonoBehaviour
{
    public EventType eventType;
    public IconTypes icon;
    [ConditionalField(nameof(eventType), false, EventType.Use, EventType.UseOnce)] public bool keycardRequired = false;
    [ConditionalField(nameof(keycardRequired))] public int keycardID;
    public UnityEvent action;
    [ConditionalField(nameof(eventType), false, EventType.PressurePlateOnOff, EventType.StoryNote)] public UnityEvent actionOff;

    private bool alreadyUsed = false;
    private List<Collider> others = new List<Collider>();

    void Awake()
    {
        if (action == null)
            action = new UnityEvent();
    }

    private void Update()
    {
        others.RemoveAll(item => item == null);
        foreach (Collider other in others)
        {
            if (other.tag == "Player" || other.tag == "Clone")
            {
                if (Input.GetButtonDown("Use"))
                {
                    if (eventType == EventType.Use || eventType == EventType.StoryNote
                        || (eventType == EventType.UseOnce && !alreadyUsed))
                    {
                        if (!keycardRequired || other.gameObject.GetComponent<KeycardInventory>().HasKeycard(keycardID))
                        {
                            action.Invoke();
                            alreadyUsed = true;
                        }
                        else
                        {
                            Debug.Log("Keycard not collected yet");
                            GameManager.audioPlayer.AccessDenied();
                        }
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
        GameManager.indicator.TurnOn(icon);

        if ((eventType == EventType.PressurePlateOnOff || eventType == EventType.Trigger
            || (eventType == EventType.TriggerOnce && !alreadyUsed))
            && (other.tag == "Player" || other.tag == "Clone"))
        {
            alreadyUsed = true;
            action.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        others.Remove(other);
        GameManager.indicator.TurnOff();

        if ((other.tag == "Player" || other.tag == "Clone")
            && eventType == EventType.PressurePlateOnOff || (eventType == EventType.StoryNote && alreadyUsed))
        {
            actionOff.Invoke();
        }
    }
}
