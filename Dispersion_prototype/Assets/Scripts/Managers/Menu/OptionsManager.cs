using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static bool movementRelativeToCamOption;
    public static bool thoughtBubblesEnabled;
    public static bool thoughtAutoForward;

    private static readonly string PREFS_MOVEMENT = "MovementRelativeCam";
    private static readonly string PREFS_THOUGHTS = "EnableThoughtBubbles";
    private static readonly string PREFS_THOUGHTS_AUTO = "EnableThoughtsAutoForward";

    void Awake()
    {
        if (!PlayerPrefs.HasKey(PREFS_MOVEMENT))
        {
            PlayerPrefs.SetInt(PREFS_MOVEMENT, 1);
        }
        movementRelativeToCamOption = PlayerPrefs.GetInt(PREFS_MOVEMENT) != 0;


        if (!PlayerPrefs.HasKey(PREFS_THOUGHTS))
        {
            PlayerPrefs.SetInt(PREFS_THOUGHTS, 1);
        }
        thoughtBubblesEnabled = PlayerPrefs.GetInt(PREFS_THOUGHTS) != 0;


        if (!PlayerPrefs.HasKey(PREFS_THOUGHTS_AUTO))
        {
            PlayerPrefs.SetInt(PREFS_THOUGHTS_AUTO, 1);
        }
        thoughtAutoForward = PlayerPrefs.GetInt(PREFS_THOUGHTS_AUTO) != 0;
    }

    public void ChangeMovementRelativeCam()
    {
        movementRelativeToCamOption = !movementRelativeToCamOption;
        int boolInt = movementRelativeToCamOption ? 1 : 0;
        PlayerPrefs.SetInt(PREFS_MOVEMENT, boolInt);
    }

    public void ToggleThoughtBubblesSetting()
    {
        thoughtBubblesEnabled = !thoughtBubblesEnabled;
        int boolInt = thoughtBubblesEnabled ? 1 : 0;
        PlayerPrefs.SetInt(PREFS_THOUGHTS, boolInt);
    }

    public void ToggleThoughtAutoForwardSetting()
    {
        thoughtAutoForward = !thoughtAutoForward;
        int boolInt = thoughtAutoForward ? 1 : 0;
        PlayerPrefs.SetInt(PREFS_THOUGHTS_AUTO, boolInt);
    }
}
