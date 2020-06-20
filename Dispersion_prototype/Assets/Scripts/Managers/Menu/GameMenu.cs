using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public static bool isPaused;

    private PlayerControls input;

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (input.Gameplay.Pause.triggered)
        {
            if (!isPaused)
            {
                pauseMenu.Pause();
                isPaused = true;
            }
            else if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.Resume();
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
