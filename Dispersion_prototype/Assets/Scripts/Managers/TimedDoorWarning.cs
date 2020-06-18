using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedDoorWarning : MonoBehaviour
{
    public AudioClip warningAudio;
    public GameObject warningUI;

    private AudioSource source = null;
    private Image[] images;
    private int count = 0;
    private float time = 0;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = warningAudio;
        source.volume = 0.5f;
        source.loop = true;

        images = warningUI.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.SetAlpha(0);
        }
    }

    private void Update()
    {
        if (count > 0 || images[0].color.a > 0.01f)
        {
            time += Time.deltaTime * 2;
            float alpha = Mathf.PingPong(time, 1);
            foreach (Image img in images)
            {
                img.SetAlpha(alpha);
            }
            if (count == 0 && images[0].color.a < 0.01f)
            {
                time = 0;
            }
        }
    }

    public void TurnOn()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
        count++;
    }

    public void TurnOff()
    {
        count--;
        if (count == 0)
        {
            source.Stop();
        }
    }
}
