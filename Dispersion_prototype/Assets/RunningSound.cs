using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSound : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource source = animator.gameObject.GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource source = animator.gameObject.GetComponent<AudioSource>();
        if (source != null)
        {
            source.Stop();
        }
    }
}
