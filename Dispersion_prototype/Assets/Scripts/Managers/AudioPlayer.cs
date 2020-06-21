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
    private float doorStartTime = 0;

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
        if ((Time.time - doorStartTime) >= doorSlide.length)
        {
            source.PlayOneShot(doorSlide);
            doorStartTime = Time.time;
        }
    }

    public void KeycardPickedUp()
    {
        source.PlayOneShot(keycardPickedUp, .7f);
    }

    public void AccessDenied()
    {
        source.PlayOneShot(accessDenied, .8f);
    }

    public void Cloning()
    {
        source.PlayOneShot(cloning, .8f);
    }

    public void MovingWalls()
    {
        source.PlayOneShot(movingWalls, .5f);
    }

    public void ElectricShock()
    {
        source.PlayOneShot(zap, .7f);
    }
}
