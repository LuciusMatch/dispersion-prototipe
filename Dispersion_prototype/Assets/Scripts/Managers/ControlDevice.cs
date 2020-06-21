using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ControlDevice : MonoBehaviour
{
    public static InputControlScheme controlScheme;

    void OnEnable()
    {
        InputUser.onChange += onInputDeviceChange;
        controlScheme = (InputControlScheme) InputUser.all[0].controlScheme;
    }

    void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            controlScheme = (InputControlScheme) user.controlScheme;
            Debug.Log("Scheme changed to " + controlScheme.name);
        }
    }
}
