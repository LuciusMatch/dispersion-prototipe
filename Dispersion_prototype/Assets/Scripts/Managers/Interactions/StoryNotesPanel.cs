using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryNotesPanel : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text author;
    [SerializeField] private Text text;

    private AnimationHandler anim;
    private bool open = false;

    private void Awake()
    {
        anim = GetComponent<AnimationHandler>();
    }

    public void ToggleNote(StoryNote note)
    {
        title.text = note.title;
        author.text = note.author;
        text.text = note.text;

        anim.PlayToggle();
        open = !open;
    }

    public void CloseNote()
    {
        if (open)
        {
            anim.PlayReverse();
            open = false;
        }
    }
}
