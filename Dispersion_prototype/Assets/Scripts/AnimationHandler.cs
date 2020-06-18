using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Anim
{
    public Animation anim;
    public string clipName;
    public string reverseName;
}

public enum objectType { Button, Door };

public class AnimationHandler : MonoBehaviour
{
    private List<Anim> anims = new List<Anim>();
    public bool crRunning = false;
    public bool isReversed = false;
    public bool hasSound = false;
    [ConditionalField(nameof(hasSound))] public objectType objType;

    private void Awake()
    {
        foreach (Animation a in GetComponentsInChildren<Animation>())
        {
            Anim newAnim = new Anim();
            newAnim.anim = a;
            newAnim.clipName = a.clip.name;
            newAnim.reverseName = newAnim.clipName + "_reverse";
            anims.Add(newAnim);
        }
    }

    public bool IsPlaying()
    {
        foreach (Anim a in anims)
        {
            if (a.anim.isPlaying)
                return true;
        }

        return false;
    }

    public void Play()
    {
        if (!IsPlaying())
        {
            foreach (Anim a in anims)
            {
                a.anim.Play();
            }
            PlaySound();
        }
    }

    public void PlayReverse()
    {
        if (!IsPlaying())
        {
            foreach (Anim a in anims)
            {
                a.anim.Play(a.reverseName);
            }
            PlaySound();
        }
    }

    public void PlayToggle()
    {
        if (!IsPlaying())
        {
            if (isReversed)
            {
                PlayReverse();
                isReversed = false;
            }
            else
            {
                Play();
                isReversed = true;
            }
        }
    }

    public void PlayAndWaitBeforeReverse(float seconds)
    {
        if (!crRunning)
        {
            StartCoroutine("PlayAndWait", seconds);
        }
    }

    private IEnumerator PlayAndWait(float seconds)
    {
        crRunning = true;

        Play();
        while (IsPlaying())
        {
            yield return null;
        }

        TimedDoorWarning warning = GameManager.Instance.GetComponent<TimedDoorWarning>();
        warning.TurnOn();

        yield return new WaitForSeconds(seconds);

        PlayReverse();
        while (IsPlaying())
        {
            yield return null;
        }

        warning.TurnOff();
        crRunning = false;
    }

    private void PlaySound()
    {
        if (hasSound)
        {
            switch (objType)
            {
                case objectType.Button:
                    GameManager.audioPlayer.ButtonPress();
                    break;
                case objectType.Door:
                    GameManager.audioPlayer.DoorSlide();
                    break;
            }
        }
    }
}
