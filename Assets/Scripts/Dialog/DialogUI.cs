using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{

    public Text LeftSpeaker;
    public Text RightSpeaker;
    public Text Box;

    public Sprite BlankSprite;

    public Image Left;
    public Image Right;

    public DialogEvent dialogEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(Sprite Speaker, string SpeakerName, string Content, bool RightSide)
    {
        Left.sprite = BlankSprite;
        Right.sprite = BlankSprite;

        if(RightSide == true)
        {
            if (Speaker != null)
            {
                Right.sprite = Speaker;
            }
            RightSpeaker.text = SpeakerName;

            LeftSpeaker.text = " ";
        }
        else
        {
            if (Speaker != null)
            {
                Left.sprite = Speaker;
            }
            LeftSpeaker.text = SpeakerName;

            RightSpeaker.text = " ";
        }

        Box.text = Content;
    }

    public void ToggleVisible(bool on)
    {
        if(on == true)
        {
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        if(on == false)
        {
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void NextLine()
    {
        dialogEvent.NextLine();
    }
}
