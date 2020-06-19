using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IconTypes { None, Joystick, Read, Walk, Keycard };

public class DisplayIndicator : MonoBehaviour
{
    [SerializeField] private Image indicator;
    
    [SerializeField] private Sprite joystick;
    [SerializeField] private Sprite read;
    [SerializeField] private Sprite walk;
    [SerializeField] private Sprite keycard;
    
    public void TurnOn(IconTypes type)
    {
        switch (type)
        {
            case IconTypes.Joystick:
                indicator.sprite = joystick;
                break;
            case IconTypes.Read:
                indicator.sprite = read;
                break;
            case IconTypes.Walk:
                indicator.sprite = walk;
                break;
            case IconTypes.Keycard:
                indicator.sprite = keycard;
                break;
        }

        if (type != IconTypes.None)
            indicator.gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        indicator.gameObject.SetActive(false);
    }
}
