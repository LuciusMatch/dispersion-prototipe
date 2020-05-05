using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animation anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
    }

    public void Play()
    {
        anim.Play();
    }
}
