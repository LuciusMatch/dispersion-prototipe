using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateOptions : MonoBehaviour
{
    OptionsManager optionsManager;
    public Toggle cameraRelativeMovement;
    void Start()
    {
        cameraRelativeMovement = transform.Find("LevelRelativeToggle").GetComponent<Toggle>();
        cameraRelativeMovement.isOn = OptionsManager.movementRelativeToCamOption;
    }

    void SetOptionsUI()
    {
        cameraRelativeMovement.isOn = !OptionsManager.movementRelativeToCamOption;
    }
}
