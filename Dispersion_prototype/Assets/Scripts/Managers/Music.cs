using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioMixerGroup group;
    private AudioSource source;
    private int nextId = 0;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = group;
        source.ignoreListenerPause = true;
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(playlist[nextId]);
            nextId = (nextId + 1) % playlist.Length;
        }
    }
}
