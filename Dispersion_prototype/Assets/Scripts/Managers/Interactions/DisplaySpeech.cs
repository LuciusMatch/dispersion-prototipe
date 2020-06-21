using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeech : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject arrow;
    [SerializeField] int speed = 25;

    private int i = 0;
    private List<string> lines;
    private Image image;
    public static bool displaying = false;
    private bool typing = false;
    private bool skip = false;
    private bool fadingIn = false;
    private float t = 100;

    private PlayerControls input;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.SetAlpha(0);
        text.SetAlpha(0);

        input = new PlayerControls();
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

        if (image.color.a >= 0.79f)
        {
            displaying = true;
        } else
        {
            displaying = false;
        }

        t += 4 * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (input.UI.Submit.triggered && displaying)
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
        if (OptionsManager.thoughtBubblesEnabled)
        {
            if (displaying)
            {
                lines.AddRange(speech.lines);
            }
            else
            {
                i = 0;
                lines = new List<string>(speech.lines);
                DisplayNext();
                fadingIn = true;
                text.SetAlpha(1);
                t = 0;
            }
        }
    }

    public void DisplayNext()
    {
        text.text = "";
        if (i < lines.Count)
        {
            StartCoroutine(TypeLine(i));
            i++;
        }
        else
        {
            Close();
        }
    }

    public void Close()
    {
        fadingIn = false;
        t = 0;
    }

    private IEnumerator TypeLine(int lineIndex)
    {
        arrow.SetActive(false);
        if (lineIndex == 0) yield return new WaitForSeconds(0.25f); // waiting for the box to fade in

        typing = true;
        foreach (char letter in lines[lineIndex])
        {
            text.text += letter;
            if (!skip)
            {
                yield return new WaitForSeconds(1f / speed);
            }
        }

        if (lineIndex < lines.Count - 1)
        {
            arrow.SetActive(true);
        }

        typing = false;
        skip = false;

        if (OptionsManager.thoughtAutoForward)
        {
            yield return new WaitForSeconds(2 + lines[lineIndex].Length * 0.03f);

            if (lineIndex + 1 == i && displaying)
            {
                DisplayNext();
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
