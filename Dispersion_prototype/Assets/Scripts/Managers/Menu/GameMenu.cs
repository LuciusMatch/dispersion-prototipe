using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public static bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!isPaused)
            {
                pauseMenu.Pause();
                isPaused = true;
            }
            else
            {
                pauseMenu.Resume();
            }
        }
    }
}
