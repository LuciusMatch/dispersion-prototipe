using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Anim
{
    public Animation anim;
    public string clipName;
    public string reverseName;
}

public class AnimationHandler : MonoBehaviour
{
    private List<Anim> anims = new List<Anim>();
    public bool crRunning = false;
    public bool isReversed = false;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

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

            if (source != null)
            {
                source.Play();
            }
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

            if (source != null)
            {
                source.Play();
            }
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
}
