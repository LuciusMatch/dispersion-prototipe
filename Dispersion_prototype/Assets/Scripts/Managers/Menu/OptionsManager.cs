﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public bool movementRelativeToCamOption;

    public static OptionsManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("MovementRelativeCam"))
            movementRelativeToCamOption = PlayerPrefs.GetInt("MovementRelativeCam") != 0;
        else
            movementRelativeToCamOption = true;
    }

    void Start()
    {
        
    }

    public void ChangeMovementRelativeCam()
    {
        movementRelativeToCamOption = !movementRelativeToCamOption;
        int boolInt = movementRelativeToCamOption ? 1 : 0;
        PlayerPrefs.SetInt("MovementRelativeCam", boolInt);
    }
}
