using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip buttonPress;
    [SerializeField] private AudioClip doorSlide;
    [SerializeField] private AudioClip keycardPickedUp;
    [SerializeField] private AudioClip accessDenied;
    [SerializeField] private AudioClip cloning;
    [SerializeField] private AudioClip movingWalls;
    [SerializeField] private AudioClip zap;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ButtonPress()
    {
        source.PlayOneShot(buttonPress);
    }

    public void DoorSlide()
    {
        source.PlayOneShot(doorSlide);
    }

    public void KeycardPickedUp()
    {
        source.PlayOneShot(keycardPickedUp);
    }

    public void AccessDenied()
    {
        source.PlayOneShot(accessDenied);
    }

    public void Cloning()
    {
        source.PlayOneShot(cloning);
    }

    public void MovingWalls()
    {
        source.PlayOneShot(movingWalls);
    }

    public void ElectricShock()
    {
        source.PlayOneShot(zap);
    }
}
