using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static bool movementRelativeToCamOption;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("MovementRelativeCam"))
        {
            PlayerPrefs.SetInt("MovementRelativeCam", 1);
        }
        
        movementRelativeToCamOption = PlayerPrefs.GetInt("MovementRelativeCam") != 0;
    }

    public void ChangeMovementRelativeCam()
    {
        movementRelativeToCamOption = !movementRelativeToCamOption;
        int boolInt = movementRelativeToCamOption ? 1 : 0;
        PlayerPrefs.SetInt("MovementRelativeCam", boolInt);
    }
}
