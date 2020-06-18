using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] playlist;
    private AudioSource source;
    private int nextId = 0;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
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
