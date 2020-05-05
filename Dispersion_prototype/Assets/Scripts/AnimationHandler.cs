using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animation anim;
    private string clipName;
    private string reverseName;
    private bool crRunning = false;
    private bool isReversed = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        clipName = anim.clip.name;
        reverseName = clipName + "_reverse";
    }

    public void Play()
    {
        anim.Play();
    }

    public void PlayReverse()
    {
        anim.Play(reverseName);
    }

    public void PlayToggle()
    {
        if (!anim.isPlaying)
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
        while (anim.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(seconds);

        PlayReverse();
        while (anim.isPlaying)
        {
            yield return null;
        }

        crRunning = false;
    }
}
