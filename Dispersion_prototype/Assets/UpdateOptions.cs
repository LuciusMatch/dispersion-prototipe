using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateOptions : MonoBehaviour
{
    OptionsManager optionsManager;
    Toggle cameraRelativeMovement;
    void Start()
    {
        optionsManager = GameObject.Find("Options Manager").GetComponent<OptionsManager>();
        cameraRelativeMovement = transform.Find("LevelRelativeToggle").GetComponent<Toggle>();
        SetOptionsUI();
    }

    void SetOptionsUI()
    {
        cameraRelativeMovement.isOn = !optionsManager.movementRelativeToCamOption;
    }
}
