using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationHandler : MonoBehaviour
{
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void Play()
    {
        anim.Play();
    }
}
