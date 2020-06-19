using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeech : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] int speed = 25;

    private int i = 0;
    private Speech speech;
    private Image image;
    private bool displaying = false;
    private bool typing = false;
    private bool skip = false;
    private bool fadingIn = false;
    private float t = 100;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.SetAlpha(0);
        text.SetAlpha(0);
    }

    private void Update()
    {
        if (fadingIn)
        {
            image.SetAlpha(Mathf.Lerp(0, 0.8f, t));
        }
        else
        {
            image.SetAlpha(Mathf.Lerp(0.8f, 0, t));
            text.SetAlpha(Mathf.Lerp(1, 0, t));
        }

        t += 4 * Time.deltaTime;

        if (Input.GetButtonDown("Submit") && displaying)
        {
            if (!typing)
            {
                DisplayNext();
            }
            else
            {
                skip = true;
            }
        }
    }

    public void Display(Speech speech)
    {
        i = 0;
        this.speech = speech;
        DisplayNext();
        displaying = true;
        fadingIn = true;
        text.SetAlpha(1);
        t = 0;
    }

    public void DisplayNext()
    {
        text.text = "";
        if (i < speech.lines.Length)
        {
            StartCoroutine(TypeLine(speech.lines[i]));
            i++;
        }
        else
        {
            Close();
        }
    }

    public void Close()
    {
        displaying = false;
        fadingIn = false;
        t = 0;
    }

    private IEnumerator TypeLine(string line)
    {
        if (i == 0) yield return new WaitForSeconds(0.25f); // waiting for the box to fade in

        typing = true;
        foreach (char letter in line)
        {
            text.text += letter;
            if (!skip)
            {
                yield return new WaitForSeconds(1f / speed);
            }
        }
        typing = false;
        skip = false;
    }
}
