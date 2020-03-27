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
        optionsManager = OptionsManager.instance.GetComponent<OptionsManager>();
        cameraRelativeMovement = transform.Find("LevelRelativeToggle").GetComponent<Toggle>();
        cameraRelativeMovement.isOn = optionsManager.movementRelativeToCamOption;
    }

    void SetOptionsUI()
    {
        cameraRelativeMovement.isOn = !optionsManager.movementRelativeToCamOption;
    }
}
