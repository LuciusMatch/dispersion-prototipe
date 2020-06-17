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
        foreach (Anim a in anims)
        {
            a.anim.Play();
        }
    }

    public void PlayReverse()
    {
        foreach (Anim a in anims)
        {
            a.anim.Play(a.reverseName);
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

        yield return new WaitForSeconds(seconds);

        PlayReverse();
        while (IsPlaying())
        {
            yield return null;
        }

        crRunning = false;
    }
}
