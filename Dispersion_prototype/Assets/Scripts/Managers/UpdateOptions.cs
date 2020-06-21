using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateOptions : MonoBehaviour
{
    public Toggle cameraRelativeMovement;
    public Toggle thoughtBubbles;
    public Toggle thoughtAutoForward;

    void OnEnable()
    {
        cameraRelativeMovement.isOn = OptionsManager.movementRelativeToCamOption;
        thoughtBubbles.isOn = OptionsManager.thoughtBubblesEnabled;
        thoughtAutoForward.isOn = OptionsManager.thoughtAutoForward;
    }
}
