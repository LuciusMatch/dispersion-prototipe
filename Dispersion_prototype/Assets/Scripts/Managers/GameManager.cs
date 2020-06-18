﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public List<GameObject> clones;
    public GameObject mainCamera;
    public Transform checkPointParent;

    public static GameManager Instance { get; private set; }
    public static AudioPlayer audioPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioPlayer = GetComponent<AudioPlayer>();
        }
        else
        {
            Debug.LogWarning("Multiple instances of " + this + " in scene!");
        }
    }

    public static void ButtonPress()
    {
        audioPlayer.ButtonPress();
    }
}
